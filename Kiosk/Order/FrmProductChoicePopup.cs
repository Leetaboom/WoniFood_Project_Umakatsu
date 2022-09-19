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
    public partial class FrmProductChoicePopup : Form, IFormMessage
    {
        FrmMenuList menuList;
        public ProductInfo productInfo = new ProductInfo();

        DBControl.MenuListCfgDAO menuListCfgDao = new DBControl.MenuListCfgDAO();

        string ctrlName = string.Empty;
        //int ctrlIndex = -1;

        System.Timers.Timer waitTime = new System.Timers.Timer(20000);

        public FrmProductChoicePopup()
        {
            InitializeComponent();
        }

        public FrmProductChoicePopup(FrmMenuList menuList, Bitmap bitmap, string groupName, string productName, string tcod)
        {
            InitializeComponent();

            menuListCfgDao.BRAND = StoreInfo.BrnadCode;
            menuListCfgDao.STORE = StoreInfo.StoreCode;
            menuListCfgDao.DESK = StoreInfo.StoreDesk;

            this.BackgroundImage = bitmap;
            productInfo.groupName = groupName;
            productInfo.productName = productName;
            productInfo.productTcod = tcod;
            this.menuList = menuList;
            waitTime.Elapsed += OnTimedEvent;
        }

        private void FrmProductChoicePopup_Load(object sender, EventArgs e)
        {
            lblTitle.Text = productInfo.groupName;
            lblMenuName.Text = productInfo.productName;
            picBoxMenu.BackgroundImage = menuList.ImageLoad(productInfo.productName, productInfo.productTcod);
            GetMenuItemAmt(productInfo.productName, productInfo.productTcod);
            waitTime.Start();
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;

            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "picBoxBack":
                    bitmap = UtilHelper.ChangeOpacity(picBoxBack.BackgroundImage, 1f);
                    picBoxBack.BackgroundImage.Dispose();
                    picBoxBack.BackgroundImage = bitmap;
                    break;

                case "lblSubTitle_0":
                case "lblSubAmt_0":
                    bitmap = UtilHelper.ChangeOpacity(panelSubItem_0.Style.BackgroundImage, 1f);
                    panelSubItem_0.Style.BackgroundImage.Dispose();
                    panelSubItem_0.Style.BackgroundImage = bitmap;
                    break;
            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {

            switch (ctrlName)
            {
                case "picBoxBack":
                    picBoxBack.BackgroundImage.Dispose();
                    picBoxBack.BackgroundImage = Properties.Resources.btn_back1;
                    UtilHelper.Delay(100);

                    BtnEventProc();
                    break;

                case "lblSubTitle_0":
                case "lblSubAmt_0":
                    panelSubItem_0.Style.BackgroundImage.Dispose();
                    panelSubItem_0.Style.BackgroundImage = Properties.Resources.btn_bg_orange;
                    UtilHelper.Delay(100);

                    BtnEventProc();
                    break;
            }
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            waitTime.Stop();
            waitTime = null;
            productInfo = null;
            menuList = null;

            this.DialogResult = DialogResult.Yes;
        }

        private void BtnEventProc()
        {
            switch (ctrlName)
            {
                case "picBoxBack":
                    waitTime.Stop();
                    waitTime = null;
                    //productInfo = null;
                    menuList = null;
                    menuListCfgDao = null;

                    this.DialogResult = DialogResult.Cancel;
                    break;

                case "lblSubTitle_0":
                case "lblSubAmt_0":
                    productInfo.productGubn = "단품";
                    productInfo.memo1 = string.Empty;
                    productInfo.memo2 = string.Empty;
                    productInfo.dAmt = lblSubAmt_0.Text.ToDecimal();
                    productInfo.dTotAmt = productInfo.dAmt;

                    waitTime.Stop();
                    waitTime = null;
                   // productInfo = null;
                    menuList = null;
                    menuListCfgDao = null;

                    this.DialogResult = DialogResult.OK;
                    break;

                case "lblSetTitle":
                case "lblSetAmt":
                    break;
            }

        }

        public void Receive(Form frm, string msg)
        {

        }

        public void GetMenuItemAmt(string name, string tcod)
        {
            int count = 0;

            using (DataTable dt = menuListCfgDao.MenuListCfgType1_SelectL(name, tcod))
            {
                int rowsCount = dt.Rows.Count;

                if (rowsCount != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        switch (rowsCount)
                        {
                            case 1:
                                while (dtr.Read())
                                {
                                    lblSubTitle_0.Text = dtr["PT_OPNM"].ToString();
                                    lblSubAmt_0.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());

                                    panelSubItem_1.Visible = false;
                                    panelSubItem_2.Visible = false;
                                    panelSubItem_3.Visible = false;
                                }
                                break;

                            case 2:
                                count = 0;
                                while (dtr.Read())
                                {
                                    switch(count)
                                    {
                                        case 0:
                                            lblSubTitle_0.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_0.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            count++;
                                            break;

                                        case 1:
                                            lblSubTitle_1.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_1.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                    panelSubItem_2.Visible = false;
                                    panelSubItem_3.Visible = false;
                                    
                                }
                                break;

                            case 3:
                                count = 0;
                                while (dtr.Read())
                                {
                                    switch (count)
                                    {
                                        case 0:
                                            lblSubTitle_0.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_0.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            count++;
                                            break;

                                        case 1:
                                            lblSubTitle_1.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_1.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            count++;
                                            break;

                                        case 2:
                                            lblSubTitle_2.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_2.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                    panelSubItem_3.Visible = false;

                                }
                                break;

                            case 4:
                                count = 0;
                                while (dtr.Read())
                                {
                                    switch (count)
                                    {
                                        case 0:
                                            lblSubTitle_0.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_0.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            count++;
                                            break;

                                        case 1:
                                            lblSubTitle_1.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_1.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            count++;
                                            break;

                                        case 2:
                                            lblSubTitle_2.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_2.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            count++;
                                            break;

                                        case 3:
                                            lblSubTitle_3.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_3.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        public class ProductInfo
        {
            public string groupName = string.Empty;
            public string productName = string.Empty;
            public string productTcod = string.Empty;
            public string productGubn = string.Empty;
            public string memo1 = string.Empty;
            public string memo2 = string.Empty;
            public decimal dOpAmt1 = 0;
            public decimal dOpAmt2 = 0;
            public decimal dAmt = 0;
            public decimal dTotAmt = 0;
        }
    }
}
