using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankProject
{
    internal class Enumeration
    {
        internal enum Difficulty
        {
            Low = 0,
            Medium = 1,
            High = 2
        }

        internal enum QuestionType
        {
            Classic = 0,
            GapFilling = 1,
            MultipleChoice = 2,
            TrueFalse = 3,
        }
    }
}