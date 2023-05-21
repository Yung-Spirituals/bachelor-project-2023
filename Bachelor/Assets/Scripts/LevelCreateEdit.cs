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

        foreach (string item in items) gameModes.options.Add(new TMP_Dropdown.OptionData(item));
        
    }

    public void NewLevel() { EditToolScriptManager.Instance.NewLevel(); }
    
    public void Save() { StartCoroutine(SaveLevel()); }

    private IEnumerator SaveLevel()
    {
        //add errors for lacking info, info popup for the error.
        Level level = GameDataManager.Instance.GetGameData().ActiveLevel;
        level.Subject = GameDataManager.Instance.GetGameData().ActiveSubject;
        level.LevelType = gameModes.options[gameModes.value].text;
        level.LevelGoal = goalText.text;
        
        if (level.ID == 0) yield return WebCommunicationUtil.PutNewGameDataRequest(
                    level.Subject, level, null, "/level");
        else yield return WebCommunicationUtil.PutUpdateGameDataRequest(
                    level.Subject, level, null, "/level");
        
        yield return EditToolScriptManager.Instance.Refresh();
        EditToolScriptManager.Instance.DisplayPopup(
            "Lagret!",
            "Alle endringer er lagret, du kan nå forlate denne siden.",
            false);
    }
    public void LeaveLevel()
    {
        Level level = GameDataManager.Instance.GetGameData().ActiveLevel;
        if (level.ID == 0) EditToolScriptManager.Instance.DisplayPopup(
                "Dette nivået er ikke lagret enda",
                "Dette nivået er ikke lagret enda, er du sikker på at du vil fortsette?",
                true);
        else if (level.LevelType != gameModes.options[gameModes.value].text || level.LevelGoal != goalText.text) 
            EditToolScriptManager.Instance.DisplayPopup(
                "Ikke-lagrede endringer!",
                "Du har en eller flere ikke-lagrede endringer, er du sikker på at du vil fortsette?",
                true);
        else EditToolScriptManager.Instance.Back();
    }
}