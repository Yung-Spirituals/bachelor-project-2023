using System;

namespace Test_Scripts
{
    public class Question
    {
        private String question;
        private String[] options;
        private int correctOption;

        public Question(String question, String option0, String option1, String option2, String option3, int correctOption)
        {
            this.question = question;
            options = new[] { option0, option1, option2, option3 };
            this.correctOption = correctOption;
        }

        public Question(String question, String option0, String option1, int correctOption)
        {
            this.question = question;
            options = new[] { option0, option1 };
            this.correctOption = correctOption;
        }   

        public String GetQuestion()
        {
            return question;
        }

        public String[] GetOptions()
        {
            return options;
        }
        
        // Make into an array to allow for multiple correct options?
        public int GetCorrectOption()
        {
            return correctOption;
        }
    }
}