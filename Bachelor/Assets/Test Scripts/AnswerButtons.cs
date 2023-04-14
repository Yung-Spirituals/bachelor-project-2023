using System.Collections;
using Test_Scripts;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtons : MonoBehaviour
{
    public GameObject[] answers;

    public void Answer(int option)
    {
        if (PauseManager.Instance.isPaused) { return; }
        bool correct = false;
        if (QuestionGenerate.actualAnswer == option)
        {
            ScoreManager.Instance.ChangeScore(1);
            correct = true;
        }
        ColorButtons(option, correct);
        DisableButtonsAndDisplayNextQuestion();
    }

    private void ColorButtons(int option, bool correct)
    {
        answers[option].GetComponent<Image>().color = correct ? new Color32(155, 213, 82, 255) :
            new Color32(255, 103, 103, 255);
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(1);

        foreach (GameObject anwser in answers)
        {
            anwser.GetComponent<Image>().color = Color.white;
            anwser.GetComponent<Button>().enabled = true;
        }
        QuestionGenerate.NextQuestion();
    }

    private void DisableButtonsAndDisplayNextQuestion()
    {
        foreach (GameObject anwser in answers)
        {
            anwser.GetComponent<Button>().enabled = false;
        }
        StartCoroutine(NextQuestion());
    }
}
