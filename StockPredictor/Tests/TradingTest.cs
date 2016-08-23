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
            tr.autoTrade("BIIB",true, 300);
            //tr.simulateTradeMaster("ibb", false,false,0,true,false,false,false);
            //tr.simulateTradeMaster("ibb",true, false, 0,false,true,false,false);
            //tr.simulateTradeMaster("ibb", true, true, 80.45m,false,false,true, true);
        }

    }
}
