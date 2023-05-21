using TMPro;
using UnityEngine;

public class SubjectDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subjectName;
    
    [SerializeField] public Subject subject;

    public void UpdateDisplay() { subjectName.text = subject.SubjectName; }

    public void ShowPopUp() { SubjectHubManager.Instance.DisplayPopUp(subject); }

    public void SelectSubject() { EditToolScriptManager.Instance.SelectSubject(subject); }
}