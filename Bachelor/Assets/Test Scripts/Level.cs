using Test_Scripts;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private string story;
    [SerializeField] private string level;
    [SerializeField] private string levelGoal;
    [SerializeField] private string howToPlay;
    [SerializeField] private string highScore;

    //[SerializeField] private TMPro.TextMeshProUGUI levelText;
    [SerializeField] private TMPro.TextMeshProUGUI levelGoalText;
    [SerializeField] private TMPro.TextMeshProUGUI howToPlayText;
    [SerializeField] private TMPro.TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject goToLevelButton;
    [SerializeField] private GameObject popUp;

    private void Start()
    {
        highScore = "HighScore: " + HighScoreManager.Instance.GetLevelHighScore(story, level);
        goToLevelButton.GetComponent<SwitchScene>().scene = level;
    }

    public void ShowPopUp()
    {
        //levelText.text = level;
        levelGoalText.text = levelGoal;
        howToPlayText.text = howToPlay;
        highScoreText.text = highScore;
        popUp.SetActive(true);
    }
}
