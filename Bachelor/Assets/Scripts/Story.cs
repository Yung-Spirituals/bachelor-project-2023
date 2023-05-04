using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Story
{
    public long ID => id;

    public List<Level> Levels
    {
        get => _levels;
        set => _levels = value;
    }

    public string StoryName
    {
        get => _storyName;
        set => _storyName = value;
    }

    public string IconUrl
    {
        get => _iconUrl;
        set => _iconUrl = value;
    }

    public string BackgroundUrl
    {
        get => _backgroundUrl;
        set => _backgroundUrl = value;
    }

    public string StoryShortDescription
    {
        get => _storyShortDescription;
        set => _storyShortDescription = value;
    }

    public string StoryTitle
    {
        get => _storyTitle;
        set => _storyTitle = value;
    }

    public string StoryFullDescription
    {
        get => _storyFullDescription;
        set => _storyFullDescription = value;
    }

    [SerializeField] private long id;
    [SerializeField] private List<Level> _levels = new ();
    [SerializeField] private string _storyName;
    [SerializeField] private string _iconUrl;
    [SerializeField] private string _backgroundUrl;
    [SerializeField] private string _storyShortDescription;
    [SerializeField] private string _storyTitle;
    [SerializeField] private string _storyFullDescription;
    
    public Story() {}

    public Story(string storyName, string iconUrl, string backgroundUrl,
        string storyShortDescription, string storyTitle, string storyFullDescription)
    {
        _storyName = storyName;
        _iconUrl = iconUrl;
        _backgroundUrl = backgroundUrl;
        _storyShortDescription = storyShortDescription;
        _storyTitle = storyTitle;
        _storyFullDescription = storyFullDescription;
    }
}