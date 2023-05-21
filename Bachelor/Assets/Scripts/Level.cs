using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public long ID
    {
        get => id;
        set => id = value;
    }

    public Subject Subject
    {
        get => subject;
        set => subject = value;
    }

    public List<Question> Questions
    {
        get => questions;
        set => questions = value;
    }

    public string LevelName
    {
        get => levelName;
        set => levelName = value;
    }

    public string BackgroundUrl
    {
        get => backgroundUrl;
        set => backgroundUrl = value;
    }

    public string LevelType
    {
        get => levelType;
        set => levelType = value;
    }

    public string LevelGoal
    {
        get => levelGoal;
        set => levelGoal = value;
    }

    public string HowToPlay
    {
        get => howToPlay;
        set => howToPlay = value;
    }

    [SerializeField] private long id;
    // ReSharper disable once InconsistentNaming
    [NonSerialized] private Subject subject;
    [SerializeField] private List<Question> questions = new ();
    [SerializeField] private string levelName;
    [SerializeField] private string backgroundUrl;
    [SerializeField] private string levelType;
    [SerializeField] private string levelGoal;
    [SerializeField] private string howToPlay;

    public Level() {}

    public Level(Subject subject,
        string levelName, string backgroundUrl, string levelType, string levelGoal, string howToPlay)
    {
        this.subject = subject;
        this.levelName = levelName;
        this.backgroundUrl = backgroundUrl;
        this.levelType = levelType;
        this.levelGoal = levelGoal;
        this.howToPlay = howToPlay;
    }
}