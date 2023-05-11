using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    private List<Question> _questions = new ();

    public Image questionPicture;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answers;
    public bool scrambleAnswers;
    public bool Locked = false;
    public Transform listParent;
        
    private Question currentQuestion;
    private int nextQuestionIndex;
    private int questionAmount;
    private string[] options;
    private List<string> imageUrls = new ();
    private List<Sprite> images = new ();
        
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
        
    private IEnumerator Start()
    {
        _questions = GameDataManager.Instance.GetGameData().ActiveLevel.Questions;
        yield return GetImages();
        questionAmount = _questions.Count;
        nextQuestionIndex = 0;
        NextQuestion();
    }

    private IEnumerator GetImages()
    {
        foreach (Question question in _questions) imageUrls.Add(question.GetImageUrl());
        yield return WebCommunicationUtil.GetImagesFromUrls(images, imageUrls);
    }

    public bool Answer(int answer, bool moveOnIfWrong)
    {
        bool[] isOptions = currentQuestion.GetIsOptions();
        bool correct = isOptions[answer];

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
            StartCoroutine(EndGame());
            return;
        }
        currentQuestion = _questions[nextQuestionIndex];
        nextQuestionIndex++;

        StartCoroutine(PushTextOnScreen());
    }

    private static IEnumerator EndGame()
    {
        yield return WaitOneSecond();
        LevelManager.Instance.EndGame();
    }

    private static IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator PushTextOnScreen()
    {
        yield return WaitOneSecond();
        DisplayNewQuestion();
    }

    private void DisplayNewQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        if (questionPicture != null)
        {
            if (currentQuestion.GetImageUrl() != "")
            {
                questionPicture.sprite = images[nextQuestionIndex - 1];
                questionPicture.color = new Color32(255,255,255,255);
            }
            else
            {
                questionPicture.sprite = null;
                questionPicture.color = new Color32(82,82,82,255);
            }
        }
        if (!scrambleAnswers)
        {
            options = currentQuestion.GetOptions();
        }
        else
        {
            System.Random rand = new System.Random();
            options = currentQuestion.GetOptions().OrderBy(_ => rand.Next()).ToArray();
        }
        for (int i = 0; i < answers.Length; i++) answers[i].text = options[i];
    }
    
    public void ConfirmListAnswer(bool orderMatters)
    {
        if (Locked || PauseManager.Instance.isPaused) return;
        Locked = true;
        if (!orderMatters) Answer(true, true);
        else
        {
            string[] correctOrder = currentQuestion.GetOptions();
            for (int i = 0; i < correctOrder.Length; i++)
            {
                if (correctOrder[i] == listParent.GetChild(i).GetComponent<ListDragController>().textEntry.text) 
                    ScoreManager.Instance.ChangeScore(1);
                else
                {
                    Transform answer = listParent.GetChild(i).transform;
                    answer.GetComponent<Image>().color = new Color32(244,140,81,255);
                    answer.GetComponent<Shadow>().effectColor = new Color32(216,108,48,255);
                }
            }
        }

        Answer(false, true);
        StartCoroutine(ButtonReset());
    }

    private IEnumerator ButtonReset()
    {
        yield return WaitOneSecond();
        foreach (TextMeshProUGUI answer in answers)
        {
            Transform parent = answer.transform.parent;
            parent.GetComponent<Image>().color = new Color32(77,161,223,255);
            parent.GetComponent<Shadow>().effectColor = new Color32(32,112,172,255);
        }
        Locked = false;
        yield return null;
    }

    public Question CurrentQuestion
    {
        get => currentQuestion;
        set => currentQuestion = value;
    }

    public int QuestionAmount => questionAmount;
}