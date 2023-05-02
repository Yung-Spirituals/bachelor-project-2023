using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public long ID => id;

    public Story Story
    {
        get => _story;
        set => _story = value;
    }

    public List<Question> Questions
    {
        get => _questions;
        set => _questions = value;
    }

    public string LevelName
    {
        get => _levelName;
        set => _levelName = value;
    }

    public string BackgroundUrl
    {
        get => _backgroundUrl;
        set => _backgroundUrl = value;
    }

    public string LevelType
    {
        get => _levelType;
        set => _levelType = value;
    }

    public string LevelGoal
    {
        get => _levelGoal;
        set => _levelGoal = value;
    }

    public string HowToPlay
    {
        get => _howToPlay;
        set => _howToPlay = value;
    }

    [SerializeField] private long id;
    [NonSerialized] private Story _story;
    [SerializeField] private List<Question> _questions = new ();
    [SerializeField] private string _levelName;
    [SerializeField] private string _backgroundUrl;
    [SerializeField] private string _levelType;
    [SerializeField] private string _levelGoal;
    [SerializeField] private string _howToPlay;

    public Level(long id, Story story,
        string levelName, string backgroundUrl, string levelType, string levelGoal, string howToPlay)
    {
        this.id = id;
        _story = story;
        _levelName = levelName;
        _backgroundUrl = backgroundUrl;
        _levelType = levelType;
        _levelGoal = levelGoal;
        _howToPlay = howToPlay;
    }
}