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
        private static string[] inputArray;
        private static bool ScanAllBio;
        private static RepeaterGlobalVariables repeatGlobal;
        private static RepeaterData repeatData;
        //this variable is used to cotrol how many times the trader will run.  ExecutionTimes >= maxExecutionTimes
        private static int maxExecutionTimes = 12;

        public void RunRepeater(string str)
        {
            ScanAllBio = false;
            //the number of times an execution takes place
            ExecutionTimes = 0;
            if(str.ToUpper() == "BIO") {
                string[] inputs = new string[4];
                inputs[0] = "GILD";
                inputs[1] = "HZNP";
                inputs[2] = "BIIB";
                inputs[3] = "CELG";
                inputArray = inputs;
                ScanAllBio = true;
            }
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
            if(ExecutionTimes >= maxExecutionTimes)
            {
                readScanResultsAndTrade();
                aTimer.Stop();
                return;
            }
            Form1.Instance.repeatGlobal.RepeaterIsRunning = true;
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            ExecuteScans();

        }
        //execute the scans
        private static void ExecuteScans()
        {
           
            Form1.Instance.repeatGlobal.RepeaterIsRunning = true;
            //read the latest prices        
            RunMethods rm = new RunMethods();
            //<----Run this if it is proccessing all bio stocks at the same time--->
            if (ScanAllBio)
            {
                foreach (string ticker in inputArray)
                {
                    input = ticker;//change the input value
                    Form1.Instance.scanMetrics.Ticker = input; // set the ticker for display purposes
                    string callField2 = "repeatGlobal" + input;
                    //set the prices to zero to prevent mis information
                    Form1.Instance.repeatGlobal.OpenPrice = 0;
                    Form1.Instance.repeatGlobal.CurrentPrice = 0;
                    Mining.RunBrowserThread(input);                  
                    //dynamically cal the field     
                    repeatGlobal = GetFieldValue<RepeaterGlobalVariables>(Form1.Instance, callField2);
                    //load the previous links from the last search here
                    Form1.Instance.repeatGlobal.LinksOld = repeatGlobal.LinksOld;
                    //run the scans here
                    rm.runStockPredictor(input, false);
                    //save the old links here
                    repeatGlobal.LinksOld = Form1.Instance.repeatGlobal.LinksOld ;
                    readScanResultsAndTrade();
                }
            }
            //<-----end proccess all bio here ------>
            else   //do this if it isn't on scanning all the metrics
            {
                Form1.Instance.scanMetrics.Ticker = input; // set the ticker for display purposes 
                Mining.RunBrowserThread(input);              
                rm.runStockPredictor(input, false);
                readScanResultsAndTrade();
            }
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
            Mining.RunBrowserThread(input);          
            foreach (string method in methods)
            {
                    int result = exl.ReadLatestFinalScore(myPassExcelApp, input, method);//read the total scores from the excel file
                    ProcessResults(result, method);
                //trade when the predictor dictates, on the last run and don't trade on the first run
                if (!noTrading || ExecutionTimes >= maxExecutionTimes || ExecutionTimes > 0)
                {
                    processTrades(method,trader, myPassExcelApp);
                }
            }
            
        }
        //process trades
        public static void processTrades(string method, Trading trader, Microsoft.Office.Interop.Excel.Application myPassExcelApp)
        {
            try {
                if(ScanAllBio)
                {
                    string callField = "repeatData" + method + input;
                    //dynamically cal the field     
                    repeatData = GetFieldValue<RepeaterData>(Form1.Instance, callField);
                }
                else//do this if it isn't processing multiple stocks
                { 
            string callField = "repeatData" + method;
            //dynamically cal the field     
           repeatData = GetFieldValue<RepeaterData>(Form1.Instance, callField);
                }
            }
            catch { Console.WriteLine("Failed to load data variables in repeater --> processtrades"); }
            try { 
            //if the price is less than 1 to determine if it exists
            if(repeatData.PositionOpenPrice < 1 && ExecutionTimes < maxExecutionTimes && Form1.Instance.repeatGlobal.CurrentPrice > 0)
            {
                    OpenNewPosition();
            }
           else if(repeatData.PositionOpenPrice > 0 && Form1.Instance.repeatGlobal.CurrentPrice > 0)
            {        // if there are open positions check the following conditions are met before selling. 
                    if((!repeatData.IsShortSale && isShort) || (repeatData.IsShortSale && !isShort) || (ExecutionTimes >= maxExecutionTimes) 
                        || (repeatData.IsStrong && !isStrong) || (!repeatData.IsStrong && isStrong))
                    {
                        if (ExecutionTimes >= maxExecutionTimes)
                        {
                            SellPosition(trader, method, myPassExcelApp);
                        }
                        else
                        { 
                        SellPosition(trader, method, myPassExcelApp);
                        OpenNewPosition();
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
        private static void OpenNewPosition()
        {
            repeatData.PositionOpenPrice = Form1.Instance.repeatGlobal.CurrentPrice;
            repeatData.IsShortSale = isShort;
            repeatData.IsStrong = isStrong;
        }
        //open a new position
        private static void SellPosition(Trading trader, string method, Microsoft.Office.Interop.Excel.Application myPassExcelApp)
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
                isShort = true; isStrong = false; noTrading = false;
                Form1.Instance.AppendOutputText("\r\n" + "Sell " + "\r\n" + method + "\r\n");
            }
            if (score <= -15)
            {
                isShort = true; isStrong = true; noTrading = false;
                Form1.Instance.AppendOutputText("\r\n" + "Strong sell :" + "\r\n" + method + "\r\n");
            }
            //positive scores buy and strong buy
            if (score > 4 && score < 15)
            {
                isShort = false; isStrong = false; noTrading = false;
                Form1.Instance.AppendOutputText("\r\n" + "Buy : " + "\r\n" + method + "\r\n");
            };
            if (score >= 15)
            {
                isShort = false; isStrong = true; noTrading = false;
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
