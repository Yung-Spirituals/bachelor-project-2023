using UnityEngine;

public class LevelHubManager: MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    [SerializeField] private TMPro.TextMeshProUGUI levelGoalText;
    //[SerializeField] private TMPro.TextMeshProUGUI howToPlayText;
    [SerializeField] private TMPro.TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject goToLevelButton;
    [SerializeField] private GameObject popUp;
    private Level _level;
    
    public static LevelHubManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(LevelHubManager)) as LevelHubManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static LevelHubManager instance;

    public void DisplayPopUp(int levelNumber, Level level)
    {
        _level = level;
        string highScore = "HighScore: " + HighScoreManager.Instance.GetLevelHighScore(
            GameDataManager.Instance.GetGameData().ActiveStory.StoryTitle, levelNumber.ToString());
        levelText.text = "Nivå " + levelNumber;
        levelGoalText.text = level.LevelGoal;
        //howToPlayText.text = level.HowToPlay;
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