using System.Collections.Generic;

public class Question
{
    private string _question = "";
    private string[] _options = {};
    private int[] _correctOptions = {};
    private int _correctOption = 255;

    public Question(string question,
        string option0, string option1, string option2, string option3, int[] correctOptions)
    {
        _question = question;
        _options = new[] { option0, option1, option2, option3 };
        _correctOptions = correctOptions;
    }
        
    public Question(string question,
        string option0, string option1, string option2, string option3, int correctOption)
    {
        _question = question;
        _options = new[] { option0, option1, option2, option3 };
        _correctOption = correctOption;
    }

    public Question(string question, string option0, string option1, int correctOption)
    {
        _question = question;
        _options = new[] { option0, option1 };
        _correctOption = correctOption;
    }

    public string GetQuestion() { return _question; }

    public string[] GetOptions() { return _options; }

    public IEnumerable<int> GetCorrectOptions() { return _correctOptions; }
        
    public int GetCorrectOption() { return _correctOption; }
}