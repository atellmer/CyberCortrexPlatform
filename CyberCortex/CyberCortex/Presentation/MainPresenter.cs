using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CyberCortex.Core.Utils;

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
            double[][] patterns = new double[4][];
            
            for (int i = 0; i < patterns.Length; i++)
            {
                patterns[i] = new double[] { 1, 2, 3, 4, 10 };
            }

            patterns = DataNormalizer.NormalizePatterns(patterns);

            //Debug.WriteLine($"{DataNormalizer.NormalizePatterns(patterns)}");
        }
    }
}
