using System;
using System.Collections;
using Test_Scripts;
using UnityEngine;

public class QuestionDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI screenQuestion;
    public TMPro.TextMeshProUGUI[] answers;
    
    private static Question newQuestion;
    private static bool updateQuestion;
    
    // Update is called once per frame
    private void Update()
    {
        if (updateQuestion) return;
        updateQuestion = true;
        StartCoroutine(PushTextOnScreen());
    }

    IEnumerator PushTextOnScreen()
    {
        yield return new WaitForSeconds(0.25f);
        screenQuestion.text = newQuestion.GetQuestion();
        String[] newAnswers = newQuestion.GetOptions();
            
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].text = newAnswers[i];
        }
    }

    public static void SetNewQuestion(Question question)
    {
        newQuestion = question;
    }
    
    public static void SetUpdateQuestion(bool value)
    {
        updateQuestion = value;
    }
}