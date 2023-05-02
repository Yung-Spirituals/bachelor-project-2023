using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonUtil
{
    [Serializable] 
    private class QuestionCollection { public List<Question> Questions; }
    
    [Serializable] 
    private class StoryCollection { public List<Story> _stories; }

    private static string _path = Application.dataPath + "/questions";

    public static void SaveJson(string story, string level, List<Question> questions)
    {
        string finalPath = _path + "/" + level + ".json";
        QuestionCollection questionCollection = new QuestionCollection { Questions = questions };
        string jString = JsonUtility.ToJson(questionCollection);
        File.WriteAllText(finalPath, jString);
    }
    
    public static List<Question> LoadJson(string story, string level)
    {
        string finalPath = _path + "/" + level + ".json";
        string jsonString = File.ReadAllText(finalPath);
        QuestionCollection collection = JsonUtility.FromJson<QuestionCollection>(jsonString);
        return collection.Questions;
    }

    public static List<Story> StoriesFromJson(string jsonString)
    {
        return JsonUtility.FromJson<StoryCollection>(jsonString)._stories;
    }
    
    public static void StoriesToJson(Story[] stories)
    {
        string json = JsonUtility.ToJson(stories);
        Debug.Log(json);
        StoryCollection collection1 = JsonUtility.FromJson<StoryCollection>(json);
    }
}
