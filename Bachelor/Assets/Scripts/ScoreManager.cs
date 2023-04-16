using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private string preScore;
    [SerializeField] private string postScore;
    [SerializeField] private GameObject scoreObject;
    private TMPro.TextMeshProUGUI _scoreText;
    private int _currentScore;

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(ScoreManager)) as ScoreManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static ScoreManager instance;

    private void Start()
    {
        _scoreText = scoreObject.GetComponent<TMPro.TextMeshProUGUI>();
        UpdateScoreText();
    }

    public void ChangeScore(int change)
    {
        _currentScore += change;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        _currentScore = 0;
        UpdateScoreText();
    }

    public int GetCurrentScore() { return _currentScore; }

    private void UpdateScoreText() { _scoreText.text = preScore + _currentScore + postScore;  }
}