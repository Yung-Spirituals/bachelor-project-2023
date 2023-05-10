using UnityEngine;

public class StoryDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI storyName;
    
    [SerializeField] public Story story;

    public void UpdateDisplay() { storyName.text = story.StoryName; }

    public void ShowPopUp() { StoryHubManager.Instance.DisplayPopUp(story); }

    public void SelectStory() { EditToolScriptManager.Instance.SelectStory(story); }
}