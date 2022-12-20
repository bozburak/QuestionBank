using QuestionBankProject.Concrete.DesignPatterns;
using System.Collections.Generic;

namespace QuestionBankProject.Interfaces.DesignPatterns
{
    internal abstract class GenerateQuestionDifficultyStrategy
    {
        public abstract List<IQuestion> Generate(List<IQuestion> questionList, QuestionGenerater creater);
    }
}