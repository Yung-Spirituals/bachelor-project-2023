using UnityEngine;

public class LevelPopup : MonoBehaviour
{
    public Level level;
    public int levelNumber;

    public void ShowPopUp() { LevelHubManager.Instance.DisplayPopUp(levelNumber, level); }
}
