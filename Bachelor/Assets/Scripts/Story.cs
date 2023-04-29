using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Story
{
    [SerializeField] private long id;
    [SerializeField] private List<Level> levels;
    [SerializeField] private string _storyName;
    [SerializeField] private string _iconUrl;
    [SerializeField] private string _backgroundUrl;
    [SerializeField] private string _storyShortDescription;
    [SerializeField] private string _storyTitle;
    [SerializeField] private string _storyFullDescription;
}