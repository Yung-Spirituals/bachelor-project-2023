using System;
using UnityEngine;

public class StoryPopup : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI storyName;
    
    public Story story;

    private void Start()
    {
        storyName.text = story.StoryName;
    }

    public void ShowPopUp() { StoryHubManager.Instance.DisplayPopUp(story); }
}