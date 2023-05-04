using System;
using UnityEngine;

public class EditToolScriptManager : MonoBehaviour
{
    [SerializeField] private GameObject themeSelect;
    [SerializeField] private GameObject themeCreate;
    [SerializeField] private GameObject levelCreate;
    [SerializeField] private GameObject standardCreate;
    [SerializeField] private GameObject trueOrFalseCreate;

    private GameObject[] uiPages;
    private GameObject activeUI;

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
        uiPages = new[] { themeSelect, themeCreate, levelCreate, standardCreate, trueOrFalseCreate };
        RefreshStory();
    }

    public void SetActiveUI(GameObject activeElement)
    {
        foreach (GameObject page in uiPages) { page.SetActive(page == activeElement); }
    }

    private void RefreshStory()
    {
        SetActiveUI(themeSelect);
        themeSelect.GetComponent<LoadExistingEntries>().LoadEntries(GameDataManager.Instance.GetGameData().Stories);
    }

    public void SelectStory(Story story)
    {
        SetActiveUI(themeCreate);
        GameDataManager.Instance.GetGameData().ActiveStory = story;
        themeCreate.GetComponent<LoadExistingEntries>().LoadEntries(story);
    }
    
    public void SelectLevel(Level level)
    {
        SetActiveUI(levelCreate);
        GameDataManager.Instance.GetGameData().ActiveLevel = level;
        levelCreate.GetComponent<LoadExistingEntries>().LoadEntries(level);
    }

    public void SelectQuestion(Question question)
    {
        GameDataManager.Instance.GetGameData().ActiveQuestion = question;
        standardCreate.GetComponent<QuestionCreateEdit>().LoadQuestion(question);
        SetActiveUI(standardCreate);
    }

    public void NewStory(string storyName)
    {
        StartCoroutine(WebCommunicationUtil.PutNewGameDataRequest(new Story(storyName,
            "", "", "", "", ""), null, null));
        GameDataManager.Instance.RefreshGameData();
        RefreshStory();
    }

    public void NewLevel(string gameMode)
    {
        StartCoroutine(WebCommunicationUtil.PutNewGameDataRequest(
            GameDataManager.Instance.GetGameData().ActiveStory,
            new Level(GameDataManager.Instance.GetGameData().ActiveStory, "", "",
                gameMode, "", ""), null));
        Save();
    }

    public void NewQuestion()
    {
        StartCoroutine(WebCommunicationUtil.PutNewGameDataRequest(GameDataManager.Instance.GetGameData()
                .ActiveStory, GameDataManager.Instance.GetGameData().ActiveLevel,
            new Question(GameDataManager.Instance.GetGameData().ActiveLevel,
                "", "", "", "", "", "",
                false, false, false, false)));
        Save();
    }

    public void Save()
    {
        GameDataManager.Instance.RefreshGameData();
        
        if (GameDataManager.Instance.GetGameData().ActiveStory != null)
        {
            GameDataManager.Instance.GetGameData().ActiveStory = GameDataManager.Instance.GetGameData().
                Stories
                .Find(o => o.ID == GameDataManager.Instance.GetGameData().ActiveStory.ID);
        }
        if (GameDataManager.Instance.GetGameData().ActiveLevel != null)
        {
            GameDataManager.Instance.GetGameData().ActiveLevel = GameDataManager.Instance.GetGameData().
                ActiveStory.Levels
                .Find(o => o.ID == GameDataManager.Instance.GetGameData().ActiveLevel.ID);
        }
        if (GameDataManager.Instance.GetGameData().ActiveQuestion != null)
        {
            GameDataManager.Instance.GetGameData().ActiveQuestion = GameDataManager.Instance.GetGameData().
                ActiveLevel.Questions
                .Find(o => o.GetId() == GameDataManager.Instance.GetGameData().ActiveQuestion.GetId());
        }
        
        Refresh();
    }

    private void Refresh()
    {
        themeSelect.GetComponent<LoadExistingEntries>().LoadEntries(GameDataManager.Instance.GetGameData().Stories);
        if (GameDataManager.Instance.GetGameData().ActiveStory == null) return;
        themeCreate.GetComponent<LoadExistingEntries>().LoadEntries(GameDataManager.Instance.GetGameData().ActiveStory);
        if (GameDataManager.Instance.GetGameData().ActiveLevel == null) return;
        levelCreate.GetComponent<LoadExistingEntries>().LoadEntries(GameDataManager.Instance.GetGameData().ActiveLevel);
    }
}
