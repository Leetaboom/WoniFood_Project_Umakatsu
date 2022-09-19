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
    public partial class FrmType2Popup : Form
    {
        DBControl.MenuListCfgDAO menuListCfgDao = new DBControl.MenuListCfgDAO();

        FrmMenuList menuList;
        FrmMenuListToGo menuListToGo;

        string ctrlName = string.Empty;
        int nCtrlIndex = -1;

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

        public FrmType2Popup()
        {
            InitializeComponent();
        }

        public FrmType2Popup(Form menuList, Bitmap bitmap, string grpName, string productName, string tCod, string memo)
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
            //menuListCfgDao.BRAND = "00002";
            menuListCfgDao.STORE = StoreInfo.StoreCode;
            menuListCfgDao.DESK = StoreInfo.StoreDesk;
        }

        private void FrmType2Popup_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            lblProductName.Text = productTitle;
            lblProductMemo.Text = memo;
            picBoxProductImg.BackgroundImage = null;
            if (menuList != null)
                picBoxProductImg.BackgroundImage = menuList.ImageLoad(productTitle, productTcod);
            else if (menuListToGo != null)
                picBoxProductImg.BackgroundImage = menuListToGo.ImageLoad(productTitle, productTcod);
            MenuType2TitleAmt(productTitle, productTcod);
            waitTime.Start();
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            waitTime.Stop();
            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            nCtrlIndex = tmpName[1].ToInteger();
            Bitmap bitmap;

            UtilHelper.spWav.Play();

            switch (nCtrlIndex)
            {
                case 0:
                    bitmap = UtilHelper.ChangeOpacity(panelSubItem_0.BackgroundImage, 1f);
                    panelSubItem_0.BackgroundImage.Dispose();
                    panelSubItem_0.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case 1:
                    bitmap = UtilHelper.ChangeOpacity(panelSubItem_1.BackgroundImage, 1f);
                    panelSubItem_1.BackgroundImage.Dispose();
                    panelSubItem_1.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case 2:
                    bitmap = UtilHelper.ChangeOpacity(panelSubItem_2.BackgroundImage, 1f);
                    panelSubItem_2.BackgroundImage.Dispose();
                    panelSubItem_2.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case 3:
                    bitmap = UtilHelper.ChangeOpacity(panelSubItem_3.BackgroundImage, 1f);
                    panelSubItem_3.BackgroundImage.Dispose();
                    panelSubItem_3.BackgroundImage = bitmap;
                    bitmap = null;
                    break;
            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (nCtrlIndex)
            {
                case 0:
                    panelSubItem_0.BackgroundImage.Dispose();
                    panelSubItem_0.BackgroundImage = Properties.Resources.btn_rad3;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case 1:
                    panelSubItem_1.BackgroundImage.Dispose();
                    panelSubItem_1.BackgroundImage = Properties.Resources.btn_rad3;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case 2:
                    panelSubItem_2.BackgroundImage.Dispose();
                    panelSubItem_2.BackgroundImage = Properties.Resources.btn_rad3;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case 3:
                    panelSubItem_3.BackgroundImage.Dispose();
                    panelSubItem_3.BackgroundImage = Properties.Resources.btn_rad3;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;
            }
        }

        private void picBoxBack_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;

            UtilHelper.spWav.Play();

            bitmap = UtilHelper.ChangeOpacity(picBoxBack.BackgroundImage, 1f);
            picBoxBack.BackgroundImage.Dispose();
            picBoxBack.BackgroundImage = bitmap;
            bitmap = null;
        }

        private void picBoxBack_MouseUp(object sender, MouseEventArgs e)
        {
            picBoxBack.BackgroundImage.Dispose();
            picBoxBack.BackgroundImage = Properties.Resources.btn_back1;
            UtilHelper.Delay(50);

            MouseUp_Proc();
        }

        private void MouseUp_Proc()
        {
            switch (ctrlName)
            {
                case "picBoxBack":
                    picBoxProductImg.BackgroundImage.Dispose();
                    this.DialogResult = DialogResult.Cancel;
                    break;
            }

            if (nCtrlIndex != -1)
            {
                switch (nCtrlIndex)
                {
                    case 0:
                        gubn = lblSubTitle_0.Text;
                        amt = lblSubAmt_0.Text.ToDecimal();
                        totAmt = amt + optAmt1 + optAmt2;

                        //picBoxProductImg.BackgroundImage.Dispose();
                        this.DialogResult = DialogResult.OK;
                        break;

                    case 1:
                        gubn = lblSubTitle_1.Text;
                        amt = lblSubAmt_1.Text.ToDecimal();
                        totAmt = amt + optAmt1 + optAmt2;

                        //picBoxProductImg.BackgroundImage.Dispose();
                        this.DialogResult = DialogResult.OK;
                        break;

                    case 2:
                        gubn = lblSubTitle_2.Text;
                        amt = lblSubAmt_2.Text.ToDecimal();
                        totAmt = amt + optAmt1 + optAmt2;

                        //picBoxProductImg.BackgroundImage.Dispose();
                        this.DialogResult = DialogResult.OK;
                        break;

                    case 3:
                        gubn = lblSubTitle_3.Text;
                        amt = lblSubAmt_3.Text.ToDecimal();
                        totAmt = amt + optAmt1 + optAmt2;

                        //picBoxProductImg.BackgroundImage.Dispose();
                        this.DialogResult = DialogResult.OK;
                        break;
                }
            }
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            waitTime.Stop();
            waitTime.Dispose();
            picBoxProductImg.BackgroundImage.Dispose();
            this.DialogResult = DialogResult.Yes;
        }

        private void MenuType2TitleAmt(string name, string tCod)
        {
            using (DataTable dt = menuListCfgDao.MenuListCfgType2_SelectL(name, tCod))
            {
                int nCount = dt.Rows.Count;
                int nSkip = 0;
                if (nCount != 0)
                {
                    int i = 0;
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        switch (nCount)
                        {
                            case 1:
                                while (dtr.Read())
                                {
                                    //lblSubTitle_0.Text = dtr["PT_OPNM"].ToString();
                                    lblSubTitle_0.Text = dtr["PT_OPNM"].ToString().Length != 0 ? dtr["PT_OPNM"].ToString() : "선택";
                                    lblSubAmt_0.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                }
                                dtr.Close();
                                break;

                            case 2:
                                while (dtr.Read())
                                {
                                    if (StoreInfo.IsTakeOut && (dtr["PT_OPNM"].ToString().Trim() == "단품"))
                                    {
                                        nSkip++;
                                        continue;
                                    }
                                    else if (!StoreInfo.IsTakeOut && (dtr["PT_OPNM"].ToString().Trim() == "커팅 O" || dtr["PT_OPNM"].ToString().Trim() == "커팅 X"))
                                    {
                                        nSkip++;
                                        continue;
                                    }

                                    switch (i)
                                    {
                                        case 0:
                                            lblSubTitle_0.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_0.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            i++;
                                            continue;

                                        case 1:
                                            lblSubTitle_1.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_1.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                }
                                dtr.Close();
                                break;

                            case 3:
                                while (dtr.Read())
                                {
                                    if (StoreInfo.IsTakeOut && (dtr["PT_OPNM"].ToString().Trim() == "단품"))
                                    {
                                        nSkip++;
                                        continue;
                                    }
                                    else if (!StoreInfo.IsTakeOut && (dtr["PT_OPNM"].ToString().Trim() == "커팅 O" || dtr["PT_OPNM"].ToString().Trim() == "커팅 X"))
                                    {
                                        nSkip++;
                                        continue;
                                    }

                                    switch (i)
                                    {
                                        case 0:
                                            lblSubTitle_0.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_0.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            i++;
                                            continue;

                                        case 1:
                                            lblSubTitle_1.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_1.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            i++;
                                            continue;

                                        case 2:
                                            lblSubTitle_2.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_2.Text = string.Format("{0:#,##0}", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                }


                                dtr.Close();
                                break;

                            case 4:
                                while (dtr.Read())
                                {
                                    if (StoreInfo.IsTakeOut && (dtr["PT_OPNM"].ToString().Trim() == "단품"))
                                    {
                                        nSkip++;
                                        continue;
                                    }
                                    else if (!StoreInfo.IsTakeOut && (dtr["PT_OPNM"].ToString().Trim() == "커팅 O" || dtr["PT_OPNM"].ToString().Trim() == "커팅 X"))
                                    {
                                        nSkip++;
                                        continue;
                                    }

                                    switch (i)
                                    {
                                        case 0:
                                            lblSubTitle_0.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_0.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            i++;
                                            continue;

                                        case 1:
                                            lblSubTitle_1.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_1.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            i++;
                                            continue;

                                        case 2:
                                            lblSubTitle_2.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_2.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            i++;
                                            continue;

                                        case 3:
                                            lblSubTitle_3.Text = dtr["PT_OPNM"].ToString();
                                            lblSubAmt_3.Text = string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                }
                                dtr.Close();
                                break;
                        }

                        int result = nCount - nSkip;

                        if(result == 1)
                        {
                            panelSubItem_1.Visible = false;
                            panelSubItem_2.Visible = false;
                            panelSubItem_3.Visible = false;
                        }
                        else if(result == 2)
                        {
                            panelSubItem_2.Visible = false;
                            panelSubItem_3.Visible = false;
                        }
                        else if(result == 3)
                        {
                            panelSubItem_3.Visible = false;
                        }
                    }
                }
            }
        }
    }
}
