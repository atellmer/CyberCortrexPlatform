using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCortex.Core.Utils
{
    public static class DataNormalizer
    {
        public static double[][] NormalizePatterns(double[][] patterns)
        {
            double[][] patternsNormalized = new double[patterns.Length][];

            for (int i = 0; i < patterns.Length; i++)
            {
                patternsNormalized[i] = NormalizePattern(patterns[i]);
            }


            return patternsNormalized;
        }

        public static double[] NormalizePattern(double[] pattern)
        {
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

            return patternNormalized;
        }
    }
}
