using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Kiosk.Properties;
using static Kiosk.MenuTypeGrp;

namespace Kiosk
{
    public partial class FrmMenuList : Form, IFormMessage
    {
        //메인폼 통신용 이벤트
        public delegate void ReturnValueHandler(string msg);
        public event ReturnValueHandler returnValueEvent;

        //------멤버변수------//
        #region 멤버변수

        //메뉴그룹, 메뉴 DAO
        DBControl.MenuListCfgDAO menuListCfgDao = new DBControl.MenuListCfgDAO();
        DBControl.MenuGrpCfgDAO menuGrpCfgDao = new DBControl.MenuGrpCfgDAO();

        //메뉴선택화면 자동 숨김용 타이머
        System.Timers.Timer waitTime = new System.Timers.Timer(StoreInfo.BWaitTime * 1000);

        //주문목록 리스트셋
        WoniOrderListSet orderListSet = new WoniOrderListSet();

        //메뉴그룹의 타입 리스트
        List<MenuTypeGrp> menuGrpList = new List<MenuTypeGrp>();

        //메뉴그룹 리스트
        List<MenuItemProperty> menuGrpBoxList = new List<MenuItemProperty>();

        //메뉴그룹 정보
        MenuGrpInfo grpInfo;
        
        FrmMain frmMain;
        //메뉴그룹 표출용 패널
        Panel grpPnl;
        Point menuBoxOldPoint;
        Size menuBoxOldSize;


        //MouseDown, MouseUp 컨트롤 이름 저장용
        string ctrlName = string.Empty;
        string plusMinusName = string.Empty;

        //방금 추가한 주문목록 깜빡임용 인덱스
        int nRunAnsyIndex;
        int nRunOldAnsyIndex;
        //선택한 주문목록 +/- 버튼의 인덱스 저장용
        int nPlusMinusBtnIndex;
        //선택한 메뉴그룹 인덱스 저장용
        int nGrpIndex = 0;
        int nGrpOldIndex = -1;
        //메뉴 그룹 현재 페이지
        int nPage = 1;
        //메뉴선택화면 표출 시,
        //사운드 재생을 막기위한 플래그
        bool isFirstSound = false;
        //메뉴 그룹 페이징 플래그
        bool isMenuGrpPaging = false;

        #endregion

        public FrmMenuList()
        {
            InitializeComponent();           
        }

        public FrmMenuList(FrmMain main)
        {
            InitializeComponent();

            this.frmMain = main;
            this.frmMain.sendValueEvent += new FrmMain.SendValueHandler(frmMain_sendValueEvent);
            waitTime.Elapsed += OnTimedEvent;

            menuGrpCfgDao.BRAND = menuListCfgDao.BRAND = StoreInfo.BrnadCode;
            //menuGrpCfgDao.BRAND = menuListCfgDao.BRAND = "00002";
            menuGrpCfgDao.STORE = menuListCfgDao.STORE = StoreInfo.StoreCode;
            menuGrpCfgDao.DESK = menuListCfgDao.DESK = StoreInfo.StoreDesk;
        }

        //------메서드------//
        #region 메서드

        /// <summary>
        /// 메뉴 선택 메서드
        /// </summary>
        private void MenuBoxMouseUp()
        {
            //선택한 메뉴의 테두리 색상 변경
            //확대된 이미지를 이전 사이즈와 움직인 위치를 이전 위치로 원복
            switch (menuGrpList[nGrpIndex].type)
            {
                case "1":
                    ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.FromArgb(61, 155, 35);
                    ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location = menuBoxOldPoint;
                    ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size = menuBoxOldSize;

                    //for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                    //    ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;
                    break;

                case "2":
                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.FromArgb(61, 155, 35);
                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location = menuBoxOldPoint;
                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size = menuBoxOldSize;

                    //for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                    //    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;
                    break;

                case "3":
                    ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.FromArgb(61, 155, 35);
                    ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location = menuBoxOldPoint;
                    ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size = menuBoxOldSize;

                    //for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                    //    ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;
                    break;
            }

            UtilHelper.Delay(50);
            MenuType_Popup();

            //선택한 메뉴의 테두리 색상 원복
            switch(menuGrpList[nGrpIndex].type)
            {
                case "1":
                    ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.FromArgb(96, 64, 48);
                    break;

                case "2":
                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.FromArgb(96, 64, 48);
                    break;

                case "3":
                    ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.FromArgb(96, 64, 48);
                    break;
            }
        }

        /// <summary>
        /// 메뉴 타입에 따른 옵션 선택 폼 팝업
        /// </summary>
        private void MenuType_Popup()
        {
            FrmType1Popup type1Popup;
            FrmType2Popup type2Popup;
            FrmType3Popup type3Popup;

            switch (menuGrpList[nGrpIndex].type)
            {
                case "1":
                    type1Popup = new FrmType1Popup(this, UtilHelper.ScreenCapture(this.Width, this.Height, this.Location),
                        menuGrpList[nGrpIndex].name,
                        ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[menuGrpBoxList[nGrpIndex].menuSelectIndex].Text,
                        ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuTCode[menuGrpBoxList[nGrpIndex].menuSelectIndex],
                        ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuMemo[menuGrpBoxList[nGrpIndex].menuSelectIndex]);

                    if (type1Popup.ShowDialog() == DialogResult.OK)
                    {
                        OrderListInfo.ItemType.Add(menuGrpList[nGrpIndex].type);
                        OrderListInfo.ItemName.Add(type1Popup.productTitle);
                        OrderListInfo.ItemOpNm.Add(type1Popup.gubn);
                        OrderListInfo.ItemGNam.Add(menuGrpList[nGrpIndex].name);
                        OrderListInfo.ItemQuan.Add(1);
                        OrderListInfo.ItemOptAmt1.Add(type1Popup.optAmt1);
                        OrderListInfo.ItemOptAmt2.Add(type1Popup.optAmt2);
                        OrderListInfo.ItemAmt.Add(type1Popup.totAmt);
                        OrderListInfo.Memo1.Add(type1Popup.memo1);
                        OrderListInfo.Memo2.Add(type1Popup.memo2);
                        OrderListInfo.ItemTCode.Add(
                        ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuTCode[menuGrpBoxList[nGrpIndex].menuSelectIndex]);

                        CreateCustomOrList(nGrpIndex);

                        for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;

                        waitTime.Start();
                    }
                    else if (type1Popup.DialogResult == DialogResult.Cancel)
                    {
                        for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;

                        waitTime.Start();
                    }
                    else if (type1Popup.DialogResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;

                        BackToMain();
                    }
                    break;

                case "2":
                    type2Popup = new FrmType2Popup(this, UtilHelper.ScreenCapture(this.Width, this.Height, this.Location),
                        menuGrpList[nGrpIndex].name,
                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[menuGrpBoxList[nGrpIndex].menuSelectIndex].Text,
                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTCode[menuGrpBoxList[nGrpIndex].menuSelectIndex],
                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuMemo[menuGrpBoxList[nGrpIndex].menuSelectIndex]);

                    if (type2Popup.ShowDialog() == DialogResult.OK)
                    {
                        switch (type2Popup.gubn)
                        {
                            case "선택":
                            case "단품":
                                OrderListInfo.ItemOpNm.Add(string.Empty);
                                break;

                            default:
                                OrderListInfo.ItemOpNm.Add(type2Popup.gubn);
                                break;
                        }

                        OrderListInfo.ItemType.Add(menuGrpList[nGrpIndex].type);
                        OrderListInfo.ItemName.Add(type2Popup.productTitle);
                        //OrderListInfo.ItemOpNm.Add(type2Popup.gubn);
                        OrderListInfo.ItemGNam.Add(menuGrpList[nGrpIndex].name);
                        OrderListInfo.ItemQuan.Add(1);
                        OrderListInfo.ItemOptAmt1.Add(type2Popup.optAmt1);
                        OrderListInfo.ItemOptAmt2.Add(type2Popup.optAmt2);
                        OrderListInfo.ItemAmt.Add(type2Popup.totAmt);
                        OrderListInfo.Memo1.Add(type2Popup.memo1);
                        OrderListInfo.Memo2.Add(type2Popup.memo2);
                        OrderListInfo.ItemTCode.Add(
                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTCode[menuGrpBoxList[nGrpIndex].menuSelectIndex]);

                        CreateCustomOrList(nGrpIndex);

                        for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        {
                            if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].soldOut[i] == false)
                                ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;
                        }
                        waitTime.Start();
                    }
                    else if (type2Popup.DialogResult == DialogResult.Cancel)
                    {

                        for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        {
                            if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].soldOut[i] == false)
                                ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;
                        }

