using System;
using UnityEngine;

[Serializable]
public class Question
{
    [SerializeField] private long id;
    
    [NonSerialized] private Level _level;
    
    [SerializeField] private string _question = "";
    [SerializeField] private string _imageUrl = "";
    
    [SerializeField] private string _option0 = "";
    [SerializeField] private string _option1 = "";
    [SerializeField] private string _option2 = "";
    [SerializeField] private string _option3 = "";

    [SerializeField] private bool _isOption0;
    [SerializeField] private bool _isOption1;
    [SerializeField] private bool _isOption2;
    [SerializeField] private bool _isOption3;

    private string[] _options = {"", "", "", ""};
    private int[] _correctOptions;
    private int _correctOption = 255;

    public Question() {}

    public Question(long id, Level level, string question, string imageUrl,
        string option0, string option1, string option2, string option3,
        bool isOption0, bool isOption1, bool isOption2, bool isOption3)
    {
        this.id = id;
        _level = level;
        _question = question;
        _imageUrl = imageUrl;
        _option0 = option0;
        _option1 = option1;
        _option2 = option2;
        _option3 = option3;
        _isOption0 = isOption0;
        _isOption1 = isOption1;
        _isOption2 = isOption2;
        _isOption3 = isOption3;

        _options = new[] { option0, option1, option2, option3 };
        _correctOptions = new[]{255, 255, 255, 255};
        FixCorrectOptions(_correctOptions);
    }

    private void FixCorrectOptions(int[] ints)
    {
        if (ints.Length != 4) return;

        ints[0] = _isOption0 ? 0 : 255;
        ints[1] = _isOption1 ? 1 : 255;
        ints[2] = _isOption2 ? 2 : 255;
        ints[3] = _isOption3 ? 3 : 255;

        if (_isOption0) _correctOption = 0;
        else if (_isOption1) _correctOption = 1;
        else if (_isOption2) _correctOption = 2;
        else if (_isOption3) _correctOption = 3;
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

    public string GetOption0() { return _option0; }
    public string GetOption1() { return _option1; }
    public string GetOption2() { return _option2; }
    public string GetOption3() { return _option3; }

    public bool GetIsOption0() { return _isOption0; }
    public bool GetIsOption1() { return _isOption1; }
    public bool GetIsOption2() { return _isOption2; }
    public bool GetIsOption3() { return _isOption3; }
}