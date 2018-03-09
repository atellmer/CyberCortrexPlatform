using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCortex.Core.AI.AdaBoost
{
    public struct WeakClassifier
    {
        public enum Direction { Up = 1, Down = -1 };
        public enum Answer { Positive = 1, Negative = -1 };
 
        private int _featureIndex;
        private double _threshold;
        private Direction _direction;

        public int GetFeatureIndex()
        {
            return _featureIndex;
        }

        public void SetFeatureIndex(int featureIndex)
        {
            _featureIndex = featureIndex;
        }

        public double GetThreshold()
        {
            return _threshold;
        }

        public void SetThreshold(double threshold)
        {
           _threshold = threshold;
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
        }

        public override string ToString()
        {
            return $"feature index: {_featureIndex}, threshold: {_threshold}, direction: {(int)_direction}";
        }

        public static WeakClassifier Train(Sample[] samples)
        {
            int size = samples.Length;
            int length = samples[0].GetPattern().Length;
            double[] data = new double[size];
            int[] answers = new int[size];   
            double threshold = 0;
            Direction? direction = null;
            double minimalError = Double.PositiveInfinity;
            double[] error = new double[length];
            WeakClassifier weakClassifier = new WeakClassifier();

            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    data[i] = samples[i].GetPattern()[j];
                    answers[i] = samples[i].GetAnswer();
                }

                Array.Sort(data, answers);

                for (int i = 0; i < size - 1; i++)
                {
                    if ((answers[i] != answers[i + 1]) && (data[i] != data[i + 1]))
                    {
                        threshold = (data[i] + data[i + 1]) / 2.0;

                        if (answers[i] >= 0)
                        {
                            direction = Direction.Up;
                        }
                        else
                        {
                            direction = Direction.Down;
                        }

                        error[j] = 0;

                        for (int k = 0; k < size; k++)
                        {
                            error[j] += Math.Abs(answers[k] - (int)GetPredict(data[k], threshold, (Direction)direction)) / 2.0;
                        }

                        if (error[j] < minimalError)
                        {
                            minimalError = error[j];
                            weakClassifier.SetFeatureIndex(j);
                            weakClassifier.SetThreshold(threshold);
                            weakClassifier.SetDirection((Direction)direction);

                        }
                    }
                }
            }

            return weakClassifier;
        }

        public static Answer GetPredict(double value, double treshold, Direction direction)
        {
            if (direction > 0)
            {
                if (value <= treshold)
                {
                    return Answer.Positive;
                }

                return Answer.Negative;
            }
            else
            {
                if (value <= treshold)
                {
                    return Answer.Negative;
                }

                return Answer.Positive;
            }
        }
    }
}
