using System;

namespace Test_Scripts
{
    public class Question
    {
        private String question;
        private String[] options;
        private int[] correctOptions;
        private int correctOption;
        private int correctCategory;

        public Question(String question,
            String option0, String option1, String option2, String option3, int[] correctOptions)
        {
            this.question = question;
            options = new[] { option0, option1, option2, option3 };
            this.correctOptions = correctOptions;
        }

        public Question(String question, String option0, String option1, int correctOption)
        {
            this.question = question;
            options = new[] { option0, option1 };
            this.correctOption = correctOption;
        }

        public Question(String question, int correctCategory)
        {
            this.question = question;
            this.correctCategory = correctCategory;
        }

        public String GetQuestion()
        {
            return question;
        }

        public String[] GetOptions()
        {
            return options;
        }

        public int[] GetCorrectOptions()
        {
            return correctOptions;
        }
        
        public int GetCorrectOption()
        {
            return correctOption;
        }

        public int GetCorrectCategory()
        {
            return correctCategory;
        }
    }
}