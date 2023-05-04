using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI levelTextMeshProUGUI;
    [SerializeField] private TMPro.TextMeshProUGUI gameModeTextMeshProUGUI;
    
    public Level level;
    
    public void UpdateDisplay()
    {
        levelTextMeshProUGUI.text = level.LevelName;
        gameModeTextMeshProUGUI.text = level.LevelType;
    }

    public void SelectLevel() { EditToolScriptManager.Instance.SelectLevel(level); }
}