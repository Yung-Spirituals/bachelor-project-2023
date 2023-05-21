using TMPro;
using UnityEngine;

// Used to display a representation of a subject.
public class SubjectDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subjectName;
    
    [SerializeField] public Subject subject;

    // Sets the text that is displayed by subjectName to be the name of the subject.
    public void UpdateDisplay() { subjectName.text = subject.SubjectName; }

    public void ShowPopUp() { SubjectHubManager.Instance.DisplayPopUp(subject); }

    public void SelectSubject() { EditToolScriptManager.Instance.SelectSubject(subject); }
}