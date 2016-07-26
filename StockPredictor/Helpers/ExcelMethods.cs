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

        public void openExcel()
        {
            myExcelApplication = null;

            myExcelApplication = new Excel.Application(); // create Excell App
            myExcelApplication.DisplayAlerts = false; // turn off alerts
            //check if the excel file exists and if it doesn't create one
            if (!File.Exists(excelFilePath))
            {
             var workBook = myExcelApplication.Workbooks.Add(Type.Missing);
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(fileFolderPath);
                workBook.SaveAs(excelFilePath);
            }
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

        public void addDataToExcel(string date, string method, string elapsedMs, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
           int posWordPercentage, int negWordPercentage,
            int positivePhraseCount, int negativePhraseCount,
            int posPhrasePercentage, int negPhrasePercentage)
        {

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
            myExcelWorkSheet.Cells[rowNumber, "L"] = posWordPercentage;
            myExcelWorkSheet.Cells[rowNumber, "M"] = negPhrasePercentage;
            rowNumber++;  // if you put this method inside a loop, you should increase rownumber by one or wat ever is your logic

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
            finally
            {
                if (myExcelApplication != null)
                {
                    myExcelApplication.Quit(); // close the excel application
                    Console.WriteLine("Excel application closed");
                }
            }

        }

        //this method will take the out put data from the methods and save it to an excel file      
        public void saveDataToExcel(string fileName,string method, string elapsedMs, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
           int posWordPercentage, int negWordPercentage,
            int positivePhraseCount, int negativePhraseCount,
            int posPhrasePercentage, int negPhrasePercentage)
        {
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
            openExcel();
            addDataToExcel(date, method, elapsedMs.ToString(), wordCount, sentenceCount, posWordCount, negWordCount,
           posWordPercentage, negWordPercentage,
            positivePhraseCount, negativePhraseCount,
            posPhrasePercentage, negPhrasePercentage);
            closeExcel();
        }
      
    }//end class
}//end name space
