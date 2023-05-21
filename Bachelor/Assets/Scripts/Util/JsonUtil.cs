using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonUtil
{
    /*
     * Used to encapsulate a list of subjects.
     * Necessary due to serialization restrictions preventing lists and arrays from being properly serialized.
     */ 
    [Serializable] private class SubjectCollection { [SerializeField] public List<Subject> subjects; }

    /*
     * Uses the JsonUtility class provided by the unity framework to convert a json string into a subject collection.
     * Returns the list of subjects contained within the subject collection object.
     */
    public static List<Subject> SubjectsFromJson(string jsonString)
    {
        return JsonUtility.FromJson<SubjectCollection>(jsonString).subjects;
    }

    // Useful serialization encapsulation for serializing different entity types simultaneously.
    [Serializable] private class JsonMultiObject
    {
        [SerializeField] public Subject subject;
        [SerializeField] public Level level;
        [SerializeField] public Question question;
    }
    
     // Creates a JsonMultiObject that is then serialized into a json string that is then returned.
     public static string SubjectLevelQuestionToJson(Subject subject, Level level, Question question)
    {
        return JsonUtility.ToJson(new JsonMultiObject()
        {
            subject = subject,
            level = level,
            question = question
        });
    }
}
