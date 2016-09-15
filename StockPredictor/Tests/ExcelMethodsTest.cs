using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;
using System.IO;

namespace StockPredictor.Tests
{
    class ExcelMethodsTest
    {
       public void testSaveDataToExcel()
        {
            fileReaderWriter frw = new fileReaderWriter();
            ExcelMethods em = new ExcelMethods();
            em.savePredictorDataToExcel(null, "exampleExcel", "test", "200",0, 1, 2, 3, 4,
         5, 6,
         7, 8,
          9, 10, 2);         
        }
    }
}
