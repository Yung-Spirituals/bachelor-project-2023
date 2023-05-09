using System;
using System.Collections.Generic;
using SoData;
using UnityEngine;

public static class JsonUtil
{
    [Serializable] 
    private class StoryCollection { [SerializeField] public List<Story> _stories; }
    
    [Serializable]
    private class JsonMultiObject
    {
        [SerializeField] public Story _story;
        [SerializeField] public Level _level;
        [SerializeField] public Question _question;
    }

    public static List<Story> StoriesFromJson(string jsonString)
    {
        return JsonUtility.FromJson<StoryCollection>(jsonString)._stories;
    }

    public static string StoriesToJson(List<Story> stories)
    {
        return JsonUtility.ToJson(new StoryCollection() {_stories = stories});
    }

    public static string StoryLevelQuestionToJson(Story story, Level level, Question question)
    {
        return JsonUtility.ToJson(new JsonMultiObject()
        {
            _story = story,
            _level = level,
            _question = question
        });
    }

    public static void UpdateLocalObjects(Story story, Level level, Question question, string jsonString)
    {
        JsonMultiObject updatedInfo = JsonUtility.FromJson<JsonMultiObject>(jsonString);
        GameDataScriptableObject gdso = GameDataManager.Instance.GetGameData();
        if (question != null) gdso.ActiveQuestion = updatedInfo._question;
        if (level != null) gdso.ActiveLevel = updatedInfo._level;
        if (story != null) gdso.ActiveStory = updatedInfo._story;
    }
}
