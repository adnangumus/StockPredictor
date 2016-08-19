using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace StockPredictor
{
    public partial class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {
            InitializeComponent();
            //SetStyle(ControlStyles.SupportsTransparentBackColor |
            //         ControlStyles.OptimizedDoubleBuffer |
            //         ControlStyles.AllPaintingInWmPaint |
            //         ControlStyles.ResizeRedraw |
            //         ControlStyles.UserPaint, true);
            //BackColor = Color.Transparent;
            base.AutoSize = false;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            
           
                SizeF size = e.Graphics.MeasureString(Text, Font);
                if (ClientSize.Width < (int)size.Width + 1 || ClientSize.Width > (int)size.Width + 1 ||
                ClientSize.Height < (int)size.Height + 1 || ClientSize.Height > (int)size.Height + 1)
                {
                    // need resizing
                    ClientSize = new Size((int)size.Width + 10, (int)size.Height + 10);
                    return;
                }
            
            using (LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, Color.Black, Color.LightGray, LinearGradientMode.ForwardDiagonal))
                e.Graphics.DrawString(Text, Font, brush, ClientRectangle);
        }
    }
}
