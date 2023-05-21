using System.Collections.Generic;
using SoData;
using UnityEngine;

public class LevelHubInitializer : MonoBehaviour
{
    [SerializeField] private GameObject levelCoinPrefab;
    [SerializeField] private Transform parentTransform;

    private void Start()
    {
        GameDataScriptableObject scriptableObject = GameDataManager.Instance.GetGameData();
        List<Level> levels = scriptableObject.ActiveSubject.Levels;
        int levelCount = levels.Count;
        for (int i = 0; i < levelCount; i++)
        {
            GameObject levelCoin = Instantiate(levelCoinPrefab, parentTransform);
            LevelPopup levelPopup = levelCoin.GetComponent<LevelPopup>();
            levelPopup.level = levels[i];
            levelPopup.levelNumber = i + 1;
        }
    }
}