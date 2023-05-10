using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtons : MonoBehaviour
{
    public GameObject[] answers;

    public void Answer(int option)
    {
        if (PauseManager.Instance.isPaused) return;
        ColorButtons();
        DisableButtonsAndDisplayNextQuestion();
        QuestionManager.Instance.Answer(option, true);
    }

    private void ColorButtons()
    {
        Question question = QuestionManager.Instance.CurrentQuestion;
        bool[] isCorrect = { question.GetIsOption0(), question.GetIsOption1(),
            question.GetIsOption2(), question.GetIsOption3() };
        for (int i = 0; i < answers.Length; i++)
        {
            if (isCorrect[i]) continue;
            answers[i].GetComponent<Image>().color = new Color32(244,140,81,255);
            answers[i].GetComponent<Shadow>().effectColor =new Color32(216,108,48,255);
        }
    }

    private IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(1f);

        foreach (GameObject answer in answers)
        {
            answer.GetComponent<Image>().color = new Color32(77,161,223,255);
            answer.GetComponent<Shadow>().effectColor = new Color32(32,112,172,255);
            answer.GetComponent<Button>().enabled = true;
        }
    }

    private void DisableButtonsAndDisplayNextQuestion()
    {
        foreach (GameObject answer in answers) answer.GetComponent<Button>().enabled = false;
        StartCoroutine(NextQuestion());
    }
}
