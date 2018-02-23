using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCortex.Core.AI.AdaBoost
{
    public class ProbabilisticSelector
    {
        private int _size;
        private int _length;
        private Random random = new Random();

        public ProbabilisticSelector(int size, int length)
        {
            this._size = size;
            this._length = length;
        }

        public void Run(double[] weightsData, double[][] data, double[] answer, double[][] samples, double[] answers)
        {
            double[] weights = new double[_size];
            double[] wheel = new double[_size];
            int[] masIndexWeights = new int[_size];
            int count = 0;
            double randomValue = 0;
            double sector = 0;
            int indexSector = 0;
            int positiveRead = 0;
            int negativeRead = 0;
            int positiveWrite = 0;
            int negativeWrite = 0;
            double minWhellPart = 0;

            for (int i = 0; i < _size; i++)
            {
                if (answers[i] == 1)
                {
                    positiveRead++;
                }
                if (answers[i] == -1)
                {
                    negativeRead++;
                }
            }

            for (int i = 0; i < _size; i++)
            {
                weights[i] = weightsData[i];
                masIndexWeights[i] = i;
            }

            Array.Sort(weights, masIndexWeights);
            indexSector = 0;

            for (int i = 3; i < _size + 3; i++)
            {
                sector = 0;
                for (int j = 0; j < i - 2; j++)
                {
                    sector += weights[j];
                }
                wheel[indexSector] = sector * 100;
                indexSector++;
            }

            int shift = 1;
            while (count < _size)
            {
                minWhellPart = wheel.Min();
                int index = 0;
                bool flag = false;

                if (minWhellPart >= 1)
                {
                    randomValue = random.Next(100);
                }
                if (minWhellPart < 1 && minWhellPart >= 0.1)
                {
                    randomValue = random.Next(1000) * 0.1;
                }
                if (minWhellPart < 0.1 && minWhellPart >= 0.01)
                {
                    randomValue = random.Next(10000) * 0.01;
                }
                if (minWhellPart < 0.01 && minWhellPart >= 0.001)
                {
                    randomValue = random.Next(100000) * 0.001;
                }
                if (minWhellPart < 0.001 && minWhellPart >= 0.0001)
                {
                    randomValue = random.Next(1000000) * 0.0001;
                }
                if (minWhellPart < 0.0001 && minWhellPart >= 0.00001)
                {
                    randomValue = random.Next(10000000) * 0.00001;
                }
                if (minWhellPart < 0.00001 && minWhellPart >= 0.000001)
                {
                    randomValue = random.Next(100000000) * 0.000001;
                }
                if (minWhellPart < 0.000001)
                {
                    randomValue = random.Next(1000000000) * 0.0000001;
                }

                for (int i = 0; i < _size - 1; i++)
                {
                    if (wheel[i] > randomValue)
                    {
                        if (i == 0)
                        {
                            index = masIndexWeights[i];
                        }

                        if (i >= shift)
                        {
                            if (randomValue > wheel[i - shift])
                            {
                                index = masIndexWeights[i];
                                flag = true;
                            }
                        }

                        if (flag == true)
                        {
                            if ((answers[index] == 1) && (positiveWrite < positiveRead))
                            {
                                for (int j = 0; j < _length; j++)
                                {
                                    data[count][j] = samples[index][j];
                                }
                                answer[count] = answers[index];
                                positiveWrite++;
                                count++;
                            }

                            if ((answers[index] == -1) && (negativeWrite < negativeRead))
                            {
                                for (int j = 0; j < _length; j++)
                                {
                                    data[count][j] = samples[index][j];
                                }
                                answer[count] = answers[index];
                                negativeWrite++;
                                count++;
                            }
                        }
                        else
                        {
                            shift++;
                            if (shift >= _size)
                            {
                                shift = 1;
                            }
                        }
                    }
                }
            }
        }
    }
}
