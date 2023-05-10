using System.Collections;
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
        EditToolScriptManager.Instance.NewLevel();
    }
    
    public void Save()
    {
        StartCoroutine(SaveLevel());
    }

    private IEnumerator SaveLevel()
    {
        Level level = GameDataManager.Instance.GetGameData().ActiveLevel;
        level.Story = GameDataManager.Instance.GetGameData().ActiveStory;
        level.LevelType = gameModes.options[gameModes.value].text;
        level.LevelGoal = goalText.text;
        if (level.ID == 0)
        {
            yield return WebCommunicationUtil.PutNewGameDataRequest(
                    level.Story, level, null, "/level");
        }
        else
        { 
            yield return WebCommunicationUtil.PutUpdateGameDataRequest(
                    level.Story, level, null, "/level");
        }
        yield return EditToolScriptManager.Instance.Refresh();
    }
}