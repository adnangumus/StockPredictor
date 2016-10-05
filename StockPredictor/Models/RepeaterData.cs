using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Models
{
    public class RepeaterData
    {
        //variables used for the repeating class
        public bool IsShortSale { get; set; }
        public bool IsBuy { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public List<string> LinksOld { get; set; }
        public bool RepeaterIsRunning { get; set; }

    }
}
