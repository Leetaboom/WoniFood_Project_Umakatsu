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
    public partial class FrmAdmin : Form
    {
        string ctrlName = string.Empty;

        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            //picBoxStoreLogo.Left = (this.ClientSize.Width - picBoxStoreLogo.Width) / 2;
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;
            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "picBoxAdminClose":
                    bitmap = UtilHelper.ChangeOpacity(picBoxAdminClose.BackgroundImage, 1f);
                    picBoxAdminClose.BackgroundImage.Dispose();
                    picBoxAdminClose.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxPgClose":
                    bitmap = UtilHelper.ChangeOpacity(picBoxPgClose.BackgroundImage, 1f);
                    picBoxPgClose.BackgroundImage.Dispose();
                    picBoxPgClose.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxStoreRegi":
                    bitmap = UtilHelper.ChangeOpacity(picBoxStoreRegi.BackgroundImage, 1f);
                    picBoxStoreRegi.BackgroundImage.Dispose();
                    picBoxStoreRegi.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxHold":
                    bitmap = UtilHelper.ChangeOpacity(picBoxHold.BackgroundImage, 1f);
                    picBoxHold.BackgroundImage.Dispose();
                    picBoxHold.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxSales":
                    bitmap = UtilHelper.ChangeOpacity(picBoxSales.BackgroundImage, 1f);
                    picBoxSales.BackgroundImage.Dispose();
                    picBoxSales.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxSClose":
                    bitmap = UtilHelper.ChangeOpacity(picBoxSClose.BackgroundImage, 1f);
                    picBoxSClose.BackgroundImage.Dispose();
                    picBoxSClose.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxSetting":
                    bitmap = UtilHelper.ChangeOpacity(picBoxSetting.BackgroundImage, 1f);
                    picBoxSetting.BackgroundImage.Dispose();
                    picBoxSetting.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxCardCancel":
                    bitmap = UtilHelper.ChangeOpacity(picBoxCardCancel.BackgroundImage, 1f);
                    picBoxCardCancel.BackgroundImage.Dispose();
                    picBoxCardCancel.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (ctrlName)
            {
                case "picBoxAdminClose":
                    picBoxAdminClose.BackgroundImage.Dispose();
                    picBoxAdminClose.BackgroundImage = Properties.Resources.btn_back;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxPgClose":
                    picBoxPgClose.BackgroundImage.Dispose();
                    picBoxPgClose.BackgroundImage = Properties.Resources.btn_end;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxStoreRegi":
                    picBoxStoreRegi.BackgroundImage.Dispose();
                    picBoxStoreRegi.BackgroundImage = Properties.Resources.btn_storeregi1;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxHold":
                    picBoxHold.BackgroundImage.Dispose();
                    picBoxHold.BackgroundImage = Properties.Resources.btn_hold1;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxSales":
                    picBoxSales.BackgroundImage.Dispose();
                    picBoxSales.BackgroundImage = Properties.Resources.btn_Sales1;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxSClose":
                    picBoxSClose.BackgroundImage.Dispose();
                    picBoxSClose.BackgroundImage = Properties.Resources.btn_sclose1;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxSetting":
                    picBoxSetting.BackgroundImage.Dispose();
                    picBoxSetting.BackgroundImage = Properties.Resources.btn_setting1;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxCardCancel":
                    picBoxCardCancel.BackgroundImage.Dispose();
                    picBoxCardCancel.BackgroundImage = Properties.Resources.btn_cardCance;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

            }
        }

        private void MouseUp_Proc()
        {
            Form frm = null;

            switch (ctrlName)
            {
                case "picBoxAdminClose":
                    this.Dispose();
                    break;

                case "picBoxPgClose":
                    frm = new FrmMsgBox(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location), "프로그램 종료", "프로그램을", "정말로 종료하시겠습니까?");

                    frm.TopMost = true;

                    if (frm.ShowDialog() == DialogResult.OK)
                        this.DialogResult = DialogResult.Yes;
                    break;

                case "picBoxStoreRegi":
                    frm = new FrmStoreInfo(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location));

                    frm.TopMost = true;

                    frm.ShowDialog();
                    break;

                case "picBoxHold":
                    frm = new FrmHoldMsgBox(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location), StoreInfo.IsHoldMode);

                    frm.TopMost = true;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    break;

                case "picBoxSales":
                    switch(StoreInfo.VanSelect)
                    {
                        case "1":
                            break;
                        case "2":
                            frm = new FrmSalesSearchNice(UtilHelper.ScreenCapture(
                                this.Width, this.Height, this.Location));
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                        case "5":
                            frm = new FrmSalesSearchDaou(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location));
                            break;
                        case "6":
                            break;
                    }

                    frm.TopMost = true;

                    frm.ShowDialog();
                    break;

                case "picBoxSClose":
                    frm = new FrmKioskClose(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location));

                    frm.TopMost = true;

                    frm.ShowDialog();
                    break;

                case "picBoxSetting":
                    frm = new FrmKisokSetting(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location));

                    frm.TopMost = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                        this.DialogResult = DialogResult.OK;
                    break;

                case "picBoxCardCancel":
                    switch(StoreInfo.VanSelect)
                    {
                        case "1":
                            break;
                        case "2":
                            frm = new FrmCardCancelNice(UtilHelper.ScreenCapture(
                                this.Width, this.Height, this.Location));
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                        case "5":
                            frm = new FrmCardCancelDaou(UtilHelper.ScreenCapture(
                                this.Width, this.Height, this.Location));
                            break;
                        case "6":
                            break;
                    }

                    frm.TopMost = true;

                    frm.ShowDialog();
                    break;

            }
        }
    }
}
