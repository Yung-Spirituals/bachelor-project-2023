using TMPro;
using UnityEngine;

public class StoryCreateEdit : MonoBehaviour
{
    [SerializeField] private TMP_InputField storyName;
    [SerializeField] private TMP_InputField storyDescription;

    public void NewStory()
    {
        EditToolScriptManager.Instance.NewStory(storyName.text);
    }

    public void SaveStory()
    {
        Story story = GameDataManager.Instance.GetGameData().ActiveStory;
        story.StoryName = storyName.text;
        story.StoryFullDescription = storyDescription.text;
        StartCoroutine(WebCommunicationUtil.PutUpdateGameDataRequest(story, null, null));
        EditToolScriptManager.Instance.Save();
    }
}