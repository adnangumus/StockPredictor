﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    public class DelegateTextWriter : AbstractTextWriter
    {
        readonly Action<string> OnWrite;
        readonly Action OnClose;
        static void NullOp() { }
        public DelegateTextWriter(Action<string> onWrite, Action onClose = null)
        {
            OnWrite = onWrite;
            OnClose = onClose ?? NullOp;
        }
        protected override void WriteString(string value) { OnWrite(value); }
        protected override void Dispose(bool disposing)
        {
            OnClose(); base.Dispose(disposing);
        }
    }
}
