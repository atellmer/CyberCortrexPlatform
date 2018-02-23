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

            double[] pattern = new double[] { 0, 1, 2, 3 };
            int answer = 1;
            Sample testSample = new Sample(pattern, answer);

            


           Debug.WriteLine($"App is running: {testSample}");
        }
    }
}
