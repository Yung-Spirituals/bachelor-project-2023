using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string level;
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
    [SerializeField] private TextMeshProUGUI statementText;
    [SerializeField] private TextMeshProUGUI scoreText;
    

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(LevelManager)) as LevelManager;
 
            return _instance;
        }
        set => _instance = value;
    }
    private static LevelManager _instance;

    public void EndGame(int possiblePoints)
    {
        int scoreAmount = ScoreManager.Instance.GetCurrentScore();
        int stars = ScoreDependantInfo(scoreAmount, possiblePoints);
        scoreText.text = scoreAmount + "/" + possiblePoints + " riktige svar";
        if (stars != 0) HighScoreManager.Instance.SubmitScore(GameDataManager.Instance.GetGameData().ActiveSubject.ID,
            GameDataManager.Instance.GetGameData().ActiveLevel.ID, 
            ScoreManager.Instance.GetCurrentScore(), stars);
        endMenu.SetActive(true);
    }

    private int ScoreDependantInfo(int score, int possibleScore)
    {
        float a = (float) score / possibleScore;

        statementText.text = a switch
        {
            > 0.85f => threeStarStatement,
            > 0.65f => twoStarStatement,
            > 0.45f => oneStarStatement,
            _ => failStatement
        };
        
        image.sprite = a switch
        {
            > 0.85f => threeStarSprite,
            > 0.65f => twoStarSprite,
            > 0.45f => oneStarSprite,
            _ => failSprite
        };

        return a switch
        {
            > 0.85f => 3,
            > 0.65f => 2,
            > 0.45f => 1,
            _ => 0
        };
    }

    public void RestartLevel() { GetComponent<SwitchScene>().LoadNewScene(level); }

    public void ReturnToLevelHub() { GetComponent<SwitchScene>().LoadNewScene(levelHub); }

    public void SetLoading(bool loading) { loadingScreen.SetActive(loading); }
}