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
    public partial class FrmOrderCancelBox : Form
    {
        string ctrlName = string.Empty;

        public FrmOrderCancelBox()
        {
            InitializeComponent();
        }

        public FrmOrderCancelBox(Bitmap bitmap)
        {
            InitializeComponent();

            this.BackgroundImage = bitmap;
            lblProductName.Text = "모든";
            lblMent.Text = "상품을 취소 합니다.";
        }

        public FrmOrderCancelBox(Bitmap bitmap, string productName)
        {
            InitializeComponent();

            this.BackgroundImage = bitmap;
            lblProductName.Text = productName;
        }


        private void FrmOrderCancelBox_Load(object sender, EventArgs e)
        {
            woniPanel1.Left = (this.ClientSize.Width - woniPanel1.Width) / 2;
            woniPanel1.Top = (this.ClientSize.Height - woniPanel1.Height) / 2;
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            Bitmap bitmap;

            ctrlName = ((Control)sender).Name;

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
                    BtnEventProc();

                    break;

                case "picBoxContinue":
                    picBoxContinue.BackgroundImage.Dispose();
                    picBoxContinue.BackgroundImage = Properties.Resources.btn_continue;
                    UtilHelper.Delay(50);
                    BtnEventProc();
                    break;
            }

        }

        private void BtnEventProc()
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
