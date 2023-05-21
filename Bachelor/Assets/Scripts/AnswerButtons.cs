using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtons : MonoBehaviour
{
    public GameObject[] answers;
    public bool iconsEnabled;

    // Submit an option as an answer to the question manager.
    public void Answer(int option)
    {
        // Ensure that t
        if (PauseManager.Instance.isPaused) return;
        ColorButtons();
        DisableButtonsAndDisplayNextQuestion();
        QuestionManager.Instance.Answer(option, true);
    }

    private void ColorButtons()
    {
        Question question = QuestionManager.Instance.CurrentQuestion;
        bool[] isCorrect = { question.IsOption0, question.IsOption1,
            question.IsOption2, question.IsOption3 };
        for (int i = 0; i < answers.Length; i++)
        {
            if (isCorrect[i])
            {
                if (!iconsEnabled) continue;
                answers[i].GetComponent<EnableDisableIcons>().SetActive(true);
                answers[i].GetComponent<EnableDisableIcons>().SetGameObjectActive(true);
            }
            else
            {
                answers[i].GetComponent<Image>().color = new Color32(244,140,81,255);
                answers[i].GetComponent<Shadow>().effectColor =new Color32(216,108,48,255);
                if (!iconsEnabled) continue;
                answers[i].GetComponent<EnableDisableIcons>().SetActive(false);
                answers[i].GetComponent<EnableDisableIcons>().SetGameObjectActive(true);
            }
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
            if (!iconsEnabled) continue;
            answer.GetComponent<EnableDisableIcons>().SetGameObjectActive(false);
        }
    }

    private void DisableButtonsAndDisplayNextQuestion()
    {
        foreach (GameObject answer in answers) answer.GetComponent<Button>().enabled = false;
        StartCoroutine(NextQuestion());
    }
}
