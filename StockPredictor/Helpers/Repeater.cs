using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace StockPredictor.Helpers
{
    class Repeater
    {
        private static System.Timers.Timer aTimer;
        

        public void RunRepeater()
        {
            Form1.Instance.repeatData.RepeaterIsRunning = true;
            ExecuteScan();
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

             aTimer.Interval = 1000*60*30;
            aTimer.Enabled = true;
        }      

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
           
            Form1.Instance.repeatData.RepeaterIsRunning = true;
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
          
        }
        //execute the scans
        private void ExecuteScan()
        {
            RunMethods rm = new RunMethods();
            rm.runStockPredictor("aapl", false);
        }

      
    }
}
