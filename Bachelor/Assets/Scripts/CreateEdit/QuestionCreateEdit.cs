using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

// Class responsible for the editing and creation of new questions.
public class QuestionCreateEdit : MonoBehaviour
{
    [SerializeField] private TMP_InputField imageUrlText;
    [SerializeField] private TMP_InputField questionText;
    [SerializeField] private TMP_InputField[] options;
    [SerializeField] private EnableDisableIcons[] correctAnswer;

    // Fill in any existing question data on the page.
    public void LoadQuestion(Question question)
    {
        if (imageUrlText != null) imageUrlText.text = question.ImageUrl;
        if (questionText != null) questionText.text = question.QuestionText;
        string[] existingOptions = question.GetOptions();
        bool[] existingIsOptions = question.GetIsOptions();
        for (int i = 0; i < options.Length; i++) options[i].text = existingOptions[i];
        for (int i = 0; i < correctAnswer.Length; i++) correctAnswer[i].SetActive(existingIsOptions[i]);
    }

    // Start saving changes to the question.
    public void Save() { StartCoroutine(EditQuestion()); }

    // For editing/creating and saving questions.
    private IEnumerator EditQuestion()
    {
        Question question = new Question();
        
        // Check if the question field is filled (questionText should not be null if question statement is required).
        if (questionText != null)
        {
            if (questionText.text == "")
            {
                // Notify the users that the question statement is missing.
                EditToolScriptManager.Instance.DisplayPopup(
                    "Mangler spørsmål!",
                    "Vennligst oppgi et Spørsmål før du fortsetter.",
                    false);
                yield break;
            }
        }

        // Determine the type of the question.
        question = GameDataManager.Instance.GetGameData().ActiveLevel.LevelType switch
        {
            GameMode.Standard => EditStandardQuestion(),
            GameMode.TrueOrFalse => EditTrueOrFalseQuestion(),
            GameMode.Rank => EditRankQuestion(),
            GameMode.MemoryCards => EditMemoryQuestion(),
            _ => question
        };
        
        // If the editing of the question fail, stop executing this function.
        if (question == null) yield break;
        
        GameDataManager.Instance.GetGameData().ActiveQuestion = question;

        /*
         * New Questions (questions that are not saved to the backend yet) always have ID == 0.
         * Send a request to add a new question to the backend if the question that is being saved is new.
         */
        if (question.ID == 0) yield return WebCommunicationUtil.PutNewGameDataRequest(
                null, GameDataManager.Instance.GetGameData().ActiveLevel, question, "/question");
        
        // If the level is already in the backend, send the updated version of it to the backend.
        else yield return WebCommunicationUtil.PutUpdateGameDataRequest(
                null, null, question, "/question");
        
        // Refresh the data held locally in the game.
        yield return EditToolScriptManager.Instance.Refresh();
        
        // Notify the user that all changes have been saved.
        EditToolScriptManager.Instance.DisplayPopup(
            "Lagret!",
            "Alle endringer er lagret, du kan nå forlate denne siden.",
            false);
    }

    // For editing/creating questions under a level utilizing the standard quiz format.
    private Question EditStandardQuestion()
    {
        // The standard quiz format requires at least one correct option.
        if (!correctAnswer[0].isEnabled && !correctAnswer[1].isEnabled &&
            !correctAnswer[2].isEnabled && !correctAnswer[3].isEnabled)
        {
            EditToolScriptManager.Instance.DisplayPopup(
                "Mangler riktig svar!",
                "Vennligst oppgi minst ett korrekt svarsalternativ.",
                false);
            return null;
        }

        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        
        // Updates the questions values.
        
        question.ImageUrl = imageUrlText.text;
        question.QuestionText = questionText.text;
        
        question.Option0 = options[0].text;
        question.Option1 = options[1].text;
        question.Option2 = options[2].text;
        question.Option3 = options[3].text;
        
        question.IsOption0 = correctAnswer[0].isEnabled;
        question.IsOption1 = correctAnswer[1].isEnabled;
        question.IsOption2 = correctAnswer[2].isEnabled;
        question.IsOption3 = correctAnswer[3].isEnabled;
        
        return question;
    }

    // For editing/creating questions under a level utilizing the true or false quiz format.
    private Question EditTrueOrFalseQuestion()
    {
        // The true or false quiz format requires at least one correct option.
        if (!correctAnswer[0].isEnabled && !correctAnswer[1].isEnabled)
        {
            EditToolScriptManager.Instance.DisplayPopup(
                "Mangler riktig svar!",
                "Vennligst oppgi minst ett korrekt svarsalternativ.",
                false);
            return null;
        }
        
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        
        // Updates the questions values.
        
        question.ImageUrl = imageUrlText.text;
        question.QuestionText = questionText.text;

        question.IsOption0 = correctAnswer[0].isEnabled;
        question.IsOption1 = correctAnswer[1].isEnabled;

        question.Option0 = options[0].text;
        question.Option1 = options[1].text;
        
        return question;
    }

    // For editing/creating questions under a level utilizing the rank quiz format.
    private Question EditRankQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        question.QuestionText = questionText.text;

        question.Option0 = options[0].text;
        question.Option1 = options[1].text;
        question.Option2 = options[2].text;
        question.Option3 = options[3].text;
        
        return question;
    }

    // For editing/creating questions under a level utilizing the memory quiz format.
    private Question EditMemoryQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        
        // A pair of cards require that both cards contain either text or an image.
        if ((options[0].text == "" && options[2].text == "") || (options[1].text == "" && options[2].text == ""))
        {
            EditToolScriptManager.Instance.DisplayPopup(
                "Mangler kort!",
                "Vennligst utfyll ett bilde eller text felt for hvert kort.",
                false);
            
            return null;
        }
        
        // Updates the questions values.
        
        question.Option0 = options[0].text;
        question.Option1 = options[1].text;
        question.Option2 = options[2].text;
        question.Option3 = options[3].text;
        
        return question;
    }

    /*
     * Attempt to leave the question create edit page.
     * If there are any unsaved changes, the user will get a confirmation popup.
     */
    public void LeaveQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        
        if (question.ID == 0)
        {
            NotSavedPopup(); 
            return;
        } 
        if (questionText != null)
        {
            if (question.QuestionText != questionText.text)
            {
                NotSavedPopup();
                return;
            }
        }
        if (imageUrlText != null)
        {
            if (question.ImageUrl != imageUrlText.text)
            {
                NotSavedPopup();
                return;
            }
        }
        string[] questionOptions = question.GetOptions();
        if (options.Where((t, i) => t.text != questionOptions[i]).Any())
        {
            NotSavedPopup();
            return;
        }

        bool[] questionIsOptions = question.GetIsOptions();
        if (correctAnswer.Where((t, i) => t.isEnabled != questionIsOptions[i]).Any())
        {
            NotSavedPopup();
            return;
        }
        
        // Leave the question edit create page immediately as no changes has been made.
        EditToolScriptManager.Instance.Back();
    }

    private static void NotSavedPopup()
    {
        EditToolScriptManager.Instance.DisplayPopup(
            "Ikke-lagrede endringer!",
            "Du har en eller flere ikke-lagrede endringer, er du sikker på at du vil fortsette?",
            true);
    }
}
