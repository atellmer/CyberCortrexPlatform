using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CyberCortex.Core.Utils;
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
           Debug.WriteLine($"App is running");
        }
    }
}
