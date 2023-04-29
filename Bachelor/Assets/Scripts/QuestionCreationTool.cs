using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCreationTool : MonoBehaviour
{
    [SerializeField] private TMP_InputField story;
    [SerializeField] private TMP_InputField level;
    [SerializeField] private TMP_InputField question;
    [SerializeField] private TMP_InputField[] options;
    [SerializeField] private Toggle[] correctAnswer;
    [SerializeField] private GameObject questionDisplayPrefab;
    [SerializeField] private GameObject questionDisplayList;
    
    private List<Question> _questions = new ();
    private Question _selectedQuestion = null;

    public void CreateQuestion(bool trueOrFalse)
    {
        if (_selectedQuestion != null)
        {
            
        }

        int[] correctOptions = GetCorrectOptions();
        string questionText = question.text;
        string option0 = options[0].text;
        string option1 = options[1].text;
        if (!trueOrFalse)
        {
            string option2 = options[2].text;
            string option3 = options[3].text;
            _questions.Add(new Question(questionText, option0, option1, option2, option3, correctOptions));
        }
        else
        {
            _questions.Add(new Question(questionText, option0, option1, correctOptions));
        }
        
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        question.text = "";
        foreach (TMP_InputField option in options) option.text = "";
        foreach (Toggle toggle in correctAnswer) toggle.isOn = false;

        DisplayQuestions();
    }

    private int[] GetCorrectOptions()
    {
        List<int> ints = new List<int>();
        for (int i = 0; i < correctAnswer.Length; i++) if (correctAnswer[i].isOn) ints.Add(i);
        return ints.ToArray();
    }

    public void LoadQuestions()
    {
        _questions = JsonUtil.LoadJson(story.text, level.text) ?? new List<Question>();
        DisplayQuestions();
    }
    
    public void SaveQuestions() { JsonUtil.SaveJson(story.text, level.text, _questions); }

    private void DisplayQuestions()
    {
        ClearDisplayedQuestions();

        foreach (Question q in _questions)
        {
            GameObject go = Instantiate(questionDisplayPrefab, questionDisplayList.transform);
            QuestionDisplay qd = go.GetComponent<QuestionDisplay>();
            qd.SetQuestion(q);
            qd.DisplayQuestion();
        }
    }

    public void ClearDisplayedQuestions()
    {
        List<GameObject> children =
            (from Transform child in questionDisplayList.transform select child.gameObject).ToList();
        children.ForEach(Destroy);
    }

    public void SelectQuestion(Question selectedQuestion)
    {
        _selectedQuestion = _questions.Find(q => ReferenceEquals(q, selectedQuestion));
        
        question.text = selectedQuestion.GetQuestion();
        
        string[] selectedQuestionOptions = selectedQuestion.GetOptions();
        for (int i = 0; i < options.Length; i++) options[i].text = selectedQuestionOptions[i];
 
        int[] correctOptions = selectedQuestion.GetCorrectOptions();
        for (int i = 0; i < correctAnswer.Length; i++) correctAnswer[i].isOn = correctOptions.Contains(i);
    }

    public void DeselectQuestion() { _selectedQuestion = null; }

    public void DeleteQuestion()
    {
        if (_selectedQuestion == null) return;
        _questions.Remove(_selectedQuestion);
        _selectedQuestion = null;
        UpdateDisplay();
    }
}