using System.Collections.Generic;
using UnityEngine;

namespace SoData
{
    // A scriptable object is a data container that is useful for storing data that needs to persist between scenes.
    [CreateAssetMenu]
    public class GameDataScriptableObject : ScriptableObject
    {
        // Meant to hold game data received from the backend.
        public List<Subject> Subjects { get; set; }
        
        // Holds the subject that is currently being interacted with, is null if none is being interacted with.
        public Subject ActiveSubject { get; set; }
        
        // Holds the level that is currently being interacted with, is null if none is being interacted with.
        public Level ActiveLevel { get; set; }

        // Holds the question that is currently being interacted with, is null if none is being interacted with.
        public Question ActiveQuestion { get; set; }
    }
}