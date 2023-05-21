using TMPro;
using UnityEngine;

// Used to display a representation of a question beneath a level.
public class QuestionDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionTextMeshProUGUI;
    
    public Question question;
    
    // Sets the text that is displayed by questionTextMeshProUGUI to be the level numeration; i.e. "Nivå 2".
    public void UpdateDisplay() { questionTextMeshProUGUI.text = question.QuestionText; }
    
    // Notify the EditToolScriptManager that the question displayed has been selected.
    public void SelectQuestion() { EditToolScriptManager.Instance.SelectQuestion(question); }
}