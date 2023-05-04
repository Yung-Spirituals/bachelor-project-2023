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

    public Question() {}

    public Question(Level level, string question, string imageUrl,
        string option0, string option1, string option2, string option3,
        bool isOption0, bool isOption1, bool isOption2, bool isOption3)
    {
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
    }

    public long GetId() { return id; }
    public Level GetLevel() { return _level; }
    public void SetLevel(Level level) { _level = level; }
    public string GetQuestion() { return _question; }
    public void SetQuestion(string question) { _question = question; }
    public string GetImageUrl() { return _imageUrl; }
    public void SetImageUrl(string imageUrl) { _imageUrl = imageUrl; }

    public string GetOption0() { return _option0; }
    public void SetOption0(string option0) { _option0 = option0; }
    public string GetOption1() { return _option1; }
    public void SetOption1(string option1) { _option1 = option1; }
    public string GetOption2() { return _option2; }
    public void SetOption2(string option2) { _option2 = option2; }
    public string GetOption3() { return _option3; }
    public void SetOption3(string option3) { _option3 = option3; }
    public string[] GetOptions() { return new[] { _option0, _option1, _option2, _option3 }; }
    public void SetOptions(string[] options)
    {
        _option0 = options[0];
        _option1 = options[1];
        _option2 = options[2];
        _option3 = options[3];
    }

    public bool GetIsOption0() { return _isOption0; }
    public void SetIsOption0(bool isOption0) { _isOption0 = isOption0; }
    public bool GetIsOption1() { return _isOption1; }
    public void SetIsOption1(bool isOption1) { _isOption1 = isOption1; }
    public bool GetIsOption2() { return _isOption2; }
    public void SetIsOption2(bool isOption2) { _isOption2 = isOption2; }
    public bool GetIsOption3() { return _isOption3; }
    public void SetIsOption3(bool isOption3) { _isOption3 = isOption3; }
    public bool[] GetIsOptions() { return new[] { _isOption0, _isOption1, _isOption2, _isOption3 }; }
    public void SetIsOptions(bool[] isOptions)
    {
        _isOption0 = isOptions[0];
        _isOption1 = isOptions[1];
        _isOption2 = isOptions[2];
        _isOption3 = isOptions[3];
    }
}