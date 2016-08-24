using System.Linq;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;

namespace StockPredictor.Helpers
{
    class ExcelMethods
    {
        private string excelFilePath = string.Empty;
        private string fileFolderPath = string.Empty;
        private int rowNumber = 1; // define first row number to enter data in excel

        Excel.Application myExcelApplication;
        Excel.Workbook myExcelWorkbook;
        Excel.Worksheet myExcelWorkSheet;

        public string ExcelFilePath
        {
            get { return excelFilePath; }
            set { excelFilePath = value; }
        }
        //for storing the folder path for the excel files
        public string FileFolderPath
        {
            get { return fileFolderPath; }
            set { fileFolderPath = value; }
        }

        public int Rownumber
        {
            get { return rowNumber; }
            set { rowNumber = value; }
        }
        //get the last row used in the excel sheet
        private int lastRow()
        {
            try { 
            //get the last row used
            Excel.Range last = myExcelWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range range = myExcelWorkSheet.get_Range("A1", last);
            int lastUsedRow = last.Row;
        //   int lastUsedColumn = last.Column;
            return lastUsedRow;
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            return 0;
    }
        //get the path of sentiment document
        private bool getSentimentPath(string fileName, string method, Excel.Application myPassedExcelApplication)
        {
            //file reader class for reading files
            fileReaderWriter frw = new fileReaderWriter();
            //create a path that puts the stock information into unique folders with the name of the stock and method on seperate 
            //excel sheets
            string folderPath = Path.Combine(frw.GetAppFolder(), @"packages\Data\" + fileName).ToString();
            fileFolderPath = folderPath;
            string filePath = Path.Combine(fileFolderPath + @"\" + fileName + method).ToString();
            ExcelFilePath = filePath;
            //check if the excel file exists and if it doesn't create one and add the headings            
            if (!File.Exists(excelFilePath + ".xlsx"))
            {
                createSheet(false, false, myPassedExcelApplication);
                return false;
            }
            return true;
        }
    //get the path of the rade documents
        private void getTradePath(string fileName, string symbol, Excel.Application myPassedExcelApplication)
        {
            
            //create a path that puts the stock information into unique folders with the name of the stock and method on seperate 
            //excel sheets
            fileReaderWriter frw = new fileReaderWriter();
            string folderPath = Path.Combine(frw.GetAppFolder(), @"packages\Data\Trades\" + symbol).ToString();
            fileFolderPath = folderPath;
            string filePath = Path.Combine(fileFolderPath + @"\" + fileName).ToString();
            ExcelFilePath = filePath;
            //check if the excel file exists and print a message to the output box            
            if (!File.Exists(excelFilePath + ".xlsx"))
            {
                TradingForm.Instance.AppendOutputText("\r\n" + "Creating trading sheet" + "\r\n");
                createSheet(false, true, myPassedExcelApplication);
            }
           
        }
        //read the score from the sentiment anlysis
        public int readLatestSentimentScore(Excel.Application myPassedExcelApplication, string fileName, string method)
        {
            //check if there is sentiment data availble for the stock
            if(!getSentimentPath(fileName, method, myPassedExcelApplication)) { TradingForm.Instance.AppendOutputText("Data on " + fileName + " doesn't exist. Run the sentenment analysis." + "\r\n"); }
            int score = 0;
            //open the excel sheet
            openExcel(myPassedExcelApplication);
            //read the latest sentiment score
            Excel.Range range = myExcelWorkSheet.get_Range("B" + lastRow());
            try { 
            score = Convert.ToInt32(range.Value);
            }
            catch (Exception ex) { TradingForm.Instance.AppendOutputText("Data on " + fileName + " doesn't exist. Run the sentenment analysis." + "\r\n"); closeExcel(); return 0; }
            closeExcel();
            return score;
        }
        //read from an excel sheet - is20 is a trade that takes place within 20minutes of open
        public decimal readPrinciple(Excel.Application myPassedExcelApplication, string fileName, string symbol, bool is20)
        {           
            if (is20) { fileName += "20"; }          
                //get the folder and file name
                getTradePath(fileName, symbol, myPassedExcelApplication);
            //open the excel sheet
            openExcel(myPassedExcelApplication);
            //set the last row
            Rownumber = lastRow();
            if (TradingForm.Instance.isRetry()) { Rownumber--; }
            Excel.Range range = myExcelWorkSheet.get_Range("A" + rowNumber);
            //read the data from the cell that has the principle
            double principle = 10000;
            if (range.Value.GetType() == typeof(double)) {
                principle  = range.Value;
            }
            //close the excel sheet
            closeExcel();
            string principleStr = principle.ToString();
            //return the principle as a decimal
            return decimal.Parse(principleStr);
        }
        //save trading data to an excel sheet
        public void saveTradingData(Excel.Application myPassedExcelApplication, string symbol, string fileName, bool is20, string principle, string startPrinciple, string buy, string sell, bool isShort, string change, bool profitable)
        {
            //get the date time to insert into the excel sheet
            string date = DateTime.Now.ToString();

            if (is20) { fileName += "20"; }
            //get the folder and file name
            getTradePath(fileName, symbol, myPassedExcelApplication);

          
            //open the excel sheet
            openExcel(myPassedExcelApplication);
            //check if the retry box is thicked and over right previos data
            if (TradingForm.Instance.isRetry()) { Rownumber--; }
            //add the data to the excel sheet
            addTradeData(principle, startPrinciple, buy, sell, isShort, change, date, profitable);           
            closeExcel();
        }

        //this method will take the out put data from the methods and save it to an excel file      
        public void savePredictorDataToExcel(Excel.Application myPassedExcelApplication, string fileName, string method, string elapsedMs,int totalScore, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
           int posWordPercentage, int negWordPercentage,
            int positivePhraseCount, int negativePhraseCount,
            int posPhrasePercentage, int negPhrasePercentage)
        {
            lock(this)
            { 
            fileName = fileName.ToUpper();
            //get the date time to insert into the excel sheet
            string date = DateTime.Now.ToString();
            //set the path for the file
            getSentimentPath(fileName, method, myPassedExcelApplication);          
            //open the excel sheet
            openExcel(myPassedExcelApplication);
            if (Form1.Instance.isRetry()) { Rownumber--; }
            //add the data to the excel sheet
            addDataToExcel(date, method, elapsedMs.ToString(), totalScore, wordCount, sentenceCount, posWordCount, negWordCount,
           posWordPercentage, negWordPercentage,
            positivePhraseCount, negativePhraseCount,
            posPhrasePercentage, negPhrasePercentage);
            closeExcel();
            }
        }

        //save the price data from yahoo to an excel sheet
        public void savePriceData(string name, string ticker, decimal openPrice, decimal closePrice, decimal changeInPercent, decimal lastTradePriceOnly)
        {
            //get the date time to insert into the excel sheet
            string date = DateTime.Now.ToString();
            //file reader class for reading files
            fileReaderWriter frw = new fileReaderWriter();
            //put the ticker symbol in lower case
            string fileName = ticker;
            //set the path for the file
            getSentimentPath(fileName, "PriceInformation", null);
            Excel.Application myPassedExcelApplication = null;
            //open the excel sheet
            openExcel(myPassedExcelApplication);
            //add the data to the excel sheet
            addDataToPriceExcel(date,name,ticker,openPrice,closePrice,changeInPercent, lastTradePriceOnly);
            closeExcel();
        }
        //open the excel sheet and pass if the sheet is opened for storing stock price information
        public void openExcel(Excel.Application myPassedExcelApplication)
        {
           if(myPassedExcelApplication == null)
            { 
            myExcelApplication = new Excel.Application(); // create Excell App
            }
            else { myExcelApplication = myPassedExcelApplication; }
            myExcelApplication.DisplayAlerts = false; // turn off alerts
            //open the excel work sheet
            myExcelWorkbook = (Excel.Workbook)(myExcelApplication.Workbooks._Open(excelFilePath, ReadOnly: false)); // open the existing excel file

           // int numberOfWorkbooks = myExcelApplication.Workbooks.Count; // get number of workbooks (optional)

            myExcelWorkSheet = (Excel.Worksheet)myExcelWorkbook.Worksheets[1]; // define in which worksheet, do you want to add data
            myExcelWorkSheet.Name = "WorkSheet 1"; // define a name for the worksheet (optinal)

           // var workSheets = myExcelWorkbook.Worksheets;
            //int numberOfSheets = workSheets.Count; // get number of worksheets (optional)
          
           //set the last row
            Rownumber = lastRow() + 1;
        }
        //create a new excel work sheet. Open it and add the headings
        public void createSheet(bool isPrice, bool isTrade, Excel.Application myPassedExcelApplication)
        {
            if (myPassedExcelApplication == null)
            {
                myExcelApplication = new Excel.Application(); // create Excell App
            }
            else { myExcelApplication = myPassedExcelApplication; }
            myExcelApplication.DisplayAlerts = false; // turn off alerts

            var workBook = myExcelApplication.Workbooks.Add(Type.Missing);
            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(fileFolderPath);
            workBook.SaveAs(excelFilePath);
            //open the excel work sheet
            myExcelWorkbook = (Excel.Workbook)(myExcelApplication.Workbooks._Open(excelFilePath, ReadOnly: false)); // open the existing excel file

          //  int numberOfWorkbooks = myExcelApplication.Workbooks.Count; // get number of workbooks (optional)

            myExcelWorkSheet = (Excel.Worksheet)myExcelWorkbook.Worksheets[1]; // define in which worksheet, do you want to add data
            myExcelWorkSheet.Name = "WorkSheet 1"; // define a name for the worksheet (optinal)

           // int numberOfSheets = myExcelWorkbook.Worksheets.Count; // get number of worksheets (optional)
           //set the last row
            Rownumber = lastRow();
            //add the headings to the excel sheet

            //add the headings for the predictor pages
            if (isPrice) {
                addHeadingToPriceExcel();
            }
            if(isTrade)
            {
                addHeadingTradingSheet();
            }
            else
            {
              
                addHeadingToExcel();
            }
            closeExcel();
        }
        //add heading to simulated trading work sheet
        private void addHeadingTradingSheet()
        {
            //add the data to the cells in the rows
            myExcelWorkSheet.Cells[rowNumber, "A"] = "Principle";
            myExcelWorkSheet.Cells[rowNumber + 1, "A"] = "10000";
            myExcelWorkSheet.Cells[rowNumber, "B"] = "Start Principle";
            myExcelWorkSheet.Cells[rowNumber, "C"] = "BuyPrice";
            myExcelWorkSheet.Cells[rowNumber, "D"] = "SellPrice";
            myExcelWorkSheet.Cells[rowNumber, "E"] = "IsShortSell";
            myExcelWorkSheet.Cells[rowNumber, "F"] = "Price Change %";
            myExcelWorkSheet.Cells[rowNumber, "G"] = "Date";
            myExcelWorkSheet.Cells[rowNumber, "H"] = "Profitable";
            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 13]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one or wat ever is your logic
        }
        //add data to trade excel sheets
        private void addTradeData(string principle, string startPrinc, string buy, string sell, bool isShort, string change, string date, bool profitable)
        {
            //add the data to the cells
            myExcelWorkSheet.Cells[rowNumber, "A"] = principle;
            myExcelWorkSheet.Cells[rowNumber, "B"] = startPrinc;
            myExcelWorkSheet.Cells[rowNumber, "C"] = buy;
            myExcelWorkSheet.Cells[rowNumber, "D"] = sell;
            myExcelWorkSheet.Cells[rowNumber, "E"] = isShort;
            myExcelWorkSheet.Cells[rowNumber, "F"] = change;
            myExcelWorkSheet.Cells[rowNumber, "g"] = date;
            myExcelWorkSheet.Cells[rowNumber, "h"] = profitable;

            //format the cells to dispaly the dates
            Excel.Range rg = (Excel.Range)myExcelWorkSheet.Cells[1, "G"];
            rg.EntireColumn.NumberFormat = "m/d/yyyy h:mm";
            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 6]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one 
        }
        //method that adds the heading to the newly created excel documents
        private void addHeadingToExcel()
        {           
            //add the data to the cells in the rows
            myExcelWorkSheet.Cells[rowNumber, "A"] = "Date";
            myExcelWorkSheet.Cells[rowNumber, "B"] = "totalScore";
            myExcelWorkSheet.Cells[rowNumber, "C"] = "posWordPercentage";
            myExcelWorkSheet.Cells[rowNumber, "D"] = "negWordPercentage";
            myExcelWorkSheet.Cells[rowNumber, "E"] = "posPhrasePercentage";
            myExcelWorkSheet.Cells[rowNumber, "F"] = "negPhrasePercentage";
            myExcelWorkSheet.Cells[rowNumber, "G"] = "ElapsedMs";
            myExcelWorkSheet.Cells[rowNumber, "H"] = "wordCount";
            myExcelWorkSheet.Cells[rowNumber, "I"] = "sentenceCount";
            myExcelWorkSheet.Cells[rowNumber, "J"] = "posWordCount";
            myExcelWorkSheet.Cells[rowNumber, "K"] = "negWordCount";         
            myExcelWorkSheet.Cells[rowNumber, "L"] = "positivePhraseCount";
            myExcelWorkSheet.Cells[rowNumber, "M"] = "negativePhraseCount";
            myExcelWorkSheet.Cells[rowNumber, "N"] = "Method";

            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 13]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one or wat ever is your logic

        }

        //add data to the excel work sheet
        private void addDataToExcel(string date, string method, string elapsedMs,int totalScore, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
           int posWordPercentage, int negWordPercentage,
            int positivePhraseCount, int negativePhraseCount,
            int posPhrasePercentage, int negPhrasePercentage)
        {
          
            //add the data to the cells
            myExcelWorkSheet.Cells[rowNumber, "A"] = date;
            myExcelWorkSheet.Cells[rowNumber, "B"] = totalScore;
            myExcelWorkSheet.Cells[rowNumber, "C"] = posWordPercentage;
            myExcelWorkSheet.Cells[rowNumber, "D"] = negWordPercentage;
            myExcelWorkSheet.Cells[rowNumber, "E"] = posPhrasePercentage;
            myExcelWorkSheet.Cells[rowNumber, "F"] = negPhrasePercentage;
            myExcelWorkSheet.Cells[rowNumber, "G"] = elapsedMs;
            myExcelWorkSheet.Cells[rowNumber, "H"] = wordCount;
            myExcelWorkSheet.Cells[rowNumber, "I"] = sentenceCount;
            myExcelWorkSheet.Cells[rowNumber, "J"] = posWordCount;
            myExcelWorkSheet.Cells[rowNumber, "K"] = negWordCount;           
            myExcelWorkSheet.Cells[rowNumber, "L"] = positivePhraseCount;
            myExcelWorkSheet.Cells[rowNumber, "M"] = negativePhraseCount;
            myExcelWorkSheet.Cells[rowNumber, "N"] = method;

            //format the cells to dispaly the dates
            Excel.Range rg = (Excel.Range)myExcelWorkSheet.Cells[1, "A"];
            rg.EntireColumn.NumberFormat = "m/d/yyyy h:mm";
            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 6]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one 

        }

        //method that adds the heading to the newly created excel documents
        private void addHeadingToPriceExcel()
        {
            //add the data to the cells in the rows
            myExcelWorkSheet.Cells[rowNumber, "A"] = "Date";
            myExcelWorkSheet.Cells[rowNumber, "B"] = "Name";
            myExcelWorkSheet.Cells[rowNumber, "C"] = "Ticker";
            myExcelWorkSheet.Cells[rowNumber, "D"] = "Open Price";
            myExcelWorkSheet.Cells[rowNumber, "E"] = "LastTradePrice";
            myExcelWorkSheet.Cells[rowNumber, "F"] = "ChangeInPercent";        
            myExcelWorkSheet.Cells[rowNumber, "G"] = "Previous close price";
            myExcelWorkSheet.Cells[rowNumber, "H"] = "30 minute mark";
            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 6]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one or wat ever is your logic
        }
        //method that adds the heading to the newly created excel documents
        private void addDataToPriceExcel(string date, string name, string ticker, decimal openPrice, decimal closePrice, decimal changeInPercent, decimal lastTradePriceOnly)
        {
            //add the data to the cells in the rows
            myExcelWorkSheet.Cells[rowNumber, "A"] = date;
            myExcelWorkSheet.Cells[rowNumber, "B"] = name;
            myExcelWorkSheet.Cells[rowNumber, "C"] = ticker;
            myExcelWorkSheet.Cells[rowNumber, "D"] = openPrice;
            myExcelWorkSheet.Cells[rowNumber, "E"] = lastTradePriceOnly;
            myExcelWorkSheet.Cells[rowNumber, "F"] = changeInPercent;            
            myExcelWorkSheet.Cells[rowNumber, "G"] = closePrice;
            //format the cells to dispaly the dates
            Excel.Range rg = (Excel.Range)myExcelWorkSheet.Cells[1, "A"];
            rg.EntireColumn.NumberFormat = "m/d/yyyy h:mm";
            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 6]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one 
        }

