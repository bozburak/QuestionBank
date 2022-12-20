using QuestionBankProject.Interfaces.DesignPatterns;
using System;
using System.Collections.Generic;

namespace QuestionBankProject.Concrete.DesignPatterns
{
    internal class QuestionBank : IQuestionBank
    {
        private int Sequence { get; set; }

        private List<IQuestion> questionList = new List<IQuestion>();
        private GenerateQuestionDifficultyStrategy generateStrategy;

        public void SetGenerateStrategy(GenerateQuestionDifficultyStrategy generateStrategy)
        {
            this.generateStrategy = generateStrategy;
        }

        public void Add(IQuestion question)
        {
            questionList.Add(question);
        }

        // strategy and factory design pattern
        public List<IQuestion> Generate()
        {
            QuestionGenerater creater = new QuestionGenerater();
            return generateStrategy.Generate(questionList, creater);
        }

        private QuestionBank()
        { }

        private static readonly object isLock = new object();
        private static QuestionBank instance = null;

        // singleton
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