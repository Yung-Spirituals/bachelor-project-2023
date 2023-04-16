using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject endMenu;
    [SerializeField] private string story;
    [SerializeField] private string level;
    [SerializeField] private bool levelIsCompleted;
    [SerializeField] private string levelHub;

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

    public void EndGame(bool won)
    {
        levelIsCompleted = true;
        PauseManager.Instance.canPause = false;
        HighScoreManager.Instance.SubmitScore(
            story ,level, ScoreManager.Instance.GetCurrentScore());
        endMenu.SetActive(true);
    }
        
    public void RestartLevel()
    {
        levelIsCompleted = false;
        GetComponent<SwitchScene>().LoadNewScene(level);
    }

    public void ReturnToLevelHub() { GetComponent<SwitchScene>().LoadNewScene(levelHub); }
}