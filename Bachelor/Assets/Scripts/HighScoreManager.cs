using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreManager: MonoBehaviour
{
    private List<HighScoreCollection> _highScoreCollections;
    [SerializeField] private List<long> subjectIds;
    private class HighScore
    {
        public readonly long LevelId;
        public int Score;
        public int Stars;

        public HighScore(long levelId, int score, int stars)
        {
            LevelId = levelId;
            Score = score;
            Stars = stars;
        }
    }
    private class HighScoreCollection
    {
        public readonly long SubjectId;
        private static readonly List<HighScore> HighScores = new ();

        public HighScoreCollection(long subjectId) { SubjectId = subjectId; }

        public bool SubmitScore(HighScore newScore)
        {
            var highScore = GetHighScore(newScore.LevelId);
            if (highScore.Score >= newScore.Score) return false;
            highScore.Score = newScore.Score;
            highScore.Stars = newScore.Stars;
            return true;
        }

        public HighScore GetHighScore(long levelId)
        {
            var highScore = HighScores.Find(score => score.LevelId == levelId);
            if (highScore != null) return highScore;
                
            highScore = new HighScore(levelId, 0, 0);
            HighScores.Add(highScore);
            return highScore;
        }
    }

    public static HighScoreManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(HighScoreManager)) as HighScoreManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static HighScoreManager instance;

    private void Awake()
    {
        _highScoreCollections = new List<HighScoreCollection>();
        foreach (HighScoreCollection highScoreCollection in subjectIds.Select(id =>
                     JsonUtility.FromJson<HighScoreCollection>(PlayerPrefs.GetString(id.ToString(), null)) 
                     ?? new HighScoreCollection(id)))
        {
            _highScoreCollections.Add(highScoreCollection);
        }
    }

    private HighScoreCollection GetSubjectHighScores(long subjectId)
    {
        HighScoreCollection subjectHighScores = _highScoreCollections
            .Find(subjectScores => subjectScores.SubjectId == subjectId);
        if (subjectHighScores != null) return subjectHighScores;
                
        subjectHighScores = new HighScoreCollection(subjectId);
        _highScoreCollections.Add(subjectHighScores);
        return subjectHighScores;
    }

    public int GetLevelHighScore(long subjectId, long levelId)
    {
        return GetSubjectHighScores(subjectId).GetHighScore(levelId).Score;
    }

    public int GetLevelStars(long subjectId, long levelId)
    {
        return GetSubjectHighScores(subjectId).GetHighScore(levelId).Stars;
    }

    public bool SubmitScore(long subjectId, long levelId, int score, int stars)
    {
        var newScore = new HighScore(levelId, score, stars);
        bool isNewHighScore = GetSubjectHighScores(subjectId).SubmitScore(newScore);
        if (isNewHighScore) SaveScores(subjectId);
        return isNewHighScore;
    }

    private void SaveScores(long subjectId)
    {
        string json = JsonUtility.ToJson(GetSubjectHighScores(subjectId));
        PlayerPrefs.SetString(subjectId.ToString(), json);
        PlayerPrefs.Save();
    }
}