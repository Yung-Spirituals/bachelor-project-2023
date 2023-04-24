using TMPro;
using UnityEngine;

public class QuestionDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _question;
    [SerializeField] private TextMeshProUGUI _option0;
    [SerializeField] private TextMeshProUGUI _option1;
    [SerializeField] private TextMeshProUGUI _option2;
    [SerializeField] private TextMeshProUGUI _option3;
    [SerializeField] private bool _isTrueOrFalse;
    private Question _privateQuestion;

    public QuestionDisplay(bool isTrueOrFalse) { _isTrueOrFalse = isTrueOrFalse; }

    public void SetQuestion(Question question) { _privateQuestion = question; }

    public void DisplayQuestion()
    {
        _question.text = _privateQuestion.GetQuestion();
        _option0.text = _privateQuestion.GetOptions()[0];
        _option1.text = _privateQuestion.GetOptions()[1];
        if (_isTrueOrFalse) return;
        _option2.text = _privateQuestion.GetOptions()[2];
        _option3.text = _privateQuestion.GetOptions()[3];
    }
}