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
    public partial class FrmMsgBox : Form
    {
        string ctrlName = string.Empty;

        public FrmMsgBox()
        {
            InitializeComponent();
        }

        public FrmMsgBox(Bitmap bitmap, string title, string msg1, string msg2)
        {
            InitializeComponent();

            this.BackgroundImage = bitmap;
            lblTitle.Text = title;
            lblMsg1.Text = msg1;
            lblMsg2.Text = msg2;
        }
        private void FrmMsgBox_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;
            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "picBoxCancel":
                    bitmap = UtilHelper.ChangeOpacity(picBoxCancel.BackgroundImage, 1f);
                    picBoxCancel.BackgroundImage.Dispose();
                    picBoxCancel.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxContinue":
                    bitmap = UtilHelper.ChangeOpacity(picBoxContinue.BackgroundImage, 1f);
                    picBoxContinue.BackgroundImage.Dispose();
                    picBoxContinue.BackgroundImage = bitmap;
                    bitmap = null;
                    break;
            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (ctrlName)
            {
                case "picBoxCancel":
                    picBoxCancel.BackgroundImage.Dispose();
                    picBoxCancel.BackgroundImage = Properties.Resources.btn_cancel;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxContinue":
                    picBoxContinue.BackgroundImage.Dispose();
                    picBoxContinue.BackgroundImage = Properties.Resources.btn_confirm;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;
            }
        }

        private void MouseUp_Proc()
        {
            switch (ctrlName)
            {
                case "picBoxCancel":
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case "picBoxContinue":
                    this.DialogResult = DialogResult.OK;
                    break;
            }
        }
    }
}
