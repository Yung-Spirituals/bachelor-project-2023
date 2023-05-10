using UnityEngine;

public class QuestionDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI questionTextMeshProUGUI;
    
    public Question question;

    public void UpdateDisplay() { questionTextMeshProUGUI.text = question.GetQuestion(); }
    
    public void SelectQuestion() { EditToolScriptManager.Instance.SelectQuestion(question); }
}