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
    public partial class FrmLoadingBox : Form
    {
        public FrmLoadingBox()
        {
            InitializeComponent();
        }

        private void FrmLoadingBox_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;
        }
    }
}