                        waitTime.Start();
                    }
                    else if (type2Popup.DialogResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        {
                            if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].soldOut[i] == false)
                                ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;
                        }

                        BackToMain();
                    }
                    break;

                case "3":
                    type3Popup = new FrmType3Popup(this, UtilHelper.ScreenCapture(this.Width, this.Height, this.Location),
                        menuGrpList[nGrpIndex].name,
                        ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[menuGrpBoxList[nGrpIndex].menuSelectIndex].Text,
                        ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuTCode[menuGrpBoxList[nGrpIndex].menuSelectIndex],
                        ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuMemo[menuGrpBoxList[nGrpIndex].menuSelectIndex]);

                    if (type3Popup.ShowDialog() == DialogResult.OK)
                    {
                        switch (type3Popup.gubn)
                        {
                            case "선택":
                            case "단품":
                                OrderListInfo.ItemOpNm.Add(string.Empty);
                                break;

                            default:
                                OrderListInfo.ItemOpNm.Add(type3Popup.gubn);
                                break;
                        }

                        OrderListInfo.ItemType.Add(menuGrpList[nGrpIndex].type);
                        OrderListInfo.ItemName.Add(type3Popup.productTitle);
                        //OrderListInfo.ItemOpNm.Add(type3Popup.gubn);
                        OrderListInfo.ItemGNam.Add(menuGrpList[nGrpIndex].name);
                        OrderListInfo.ItemQuan.Add(1);
                        OrderListInfo.ItemOptAmt1.Add(type3Popup.optAmt1);
                        OrderListInfo.ItemOptAmt2.Add(type3Popup.optAmt2);
                        OrderListInfo.ItemAmt.Add(type3Popup.totAmt);
                        OrderListInfo.Memo1.Add(type3Popup.memo1);
                        OrderListInfo.Memo2.Add(type3Popup.memo2);
                        OrderListInfo.ItemTCode.Add(
                        ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuTCode[menuGrpBoxList[nGrpIndex].menuSelectIndex]);

                        CreateCustomOrList(nGrpIndex);

                        for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;

                        waitTime.Start();
                    }
                    else if (type3Popup.DialogResult == DialogResult.Cancel)
                    {
                        for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;

                        waitTime.Start();
                    }
                    else if (type3Popup.DialogResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = true;

                        BackToMain();
                    }
                    break;
            }

        }

        /// <summary>
        /// 메뉴 선택 완료 시, 주문목록에 메뉴 추가 메서드
        /// </summary>
        /// <param name="gprIndex"></param>
        private void CreateCustomOrList(int gprIndex)
        {
            if (orderListSet.panelTitle.Count != 0)
            {
                for (int i = 0; i < orderListSet.panelTitle.Count; i++)
                {
                    if (OrderListInfo.ItemOpNm[OrderListInfo.ItemOpNm.Count - 1].CompareTo("세트") == 0)
                    {
                        if (orderListSet.labelTitle[i].Text.CompareTo(OrderListInfo.ItemName[OrderListInfo.ItemName.Count - 1] + " " +
                            OrderListInfo.ItemOpNm[OrderListInfo.ItemOpNm.Count - 1]) == 0 && UtilHelper.SubGap(orderListSet.labelMemo[i].Text).CompareTo(
                            OrderListInfo.Memo1[OrderListInfo.Memo1.Count - 1] + OrderListInfo.Memo2[OrderListInfo.Memo2.Count - 1]) == 0)
                        {
                            orderListSet.labelQuan[i].Text = (orderListSet.labelQuan[i].Text.ToInteger() + 1).ToString();
                            orderListSet.labelAmt[i].Text = string.Format(@"\{0:#,##0}", orderListSet.labelAmt[i].Text.ToDecimal() +
                                OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1]);
                            lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", lblTotalAmt.Text.ToDecimal() +
                                OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1]);
                            OrderListInfo.ItemQuan[OrderListInfo.ItemQuan.Count - 1] = orderListSet.labelQuan[i].Text.ToInteger();
                            OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1] = orderListSet.labelAmt[i].Text.ToDecimal();

                            OrderListInfo.ItemType.RemoveAt(i);
                            OrderListInfo.ItemName.RemoveAt(i);
                            OrderListInfo.ItemAmt.RemoveAt(i);
                            OrderListInfo.ItemOpNm.RemoveAt(i);
                            OrderListInfo.ItemGNam.RemoveAt(i);
                            OrderListInfo.ItemOptAmt1.RemoveAt(i);
                            OrderListInfo.ItemOptAmt2.RemoveAt(i);
                            OrderListInfo.ItemQuan.RemoveAt(i);
                            OrderListInfo.Memo1.RemoveAt(i);
                            OrderListInfo.Memo2.RemoveAt(i);

                            nRunAnsyIndex = i;

                            if (backgroundWorker1.IsBusy == true)
                                backgroundWorker1.Dispose();
                            else
                                backgroundWorker1.RunWorkerAsync();

                            return;
                        }
                    }
                    else
                    {
                        if (orderListSet.labelTitle[i].Text.CompareTo(OrderListInfo.ItemName[OrderListInfo.ItemName.Count - 1] + " " +
                            OrderListInfo.ItemOpNm[OrderListInfo.ItemOpNm.Count - 1]) == 0 &&
                            orderListSet.labelMemo[i].Text.Trim().CompareTo(OrderListInfo.Memo1[OrderListInfo.Memo1.Count - 1]) == 0)
                        //if (orderListSet.labelTitle[i].Text.CompareTo(OrderListInfo.ItemName[OrderListInfo.ItemName.Count - 1]) == 0)
                        {
                            orderListSet.labelQuan[i].Text = (orderListSet.labelQuan[i].Text.ToInteger() + 1).ToString();
                            orderListSet.labelAmt[i].Text = string.Format(@"\{0:#,##0}", orderListSet.labelAmt[i].Text.ToDecimal() +
                                OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1]);
                            //lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", orderListSet.labelAmt[i].Text.ToDecimal());// +
                            //OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1]);
                            lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", lblTotalAmt.Text.ToDecimal() +
                                OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1]);
                            OrderListInfo.ItemQuan[OrderListInfo.ItemQuan.Count - 1] = orderListSet.labelQuan[i].Text.ToInteger();
                            OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1] = orderListSet.labelAmt[i].Text.ToDecimal();

                            OrderListInfo.ItemType.RemoveAt(i);
                            OrderListInfo.ItemName.RemoveAt(i);
                            OrderListInfo.ItemAmt.RemoveAt(i);
                            OrderListInfo.ItemOpNm.RemoveAt(i);
                            OrderListInfo.ItemGNam.RemoveAt(i);
                            OrderListInfo.ItemOptAmt1.RemoveAt(i);
                            OrderListInfo.ItemOptAmt2.RemoveAt(i);
                            OrderListInfo.ItemQuan.RemoveAt(i);
                            OrderListInfo.Memo1.RemoveAt(i);
                            OrderListInfo.Memo2.RemoveAt(i);

                            nRunAnsyIndex = i;

                            if (backgroundWorker1.IsBusy == true)
                                backgroundWorker1.Dispose();
                            else
                                backgroundWorker1.RunWorkerAsync();

                            return;
                        }
                    }
                }
            }

            DoubleBuffer_Panel listMainPanel = new DoubleBuffer_Panel();
            listMainPanel.Height = 60;

            DoubleBuffer_Panel listTitlePanel = new DoubleBuffer_Panel();
            listTitlePanel.Name = "listTitlePanel_" + orderListSet.panelTitle.Count;
            listTitlePanel.Size = new Size(340, 60);
            listTitlePanel.BackColor = Color.Transparent;

            Label listTitle = new Label();
            listTitle.Name = "listTitle_" + orderListSet.labelTitle.Count;
            listTitle.AutoSize = false;
            listTitle.Dock = DockStyle.Top;
            listTitle.Height = 48;
            listTitle.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
            listTitle.TextAlign = ContentAlignment.MiddleLeft;

            Label listMemo = new Label();
            listMemo.Name = "ListMemo_" + orderListSet.labelMemo.Count;
            listMemo.AutoSize = false;
            listMemo.Dock = DockStyle.Bottom;
            listMemo.Height = 12;
            listMemo.Font = new Font("나눔스퀘어", 8, FontStyle.Bold);
            listMemo.TextAlign = ContentAlignment.MiddleLeft;
            listMemo.ForeColor = Color.RoyalBlue;

            PictureBox listPlusBtn = new PictureBox();
            listPlusBtn.Name = "ListPlusBtn_" + orderListSet.picBoxBtnPlus.Count;
            listPlusBtn.Size = new Size(43, 43);
            listPlusBtn.Location = new Point(listTitlePanel.Width + 2, 3);
            listPlusBtn.BackgroundImage = Resources.btn_plus;
            listPlusBtn.MouseDown += new MouseEventHandler(picBoxPlusMinusBtn_MouseDown);
            listPlusBtn.MouseUp += new MouseEventHandler(picBoxPlusMinusBtn_MouseUp);

            Label listQuan = new Label();
            listQuan.Name = "ListQuan_" + orderListSet.labelQuan.Count;
            listQuan.AutoSize = false;
            listQuan.Size = new Size(43, 60);
            listQuan.Location = new Point(listTitlePanel.Width + listPlusBtn.Width + 4, 0);
            listQuan.Font = new Font("나눔스퀘어", 16, FontStyle.Bold);
            listQuan.TextAlign = ContentAlignment.MiddleCenter;
            listQuan.BackColor = Color.Transparent;

            PictureBox listMinusBtn = new PictureBox();
            listMinusBtn.Name = "ListMinusBtn_" + orderListSet.picBoxBtnMinus.Count;
            listMinusBtn.Size = new Size(43, 43);
            listMinusBtn.Location = new Point(listTitlePanel.Width + listPlusBtn.Width + listQuan.Width + 6, 3);
            listMinusBtn.BackgroundImage = Properties.Resources.btn_minus;
            listMinusBtn.MouseDown += new MouseEventHandler(picBoxPlusMinusBtn_MouseDown);
            listMinusBtn.MouseUp += new MouseEventHandler(picBoxPlusMinusBtn_MouseUp);

            Label listAmt = new Label();
            listAmt.Name = "ListAmt_" + orderListSet.labelAmt.Count;
            listAmt.AutoSize = false;
            listAmt.Size = new Size(130, 60);
            listAmt.Location = new Point(listTitlePanel.Width + listPlusBtn.Width + listQuan.Width + listMinusBtn.Width, 0);
            listAmt.Font = new Font("나눔스퀘어", 16, FontStyle.Bold);
            listAmt.TextAlign = ContentAlignment.MiddleRight;
            listAmt.BackColor = Color.Transparent;

            listTitle.Text = OrderListInfo.ItemName[OrderListInfo.ItemName.Count - 1] + " " +
                OrderListInfo.ItemOpNm[OrderListInfo.ItemOpNm.Count - 1];

            if (OrderListInfo.ItemOpNm[OrderListInfo.ItemOpNm.Count - 1].CompareTo("세트") == 0)
            {
                if (OrderListInfo.Memo1[OrderListInfo.Memo1.Count - 1].Length != 0 &&
                    OrderListInfo.Memo2[OrderListInfo.Memo2.Count - 1].Length != 0)
                    listMemo.Text = OrderListInfo.Memo1[OrderListInfo.Memo1.Count - 1] + "," + OrderListInfo.Memo2[OrderListInfo.Memo2.Count - 1];
                else if (OrderListInfo.Memo1[OrderListInfo.Memo1.Count - 1].Length != 0 &&
                    OrderListInfo.Memo2[OrderListInfo.Memo2.Count - 1].Length == 0)
                    listMemo.Text = OrderListInfo.Memo1[OrderListInfo.Memo1.Count - 1];
                else if (OrderListInfo.Memo1[OrderListInfo.Memo1.Count - 1].Length == 0 &&
                    OrderListInfo.Memo2[OrderListInfo.Memo2.Count - 1].Length != 0)
                    listMemo.Text = OrderListInfo.Memo2[OrderListInfo.Memo2.Count - 1];
            }
            else
            {
                //listMemo.Text = OrderListInfo.Memo1[OrderListInfo.Memo1.Count - 1];
            }
            listQuan.Text = "1";
            listAmt.Text = string.Format(@"\{0:##,#0}", OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1]);
            lblTotalAmt.Text = string.Format(@"\ {0:##,#0}", lblTotalAmt.Text.ToDecimal() +
                OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1].ToDecimal());
            orderListSet.danga.Add(OrderListInfo.ItemAmt[OrderListInfo.ItemAmt.Count - 1]);

            orderListSet.panelMain.Add(listMainPanel);
            orderListSet.panelTitle.Add(listTitlePanel);
            orderListSet.picBoxBtnPlus.Add(listPlusBtn);
            orderListSet.picBoxBtnMinus.Add(listMinusBtn);
            orderListSet.labelTitle.Add(listTitle);
            orderListSet.labelMemo.Add(listMemo);
            orderListSet.labelQuan.Add(listQuan);
            orderListSet.labelAmt.Add(listAmt);

            listTitlePanel.Controls.Add(listTitle);
            listTitlePanel.Controls.Add(listMemo);
            listMainPanel.Controls.Add(listTitlePanel);
            listMainPanel.Controls.Add(listPlusBtn);
            listMainPanel.Controls.Add(listQuan);
            listMainPanel.Controls.Add(listMinusBtn);
            listMainPanel.Controls.Add(listAmt);

            woniFillList1.Controls.Add(listMainPanel);
            woniFillList1.VerticalScroll.Value = woniFillList1.VerticalScroll.Maximum - orderListSet.panelMain[0].Height;
            woniFillList1.AutoScrollPosition = new Point(0, woniFillList1.Height);

            nRunAnsyIndex = orderListSet.panelMain.Count - 1;

            if (backgroundWorker1.IsBusy == true)
                backgroundWorker1.Dispose();
            else
                backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// 주문목록의 메뉴 개수 +/- 메서드
        /// </summary>
        /// <param name="ctrlName">호출한 컨트롤의 Name</param>
        private void ProcPlusMinusBtn(string ctrlName)
        {
            switch (ctrlName)
            {
                case "ListPlus":
                    orderListSet.labelQuan[nPlusMinusBtnIndex].Text =
                        (orderListSet.labelQuan[nPlusMinusBtnIndex].Text.ToInteger() + 1).ToString();
                    orderListSet.labelAmt[nPlusMinusBtnIndex].Text =
                        string.Format(@"\{0:##,#0}", orderListSet.labelAmt[nPlusMinusBtnIndex].Text.ToDecimal() +
                        orderListSet.danga[nPlusMinusBtnIndex]);

                    OrderListInfo.ItemQuan[nPlusMinusBtnIndex] = orderListSet.labelQuan[nPlusMinusBtnIndex].Text.ToInteger();
                    OrderListInfo.ItemAmt[nPlusMinusBtnIndex] = orderListSet.labelAmt[nPlusMinusBtnIndex].Text.ToDecimal();
                    lblTotalAmt.Text = string.Format(@"\ {0:##,#0}", lblTotalAmt.Text.ToDecimal() + orderListSet.danga[nPlusMinusBtnIndex]);
                    orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].Enabled = true;
                    break;

                case "ListMinus":
                    orderListSet.labelQuan[nPlusMinusBtnIndex].Text =
                        (orderListSet.labelQuan[nPlusMinusBtnIndex].Text.ToInteger() - 1).ToString();
                    orderListSet.labelAmt[nPlusMinusBtnIndex].Text =
                        string.Format(@"\{0:##,#0}", orderListSet.labelAmt[nPlusMinusBtnIndex].Text.ToDecimal() - orderListSet.danga[nPlusMinusBtnIndex]);
                    OrderListInfo.ItemQuan[nPlusMinusBtnIndex] = orderListSet.labelQuan[nPlusMinusBtnIndex].Text.ToInteger();
                    OrderListInfo.ItemAmt[nPlusMinusBtnIndex] = orderListSet.labelAmt[nPlusMinusBtnIndex].Text.ToDecimal();
                    lblTotalAmt.Text = string.Format(@"\ {0:##,#0}", lblTotalAmt.Text.ToDecimal() - orderListSet.danga[nPlusMinusBtnIndex]);
                    orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].Enabled = true;
                    break;

                case "ListCancel":
                    FrmOrderCancelBox orderDelBox = new FrmOrderCancelBox(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location),
                        orderListSet.labelTitle[nPlusMinusBtnIndex].Text);

                    if (orderDelBox.ShowDialog() == DialogResult.OK)
                    {
                        if (nPlusMinusBtnIndex == nRunAnsyIndex && backgroundWorker1.IsBusy)
                            backgroundWorker1.Dispose();
                        lblTotalAmt.Text = string.Format(@"\ {0:##,#0}", lblTotalAmt.Text.ToDecimal() - orderListSet.danga[nPlusMinusBtnIndex]);

                        woniFillList1.Controls.Remove(orderListSet.panelMain[nPlusMinusBtnIndex]);
                        orderListSet.panelMain.RemoveAt(nPlusMinusBtnIndex);
                        orderListSet.panelTitle.RemoveAt(nPlusMinusBtnIndex);
                        orderListSet.labelTitle.RemoveAt(nPlusMinusBtnIndex);
                        orderListSet.labelMemo.RemoveAt(nPlusMinusBtnIndex);
                        orderListSet.labelQuan.RemoveAt(nPlusMinusBtnIndex);
                        orderListSet.labelAmt.RemoveAt(nPlusMinusBtnIndex);
                        orderListSet.picBoxBtnMinus.RemoveAt(nPlusMinusBtnIndex);
                        orderListSet.picBoxBtnPlus.RemoveAt(nPlusMinusBtnIndex);
                        orderListSet.danga.RemoveAt(nPlusMinusBtnIndex);

                        OrderListInfo.ItemName.RemoveAt(nPlusMinusBtnIndex);
                        OrderListInfo.ItemAmt.RemoveAt(nPlusMinusBtnIndex);
                        OrderListInfo.ItemOpNm.RemoveAt(nPlusMinusBtnIndex);
                        OrderListInfo.ItemGNam.RemoveAt(nPlusMinusBtnIndex);
                        OrderListInfo.ItemOptAmt1.RemoveAt(nPlusMinusBtnIndex);
                        OrderListInfo.ItemOptAmt2.RemoveAt(nPlusMinusBtnIndex);
                        OrderListInfo.ItemQuan.RemoveAt(nPlusMinusBtnIndex);
                        OrderListInfo.Memo1.RemoveAt(nPlusMinusBtnIndex);
                        OrderListInfo.Memo2.RemoveAt(nPlusMinusBtnIndex);

                        for (int i = 0; i < orderListSet.picBoxBtnPlus.Count; i++)
                        {
                            orderListSet.picBoxBtnMinus[i].Name = "ListMinusBtn_" + i;
                            orderListSet.picBoxBtnPlus[i].Name = "ListPlusBtn_" + i;
                        }
                    }
                    else
                        orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// 메뉴리스트 폼 초기화
        /// </summary>
        private void MenuListLayoutInit()
        {
            grpInfo = new MenuGrpInfo();
            InitGrpList();
            CreateGrpList(nPage);
            CreateMenuList();
        }

        /// <summary>
        /// 메뉴그룹 초기화
        /// (버튼표출 최대개수를 넘어가면 페이징 활성화)
        /// </summary>
        private void InitGrpList()
        {
            int nGrpCount = menuGrpCfgDao.MenuGrpCfgSelect().Rows.Count;

            for (int i = 0; i < nGrpCount; i++)
            {
                PictureBox grpBox = new PictureBox();
                grpInfo.Add(grpBox, i, grpBox_MouseDown, grpBox_MouseUp);
            }

            using (DataTable dt = menuGrpCfgDao.MenuGrpCfgSelect())
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        int index = 0;

                        while (dtr.Read())
                        {
                            grpInfo.grpBoxLabelList[index].Text = dtr["MG_NAME"].ToString();
                            grpInfo.grpBoxType[index] = dtr["MG_TYPE"].ToString();

                            index++;
                        }

                        dtr.Close();
                    }
                }
            }

            //페이징 활성화
            if (nGrpCount > StoreInfo.MaxTap)
            {
                isMenuGrpPaging = true;

                PictureBox left = new PictureBox();

                left.Location = new Point(0, 0);
                left.Size = new Size(100, panelMenuGrp.Height);
                left.BackgroundImage = Resources.btn_arrow_left;
                left.Name = "Left";
                left.MouseDown += grpPaging_MouseDown;
                left.MouseUp += grpPaging_MouseUp;
                panelMenuGrp.Controls.Add(left);

                PictureBox right = new PictureBox();

                right.Location = new Point(panelMenuGrp.Width - 100, 0);
                right.Size = new Size(100, panelMenuGrp.Height);
                right.Name = "Right";
                right.BackgroundImage = Resources.btn_arrow_right;
                right.MouseDown += grpPaging_MouseDown;
                right.MouseUp += grpPaging_MouseUp;
                panelMenuGrp.Controls.Add(right);

                //페이징 버튼을 제외한 영역에
                //카테고리 버튼용 패널 생성
                grpPnl = new Panel()
                {
                    Location = new Point(100, 0),
                    Size = new Size(panelMenuGrp.Width - 200, panelMenuGrp.Height),
                    BackColor = Color.White
                };

                panelMenuGrp.Controls.Add(grpPnl);
            }
        }

        /// <summary>
        /// 지정된 페이지의 메뉴그룹 버튼을 버튼표출 최대개수만큼 생성
        /// </summary>
        /// <param name="page">현재 페이지</param>
        private void CreateGrpList(int page)
        {
            Point grpBoxLoc;
            Size grpBoxSize;

            //페이징여부에 따른 패널 생성
            if (isMenuGrpPaging)
            {
                grpBoxLoc = new Point(0, 0);
                grpBoxSize = new Size(grpPnl.Width, grpPnl.Height);
                grpPnl.Controls.Clear();
            }
            else
            {
                grpBoxLoc = new Point(0, 0);
                grpBoxSize = new Size(panelMenuGrp.Width, panelMenuGrp.Height);
                panelMenuGrp.Controls.Clear();
            }

            int grpBoxNum = grpInfo.grpBoxList.Count / (page * StoreInfo.MaxTap) >= 1 ?
                StoreInfo.MaxTap : (grpInfo.grpBoxList.Count - ((page - 1) * StoreInfo.MaxTap)) % StoreInfo.MaxTap;

            for (int i = 0; i < grpBoxNum; i++, grpBoxLoc.X += grpBoxSize.Width / StoreInfo.MaxTap)
            {
                grpInfo.grpBoxList[i + ((page - 1) * StoreInfo.MaxTap)].Size = new Size(grpBoxSize.Width / StoreInfo.MaxTap, grpBoxSize.Height);
                grpInfo.grpBoxList[i + ((page - 1) * StoreInfo.MaxTap)].Location = new Point(grpBoxLoc.X, 0);

                //페이징여부에 따라 각 패널에 버튼 추가
                if (isMenuGrpPaging)
                    grpPnl.Controls.Add(grpInfo.grpBoxList[i + ((page - 1) * 4)]);
                else
                    panelMenuGrp.Controls.Add(grpInfo.grpBoxList[i + ((page - 1) * 4)]);
            }
        }

        /// <summary>
        /// 메뉴 그룹별로 메뉴생성(최대 5 x 10개)
        /// </summary>
        private void CreateMenuList()
        {
            for (int i = 0; i < grpInfo.grpBoxList.Count; i++)
            {
                switch (grpInfo.grpBoxType[i])
                {
                    case "1":
                        MenuTypeGrp menuGrpType1 = new MenuTypeGrp1();
                        menuGrpType1.name = grpInfo.grpBoxLabelList[i].Text;
                        menuGrpType1.type = "1";
                        menuGrpList.Add(menuGrpType1);
                        break;

                    case "2":
                        MenuTypeGrp menuGrpType2 = new MenuTypeGrp2();
                        menuGrpType2.name = grpInfo.grpBoxLabelList[i].Text;
                        menuGrpType2.type = "2";
                        menuGrpList.Add(menuGrpType2);
                        break;

                    case "3":
                        MenuTypeGrp menuGrpType3 = new MenuTypeGrp3();
                        menuGrpType3.name = grpInfo.grpBoxLabelList[i].Text;
                        menuGrpType3.type = "3";
                        menuGrpList.Add(menuGrpType3);
                        break;
                }
            }

            if (menuGrpList.Count != 0)
            {
                for (int i = 0; i < menuGrpList.Count; i++)
                {
                    switch (menuGrpList[i].type)
                    {
                        case "1":
                            for (int j = 0; j < ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1.Length; j++)
                            {
                                ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j] = new MenuListType1();
                                CreateType1_MenuList(i, j);
                            }
                            break;

                        case "2":
                            for (int j = 0; j < ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2.Length; j++)
                            {
                                ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j] = new MenuListType2();
                                CreateType2_MenuList(i, j);
                            }
                            break;

                        case "3":
                            for (int j = 0; j < ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3.Length; j++)
                            {
                                ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j] = new MenuListType3();
                                CreateType3_MenuList(i, j);
                            }
                            break;
                    }
                }

                GetMenuListCfgSelect();
            }
        }

        /// <summary>
        /// 메뉴 타입1 메뉴리스트 생성 메서드
        /// </summary>
        /// <param name="x">메뉴그룹 인덱스</param>
        /// <param name="z">메뉴리스트 페이지</param>
        private void CreateType1_MenuList(int x, int z)
        {
            panelMenuList.SuspendLayout();
            Point menuBoxLoc = new Point(5, 12);
            Size menuBoxSize = new Size(panelMenuList.Width, panelMenuList.Height);

            int j = 0, k = 0;

            for (int i = 0; i < 10; i++, menuBoxLoc.X += menuBoxSize.Width / 2)
            {
                if (j == 2)
                {
                    menuBoxLoc.Y += menuBoxSize.Height / 5;
                    menuBoxLoc.X = 5;
                    j = 0;
                    k++;
                }
                if (k == 5)
                    break;
                j++;

                DoubleBuffer_Panel menuBox = new DoubleBuffer_Panel();
                menuBox.Location = menuBoxLoc;
                menuBox.Size = new Size(menuBoxSize.Width / 2 - 10, menuBoxSize.Height / 5 - 10);
                menuBox.Name = "MenuBox_" + i;
                menuBox.BorderSize = 5;
                menuBox.BorderColor = Color.FromArgb(61, 155, 35);
                menuBox.BorderRadius = 80;
                menuBox.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuBox.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                DoubleBuffer_Panel menuImgBox = new DoubleBuffer_Panel();
                menuImgBox.Location = new Point(20, 24);
                menuImgBox.Size = new Size(178, 146);
                menuImgBox.Name = "MenuImgBox_" + i;
                menuImgBox.BorderColor = Color.FromArgb(240, 130, 00);
                menuImgBox.BorderSize = 3;
                menuImgBox.BorderRadius = 80;
                menuImgBox.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuImgBox.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuTitle = new Label();
                menuTitle.AutoSize = false;
                menuTitle.Location = new Point(205, 16);
                menuTitle.Size = new Size(270, 70);
                menuTitle.Name = "MenuTitle_" + i;
                menuTitle.Font = new Font("나눔스퀘어", 16, FontStyle.Bold);
                menuTitle.TextAlign = ContentAlignment.MiddleRight;
                menuTitle.Text = "";
                menuTitle.BackColor = Color.Transparent;
                menuTitle.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuTitle.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuSingleTitle = new Label();
                menuSingleTitle.AutoSize = false;
                menuSingleTitle.Location = new Point(200, 68);
                menuSingleTitle.Size = new Size(menuTitle.Width / 2, menuTitle.Height);
                menuSingleTitle.Name = "MenuSingleTitle_" + i;
                menuSingleTitle.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuSingleTitle.TextAlign = ContentAlignment.BottomRight;
                menuSingleTitle.Text = "";
                menuSingleTitle.BackColor = Color.Transparent;
                menuSingleTitle.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuSingleTitle.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuSetTitle = new Label();
                menuSetTitle.AutoSize = false;
                menuSetTitle.Location = new Point(200, 120);
                menuSetTitle.Size = new Size(menuSingleTitle.Width, menuSingleTitle.Height);
                menuSetTitle.Name = "MenuSetTitle_" + i;
                menuSetTitle.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuSetTitle.TextAlign = ContentAlignment.TopRight;
                menuSetTitle.Text = "";
                menuSetTitle.BackColor = Color.Transparent;
                menuSetTitle.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuSetTitle.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuSinglePrice = new Label();
                menuSinglePrice.AutoSize = false;
                menuSinglePrice.Location = new Point(menuSingleTitle.Location.X + menuSingleTitle.Width + 1, menuSingleTitle.Location.Y);
                menuSinglePrice.Size = new Size(menuSingleTitle.Width - 1, menuSingleTitle.Height);
                menuSinglePrice.Name = "MenuSinglePrice_" + i;
                menuSinglePrice.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuSinglePrice.TextAlign = ContentAlignment.BottomRight;
                menuSinglePrice.Text = "";
                menuSinglePrice.BackColor = Color.Transparent;
                menuSinglePrice.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuSinglePrice.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuSetPrice = new Label();
                menuSetPrice.AutoSize = false;
                menuSetPrice.Location = new Point(menuSetTitle.Location.X + menuSetTitle.Width + 1, menuSetTitle.Location.Y);
                menuSetPrice.Size = new Size(menuSetTitle.Width - 1, menuSetTitle.Height);
                menuSetPrice.Name = "MenuSetPrice_" + i;
                menuSetPrice.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuSetPrice.TextAlign = ContentAlignment.TopRight;
                menuSetPrice.Text = "";
                menuSetPrice.BackColor = Color.Transparent;
                menuSetPrice.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuSetPrice.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                menuBox.Controls.Add(menuImgBox);
                menuBox.Controls.Add(menuTitle);
                menuBox.Controls.Add(menuSingleTitle);
                menuBox.Controls.Add(menuSetTitle);
                menuBox.Controls.Add(menuSinglePrice);
                menuBox.Controls.Add(menuSetPrice);

                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuImgBox.Add(menuImgBox);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuTitle.Add(menuTitle);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuSingleTitle.Add(menuSingleTitle);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuSetTitle.Add(menuSetTitle);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuSingleAmt.Add(menuSinglePrice);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuSetAmt.Add(menuSetPrice);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuBox.Add(menuBox);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuTCode.Add(string.Empty);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].menuMemo.Add(string.Empty);
                ((MenuTypeGrp1)menuGrpList[x]).menuGrpType1[z].soldOut.Add(false);

            }
            panelMenuList.ResumeLayout(false);
        }

        /// <summary>
        /// 메뉴 타입2 메뉴리스트 생성 메서드
        /// </summary>
        /// <param name="x">메뉴그룹 인덱스</param>
        /// <param name="z">메뉴리스트 페이지</param>
        private void CreateType2_MenuList(int x, int z)
        {
            panelMenuList.SuspendLayout();
            Point menuBoxLoc = new Point(5, 12);
            Size menuBoxSize = new Size(panelMenuList.Width, panelMenuList.Height);

            int j = 0, k = 0;

            for (int i = 0; i < 10; i++, menuBoxLoc.X += menuBoxSize.Width / 2)
            {
                if (j == 2)
                {
                    menuBoxLoc.Y += menuBoxSize.Height / 5;
                    menuBoxLoc.X = 5;
                    j = 0;
                    k++;
                }
                if (k == 5)
                    break;
                j++;

                DoubleBuffer_Panel menuBox = new DoubleBuffer_Panel();
                menuBox.Location = menuBoxLoc;
                menuBox.Size = new Size(menuBoxSize.Width / 2 - 10, menuBoxSize.Height / 5 - 10);
                menuBox.Name = "MenuBox_" + i;
                menuBox.BorderColor = Color.FromArgb(96, 64, 48);
                menuBox.BorderSize = 8;
                menuBox.BorderRadius = 60;
                menuBox.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuBox.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                DoubleBuffer_Panel menuImgBox = new DoubleBuffer_Panel();
                menuImgBox.Location = new Point(20, 24);
                menuImgBox.Size = new Size(178, 146);
                menuImgBox.Name = "MenuImgBox_" + i;
                menuImgBox.BorderColor = Color.FromArgb(240, 130, 00);
                menuImgBox.BorderSize = 4;
                menuImgBox.BorderRadius = 60;
                menuImgBox.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuImgBox.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuTitle = new Label();
                menuTitle.AutoSize = false;
                //menuTitle.Location = new Point(220, 16);
                menuTitle.Location = new Point(220, 5);
                menuTitle.Size = new Size(270, 70);
                menuTitle.Name = "MenuTitle_" + i;
                menuTitle.Font = new Font("나눔스퀘어", 18, FontStyle.Bold);
                menuTitle.TextAlign = ContentAlignment.MiddleRight;
                menuTitle.Text = "";
                menuTitle.BackColor = Color.Transparent;
                menuTitle.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuTitle.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuSubTitle1 = new Label();
                menuSubTitle1.AutoSize = false;
                //menuSubTitle1.Location = new Point(225, 50);
                //menuSubTitle1.Location = new Point(225, 35);
                menuSubTitle1.Location = new Point(225, 24);
                menuSubTitle1.Size = new Size(menuTitle.Width / 2 + 40, menuTitle.Height);
                menuSubTitle1.Name = "MenuSingleTitle_" + i;
                menuSubTitle1.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuSubTitle1.TextAlign = ContentAlignment.BottomLeft;
                menuSubTitle1.Text = "";
                menuSubTitle1.BackColor = Color.Transparent;
                menuSubTitle1.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuSubTitle1.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuSubTitle2 = new Label();
                menuSubTitle2.AutoSize = false;
                //menuSubTitle2.Location = new Point(225, 110);
                menuSubTitle2.Location = new Point(225, 99);
                menuSubTitle2.Size = new Size(menuTitle.Width / 2 + 40, menuTitle.Height - 50);
                menuSubTitle2.Name = "MenuSetTitle_" + i;
                menuSubTitle2.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuSubTitle2.TextAlign = ContentAlignment.TopLeft;
                menuSubTitle2.Text = "";
                menuSubTitle2.BackColor = Color.Transparent;
                menuSubTitle2.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuSubTitle2.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuSubTitle3 = new Label();
                menuSubTitle3.AutoSize = false;
                //menuSubTitle3.Location = new Point(225, 136);
                menuSubTitle3.Location = new Point(225, 125);
                menuSubTitle3.Size = new Size(menuTitle.Width / 2 + 40, menuTitle.Height - 50);
                menuSubTitle3.Name = "MenuSinglePrice_" + i;
                menuSubTitle3.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuSubTitle3.TextAlign = ContentAlignment.TopLeft;
                menuSubTitle3.Text = "";
                menuSubTitle3.BackColor = Color.Transparent;
                menuSubTitle3.BringToFront();
                menuSubTitle3.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuSubTitle3.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuSubTitle4 = new Label();
                menuSubTitle4.AutoSize = false;
                //menuSubTitle4.Location = new Point(225, 162);
                menuSubTitle4.Location = new Point(225, 151);
                menuSubTitle4.Size = new Size(menuTitle.Width / 2 + 40, menuTitle.Height - 50);
                menuSubTitle4.Name = "MenuSinglePrice_" + i;
                menuSubTitle4.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuSubTitle4.TextAlign = ContentAlignment.TopLeft;
                menuSubTitle4.Text = "";
                menuSubTitle4.BackColor = Color.Transparent;
                menuSubTitle4.BringToFront();
                menuSubTitle4.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuSubTitle4.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuAmt1 = new Label();
                menuAmt1.AutoSize = false;
                menuAmt1.Location = new Point(menuSubTitle1.Location.X + menuSubTitle1.Width - 10, menuSubTitle1.Location.Y);
                menuAmt1.Size = new Size(menuSubTitle1.Width - 78, menuSubTitle1.Height);
                menuAmt1.Name = "MenuSetPrice_" + i;
                menuAmt1.Font = new Font("나눔스퀘어", 14, FontStyle.Bold);
                menuAmt1.TextAlign = ContentAlignment.BottomRight;
                menuAmt1.Text = "";
                menuAmt1.BackColor = Color.Transparent;
                menuAmt1.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuAmt1.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuAmt2 = new Label();
                menuAmt2.AutoSize = false;
                menuAmt2.Location = new Point(menuSubTitle2.Location.X + menuSubTitle2.Width - 10, menuSubTitle2.Location.Y);
                menuAmt2.Size = new Size(menuSubTitle2.Width - 78, menuSubTitle2.Height + 8);
                menuAmt2.Name = "MenuSetPrice_" + i;
                menuAmt2.Font = new Font("나눔고딕", 14, FontStyle.Bold);
                menuAmt2.TextAlign = ContentAlignment.TopRight;
                menuAmt2.Text = "";
                menuAmt2.BackColor = Color.Transparent;
                menuAmt2.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuAmt2.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuAmt3 = new Label();
                menuAmt3.AutoSize = false;
                menuAmt3.Location = new Point(menuSubTitle3.Location.X + menuSubTitle3.Width - 10, menuSubTitle3.Location.Y);
                menuAmt3.Size = new Size(menuSubTitle3.Width - 78, menuSubTitle3.Height);
                menuAmt3.Name = "MenuSetPrice_" + i;
                menuAmt3.Font = new Font("나눔고딕", 14, FontStyle.Bold);
                menuAmt3.TextAlign = ContentAlignment.TopRight;
                menuAmt3.Text = "";
                menuAmt3.BackColor = Color.Transparent;
                menuAmt3.BringToFront();
                menuAmt3.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuAmt3.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuAmt4 = new Label();
                menuAmt4.AutoSize = false;
                menuAmt4.Location = new Point(menuSubTitle3.Location.X + menuSubTitle3.Width - 10, menuSubTitle4.Location.Y);
                menuAmt4.Size = new Size(menuSubTitle3.Width - 78, menuSubTitle3.Height);
                menuAmt4.Name = "MenuSetPrice_" + i;
                menuAmt4.Font = new Font("나눔고딕", 14, FontStyle.Bold);
                menuAmt4.TextAlign = ContentAlignment.TopRight;
                menuAmt4.Text = "";
                menuAmt4.BackColor = Color.Transparent;
                menuAmt4.BringToFront();
                menuAmt4.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuAmt4.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                menuBox.Controls.Add(menuImgBox);
                menuBox.Controls.Add(menuTitle);
                menuBox.Controls.Add(menuSubTitle1);
                menuBox.Controls.Add(menuSubTitle2);
                menuBox.Controls.Add(menuSubTitle3);
                menuBox.Controls.Add(menuSubTitle4);
                menuBox.Controls.Add(menuAmt1);
                menuBox.Controls.Add(menuAmt2);
                menuBox.Controls.Add(menuAmt3);
                menuBox.Controls.Add(menuAmt4);

                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuImgBox.Add(menuImgBox);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuTitle.Add(menuTitle);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuSubTitle1.Add(menuSubTitle1);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuSubTitle2.Add(menuSubTitle2);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuSubTitle3.Add(menuSubTitle3);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuSubTitle4.Add(menuSubTitle4);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuAmt1.Add(menuAmt1);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuAmt2.Add(menuAmt2);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuAmt3.Add(menuAmt3);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuAmt4.Add(menuAmt4);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuBox.Add(menuBox);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuTCode.Add(string.Empty);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].menuMemo.Add(string.Empty);
                ((MenuTypeGrp2)menuGrpList[x]).menuGrpType2[z].soldOut.Add(false);
            }
            panelMenuList.ResumeLayout();
        }

        /// <summary>
        /// 메뉴 타입3 메뉴리스트 생성 메서드
        /// </summary>
        /// <param name="x">메뉴그룹 인덱스</param>
        /// <param name="z">메뉴리스트 페이지</param>
        private void CreateType3_MenuList(int x, int z)
        {
            panelMenuList.SuspendLayout();
            Point menuBoxLoc = new Point(5, 12);
            Size menuBoxSize = new Size(panelMenuList.Width, panelMenuList.Height);

            int j = 0, k = 0;

            for (int i = 0; i < 10; i++, menuBoxLoc.X += menuBoxSize.Width / 2)
            {
                if (j == 2)
                {
                    menuBoxLoc.Y += menuBoxSize.Height / 5;
                    menuBoxLoc.X = 5;
                    j = 0;
                    k++;
                }

                if (k == 5)
                    break;
                j++;

                DoubleBuffer_Panel menuBox = new DoubleBuffer_Panel();
                menuBox.Location = menuBoxLoc;
                menuBox.Size = new Size(menuBoxSize.Width / 2 - 10, menuBoxSize.Height / 5 - 10);
                menuBox.Name = "MenuBox_" + i;
                menuBox.BorderColor = Color.FromArgb(96, 64, 48);
                menuBox.BorderSize = 5;
                menuBox.BorderRadius = 80;
                menuBox.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuBox.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                DoubleBuffer_Panel menuImgBox = new DoubleBuffer_Panel();
                menuImgBox.Location = new Point(20, 24);
                menuImgBox.Size = new Size(178, 146);
                menuImgBox.Name = "MenuImgBox_" + i;
                menuImgBox.BorderColor = Color.FromArgb(240, 130, 00);
                menuImgBox.BorderSize = 2;
                menuImgBox.BorderRadius = 80;
                menuImgBox.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuImgBox.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                Label menuTitle = new Label();
                menuTitle.AutoSize = false;
                menuTitle.Location = new Point(205, 16);
                menuTitle.Size = new Size(270, 70);
                menuTitle.Name = "MenuTitle_" + i;
                menuTitle.Font = new Font("나눔고딕", 16, FontStyle.Bold);
                menuTitle.TextAlign = ContentAlignment.MiddleRight;
                menuTitle.Text = "";
                menuTitle.BackColor = Color.Transparent;
                menuTitle.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuTitle.MouseDown += new MouseEventHandler(menuBox_MouseDown);


                Label menuAmt1 = new Label();
                menuAmt1.AutoSize = false;
                menuAmt1.Location = new Point(200, 66);
                menuAmt1.Size = menuTitle.Size;
                menuAmt1.Name = "MenuSetPrice_" + i;
                menuAmt1.Font = new Font("나눔고딕", 14, FontStyle.Bold);
                menuAmt1.TextAlign = ContentAlignment.BottomRight;
                menuAmt1.Text = "";
                menuAmt1.BackColor = Color.Transparent;
                menuAmt1.MouseUp += new MouseEventHandler(menuBox_MouseUp);
                menuAmt1.MouseDown += new MouseEventHandler(menuBox_MouseDown);

                menuBox.Controls.Add(menuImgBox);
                menuBox.Controls.Add(menuTitle);
                menuBox.Controls.Add(menuAmt1);

                ((MenuTypeGrp3)menuGrpList[x]).menuGrpType3[z].menuImgBox.Add(menuImgBox);
                ((MenuTypeGrp3)menuGrpList[x]).menuGrpType3[z].menuTitle.Add(menuTitle);
                ((MenuTypeGrp3)menuGrpList[x]).menuGrpType3[z].menuAmt.Add(menuAmt1);
                ((MenuTypeGrp3)menuGrpList[x]).menuGrpType3[z].menuBox.Add(menuBox);
                ((MenuTypeGrp3)menuGrpList[x]).menuGrpType3[z].menuTCode.Add(string.Empty);
                ((MenuTypeGrp3)menuGrpList[x]).menuGrpType3[z].menuMemo.Add(string.Empty);
                ((MenuTypeGrp3)menuGrpList[x]).menuGrpType3[z].soldOut.Add(false);
            }
            panelMenuList.ResumeLayout(false);
        }

        /// <summary>
        /// 메뉴리스트 이전페이지 이동 메서드
        /// </summary>
        /// <param name="grpIndex">메뉴그룹 인덱스</param>
        private void PagePrev(int grpIndex)
        {
            switch (menuGrpList[grpIndex].type)
            {
                case "1":
                    if (menuGrpBoxList[grpIndex].pageNow < menuGrpBoxList[grpIndex].totalPage + 1 && menuGrpBoxList[grpIndex].pageNow > 0)
                    {
                        for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;

                        lblPage.Text = string.Format("{0} / {1}", (--menuGrpBoxList[grpIndex].pageNow) + 1, menuGrpBoxList[grpIndex].totalPage + 1);

                        for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                        {
                            if (((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                        }
                    }
                    break;

                case "2":
                    if (menuGrpBoxList[grpIndex].pageNow < menuGrpBoxList[grpIndex].totalPage + 1 && menuGrpBoxList[grpIndex].pageNow > 0)
                    {
                        for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;

                        lblPage.Text = string.Format("{0} / {1}", (--menuGrpBoxList[grpIndex].pageNow) + 1, menuGrpBoxList[grpIndex].totalPage + 1);

                        for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                        {
                            if (((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                        }
                    }
                    break;


                case "3":
                    if (menuGrpBoxList[grpIndex].pageNow < menuGrpBoxList[grpIndex].totalPage + 1 && menuGrpBoxList[grpIndex].pageNow > 0)
                    {
                        for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;

                        lblPage.Text = string.Format("{0} / {1}", (--menuGrpBoxList[grpIndex].pageNow) + 1, menuGrpBoxList[grpIndex].totalPage + 1);

                        for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                        {
                            if (((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 메뉴리스트 다음페이지 이동 메서드
        /// </summary>
        /// <param name="grpIndex">메뉴그룹 인덱스</param>
        private void PageNext(int grpIndex)
        {
            switch (menuGrpList[grpIndex].type)
            {
                case "1":
                    if (menuGrpBoxList[grpIndex].pageNow >= 0 && menuGrpBoxList[grpIndex].pageNow < menuGrpBoxList[grpIndex].totalPage)
                    {
                        for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;

                        lblPage.Text = string.Format("{0} / {1}", (++menuGrpBoxList[grpIndex].pageNow) + 1, menuGrpBoxList[grpIndex].totalPage + 1);

                        if (((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox[0].Visible == false)
                        {

                            for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            {
                                if (((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                    ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                            }

                        }
                        else
                        {
                            for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            {
                                panelMenuList.Controls.Add(((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox[i]);

                                if (((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                    ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                                else
                                    ((MenuTypeGrp1)menuGrpList[grpIndex]).menuGrpType1[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;
                            }
                        }
                    }
                    break;

                case "2":
                    if (menuGrpBoxList[grpIndex].pageNow >= 0 && menuGrpBoxList[grpIndex].pageNow < menuGrpBoxList[grpIndex].totalPage)
                    {
                        for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;

                        lblPage.Text = string.Format("{0} / {1}", (++menuGrpBoxList[grpIndex].pageNow) + 1, menuGrpBoxList[grpIndex].totalPage + 1);

                        if (((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox[0].Visible == false)
                        {
                            for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                                if (((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                    ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                        }
                        else
                        {
                            for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            {
                                panelMenuList.Controls.Add(((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox[i]);

                                if (((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                    ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                                else
                                    ((MenuTypeGrp2)menuGrpList[grpIndex]).menuGrpType2[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;
                            }
                        }
                    }
                    break;

                case "3":
                    if (menuGrpBoxList[grpIndex].pageNow >= 0 && menuGrpBoxList[grpIndex].pageNow < menuGrpBoxList[grpIndex].totalPage)
                    {
                        for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;

                        lblPage.Text = string.Format("{0} / {1}", (++menuGrpBoxList[grpIndex].pageNow) + 1, menuGrpBoxList[grpIndex].totalPage + 1);

                        if (((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox[0].Visible == false)
                        {
                            for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                                if (((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                    ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                        }
                        else
                        {
                            for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox.Count; i++)
                            {
                                panelMenuList.Controls.Add(((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox[i]);

                                if (((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                    ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = true;
                                else
                                    ((MenuTypeGrp3)menuGrpList[grpIndex]).menuGrpType3[menuGrpBoxList[grpIndex].pageNow].menuBox[i].Visible = false;
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 메뉴그룹의 메뉴 개수에 따른 총 페이지 설정
        /// </summary>
        /// <param name="grpIndex"></param>
        /// <param name="grpName"></param>
        private void GetMenuGrpTotalPage(int grpIndex, string grpName)
        {
            using (DataTable dt = menuListCfgDao.MenuListCfgTotalPageSelectL(grpName))
            {
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows.Count <= 10)
                        menuGrpBoxList[grpIndex].totalPage = 0;
                    else if (dt.Rows.Count > 10 && dt.Rows.Count <= 20)
                        menuGrpBoxList[grpIndex].totalPage = 1;
                    else if (dt.Rows.Count > 20 && dt.Rows.Count <= 30)
                        menuGrpBoxList[grpIndex].totalPage = 2;
                    else if (dt.Rows.Count > 30 && dt.Rows.Count <= 40)
                        menuGrpBoxList[grpIndex].totalPage = 3;
                    else if (dt.Rows.Count > 40 && dt.Rows.Count <= 50)
                        menuGrpBoxList[grpIndex].totalPage = 4;
                }
            }
        }

        /// <summary>
        /// 메뉴 그룹의 모든 메뉴명, 이미지, 가격 설정
        /// </summary>
        private void GetMenuListCfgSelect()
        {
            for (int i = 0; i < grpInfo.grpBoxList.Count; i++)
            {
                nGrpIndex = i;
                switch (grpInfo.grpBoxType[i])
                {
                    case "1":
                        for (int j = 0; j < ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1.Length; j++)
                        {
                            for (int k = 0; k < ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].menuBox.Count; k++)
                            {
                                menuListCfgDao.GNAM = menuGrpList[i].name;
                                menuListCfgDao.TYPE = menuGrpList[i].type;
                                menuListCfgDao.PAGE = j;
                                menuListCfgDao.COLROW = k;

                                using (DataTable dt = menuListCfgDao.MenuListCfgSelectL())
                                {
                                    if (dt.Rows.Count != 0)
                                    {
                                        using (DataTableReader dtr = new DataTableReader(dt))
                                        {
                                            while (dtr.Read())
                                            {
                                                if (dtr["MT_TNAM"].ToString().Length != 0)
                                                {
                                                    ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].menuTitle[k].Text = dtr["MT_TNAM"].ToString();
                                                    if (Encoding.Default.GetBytes(dtr["MT_TNAM"].ToString()).Length >= 22)
                                                        ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].menuTitle[k].Font = new Font("나눔스퀘어", 15, FontStyle.Bold);
                                                    ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].menuTCode[k] = dtr["MT_TCOD"].ToString();
                                                    ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].menuImgBox[k].BackgroundImage =
                                                        ImageLoad(dtr["MT_TNAM"].ToString(), dtr["MT_TCOD"].ToString());
                                                    ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].menuImgBox[k].BackgroundImageLayout =
                                                        ImageLayout.Stretch;
                                                    MenuItemAmt(dtr["MT_TNAM"].ToString(), menuGrpList[i].type, dtr["MT_TCOD"].ToString(), j, k);
                                                    ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].menuMemo[k] = MemoLoad(dtr["MT_TNAM"].ToString(), dtr["MT_TCOD"].ToString());
                                                }

                                                if (dtr["MT_SOLDYN"].ToString().CompareTo("Y") == 0)
                                                {
                                                    ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].soldOut[k] = true;
                                                    ((MenuTypeGrp1)menuGrpList[i]).menuGrpType1[j].menuBox[k].Enabled = false;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case "2":
                        for (int j = 0; j < ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2.Length; j++)
                        {
                            for (int k = 0; k < ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].menuBox.Count; k++)
                            {
                                menuListCfgDao.GNAM = menuGrpList[i].name;
                                menuListCfgDao.TYPE = menuGrpList[i].type;
                                menuListCfgDao.PAGE = j;
                                menuListCfgDao.COLROW = k;

                                using (DataTable dt = menuListCfgDao.MenuListCfgSelectL())
                                {
                                    if (dt.Rows.Count != 0)
                                    {
                                        using (DataTableReader dtr = new DataTableReader(dt))
                                        {
                                            while (dtr.Read())
                                            {
                                                if (dtr["MT_TNAM"].ToString().Length != 0)
                                                {
                                                    ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].menuTitle[k].Text = dtr["MT_TNAM"].ToString();
                                                    if (Encoding.Default.GetBytes(dtr["MT_TNAM"].ToString()).Length >= 22)
                                                        ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].menuTitle[k].Font = new Font("나눔스퀘어", 15, FontStyle.Bold);
                                                    ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].menuTCode[k] = dtr["MT_TCOD"].ToString();
                                                    ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].menuImgBox[k].BackgroundImage =
                                                        ImageLoad(dtr["MT_TNAM"].ToString(), dtr["MT_TCOD"].ToString());
                                                    ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].menuImgBox[k].BackgroundImageLayout =
                                                        ImageLayout.Stretch;
                                                    MenuItemAmt(dtr["MT_TNAM"].ToString(), menuGrpList[i].type, dtr["MT_TCOD"].ToString(), j, k);
                                                    ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].menuMemo[k] = MemoLoad(dtr["MT_TNAM"].ToString(), dtr["MT_TCOD"].ToString());
                                                }

                                                if (dtr["MT_SOLDYN"].ToString().CompareTo("Y") == 0)
                                                {
                                                    ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].soldOut[k] = true;
                                                    ((MenuTypeGrp2)menuGrpList[i]).menuGrpType2[j].menuBox[k].Enabled = false;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case "3":
                        for (int j = 0; j < ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3.Length; j++)
                        {
                            for (int k = 0; k < ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].menuBox.Count; k++)
                            {
                                menuListCfgDao.GNAM = menuGrpList[i].name;
                                menuListCfgDao.TYPE = menuGrpList[i].type;
                                menuListCfgDao.PAGE = j;
                                menuListCfgDao.COLROW = k;

                                using (DataTable dt = menuListCfgDao.MenuListCfgSelectL())
                                {
                                    if (dt.Rows.Count != 0)
                                    {
                                        using (DataTableReader dtr = new DataTableReader(dt))
                                        {
                                            while (dtr.Read())
                                            {
                                                if (dtr["MT_TNAM"].ToString().Length != 0)
                                                {
                                                    ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].menuTitle[k].Text = dtr["MT_TNAM"].ToString();
                                                    if (Encoding.Default.GetBytes(dtr["MT_TNAM"].ToString()).Length >= 22)
                                                        ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].menuTitle[k].Font = new Font("나눔스퀘어", 15, FontStyle.Bold);
                                                    ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].menuTCode[k] = dtr["MT_TCOD"].ToString();
                                                    ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].menuImgBox[k].BackgroundImage =
                                                        ImageLoad(dtr["MT_TNAM"].ToString(), dtr["MT_TCOD"].ToString());
                                                    ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].menuImgBox[k].BackgroundImageLayout =
                                                        ImageLayout.Stretch;
                                                    MenuItemAmt(dtr["MT_TNAM"].ToString(), menuGrpList[i].type, dtr["MT_TCOD"].ToString(), j, k);
                                                    ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].menuMemo[k] = MemoLoad(dtr["MT_TNAM"].ToString(), dtr["MT_TCOD"].ToString());
                                                }

                                                if (dtr["MT_SOLDYN"].ToString().CompareTo("Y") == 0)
                                                {
                                                    ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].soldOut[k] = true;
                                                    ((MenuTypeGrp3)menuGrpList[i]).menuGrpType3[j].menuBox[k].Enabled = false;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 메뉴 타입에 따른 메뉴선택 팝업창의 버튼 텍스트 설정
        /// </summary>
        /// <param name="grpName">메뉴명</param>
        /// <param name="grpType">메뉴타입</param>
        /// <param name="tCode">메뉴코드</param>
        /// <param name="x">메뉴페이지</param>
        /// <param name="z">메뉴순번</param>
        private void MenuItemAmt(string grpName, string grpType, string tCode, int x, int z)
        {
            bool isCuttingMenu = false;
            foreach (string str in StoreInfo.CuttingMenu)
            {
                if (grpName == str)
                    isCuttingMenu = true;
            }
            using (DataTable dt = menuListCfgDao.MenuListCfgAmtLisSelectL(grpName, tCode))
            {
                if (dt.Rows.Count != 0)
                {
                    int i = 0;
                    
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            //메뉴 타입 1 : 항목 2개
                            //메뉴 타입 2 : 항목 4개
                            //메뉴 타입 3 : 항목 1개
                            if (isCuttingMenu && (dtr["PT_OPNM"].ToString() == "커팅 O" || dtr["PT_OPNM"].ToString() == "커팅 X"))
                                continue;

                            switch (i)
                            {
                                case 0:
                                    switch (grpType)
                                    {
                                        case "1":
                                            ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[x].menuSingleTitle[z].Text = dtr["PT_OPNM"].ToString();
                                            ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[x].menuSingleAmt[z].Text =
                                                string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;

                                        case "2":
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[x].menuSubTitle1[z].Text = dtr["PT_OPNM"].ToString();
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[x].menuAmt1[z].Text =
                                                string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;

                                        case "3":
                                            //((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[x].menuTitle[z].Text = dtr["PT_OPNM"].ToString();
                                            ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[x].menuAmt[z].Text =
                                                string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                    break;

                                case 1:
                                    switch (grpType)
                                    {
                                        case "1":
                                            ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[x].menuSetTitle[z].Text = dtr["PT_OPNM"].ToString();
                                            ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[x].menuSetAmt[z].Text =
                                                string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;

                                        case "2":
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[x].menuSubTitle2[z].Text = dtr["PT_OPNM"].ToString();
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[x].menuAmt2[z].Text =
                                                string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;

                                    }
                                    break;

                                case 2:
                                    switch (grpType)
                                    {
                                        case "2":
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[x].menuSubTitle3[z].Text = dtr["PT_OPNM"].ToString();
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[x].menuAmt3[z].Text =
                                                string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;

                                    }
                                    break;

                                case 3:
                                    switch (grpType)
                                    {
                                        case "2":
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[x].menuSubTitle4[z].Text = dtr["PT_OPNM"].ToString();
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[x].menuAmt4[z].Text =
                                                string.Format("{0:#,##0}원", dtr["PT_PRICE"].ToDecimal());
                                            break;
                                    }
                                    break;
                            }
                            if (i < dt.Rows.Count)
                                i++;
                        }
                    }
                }
            }
        }

        public Bitmap ImageLoad(string name, string tcod)
        {
            byte[] bImage = null;

            using (DataTableReader dtr = new DataTableReader(menuListCfgDao.MenuListCfgImageSelectL(name, tcod)))
            {
                while (dtr.Read())
                    bImage = (byte[])dtr["MR_IMAGE"];

                dtr.Close();

                if (bImage != null)
                    return new Bitmap(new MemoryStream(bImage));
            }

            return null;
        }

        public string MemoLoad(string name, string tcode)
        {
            string memo = string.Empty;
            using (DataTableReader dtr = new DataTableReader(menuListCfgDao.MenuListCfgMemoSelectL(name, tcode)))
            {
                while (dtr.Read())
                    memo = dtr["MR_MEMO"].ToString();

                dtr.Close();
            }

            return memo;
        }

        /// <summary>
        /// 메인화면으로 돌아갈 때 실행되는 메서드
        /// </summary>
        private void BackToMain()
        {
            if (backgroundWorker1.IsBusy)
                backgroundWorker1.Dispose();
            ClearMemory();
            ReturnSendMsg("HIDE");
            this.Visible = false;
        }

        /// <summary>
        /// 모든 주문사항, 멤버변수 초기화
        /// </summary>
        private void ClearMemory()
        {
            OrderListInfo.OrderListClear();

            lblTotalAmt.Text = @"\ 0";
            orderListSet.panelMain.Clear();
            orderListSet.panelTitle.Clear();
            orderListSet.picBoxBtnPlus.Clear();
            orderListSet.picBoxBtnMinus.Clear();
            orderListSet.labelTitle.Clear();
            orderListSet.labelMemo.Clear();
            orderListSet.labelAmt.Clear();
            orderListSet.labelQuan.Clear();
            orderListSet.danga.Clear();

            woniFillList1.Controls.Clear();

            grpInfo.grpBoxLabelList[nGrpIndex].ForeColor = Color.FromArgb(132, 92, 85);
            if (grpInfo.grpBoxList[nGrpIndex].BackgroundImage != null)
            {
                grpInfo.grpBoxList[nGrpIndex].BackgroundImage.Dispose();
                grpInfo.grpBoxList[nGrpIndex].BackgroundImage = null;
            }
            grpInfo.grpBoxList[nGrpIndex].BackgroundImage = Resources.btn_menu_bg;

            switch(grpInfo.grpBoxType[nGrpIndex])
            {
                case "1":
                    for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                    break;

                case "2":
                    for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                    break;

                case "3":
                    for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                    break;
            }

            nRunAnsyIndex = nRunOldAnsyIndex = -1;

            nGrpIndex = 0;
            nGrpOldIndex = -1;
            nPage = 1;
        }

        /// <summary>
        /// 메인화면 통신용 메서드
        /// </summary>
        /// <param name="msg">전송할 메시지 문자열</param>
        private void ReturnSendMsg(string msg)
        {
            returnValueEvent(msg);
        }

        /// <summary>
        /// 다른 화면에서 변경한 내용을 적용하기위한 통신용 메서드
        /// (주문내역, 품목설정)
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="msg">전달할 메시지</param>
        public void Receive(Form frm, string msg)
        {
            string[] tmp = msg.Split('-');
            int index = tmp[1].ToInteger();

            if (tmp[0].CompareTo("Delete") == 0)
            {
                lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", lblTotalAmt.Text.ToDecimal() - orderListSet.danga[index]);
                woniFillList1.Controls.Remove(orderListSet.panelMain[index]);
                orderListSet.panelMain.RemoveAt(index);
                orderListSet.panelTitle.RemoveAt(index);
                orderListSet.labelTitle.RemoveAt(index);
                orderListSet.labelMemo.RemoveAt(index);
                orderListSet.labelQuan.RemoveAt(index);
                orderListSet.labelAmt.RemoveAt(index);
                orderListSet.picBoxBtnMinus.RemoveAt(index);
                orderListSet.picBoxBtnPlus.RemoveAt(index);
                orderListSet.danga.RemoveAt(index);

                for (int i = 0; i < orderListSet.picBoxBtnMinus.Count; i++)
                {
                    orderListSet.picBoxBtnMinus[i].Name = "ListMinusBtn_" + i;
                    orderListSet.picBoxBtnPlus[i].Name = "ListPlusBtn_" + i;
                }
            }
            else if (tmp[0].CompareTo("Up") == 0 || tmp[0].CompareTo("Down") == 0)
            {
                orderListSet.labelQuan[index].Text = OrderListInfo.ItemQuan[index].ToString();
                orderListSet.labelAmt[index].Text = string.Format(@"\{0:#,##0}", OrderListInfo.ItemAmt[index]);
                lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", tmp[2].ToDecimal());
            }
            else if (tmp[0].CompareTo("SOLDOUT") == 0)
            {
                string[] data = tmp[1].Split(':');

                string grpName = data[2];
                string grpType = "";
                string grpNum = "";
                int page = data[0].ToInteger();
                int no = data[1].ToInteger();

                for (int i = 0; i < menuGrpList.Count; i++)
                {
                    if (menuGrpList[i].name.CompareTo(grpName) == 0)
                    {
                        grpType = menuGrpList[i].type;
                        grpNum = i.ToString();
                        break;
                    }

                }

                switch (grpType)
                {
                    case "1":
                        ((MenuTypeGrp1)menuGrpList[grpNum.ToInteger()]).menuGrpType1[page].menuBox[no].Enabled = false;
                        ((MenuTypeGrp1)menuGrpList[grpNum.ToInteger()]).menuGrpType1[page].soldOut[no] = true;
                        break;

                    case "2":
                        ((MenuTypeGrp2)menuGrpList[grpNum.ToInteger()]).menuGrpType2[page].menuBox[no].Enabled = false;
                        ((MenuTypeGrp2)menuGrpList[grpNum.ToInteger()]).menuGrpType2[page].soldOut[no] = true;
                        break;

                    case "3":
                        ((MenuTypeGrp3)menuGrpList[grpNum.ToInteger()]).menuGrpType3[page].menuBox[no].Enabled = false;
                        ((MenuTypeGrp3)menuGrpList[grpNum.ToInteger()]).menuGrpType3[page].soldOut[no] = true;
                        break;
                }
            }
            else if (tmp[0].CompareTo("SOLDCANCEL") == 0)
            {
                string[] data = tmp[1].Split(':');

                string grpName = data[2];
                string grpType = "";
                string grpNum = "";
                int page = data[0].ToInteger();
                int no = data[1].ToInteger();

                for (int i = 0; i < menuGrpList.Count; i++)
                {
                    if (menuGrpList[i].name.CompareTo(grpName) == 0)
                    {
                        grpType = menuGrpList[i].type;
                        grpNum = i.ToString();
                        break;
                    }
                }

                switch (grpType)
                {
                    case "1":
                        ((MenuTypeGrp1)menuGrpList[grpNum.ToInteger()]).menuGrpType1[page].menuBox[no].Enabled = true;
                        ((MenuTypeGrp1)menuGrpList[grpNum.ToInteger()]).menuGrpType1[page].soldOut[no] = false;
                        break;

                    case "2":
                        ((MenuTypeGrp2)menuGrpList[grpNum.ToInteger()]).menuGrpType2[page].menuBox[no].Enabled = true;
                        ((MenuTypeGrp2)menuGrpList[grpNum.ToInteger()]).menuGrpType2[page].soldOut[no] = false;
                        break;

                    case "3":
                        ((MenuTypeGrp3)menuGrpList[grpNum.ToInteger()]).menuGrpType3[page].menuBox[no].Enabled = true;
                        ((MenuTypeGrp3)menuGrpList[grpNum.ToInteger()]).menuGrpType3[page].soldOut[no] = false;
                        break;

                }
            }
            else
            {
                for (int i = 0; i < orderListSet.picBoxBtnMinus.Count; i++)
                {
                    orderListSet.picBoxBtnMinus[i].Name = "ListMinusBtn_" + i;
                    orderListSet.picBoxBtnPlus[i].Name = "ListPlusBtn_" + i;
                }
            }
        }

        #endregion

        //------이벤트------//
        #region 이벤트

        private void FrmMenuList_Load(object sender, EventArgs e)
        {
            OrderListInfo.OrderListClear();
            MenuListLayoutInit();
            //picBoxStoreLogo.Left = (this.ClientSize.Width - picBoxStoreLogo.Width) / 2;

            for (int i = 0; i < grpInfo.grpBoxList.Count; i++)
            {
                menuGrpBoxList.Add(new MenuItemProperty());
                menuGrpBoxList[i].pageNow = 0;
                menuGrpBoxList[i].menuSelectIndex = -1;

                GetMenuGrpTotalPage(i, menuGrpList[i].name);
            }
        }
        
        //메뉴선택화면 자동 숨김 이벤트 (마지막 동작 15초 후 이벤트호출)
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            waitTime.Stop();
            this.Invoke(new MethodInvoker(
                delegate ()
                {
                    BackToMain();
                }));
        }

        //메뉴그룹 페이징 버튼 이벤트
        private void grpPaging_MouseDown(object sender, EventArgs e)
        {
            string name = ((Control)sender).Name;
            PictureBox pic = sender as PictureBox;

            switch(name)
            {
                case "Left":
                    if(pic.BackgroundImage != null)
                    {
                        pic.BackgroundImage.Dispose();
                        pic.BackgroundImage = null;
                    }
                    pic.BackgroundImage = Resources.btn_arrow_left_hover;
                    break;
                case "Right":
                    if (pic.BackgroundImage != null)
                    {
                        pic.BackgroundImage.Dispose();
                        pic.BackgroundImage = null;
                    }
                    pic.BackgroundImage = Resources.btn_arrow_right_hover;
                    break;
            }
        }

        private void grpPaging_MouseUp(object sender, EventArgs e)
        {
            bool pageChanged = false;
            string name = ((Control)sender).Name;
            PictureBox pic = sender as PictureBox;

            switch (name)
            {
                case "Left":
                    if (pic.BackgroundImage != null)
                    {
                        pic.BackgroundImage.Dispose();
                        pic.BackgroundImage = null;
                    }
                    pic.BackgroundImage = Resources.btn_arrow_left;

                    if (nPage > 1)
                    {
                        pageChanged = true;
                        nPage--;
                    }
                    break;
                case "Right":
                    if (pic.BackgroundImage != null)
                    {
                        pic.BackgroundImage.Dispose();
                        pic.BackgroundImage = null;
                    }
                    pic.BackgroundImage = Resources.btn_arrow_right;

                    if (nPage < (grpInfo.grpBoxList.Count / StoreInfo.MaxTap) + 1)
                    {
                        pageChanged = true;
                        nPage++;
                    }
                    break;
            }

            UtilHelper.spWav.Play();
            //페이지 번호가 가감되면 메뉴그룹 리스트를 페이지 번호에 맞게 표출
            if (pageChanged)
                CreateGrpList(nPage);
        }


        //메뉴그룹 버튼 이벤트
        private void grpBox_MouseDown(object sender, EventArgs e)
        {
            waitTime.Stop();

            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            nGrpIndex = tmpName[1].ToInteger();

            grpInfo.grpBoxLabelList[nGrpIndex].ForeColor = Color.White;
            if (grpInfo.grpBoxList[nGrpIndex].BackgroundImage != null)
            {
                grpInfo.grpBoxList[nGrpIndex].BackgroundImage.Dispose();
                grpInfo.grpBoxList[nGrpIndex].BackgroundImage = null;
            }
            grpInfo.grpBoxList[nGrpIndex].BackgroundImage = Resources.btn_menu_bg_hover;
        }

        private void grpBox_MouseUp(object sender, EventArgs e)
        {
            if (nGrpOldIndex == -1)
            {
                //메뉴선택화면이 처음 표출됐을 때,
                //초기 인덱스를 지정해주기 위한 변수
                string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                nGrpIndex = tmpName[1].ToInteger();

                grpInfo.grpBoxLabelList[nGrpIndex].ForeColor = Color.White;
                if (grpInfo.grpBoxList[nGrpIndex].BackgroundImage != null)
                {
                    grpInfo.grpBoxList[nGrpIndex].BackgroundImage.Dispose();
                    grpInfo.grpBoxList[nGrpIndex].BackgroundImage = null;
                }
                grpInfo.grpBoxList[nGrpIndex].BackgroundImage = Resources.btn_menu_bg_hover;

                nGrpOldIndex = nGrpIndex;
                isFirstSound = true;

                switch (grpInfo.grpBoxType[nGrpIndex])
                {
                    case "1":
                        for (int i = 0; i < 10; i++)
                        {
                            panelMenuList.Controls.Add(((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[0].menuBox[i]);

                            if (((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[0].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[0].menuBox[i].Visible = true;
                            else
                                ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[0].menuBox[i].Visible = false;
                        }
                        break;

                    case "2":
                        for (int i = 0; i < 10; i++)
                        {
                            panelMenuList.Controls.Add(((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i]);

                            if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuTitle[i].Text.Length != 0)
                            {
                                if(StoreInfo.IsTakeOut)
                                {
                                    if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("돈카츠") == 0)
                                    {
                                        if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuTitle[i].Text.IndexOf("+") > 0)
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i].Visible = false;
                                        else
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i].Visible = true;
                                    }
                                    if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("파스타 및 면요리") == 0)
                                    {
                                        if(((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuTitle[i].Text.Contains("스파게티"))
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i].Visible = true;
                                        else
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i].Visible = false;
                                    }
                                    else if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("주류") == 0)
                                    {
                                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i].Visible = false;
                                    }
                                    else
                                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i].Visible = true;
                                }
                                else
                                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i].Visible = true;
                            }
                            else
                                ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[0].menuBox[i].Visible = false;
                        }
                        break;

                    case "3":
                        for (int i = 0; i < 10; i++)
                        {
                            panelMenuList.Controls.Add(((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[0].menuBox[i]);

                            if (((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[0].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[0].menuBox[i].Visible = true;
                            else
                                ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[0].menuBox[i].Visible = false;
                        }
                        break;
                }
            }
            else if (nGrpOldIndex == nGrpIndex)
            {
                return;
            }
            else
            {
                grpInfo.grpBoxLabelList[nGrpOldIndex].ForeColor = Color.FromArgb(132, 92, 85);
                if (grpInfo.grpBoxList[nGrpOldIndex].BackgroundImage != null)
                {
                    grpInfo.grpBoxList[nGrpOldIndex].BackgroundImage.Dispose();
                    grpInfo.grpBoxList[nGrpOldIndex].BackgroundImage = null;
                }
                grpInfo.grpBoxList[nGrpOldIndex].BackgroundImage = Resources.btn_menu_bg;

                switch (grpInfo.grpBoxType[nGrpOldIndex])
                {
                    case "1":
                        for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[nGrpOldIndex]).menuGrpType1[menuGrpBoxList[nGrpOldIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp1)menuGrpList[nGrpOldIndex]).menuGrpType1[menuGrpBoxList[nGrpOldIndex].pageNow].menuBox[i].Visible = false;
                        break;

                    case "2":
                        for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[nGrpOldIndex]).menuGrpType2[menuGrpBoxList[nGrpOldIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp2)menuGrpList[nGrpOldIndex]).menuGrpType2[menuGrpBoxList[nGrpOldIndex].pageNow].menuBox[i].Visible = false;
                        break;

                    case "3":
                        for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[nGrpOldIndex]).menuGrpType3[menuGrpBoxList[nGrpOldIndex].pageNow].menuBox.Count; i++)
                            ((MenuTypeGrp3)menuGrpList[nGrpOldIndex]).menuGrpType3[menuGrpBoxList[nGrpOldIndex].pageNow].menuBox[i].Visible = false;
                        break;
                }
            }

            if (isFirstSound)
                isFirstSound = false;
            else
                UtilHelper.spWav.Play(); //사운드 재생


            menuGrpBoxList[nGrpIndex].pageNow = 0;

            switch (grpInfo.grpBoxType[nGrpIndex])
            {
                case "1":
                    if (((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[0].Visible == false)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        {
                            panelMenuList.Controls.Add(((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i]);

                            if (((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                            else
                                ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                        }
                    }
                    break;

                case "2":

                    if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[0].Visible == false)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.Length != 0)
                            {
                                if (StoreInfo.IsTakeOut)
                                {
                                    if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("돈카츠") == 0)
                                    {
                                        if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.IndexOf("+") > 0)
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                                        else
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                                    }
                                    if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("파스타 및 면요리") == 0)
                                    {
                                        if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.Contains("스파게티"))
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                                        else
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                                    }
                                    else if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("주류") == 0)
                                    {
                                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                                    }
                                    else
                                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                                }
                                else
                                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                            }
                            else
                                ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        {
                            panelMenuList.Controls.Add(((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i]);

                            if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.Length != 0)
                            {
                                if (StoreInfo.IsTakeOut)
                                {
                                    if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("돈카츠") == 0)
                                    {
                                        if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.IndexOf("+") > 0)
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                                        else
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                                    }
                                    if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("파스타 및 면요리") == 0)
                                    {
                                        if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.Contains("스파게티"))
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                                        else
                                            ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                                    }
                                    else if (((MenuTypeGrp2)menuGrpList[nGrpIndex]).name.CompareTo("주류") == 0)
                                    {
                                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                                    }
                                    else
                                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                                }
                                else
                                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                            }
                            else
                                ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                        }
                    }
                    break;

                case "3":
                    if (((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[0].Visible == false)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        {
                            panelMenuList.Controls.Add(((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i]);

                            if (((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuTitle[i].Text.Length != 0)
                                ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = true;
                            else
                                ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Visible = false;
                        }
                    }
                    break;
            }

            lblPage.Text = string.Format("{0} / {1}", menuGrpBoxList[nGrpIndex].pageNow + 1, menuGrpBoxList[nGrpIndex].totalPage + 1);
            nGrpOldIndex = nGrpIndex;

            waitTime.Start();
        }

        //메뉴 선택이벤트
        private void menuBox_MouseDown(object sender, MouseEventArgs e)
        {
            waitTime.Stop();

            //인덱스 저장을 위해
            //현재 선택한 메뉴의 이름을 '_' 구분자를 기준으로 문자열 분할
            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            UtilHelper.spWav.Play();

            menuGrpBoxList[nGrpIndex].menuSelectIndex = tmpName[1].ToInteger();
            switch (menuGrpList[nGrpIndex].type)
            {
                case "1":
                    //현재 선택한 메뉴의 인덱스를 저장

                    //현재 이미지박스 위치 및 사이즈 저장
                    menuBoxOldPoint = ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location;
                    menuBoxOldSize = ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size;

                    ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.Red;
                    
                    //이미지박스 확대 및 위치 변경
                    ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location =
                        new Point(menuBoxOldPoint.X - 10, menuBoxOldPoint.Y - 10);
                    ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size =
                        new Size(menuBoxOldSize.Width + 20, menuBoxOldSize.Height + 20);

                    break;

                case "2":

                    menuBoxOldPoint = ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location;
                    menuBoxOldSize = ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size;

                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.Red;
                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location =
                        new Point(menuBoxOldPoint.X - 10, menuBoxOldPoint.Y - 10);
                    ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size =
                        new Size(menuBoxOldSize.Width + 20, menuBoxOldSize.Height + 20);
                    break;

                case "3":

                    menuBoxOldPoint = ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location;
                    menuBoxOldSize = ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size;

                    ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].BorderColor = Color.Red;
                    ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Location =
                        new Point(menuBoxOldPoint.X - 10, menuBoxOldPoint.Y - 10);
                    ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuImgBox[menuGrpBoxList[nGrpIndex].menuSelectIndex].Size =
                        new Size(menuBoxOldSize.Width + 20, menuBoxOldSize.Height + 20);
                    break;
            }
        }

        private void menuBox_MouseUp(object sender, MouseEventArgs e)
        {
            //메뉴 클릭 시,
            //팝업 창이 사라지기 전까지
            //현재 메뉴리스트에 보여지는 모든 메뉴를 비활성화
            switch (menuGrpList[nGrpIndex].type)
            {
                case "1":
                    for (int i = 0; i < ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        ((MenuTypeGrp1)menuGrpList[nGrpIndex]).menuGrpType1[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = false;
                    break;

                case "2":
                    for (int i = 0; i < ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        ((MenuTypeGrp2)menuGrpList[nGrpIndex]).menuGrpType2[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = false;
                    break;

                case "3":
                    for (int i = 0; i < ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox.Count; i++)
                        ((MenuTypeGrp3)menuGrpList[nGrpIndex]).menuGrpType3[menuGrpBoxList[nGrpIndex].pageNow].menuBox[i].Enabled = false;
                    break;
            }

            UtilHelper.Delay(100);

            MenuBoxMouseUp();
        }

        //이미지버튼 이벤트
        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            //선택한 버튼을 구별하기 위한
            //Bitmap 변수 선언
            Bitmap bitmap;
            ctrlName = ((Control)sender).Name;

            UtilHelper.spWav.Play();
            waitTime.Stop();

            //현재 선택한 버튼의 기존 이미지를 어둡게하여 Bitmap 변수에 저장하고
            //기존 이미지를 Dispose 후 Bitmap 변수로 이미지 교체
            switch (ctrlName)
            {
                case "picBoxPagePrev":
                    bitmap = UtilHelper.ChangeOpacity(picBoxPagePrev.BackgroundImage, 1f);
                    if (picBoxPagePrev.BackgroundImage != null)
                    {
                        picBoxPagePrev.BackgroundImage.Dispose();
                        picBoxPagePrev.BackgroundImage = null;
                    }
                    picBoxPagePrev.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxPageNext":
                    bitmap = UtilHelper.ChangeOpacity(picBoxPageNext.BackgroundImage, 1f);
                    if (picBoxPageNext.BackgroundImage != null)
                    {
                        picBoxPageNext.BackgroundImage.Dispose();
                        picBoxPageNext.BackgroundImage = null;
                    }
                    picBoxPageNext.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxAllCancel":
                    bitmap = UtilHelper.ChangeOpacity(picBoxAllCancel.BackgroundImage, 1f);
                    if (picBoxAllCancel.BackgroundImage != null)
                    {
                        picBoxAllCancel.BackgroundImage.Dispose();
                        picBoxAllCancel.BackgroundImage = null;
                    }
                    picBoxAllCancel.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxPayMent":
                    bitmap = UtilHelper.ChangeOpacity(picBoxPayMent.BackgroundImage, 1f);
                    if (picBoxPayMent.BackgroundImage != null)
                    {
                        picBoxPayMent.BackgroundImage.Dispose();
                        picBoxPayMent.BackgroundImage = null;
                    }
                    picBoxPayMent.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxMain":
                    bitmap = UtilHelper.ChangeOpacity(picBoxMain.BackgroundImage, 1f);
                    if (picBoxMain.BackgroundImage != null)
                    {
                        picBoxMain.BackgroundImage.Dispose();
                        picBoxMain.BackgroundImage = null;
                    }

                    picBoxMain.BackgroundImage = bitmap;
                    bitmap = null;
                    break;
            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            //선택했던 버튼의 이미지를 원래 이미지로 원복 후
            //버튼에 해당되는 로직 호출
            switch (ctrlName)
            {
                case "picBoxPagePrev":
                    if (picBoxPagePrev.BackgroundImage != null)
                    {
                        picBoxPagePrev.BackgroundImage.Dispose();
                        picBoxPagePrev.BackgroundImage = null;
                    }
                    picBoxPagePrev.BackgroundImage = Properties.Resources.btn_left;
                    UtilHelper.Delay(100);
                    PagePrev(nGrpIndex);
                    break;

                case "picBoxPageNext":
                    if (picBoxPageNext.BackgroundImage != null)
                    {
                        picBoxPageNext.BackgroundImage.Dispose();
                        picBoxPageNext.BackgroundImage = null;
                    }
                    picBoxPageNext.BackgroundImage = Properties.Resources.btn_right;
                    UtilHelper.Delay(100);
                    PageNext(nGrpIndex);
                    break;

                case "picBoxAllCancel":
                    if (picBoxAllCancel.BackgroundImage != null)
                    {
                        picBoxAllCancel.BackgroundImage.Dispose();
                        picBoxAllCancel.BackgroundImage = null;
                    }
                    picBoxAllCancel.BackgroundImage = Properties.Resources.btn_cancel_all;
                    UtilHelper.Delay(100);

                    if (woniFillList1.Controls.Count != 0)
                    {
                        FrmOrderCancelBox orderCancel = new FrmOrderCancelBox(UtilHelper.ScreenCapture(
                            this.Width, this.Height, this.Location));

                        //모든 주문취소 확인 시, 메뉴선택화면의 변수들을 초기화
                        //초기화 완료 후, 메뉴선택화면 숨김
                        if (orderCancel.ShowDialog() == DialogResult.OK)
                        {
                            BackToMain();
                        }
                    }
                    else
                    {
                        BackToMain();
                    }
                    break;

                case "picBoxPayMent":
                    if (picBoxPayMent.BackgroundImage != null)
                    {
                        picBoxPayMent.BackgroundImage.Dispose();
                        picBoxPayMent.BackgroundImage = null;
                    }
                    picBoxPayMent.BackgroundImage = Properties.Resources.btn_payment;
                    UtilHelper.Delay(100);

                    //주문목록에 선택한 메뉴가 하나도 없으면 return;
                    if (woniFillList1.Controls.Count == 0)
                        return;

                    FrmOrderList orderList = new FrmOrderList(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location), orderListSet.danga, this);

                    //결제완료 시, 메뉴선택화면의 변수들을 초기화
                    //초기화 완료 후, 메뉴선택화면 숨김
                    if (orderList.ShowDialog() == DialogResult.OK)
                    {
                        BackToMain();
                    }
                    else if (orderList.DialogResult == DialogResult.Yes)
                    {
                        BackToMain();
                    }
                    //결제취소 시, 메뉴선택화면 자동 숨김 타이머 활성화
                    else if (orderList.DialogResult == DialogResult.Cancel)
                    {
                        waitTime.Start();
                    }
                    break;

                case "picBoxMain":
                    if (picBoxMain.BackgroundImage != null)
                    {
                        picBoxMain.BackgroundImage.Dispose();
                        picBoxMain.BackgroundImage = null;
                    }
                    picBoxMain.BackgroundImage = Resources.btn_title_home4;
                    UtilHelper.Delay(100);

                    //주문목록에 선택한 메뉴가 있으면,
                    //주문취소 확인창 팝업
                    if (woniFillList1.Controls.Count != 0)
                    {
                        FrmOrderCancelBox orderCancel = new FrmOrderCancelBox(UtilHelper.ScreenCapture(
                            this.Width, this.Height, this.Location));

                        if (orderCancel.ShowDialog() == DialogResult.OK)
                        {
                            BackToMain();
                        }
                    }
                    else
                    {
                        BackToMain();
                    }
                    break;
            }
        }

        //주문목록 이미지버튼 이벤트
        private void picBoxPlusMinusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            nPlusMinusBtnIndex = tmpName[1].ToInteger();

            //선택한 버튼을 구별하기 위한
            //Bitmap 변수 선언
            Bitmap bitmap;

            UtilHelper.spWav.Play();

            waitTime.Stop();

            //현재 선택한 버튼의 기존 이미지를 어둡게하여 Bitmap 변수에 저장하고
            //기존 이미지를 Dispose 후 Bitmap 변수로 이미지 교체
            if (tmpName[0].StartsWith("ListPlus"))
            {
                bitmap = UtilHelper.ChangeOpacity(orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage, 1f);

                if (orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage != null)
                {
                    orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage.Dispose();
                    orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage = null;
                }
                orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage = bitmap;

            }
            else
            {
                bitmap = UtilHelper.ChangeOpacity(orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage, 1f);

                if (orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage != null)
                {
                    orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage.Dispose();
                    orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage = null;
                }
                orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage = bitmap;
            }

            bitmap = null;
        }

        private void picBoxPlusMinusBtn_MouseUp(object sender, MouseEventArgs e)
        {
            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            //선택했던 버튼의 이미지를 원래 이미지로 원복 후
            //버튼에 해당되는 로직 호출
            if (tmpName[0].StartsWith("ListPlus"))
            {
                orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].Enabled = false;
                if (orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage != null)
                {
                    orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage.Dispose();
                    orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage = null;
                }
                orderListSet.picBoxBtnPlus[nPlusMinusBtnIndex].BackgroundImage = Properties.Resources.btn_plus;
                UtilHelper.Delay(100);
                ProcPlusMinusBtn("ListPlus");
            }
            else
            {
                orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].Enabled = false;
                if (orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage != null)
                {
                    orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage.Dispose();
                    orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage = null;
                }
                orderListSet.picBoxBtnMinus[nPlusMinusBtnIndex].BackgroundImage = Properties.Resources.btn_minus;
                UtilHelper.Delay(100);

                //주문 개수가 1개 이상이면 주문 개수를 --
                //주문 개수가 1개 이하면 해당 메뉴 주문취소
                if (orderListSet.labelQuan[nPlusMinusBtnIndex].Text.ToInteger() > 1)
                    ProcPlusMinusBtn("ListMinus");
                else
                    ProcPlusMinusBtn("ListCancel");
            }

            waitTime.Start();
        }

        //메인폼 통신용 이벤트
        private void frmMain_sendValueEvent(string msg)
        {
            switch (msg)
            {
                case "SHOW":
                    if (grpInfo.grpBoxList.Count != 0)
                    {
                        //ClearMemory();
                        CreateGrpList(nPage);

                        this.Visible = true;
                        isFirstSound = true;

                        grpBox_MouseUp(grpInfo.grpBoxList[0], null);

                        //포장 여부에 따라 좌측 상단의 이미지들을 변경
                        if (StoreInfo.IsTakeOut)
                        {
                            if (picBoxEatin.BackgroundImage != null)
                            {
                                picBoxEatin.BackgroundImage.Dispose();
                                picBoxEatin.BackgroundImage = null;
                            }
                            picBoxEatin.BackgroundImage = Properties.Resources.icon_title_eatin_dis;

                            if (picBoxTakeout.BackgroundImage != null)
                            {
                                picBoxTakeout.BackgroundImage.Dispose();
                                picBoxTakeout.BackgroundImage = null;
                            }
                            picBoxTakeout.BackgroundImage = Properties.Resources.icon_title_takeout;
                        }
                        else
                        {
                            if (picBoxEatin.BackgroundImage != null)
                            {
                                picBoxEatin.BackgroundImage.Dispose();
                                picBoxEatin.BackgroundImage = null;
                            }
                            picBoxEatin.BackgroundImage = Properties.Resources.icon_title_eatin;

                            if (picBoxEatin.BackgroundImage != null)
                            {
                                picBoxTakeout.BackgroundImage.Dispose();
                                picBoxTakeout.BackgroundImage = null;
                            }
                            picBoxTakeout.BackgroundImage = Properties.Resources.icon_title_takeout_dis;
                        }
                    }
                    else
                    {

                    }
                    break;
            }
        }

        //메뉴 추가 시, 주문목록의 해당 항목 깜빡임 이벤트
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if(nRunOldAnsyIndex != -1)
                orderListSet.panelMain[nRunOldAnsyIndex].BackColor = Color.Transparent;
            nRunOldAnsyIndex = nRunAnsyIndex;

            for (int i = 0; i < 3; i++)
            {
                UtilHelper.Delay(500);
                if (this.Visible == false || nRunAnsyIndex != (orderListSet.panelMain.Count - 1))
                    break;
                orderListSet.panelMain[nRunAnsyIndex].BackColor = Color.OrangeRed;
                UtilHelper.Delay(500);
                if (this.Visible == false || nRunAnsyIndex != (orderListSet.panelMain.Count - 1))
                    break;
                orderListSet.panelMain[nRunAnsyIndex].BackColor = Color.White;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    break;
            }
            base.OnFormClosing(e);
        }

        #endregion

    }

    //------클래스------//
    #region 클래스

    //class MenuItemProperty
    //{
    //    public int totalPage;
    //    public int pageNow;
    //    public int menuSelectIndex;
    //}

    //class WoniOrderListSet
    //{
    //    public List<DoubleBuffer_Panel> panelMain = new List<DoubleBuffer_Panel>();
    //    public List<DoubleBuffer_Panel> panelTitle = new List<DoubleBuffer_Panel>();
    //    public List<PictureBox> picBoxBtnPlus = new List<PictureBox>();
    //    public List<PictureBox> picBoxBtnMinus = new List<PictureBox>();
    //    public List<Label> labelTitle = new List<Label>();
    //    public List<Label> labelMemo = new List<Label>();
    //    public List<Label> labelQuan = new List<Label>();
    //    public List<Label> labelAmt = new List<Label>();
    //    public List<decimal> danga = new List<decimal>();
    //}

    //class MenuTypeGrp1 : MenuTypeGrp
    //{
    //    public MenuListType1[] menuGrpType1 = new MenuListType1[5];
    //}

    //class MenuTypeGrp2 : MenuTypeGrp
    //{
    //    public MenuListType2[] menuGrpType2 = new MenuListType2[5];
    //}

    //class MenuTypeGrp3 : MenuTypeGrp
    //{
    //    public MenuListType3[] menuGrpType3 = new MenuListType3[5];
    //}

    //class MenuListType1
    //{
    //    public List<DoubleBuffer_Panel> menuBox = new List<DoubleBuffer_Panel>();
    //    public List<DoubleBuffer_Panel> menuImgBox = new List<DoubleBuffer_Panel>();
    //    public List<string> type = new List<string>();
    //    public List<Label> menuTitle = new List<Label>();
    //    public List<Label> menuSingleTitle = new List<Label>();
    //    public List<Label> menuSetTitle = new List<Label>();
    //    public List<Label> menuSingleAmt = new List<Label>();
    //    public List<Label> menuSetAmt = new List<Label>();
    //    public List<string> menuMemo = new List<string>();
    //    public List<string> menuTCode = new List<string>();
    //    public List<bool> soldOut = new List<bool>();
    //}

    //class MenuListType2
    //{
    //    public List<DoubleBuffer_Panel> menuBox = new List<DoubleBuffer_Panel>();
    //    public List<DoubleBuffer_Panel> menuImgBox = new List<DoubleBuffer_Panel>();
    //    public List<Label> menuTitle = new List<Label>();
    //    public List<Label> menuSubTitle1 = new List<Label>();
    //    public List<Label> menuSubTitle2 = new List<Label>();
    //    public List<Label> menuSubTitle3 = new List<Label>();
    //    public List<Label> menuSubTitle4 = new List<Label>();
    //    public List<Label> menuAmt1 = new List<Label>();
    //    public List<Label> menuAmt2 = new List<Label>();
    //    public List<Label> menuAmt3 = new List<Label>();
    //    public List<Label> menuAmt4 = new List<Label>();
    //    public List<string> menuMemo = new List<string>();
    //    public List<string> menuTCode = new List<string>();
    //    public List<bool> soldOut = new List<bool>();
    //}

    //class MenuListType3
    //{
    //    public List<DoubleBuffer_Panel> menuBox = new List<DoubleBuffer_Panel>();
    //    public List<DoubleBuffer_Panel> menuImgBox = new List<DoubleBuffer_Panel>();
    //    public List<Label> menuTitle = new List<Label>();
    //    //public List<Label> menuSingelTitle = new List<Label>();
    //    public List<Label> menuAmt = new List<Label>();
    //    public List<string> menuMemo = new List<string>();
    //    public List<string> menuTCode = new List<string>();
    //    public List<bool> soldOut = new List<bool>();
    //}

    //class SetMenuGrp
    //{
    //    public SetMenuList[] setMenuGrp = new SetMenuList[6];
    //}

    //class SetMenuList
    //{
    //    public List<DoubleBuffer_Panel> menuBox = new List<DoubleBuffer_Panel>();
    //    public List<DoubleBuffer_Panel> menuImgBox = new List<DoubleBuffer_Panel>();
    //    public List<Label> menuTitle = new List<Label>();
    //    public List<Label> menuSingleTitle = new List<Label>();
    //    public List<Label> menuSetTitle = new List<Label>();
    //    public List<Label> menuSigleAmt = new List<Label>();
    //    public List<Label> menuSetAmt = new List<Label>();
    //    public List<string> menuTCode = new List<string>();
    //}

    #endregion
}
