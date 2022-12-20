using QuestionBankProject.Interfaces.DesignPatterns;

namespace QuestionBankProject.Concrete.DesignPatterns
{
    internal class QuestionBank : IQuestionBank
    {
        private int Sequence { get; set; }

        private QuestionBank()
        { }

        private static readonly object isLock = new object();
        private static QuestionBank instance = null;

        public static QuestionBank Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (isLock)
                    {
                        if (instance == null)
                        {
                            instance = new QuestionBank();
                        }
                    }
                }
                return instance;
            }
        }

        private void Increase() => Sequence = Sequence + 1;

        public int GetSequence()
        {
            Increase();
            return Sequence;
        }
    }
}