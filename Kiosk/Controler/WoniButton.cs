using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Kiosk.Controler
{
    public partial class WoniButton : DevComponents.DotNetBar.ButtonX
    {
        public WoniButton()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, false);
            this.SetStyle(System.Windows.Forms.ControlStyles.StandardClick | System.Windows.Forms.ControlStyles.StandardDoubleClick, false);
        }
    }
}
