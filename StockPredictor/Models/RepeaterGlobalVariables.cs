using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Models
{
    public class RepeaterGlobalVariables
    {
        public List<string> LinksOld { get; set; }
        public double OpenPrice { get; set; }
        public double CurrentPrice { get; set; }
        public double ClosePrice { get; set; }
        public bool RepeaterIsRunning { get; set; }

       
    }
}
