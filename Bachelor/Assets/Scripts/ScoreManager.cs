using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private string preScore;
    [SerializeField] private string postScore;
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
    private int _currentScore;
    private int _possibleScore = 0;

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
    
    public int GetCurrentScore() { return _currentScore; }

    public void ChangeScore(int change)
    {
        _currentScore += change;
        if (_scoreText == null) return;
        
        if (_possibleScore != 0)
        {
            _scoreText.text = preScore + _currentScore + "/" + _possibleScore + postScore;
        }
        else
        {
            _scoreText.text = preScore + _currentScore + postScore;
        }
    }
}