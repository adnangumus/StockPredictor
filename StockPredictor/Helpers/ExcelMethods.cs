
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;


namespace StockPredictor.Helpers
{
    class ExcelMethods
    {
        private readonly object locker = new object();
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
            try
            {
                //get the last row used
                Excel.Range last = myExcelWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                Excel.Range range = myExcelWorkSheet.get_Range("A1", last);
                int lastUsedRow = last.Row;
                //   int lastUsedColumn = last.Column;
                return lastUsedRow;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
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
        public int readLatestFinalScore(Excel.Application myPassedExcelApplication, string fileName, string method)
        {
            //check if there is sentiment data availble for the stock
            if (!getSentimentPath(fileName, method, myPassedExcelApplication)) { TradingForm.Instance.AppendOutputText("Data on " + fileName + " doesn't exist. Run the sentenment analysis." + "\r\n"); }
            int score = 0;
            //open the excel sheet
            openExcel(myPassedExcelApplication);
            //read the latest sentiment score
            Excel.Range range = myExcelWorkSheet.get_Range("B" + lastRow());
            try
            {
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

            try { 
            if (range.Value.GetType() == typeof(double))
            {
                principle = range.Value;
            }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            //close the excel sheet
            closeExcel();
            string principleStr = principle.ToString();
            //return the principle as a decimal
            return decimal.Parse(principleStr);
        }
        //save trading data to an excel sheet
        public void saveTradingData(Excel.Application myPassedExcelApplication, string symbol, string fileName, bool is20, string principle, string startPrinciple, string buy, string sell, bool isShort, string change, bool profitable, bool isStrong)
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
            addTradeData(principle, startPrinciple, buy, sell, isShort, change, date, profitable, isStrong);
            closeExcel();
        }

        //this method will take the out put data from the methods and save it to an excel file      
        public void savePredictorDataToExcel(Excel.Application myPassedExcelApplication, string fileName, string method, string elapsedMs, int totalSentiment, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
           int posWordPercentage, int negWordPercentage,
            int positivePhraseCount, int negativePhraseCount,
            int posPhrasePercentage, int negPhrasePercentage, double scoreFinal)
        {
            lock (locker)
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
                addDataToExcel(date, method, elapsedMs.ToString(), totalSentiment, wordCount, sentenceCount, posWordCount, negWordCount,
               posWordPercentage, negWordPercentage,
                positivePhraseCount, negativePhraseCount,
                posPhrasePercentage, negPhrasePercentage, scoreFinal);
                closeExcel();
            }
        }

       
        //open the excel sheet and pass if the sheet is opened for storing stock price information
        public void openExcel(Excel.Application myPassedExcelApplication)
        {
            if (myPassedExcelApplication == null)
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

           
            if (isTrade)
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
            myExcelWorkSheet.Cells[rowNumber, "A"] = "Date";
            myExcelWorkSheet.Cells[rowNumber, "B"] = "Profitable";
            myExcelWorkSheet.Cells[rowNumber, "C"] = "Principle";
            myExcelWorkSheet.Cells[rowNumber + 1, "C"] = "10000";           
            myExcelWorkSheet.Cells[rowNumber, "D"] = "Start Principle";
            myExcelWorkSheet.Cells[rowNumber, "E"] = "BuyPrice";
            myExcelWorkSheet.Cells[rowNumber, "F"] = "SellPrice";
            myExcelWorkSheet.Cells[rowNumber, "G"] = "IsShortSell";
            myExcelWorkSheet.Cells[rowNumber, "H"] = "Price Change %";
            myExcelWorkSheet.Cells[rowNumber, "I"] = "Strong trade";


            try
            {
                // Auto fit automatically adjust the width of columns of Excel  in givien range .  
                myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 13]].EntireColumn.AutoFit();
                rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one or wat ever is your logic
            }
            catch (Exception ex) { Console.WriteLine("Exception in adding heading : " + ex.Message); }
        }
        //add data to trade excel sheets
        private void addTradeData(string principle, string startPrinc, string buy, string sell, bool isShort, string change, string date, bool profitable, bool isStrong)
        {
            //add the data to the cells
            myExcelWorkSheet.Cells[rowNumber, "A"] = date;
            myExcelWorkSheet.Cells[rowNumber, "B"] = profitable;
            myExcelWorkSheet.Cells[rowNumber, "C"] = principle;
            myExcelWorkSheet.Cells[rowNumber, "D"] = startPrinc;
            myExcelWorkSheet.Cells[rowNumber, "E"] = buy;
            myExcelWorkSheet.Cells[rowNumber, "F"] = sell;
            myExcelWorkSheet.Cells[rowNumber, "G"] = isShort;
            myExcelWorkSheet.Cells[rowNumber, "H"] = change;
            myExcelWorkSheet.Cells[rowNumber, "I"] = isStrong;



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
            myExcelWorkSheet.Cells[rowNumber, "B"] = "ScoreFinal";
            myExcelWorkSheet.Cells[rowNumber, "C"] = "Verdict";
            myExcelWorkSheet.Cells[rowNumber, "D"] = "totalSentiment";
            myExcelWorkSheet.Cells[rowNumber, "E"] = "wordCount";
            myExcelWorkSheet.Cells[rowNumber, "F"] = "sentenceCount";
            myExcelWorkSheet.Cells[rowNumber, "G"] = "posWordPercentage";
            myExcelWorkSheet.Cells[rowNumber, "H"] = "negWordPercentage";
            myExcelWorkSheet.Cells[rowNumber, "I"] = "posPhrasePercentage";
            myExcelWorkSheet.Cells[rowNumber, "J"] = "negPhrasePercentage";
            myExcelWorkSheet.Cells[rowNumber, "K"] = "ElapsedMs";
            myExcelWorkSheet.Cells[rowNumber, "L"] = "posWordCount";
            myExcelWorkSheet.Cells[rowNumber, "M"] = "negWordCount";
            myExcelWorkSheet.Cells[rowNumber, "N"] = "positivePhraseCount";
            myExcelWorkSheet.Cells[rowNumber, "O"] = "negativePhraseCount";
            myExcelWorkSheet.Cells[rowNumber, "P"] = "Method";
            myExcelWorkSheet.Cells[rowNumber, "Q"] = "RSI";
            myExcelWorkSheet.Cells[rowNumber, "R"] = "PEG";
            myExcelWorkSheet.Cells[rowNumber, "S"] = "200Moving%";
            myExcelWorkSheet.Cells[rowNumber, "T"] = "50Moving%";
            myExcelWorkSheet.Cells[rowNumber, "U"] = "PriceBook";
            myExcelWorkSheet.Cells[rowNumber, "V"] = "Dividend";
            myExcelWorkSheet.Cells[rowNumber, "W"] = "Bollinger";


            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 13]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one or wat ever is your logic

        }

        //add data to the excel work sheet
        private void addDataToExcel(string date, string method, string elapsedMs, int totalSentiment, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
           int posWordPercentage, int negWordPercentage,
            int positivePhraseCount, int negativePhraseCount,
            int posPhrasePercentage, int negPhrasePercentage, double scoreFinal)
        {

            //add the data to the cells
            myExcelWorkSheet.Cells[rowNumber, "A"] = date;
            myExcelWorkSheet.Cells[rowNumber, "B"] = scoreFinal;
            myExcelWorkSheet.Cells[rowNumber, "C"] = Form1.Instance.verdict;
            myExcelWorkSheet.Cells[rowNumber, "D"] = totalSentiment;
            myExcelWorkSheet.Cells[rowNumber, "E"] = wordCount;
            myExcelWorkSheet.Cells[rowNumber, "F"] = sentenceCount;
            myExcelWorkSheet.Cells[rowNumber, "G"] = posWordPercentage;
            myExcelWorkSheet.Cells[rowNumber, "H"] = negWordPercentage;
            myExcelWorkSheet.Cells[rowNumber, "I"] = posPhrasePercentage;
            myExcelWorkSheet.Cells[rowNumber, "J"] = negPhrasePercentage;
            myExcelWorkSheet.Cells[rowNumber, "K"] = elapsedMs;
            myExcelWorkSheet.Cells[rowNumber, "L"] = posWordCount;
            myExcelWorkSheet.Cells[rowNumber, "M"] = negWordCount;
            myExcelWorkSheet.Cells[rowNumber, "N"] = positivePhraseCount;
            myExcelWorkSheet.Cells[rowNumber, "O"] = negativePhraseCount;
            myExcelWorkSheet.Cells[rowNumber, "P"] = method;
            //add the fundamental and technical data here
            myExcelWorkSheet.Cells[rowNumber, "Q"] = Form1.Instance.rsi;
            myExcelWorkSheet.Cells[rowNumber, "R"] = Form1.Instance.peg;
            myExcelWorkSheet.Cells[rowNumber, "S"] = Form1.Instance.moving200;
            myExcelWorkSheet.Cells[rowNumber, "T"] = Form1.Instance.moving50;
            myExcelWorkSheet.Cells[rowNumber, "U"] = Form1.Instance.priceBook;
            myExcelWorkSheet.Cells[rowNumber, "V"] = Form1.Instance.dividend;
            myExcelWorkSheet.Cells[rowNumber, "W"] = Form1.Instance.bollingerVerdict;

            try
            {
                //format the cells to dispaly the dates
                Excel.Range rg = (Excel.Range)myExcelWorkSheet.Cells[1, "A"];
                rg.EntireColumn.NumberFormat = "m/d/yyyy h:mm";
                // Auto fit automatically adjust the width of columns of Excel  in givien range .  
                myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 6]].EntireColumn.AutoFit();
                rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one 
            }
            catch (Exception ex) { Console.WriteLine("Exception in adding heading : " + ex.Message); }

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
            catch (Exception ex)
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
            if (myExcelWorkSheet != null)
            {
                Marshal.FinalReleaseComObject(myExcelWorkSheet);
            }
            if (myExcelWorkbook != null)
            {
                Marshal.FinalReleaseComObject(myExcelWorkbook);
            }
        }

    }//end class
}//end name space
