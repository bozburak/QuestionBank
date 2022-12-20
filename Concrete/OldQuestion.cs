using System;
using System.Collections.Generic;

namespace QuestionBankProject.Interfaces
{
    internal class OldQuestion : IOldQuestion, IComparable
    {
        private static int sequence = 1;
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public List<string> CorrectAnswers { get; set; }
        public int Point { get; set; }
        public string Difficulty { get; set; }
        public string QuestionType { get; set; }

        // (polymorphism) overload example with ctor
        public OldQuestion()
        {
        }

        public OldQuestion(string questionText, List<string> answers, List<string> correctAnswers, int point, string difficulty, string questionType)
        {
            Id = sequence;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswers = correctAnswers;
            Point = point;
            Difficulty = difficulty;
            QuestionType = questionType;
            sequence = sequence + 1;
        }

        public int CompareTo(object obj)
        {
            OldQuestion question = obj as OldQuestion;
            int result = 0;
            if (question != null)
            {
                if (this.Point > question.Point)
                    result = 1;
                else if (this.Point == question.Point)
                    result = 0;
                else
                    result = -1;
            }
            return result;
        }
    }
}