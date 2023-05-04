using System.Collections;
using System.Collections.Generic;
using SoData;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private GameDataScriptableObject _gameDataScriptableObject;
    
    public static GameDataManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(GameDataManager)) as GameDataManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static GameDataManager instance;

    public GameDataScriptableObject GetGameData()
    {
        if (_gameDataScriptableObject.Stories.Count != 0) return _gameDataScriptableObject;
        RefreshGameData();
        return _gameDataScriptableObject;
    }

    public void RefreshGameData()
    {
        StartCoroutine(WebCommunicationUtil.GetGameDataRequest(_gameDataScriptableObject));
        _gameDataScriptableObject.Stories = _gameDataScriptableObject.Stories.OrderBy(story => story.ID).ToList();
        foreach (Story story in _gameDataScriptableObject.Stories)
        {
            story.Levels = story.Levels.OrderBy(level => level.ID).ToList();
        }
    }
}