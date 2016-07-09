using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockPredictor.Helpers;
using StockPredictor.Tests;

namespace StockPredictor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Test_Click(object sender, EventArgs e)
        {
           // PosTaggerTest pt = new PosTaggerTest();
            SpellCheckTest sct = new SpellCheckTest();
           // pt.testTagger();
            sct.spellCheckArticleTest();
           
        }
    }
}
