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
    public partial class FrmNetworkErr : Form
    {
        public FrmNetworkErr()
        {
            InitializeComponent();

            this.Size = new Size(900, 900);
        }

        public FrmNetworkErr(Bitmap bitmap)
        {
            this.Size = bitmap.Size;
            this.BackgroundImage = bitmap;
        }
        private void FrmNetworkErr_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;
        }

        private void picBoxCconfirm_MouseDown(object sender, MouseEventArgs e)
        {
            UtilHelper.spWav.Play();

            picBoxCconfirm.BackgroundImage = UtilHelper.ChangeOpacity(picBoxCconfirm.BackgroundImage, 1f);
        }

        private void picBoxCconfirm_MouseUp(object sender, MouseEventArgs e)
        {
            picBoxCconfirm.BackgroundImage = Properties.Resources.btn_confirm;
            UtilHelper.Delay(100);

            this.DialogResult = DialogResult.OK;
        }
    }
}
