﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Question CurrentQuestion { get; private set; }
    
    private List<Question> _questions = new ();

    public Image questionPicture;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answers;
    public bool scrambleAnswers;
    public bool locked;
    public Transform listParent;

    private int _nextQuestionIndex;
    private int _questionAmount;
    private string[] _options;
    private readonly List<string> _imageUrls = new ();
    private readonly List<Sprite> _images = new ();
        
    public static QuestionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(QuestionManager)) as QuestionManager;
 
            return _instance;
        }
        set => _instance = value;
    }
    private static QuestionManager _instance;
        
    private IEnumerator Start()
    {
        LevelManager.Instance.SetLoading(true);
        _questions = GameDataManager.Instance.GetGameData().ActiveLevel.Questions;
        yield return GetImages();
        LevelManager.Instance.SetLoading(false);
        _questionAmount = _questions.Count;
        _nextQuestionIndex = 0;
        NextQuestion();
    }

    private IEnumerator GetImages()
    {
        foreach (Question question in _questions) _imageUrls.Add(question.ImageUrl);
        yield return WebCommunicationUtil.GetImagesFromUrls(_images, _imageUrls);
    }

    public bool Answer(int answer, bool moveOnIfWrong)
    {
        bool[] isOptions = CurrentQuestion.GetIsOptions();
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
        if (_nextQuestionIndex >= _questions.Count)
        {
            StartCoroutine(EndGame());
            return;
        }
        CurrentQuestion = _questions[_nextQuestionIndex];
        _nextQuestionIndex++;
        if (_nextQuestionIndex == 1) DisplayNewQuestion();
        else StartCoroutine(PushTextOnScreen());
    }

    private IEnumerator EndGame()
    {
        yield return WaitOneSecond();
        int possiblePoints;
        if (scrambleAnswers) possiblePoints =  _questionAmount * 4;
        else possiblePoints = _questionAmount;
        LevelManager.Instance.EndGame(possiblePoints);
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
        questionText.text = CurrentQuestion.QuestionText;
        if (questionPicture != null)
        {
            if (CurrentQuestion.ImageUrl != "")
            {
                questionPicture.sprite = _images[_nextQuestionIndex - 1];
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
            _options = CurrentQuestion.GetOptions();
        }
        else
        {
            System.Random rand = new System.Random();
            _options = CurrentQuestion.GetOptions().OrderBy(_ => rand.Next()).ToArray();
        }
        for (int i = 0; i < answers.Length; i++) answers[i].text = _options[i];
    }
    
    public void ConfirmListAnswer(bool orderMatters)
    {
        if (locked || PauseManager.Instance.isPaused) return;
        locked = true;
        if (!orderMatters) Answer(true, true);
        else
        {
            string[] correctOrder = CurrentQuestion.GetOptions();
            for (int i = 0; i < correctOrder.Length; i++)
            {
                Transform answer = listParent.GetChild(i);
                if (correctOrder[i] == listParent.GetChild(i).GetComponent<ListDragController>().textEntry.text)
                {
                    ScoreManager.Instance.ChangeScore(1);
                    answer.GetComponent<EnableDisableIcons>().SetActive(true);
                    answer.GetComponent<EnableDisableIcons>().SetGameObjectActive(true);
                }
                else
                {
                    answer.GetComponent<Image>().color = new Color32(244,140,81,255);
                    answer.GetComponent<Shadow>().effectColor = new Color32(216,108,48,255);
                    answer.GetComponent<EnableDisableIcons>().SetActive(false);
                    answer.GetComponent<EnableDisableIcons>().SetGameObjectActive(true);
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
            parent.GetComponent<EnableDisableIcons>().SetGameObjectActive(false);
        }
        locked = false;
        yield return null;
    }
}