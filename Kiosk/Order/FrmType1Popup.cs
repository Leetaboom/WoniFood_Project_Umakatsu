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
    public partial class FrmType1Popup : Form, IFormMessage
    {
        public delegate void SendValueHandler(string data);
        public event SendValueHandler sendValueEvent;

        DBControl.MenuListCfgDAO menuListCfgDao = new DBControl.MenuListCfgDAO();

        private FrmMenuList menuList;
        private FrmMenuListToGo menuListToGo;

        string ctrlName = string.Empty;

        public string productTitle = string.Empty;
        public string productTcod = string.Empty;
        public string gubn = string.Empty;
        public string memo = string.Empty;
        public string memo1 = string.Empty;
        public string memo2 = string.Empty;
        public decimal optAmt1 = 0;
        public decimal optAmt2 = 0;
        public decimal amt = 0;
        public decimal totAmt = 0;

        System.Timers.Timer waitTime = new System.Timers.Timer(StoreInfo.BWaitTime * 1000);


        public FrmType1Popup()
        {
            InitializeComponent();
        }

        public FrmType1Popup(Form menuList, Bitmap bitmap, string grpName, string productName, string tCod, string memo)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            this.lblTitle.Text = grpName;
            if (menuList is FrmMenuList)
                this.menuList = menuList as FrmMenuList;
            else if (menuList is FrmMenuListToGo)
                this.menuListToGo = menuList as FrmMenuListToGo;
            this.productTitle = productName;
            this.productTcod = tCod;
            this.memo = memo;
            waitTime.Elapsed += OnTimedEvent;
        }

        private void FrmType1Popup_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            lblProductName.Text = productTitle;
            lblProductMemo.Text = memo;
            Type1TitleAmt(productTitle, productTcod);
            picBoxProductImg.BackgroundImage = null;
            if (menuList != null)
                picBoxProductImg.BackgroundImage = menuList.ImageLoad(productTitle, productTcod);
            else if (menuListToGo != null)
                picBoxProductImg.BackgroundImage = menuListToGo.ImageLoad(productTitle, productTcod);
            waitTime.Start();
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            waitTime.Stop();
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;

            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "picBoxBack":
                    bitmap = UtilHelper.ChangeOpacity(picBoxBack.BackgroundImage, 1f);
                    picBoxBack.BackgroundImage.Dispose();
                    picBoxBack.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "lblSingleTitle":
                case "lblSingleAmt":
                    bitmap = UtilHelper.ChangeOpacity(panelSingle.BackgroundImage, 1f);
                    panelSingle.BackgroundImage.Dispose();
                    panelSingle.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "lblSetTitle":
                case "lblSetAmt":
                    bitmap = UtilHelper.ChangeOpacity(panelSet.BackgroundImage, 1f);
                    panelSet.BackgroundImage.Dispose();
                    panelSet.BackgroundImage = bitmap;
                    bitmap = null;
                    break;
            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (ctrlName)
            {
                case "picBoxBack":
                    picBoxBack.BackgroundImage.Dispose();
                    picBoxBack.BackgroundImage = Properties.Resources.btn_back;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "lblSingleTitle":
                case "lblSingleAmt":
                    panelSingle.BackgroundImage.Dispose();
                    panelSingle.BackgroundImage = Properties.Resources.btn_bg_orange;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "lblSetTitle":
                case "lblSetAmt":
                    panelSet.BackgroundImage.Dispose();
                    panelSet.BackgroundImage = Properties.Resources.btn_bg_orange;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;
            }
        }

        private void MouseUp_Proc()
        {
            switch (ctrlName)
            {
                case "picBoxBack":
                    picBoxProductImg.BackgroundImage.Dispose();
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case "lblSingleTitle":
                case "lblSingleAmt":                   
                    gubn = "단품";
                    memo1 = string.Empty;
                    memo2 = string.Empty;
                    amt = lblSingleAmt.Text.ToDecimal();
                    totAmt = amt;
                    picBoxBack.BackgroundImage.Dispose();
                    this.DialogResult = DialogResult.OK;
                    break;

                case "lblSetTitle":
                case "lblSetAmt":
                    (FrmMain.setMenuOptPopup as IFormMessage).Receive(this, productTitle);
                    break;
            }
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            waitTime.Stop();
            waitTime.Dispose();

            picBoxProductImg.BackgroundImage.Dispose();
            picBoxBack.BackgroundImage.Dispose();
            this.DialogResult = DialogResult.Yes;
        }

        public void Receive(Form frm, string msg)
        {
            string[] tmp = msg.Split(':');

            if (msg == "TIMEOUT")
            {
                DialogResult = DialogResult.Yes;
                return;
            }

            if (tmp[0] == "세트")
            {
                gubn = tmp[0];
                amt = lblSetAmt.ToDecimal();
                memo1 = tmp[1];
                optAmt1 = tmp[2].ToDecimal();
                memo2 = tmp[3];
                optAmt2 = tmp[4].ToDecimal();

                totAmt = amt + optAmt1 + optAmt2;

                this.DialogResult = DialogResult.OK;
            }
        }

        private void Type1TitleAmt(string name, string tCod)
        {
            menuListCfgDao.BRAND = StoreInfo.BrnadCode;
            menuListCfgDao.STORE = StoreInfo.StoreCode;
            menuListCfgDao.DESK = StoreInfo.StoreDesk;

            using (DataTable dt = menuListCfgDao.MenuListCfgType1_SelectL(name, tCod))
            {
                int nCount = dt.Rows.Count;
                if (nCount != 0)
                {
                    int i = 0;
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        switch (nCount)
                        {
                            case 1:
                                while(dtr.Read())
                                {
                                    lblSingleTitle.Text = dtr["PT_OPNM"].ToString();
                                    lblSingleAmt.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());

                                    panelSet.Visible = false;
                                }
                                dtr.Close();
                                break;

                            case 2:
                                while (dtr.Read())
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            lblSingleTitle.Text = dtr["PT_OPNM"].ToString();
                                            lblSingleAmt.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            i++;
                                            continue;

                                        case 1:
                                            lblSetTitle.Text = dtr["PT_OPNM"].ToString();
                                            lblSetAmt.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                }
                                dtr.Close();
                                break;
                        }
                    }
                }
            }
        }
    }
}
