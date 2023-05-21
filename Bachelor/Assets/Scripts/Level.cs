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

    [SerializeField] private long id;
    // ReSharper disable once InconsistentNaming
    [NonSerialized] private Subject subject;
    [SerializeField] private List<Question> questions = new ();
    [SerializeField] private string levelName;
    [SerializeField] private string levelType;
    [SerializeField] private string levelGoal;

    public Level() {}

    public Level(Subject subject, string levelName, string levelType, string levelGoal)
    {
        this.subject = subject;
        this.levelName = levelName;
        this.levelType = levelType;
        this.levelGoal = levelGoal;
    }
}