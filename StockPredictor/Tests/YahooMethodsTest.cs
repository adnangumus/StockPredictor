using StockPredictor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Tests
{
    class YahooMethodsTest
    {
        //test the method to get stock prices
        public void testGetStockPrices() {
        YahooStockMethods ysm = new YahooStockMethods();
            ysm.getBioStockPrices();
                 }
    }
}
