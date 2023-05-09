using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : MonoBehaviour
{
    public Level level;
    public int levelNumber;

    [SerializeField] private Sprite unavailable;
    [SerializeField] private Sprite noStar;
    [SerializeField] private Sprite oneStar;
    [SerializeField] private Sprite twoStar;
    [SerializeField] private Sprite threeStar;

    private void Start()
    {
        int stars = HighScoreManager.Instance.GetLevelStars(
            GameDataManager.Instance.GetGameData().ActiveStory.StoryTitle, levelNumber.ToString());
        Debug.Log("" + stars);
        switch (stars)
        {
            case 3:
                GetComponent<Image>().sprite = threeStar;
                break;
            case 2:
                GetComponent<Image>().sprite = twoStar;
                break;
            case 1:
                GetComponent<Image>().sprite = oneStar;
                break;
            case 0:
                GetComponent<Image>().sprite = noStar;
                break;
        }
    }

    public void ShowPopUp() { LevelHubManager.Instance.DisplayPopUp(levelNumber, level); }
}
