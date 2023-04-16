using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtons : MonoBehaviour
{
    public GameObject[] answers;

    public void Answer(int option)
    {
        if (PauseManager.Instance.isPaused) return;
        ColorButtons(option, QuestionManager.Instance.Answer(option, true));
        DisableButtonsAndDisplayNextQuestion();
    }

    private void ColorButtons(int option, bool correct)
    {
        answers[option].GetComponent<Image>().color = correct ? new Color32(155, 213, 82, 255) :
            new Color32(255, 103, 103, 255);
    }

    private IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(1);

        foreach (GameObject answer in answers)
        {
            answer.GetComponent<Image>().color = Color.white;
            answer.GetComponent<Button>().enabled = true;
        }
    }

    private void DisableButtonsAndDisplayNextQuestion()
    {
        foreach (GameObject answer in answers) answer.GetComponent<Button>().enabled = false;
        StartCoroutine(NextQuestion());
    }
}
