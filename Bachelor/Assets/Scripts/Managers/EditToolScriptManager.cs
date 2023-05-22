using System.Collections;
using SoData;
using UnityEngine;

public class EditToolScriptManager : MonoBehaviour
{
    [SerializeField] private GameObject subjectSelect;
    [SerializeField] private GameObject subjectCreate;
    [SerializeField] private GameObject levelCreate;
    [SerializeField] private GameObject standardCreate;
    [SerializeField] private GameObject trueOrFalseCreate;
    [SerializeField] private GameObject rankCreate;
    [SerializeField] private GameObject memoryCreate;

    [SerializeField] private GameObject infoPopup;
    [SerializeField] private GameObject continueOrNotPopup;

    private GameObject[] _uiPages;
    private GameObject _activeUI;
    private GameDataScriptableObject _gameDataScriptableObject;

    public static EditToolScriptManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(EditToolScriptManager)) as EditToolScriptManager;
 
            return instance;
        }
        set => instance = value;
    }
    private static EditToolScriptManager instance;

    private void Start()
    {
        // All ui pages in the scene.
        _uiPages = new[] { subjectSelect, subjectCreate, levelCreate, standardCreate,
            trueOrFalseCreate, rankCreate, memoryCreate };
        
        _gameDataScriptableObject = GameDataManager.Instance.GetGameData();
        
        // Makes sure there is no active instances of subject, level, or question before editing begins.
        _gameDataScriptableObject.ActiveSubject = null;
        _gameDataScriptableObject.ActiveLevel = null;
        _gameDataScriptableObject.ActiveQuestion = null;
        
        // Displays the subject select page.
        SetActiveUI(subjectSelect);
        
        // Populate pages with the latest data.
        StartCoroutine(Refresh());
    }

    public void SetActiveUI(GameObject activeElement)
    {
        // Enables the provided ui page, and disable all others.
        foreach (GameObject page in _uiPages) { page.SetActive(page == activeElement); }
        _activeUI = activeElement;
    }

    public void SelectSubject(Subject subject)
    {
        _gameDataScriptableObject.ActiveSubject = subject;
        StartCoroutine(subjectCreate.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.ActiveSubject));
        SetActiveUI(subjectCreate);
    }
    
    public void SelectLevel(Level level)
    {
        _gameDataScriptableObject.ActiveLevel = level;
        StartCoroutine(levelCreate.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.ActiveLevel));
        SetActiveUI(levelCreate);
    }

    // Loads the correct ui page for editing a question based on the quiz format of the level.
    public void SelectQuestion(Question question)
    {
        _gameDataScriptableObject.ActiveQuestion = question;
        switch (_gameDataScriptableObject.ActiveLevel.LevelType)
        {
            case GameMode.Standard:
                standardCreate.GetComponent<QuestionCreateEdit>().LoadQuestion(
                    _gameDataScriptableObject.ActiveQuestion);
                SetActiveUI(standardCreate);
                break;
            case GameMode.TrueOrFalse:
                trueOrFalseCreate.GetComponent<QuestionCreateEdit>().LoadQuestion(
                    _gameDataScriptableObject.ActiveQuestion);
                SetActiveUI(trueOrFalseCreate);
                break;
            case GameMode.Rank:
                rankCreate.GetComponent<QuestionCreateEdit>().LoadQuestion(
                    _gameDataScriptableObject.ActiveQuestion);
                SetActiveUI(rankCreate);
                break;
            case GameMode.MemoryCards:
                memoryCreate.GetComponent<QuestionCreateEdit>().LoadQuestion(
                    _gameDataScriptableObject.ActiveQuestion);
                SetActiveUI(memoryCreate);
                break;
        }
    }

    public void NewSubject()
    {
        _gameDataScriptableObject.ActiveSubject = new Subject();
        SelectSubject(_gameDataScriptableObject.ActiveSubject);
        SetActiveUI(subjectCreate);
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
            case GameMode.MemoryCards:
                SetActiveUI(memoryCreate);
                break;
        }
    }
    
    public void Back()
    {
        if (_activeUI == subjectCreate) BackFromSubject();
        else if (_activeUI == levelCreate) BackFromLevel();
        else BackFromQuestion();
    }

    private void BackFromSubject()
    {
        _gameDataScriptableObject.ActiveSubject = null;
        SetActiveUI(subjectSelect);
    }

    private void BackFromLevel()
    {
        _gameDataScriptableObject.ActiveLevel = null;
        SetActiveUI(subjectCreate);
    }

    private void BackFromQuestion()
    {
        _gameDataScriptableObject.ActiveQuestion = null;
        SetActiveUI(levelCreate);
    }

    // Gets the latest game data and updates the display accordingly
    public IEnumerator Refresh()
    {
        yield return subjectSelect.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.Subjects);
        if (_gameDataScriptableObject.ActiveSubject == null) yield break;
        yield return subjectCreate.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.ActiveSubject);
        if (_gameDataScriptableObject.ActiveLevel == null) yield break;
        yield return levelCreate.GetComponent<LoadExistingEntries>().LoadEntries(_gameDataScriptableObject.ActiveLevel);
    }

    //
    public void DisplayPopup(string title, string text, bool canDecline)
    {
        if (canDecline) continueOrNotPopup.GetComponent<Popup>().Display(title, text);
        else infoPopup.GetComponent<Popup>().Display(title, text);
    }

    public void StartDelete() { StartCoroutine(Delete()); }

    private IEnumerator Delete()
    {
        if (_gameDataScriptableObject.ActiveQuestion != null)
        {
            if (_gameDataScriptableObject.ActiveQuestion.ID == 0) yield break;
            
            yield return WebCommunicationUtil.DeleteGameDataRequest(
                "/question", _gameDataScriptableObject.ActiveQuestion.ID);
            
        }

        else if (_gameDataScriptableObject.ActiveLevel != null)
        {
            if (_gameDataScriptableObject.ActiveLevel.ID == 0) yield break;
            
            yield return WebCommunicationUtil.DeleteGameDataRequest(
                "/level", _gameDataScriptableObject.ActiveLevel.ID);
        }

        else if (_gameDataScriptableObject.ActiveSubject != null)
        {
            if (_gameDataScriptableObject.ActiveSubject.ID == 0) yield break;
            
            yield return WebCommunicationUtil.DeleteGameDataRequest(
                "/subject", _gameDataScriptableObject.ActiveSubject.ID);
        }
        
        Back();
        yield return Refresh();
    }
}
