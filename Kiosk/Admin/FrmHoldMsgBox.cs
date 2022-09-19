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
    public partial class FrmHoldMsgBox : Form
    {
        string ctrlName = string.Empty;
        bool isHoldMode;

        public FrmHoldMsgBox()
        {
            InitializeComponent();
        }

        public FrmHoldMsgBox(Bitmap bitmap, bool isHoldMode)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            this.isHoldMode = isHoldMode;
        }


        private void FrmHoldMsgBox_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            if (isHoldMode)
            {
                lblMsg1.Text = "키오스크 사용을 재개하고";
                lblMsg2.Text = "HOLD 모드를 종료 합니다.";
            }
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
                    if (isHoldMode)
                    {
                        UtilHelper.SetIniValue("HOLDMODE", "FLAG", "0", MainInit.iniPath);

                        StoreInfo.IsHoldMode = false;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        UtilHelper.SetIniValue("HOLDMODE", "FLAG", "1", MainInit.iniPath);
                        StoreInfo.IsHoldMode = true;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;
            }
        }
    }
}
