using System.Collections;
using TMPro;
using UnityEngine;

public class SubjectCreateEdit : MonoBehaviour
{
    [SerializeField] private TMP_InputField subjectName;
    [SerializeField] private TMP_InputField subjectDescription;

    public void NewSubject() { EditToolScriptManager.Instance.NewSubject(); }

    public void Save() { StartCoroutine(SaveSubject()); }

    private IEnumerator SaveSubject()
    {
        Subject subject = GameDataManager.Instance.GetGameData().ActiveSubject;
        if (subjectName.text == "")
        {
            EditToolScriptManager.Instance.DisplayPopup(
                "Mangler tittel!",
                "Vennligst oppgi en tittel før du fortsetter.",
                false);
            yield break;
        }
        subject.SubjectName = subjectName.text;
        subject.SubjectDescription = subjectDescription.text;
        
        if (subject.ID == 0) yield return WebCommunicationUtil.PutNewGameDataRequest(
                subject,null, null, "/subject");
        else yield return WebCommunicationUtil.PutUpdateGameDataRequest(
                subject, null, null, "/subject");
        
        yield return EditToolScriptManager.Instance.Refresh();
        EditToolScriptManager.Instance.DisplayPopup(
            "Lagret!",
            "Alle endringer er lagret, du kan nå forlate denne siden.",
            false);
    }
    
    public void LeaveSubject()
    {
        if (CheckForChanges()) 
            EditToolScriptManager.Instance.DisplayPopup(
                "Ikke-lagrede endringer!",
                "Du har en eller flere ikke-lagrede endringer, er du sikker på at du vil fortsette?",
                true);
        else EditToolScriptManager.Instance.Back();
    }

    private bool CheckForChanges()
    {
        Subject subject = GameDataManager.Instance.GetGameData().ActiveSubject;
        bool changed = subject.ID == 0 ||
                       subject.SubjectName != subjectName.text ||
                       subject.SubjectDescription != subjectDescription.text;
        return changed;
    }
}   