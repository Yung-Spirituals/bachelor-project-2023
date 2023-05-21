using SoData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCoin : MonoBehaviour
{
    public Level level;
    public int levelNumber;

    [SerializeField] private TextMeshProUGUI levelNumeration;

    [SerializeField] private Sprite unavailable;
    [SerializeField] private Sprite noStar;
    [SerializeField] private Sprite oneStar;
    [SerializeField] private Sprite twoStar;
    [SerializeField] private Sprite threeStar;

    private void Start() { Initialize(); }

    private void Initialize()
    {
        GameDataScriptableObject gdso = GameDataManager.Instance.GetGameData();
        
        // Display the number representing the level.
        levelNumeration.text = levelNumber.ToString();
        
        // If the level is not the first level in the subject and the player has not passed the level before it
        if (levelNumber != 1 && HighScoreManager.Instance.GetLevelStars(
                gdso.ActiveSubject.ID, gdso.ActiveSubject.Levels[levelNumber - 2].ID) == 0)
        {
            GetComponent<Image>().sprite = unavailable;
            levelNumeration.color =new Color32(22, 22, 22, 255);
            GetComponent<Button>().interactable = false;
        }
        // Display the amount of stars the player has achieved, zero if the player has not completed the stage yet.
        else
        {
            int stars = HighScoreManager.Instance.GetLevelStars(gdso.ActiveSubject.ID, level.ID);
            GetComponent<Image>().sprite = stars switch
            {
                3 => threeStar,
                2 => twoStar,
                1 => oneStar,
                0 => noStar,
                _ => GetComponent<Image>().sprite
            };
            
            // Slightly move the text indicating what level the button represents down and recolor it for visibility.
            levelNumeration.rectTransform.localPosition = new Vector3(0, -15);
            levelNumeration.color = new Color32(255, 255, 255, 255);
        }
    }

    // Display a popup component with level details and a button for playing the level.
    public void ShowPopUp() { LevelHubManager.Instance.DisplayPopUp(levelNumber, level); }
}
