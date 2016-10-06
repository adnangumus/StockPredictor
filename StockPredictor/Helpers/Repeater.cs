using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using StockPredictor.Models;

namespace StockPredictor.Helpers
{
    class Repeater
    {
        private static System.Timers.Timer aTimer;
        private static string input;
        private static bool isShort;
        private static bool isStrong;
        private static bool noTrading;
        private static int ExecutionTimes;

        public void RunRepeater(string str)
        {
            //the number of times an execution takes place
            ExecutionTimes = 0;
            //get the input as an argument and store it   
            input = str.ToUpper();             
            Form1.Instance.repeatGlobal.RepeaterIsRunning = true;
            ExecuteScans();
         //  readScanResultsAndTrade();
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
             aTimer.Interval = (1000 * 60) * 30;
            //aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

       

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //control the amount of times the functio executes
            ExecutionTimes++;
            if(ExecutionTimes > 14)
            {
                readScanResultsAndTrade();
                aTimer.Stop();
                return; }
            Form1.Instance.repeatGlobal.RepeaterIsRunning = true;
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            ExecuteScans();

        }
        //execute the scans
        private static void ExecuteScans()
        {
            Form1.Instance.repeatGlobal.RepeaterIsRunning = true;
            //read the latest prices        
            Mining miner = new Mining();          
             miner.RunBrowserThread(input);          
            RunMethods rm = new RunMethods();
            rm.runStockPredictor(input, false);
            readScanResultsAndTrade();
        }
        //read the results of the scans
       private static void readScanResultsAndTrade()
        {
            //set the different methods as an array
            string[] methods = new string[4];
            methods[0] = "Bag"; methods[1] = "Named"; methods[2] = "Noun"; methods[3] = "Random";
            ExcelMethods exl = new ExcelMethods();
            Microsoft.Office.Interop.Excel.Application myPassExcelApp = exl.startExcelApp();
            Trading trader = new Trading();
            Mining miner = new Mining();
            miner.RunBrowserThread(input);//reload the prices
            foreach (string method in methods)
            {
                    int result = exl.ReadLatestFinalScore(myPassExcelApp, input, method);//read the total scores from the excel file
                    ProcessResults(result, method);
                
                if (!noTrading || ExecutionTimes > 14)
                {
                    processTrades(method,trader, myPassExcelApp);
                }
            }
        }
        //process trades
        public static void processTrades(string method, Trading trader, Microsoft.Office.Interop.Excel.Application myPassExcelApp)
        {
         
            string callField = "repeatData" + method;
            //dynamically cal the field     
            RepeaterData repeatData = GetFieldValue<RepeaterData>(Form1.Instance, callField);
            try { 
            //if the price is less than 1 to determine if it exists
            if(repeatData.PositionOpenPrice < 1 && ExecutionTimes < 15)
            {
                    OpenNewPosition(repeatData);
            }
           else if(repeatData.PositionOpenPrice > 0)
            {
                    if((!repeatData.IsShortSale && isShort) || (repeatData.IsShortSale && !isShort) || (ExecutionTimes > 14))
                    {
                        if (ExecutionTimes > 14)
                        {
                            SellPosition(repeatData, trader, method, myPassExcelApp);
                        }
                        else
                        { 
                        SellPosition(repeatData, trader, method, myPassExcelApp);
                        OpenNewPosition(repeatData);
                        }
                    }                

                }
            }
            catch(Exception ex) //catch the expection and set the values
            {
                Console.WriteLine(ex.Message + " In Process Trades - Repeater Class");
                repeatData.PositionOpenPrice = Form1.Instance.repeatGlobal.CurrentPrice;
                repeatData.IsShortSale = isShort;
                repeatData.IsStrong = isStrong;
            }
        }
        //open a new position
        private static void OpenNewPosition(RepeaterData repeatData)
        {
            repeatData.PositionOpenPrice = Form1.Instance.repeatGlobal.CurrentPrice;
            repeatData.IsShortSale = isShort;
            repeatData.IsStrong = isStrong;
        }
        //open a new position
        private static void SellPosition(RepeaterData repeatData, Trading trader, string method, Microsoft.Office.Interop.Excel.Application myPassExcelApp)
        {
            string[] prices = new string[2];
            prices[0] = repeatData.PositionOpenPrice.ToString();
            prices[1] = Form1.Instance.repeatGlobal.CurrentPrice.ToString();
            trader.simulateTrade(input, repeatData.IsShortSale, false, method, prices, myPassExcelApp, repeatData.IsStrong);
        }
        //process the final result
        private static void ProcessResults(int score, string method)
        {
            //if neutral data is stored then stop the auto trade
            if (score <= 4 && score >= -4 ) { Form1.Instance.AppendOutputText("\r\n" + "Neutral : No trading!" + "\r\n" + method + "\r\n"); noTrading = true; }
            //negative scores sell and strong sell        
            if (score < -4 && score > -15)
            {
                isShort = true; isStrong = false;
                Form1.Instance.AppendOutputText("\r\n" + "Sell " + "\r\n" + method + "\r\n");
            }
            if (score <= -15)
            {
                isShort = true; isStrong = true;
                Form1.Instance.AppendOutputText("\r\n" + "Strong sell :" + "\r\n" + method + "\r\n");
            }
            //positive scores buy and strong buy
            if (score > 4 && score < 15)
            {
                isShort = false; isStrong = false;
                Form1.Instance.AppendOutputText("\r\n" + "Buy : " + "\r\n" + method + "\r\n");
            };
            if (score >= 15)
            {
                isShort = false; isStrong = true;
                Form1.Instance.AppendOutputText("\r\n" + "Strong buy :" + "\r\n" + method + "\r\n");
            }
        }

      

        //get the values from an instance dynamically
        public static T GetFieldValue<T>(object obj, string fieldName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var field = obj.GetType().GetField(fieldName, BindingFlags.Public |
                                                          BindingFlags.NonPublic |
                                                          BindingFlags.Instance);

            if (field == null)
                throw new ArgumentException("fieldName", "No such field was found.");

            if (!typeof(T).IsAssignableFrom(field.FieldType))
                throw new InvalidOperationException("Field type and requested type are not compatible.");

            return (T)field.GetValue(obj);
        }
    }
}
