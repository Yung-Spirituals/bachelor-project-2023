using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    [SerializeField] private long id;
    [SerializeField] private Story _story;
    [SerializeField] private List<Question> _questions;
    [SerializeField] private string _levelName;
    [SerializeField] private string _backgroundUrl;
    [SerializeField] private string _levelType;
    [SerializeField] private string _levelGoal;
    [SerializeField] private string _howToPlay;
}