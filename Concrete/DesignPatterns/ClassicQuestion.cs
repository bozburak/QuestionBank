﻿using QuestionBankProject.Interfaces.DesignPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankProject.Concrete.DesignPatterns
{
    internal class ClassicQuestion : IQuestion
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
            QuestionType = Enumeration.QuestionType.Classic.ToString();
            return this;
        }
    }
}