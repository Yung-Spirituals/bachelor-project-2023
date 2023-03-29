using Test_Scripts;
using UnityEngine;

public class QuestionGenerate : MonoBehaviour
{
    public static int actualAnswer;
    private static bool displayingQuestion;
    private static int questionNumber;
    
    private void Update()
    {
        if (displayingQuestion) return;
        displayingQuestion = true;
        
        if (questionNumber == 0)
        {
            QuestionDisplay.SetNewQuestion(new Question(
                "What are the primary four states of a banana?",
                "A. Green banana, yellow banana, spotted banana, brown banana",
                "B. Green banana",
                "C. Yellow banana",
                "D. Brown banana",
                0));
            actualAnswer = 0;
        }
            
        if (questionNumber == 1)
        {
            QuestionDisplay.SetNewQuestion(new Question(
                "Best banana?",
                "A. Green banana",
                "B. Good banana",
                "C. Bad banana",
                "D. Brown banana",
                1));
            actualAnswer = 1;
        }
            
        if (questionNumber == 2)
        {
            QuestionDisplay.SetNewQuestion(new Question(
                "Big boo??",
                "A. Smol boo",
                "B. Green boo",
                "C. King boo",
                "D. Wizard hat boo",
                2));
            actualAnswer = 2;
        }
            
        if (questionNumber == 3)
        {
            QuestionDisplay.SetNewQuestion(new Question(
                "Is daddy old?",
                "A. Young",
                "B. Old",
                "C. Oldest",
                "D. Youngest",
                3));
            actualAnswer = 3;
        }

        // All questions go above this line
        QuestionDisplay.SetUpdateQuestion(false);
    }
    public static void NextQuestion()
    {
        questionNumber++;
        displayingQuestion = false;
    }
}
