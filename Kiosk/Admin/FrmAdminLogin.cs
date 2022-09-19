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
    public partial class FrmAdminLogin : Form
    {
        DBControl.StoreDAO storeDao = new DBControl.StoreDAO();

        string ctrlName = string.Empty;

        public FrmAdminLogin()
        {
            InitializeComponent();
        }

        public FrmAdminLogin(Bitmap bitmap)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;

            storeDao.BRAND = StoreInfo.BrnadCode;
            storeDao.STORE = StoreInfo.StoreCode;
            storeDao.DESK = StoreInfo.StoreDesk;
        }

        private void FrmAdminLogin_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;
        }

        private void txtBoxAdminPw_TextChanged(object sender, EventArgs e)
        {
            txtBoxAdminPw.SelectionStart = txtBoxAdminPw.Text.Length;
            txtBoxAdminPw.ScrollToCaret();
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;

            UtilHelper.spWav.Play();

            switch(ctrlName)
            {
                case "picBoxCancel":
                    bitmap = UtilHelper.ChangeOpacity(picBoxCancel.BackgroundImage, 1f);
                    picBoxCancel.BackgroundImage.Dispose();
                    picBoxCancel.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxOK":
                    bitmap = UtilHelper.ChangeOpacity(picBoxOK.BackgroundImage, 1f);
                    picBoxOK.BackgroundImage.Dispose();
                    picBoxOK.BackgroundImage = bitmap;
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
                    picBoxCancel.BackgroundImage = Properties.Resources.btn_back;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxOK":
                    picBoxOK.BackgroundImage.Dispose();
                    picBoxOK.BackgroundImage = Properties.Resources.btn_confirm;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;
            }
        }

        private void btnKeyPad_Click(object sender, EventArgs e)
        {
            UtilHelper.spWav.Play();

            switch(((Control)sender).Name)
            {
                case "btnKeyPad_1":
                    txtBoxAdminPw.Text += "1";
                    break;

                case "btnKeyPad_2":
                    txtBoxAdminPw.Text += "2";
                    break;

                case "btnKeyPad_3":
                    txtBoxAdminPw.Text += "3";
                    break;

                case "btnKeyPad_4":
                    txtBoxAdminPw.Text += "4";
                    break;

                case "btnKeyPad_5":
                    txtBoxAdminPw.Text += "5";
                    break;

                case "btnKeyPad_6":
                    txtBoxAdminPw.Text += "6";
                    break;

                case "btnKeyPad_7":
                    txtBoxAdminPw.Text += "7";
                    break;

                case "btnKeyPad_8":
                    txtBoxAdminPw.Text += "8";
                    break;

                case "btnKeyPad_9":
                    txtBoxAdminPw.Text += "9";
                    break;

                case "btnKeyPad_0":
                    txtBoxAdminPw.Text += "0";
                    break;

                case "btnKeyPad_Bsp":
                    if (txtBoxAdminPw.TextLength != 0)
                        txtBoxAdminPw.Text = txtBoxAdminPw.Text.Substring(0, txtBoxAdminPw.Text.Length - 1);
                    break;

                case "btnKeyPad_Ent":
                    if (storeDao.IsStoreLoginCheck(txtBoxAdminPw.Text))
                    {
                        storeDao = null;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        dbpMain.Visible = false;
                        FrmErrorBox errBox = new FrmErrorBox("알림", "비밀번호가 틀립니다.");

                        if (errBox.ShowDialog() == DialogResult.OK)
                        {
                            dbpMain.Visible = true;
                            txtBoxAdminPw.Text = string.Empty;
                            txtBoxAdminPw.Focus();
                        }
                    }
                    break;
            }
        }

        private void MouseUp_Proc()
        {
            switch (ctrlName)
            {
                case "picBoxCancel":
                    this.Dispose();
                    break;

                case "picBoxOK":
                    if (storeDao.IsStoreLoginCheck(txtBoxAdminPw.Text))
                    {
                        storeDao = null;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        dbpMain.Visible = false;
                        FrmErrorBox errBox = new FrmErrorBox("알림", "비밀번호가 틀립니다.");

                        if (errBox.ShowDialog() == DialogResult.OK)
                        {
                            dbpMain.Visible = true;
                            txtBoxAdminPw.Text = string.Empty;
                            txtBoxAdminPw.Focus();
                        }
                    }
                    break;
            }
        }
    }
}
