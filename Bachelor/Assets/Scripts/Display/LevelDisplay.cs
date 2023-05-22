using TMPro;
using UnityEngine;

// Used to display a representation of a level beneath a subject.
public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelTextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI gameModeTextMeshProUGUI;
    
    public Level level;
    
    // Sets the text that is displayed by gameModeTextMeshProUGUI to be the type of the level.
    public void UpdateDisplay() { gameModeTextMeshProUGUI.text = level.LevelType; }

    // Sets the text that is displayed by levelTextMeshProUGUI to that of the level name.
    public void SetLevelDisplayName(string levelName) { levelTextMeshProUGUI.text = levelName; }

    // Notify the EditToolScriptManager that the level displayed has been selected.
    public void SelectLevel() { EditToolScriptManager.Instance.SelectLevel(level); }
}