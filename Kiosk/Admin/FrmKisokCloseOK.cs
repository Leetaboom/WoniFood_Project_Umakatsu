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
    public partial class FrmKisokCloseOK : Form
    {
        string ctrlName = string.Empty;

        public FrmKisokCloseOK(Bitmap bitmap)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
        }

        public FrmKisokCloseOK(Bitmap bitmap, string title, string ment1, string ment2)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            this.lblTitle.Text = title;
            this.lblMsg1.Text = ment1;
            this.lblMsg2.Text = ment2;
        }

        private void FrmKisokCloseOK_Load(object sender, EventArgs e)
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

                case "picBoxConfirm":
                    bitmap = UtilHelper.ChangeOpacity(picBoxConfirm.BackgroundImage, 1f);
                    picBoxConfirm.BackgroundImage.Dispose();
                    picBoxConfirm.BackgroundImage = bitmap;
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

                case "picBoxConfirm":
                    picBoxConfirm.BackgroundImage.Dispose();
                    picBoxConfirm.BackgroundImage = Properties.Resources.btn_confirm;
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

                case "picBoxConfirm":
                    this.DialogResult = DialogResult.OK;
                    break;

            }
        }

        private void CardApp(object sender)
        {
            try
            {
                //UtilHelper.cardInfo.URL = @"System\wav\card.mp3";
                //UtilHelper.cardInfo.controls.play();
            }
            catch
            {
                this.DialogResult = DialogResult.Cancel;
            }
            finally
            {

            }
        }
    }
}
