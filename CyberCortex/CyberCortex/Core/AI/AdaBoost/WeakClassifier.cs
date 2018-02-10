using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCortex.Core.AI.AdaBoost
{
    public class WeakClassifier
    {
        private double[] _classifier = new double[3];
        private double[] _data;
        private double[] _answer;
        private int _length;
        private int _size;

        public WeakClassifier(int size, int length)
        {
            this._data = new double[size];
            this._answer = new double[size];
            this._length = length;
            this._size = size;
        }

        public double[] Train(double[][] samples, double[] answers)
        {
            double threshold = 0;
            double direction = 0;
            double minimalError = Double.PositiveInfinity;
            double[] error = new double[_length];

            for (int j = 0; j < _length; j++)
            {
                for (int i = 0; i < _size; i++)
                {
                    _data[i] = samples[i][j];
                    _answer[i] = answers[i];
                }

                Array.Sort(_data, _answer);

                for (int i = 0; i < _size - 1; i++)
                {
                    if ((_answer[i] != _answer[i + 1]) && (_data[i] != _data[i + 1]))
                    {
                        threshold = (_data[i] + _data[i + 1]) / 2.0;

                        if (_answer[i] >= 0)
                        {
                            direction = 1;
                        }
                        else
                        {
                            direction = -1;
                        }

                        error[j] = 0;

                        for (int k = 0; k < _size; k++)
                        {
                            error[j] += Math.Abs(_answer[k] - GetPredict(_data[k], threshold, direction)) / 2.0;
                        }

                        if (error[j] < minimalError)
                        {
                            minimalError = error[j];
                            _classifier[0] = j;
                            _classifier[1] = threshold;
                            _classifier[2] = direction;
                        }
                    }
                }
            }

            return _classifier;
        }

        public double GetPredict(double value, double treshold, double direction)
        {
            if (direction > 0)
            {
                if (value <= treshold)
                {
                    return 1;
                }

                return -1;
            }
            else
            {
                if (value <= treshold)
                {
                    return -1;
                }
 
                return 1;
            }
        }
    }
}
