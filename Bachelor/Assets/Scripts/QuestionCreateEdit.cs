using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        for (int i = 0; i < existingIsOptions.Length; i++)
        {
            correctAnswer[i].SetActive(existingIsOptions[i]);
        }
        GameDataManager.Instance.GetGameData().ActiveQuestion = question;
    }

    public void EditQuestion()
    {
        switch (GameDataManager.Instance.GetGameData().ActiveLevel.LevelType)
        {
            case GameMode.Standard:
                EditStandardQuestion();
                break;
            case GameMode.TrueOrFalse:
                EditTrueOrFalseQuestion();
                break;
            case GameMode.Rank:
                EditRankQuestion();
                break;
        }

        StartCoroutine(WebCommunicationUtil.PutUpdateGameDataRequest(null, 
            GameDataManager.Instance.GetGameData().ActiveLevel,
            GameDataManager.Instance.GetGameData().ActiveQuestion));
        EditToolScriptManager.Instance.Save();
    }

    private void EditStandardQuestion()
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
        
        GameDataManager.Instance.GetGameData().ActiveQuestion = question;
    }

    private void EditTrueOrFalseQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        question.SetImageUrl(imageUrlText.text);
        question.SetQuestion(questionText.text);

        question.SetIsOption0(correctAnswer[0]);
        question.SetIsOption2(correctAnswer[1]);

        question.SetOption0("Sant");
        question.SetOption1("Usant");
        
        GameDataManager.Instance.GetGameData().ActiveQuestion = question;
    }

    private void EditRankQuestion()
    {
        Question question = GameDataManager.Instance.GetGameData().ActiveQuestion;
        question.SetQuestion(questionText.text);
        
        question.SetOption0(options[0].text);
        question.SetOption1(options[1].text);
        question.SetOption2(options[2].text);
        question.SetOption3(options[3].text);
        
        GameDataManager.Instance.GetGameData().ActiveQuestion = question;
    }
}
