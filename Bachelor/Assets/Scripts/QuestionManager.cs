using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    private List<Question> _questions = new ();
    
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answers;
        
    private Question currentQuestion;
    private int nextQuestionIndex;
    private int questionAmount;
        
    public static QuestionManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(QuestionManager)) as QuestionManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static QuestionManager instance;
        
    private void Start()
    {
        _questions = GameDataManager.Instance.GetGameData().ActiveLevel.Questions;
        questionAmount = _questions.Count;
        nextQuestionIndex = 0;
        NextQuestion();
    }
        
    public bool Answer(int answer, bool moveOnIfWrong)
    {
        bool[] options = currentQuestion.GetIsOptions();
        bool correct = options[answer];

        Answer(correct, moveOnIfWrong);
        return correct;
    }
        
    public void Answer(bool answer, bool moveOnIfWrong)
    {
        if (answer) ScoreManager.Instance.ChangeScore(1);
        if (moveOnIfWrong || answer) NextQuestion();
    }

    private void NextQuestion()
    {
        if (nextQuestionIndex >= _questions.Count)
        {
            LevelManager.Instance.EndGame();
            return;
        }
        currentQuestion = _questions[nextQuestionIndex];
        nextQuestionIndex++;

        StartCoroutine(PushTextOnScreen());
    }
        

    private IEnumerator PushTextOnScreen()
    {
        yield return new WaitForSeconds(0.50f);
        DisplayNewQuestion();
    }

    private void DisplayNewQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        string[] options = currentQuestion.GetOptions();
        for (int i = 0; i < answers.Length; i++) answers[i].text = options[i];
    }

    public Question CurrentQuestion
    {
        get => currentQuestion;
        set => currentQuestion = value;
    }

    public int QuestionAmount => questionAmount;
}