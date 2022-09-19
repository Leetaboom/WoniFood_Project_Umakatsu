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
    public partial class FrmType3Popup : Form
    {
        DBControl.MenuListCfgDAO menuListCfgDao = new DBControl.MenuListCfgDAO();

        FrmMenuList menuList;
        FrmMenuListToGo menuListToGo;

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

        public FrmType3Popup()
        {
            InitializeComponent();
        }

        public FrmType3Popup(Form menuList, Bitmap bitmap, string grpName, string productName, string tCod, string memo)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            this.lblTitle.Text = grpName;
            this.productTitle = productName;
            this.productTcod = tCod;
            if (menuList is FrmMenuList)
                this.menuList = menuList as FrmMenuList;
            else if (menuList is FrmMenuListToGo)
                this.menuListToGo = menuList as FrmMenuListToGo;
            this.memo = memo;
            waitTime.Elapsed += OnTimedEvent;

            menuListCfgDao.BRAND = StoreInfo.BrnadCode;
            menuListCfgDao.STORE = StoreInfo.StoreCode;
            menuListCfgDao.DESK = StoreInfo.StoreDesk;
        }

        private void FrmType3Popup_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            lblProductName.Text = productTitle;
            lblProductMemo.Text = memo;
            Type3TitleAmt(productTitle, productTcod);
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
                    panelSingle.BackgroundImage = Properties.Resources.btn_rad3;
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
                    picBoxBack.BackgroundImage.Dispose();
                    picBoxProductImg.BackgroundImage.Dispose();

                    this.DialogResult = DialogResult.Cancel;
                    break;

                case "lblSingleTitle":
                case "lblSingleAmt":
                    gubn = lblSingleTitle.Text;
                    memo1 = string.Empty;
                    memo2 = string.Empty;
                    amt = lblSingleAmt.Text.ToDecimal();
                    totAmt = amt;

                    picBoxBack.BackgroundImage.Dispose();
                    picBoxProductImg.BackgroundImage.Dispose();
                    this.DialogResult = DialogResult.OK;
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

        private void Type3TitleAmt(string name, string tCod)
        {
            using (DataTable dt = menuListCfgDao.MenuListCfgType3_SelectL(name, tCod))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            lblSingleTitle.Text = dtr["PT_OPNM"].ToString().Length == 0 ? "단품" : dtr["PT_OPNM"].ToString();
                            lblSingleAmt.Text = string.Format("{0:#,##0}원" , dtr["PT_PRICE"].ToDecimal());

                        }

                        dtr.Close();
                    }
                }
            }
        }
    }
}
