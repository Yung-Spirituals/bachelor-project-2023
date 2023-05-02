using SoData;
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
        if (_gameDataScriptableObject.Stories.Count == 0) StartCoroutine(
            WebCommunicationManager.GetGameDataRequest(_gameDataScriptableObject));
        return _gameDataScriptableObject;
    }
}