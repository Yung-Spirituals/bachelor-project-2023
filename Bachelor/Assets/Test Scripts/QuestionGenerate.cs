using Test_Scripts;
using UnityEngine;

public class QuestionGenerate : MonoBehaviour
{
    public static int actualAnswer;
    private static bool displayingQuestion;
    private static int questionNumber;
    public bool isTwoOptions = false;
    
    private void Update()
    {
        if (displayingQuestion) return;
        displayingQuestion = true;
        
        if (questionNumber == 0)
        {
            if(isTwoOptions)
            {
                QuestionDisplay.SetNewQuestion(new Question(
                    "Is Musa sapientum the scientific name for banana?",
                    "Yes",
                    "No",
                    0));
                actualAnswer = 0;
            }
            else
            {
                QuestionDisplay.SetNewQuestion(new Question(
                    "What is a cluster of bananas called?",
                    "A. A cluster",
                    "B. A hand",
                    "C. A boat",
                    "D. A bunch",
                    new[]{0}));
                actualAnswer = 0;
            }
        }
            
        if (questionNumber == 1)
        {
            if(isTwoOptions)
            {
                QuestionDisplay.SetNewQuestion(new Question(
                    "Can bananas be eaten when green?",
                    "Yes, but then don't taste as good",
                    "N o",
                    0));
                actualAnswer = 0;
            }
            else
            {
                QuestionDisplay.SetNewQuestion(new Question(
                    "What percentage of a banana is water?",
                    "A. 50%",
                    "B. 75%",
                    "C. 88%",
                    "D. 99%",
                    new[]{1}));
                actualAnswer = 1;
            }
        }
            
        if (questionNumber == 2)
        {
            if(isTwoOptions)
            {
                QuestionDisplay.SetNewQuestion(new Question(
                    "Bananas are not radioactive",
                    "Yes, they aren't",
                    "No, they are",
                    1));
                actualAnswer = 1;
            }
            else
            {
                QuestionDisplay.SetNewQuestion(new Question(
                    "Which country produces the most banana?",
                    "A. Brazil",
                    "B. Mexico",
                    "C. India",
                    "D. China",
                    new[] { 2 }));
                actualAnswer = 2;
            }
        }
            
        if (questionNumber == 3)
        {
            if (isTwoOptions)
            {
                QuestionDisplay.SetNewQuestion(new Question(
                    "Do bananas contain sodium?",
                    "Yes",
                    "No",
                    1));
                actualAnswer = 1;
            }
            else
            {
                QuestionDisplay.SetNewQuestion(new Question(
                    "How many varieties of bananas exist?",
                    "A. Around 100",
                    "B. Around 500",
                    "C. Around 2000",
                    "D. Around 1000",
                    new[] { 3 }));
                actualAnswer = 3;
            }
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
