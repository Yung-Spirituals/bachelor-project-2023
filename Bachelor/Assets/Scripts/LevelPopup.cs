using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : MonoBehaviour
{
    public Level level;
    public int levelNumber;
    public bool available = true;

    [SerializeField] private TMPro.TextMeshProUGUI levelNumeration;

    [SerializeField] private Sprite unavailable;
    [SerializeField] private Sprite noStar;
    [SerializeField] private Sprite oneStar;
    [SerializeField] private Sprite twoStar;
    [SerializeField] private Sprite threeStar;

    private void Start()
    {
        if (levelNumber != 1 && HighScoreManager.Instance
                .GetLevelStars(GameDataManager.Instance.GetGameData().ActiveStory.StoryTitle,
                    (levelNumber - 1).ToString()) == 0)
        {
            GetComponent<Image>().sprite = unavailable;
            levelNumeration.color =new Color32(22, 22, 22, 255);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            int stars = HighScoreManager.Instance.GetLevelStars(
                GameDataManager.Instance.GetGameData().ActiveStory.StoryTitle, levelNumber.ToString());
            GetComponent<Image>().sprite = stars switch
            {
                3 => threeStar,
                2 => twoStar,
                1 => oneStar,
                0 => noStar,
                _ => GetComponent<Image>().sprite
            };
            
            levelNumeration.rectTransform.localPosition = new Vector3(0, -15);
            levelNumeration.color = new Color32(255, 255, 255, 255);
        }
        
        levelNumeration.text = levelNumber.ToString();
    }

    public void ShowPopUp() { LevelHubManager.Instance.DisplayPopUp(levelNumber, level); }
}
