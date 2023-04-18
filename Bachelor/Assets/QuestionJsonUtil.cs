using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionJsonUtil : MonoBehaviour
{
    [Serializable]
    public class QuestionCollection
    {
        public List<Question> Questions;
    }
    
    public static QuestionJsonUtil Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(QuestionJsonUtil)) as QuestionJsonUtil;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static QuestionJsonUtil instance;


    private string _path;

    private void Start()
    {
        _path = Application.dataPath + "/questions";
    }

    public void SaveJson(string story, string level, List<Question> questions)
    {
        string finalPath = _path + "/" + level + ".json";
        QuestionCollection questionCollection = new QuestionCollection
        {
            Questions = questions
        };
        string jString = JsonUtility.ToJson(questionCollection);
        File.WriteAllText(finalPath, jString);
    }
    
    public List<Question> LoadJson(string story, string level)
    {
        string finalPath = _path + "/" + level + ".json";
        string jsonString = File.ReadAllText(finalPath);
        JsonUtility.FromJson<QuestionCollection>(jsonString);
        QuestionCollection collection = JsonUtility.FromJson<QuestionCollection>(jsonString);
        return collection.Questions;
    }
}
