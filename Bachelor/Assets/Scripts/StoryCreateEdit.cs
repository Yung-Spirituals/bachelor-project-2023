using System.Collections;
using TMPro;
using UnityEngine;

public class StoryCreateEdit : MonoBehaviour
{
    [SerializeField] private TMP_InputField storyName;
    [SerializeField] private TMP_InputField storyDescription;

    public void NewStory()
    {
        EditToolScriptManager.Instance.NewStory();
    }

    public void Save()
    {
        StartCoroutine(SaveStory());
    }

    private IEnumerator SaveStory()
    {
        Story story = GameDataManager.Instance.GetGameData().ActiveStory;
        story.StoryName = storyName.text;
        story.StoryFullDescription = storyDescription.text;
        if (story.ID == 0)
        {
            yield return WebCommunicationUtil.PutNewGameDataRequest(
                story,null, null, "/story");
        }
        else
        {
            yield return WebCommunicationUtil.PutUpdateGameDataRequest(
                story, null, null, "/story");
        }
        yield return EditToolScriptManager.Instance.Refresh();
    }
}   