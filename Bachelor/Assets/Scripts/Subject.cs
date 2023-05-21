using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Subject
{
    public long ID
    {
        get => id;
        set => id = value;
    }

    public List<Level> Levels
    {
        get => levels;
        set => levels = value;
    }

    public string SubjectName
    {
        get => subjectName;
        set => subjectName = value;
    }

    public string BackgroundUrl
    {
        get => backgroundUrl;
        set => backgroundUrl = value;
    }

    public string SubjectDescription
    {
        get => subjectDescription;
        set => subjectDescription = value;
    }

    [SerializeField] private long id;
    [SerializeField] private List<Level> levels = new ();
    [SerializeField] private string subjectName;
    [SerializeField] private string backgroundUrl;
    [SerializeField] private string subjectDescription;
    
    public Subject() {}

    public Subject(string subjectName, string backgroundUrl, string subjectDescription)
    {
        this.subjectName = subjectName;
        this.backgroundUrl = backgroundUrl;
        this.subjectDescription = subjectDescription;
    }
}