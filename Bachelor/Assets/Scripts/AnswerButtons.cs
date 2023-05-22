using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtons : MonoBehaviour
{
    public GameObject[] answers;

    // Submit an option as an answer to the question manager.
    public void Answer(int option)
    {
        // Ensure that that the buttons don't do anything whilst the game is paused.
        if (PauseManager.Instance.isPaused) return;
        
        // Provides feedback to the user through visual elements like color and symbols.
        ColorButtons();
        
        // Disable the buttons until the next question is displayed, and reset them for the next question.
        DisableButtonsAndDisplayNextQuestion();
        
        // Notify the question manager of what option the player selected.
        QuestionManager.Instance.Answer(option, true);
    }
    
    // Colors the buttons if they are for the wrong option, also displays symbols to convey if the option is correct.
    private void ColorButtons()
    {
        Question question = QuestionManager.Instance.CurrentQuestion;
        
        // For each answer button, decides what to do with them depending on if it holds a right or wrong answer.
        bool[] isCorrect = { question.IsOption0, question.IsOption1,
            question.IsOption2, question.IsOption3 };
        for (int i = 0; i < answers.Length; i++)
        {
            // Sets the icon to its active state, and makes it visible
            if (isCorrect[i])
            {
                answers[i].GetComponent<EnableDisableIcons>().SetActive(true);
                answers[i].GetComponent<EnableDisableIcons>().SetGameObjectActive(true);
            }
            
            // Colors the buttons containing the wrong answers orange. Sets its disable state, and makes it visible.
            else
            {
                answers[i].GetComponent<Image>().color = new Color32(244,140,81,255);
                answers[i].GetComponent<Shadow>().effectColor =new Color32(216,108,48,255);
                answers[i].GetComponent<EnableDisableIcons>().SetActive(false);
                answers[i].GetComponent<EnableDisableIcons>().SetGameObjectActive(true);
            }
        }
    }
    
    

    // Waits one second before resetting the color of the buttons, hides icons, and re-enables the answer buttons.
    private IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(1f);

        foreach (GameObject answer in answers)
        {
            answer.GetComponent<Image>().color = new Color32(77,161,223,255);
            answer.GetComponent<Shadow>().effectColor = new Color32(32,112,172,255);
            answer.GetComponent<Button>().enabled = true;
            answer.GetComponent<EnableDisableIcons>().SetGameObjectActive(false);
        }
    }

    // Disables answer buttons, and calls for the buttons to be prepared for the next question.
    private void DisableButtonsAndDisplayNextQuestion()
    {
        foreach (GameObject answer in answers) answer.GetComponent<Button>().enabled = false;
        StartCoroutine(NextQuestion());
    }
}
