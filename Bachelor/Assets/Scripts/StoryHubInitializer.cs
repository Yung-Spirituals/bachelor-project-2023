using System.Collections;
using System.Collections.Generic;
using SoData;
using UnityEngine;

public class StoryHubInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private bool isAdmin;
    [SerializeField] private GameObject _editGameDataButton;

    private void Start()
    {
        StartCoroutine(WaitFor());
    }

    private IEnumerator WaitFor()
    {
        GameDataScriptableObject scriptableObject = GameDataManager.Instance.GetGameData();
        scriptableObject.Stories = new List<Story>();
        scriptableObject = GameDataManager.Instance.GetGameData();
        yield return new WaitUntil(() => scriptableObject.Stories.Count != 0);
        List<Story> stories = scriptableObject.Stories;
        foreach (Story story in stories)
        {
            GameObject storyPreview = Instantiate(_gameObject, _parentTransform);
            StoryDisplay storyDisplay = storyPreview.GetComponent<StoryDisplay>();
            storyDisplay.story = story;
            storyDisplay.UpdateDisplay();
        }

        if (isAdmin) { Instantiate(_editGameDataButton, _parentTransform); }
    }
}