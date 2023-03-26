using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerate : MonoBehaviour
{
    public static string actualAnswer;
    public static bool displayingQuestion = false;
    public int questionNumber;
    
    void Update()
    {
        if (displayingQuestion == false)
        {
            displayingQuestion = true;
            questionNumber = Random.Range(1, 5);
            if (questionNumber == 1)
            {
                QuestionDisplay.newQuestion = "What are the primary four states of a banana?";
                QuestionDisplay.newA = "A. Green banana, yellow banana, spotted banana, brown banana";
                QuestionDisplay.newB = "B. Green banana";
                QuestionDisplay.newC = "C. Yellow banana";
                QuestionDisplay.newD = "D. Brown banana";
                actualAnswer = "A";
            }
            
            if (questionNumber == 2)
            {
                QuestionDisplay.newQuestion = "Best banana";
                QuestionDisplay.newA = "A. Green banana";
                QuestionDisplay.newB = "B. Good banana";
                QuestionDisplay.newC = "C. Yellow";
                QuestionDisplay.newD = "D. Banana";
                actualAnswer = "B";
            }
            
            if (questionNumber == 3)
            {
                QuestionDisplay.newQuestion = "Big boo?";
                QuestionDisplay.newA = "A. Smol boo";
                QuestionDisplay.newB = "B. Green boo";
                QuestionDisplay.newC = "C. King boo";
                QuestionDisplay.newD = "D. Wizard hat boo";
                actualAnswer = "C";
            }
            
            if (questionNumber == 4)
            {
                QuestionDisplay.newQuestion = "Is daddy old?";
                QuestionDisplay.newA = "A. Young";
                QuestionDisplay.newB = "B. Abit old";
                QuestionDisplay.newC = "C. Kinda old";
                QuestionDisplay.newD = "D. Oldest";
                actualAnswer = "D";
            }
            
            // All questions go above this line
            QuestionDisplay.updateQuestion = false;
        }
    }
}
