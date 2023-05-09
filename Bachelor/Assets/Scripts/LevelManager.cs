using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject endMenu;
    [SerializeField] private string story;
    [SerializeField] private string level;
    [SerializeField] private bool levelIsCompleted;
    [SerializeField] private string levelHub;

    [SerializeField] private Sprite threeStarSprite;
    [SerializeField] private Sprite twoStarSprite;
    [SerializeField] private Sprite oneStarSprite;
    [SerializeField] private Sprite failSprite;

    [SerializeField] private string threeStarStatement;
    [SerializeField] private string twoStarStatement;
    [SerializeField] private string oneStarStatement;
    [SerializeField] private string failStatement;

    [SerializeField] private Image image;
    [SerializeField] private TMPro.TextMeshProUGUI statementText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(LevelManager)) as LevelManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static LevelManager instance;

    public void EndGame()
    {
        levelIsCompleted = true;
        PauseManager.Instance.canPause = false;
        int stars = ScoreDependantInfo();
        int questionAmount;
        if (QuestionManager.Instance.scrambleAnswers) questionAmount = QuestionManager.Instance.QuestionAmount * 4;
        else questionAmount = QuestionManager.Instance.QuestionAmount;
        scoreText.text = ScoreManager.Instance.GetCurrentScore() + "/" + questionAmount
                         + " riktige svar";
        if (stars != 0) HighScoreManager.Instance.SubmitScore(story, 
            (GameDataManager.Instance.GetGameData().ActiveStory.Levels
                .FindIndex(o => o.ID == GameDataManager.Instance.GetGameData().ActiveLevel.ID) + 1).ToString(),
            ScoreManager.Instance.GetCurrentScore(), stars);
        endMenu.SetActive(true);
    }

    private int ScoreDependantInfo()
    {
        float a = (float) ScoreManager.Instance.GetCurrentScore() / QuestionManager.Instance.QuestionAmount;

        statementText.text = a switch
        {
            > 0.90f => threeStarStatement,
            > 0.70f => twoStarStatement,
            > 0.50f => oneStarStatement,
            _ => failStatement
        };
        
        image.sprite = a switch
        {
            > 0.90f => threeStarSprite,
            > 0.70f => twoStarSprite,
            > 0.50f => oneStarSprite,
            _ => failSprite
        };

        return a switch
        {
            > 0.90f => 3,
            > 0.70f => 2,
            > 0.50f => 1,
            _ => 0
        };
    }

    public void RestartLevel()
    {
        levelIsCompleted = false;
        GetComponent<SwitchScene>().LoadNewScene(level);
    }

    public void ReturnToLevelHub() { GetComponent<SwitchScene>().LoadNewScene(levelHub); }
}