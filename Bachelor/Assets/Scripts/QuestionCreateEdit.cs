using System.Collections;
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
        imageUrlText.text = question.GetImageUrl();
        questionText.text = question.GetQuestion();
        string[] existingOptions = question.GetOptions();
        bool[] existingIsOptions = question.GetIsOptions();
        for (int i = 0; i < options.Length; i++)
        {
            options[i].text = existingOptions[i];
        }
        for (int i = 0; i < correctAnswer.Length; i++)
        {
            correctAnswer[i].SetActive(existingIsOptions[i]);
        }
    }

    public void Save()
    {
        StartCoroutine(EditQuestion());
    }

    private IEnumerator EditQuestion()
    {
        Question question = new Question();
        switch (GameDataManager.Instance.GetGameData().ActiveLevel.LevelType)
        {
            case GameMode.Standard:
                question = EditStandardQuestion();
                break;
            case GameMode.TrueOrFalse:
                question = EditTrueOrFalseQuestion();
                break;
            case GameMode.Rank:
                question = EditRankQuestion();
                break;
        }

        if (question.GetId() == 0)
        { 
            yield return WebCommunicationUtil.PutNewGameDataRequest(
                null, GameDataManager.Instance.GetGameData().ActiveLevel, question, "/question");
        }
        else
        {
            yield return WebCommunicationUtil.PutUpdateGameDataRequest(null, null, question, "/question");
        }
        yield return EditToolScriptManager.Instance.Refresh();
    }

    private Question EditStandardQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        question.SetImageUrl(imageUrlText.text);
        question.SetQuestion(questionText.text);
        
        question.SetOption0(options[0].text);
        question.SetOption1(options[1].text);
        question.SetOption2(options[2].text);
        question.SetOption3(options[3].text);
        
        question.SetIsOption0(correctAnswer[0]);
        question.SetIsOption1(correctAnswer[1]);
        question.SetIsOption2(correctAnswer[2]);
        question.SetIsOption3(correctAnswer[3]);
        return question;
    }

    private Question EditTrueOrFalseQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        question.SetImageUrl(imageUrlText.text);
        question.SetQuestion(questionText.text);

        question.SetIsOption0(correctAnswer[0]);
        question.SetIsOption2(correctAnswer[1]);

        question.SetOption0("Sant");
        question.SetOption1("Usant");
        return question;
    }

    private Question EditRankQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        question.SetQuestion(questionText.text);
        
        question.SetOption0(options[0].text);
        question.SetOption1(options[1].text);
        question.SetOption2(options[2].text);
        question.SetOption3(options[3].text);
        return question;
    }
}
