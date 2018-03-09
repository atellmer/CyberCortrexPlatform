using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CyberCortex.Core.Utils;
using CyberCortex.Core.AI;
using CyberCortex.Core.AI.AdaBoost;

namespace CyberCortex.Presentation
{
    class MainPresenter
    {
        private readonly MainPage _mainPage;
        public MainPresenter(MainPage mainPage)
        {
            this._mainPage = mainPage;
        }
        public void Run()
        {
            Sample[] samples = new Sample[] {
              new Sample(new double[] { 80, 180, 6, 1 }, 1),
              new Sample(new double[] { 50, 160, 500, 2 }, -1),
              new Sample(new double[] { 90, 190, 20, 1 }, 1),
              new Sample(new double[] { 80, 180, 100, 2 }, -1),
              new Sample(new double[] { 55, 170, 5, 2 }, -1)
            };

            Sample[] normalizedSamples = DataNormalizer.NormalizeSamples(samples);

            Sample[] testSamples = new Sample[] {
              new Sample(new double[] { 70, 170, 4, 1 }, 1),
              new Sample(new double[] { 95, 182, 24, 1 }, 1),
              new Sample(new double[] { 130, 170, 250, 2 }, -1),
              new Sample(new double[] { 65, 185, 600, 2 }, -1),
              new Sample(new double[] { 60, 185, 0, 2 }, -1)
            };

            Sample[] normalizedTestSamples = DataNormalizer.NormalizeSamples(testSamples);

            //WeakClassifier weakClassifier = WeakClassifier.Train(samples);

            //Random random = new Random();

            //Sample[] samplesSelected = ProbabilisticSelector.Select(random, new double[] { 0.18, 0.8, 0.01, 0.01 }, samples);

            StrongClassifier[] classifiers = StrongClassifier.Train(normalizedSamples, 10, "1");

            int predict1 = (int)StrongClassifier.GetPredict(normalizedTestSamples[0].GetPattern(), classifiers);
            int predict2 = (int)StrongClassifier.GetPredict(normalizedTestSamples[1].GetPattern(), classifiers);
            int predict3 = (int)StrongClassifier.GetPredict(normalizedTestSamples[2].GetPattern(), classifiers);
            int predict4 = (int)StrongClassifier.GetPredict(normalizedTestSamples[3].GetPattern(), classifiers);
            int predict5 = (int)StrongClassifier.GetPredict(normalizedTestSamples[4].GetPattern(), classifiers);


            Debug.WriteLine($"App is running: {classifiers[0]}");
        }
    }
}
