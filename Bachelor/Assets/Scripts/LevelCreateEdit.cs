using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCreateEdit : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown gameModes;
    [SerializeField] private TMP_InputField goalText;

    private void Start()
    {
        gameModes.options.Clear();
        List<string> items = new ()
        {
            GameMode.Standard,
            GameMode.TrueOrFalse,
            GameMode.Rank,
            GameMode.MemoryCards
        };

        foreach (string item in items)
        {
            gameModes.options.Add(new TMP_Dropdown.OptionData(item));
        }
    }

    public void NewLevel()
    {
        EditToolScriptManager.Instance.NewLevel(gameModes.itemText.text);
    }

    public void Save()
    {
        Level level = GameDataManager.Instance.GetGameData().ActiveLevel;
        level.LevelType = gameModes.itemText.text;
        level.LevelGoal = goalText.text;
        StartCoroutine(WebCommunicationUtil
                .PutUpdateGameDataRequest(GameDataManager.Instance.GetGameData().ActiveStory, level, null));
        EditToolScriptManager.Instance.Save();
    }
}