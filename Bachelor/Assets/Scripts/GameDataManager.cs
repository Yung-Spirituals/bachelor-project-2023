using System.Collections;
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

    private void Start() { StartCoroutine(RefreshGameData()); }

    public GameDataScriptableObject GetGameData() { return _gameDataScriptableObject; }

    public IEnumerator RefreshGameData()
    {
        yield return WebCommunicationUtil.GetGameDataRequest(_gameDataScriptableObject);
        _gameDataScriptableObject.Stories = _gameDataScriptableObject.Stories.OrderBy(story => story.ID).ToList();
        foreach (Story story in _gameDataScriptableObject.Stories)
        {
            story.Levels = story.Levels.OrderBy(level => level.ID).ToList();
        }
        yield return UpdateActives();
    }

    private IEnumerator UpdateActives()
    {
        
        if (_gameDataScriptableObject.ActiveStory != null)
        {
            _gameDataScriptableObject.ActiveStory = _gameDataScriptableObject.Stories
                .Find(o => o.ID == _gameDataScriptableObject.ActiveStory.ID);
        }
        if (_gameDataScriptableObject.ActiveLevel != null)
        {
            if (_gameDataScriptableObject.ActiveStory != null)
                _gameDataScriptableObject.ActiveLevel = _gameDataScriptableObject.ActiveStory.Levels
                    .Find(o => o.ID == _gameDataScriptableObject.ActiveLevel.ID);
        }

        if (_gameDataScriptableObject.ActiveQuestion != null)
        {
            if (_gameDataScriptableObject.ActiveLevel != null)
                _gameDataScriptableObject.ActiveQuestion = _gameDataScriptableObject.ActiveLevel.Questions
                    .Find(o => o.GetId() == _gameDataScriptableObject.ActiveQuestion.GetId());
        }

        yield return _gameDataScriptableObject;
    }
}