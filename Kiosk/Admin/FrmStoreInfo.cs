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
    public partial class FrmStoreInfo : Form
    {
        DBControl.StoreDAO storeDao = new DBControl.StoreDAO();
        FrmKeyPad keyPad;

        string ctrlName = string.Empty;

        public FrmStoreInfo()
        {
            InitializeComponent();
        }

        public FrmStoreInfo(Bitmap bitmap)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
        }

        private void FrmStoreInfo_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            StoreInfoInit();
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;
            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "picBoxKBStore":
                    bitmap = UtilHelper.ChangeOpacity(picBoxKBStore.BackgroundImage, 1f);
                    picBoxKBStore.BackgroundImage.Dispose();
                    picBoxKBStore.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxKBSano":
                    bitmap = UtilHelper.ChangeOpacity(picBoxKBSano.BackgroundImage, 1f);
                    picBoxKBSano.BackgroundImage.Dispose();
                    picBoxKBSano.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxKBSang":
                    bitmap = UtilHelper.ChangeOpacity(picBoxKBSang.BackgroundImage, 1f);
                    picBoxKBSang.BackgroundImage.Dispose();
                    picBoxKBSang.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxKBAdd1":
                    bitmap = UtilHelper.ChangeOpacity(picBoxKBAdd1.BackgroundImage, 1f);
                    picBoxKBAdd1.BackgroundImage.Dispose();
                    picBoxKBAdd1.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxKBAdd2":
                    bitmap = UtilHelper.ChangeOpacity(picBoxKBAdd2.BackgroundImage, 1f);
                    picBoxKBAdd2.BackgroundImage.Dispose();
                    picBoxKBAdd2.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxKBPhone":
                    bitmap = UtilHelper.ChangeOpacity(picBoxKBPhone.BackgroundImage, 1f);
                    picBoxKBPhone.BackgroundImage.Dispose();
                    picBoxKBPhone.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxKBCeo":
                    bitmap = UtilHelper.ChangeOpacity(picBoxKBCeo.BackgroundImage, 1f);
                    picBoxKBCeo.BackgroundImage.Dispose();
                    picBoxKBCeo.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxKBAdminPw":
                    bitmap = UtilHelper.ChangeOpacity(picBoxKBAdminPw.BackgroundImage, 1f);
                    picBoxKBAdminPw.BackgroundImage.Dispose();
                    picBoxKBAdminPw.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

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
                case "picBoxKBStore":
                    picBoxKBStore.BackgroundImage.Dispose();
                    picBoxKBStore.BackgroundImage = Properties.Resources.keyboard;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxKBSano":
                    picBoxKBSano.BackgroundImage.Dispose();
                    picBoxKBSano.BackgroundImage = Properties.Resources.keyboard;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxKBSang":
                    picBoxKBSang.BackgroundImage.Dispose();
                    picBoxKBSang.BackgroundImage = Properties.Resources.keyboard;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxKBAdd1":
                    picBoxKBAdd1.BackgroundImage.Dispose();
                    picBoxKBAdd1.BackgroundImage = Properties.Resources.keyboard;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxKBAdd2":
                    picBoxKBAdd2.BackgroundImage.Dispose();
                    picBoxKBAdd2.BackgroundImage = Properties.Resources.keyboard;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxKBPhone":
                    picBoxKBPhone.BackgroundImage.Dispose();
                    picBoxKBPhone.BackgroundImage = Properties.Resources.keyboard;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxKBCeo":
                    picBoxKBCeo.BackgroundImage.Dispose();
                    picBoxKBCeo.BackgroundImage = Properties.Resources.keyboard;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxKBAdminPw":
                    picBoxKBAdminPw.BackgroundImage.Dispose();
                    picBoxKBAdminPw.BackgroundImage = Properties.Resources.keyboard;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

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
                case "picBoxKBStore":
                    txtBoxStoreName.Focus();
                    UtilHelper.Delay(50);
                    keyPad = new FrmKeyPad(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), "가맹점명을 입력하세요.");

                    keyPad.TopMost = true;

                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxStoreName.Text = keyPad.txtBoxKeyValue.Text;
                        txtBoxStoreName.Select(txtBoxStoreName.Text.Length, 0);
                    }
                    break;

                case "picBoxKBSano":
                    txtBoxSano.Focus();
                    UtilHelper.Delay(50);
                    keyPad = new FrmKeyPad(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), "사업자번호를 입력하세요.");

                    keyPad.TopMost = true;

                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxSano.Text = keyPad.txtBoxKeyValue.Text;
                        txtBoxSano.Select(txtBoxSano.Text.Length, 0);
                    }
                    break;

                case "picBoxKBSang":
                    txtBoxSang.Focus();
                    UtilHelper.Delay(50);
                    keyPad = new FrmKeyPad(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), "상호를 입력하세요.");

                    keyPad.TopMost = true;

                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxSang.Text = keyPad.txtBoxKeyValue.Text;
                        txtBoxSang.Select(txtBoxSang.Text.Length, 0);
                    }
                    break;

                case "picBoxKBAdd1":
                    txtBoxAdd1.Focus();
                    UtilHelper.Delay(50);
                    keyPad = new FrmKeyPad(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), "주소를 입력하세요.");

                    keyPad.TopMost = true;

                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxAdd1.Text = keyPad.txtBoxKeyValue.Text;
                        txtBoxAdd1.Select(txtBoxAdd1.Text.Length, 0);
                    }
                    break;

                case "picBoxKBAdd2":
                    txtBoxAdd2.Focus();
                    UtilHelper.Delay(50);
                    keyPad = new FrmKeyPad(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), "상세 주소를 입력하세요.");

                    keyPad.TopMost = true;

                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxAdd2.Text = keyPad.txtBoxKeyValue.Text;
                        txtBoxAdd2.Select(txtBoxAdd2.Text.Length, 0);
                    }
                    break;

                case "picBoxKBPhone":
                    txtBoxPhon.Focus();
                    UtilHelper.Delay(50);
                    keyPad = new FrmKeyPad(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), "연락처를 입력하세요.");

                    keyPad.TopMost = true;

                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxPhon.Text = keyPad.txtBoxKeyValue.Text;
                        txtBoxPhon.Select(txtBoxPhon.Text.Length, 0);
                    }
                    break;

                case "picBoxKBCeo":
                    txtBoxCeo.Focus();
                    UtilHelper.Delay(50);
                    keyPad = new FrmKeyPad(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), "대표자명을 입력하세요.");

                    keyPad.TopMost = true;

                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxCeo.Text = keyPad.txtBoxKeyValue.Text;
                        txtBoxCeo.Select(txtBoxCeo.Text.Length, 0);
                    }
                    break;

                case "picBoxKBAdminPw":
                    txtBoxAdminPw.Focus();
                    UtilHelper.Delay(50);
                    keyPad = new FrmKeyPad(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), "관리자 비밀번호를 입력하세요.");

                    keyPad.TopMost = true;

                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxAdminPw.Text = keyPad.txtBoxKeyValue.Text;
                        txtBoxAdminPw.Select(txtBoxAdminPw.Text.Length, 0);
                    }
                    break;

                case "picBoxCancel":
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case "picBoxConfirm":
                    this.DialogResult = DialogResult.OK;
                    break;
            }
        }

        private void StoreInfoInit()
        {
            txtBoxBrand.Text = StoreInfo.BrnadCode;
            txtBoxStoreCode.Text = StoreInfo.StoreCode;
            txtBoxStoreName.Text = StoreInfo.StoreName;
            txtBoxDesk.Text = StoreInfo.StoreDesk;
            txtBoxSano.Text = StoreInfo.StoreSano;
            txtBoxSang.Text = StoreInfo.StoreSang;
            txtBoxAdd1.Text = StoreInfo.StoreAdd1;
            txtBoxAdd2.Text = StoreInfo.StoreAdd2;
            txtBoxPhon.Text = StoreInfo.StorePhon;
            txtBoxCeo.Text = StoreInfo.StoreCeo;
            txtBoxAdminPw.Text = "";
        }
    }
}
