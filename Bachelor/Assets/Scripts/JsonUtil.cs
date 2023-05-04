using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonUtil
{
    [Serializable] 
    private class StoryCollection { [SerializeField] public List<Story> _stories; }
    
    [Serializable]
    private class JsonMultiObject
    {
        [SerializeField] public Story _story;
        [SerializeField] public Level _Level;
        [SerializeField] public Question _question;
    }

    public static List<Story> StoriesFromJson(string jsonString)
    {
        return JsonUtility.FromJson<StoryCollection>(jsonString)._stories;
    }

    public static string StoriesToJson(List<Story> stories)
    {
        return JsonUtility.ToJson(new StoryCollection(){_stories = stories});
    }

    public static string StoryLevelQuestionToJson(Story story, Level level, Question question)
    {
        return JsonUtility.ToJson(new JsonMultiObject()
        {
            _story = story,
            _Level = level,
            _question = question
        });
    }
}
