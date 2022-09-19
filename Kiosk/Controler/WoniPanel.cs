using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk.Controler
{
    public partial class WoniPanel : DevComponents.DotNetBar.PanelEx
    {
        public WoniPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Selectable | ControlStyles.FixedHeight, false);
            SetStyle(ControlStyles.ResizeRedraw, true);
            TabStop = false;
            BackColor = System.Drawing.Color.White;
            UpdateStyles();
        }
    }
}
