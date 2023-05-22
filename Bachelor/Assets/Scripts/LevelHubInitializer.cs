using System.Collections.Generic;
using SoData;
using UnityEngine;

// Script for initializing the level hub scene
public class LevelHubInitializer : MonoBehaviour
{
    [SerializeField] private GameObject levelCoinPrefab;
    [SerializeField] private Transform parentTransform;

    // Initialize when the script is enabled (this script should be available when the scene is loaded).
    private void Start() { Initialize(); }
    
    private void Initialize()
    {
        // Retrieves the levels of the selected subject.
        GameDataScriptableObject scriptableObject = GameDataManager.Instance.GetGameData();
        List<Level> levels = scriptableObject.ActiveSubject.Levels;
        
        // Instantiate a level button for each level and assigns it to the button along with its numeration.
        for (int i = 0; i < levels.Count; i++)
        {
            GameObject levelCoinObject = Instantiate(levelCoinPrefab, parentTransform);
            LevelCoin levelCoinScript = levelCoinObject.GetComponent<LevelCoin>();
            levelCoinScript.level = levels[i];
            levelCoinScript.levelNumber = i + 1;
        }
    }
}