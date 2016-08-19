using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    public abstract class AbstractTextWriter : TextWriter
    {
        protected abstract void WriteString(string value);
        public override Encoding Encoding { get { return Encoding.Unicode; } }
        public override void Write(char[] buffer, int index, int count)
        {
            WriteString(new string(buffer, index, count));
        }
        public override void Write(char value)
        {
            WriteString(value.ToString(FormatProvider));
        }
        public override void Write(string value) { WriteString(value); }
        //subclasses might override Flush, Dispose and Encoding
    }
}
