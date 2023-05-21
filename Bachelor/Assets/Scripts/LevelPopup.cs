using SoData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : MonoBehaviour
{
    public Level level;
    public int levelNumber;

    [SerializeField] private TextMeshProUGUI levelNumeration;

    [SerializeField] private Sprite unavailable;
    [SerializeField] private Sprite noStar;
    [SerializeField] private Sprite oneStar;
    [SerializeField] private Sprite twoStar;
    [SerializeField] private Sprite threeStar;

    private void Start()
    {
        GameDataScriptableObject gdso = GameDataManager.Instance.GetGameData();
        if (levelNumber != 1 && HighScoreManager.Instance.GetLevelStars(
                gdso.ActiveSubject.ID, gdso.ActiveSubject.Levels[levelNumber - 2].ID) == 0)
        {
            GetComponent<Image>().sprite = unavailable;
            levelNumeration.color =new Color32(22, 22, 22, 255);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            int stars = HighScoreManager.Instance.GetLevelStars(
                gdso.ActiveSubject.ID, level.ID);
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
