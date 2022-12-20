using QuestionBankProject.Interfaces.DesignPatterns;
using System.Collections.Generic;
using System;
using static QuestionBankProject.Enumeration;
using System.Linq;

namespace QuestionBankProject.Concrete.DesignPatterns
{
    internal class GenerateHighQuestion : GenerateQuestionDifficultyStrategy
    {
        public override List<IQuestion> Generate(List<IQuestion> questionList, QuestionGenerater creater)
        {
            var questions = new List<IQuestion>();
            foreach (var question in questionList.Where(x => x.Difficulty != Enumeration.Difficulty.Low.ToString()))
            {
                questions.Add(creater.FactoryMethod((QuestionType)Enum.Parse(typeof(QuestionType), question.QuestionType)));
            }
            return questions;
        }
    }
}