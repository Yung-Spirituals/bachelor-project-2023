using TMPro;
using UnityEngine;

public class QuestionDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionTextMeshProUGUI;
    
    public Question question;

    public void UpdateDisplay() { questionTextMeshProUGUI.text = question.QuestionText; }
    
    public void SelectQuestion() { EditToolScriptManager.Instance.SelectQuestion(question); }
}