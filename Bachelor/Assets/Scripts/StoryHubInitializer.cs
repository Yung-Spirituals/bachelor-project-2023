using System.Collections.Generic;
using SoData;
using UnityEngine;

public class StoryHubInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Transform _parentTransform;

    private void Start()
    {
        GameDataScriptableObject scriptableObject = GameDataManager.Instance.GetGameData();
        List<Story> stories = scriptableObject.Stories;
        for (int i = 0; i < stories.Count; i++)
        {
            GameObject storyPreview = Instantiate(_gameObject, _parentTransform);
            StoryPopup storyPopup = storyPreview.GetComponent<StoryPopup>();
            storyPopup.story = stories[i];
        }
    }
}