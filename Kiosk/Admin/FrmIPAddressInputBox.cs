using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Telerik.WinControls.UI;

namespace Kiosk
{
    public partial class FrmIPAddressInputBox : Form
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(uint vk, uint scan, uint flags, uint extraInfo);

        public string strIpAddress;
        string ctrlName;
        int fieldIndex = 0;

        public FrmIPAddressInputBox()
        {
            InitializeComponent();
        }

        public FrmIPAddressInputBox(Bitmap bitmap, string title, string ipAddress)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            this.lblTitle.Text = title;
            this.strIpAddress = ipAddress;
        }

        private void FrmIPAddressInputBox_Load(object sender, EventArgs e)
        {
            ipAddress.Text = strIpAddress;
        }

        private void btnKeyPad_Click(object sender, EventArgs e)
        {
            Control KeyButton = (Control)sender;
            ipAddress.Select(ipAddress.Text.Length, 0);
            switch (KeyButton.Name)
            {
                case "btnKeyPad_1":
                    keybd_event((byte)Keys.D1, 0, 0, 0);
                    keybd_event((byte)Keys.D1, 0, 0x02, 0);
                    break;

                case "btnKeyPad_2":
                    keybd_event((byte)Keys.D2, 0, 0, 0);
                    keybd_event((byte)Keys.D2, 0, 0x02, 0);
                    break;

                case "btnKeyPad_3":
                    keybd_event((byte)Keys.D3, 0, 0, 0);
                    keybd_event((byte)Keys.D3, 0, 0x02, 0);
                    break;

                case "btnKeyPad_4":
                    keybd_event((byte)Keys.D4, 0, 0, 0);
                    keybd_event((byte)Keys.D4, 0, 0x02, 0);
                    break;

                case "btnKeyPad_5":
                    keybd_event((byte)Keys.D5, 0, 0, 0);
                    keybd_event((byte)Keys.D5, 0, 0x02, 0);
                    break;

                case "btnKeyPad_6":
                    keybd_event((byte)Keys.D6, 0, 0, 0);
                    keybd_event((byte)Keys.D6, 0, 0x02, 0);
                    break;

                case "btnKeyPad_7":
                    keybd_event((byte)Keys.D7, 0, 0, 0);
                    keybd_event((byte)Keys.D7, 0, 0x02, 0);
                    break;

                case "btnKeyPad_8":
                    keybd_event((byte)Keys.D8, 0, 0, 0);
                    keybd_event((byte)Keys.D8, 0, 0x02, 0);
                    break;

                case "btnKeyPad_9":
                    keybd_event((byte)Keys.D9, 0, 0, 0);
                    keybd_event((byte)Keys.D9, 0, 0x02, 0);
                    break;

                case "btnKeyPad_0":
                    keybd_event((byte)Keys.D0, 0, 0, 0);
                    keybd_event((byte)Keys.D0, 0, 0x02, 0);
                    break;

                case "btnKeyPad_Bsp":
                    keybd_event((byte)Keys.Back, 0, 0, 0);
                    keybd_event((byte)Keys.Back, 0, 0x02, 0);
                    break;

                case "btnKeyPad_Ent":
                    keybd_event((byte)Keys.OemPeriod, 0, 0, 0);
                    keybd_event((byte)Keys.OemPeriod, 0, 0x02, 0);
                    break;
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

                case "picBxoConfirm":
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
                    if (ValidateCheck())
                    {
                        strIpAddress = ipAddress.Text;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;
            }
        }

        private bool ValidateCheck()
        {
            if (ipAddress.Text.Split('.').Length != 4)
            {
                FrmErrorBox errBox = new FrmErrorBox(UtilHelper.ScreenCapture(this.Bounds.Width, this.Bounds.Height, this.Location), "아이피 입력 오류", "정확한 아이피를 입력해 주십시오. \r\n ex)192.168.0.1");
                errBox.TopMost = true;
                if(errBox.ShowDialog() == DialogResult.OK)
                    return false;
            }

            foreach(string s in ipAddress.Text.Split('.'))
            {
                if(s.ToInteger() > 255)
                {
                    FrmErrorBox errBox = new FrmErrorBox(UtilHelper.ScreenCapture(this.Bounds.Width, this.Bounds.Height, this.Location), "아이피 입력 오류", "0부터 255사이의 숫자만 입력해 주십시오. \r\n ex)192.168.0.1");
                    errBox.TopMost = true;
                    if (errBox.ShowDialog() == DialogResult.OK)
                        return false;
                }
            }

            return true;
        }
    }
}
