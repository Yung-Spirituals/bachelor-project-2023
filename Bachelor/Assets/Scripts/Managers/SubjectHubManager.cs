using TMPro;
using UnityEngine;

public class SubjectHubManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subjectNameText;
    [SerializeField] private TextMeshProUGUI subjectDescriptionText;
    [SerializeField] private GameObject goToSubjectButton;
    [SerializeField] private GameObject popUp;
    private Subject _subject;
    
    public static SubjectHubManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(SubjectHubManager)) as SubjectHubManager;
 
            return _instance;
        }
        set => _instance = value;
    }
    private static SubjectHubManager _instance;

    public void DisplayPopUp(Subject subject)
    {
        _subject = subject;
        subjectNameText.text = subject.SubjectName;
        subjectDescriptionText.text = subject.SubjectDescription;
        goToSubjectButton.GetComponent<SwitchScene>().scene = "LevelHub";
        popUp.SetActive(true);
    }

    public void SelectSubject()
    {
        GameDataManager.Instance.GetGameData().ActiveSubject = _subject;
        goToSubjectButton.GetComponent<SwitchScene>().LoadNewScene();
    }
}