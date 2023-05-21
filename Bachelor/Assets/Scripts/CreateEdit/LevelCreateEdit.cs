using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Class responsible for the editing and creation of new levels.
public class LevelCreateEdit : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown gameModes;
    [SerializeField] private TMP_InputField goalText;
    
    private void Start()
    {
        // Prepare the options for the dropdown element.
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

    // Notifies the EditToolScriptManager to create a new level.
    public void NewLevel() { EditToolScriptManager.Instance.NewLevel(); }
    
    // Start saving the changes to the level.
    public void Save() { StartCoroutine(SaveLevel()); }

    // Send the current state of the level data to the backend, where it will be saved.
    private IEnumerator SaveLevel()
    {
        // Update local information using what the user has inputted.
        Level level = GameDataManager.Instance.GetGameData().ActiveLevel;
        level.Subject = GameDataManager.Instance.GetGameData().ActiveSubject;
        level.LevelType = gameModes.options[gameModes.value].text;
        level.LevelGoal = goalText.text;
        
        /*
         * New levels (levels that are not saved to the backend yet) always have ID == 0.
         * Send a request to add a new level to the backend if the level that is being saved is new.
         */
        if (level.ID == 0) yield return WebCommunicationUtil.PutNewGameDataRequest(
                    level.Subject, level, null, "/level");
        
        // If the level is already in the backend, send the updated version of it to the backend.
        else yield return WebCommunicationUtil.PutUpdateGameDataRequest(
                    level.Subject, level, null, "/level");
        
        // Refresh the data held locally in the game.
        yield return EditToolScriptManager.Instance.Refresh();
        
        // Notify the user that all changes have been saved.
        EditToolScriptManager.Instance.DisplayPopup(
            "Lagret!",
            "Alle endringer er lagret, du kan nå forlate denne siden.",
            false);
    }
    
    /*
     * Attempt to leave the level create edit page.
     * If there are any unsaved changes, the user will get a confirmation popup.
     */
    public void LeaveLevel()
    {
        Level level = GameDataManager.Instance.GetGameData().ActiveLevel;
        
        // Ask the user if they are sure they wish to leave, as the new level is not saved.
        if (level.ID == 0) EditToolScriptManager.Instance.DisplayPopup(
                "Dette nivået er ikke lagret enda",
                "Dette nivået er ikke lagret enda, er du sikker på at du vil fortsette?",
                true);
        
        // Ask the user if they are sure they wish to leave, as any changes will be lost.
        else if (level.LevelType != gameModes.options[gameModes.value].text || level.LevelGoal != goalText.text) 
            EditToolScriptManager.Instance.DisplayPopup(
                "Ikke-lagrede endringer!",
                "Du har en eller flere ikke-lagrede endringer, er du sikker på at du vil fortsette?",
                true);
        
        // Leave the level edit create page immediately as no changes has been made.
        else EditToolScriptManager.Instance.Back();
    }
}