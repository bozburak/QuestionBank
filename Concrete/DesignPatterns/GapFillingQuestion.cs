using QuestionBankProject.Interfaces.DesignPatterns;
using System.Collections.Generic;

namespace QuestionBankProject.Concrete.DesignPatterns
{
    internal class GapFillingQuestion : IQuestion
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }// List for multi gap filling
        public List<string> CorrectAnswers { get; set; }// List for multi gap filling and Multiple Choice
        public int Point { get; set; }
        public string Difficulty { get; set; }
        public string QuestionType { get; set; }

        public IQuestion Generate(string questionText, List<string> answers, List<string> correctAnswers, int point, string difficulty)
        {
            Id = QuestionBank.Instance.GetSequence();
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswers = correctAnswers;
            Point = point;
            Difficulty = difficulty;
            QuestionType = Enumeration.QuestionType.GapFilling.ToString();
            return this;
        }
    }
}