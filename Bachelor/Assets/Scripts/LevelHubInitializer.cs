﻿using System.Collections.Generic;
using SoData;
using UnityEngine;

public class LevelHubInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Transform _parentTransform;

    private void Start()
    {
        GameDataScriptableObject scriptableObject = GameDataManager.Instance.GetGameData();
        List<Level> levels = scriptableObject.ActiveStory.Levels;
        int levelCount = levels.Count;
        for (int i = 0; i < levelCount; i++)
        {
            GameObject levelCoin = Instantiate(_gameObject, _parentTransform);
            LevelPopup levelPopup = levelCoin.GetComponent<LevelPopup>();
            levelPopup.level = levels[i];
            levelPopup.levelNumber = i + 1;
        }
    }
}