using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;

namespace StockPredictor.Tests
{
    class TradingTest
    {
        public void testTrade()
        {
            Trading tr = new Trading();
            tr.simulateTrade("ibb", false,false,0);
            tr.simulateTrade("ibb",true, false, 0);          
            tr.simulateTrade("ibb", true, true, 80.45m);
        }

    }
}
