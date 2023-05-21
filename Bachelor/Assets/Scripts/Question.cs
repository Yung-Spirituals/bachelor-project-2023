using System;
using UnityEngine;

// Class representing a question.
[Serializable]
public class Question
{
    public long ID
    {
        get => id;
        set => id = value;
    }

    public Level Level
    {
        get => level;
        set => level = value;
    }

    public string QuestionText
    {
        get => question;
        set => question = value;
    }

    public string ImageUrl
    {
        get => imageUrl;
        set => imageUrl = value;
    }

    public string Option0
    {
        get => option0;
        set => option0 = value;
    }

    public string Option1
    {
        get => option1;
        set => option1 = value;
    }

    public string Option2
    {
        get => option2;
        set => option2 = value;
    }

    public string Option3
    {
        get => option3;
        set => option3 = value;
    }

    public bool IsOption0
    {
        get => isOption0;
        set => isOption0 = value;
    }

    public bool IsOption1
    {
        get => isOption1;
        set => isOption1 = value;
    }

    public bool IsOption2
    {
        get => isOption2;
        set => isOption2 = value;
    }

    public bool IsOption3
    {
        get => isOption3;
        set => isOption3 = value;
    }

    [SerializeField] private long id;
    
    // ReSharper disable once InconsistentNaming
    [NonSerialized] private Level level;
    
    [SerializeField] private string question = "";
    [SerializeField] private string imageUrl = "";
    
    [SerializeField] private string option0 = "";
    [SerializeField] private string option1 = "";
    [SerializeField] private string option2 = "";
    [SerializeField] private string option3 = "";

    [SerializeField] private bool isOption0;
    [SerializeField] private bool isOption1;
    [SerializeField] private bool isOption2;
    [SerializeField] private bool isOption3;

    public Question() {}

    public Question(Level level, string question, string imageUrl,
        string option0, string option1, string option2, string option3,
        bool isOption0, bool isOption1, bool isOption2, bool isOption3)
    {
        this.level = level;
        this.question = question;
        this.imageUrl = imageUrl;
        this.option0 = option0;
        this.option1 = option1;
        this.option2 = option2;
        this.option3 = option3;
        this.isOption0 = isOption0;
        this.isOption1 = isOption1;
        this.isOption2 = isOption2;
        this.isOption3 = isOption3;
    }
    
    // Return all options as a string array.
    public string[] GetOptions() { return new[] { option0, option1, option2, option3 }; }

    // Return all isOptions as a bool array. An isOption marks if the corresponding option is correct.
    public bool[] GetIsOptions() { return new[] { isOption0, isOption1, isOption2, isOption3 }; }
}