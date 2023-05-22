using System.Collections;
using TMPro;
using UnityEngine;

// Class responsible for the editing and creation of new subjects.
public class SubjectCreateEdit : MonoBehaviour
{
    [SerializeField] private TMP_InputField subjectName;
    [SerializeField] private TMP_InputField subjectDescription;

    // Notifies the EditToolScriptManager to create a new level.
    public void NewSubject() { EditToolScriptManager.Instance.NewSubject(); }

    // Start saving the changes to the subject.
    public void Save() { StartCoroutine(SaveSubject()); }

    // Send the current state of the subject data to the backend, where it will be saved.
    private IEnumerator SaveSubject()
    {
        Subject subject = GameDataManager.Instance.GetGameData().ActiveSubject;
        
        // A subject requires a name.
        if (subjectName.text == "")
        {
            EditToolScriptManager.Instance.DisplayPopup(
                "Mangler tittel!",
                "Vennligst oppgi en tittel før du fortsetter.",
                false);
            yield break;
        }
        
        // Updates the subject values.
        subject.SubjectName = subjectName.text;
        subject.SubjectDescription = subjectDescription.text;
        
        /*
         * New subjects (subjects that are not saved to the backend yet) always have ID == 0.
         * Send a request to add a new subject to the backend if the subject that is being saved is new.
         */
        if (subject.ID == 0) yield return WebCommunicationUtil.PutNewGameDataRequest(
                subject,null, null, "/subject");
        
        // If the level is already in the backend, send the updated version of it to the backend.
        else yield return WebCommunicationUtil.PutUpdateGameDataRequest(
                subject, null, null, "/subject");
        
        // Refresh the data held locally in the game.
        yield return EditToolScriptManager.Instance.Refresh();
        
        // Notify the user that all changes have been saved.
        EditToolScriptManager.Instance.DisplayPopup(
            "Lagret!",
            "Alle endringer er lagret, du kan nå forlate denne siden.",
            false);
    }
    
    /*
     * Attempt to leave the subject create edit page.
     * If there are any unsaved changes, the user will get a confirmation popup.
     */
    public void LeaveSubject()
    {
        if (CheckForChanges())
            EditToolScriptManager.Instance.DisplayPopup(
                "Ikke-lagrede endringer!",
                "Du har en eller flere ikke-lagrede endringer, er du sikker på at du vil fortsette?",
                true);
        else EditToolScriptManager.Instance.Back();
    }

    // Check for any unsaved changes
    private bool CheckForChanges()
    {
        Subject subject = GameDataManager.Instance.GetGameData().ActiveSubject;
        bool changed = subject.ID == 0 ||
                       subject.SubjectName != subjectName.text ||
                       subject.SubjectDescription != subjectDescription.text;
        return changed;
    }
}   