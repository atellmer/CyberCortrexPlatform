using System;
using System.Linq;

namespace CyberCortex.Core.AI.AdaBoost
{
    public class ProbabilisticSelector
    {
        public static Sample[] Select(Random random, double[] weightsInit, Sample[] samples)
        {
            int size = samples.Length;
            double[] weights = new double[size];
            double[] wheel = new double[size];
            int[] weightsIndexes = new int[size];
            int count = 0;
            int positiveAnswersCount = 0;
            int negativeAnswersCount = 0;
            int positiveAnswersSelectedCount = 0;
            int negativeAnswersSelectedCount = 0;
            Sample[] samplesSelected = new Sample[size];

            weights = (double[])weightsInit.Clone();

            for (int i = 0; i < size; i++)
            {
                weightsIndexes[i] = i;

                if (samples[i].GetAnswer() == 1)
                {
                    positiveAnswersCount++;
                }

                if (samples[i].GetAnswer() == -1)
                {
                    negativeAnswersCount++;
                }
            }

            Array.Sort(weights, weightsIndexes);

            wheel = GenerateProbabilisticWhell(size, weights);

            while (count < wheel.Length)
            {
                double randomValue = GetRandomValue(random, wheel.Min());

                for (int i = 0; i < wheel.Length - 1; i++)
                {
                    if (randomValue > wheel[i] && randomValue <= wheel[i + 1])
                    {
                        if ((samples[weightsIndexes[i + 1]].GetAnswer() == 1) && (positiveAnswersSelectedCount < positiveAnswersCount))
                        {
                            samplesSelected[count] = samples[weightsIndexes[i + 1]];
                            positiveAnswersSelectedCount++;
                            count++;
                        }

                        if ((samples[weightsIndexes[i + 1]].GetAnswer() == -1) && (negativeAnswersSelectedCount < negativeAnswersCount))
                        {
                            samplesSelected[count] = samples[weightsIndexes[i + 1]];
                            negativeAnswersSelectedCount++;
                            count++;
                        }
                    }
                }
            }

            return samplesSelected;
        }

        private static double[] GenerateProbabilisticWhell(int sectorsCount, double[] weights)
        {
            double sector = 0;
            int index = 0;
            double[] wheel = new double[sectorsCount];

            for (int i = 3; i < sectorsCount + 3; i++)
            {
                sector = 0;

                for (int j = 0; j < i - 2; j++)
                {
                    sector += weights[j];
                }

                wheel[index] = sector * 100;
                index++;
            }

            return wheel;
        }

        private static double GetRandomValue(Random random, double minSectorSize)
        {
            if (minSectorSize >= 1)
            {
                return random.Next(100);
            }

            if (minSectorSize < 1 && minSectorSize >= 0.1)
            {
                return random.Next(1000) * 0.1;
            }

            if (minSectorSize < 0.1 && minSectorSize >= 0.01)
            {
                return random.Next(10000) * 0.01;
            }

            if (minSectorSize < 0.01 && minSectorSize >= 0.001)
            {
                return random.Next(100000) * 0.001;
            }

            if (minSectorSize < 0.001 && minSectorSize >= 0.0001)
            {
                return random.Next(1000000) * 0.0001;
            }

            if (minSectorSize < 0.0001 && minSectorSize >= 0.00001)
            {
                return random.Next(10000000) * 0.00001;
            }

            if (minSectorSize < 0.00001 && minSectorSize >= 0.000001)
            {
                return random.Next(100000000) * 0.000001;
            }

            if (minSectorSize < 0.000001)
            {
                return random.Next(1000000000) * 0.0000001;
            }

            return 0.0;
        }
    }
}
