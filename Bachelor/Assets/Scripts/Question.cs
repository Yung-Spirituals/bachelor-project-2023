using System;
using UnityEngine;

[Serializable]
public class Question
{
    [SerializeField] private string _question = "";
    [SerializeField] private string[] _options = {};
    [SerializeField] private int[] _correctOptions = {};
    [SerializeField] private int _correctOption = 255;

    public Question(string question,
        string option0, string option1, string option2, string option3, int[] correctOptions)
    {
        _question = question;
        _options = new[] { option0, option1, option2, option3 };
        _correctOptions = correctOptions;
    }
    
    public Question(string question,
        string option0, string option1, int[] correctOptions)
    {
        _question = question;
        _options = new[] { option0, option1 };
        _correctOptions = correctOptions;
    }
        
    public Question(string question,
        string option0, string option1, string option2, string option3, int correctOption)
    {
        _question = question;
        _options = new[] { option0, option1, option2, option3 };
        _correctOption = correctOption;
        _correctOptions = new[] { correctOption };
    }

    public Question(string question, string option0, string option1, int correctOption)
    {
        _question = question;
        _options = new[] { option0, option1 };
        _correctOption = correctOption;
        _correctOptions = new[] { correctOption };
    }

    public string GetQuestion() { return _question; }
    public void SetQuestion(string question) { _question = question; }
    public string[] GetOptions() { return _options; }
    public void SetOptions(string[] options) { _options = options; }
    public int[] GetCorrectOptions() { return _correctOptions; }
    public void SetCorrectOptions(int[] correctOptions) { _correctOptions = correctOptions; }
    public int GetCorrectOption() { return _correctOption; }
    public void SetCorrectOption(int correctOption)
    {
        _correctOption = correctOption;
        _correctOptions = new[] { correctOption };
    }
}