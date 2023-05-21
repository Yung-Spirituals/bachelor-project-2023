using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelTextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI gameModeTextMeshProUGUI;
    
    public Level level;
    
    public void UpdateDisplay() { gameModeTextMeshProUGUI.text = level.LevelType; }

    public void SetLevelDisplayName(string levelName) { levelTextMeshProUGUI.text = levelName; }

    public void SelectLevel() { EditToolScriptManager.Instance.SelectLevel(level); }
}