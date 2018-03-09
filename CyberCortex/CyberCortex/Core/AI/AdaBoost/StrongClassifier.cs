using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCortex.Core.AI.AdaBoost
{
    public struct StrongClassifier
    {
        public enum Answer { Positive = 1, Negative = -1 };

        private WeakClassifier _weakClassifier;
        private double _alfa;

        public WeakClassifier GetWeakClassifier()
        {
            return _weakClassifier;
        }

        public void SetWeakClassifier(WeakClassifier weakClassifier)
        {
            _weakClassifier = weakClassifier;
        }

        public double GetAlfa()
        {
            return _alfa;
        }

        public void SetAlfa(double alfa)
        {
            _alfa = alfa;
        }

        public override string ToString()
        {
            return $"Strong classifier: [weak: {_weakClassifier}, alfa: {_alfa}]";
        }

        public static StrongClassifier[] Train(Sample[] samples, int classifiersCount, string className)
        {
            int size = samples.Length;
            int length = samples[0].GetPattern().Length;
            double[] weights = new double[size];
            double [] reports = new double[size];
            double weightsSum = 0;
            double epsilon = 0;
            double alfa = 0;
            Random random = new Random();
            StrongClassifier[] classifiers = new StrongClassifier[classifiersCount];
            Sample[] transformedSamples = (Sample[])samples.Clone();

            for (int i = 0; i < size; i++)
            {
                weights[i] = 1 / Convert.ToDouble(size);
            }

            for (int k = 0; k < classifiersCount; k++)
            {
                WeakClassifier weakClassifier = WeakClassifier.Train(transformedSamples);
                StrongClassifier strongClassifier = new StrongClassifier();
                epsilon = 0;
                weightsSum = 0;
                alfa = 0;

                for (int i = 0; i < size; i++)
                {
                    reports[i] = (int)WeakClassifier.GetPredict(samples[i].GetPattern()[weakClassifier.GetFeatureIndex()], weakClassifier.GetThreshold(), weakClassifier.GetDirection());

                    if (samples[i].GetAnswer() != reports[i])
                    {
                        epsilon += weights[i];
                    }
                }

                if (epsilon == 0)
                {
                    epsilon = 0.000000000000001;
                }

                if (epsilon == 1)
                {
                    epsilon = 0.999999999999999;
                }

                alfa = 0.5 * Math.Log((1 - epsilon) / epsilon);

                strongClassifier.SetWeakClassifier(weakClassifier);
                strongClassifier.SetAlfa(alfa);
                classifiers[k] = strongClassifier;

                for (int i = 0; i < size; i++)
                {
                    weightsSum += weights[i] * Math.Exp(-1 * alfa * samples[i].GetAnswer() * reports[i]);
                }

                for (int i = 0; i < size; i++)
                {
                    weights[i] = (weights[i] * Math.Exp(-1 * alfa * samples[i].GetAnswer() * reports[i])) / weightsSum;
                }

                transformedSamples = ProbabilisticSelector.Select(random, weights, transformedSamples);
            }

            return classifiers;
        }

        public static Answer GetPredict(double[] pattern, StrongClassifier[] classifiers)
        {
            WeakClassifier weakClassifier;
            double answer = 0;

            for (int k = 0; k < classifiers.Length; k++)
            {
                weakClassifier = classifiers[k].GetWeakClassifier();

                answer += classifiers[k].GetAlfa() * (int)WeakClassifier.GetPredict(pattern[weakClassifier.GetFeatureIndex()], weakClassifier.GetThreshold(), weakClassifier.GetDirection()); ;
            }

            if (answer > 0)
            {
                return Answer.Positive;
            }

            return Answer.Negative;
        }
    }
}
