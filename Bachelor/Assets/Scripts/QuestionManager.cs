using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    private Question[] _questions =
    {
        new("Hvilken kategori faller banan under?", "A. Bær", "B. Grønnsak", "C. Frukt", "D. Kjøtt", new[]{0}),
        new("Fish", "a", "b", "c", "d", new[]{0}),
        new("Vegetable", "a", "b", "c", "d", new[]{0}),
        new("Fruit", "a", "b", "c", "d", new[]{0})
    };
    
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answers;
        
    private Question currentQuestion;
    private int nextQuestionIndex;
        
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
        nextQuestionIndex = 0;
        NextQuestion();
    }
        
    public bool Answer(int answer, bool moveOnIfWrong)
    {
        bool correct = answer == currentQuestion.GetCorrectOption() ||
                       currentQuestion.GetCorrectOptions().Contains(answer);

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
        bool passed = true; //TODO: check if score is sufficient
        if (nextQuestionIndex >= _questions.Length)
        {
            LevelManager.Instance.EndGame(passed);
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
        for (int i = 0; i < answers.Length; i++) answers[i].text = currentQuestion.GetOptions()[i];
    }
}