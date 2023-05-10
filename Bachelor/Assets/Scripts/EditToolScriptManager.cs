using System.Collections;
using SoData;
using UnityEngine;

public class EditToolScriptManager : MonoBehaviour
{
    [SerializeField] private GameObject themeSelect;
    [SerializeField] private GameObject themeCreate;
    [SerializeField] private GameObject levelCreate;
    [SerializeField] private GameObject standardCreate;
    [SerializeField] private GameObject trueOrFalseCreate;
    [SerializeField] private GameObject rankCreate;

    private GameObject[] uiPages;
    private GameObject activeUI;
    private GameDataScriptableObject _gameDataScriptableObject;

    public static EditToolScriptManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(EditToolScriptManager)) as EditToolScriptManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static EditToolScriptManager instance;

    private void Start()
    {
        uiPages = new[] { themeSelect, themeCreate, levelCreate, standardCreate, trueOrFalseCreate, rankCreate };
        _gameDataScriptableObject = GameDataManager.Instance.GetGameData();
        _gameDataScriptableObject.ActiveStory = null;
        _gameDataScriptableObject.ActiveLevel = null;
        _gameDataScriptableObject.ActiveStory = null;
        SetActiveUI(themeSelect);
        StartCoroutine(Refresh());
    }

    public void SetActiveUI(GameObject activeElement)
    {
        foreach (GameObject page in uiPages) { page.SetActive(page == activeElement); }
    }

    public void SelectStory(Story story)
    {
        _gameDataScriptableObject.ActiveStory = story;
        StartCoroutine(themeCreate.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.ActiveStory));
        SetActiveUI(themeCreate);
    }
    
    public void SelectLevel(Level level)
    {
        _gameDataScriptableObject.ActiveLevel = level;
        StartCoroutine(levelCreate.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.ActiveLevel));
        SetActiveUI(levelCreate);
    }

    public void SelectQuestion(Question question)
    {
        _gameDataScriptableObject.ActiveQuestion = question;
        switch (_gameDataScriptableObject.ActiveLevel.LevelType)
        {
            case GameMode.Standard:
                standardCreate.GetComponent<QuestionCreateEdit>().LoadQuestion(_gameDataScriptableObject.ActiveQuestion);
                SetActiveUI(standardCreate);
                break;
            case GameMode.TrueOrFalse:
                trueOrFalseCreate.GetComponent<QuestionCreateEdit>().LoadQuestion(_gameDataScriptableObject.ActiveQuestion);
                SetActiveUI(trueOrFalseCreate);
                break;
            case GameMode.Rank:
                rankCreate.GetComponent<QuestionCreateEdit>().LoadQuestion(_gameDataScriptableObject.ActiveQuestion);
                SetActiveUI(rankCreate);
                break;
        }
    }

    public void NewStory()
    {
        _gameDataScriptableObject.ActiveStory = new Story();
        SelectStory(_gameDataScriptableObject.ActiveStory);
        SetActiveUI(themeCreate);
    }

    public void NewLevel()
    {
        _gameDataScriptableObject.ActiveLevel = new Level();
        SelectLevel(_gameDataScriptableObject.ActiveLevel);
        SetActiveUI(levelCreate);
    }

    public void NewQuestion()
    {
        _gameDataScriptableObject.ActiveQuestion = new Question();
        SelectQuestion(_gameDataScriptableObject.ActiveQuestion);
        switch (_gameDataScriptableObject.ActiveLevel.LevelType)
        {
            case GameMode.Standard:
                SetActiveUI(standardCreate);
                break;
            case GameMode.TrueOrFalse:
                SetActiveUI(trueOrFalseCreate);
                break;
            case GameMode.Rank:
                SetActiveUI(rankCreate);
                break;
        }
    }

    public void BackFromStory() { _gameDataScriptableObject.ActiveStory = null; }
    
    public void BackFromLevel() { _gameDataScriptableObject.ActiveLevel = null; }
    
    public void BackFromQuestion() { _gameDataScriptableObject.ActiveQuestion = null; }

    public IEnumerator Refresh()
    {
        yield return themeSelect.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.Stories);
        if (_gameDataScriptableObject.ActiveStory == null) yield break;
        yield return themeCreate.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.ActiveStory);
        if (_gameDataScriptableObject.ActiveLevel == null) yield break;
        yield return levelCreate.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.ActiveLevel);
    }
}
