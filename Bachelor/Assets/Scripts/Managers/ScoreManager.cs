using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private string preScore;
    [SerializeField] private string postScore;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _currentScore;
    private int _possibleScore = 0;

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(ScoreManager)) as ScoreManager;
 
            return _instance;
        }
        set => _instance = value;
    }
    private static ScoreManager _instance;
    
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