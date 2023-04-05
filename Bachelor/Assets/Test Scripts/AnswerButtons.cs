using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtons : MonoBehaviour
{
    public GameObject[] answers;

    public GameObject currentScore;
    
    //TODO: Score needs to be moved out of here
    public int scoreValue;

    private void Update()
    {
        currentScore.GetComponent<TMPro.TextMeshProUGUI>().text = "SCORE: " + scoreValue;
    }
    
    public void Answer(int option)
    {
        bool correct = false;
        if (QuestionGenerate.actualAnswer == option)
        {
            scoreValue += 5;
            correct = true;
        }
        ColorButtons(option, correct);
        DisableButtonsAndDisplayNextQuestion();
    }

    private void ColorButtons(int option, bool correct)
    {
        answers[option].GetComponent<Image>().color = correct ? new Color32(144, 238, 144, 255) :
            new Color32(196, 30, 58, 255);
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(2);

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
