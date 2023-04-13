using System.Collections.Generic;
using UnityEngine;

namespace Test_Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        //TODO: figure out a solution for high-scores
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

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        private void UpdateScoreText()
        {
            _scoreText.text = "SCORE: " + _currentScore;
        }
    }
}