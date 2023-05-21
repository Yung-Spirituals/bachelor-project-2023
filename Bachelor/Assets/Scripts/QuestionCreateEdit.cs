using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestionCreateEdit : MonoBehaviour
{
    [SerializeField] private TMP_InputField imageUrlText;
    [SerializeField] private TMP_InputField questionText;
    [SerializeField] private TMP_InputField[] options;
    [SerializeField] private EnableDisableIcons[] correctAnswer;

    public void LoadQuestion(Question question)
    {
        if (imageUrlText != null) imageUrlText.text = question.ImageUrl;
        if (questionText != null) questionText.text = question.QuestionText;
        string[] existingOptions = question.GetOptions();
        bool[] existingIsOptions = question.GetIsOptions();
        for (int i = 0; i < options.Length; i++) options[i].text = existingOptions[i];
        for (int i = 0; i < correctAnswer.Length; i++) correctAnswer[i].SetActive(existingIsOptions[i]);
    }

    public void Save() { StartCoroutine(EditQuestion()); }

    private IEnumerator EditQuestion()
    {
        Question question = new Question();
        if (questionText != null)
        {
            if (questionText.text == "")
            {
                EditToolScriptManager.Instance.DisplayPopup(
                    "Mangler spørsmål!",
                    "Vennligst oppgi et Spørsmål før du fortsetter.",
                    false);
            }
        }

        question = GameDataManager.Instance.GetGameData().ActiveLevel.LevelType switch
        {
            GameMode.Standard => EditStandardQuestion(),
            GameMode.TrueOrFalse => EditTrueOrFalseQuestion(),
            GameMode.Rank => EditRankQuestion(),
            GameMode.MemoryCards => EditMemoryQuestion(),
            _ => question
        };
        
        if (question == null) yield break;
        
        GameDataManager.Instance.GetGameData().ActiveQuestion = question;

        if (question.ID == 0) yield return WebCommunicationUtil.PutNewGameDataRequest(
                null, GameDataManager.Instance.GetGameData().ActiveLevel, question, "/question");
        
        else yield return WebCommunicationUtil.PutUpdateGameDataRequest(
                null, null, question, "/question");
        
        yield return EditToolScriptManager.Instance.Refresh();
        EditToolScriptManager.Instance.DisplayPopup(
            "Lagret!",
            "Alle endringer er lagret, du kan nå forlate denne siden.",
            false);
    }

    private Question EditStandardQuestion()
    {
        if (!correctAnswer[0].isEnabled && !correctAnswer[1].isEnabled &&
            !correctAnswer[2].isEnabled && !correctAnswer[3].isEnabled)
        {
            EditToolScriptManager.Instance.DisplayPopup(
                "Mangler riktig svar!",
                "Vennligst oppgi minst ett korrekt svarsalternativ.",
                false);
        }

        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        
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

    private Question EditTrueOrFalseQuestion()
    {
        if (!correctAnswer[0].isEnabled && !correctAnswer[1].isEnabled)
        {
            EditToolScriptManager.Instance.DisplayPopup(
                "Mangler riktig svar!",
                "Vennligst oppgi minst ett korrekt svarsalternativ.",
                false);
        }
        
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        
        question.ImageUrl = imageUrlText.text;
        question.QuestionText = questionText.text;

        question.IsOption0 = correctAnswer[0].isEnabled;
        question.IsOption1 = correctAnswer[1].isEnabled;

        question.Option0 = options[0].text;
        question.Option1 = options[1].text;
        
        return question;
    }

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

    private Question EditMemoryQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        if ((options[0].text == "" && options[2].text == "") || (options[1].text == "" && options[2].text == ""))
        {
            EditToolScriptManager.Instance.DisplayPopup(
                "Mangler kort!",
                "Vennligst utfyll ett bilde eller text felt for hvert kort.",
                false);
            
            return null;
        }
        
        question.Option0 = options[0].text;
        question.Option1 = options[1].text;
        question.Option2 = options[2].text;
        question.Option3 = options[3].text;
        
        return question;
    }

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
