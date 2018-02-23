using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCortex.Core.AI
{
    struct Sample
    {
        private double[] _pattern;
        private int _answer;

        public Sample(double[] pattern, int answer)
        {
            this._pattern = (double[])pattern.Clone();
            this._answer = answer;
        }

        public double[] GetPattern()
        {
            return _pattern;
        }

        public int GetAnswer()
        {
            return _answer;
        }

        public override string ToString()
        {
            return $"Pattern: [{string.Join("; ", _pattern)}], answer: {_answer}";
        }
    }
}
