using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreManager: MonoBehaviour
{
    private List<HighScoreCollection> _highScoreCollections;
    [SerializeField] private List<string> _stories;
    private class HighScore
    {
        public string _levelName;
        public int _score;

        public HighScore(string levelName, int score)
        {
            _levelName = levelName;
            _score = score;
        }
    }
    private class HighScoreCollection
    {
        public string story;
        public static List<HighScore> _highScores = new ();

        public HighScoreCollection(string story)
        {
            this.story = story;
        }

        public bool SubmitScore(HighScore newScore)
        {
            var highScore = GetHighScore(newScore._levelName);
            if (highScore._score >= newScore._score) return false;
            highScore._score = newScore._score;
            return true;
        }

        public HighScore GetHighScore(string levelName)
        {
            var highScore = _highScores.Find(score => score._levelName == levelName);
            if (highScore != null) return highScore;
                
            highScore = new HighScore(levelName, 0);
            _highScores.Add(highScore);
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
        foreach (var highScoreCollection in _stories.Select(story => JsonUtility.FromJson<HighScoreCollection>(
                     PlayerPrefs.GetString(story, null)) ?? new HighScoreCollection(story)))
        {
            _highScoreCollections.Add(highScoreCollection);
        }
    }

    private HighScoreCollection GetStoryHighScores(string story)
    {
        var storyHighScores = _highScoreCollections.Find(storyScores => storyScores.story == story);
        if (storyHighScores != null) return storyHighScores;
                
        storyHighScores = new HighScoreCollection(story);
        _highScoreCollections.Add(storyHighScores);
        return storyHighScores;
    }

    public int GetLevelHighScore(string story, string levelName)
    {
        return GetStoryHighScores(story).GetHighScore(levelName)._score;
    }

    public bool SubmitScore(string story, string levelName, int score)
    {
        var newScore = new HighScore(levelName, score);
        bool isNewHighScore = GetStoryHighScores(story).SubmitScore(newScore);
        if (isNewHighScore) SaveScores(story);
        return isNewHighScore;
    }

    private void SaveScores(string story)
    {
        string json = JsonUtility.ToJson(GetStoryHighScores(story));
        PlayerPrefs.SetString(story, json);
        PlayerPrefs.Save();
    }
}