using TMPro;
using UnityEngine;

public class LevelHubManager: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI levelGoalText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject goToLevelButton;
    [SerializeField] private GameObject popUp;
    private Level _level;
    
    public static LevelHubManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(LevelHubManager)) as LevelHubManager;
 
            return _instance;
        }
        set => _instance = value;
    }
    private static LevelHubManager _instance;

    public void DisplayPopUp(int levelNumber, Level level)
    {
        _level = level;
        string highScore = "Rekord: " + HighScoreManager.Instance.GetLevelHighScore(
            GameDataManager.Instance.GetGameData().ActiveSubject.ID, level.ID);
        levelText.text = "Nivå " + levelNumber;
        levelGoalText.text = level.LevelGoal;
        highScoreText.text = highScore;
        goToLevelButton.GetComponent<SwitchScene>().scene = level.LevelType;
        popUp.SetActive(true);
    }

    public void SelectLevel()
    {
        GameDataManager.Instance.GetGameData().ActiveLevel = _level;
        goToLevelButton.GetComponent<SwitchScene>().LoadNewScene();
    }
}