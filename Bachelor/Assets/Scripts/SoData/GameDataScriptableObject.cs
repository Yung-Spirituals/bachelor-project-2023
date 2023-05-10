using System.Collections.Generic;
using UnityEngine;

namespace SoData
{
    [CreateAssetMenu]
    public class GameDataScriptableObject : ScriptableObject
    {
        [SerializeField] private List<Story> stories;
        [SerializeField] private Story activeStory;
        [SerializeField] private Level activeLevel;
        [SerializeField] private Question activeQuestion;

        public List<Story> Stories
        {
            get => stories;
            set => stories = value;
        }
        
        public Story ActiveStory
        {
            get => activeStory;
            set => activeStory = value;
        }
        
        public Level ActiveLevel
        {
            get => activeLevel;
            set => activeLevel = value;
        }

        public Question ActiveQuestion
        {
            get => activeQuestion;
            set => activeQuestion = value;
        }
    }
}