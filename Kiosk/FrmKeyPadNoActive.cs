using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class FrmKeyPadNoActive : Form
    {
        private const long WS_EX_NOACTIVATE = 0x8000000L;

        private const int WM_NCMOUSEMOVE = 0xA0;
        private const int WM_NCLBUTTONDOWN = 0xA1;

        private IntPtr previousForegroundWindow = IntPtr.Zero;

        public FrmKeyPadNoActive()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | (int)WS_EX_NOACTIVATE;

                return cp;
            }
        }
    }
}
