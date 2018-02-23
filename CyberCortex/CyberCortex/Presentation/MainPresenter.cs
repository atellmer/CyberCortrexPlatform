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
            /*
            double[][] patterns = new double[3][];

            patterns[0] = new double[3] { 80, 180, 6 };
            patterns[1] = new double[3] { 50, 160, 500 };
            patterns[2] = new double[3] { 90, 190, 20 };


            ProbabilisticSelector selector = new ProbabilisticSelector(3, 3);

            //selector.Run();*/


            Sample[] samples = new Sample[] {
              new Sample(new double[] { 80, 180, 6, 1 }, 1),
              new Sample(new double[] { 50, 160, 500, 2 }, -1),
              new Sample(new double[] { 90, 190, 20, 1 }, 1),
              new Sample(new double[] { 80, 180, 7, 2 }, -1)
            };

            WeakClassifier weakClassifier = WeakClassifier.Train(samples);



            Debug.WriteLine($"App is running: {weakClassifier}");
        }
    }
}
