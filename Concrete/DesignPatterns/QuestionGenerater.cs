using QuestionBankProject.Interfaces.DesignPatterns;

namespace QuestionBankProject.Concrete.DesignPatterns
{
    internal class QuestionGenerater
    {
        public IQuestion FactoryMethod(Enumeration.QuestionType questionType)
        {
            IQuestion question = null;
            switch (questionType)
            {
                case Enumeration.QuestionType.TrueFalse:
                    question = new TrueFalseQuestion();
                    break;

                case Enumeration.QuestionType.Classic:
                    question = new ClassicQuestion();
                    break;

                case Enumeration.QuestionType.GapFilling:
                    question = new GapFillingQuestion();
                    break;

                case Enumeration.QuestionType.MultipleChoice:
                    question = new MultipleChoiceQuestion();
                    break;
            }
            return question;
        }
    }
}