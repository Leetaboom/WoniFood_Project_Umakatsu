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
    public partial class FrmKisokSetting : Form
    {
        List<string> lstPort = new List<string>();
        string iniPath = UtilHelper.Root + @"\System\conf\Setting.ini";

        string ctrlName = string.Empty;

        int nVanCheck = -1;
        bool isSound;

        public FrmKisokSetting()
        {
            InitializeComponent();
        }

        public FrmKisokSetting(Bitmap bitmap)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
        }

        private void FrmKisokSetting_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            ComboBoxPortAdd();
            ComboBoxBaudRateAdd();

            KioskLoadSetting();
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

        private void picBoxCheck_Click(object sender, EventArgs e)
        {
            if (isSound)
                UtilHelper.spWav.Play();

            switch (((Control)sender).Name)
            {
                
                case "picBoxKoces":
                    PicBoxVanCheckInit();
                    nVanCheck = 1;
                    picBoxKoces.BackgroundImage = Properties.Resources.btn_radio_over;
                    break;

                case "picBoxNice":
                    PicBoxVanCheckInit();
                    nVanCheck = 2;
                    picBoxNice.BackgroundImage = Properties.Resources.btn_radio_over;
                    break;

                case "picBoxKis":
                    PicBoxVanCheckInit();
                    nVanCheck = 3;
                    picBoxKis.BackgroundImage = Properties.Resources.btn_radio_over;
                    break;

                case "picBoxJtnet":
                    PicBoxVanCheckInit();
                    nVanCheck = 4;
                    picBoxJtnet.BackgroundImage = Properties.Resources.btn_radio_over;
                    break;

                case "picBoxDaou":
                    PicBoxVanCheckInit();
                    nVanCheck = 5;
                    picBoxDaou.BackgroundImage = Properties.Resources.btn_radio_over;
                    break;

                case "picBoxKicc":
                    PicBoxVanCheckInit();
                    nVanCheck = 6;
                    picBoxKicc.BackgroundImage = Properties.Resources.btn_radio_over;
                    break;
            }
        }

        private void ipAddressCounter_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(dbpMain.Width, dbpMain.Height);
            dbpMain.DrawToBitmap(bitmap, new Rectangle(0, 0, dbpMain.Width, dbpMain.Height));

            FrmIPAddressInputBox ipAdd = new FrmIPAddressInputBox(UtilHelper.ChangeOpacity(bitmap, 1f),
                "카운터프린터 IP를 입력하세요", ipAddressCounter.Text);

            ipAdd.TopMost = true;

            if (ipAdd.ShowDialog() == DialogResult.OK)
            {
                ipAddressCounter.Text = ipAdd.strIpAddress;
            }
        }

        private void ipAddressKitchen_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(dbpMain.Width, dbpMain.Height);
            dbpMain.DrawToBitmap(bitmap, new Rectangle(0, 0, dbpMain.Width, dbpMain.Height));

            FrmIPAddressInputBox ipAdd = new FrmIPAddressInputBox(UtilHelper.ChangeOpacity(bitmap, 1f),
                "주방프린터1 IP를 입력하세요", ipAddressKitchen.Text);

            ipAdd.TopMost = true;

            if (ipAdd.ShowDialog() == DialogResult.OK)
            {
                ipAddressKitchen.Text = ipAdd.strIpAddress;
            }
        }

        private void ipAddressKitchen1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(dbpMain.Width, dbpMain.Height);
            dbpMain.DrawToBitmap(bitmap, new Rectangle(0, 0, dbpMain.Width, dbpMain.Height));

            FrmIPAddressInputBox ipAdd = new FrmIPAddressInputBox(UtilHelper.ChangeOpacity(bitmap, 1f),
                "주방프린터2 IP를 입력하세요", ipAddressKitchen1.Text);

            ipAdd.TopMost = true;

            if (ipAdd.ShowDialog() == DialogResult.OK)
            {
                ipAddressKitchen1.Text = ipAdd.strIpAddress;
            }
        }

        private void txtBoxCounterPort_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(dbpMain.Width, dbpMain.Height);
            dbpMain.DrawToBitmap(bitmap, new Rectangle(0, 0, dbpMain.Width, dbpMain.Height));

            FrmIpPortInputBox ipPort = new FrmIpPortInputBox(UtilHelper.ChangeOpacity(bitmap, 1f),
                "카운터프린터 Port를 입력하세요", txtBoxCounterPort.Text);

            ipPort.TopMost = true;

            if (ipPort.ShowDialog() == DialogResult.OK)
            {
                txtBoxCounterPort.Text = ipPort.strPort;
            }
        }

        private void txtBoxKitchenPort_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(dbpMain.Width, dbpMain.Height);
            dbpMain.DrawToBitmap(bitmap, new Rectangle(0, 0, dbpMain.Width, dbpMain.Height));

            FrmIpPortInputBox ipPort = new FrmIpPortInputBox(UtilHelper.ChangeOpacity(bitmap, 1f),
                "주방프린터1 Port를 입력하세요", txtBoxKitchenPort.Text);

            ipPort.TopMost = true;

            if (ipPort.ShowDialog() == DialogResult.OK)
            {
                txtBoxKitchenPort.Text = ipPort.strPort;
            }
        }

        private void txtBoxKitchenPort1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(dbpMain.Width, dbpMain.Height);
            dbpMain.DrawToBitmap(bitmap, new Rectangle(0, 0, dbpMain.Width, dbpMain.Height));

            FrmIpPortInputBox ipPort = new FrmIpPortInputBox(UtilHelper.ChangeOpacity(bitmap, 1f),
                "주방프린터2 Port를 입력하세요", txtBoxKitchenPort1.Text);

            ipPort.TopMost = true;

            if (ipPort.ShowDialog() == DialogResult.OK)
            {
                txtBoxKitchenPort1.Text = ipPort.strPort;
            }
        }

        private void txtBoxTid_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(dbpMain.Width, dbpMain.Height);
            dbpMain.DrawToBitmap(bitmap, new Rectangle(0, 0, dbpMain.Width, dbpMain.Height));

            FrmIpPortInputBox tid = new FrmIpPortInputBox(UtilHelper.ChangeOpacity(bitmap, 1f),
                "단말기번호를 입력하세요", txtBoxTid.Text);

            tid.TopMost = true;

            if (tid.ShowDialog() == DialogResult.OK)
            {
                txtBoxTid.Text = tid.strPort;
            }
        }

        private void txtBoxSvrPort_Click(object sender, EventArgs e)
        {

            Bitmap bitmap = new Bitmap(dbpMain.Width, dbpMain.Height);
            dbpMain.DrawToBitmap(bitmap, new Rectangle(0, 0, dbpMain.Width, dbpMain.Height));

            FrmIpPortInputBox svrPort = new FrmIpPortInputBox(UtilHelper.ChangeOpacity(bitmap, 1f),
                "서버포트번호를 입력하세요", txtBoxSvrPort.Text);

            svrPort.TopMost = true;

            if (svrPort.ShowDialog() == DialogResult.OK)
            {
                txtBoxSvrPort.Text = svrPort.strPort;
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
                    KioskSettingSave();
                    break;
            }
        }

        private void ComboBoxPortAdd()
        {
            lstPort = UtilHelper.GetConnectComDevice();

            for (int i = 0; i < lstPort.Count; i++)
            {
                comboBoxCounter.Items.Add(lstPort[i]);
                comboBoxKitchen1.Items.Add(lstPort[i]);
                comboBoxKitchen2.Items.Add(lstPort[i]);
                comboBoxReceipt.Items.Add(lstPort[i]);
            }
        }

        private void ComboBoxBaudRateAdd()
        {
            comboBoxRecBaudRate.Items.Add("9600");
            comboBoxRecBaudRate.Items.Add("19200");
            comboBoxRecBaudRate.Items.Add("115200");

            comboBoxCountBaudRate.Items.Add("9600");
            comboBoxCountBaudRate.Items.Add("19200");
            comboBoxCountBaudRate.Items.Add("115200");

            comboBoxKitc1BaudRate.Items.Add("9600");
            comboBoxKitc1BaudRate.Items.Add("19200");
            comboBoxKitc1BaudRate.Items.Add("115200");

            comboBoxKitc2BaudRate.Items.Add("9600");
            comboBoxKitc2BaudRate.Items.Add("19200");
            comboBoxKitc2BaudRate.Items.Add("115200");
        }

        private void PicBoxVanCheckInit()
        {
            nVanCheck = -1;
            picBoxKoces.BackgroundImage.Dispose();
            picBoxNice.BackgroundImage.Dispose();
            picBoxKis.BackgroundImage.Dispose();
            picBoxJtnet.BackgroundImage.Dispose();
            picBoxDaou.BackgroundImage.Dispose();
            picBoxKicc.BackgroundImage.Dispose();

            picBoxKoces.BackgroundImage = Properties.Resources.btn_radio;
            picBoxNice.BackgroundImage = Properties.Resources.btn_radio;
            picBoxKis.BackgroundImage = Properties.Resources.btn_radio;
            picBoxJtnet.BackgroundImage = Properties.Resources.btn_radio;
            picBoxDaou.BackgroundImage = Properties.Resources.btn_radio;
            picBoxKicc.BackgroundImage = Properties.Resources.btn_radio;
        }

        private void KioskLoadSetting()
        {
            comboBoxKitchen1.Text = StoreInfo.Kitchen1Prn;
            comboBoxKitchen2.Text = StoreInfo.Kitchen2Prn;
            comboBoxReceipt.Text = StoreInfo.ReceiptPrn;
            comboBoxCounter.Text = StoreInfo.CounterPrn;
            comboBoxKitc1BaudRate.Text = StoreInfo.Kitchen1Rate.ToString();
            comboBoxKitc2BaudRate.Text = StoreInfo.Kitchen2Rate.ToString();
            comboBoxRecBaudRate.Text = StoreInfo.ReceiptRate.ToString();
            comboBoxCountBaudRate.Text = StoreInfo.CounterRate.ToString();
            comboBoxCType.Text = StoreInfo.CounterType;
            comboBoxK1Type.Text = StoreInfo.Kitchen1Type;
            comboBoxK2Type.Text = StoreInfo.Kitchen2Type;
            ipAddressCounter.Text = StoreInfo.CounterPrnIP1;
            txtBoxCounterPort.Text = StoreInfo.CounterPrnPort1.ToString();
            ipAddressKitchen.Text = StoreInfo.KitchenPrnIP1;
            txtBoxKitchenPort.Text = StoreInfo.KitchenPrnPort1.ToString();
            ipAddressKitchen1.Text = StoreInfo.KitchenPrnIP2;
            txtBoxKitchenPort1.Text = StoreInfo.KitchenPrnPort2.ToString();
            txtBoxTid.Text = StoreInfo.Tid;
            txtBoxSvrPort.Text = StoreInfo.Port;

            switch (StoreInfo.VanSelect)
            {
                case "1":
                    isSound = true;
                    picBoxCheck_Click(picBoxKoces, null);
                    break;

                case "2":
                    isSound = true;
                    picBoxCheck_Click(picBoxNice, null);
                    break;

                case "3":
                    isSound = true;
                    picBoxCheck_Click(picBoxKis, null);
                    break;

                case "4":
                    isSound = true;
                    picBoxCheck_Click(picBoxJtnet, null);
                    break;

                case "5":
                    isSound = true;
                    picBoxCheck_Click(picBoxDaou, null);
                    break;

                case "6":
                    isSound = true;
                    picBoxCheck_Click(picBoxKicc, null);
                    break;
            }
        }

        private void KioskSettingSave()
        {
            switch (nVanCheck)
            {
                case 1:
                    UtilHelper.SetIniValue("VAN", "FLAG", "1", iniPath);
                    break;

                case 2:
                    UtilHelper.SetIniValue("VAN", "FLAG", "2", iniPath);
                    break;

                case 3:
                    UtilHelper.SetIniValue("VAN", "FLAG", "3", iniPath);
                    break;

                case 4:
                    UtilHelper.SetIniValue("VAN", "FLAG", "4", iniPath);
                    break;

                case 5:
                    UtilHelper.SetIniValue("VAN", "FLAG", "5", iniPath);
                    break;

                case 6:
                    UtilHelper.SetIniValue("VAN", "FLAG", "6", iniPath);
                    break;
            }

            UtilHelper.SetIniValue("PRINT", "KITCHEN1PRN", comboBoxKitchen1.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHEN2PRN", comboBoxKitchen2.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "RECEIPTPRN", comboBoxReceipt.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "COUNTERPRN", comboBoxCounter.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHEN1RATE", comboBoxKitc1BaudRate.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHEN2RATE", comboBoxKitc2BaudRate.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "RECEIPTRATE", comboBoxRecBaudRate.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "COUNTERRATE", comboBoxCountBaudRate.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "COUNTERTYPE", comboBoxCType.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHEN1TYPE", comboBoxK1Type.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHEN2TYPE", comboBoxK2Type.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "COUNTERPRN_IP", ipAddressCounter.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "COUNTERPRN_PORT", txtBoxCounterPort.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHENPRN_IP", ipAddressKitchen.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHENPRN_PORT", txtBoxKitchenPort.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHENPRN1_IP", ipAddressKitchen1.Text, iniPath);
            UtilHelper.SetIniValue("PRINT", "KITCHENPRN1_PORT", txtBoxKitchenPort1.Text, iniPath);
            UtilHelper.SetIniValue("VAN", "TID", txtBoxTid.Text, iniPath);
            UtilHelper.SetIniValue("VAN", "PORT", txtBoxSvrPort.Text, iniPath);

            MainInit.SettingLoad();
            dbpMain.Visible = false;

            FrmErrorBox msgBox = new FrmErrorBox("저장완료", "환경설정 저장이 완료 되었습니다.");

            if (msgBox.ShowDialog() == DialogResult.OK)
                this.DialogResult = DialogResult.OK;
        }
    }
}
