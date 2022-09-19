using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Kiosk
{
    public partial class FrmSetMenuOptPopup : Form, IFormMessage
    {
        DBControl.MenuListCfgDAO menuListCfgDao = new DBControl.MenuListCfgDAO();

        FrmType1Popup type1Popup;
        FrmMenuList menuList = new FrmMenuList();
        BasicOptInfo basicOptInfo = new BasicOptInfo();
        OptionItemGrp optionItemGrp = new OptionItemGrp();

        OptionItemProperty optItemVar = new OptionItemProperty();

        string ctrlName = string.Empty;
        int nBasicIndex = 0;
        bool isFirstSound = false;

        public string basicOptTitle = string.Empty;
        public string optionItemTitle = string.Empty;
        public decimal basicOptAmt = 0;
        public decimal optionItemAmt = 0;


        System.Timers.Timer waitTime = new System.Timers.Timer(StoreInfo.BWaitTime.ToInteger() * 1000);

        public FrmSetMenuOptPopup()
        {
            InitializeComponent();

            waitTime.Elapsed += OnTimedEvent;
            menuListCfgDao.BRAND = StoreInfo.BrnadCode;
            menuListCfgDao.STORE = StoreInfo.StoreCode;
            menuListCfgDao.DESK = StoreInfo.StoreDesk;
        }

        private void FrmSetMentOptPopup_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            ThreadPool.QueueUserWorkItem(SetPopupShow, true);
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            waitTime.Stop();

            this.Invoke(new MethodInvoker(
                delegate ()
                {
                    SetMenuListInit();

                    this.Visible = false;
                    (type1Popup as IFormMessage).Receive(null, "TIMEOUT");
                }));
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
                    bitmap = null;
                    break;

                case "picBoxPrev":
                    bitmap = UtilHelper.ChangeOpacity(picBoxPrev.BackgroundImage, 1f);
                    picBoxPrev.BackgroundImage.Dispose();
                    picBoxPrev.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxNext":
                    bitmap = UtilHelper.ChangeOpacity(picBoxNext.BackgroundImage, 1f);
                    picBoxNext.BackgroundImage.Dispose();
                    picBoxNext.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "lblSelectOk":
                    bitmap = UtilHelper.ChangeOpacity(panelOk.BackgroundImage, 1f);
                    panelOk.BackgroundImage.Dispose();
                    panelOk.BackgroundImage = bitmap;
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

                case "picBoxPrev":
                    picBoxPrev.BackgroundImage.Dispose();
                    picBoxPrev.BackgroundImage = Properties.Resources.btn_page_back;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxNext":
                    picBoxNext.BackgroundImage.Dispose();
                    picBoxNext.BackgroundImage = Properties.Resources.btn_page_next;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "lblSelectOk":
                    panelOk.BackgroundImage.Dispose();
                    panelOk.BackgroundImage = Properties.Resources.btn_rad3;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;
            }
        }

        private void MouseUp_Proc()
        {
            waitTime.Stop();

            switch (ctrlName)
            {
                case "picBoxBack":
                    SetMenuListInit();
                    break;

                case "picBoxPrev":
                    if (optItemVar.optionItemPageNow < optItemVar.optionItemTotalPage + 1 && optItemVar.optionItemPageNow > 0)
                    {
                        for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                            optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Visible = false;

                        lblPage.Text = string.Format("{0} / {1}", (--optItemVar.optionItemPageNow) + 1, optItemVar.optionItemTotalPage + 1);

                        for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                            optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Visible = true;
                    }
                    waitTime.Start();
                    break;

                case "picBoxNext":
                    if (optItemVar.optionItemPageNow >= 0 && optItemVar.optionItemPageNow < optItemVar.optionItemTotalPage)
                    {
                        for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                            optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Visible = false;

                        lblPage.Text = string.Format("{0} / {1}", (--optItemVar.optionItemPageNow) + 1, optItemVar.optionItemTotalPage + 1);

                        if (optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[0].Visible == false)
                        {
                            for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                                if (optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuTitle[i].Text.Length > 0)
                                    optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Visible = true;
                        }
                        else
                        {
                            for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                            {
                                if (optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuTitle[i].Text.Length != 0)
                                    panelOptItem.Controls.Add(optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i]);
                            
                            }
                        }
                    }
                    waitTime.Start();
                    break;

                case "lblSelectOk":
                    for (int i = 0; i < optionItemGrp.optItmeInfo.Length; i++)
                    {
                        for (int j = 0; j < optionItemGrp.optItmeInfo[i].menuBox.Count; j++)
                        {
                            if (optionItemGrp.optItmeInfo[i].menuCheckBox[j].BackgroundImage != null)
                            {
                                decimal amt = optionItemGrp.optItmeInfo[i].menuSingleTitle[j].Text.ToDecimal();

                                optionItemTitle = optionItemGrp.optItmeInfo[i].menuTitle[j].Text;
                                optionItemAmt = amt;
                            }
                        }
                    }

                    (type1Popup as IFormMessage).Receive(null, "세트:" + basicOptTitle + ":" + basicOptAmt + ":"
                        + optionItemTitle + ":" + optionItemAmt);

                    SetMenuListInit();
                    this.Visible = false;

                    break;
            }
        }

        private void SetPopupShow(object sender)
        {
            this.Invoke(new MethodInvoker(
                delegate ()
                {
                    this.Visible = false;
                    OptionItemTotalPage();
                    CreateOptSetMenuList();

                    for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuCheckBox[i].BackgroundImage = Properties.Resources.btn_check;
                                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Style.BorderColor.Color = Color.Red;
                                break;
                        }
                        panelOptItem.Controls.Add(optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i]);
                    }
                    GetSetMenuList();
                    lblPage.Text = string.Format("{0} / {1}", optItemVar.optionItemPageNow + 1, optItemVar.optionItemTotalPage + 1);
                }));
        }

        private void OptionItemTotalPage()
        {
            using (DataTable dt = menuListCfgDao.OptionItemTotalPageSelectL())
            {
                int nCount = dt.Rows.Count;

                if (nCount < 8)
                    optItemVar.optionItemTotalPage = 0;
                else if (nCount > 8 && nCount <= 16)
                    optItemVar.optionItemTotalPage = 1;
                else if (nCount > 16 && nCount <= 24)
                    optItemVar.optionItemTotalPage = 2;
                else if (nCount > 24 && nCount <= 32)
                    optItemVar.optionItemTotalPage = 3;
                else if (nCount > 32 && nCount <= 40)
                    optItemVar.optionItemTotalPage = 4;
                else if (nCount > 40 && nCount <= 48)
                    optItemVar.optionItemTotalPage = 5;
                else
                    optItemVar.optionItemTotalPage = 6;

            }
        }      
        
        private void CreateOptSetMenuList()
        {
            CreateSetBasicMenu();

            for (int i = 0; i < optionItemGrp.optItmeInfo.Length; i++)
            {
                optionItemGrp.optItmeInfo[i] = new OptionItemInfo();
                CreateSetOptMenuList(i);
            }
        }           
         
        private void CreateSetBasicMenu()
        {
            Point menuBoxLoc = new Point(4, 6);
            Size menuBoxSize = new Size(panelBasic.Width, panelBasic.Height);

            for (int i = 0; i < 2; i++, menuBoxLoc.X += menuBoxSize.Width / 2)
            {
                Controler.WoniPanel menuBox = new Controler.WoniPanel();
                menuBox.Location = menuBoxLoc;
                menuBox.Size = new Size(menuBoxSize.Width / 2 - 10, menuBoxSize.Height - 25);
                menuBox.Name = "MenuBox_" + i;
                menuBox.Style.Border = DevComponents.DotNetBar.eBorderType.DoubleLine;
                menuBox.Style.BorderColor.Color = Color.Orange;
                menuBox.Style.BorderWidth = 8;
                menuBox.Click += new EventHandler(basicMenuBox_Click);

                Controler.WoniPanel menuImgBox = new Controler.WoniPanel();
                menuImgBox.Location = new Point(20, 20);
                menuImgBox.Size = new Size(158, 146);
                menuImgBox.Name = "MenuImgBox_" + i;
                menuImgBox.Style.BackColor1.Color = Color.White;
                menuImgBox.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
                menuImgBox.Style.BorderColor.Color = Color.Orange;
                menuImgBox.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
                //menuImgBox.BackgroundImage = menuList.ImageLoad("케이준양념감자");
                menuImgBox.Style.CornerDiameter = 10;
                menuImgBox.Click += new EventHandler(basicMenuBox_Click);

                PictureBox menuCheckBox = new PictureBox();
                menuCheckBox.Location = new Point(30, 20);
                menuCheckBox.Size = new Size(127, 114);
                menuCheckBox.Name = "MenuCheckBox_" + i;
                menuCheckBox.BackColor = Color.Transparent;
                menuCheckBox.BackgroundImageLayout = ImageLayout.None;
                menuCheckBox.Click += new EventHandler(basicMenuBox_Click);

                Label menuTitle = new Label();
                menuTitle.AutoSize = false;
                menuTitle.Location = new Point(184, 16);
                menuTitle.Size = new Size(250, 50);
                menuTitle.Name = "MenuTitle_" + i;
                menuTitle.Font = new Font("나눔바른고딕", 16, FontStyle.Bold);
                menuTitle.TextAlign = ContentAlignment.MiddleCenter;
                //menuTitle.Text = "케이준양념감자";
                menuTitle.Click += new EventHandler(basicMenuBox_Click);

                Label menuSingleTitle = new Label();
                menuSingleTitle.AutoSize = false;
                menuSingleTitle.Location = new Point(184, 68);
                menuSingleTitle.Size = new Size(menuTitle.Width, menuTitle.Height);
                menuSingleTitle.Name = "MenuSingleTitle_" + i;
                menuSingleTitle.Font = new Font("나눔바른고딕", 12, FontStyle.Bold);
                menuSingleTitle.TextAlign = ContentAlignment.MiddleCenter;
                //menuSingleTitle.Text = "중";
                menuSingleTitle.Click += new EventHandler(basicMenuBox_Click);

                Label menuSingleAmt = new Label();
                menuSingleAmt.AutoSize = false;
                menuSingleAmt.Location = new Point(184, 120);
                menuSingleAmt.Size = new Size(menuSingleTitle.Width, menuSingleTitle.Height);
                menuSingleAmt.Name = "MenuSingleAmt_" + i;
                menuSingleAmt.Font = new Font("나눔바른고딕", 12, FontStyle.Bold);
                menuSingleAmt.TextAlign = ContentAlignment.MiddleCenter;
                //menuSingleAmt.Text = "0원";
                menuSingleAmt.Click += new EventHandler(basicMenuBox_Click);

                menuBox.Controls.Add(menuImgBox);
                menuImgBox.Controls.Add(menuCheckBox);
                menuBox.Controls.Add(menuTitle);
                menuBox.Controls.Add(menuSingleTitle);
                menuBox.Controls.Add(menuSingleAmt);

                basicOptInfo.menuImgBox.Add(menuImgBox);
                basicOptInfo.menuCheckBox.Add(menuCheckBox);
                basicOptInfo.menuTitle.Add(menuTitle);
                basicOptInfo.menuSingleTitle.Add(menuSingleTitle);
                basicOptInfo.menuSingleAmt.Add(menuSingleAmt);
                basicOptInfo.menuBox.Add(menuBox);
                basicOptInfo.menuTCode.Add(string.Empty);
            }
        }

        private void CreateSetOptMenuList(int index)
        {
            Point menuBoxLoc = new Point(4, 6);
            Size menuBoxSize = new Size(panelOptItem.Width, panelOptItem.Height);

            int j = 0;
            int k = 0;

            for (int i = 0; i < 8; i++, menuBoxLoc.X += menuBoxSize.Width / 2)
            {
                if (j == 2)
                {
                    menuBoxLoc.Y += menuBoxSize.Height / 4;
                    menuBoxLoc.X = 4;
                    j = 0;
                    k++;
                }

                if (k == 4)
                    break;
                j++;

                Controler.WoniPanel menuBox = new Controler.WoniPanel();
                menuBox.Location = menuBoxLoc;
                menuBox.Size = new Size(menuBoxSize.Width / 2 - 10, menuBoxSize.Height / 4 - 25);
                menuBox.Name = "MenuBox_" + i;
                menuBox.Style.Border = DevComponents.DotNetBar.eBorderType.DoubleLine;
                menuBox.Style.BorderColor.Color = Color.Orange;
                menuBox.Style.BorderWidth = 8;
                menuBox.Click += new EventHandler(optItemMenuBox_Click);

                Controler.WoniPanel menuImgBox = new Controler.WoniPanel();
                menuImgBox.Location = new Point(20, 20);
                menuImgBox.Size = new Size(158, 146);
                menuImgBox.Name = "MenuImgBox_" + i;
                menuImgBox.Style.BackColor1.Color = Color.White;
                menuImgBox.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
                menuImgBox.Style.BorderColor.Color = Color.Orange;
                menuImgBox.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
                menuImgBox.Style.CornerDiameter = 10;
                menuImgBox.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Stretch;
                menuImgBox.Click += new EventHandler(optItemMenuBox_Click);

                PictureBox menuCheckBox = new PictureBox();
                menuCheckBox.Location = new Point(26, 20);
                menuCheckBox.Size = new Size(127, 114);
                menuCheckBox.Name = "MenuCheckBox_" + i;
                menuCheckBox.BackColor = Color.Transparent;
                menuCheckBox.BackgroundImageLayout = ImageLayout.None;
                menuCheckBox.Click += new EventHandler(optItemMenuBox_Click);

                Label menuTitle = new Label();
                menuTitle.AutoSize = false;
                menuTitle.Location = new Point(184, 44);
                menuTitle.Size = new Size(250, 50);
                menuTitle.Name = "MenuTitle_" + i;
                menuTitle.Font = new Font("나눔바른고딕", 16, FontStyle.Bold);
                menuTitle.TextAlign = ContentAlignment.MiddleCenter;
                menuTitle.Text = "";
                menuTitle.Click += new EventHandler(optItemMenuBox_Click);


                Label menuSingleTitle = new Label();
                menuSingleTitle.AutoSize = false;
                menuSingleTitle.Location = new Point(184, 88);
                menuSingleTitle.Size = new Size(menuTitle.Width, menuTitle.Height);
                menuSingleTitle.Name = "MenuSingleTitle_" + i;
                menuSingleTitle.Font = new Font("나눔바른고딕", 12, FontStyle.Bold);
                menuSingleTitle.TextAlign = ContentAlignment.MiddleCenter;
                menuSingleTitle.Text = "";
                menuSingleTitle.Click += new EventHandler(optItemMenuBox_Click);

                menuBox.Controls.Add(menuImgBox);
                menuImgBox.Controls.Add(menuCheckBox);
                menuBox.Controls.Add(menuTitle);
                menuBox.Controls.Add(menuSingleTitle);

                optionItemGrp.optItmeInfo[index].menuImgBox.Add(menuImgBox);
                optionItemGrp.optItmeInfo[index].menuCheckBox.Add(menuCheckBox);
                optionItemGrp.optItmeInfo[index].menuTitle.Add(menuTitle);
                optionItemGrp.optItmeInfo[index].menuSingleTitle.Add(menuSingleTitle);
                optionItemGrp.optItmeInfo[index].menuBox.Add(menuBox);
                optionItemGrp.optItmeInfo[index].menuTCode.Add(string.Empty);
            }
        }

        private void BasicCheckVisable()
        {
            for (int i = 0; i < basicOptInfo.menuBox.Count; i++)
            {
                if (basicOptInfo.menuCheckBox[i].BackgroundImage != null)
                    basicOptInfo.menuCheckBox[i].BackgroundImage = null;

                basicOptInfo.menuBox[i].Style.BorderColor.Color = Color.Orange;
            }
        }

        private void OptItemCheckVisable()
        {
            for (int i = 0; i < optionItemGrp.optItmeInfo.Length; i++)
            {
                for (int j = 0; j < optionItemGrp.optItmeInfo[i].menuBox.Count; j++)
                {
                    if (optionItemGrp.optItmeInfo[i].menuCheckBox[j].BackgroundImage != null)
                        optionItemGrp.optItmeInfo[i].menuCheckBox[j].BackgroundImage = null;

                    optionItemGrp.optItmeInfo[i].menuBox[j].Style.BorderColor.Color = Color.Orange;
                }
            }
        }

        private void basicMenuBox_Click(object sender, EventArgs e)
        {
            waitTime.Stop();

            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            nBasicIndex = tmpName[1].ToInteger();

            BasicCheckVisable();

            if (isFirstSound != true)
                UtilHelper.spWav.Play();

            basicOptInfo.menuCheckBox[nBasicIndex].BackgroundImage = Properties.Resources.btn_check;
            basicOptInfo.menuBox[nBasicIndex].Style.BorderColor.Color = Color.Red;

            waitTime.Start();
        }

        private void optItemMenuBox_Click(object sender, EventArgs e)
        {
            waitTime.Stop();

            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            optItemVar.optionItemSelectIndex = tmpName[1].ToInteger();

            OptItemCheckVisable();

            if (isFirstSound != true)
                UtilHelper.spWav.Play();

            optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuCheckBox[optItemVar.optionItemSelectIndex].BackgroundImage =
                Properties.Resources.btn_check;
            optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[optItemVar.optionItemSelectIndex].Style.BorderColor.Color =
                Color.Red;

            waitTime.Start();
        }

        private void SetOptMenuListInit()
        {
            BasicCheckVisable();
            OptItemCheckVisable();

            basicOptInfo.menuCheckBox[0].BackgroundImage = Properties.Resources.btn_check;
            basicOptInfo.menuBox[0].Style.BorderColor.Color = Color.Orange;

            if (optItemVar.optionItemPageNow != 0)
            {
                for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                    optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Visible = false;

                optItemVar.optionItemPageNow = 0;

                for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                    optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Visible = true;

                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[0].Style.BorderColor.Color = Color.Red;
                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuCheckBox[0].BackgroundImage = Properties.Resources.btn_check;
            }
            else
            {
                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[0].Style.BorderColor.Color = Color.Red;
                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuCheckBox[0].BackgroundImage = Properties.Resources.btn_check;
            }

            lblPage.Text = string.Format("{0} / {1}", optItemVar.optionItemPageNow + 1, optItemVar.optionItemTotalPage + 1);

            this.Visible = false;
        }

        private void GetSetMenuList()
        {
            Bitmap bitmap;

            for (int i = 0; i < optionItemGrp.optItmeInfo.Length; i++)
            {
                for (int k = 0; k < optionItemGrp.optItmeInfo[i].menuBox.Count; k++)
                {
                    menuListCfgDao.PAGE = i;
                    menuListCfgDao.COLROW = k;

                    using (DataTable dt = menuListCfgDao.SetMenuListCfgSelectL())
                    {
                        if (dt.Rows.Count != 0)
                        {
                            using (DataTableReader dtr = new DataTableReader(dt))
                            {
                                while (dtr.Read())
                                {
                                    if (dtr["ST_NAME"].ToString().Length > 0)
                                    {
                                        if (i == 0 && k == 0)
                                        {
                                            bitmap = menuList.ImageLoad(dtr["ST_NAME"].ToString(), dtr["ST_TCOD"].ToString());
                                            optionItemGrp.optItmeInfo[i].menuTitle[k].Text = dtr["ST_NAME"].ToString();
                                            optionItemGrp.optItmeInfo[i].menuSingleTitle[k].Text = dtr["ST_PRICE"].ToString();
                                            optionItemGrp.optItmeInfo[i].menuImgBox[k].BackgroundImage = bitmap;
                                            continue;
                                        }
                                        else if (i == 0 && k == 1)
                                        {
                                            bitmap = menuList.ImageLoad(dtr["ST_NAME"].ToString(), dtr["ST_TCOD"].ToString());
                                            optionItemGrp.optItmeInfo[i].menuTitle[k].Text = dtr["ST_NAME"].ToString();
                                            optionItemGrp.optItmeInfo[i].menuSingleTitle[k].Text = dtr["ST_PRICE"].ToString();
                                            optionItemGrp.optItmeInfo[i].menuImgBox[k].BackgroundImage = bitmap;
                                            continue;
                                        }
                                        else
                                        {
                                            bitmap = menuList.ImageLoad(dtr["ST_NAME"].ToString(), dtr["ST_TCOD"].ToString());
                                            optionItemGrp.optItmeInfo[i].menuTitle[k].Text = dtr["ST_NAME"].ToString();
                                            optionItemGrp.optItmeInfo[i].menuSingleTitle[k].Text = dtr["ST_PRICE"].ToString();
                                            optionItemGrp.optItmeInfo[i].menuImgBox[k].BackgroundImage = bitmap;

                                        }
                                    }
                                    else
                                    {
                                        optionItemGrp.optItmeInfo[i].menuBox[k].Visible = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SetMenuListInit()
        {
            BasicCheckVisable();
            OptItemCheckVisable();

            basicOptInfo.menuCheckBox[0].BackgroundImage = Properties.Resources.btn_check;
            basicOptInfo.menuBox[0].Style.BorderColor.Color = Color.Orange;

            if (optItemVar.optionItemPageNow != 0)
            {
                for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                    optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Visible = false;

                optItemVar.optionItemPageNow = 0;

                for (int i = 0; i < optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox.Count; i++)
                    optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[i].Visible = true;

                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[0].Style.BorderColor.Color = Color.Red;
                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuCheckBox[0].BackgroundImage = Properties.Resources.btn_check;
            }
            else
            {
                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuBox[0].Style.BorderColor.Color = Color.Red;
                optionItemGrp.optItmeInfo[optItemVar.optionItemPageNow].menuCheckBox[0].BackgroundImage = Properties.Resources.btn_check;
            }

            lblPage.Text = string.Format("{0} / {1}", optItemVar.optionItemPageNow + 1, optItemVar.optionItemTotalPage + 1);

            this.Visible = false;
        }

        public void Receive(Form frm, string msg)
        {
            type1Popup = (FrmType1Popup)frm;
            this.BackgroundImage = type1Popup.BackgroundImage;
            this.Visible = true;
            waitTime.Start();
        }
    }

    public class OptionItemProperty
    {
        public int optionItemTotalPage;
        public int optionItemPageNow = 0;
        public int optionItemSelectIndex = -1;
    }

    public class BasicOptInfo
    {
        public List<Controler.WoniPanel> menuBox = new List<Controler.WoniPanel>();
        public List<Controler.WoniPanel> menuImgBox = new List<Controler.WoniPanel>();
        public List<PictureBox> menuCheckBox = new List<PictureBox>();
        public List<Label> menuTitle = new List<Label>();
        public List<Label> menuSingleTitle = new List<Label>();
        public List<Label> menuSingleAmt = new List<Label>();
        public List<string> menuTCode = new List<string>();
    }

    public class OptionItemGrp
    {
        public OptionItemInfo[] optItmeInfo = new OptionItemInfo[6];
    }

    public class OptionItemInfo
    {
        public List<Controler.WoniPanel> menuBox = new List<Controler.WoniPanel>();
        public List<Controler.WoniPanel> menuImgBox = new List<Controler.WoniPanel>();
        public List<PictureBox> menuCheckBox = new List<PictureBox>();
        public List<Label> menuTitle = new List<Label>();
        public List<Label> menuSingleTitle = new List<Label>();
        public List<string> menuTCode = new List<string>();
    }
}
