using System.Collections;
using SoData;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private GameDataScriptableObject gameDataScriptableObject;
    
    public static GameDataManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(GameDataManager)) as GameDataManager;
 
            return instance;
        }
        set => instance = value;
    }
    private static GameDataManager instance;

    private void Start() { StartCoroutine(RefreshGameData()); }

    public GameDataScriptableObject GetGameData() { return gameDataScriptableObject; }

    public IEnumerator RefreshGameData()
    {
        yield return WebCommunicationUtil.GetGameDataRequest(gameDataScriptableObject);
        gameDataScriptableObject.Subjects = gameDataScriptableObject.Subjects.OrderBy(subject => subject.ID).ToList();
        foreach (Subject subject in gameDataScriptableObject.Subjects)
        {
            subject.Levels = subject.Levels.OrderBy(level => level.ID).ToList();
        }
        yield return UpdateActives();
    }

    private IEnumerator UpdateActives()
    {
        
        if (gameDataScriptableObject.ActiveSubject != null)
        {
            gameDataScriptableObject.ActiveSubject = gameDataScriptableObject.Subjects
                .Find(o => o.ID == gameDataScriptableObject.ActiveSubject.ID);
        }
        if (gameDataScriptableObject.ActiveLevel != null)
        {
            if (gameDataScriptableObject.ActiveSubject != null)
                gameDataScriptableObject.ActiveLevel = gameDataScriptableObject.ActiveSubject.Levels
                    .Find(o => o.ID == gameDataScriptableObject.ActiveLevel.ID);
        }

        if (gameDataScriptableObject.ActiveQuestion != null)
        {
            if (gameDataScriptableObject.ActiveLevel != null)
                gameDataScriptableObject.ActiveQuestion = gameDataScriptableObject.ActiveLevel.Questions
                    .Find(o => o.ID == gameDataScriptableObject.ActiveQuestion.ID);
        }

        yield return gameDataScriptableObject;
    }
}