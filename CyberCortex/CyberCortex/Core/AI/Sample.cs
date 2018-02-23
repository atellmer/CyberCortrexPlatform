using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCortex.Core.AI
{
    public struct Sample
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

        public static Sample Normalize(Sample sample)
        {
            double[] pattern = sample.GetPattern();
            double[] patternNormalized = new double[pattern.Length];
            int count = pattern.Length;
            double average = 0.0;
            double stdDev = 0.0;

            average = pattern.Average();

            foreach (double feature in pattern)
            {
                stdDev += Math.Pow((feature - average), 2);
            }

            stdDev = Math.Sqrt(stdDev / (count - 1));

            for (int i = 0; i < pattern.Length; i++)
            {
                patternNormalized[i] = (pattern[i] - average) / stdDev;
                patternNormalized[i] = (Math.Exp(patternNormalized[i]) - Math.Exp(-1 * patternNormalized[i])) / (Math.Exp(patternNormalized[i]) + Math.Exp(-1 * patternNormalized[i]));
            }

            return new Sample(patternNormalized, sample.GetAnswer());
        }
    }      
}
