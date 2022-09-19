using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Kiosk
{
    public partial class FrmIpPortInputBox : Form
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(uint vk, uint scan, uint flags, uint extraInfo);

        public string strPort;
        string ctrlName;

        public FrmIpPortInputBox()
        {
            InitializeComponent();
        }

        public FrmIpPortInputBox(Bitmap bitmap, string title, string port)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            lblTitle.Text = title;
            strPort = port;
        }

        private void FrmIpPortInputBox_Load(object sender, EventArgs e)
        {
            txtBoxPort.Text = strPort;
        }

        private void btnKeyPad_Click(object sender, EventArgs e)
        {
            Control KeyButton = (Control)sender;
            txtBoxPort.Select(txtBoxPort.Text.Length, 0);
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
                    strPort = txtBoxPort.Text;
                    this.DialogResult = DialogResult.OK;
                    break;
            }
        }

        private void txtBoxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
                e.Handled = true;
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
                    if (ValidateCheck())
                    {
                        strPort = txtBoxPort.Text;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;
            }
        }

        private bool ValidateCheck()
        {
            FrmErrorBox errBox;
            if(txtBoxPort.Text.Length == 0)
            {
                errBox = new FrmErrorBox(UtilHelper.ScreenCapture(this.Bounds.Width, this.Bounds.Height, this.Location), "포트번호 입력 오류", "0과 65535 사이의 숫자를 입력해 주십시오.");
                errBox.TopMost = true;
                if(errBox.ShowDialog() == DialogResult.OK)
                    return false;
            }

            if(txtBoxPort.Text.ToInteger() > 65535)
            {
                errBox = new FrmErrorBox(UtilHelper.ScreenCapture(this.Bounds.Width, this.Bounds.Height, this.Location), "포트번호 입력 오류", "포트번호는 65535를 초과할 수 없습니다.");
                errBox.TopMost = true;
                if (errBox.ShowDialog() == DialogResult.OK)
                    return false;
            }

            return true;
        }
    }
}
