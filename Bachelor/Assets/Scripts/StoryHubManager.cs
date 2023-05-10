using UnityEngine;

public class StoryHubManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI storyNameText;
    //[SerializeField] private TMPro.TextMeshProUGUI storyTitleText;
    //[SerializeField] private TMPro.TextMeshProUGUI storyShortDescriptionText;
    [SerializeField] private TMPro.TextMeshProUGUI storyFullDescriptionText;
    [SerializeField] private GameObject goToStoryButton;
    [SerializeField] private GameObject popUp;
    private Story _story;
    
    public static StoryHubManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(StoryHubManager)) as StoryHubManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static StoryHubManager instance;

    public void DisplayPopUp(Story story)
    {
        _story = story;
        //string highScore = "HighScore: " + HighScoreManager.Instance.GetLevelHighScore(
        //    GameDataManager.Instance.GetGameData().ActiveStory.StoryTitle, levelNumber.ToString());
        storyNameText.text = story.StoryName;
        //storyTitleText.text = story.StoryTitle;
        //storyShortDescriptionText.text = story.StoryShortDescription;
        storyFullDescriptionText.text = story.StoryFullDescription;
        goToStoryButton.GetComponent<SwitchScene>().scene = "LevelHub"; //TODO: Level hub scene goes here
        popUp.SetActive(true);
    }

    public void SelectStory()
    {
        GameDataManager.Instance.GetGameData().ActiveStory = _story;
        goToStoryButton.GetComponent<SwitchScene>().LoadNewScene();
    }
}