        public void closeExcel()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
               
                Marshal.FinalReleaseComObject(myExcelWorkSheet);

                myExcelWorkbook.SaveAs(excelFilePath, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value); // Save data in excel


                myExcelWorkbook.Close(true, excelFilePath, System.Reflection.Missing.Value); // close the worksheet
                Marshal.FinalReleaseComObject(myExcelWorkbook);

            }
            catch(Exception ex)
            {
                Console.WriteLine("Excel document couldn't be closed: This might require you to restart your computer to resolve" + ex.Message);
            }
            finally
            {
                if (myExcelApplication != null)
                {
                    myExcelApplication.Quit(); // close the excel application 
                 
                    Console.WriteLine("Excel application closed");
                }
            }           
        }
        //start the excel application
        public Excel.Application startExcelApp()
        {
            myExcelApplication = null;
            myExcelApplication = new Excel.Application(); // create Excell App
            return myExcelApplication;
        }
        //close the excell application after use
        public void quitExcel(Excel.Application myPassedExcelApplication)
        {
                if (myExcelApplication != null)
                {              
                myExcelApplication.Quit(); // close the excel application
                    Console.WriteLine("Excel application closed");               
                }
            if (myPassedExcelApplication != null)
            {                
                myPassedExcelApplication.Quit(); // close the excel application
                Console.WriteLine("Excel application closed");
            }
            if(myExcelWorkSheet != null)
            {
                Marshal.FinalReleaseComObject(myExcelWorkSheet);
            }
            if(myExcelWorkbook != null)
            {
                Marshal.FinalReleaseComObject(myExcelWorkbook);
            }
        }
     
    }//end class
}//end name space
