using System.Linq;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

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

        //this method will take the out put data from the methods and save it to an excel file      
        public void savePredictorDataToExcel(string fileName, string method, string elapsedMs, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
           int posWordPercentage, int negWordPercentage,
            int positivePhraseCount, int negativePhraseCount,
            int posPhrasePercentage, int negPhrasePercentage)
        {
            fileName = fileName.ToUpper();
            //get the date time to insert into the excel sheet
            string date = DateTime.Now.ToString();
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
                createSheet(false);               
            }
            //open the excel sheet
            openExcel();
            //add the data to the excel sheet
            addDataToExcel(date, method, elapsedMs.ToString(), wordCount, sentenceCount, posWordCount, negWordCount,
           posWordPercentage, negWordPercentage,
            positivePhraseCount, negativePhraseCount,
            posPhrasePercentage, negPhrasePercentage);
            closeExcel();
        }

        //save the price data from yahoo to an excel sheet
        public void savePriceData(string name, string ticker, decimal openPrice, decimal closePrice, decimal changeInPercent)
        {
            //get the date time to insert into the excel sheet
            string date = DateTime.Now.ToString();
            //file reader class for reading files
            fileReaderWriter frw = new fileReaderWriter();
            //put the ticker symbol in lower case
            string fileName = ticker;
            //create a path that puts the stock information into unique folders with the name of the stock and method on seperate 
            //excel sheets
            string folderPath = Path.Combine(frw.GetAppFolder(), @"packages\Data\" + ticker).ToString();
            fileFolderPath = folderPath;
            string filePath = Path.Combine(fileFolderPath + @"\" + fileName + "priceInformation").ToString();
            ExcelFilePath = filePath;
            //check if the excel file exists and if it doesn't create one and add the headings            
            if (!File.Exists(excelFilePath + ".xlsx"))
            {
                createSheet(true);               
            }
            //open the excel sheet
            openExcel();
            //add the data to the excel sheet
            addDataToPriceExcel(date,name,ticker,openPrice,closePrice,changeInPercent);
            closeExcel();
        }
        //open the excel sheet and pass if the sheet is opend for storing stock price information
        public void openExcel()
        {
            myExcelApplication = null;
            myExcelApplication = new Excel.Application(); // create Excell App
            myExcelApplication.DisplayAlerts = false; // turn off alerts
            //open the excel work sheet
            myExcelWorkbook = (Excel.Workbook)(myExcelApplication.Workbooks._Open(excelFilePath, ReadOnly: false)); // open the existing excel file

            int numberOfWorkbooks = myExcelApplication.Workbooks.Count; // get number of workbooks (optional)

            myExcelWorkSheet = (Excel.Worksheet)myExcelWorkbook.Worksheets[1]; // define in which worksheet, do you want to add data
            myExcelWorkSheet.Name = "WorkSheet 1"; // define a name for the worksheet (optinal)

            int numberOfSheets = myExcelWorkbook.Worksheets.Count; // get number of worksheets (optional)
          
            Excel.Range last = myExcelWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range range = myExcelWorkSheet.get_Range("A1", last);
            int lastUsedRow = last.Row;
            int lastUsedColumn = last.Column;
            Rownumber = lastUsedRow + 1;
        }
        //create a new excel work sheet. Open it and add the headings
        public void createSheet(bool isPrice)
        {
            myExcelApplication = null;
            myExcelApplication = new Excel.Application(); // create Excell App
            myExcelApplication.DisplayAlerts = false; // turn off alerts

            var workBook = myExcelApplication.Workbooks.Add(Type.Missing);
            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(fileFolderPath);
            workBook.SaveAs(excelFilePath);
            //open the excel work sheet
            myExcelWorkbook = (Excel.Workbook)(myExcelApplication.Workbooks._Open(excelFilePath, ReadOnly: false)); // open the existing excel file

            int numberOfWorkbooks = myExcelApplication.Workbooks.Count; // get number of workbooks (optional)

            myExcelWorkSheet = (Excel.Worksheet)myExcelWorkbook.Worksheets[1]; // define in which worksheet, do you want to add data
            myExcelWorkSheet.Name = "WorkSheet 1"; // define a name for the worksheet (optinal)

            int numberOfSheets = myExcelWorkbook.Worksheets.Count; // get number of worksheets (optional)

            Excel.Range last = myExcelWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range range = myExcelWorkSheet.get_Range("A1", last);
            int lastUsedRow = last.Row;
            int lastUsedColumn = last.Column;
            Rownumber = lastUsedRow;
            //add the headings to the excel sheet

            //add the headings for the predictor pages
            if (!isPrice) { 
            addHeadingToExcel();
            }
            else
            {
                addHeadingToPriceExcel();
            }
            closeExcel();
        }

        //method that adds the heading to the newly created excel documents
        public void addHeadingToExcel()
        {           
            //add the data to the cells in the rows
            myExcelWorkSheet.Cells[rowNumber, "A"] = "Date";
            myExcelWorkSheet.Cells[rowNumber, "B"] = "Method";
            myExcelWorkSheet.Cells[rowNumber, "C"] = "ElapsedMs";
            myExcelWorkSheet.Cells[rowNumber, "D"] = "wordCount";
            myExcelWorkSheet.Cells[rowNumber, "E"] = "sentenceCount";
            myExcelWorkSheet.Cells[rowNumber, "F"] = "posWordCount";
            myExcelWorkSheet.Cells[rowNumber, "G"] = "negWordCount";
            myExcelWorkSheet.Cells[rowNumber, "H"] = "posWordPercentage";
            myExcelWorkSheet.Cells[rowNumber, "I"] = "negWordPercentage";
            myExcelWorkSheet.Cells[rowNumber, "J"] = "positivePhraseCount";
            myExcelWorkSheet.Cells[rowNumber, "K"] = "negativePhraseCount";
            myExcelWorkSheet.Cells[rowNumber, "L"] = "posPhrasePercentage";
            myExcelWorkSheet.Cells[rowNumber, "M"] = "negPhrasePercentage";
            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 13]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one or wat ever is your logic

        }

        //add data to the excel work sheet
        public void addDataToExcel(string date, string method, string elapsedMs, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
           int posWordPercentage, int negWordPercentage,
            int positivePhraseCount, int negativePhraseCount,
            int posPhrasePercentage, int negPhrasePercentage)
        {          
            //add the data to the cells
            myExcelWorkSheet.Cells[rowNumber, "A"] = date;
            myExcelWorkSheet.Cells[rowNumber, "B"] = method;
            myExcelWorkSheet.Cells[rowNumber, "C"] = elapsedMs;
            myExcelWorkSheet.Cells[rowNumber, "D"] = wordCount;
            myExcelWorkSheet.Cells[rowNumber, "E"] = sentenceCount;
            myExcelWorkSheet.Cells[rowNumber, "F"] = posWordCount;
            myExcelWorkSheet.Cells[rowNumber, "G"] = negWordCount;
            myExcelWorkSheet.Cells[rowNumber, "H"] = posWordPercentage;
            myExcelWorkSheet.Cells[rowNumber, "I"] = negWordPercentage;
            myExcelWorkSheet.Cells[rowNumber, "J"] = positivePhraseCount;
            myExcelWorkSheet.Cells[rowNumber, "K"] = negativePhraseCount;
            myExcelWorkSheet.Cells[rowNumber, "L"] = posPhrasePercentage;
            myExcelWorkSheet.Cells[rowNumber, "M"] = negPhrasePercentage;
            //format the cells to dispaly the dates
            Excel.Range rg = (Excel.Range)myExcelWorkSheet.Cells[1, "A"];
            rg.EntireColumn.NumberFormat = "m/d/yyyy h:mm";
            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 6]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one 

        }

        //method that adds the heading to the newly created excel documents
        public void addHeadingToPriceExcel()
        {
            //add the data to the cells in the rows
            myExcelWorkSheet.Cells[rowNumber, "A"] = "Date";
            myExcelWorkSheet.Cells[rowNumber, "B"] = "Name";
            myExcelWorkSheet.Cells[rowNumber, "C"] = "Ticker";
            myExcelWorkSheet.Cells[rowNumber, "D"] = "Open Price";
            myExcelWorkSheet.Cells[rowNumber, "E"] = "Close Price";
            myExcelWorkSheet.Cells[rowNumber, "F"] = "ChangeInPercent";
            // Auto fit automatically adjust the width of columns of Excel  in givien range .  
            myExcelWorkSheet.Range[myExcelWorkSheet.Cells[1, 1], myExcelWorkSheet.Cells[rowNumber, 6]].EntireColumn.AutoFit();
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one or wat ever is your logic
        }
        //method that adds the heading to the newly created excel documents
        public void addDataToPriceExcel(string date, string name, string ticker, decimal openPrice, decimal closePrice, decimal changeInPercent)
        {
            //add the data to the cells in the rows
            myExcelWorkSheet.Cells[rowNumber, "A"] = date;
            myExcelWorkSheet.Cells[rowNumber, "B"] = name;
            myExcelWorkSheet.Cells[rowNumber, "C"] = ticker;
            myExcelWorkSheet.Cells[rowNumber, "D"] = openPrice;
            myExcelWorkSheet.Cells[rowNumber, "E"] = closePrice;
            myExcelWorkSheet.Cells[rowNumber, "F"] = changeInPercent;
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
                myExcelWorkbook.SaveAs(excelFilePath, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value); // Save data in excel


                myExcelWorkbook.Close(true, excelFilePath, System.Reflection.Missing.Value); // close the worksheet


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

     
    }//end class
}//end name space
