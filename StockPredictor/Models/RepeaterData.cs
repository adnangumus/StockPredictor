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
        public bool IsStrong { get; set; }
        public double PositionOpenPrice { get; set; }
        public double PositionClosePrice { get; set; }
     
    }
}
