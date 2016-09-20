using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;
using System.IO;
using Microsoft.Office.Interop.Excel;


namespace StockPredictor.Tests
{
    class ExcelMethodsTest
    {
       public void testSaveDataToExcel()
        {
           
            ExcelMethods em = new ExcelMethods();
            em.savePredictorDataToExcel(null, "exampleExcel", "test", "200",0, 1, 2, 3, 4,
         5, 6,
         7, 8,
          9, 10, 2);         
        }

        public void testLongTrades()
        {
            //start the excel application object
            ExcelMethods exl = new ExcelMethods();
            Application myPassedExcelApplication = exl.startExcelApp();

            ExcelMethods em = new ExcelMethods();
          //  em.saveLongHoldTrade( myPassedExcelApplication, "test", "testExample", "10000", "100", "", false, "", false);
            em.saveLongHoldTrade(myPassedExcelApplication, "test", "testExample", "9880", "", "98", true, "-2", false);
        }
    }
}
