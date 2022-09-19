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
    public partial class FrmErrorBox : Form
    {
        System.Timers.Timer waitTime = new System.Timers.Timer(1000);

        public FrmErrorBox()
        {
            InitializeComponent();
        }

        public FrmErrorBox(string title, string msg)
        {
            InitializeComponent();

            this.Size = new Size(900, 900);
            this.lblTitle.Text = title;
            this.lblMsg.Text = msg;

        }

        public FrmErrorBox(Bitmap bitmap, string title, string msg)
        {
            InitializeComponent();

            this.Size = bitmap.Size;
            this.BackgroundImage = bitmap;

            this.lblTitle.Text = title;
            this.lblMsg.Text = msg;
        }

        private void FrmErrorBox_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;
        }

        private void picBoxConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            UtilHelper.spWav.Play();

            picBoxConfirm.BackgroundImage = UtilHelper.ChangeOpacity(picBoxConfirm.BackgroundImage, 1f);
        }

        private void picBoxConfirm_MouseUp(object sender, MouseEventArgs e)
        {
            picBoxConfirm.BackgroundImage = Properties.Resources.btn_confirm;
            UtilHelper.Delay(100);
            this.DialogResult = DialogResult.OK;
        }
    }
}
