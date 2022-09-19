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
    public partial class FrmSalesSearchDaou : Form
    {
        DBControl.SaleHDAO salehDao = new DBControl.SaleHDAO();
        DBControl.SaleDDAO saledDao = new DBControl.SaleDDAO();
        DBControl.VanDataDAO vanDataDao = new DBControl.VanDataDAO();
        DBControl.MenuListCfgDAO menuListCfgDao = new DBControl.MenuListCfgDAO();

        TheDaySales theDaySales = new TheDaySales();
        OneDaySales oneDaySales = new OneDaySales();
        MonthSales monthSales = new MonthSales();
        ItemSales itemSales = new ItemSales();
        GrpSales grpSales = new GrpSales();
        SalesCancel salesCancel = new SalesCancel();
        InpNm inpNm = new InpNm();
        ABCReport abcReport = new ABCReport();

        string ctrlName = string.Empty;
        int nSelectIndex = 0;
        bool isSound;

        public FrmSalesSearchDaou()
        {
            InitializeComponent();
        }

        public FrmSalesSearchDaou(Bitmap bitmap)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;

            salehDao.BRAND = saledDao.BRAND = vanDataDao.BRAND = menuListCfgDao.BRAND = StoreInfo.BrnadCode;
            salehDao.STORE = saledDao.BRAND = vanDataDao.STORE = menuListCfgDao.STORE = StoreInfo.StoreCode;
            salehDao.DESK = saledDao.DESK = vanDataDao.DESK = menuListCfgDao.DESK = StoreInfo.StoreDesk;
        }

        private void FrmSalesSearch_Load(object sender, EventArgs e)
        {
            woniPanel1.Left = (this.ClientSize.Width - woniPanel1.Width) / 2;
            woniPanel1.Top = (this.ClientSize.Height - woniPanel1.Height) / 2;

            lblStore.Text = StoreInfo.StoreName;
            lblKNum.Text = StoreInfo.StoreDesk;

            lblSalesDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2));

            lblStartDate.Text = lblStartDate.Text;
            lblEndDate.Text = lblEndDate.Text;
            PanelListInit();
            DgvMenuListInit();
            lblDash.Visible = false;
            lblEndDate.Visible = false;
            //panelItemList.Visible = false;

            panelList_Click(panelTheDaySales, null);
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

                case "panelSearch":
                    bitmap = UtilHelper.ChangeOpacity(panelSearch.Style.BackgroundImage, 1f);
                    panelSearch.Style.BackgroundImage.Dispose();
                    panelSearch.Style.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxReportPrint":
                    bitmap = UtilHelper.ChangeOpacity(picBoxReportPrint.Image, 1f);
                    picBoxReportPrint.Image.Dispose();
                    picBoxReportPrint.Image = bitmap;
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

                case "panelSearch":
                    panelSearch.Style.BackgroundImage.Dispose();
                    panelSearch.Style.BackgroundImage = Properties.Resources.btn_lookup;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxReportPrint":
                    picBoxReportPrint.Image.Dispose();
                    picBoxReportPrint.Image = Properties.Resources.btn_print;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;
            }
        }

        private void lblDate_Click(object sender, EventArgs e)
        {
            ctrlName = ((Control)sender).Name;

            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "lblStartDate":
                    lblStartDate.BackColor = Color.Orange;
                    UtilHelper.Delay(50);

                    DateClick_Proc();
                    break;

                case "lblEndDate":
                    lblEndDate.BackColor = Color.Orange;
                    UtilHelper.Delay(50);

                    DateClick_Proc();
                    break;
            }
        }

        private void panelList_Click(object sender, EventArgs e)
        {
            ctrlName = ((Control)sender).Name;

            if (isSound)
                UtilHelper.spWav.Play();
            else
                isSound = true;

            switch (ctrlName)
            {
                case "panelTheDaySales":
                    if (txtBoxReport.Visible == false)
                    {
                        txtBoxReport.Visible = true;
                        dgvMenuList.Visible = false;
                    }
                    PanelListInit();
                    panelTheDaySales.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelTheDaySales.Style.ForeColor.Color = Color.White;
                    txtBoxReport.Clear();
                    lblDash.Visible = false;
                    lblEndDate.Visible = false;
                    nSelectIndex = 0;

                    lblStartDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                        StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2));

                    TheDaySalesSearch();
                    break;

                case "panelOneDaySales":
                    if (txtBoxReport.Visible == false)
                    {
                        txtBoxReport.Visible = true;
                        dgvMenuList.Visible = false;
                    }
                    PanelListInit();
                    panelOneDaySales.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelOneDaySales.Style.ForeColor.Color = Color.White;
                    txtBoxReport.Clear();
                    lblDash.Visible = true;
                    lblEndDate.Visible = true;
                    nSelectIndex = 1;
                    lblStartDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                        StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2));
                    lblEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    break;

                case "panelMonthSales":
                    if (txtBoxReport.Visible == false)
                    {
                        txtBoxReport.Visible = true;
                        dgvMenuList.Visible = false;
                    }
                    PanelListInit();
                    panelMonthSales.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelMonthSales.Style.ForeColor.Color = Color.White;
                    txtBoxReport.Clear();
                    lblDash.Visible = true;
                    lblEndDate.Visible = true;
                    nSelectIndex = 2;
                    string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                        StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2));
                    lblEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    break;

                case "panelItemSales":
                    if (txtBoxReport.Visible == false)
                    {
                        txtBoxReport.Visible = true;
                        dgvMenuList.Visible = false;
                    }
                    PanelListInit();
                    panelItemSales.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelItemSales.Style.ForeColor.Color = Color.White;
                    txtBoxReport.Clear();
                    nSelectIndex = 3;
                    lblDash.Visible = true;
                    lblEndDate.Visible = true;
                    string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                        StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2));
                    lblEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    break;

                case "panelGrpSales":
                    if (txtBoxReport.Visible == false)
                    {
                        txtBoxReport.Visible = true;
                        dgvMenuList.Visible = false;
                    }
                    PanelListInit();
                    panelGrpSales.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelGrpSales.Style.ForeColor.Color = Color.White;
                    txtBoxReport.Clear();
                    nSelectIndex = 4;
                    lblDash.Visible = true;
                    lblEndDate.Visible = true;
                    lblStartDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                        StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2));
                    lblEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    break;

                case "panelSalesCancel":
                    if (txtBoxReport.Visible == false)
                    {
                        txtBoxReport.Visible = true;
                        dgvMenuList.Visible = false;
                    }
                    PanelListInit();
                    panelSalesCancel.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelSalesCancel.Style.ForeColor.Color = Color.White;
                    txtBoxReport.Clear();
                    nSelectIndex = 5;
                    lblDash.Visible = false;
                    lblEndDate.Visible = false;
                    lblStartDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                        StoreInfo.StoreOpen.Substring(4, 2),
                        StoreInfo.StoreOpen.Substring(6, 2));
                    lblEndDate.Text = lblStartDate.Text;
                    break;

                case "panelBuySales":
                    if (txtBoxReport.Visible == false)
                    {
                        txtBoxReport.Visible = true;
                        dgvMenuList.Visible = false;
                    }
                    PanelListInit();
                    panelBuySales.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelBuySales.Style.ForeColor.Color = Color.White;
                    txtBoxReport.Clear();
                    nSelectIndex = 6;
                    lblDash.Visible = true;
                    lblEndDate.Visible = true;
                    lblStartDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                        StoreInfo.StoreOpen.Substring(4, 2),
                        StoreInfo.StoreOpen.Substring(6, 2));
                    lblEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    break;

                case "panelABC":
                    if (txtBoxReport.Visible == false)
                    {
                        txtBoxReport.Visible = true;
                        dgvMenuList.Visible = false;
                    }
                    PanelListInit();
                    panelABC.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelABC.Style.ForeColor.Color = Color.White;
                    nSelectIndex = 8;
                    txtBoxReport.Clear();
                    lblDash.Visible = true;
                    lblEndDate.Visible = true;
                    lblStartDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                        StoreInfo.StoreOpen.Substring(4, 2),
                        StoreInfo.StoreOpen.Substring(6, 2));
                    lblEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    break;

                case "panelItemList":
                    PanelListInit();
                    picBoxReportPrint.Enabled = false;
                    txtBoxReport.Visible = false;
                    dgvMenuList.Visible = true;
                    dgvMenuList.Location = new Point(336, 399);
                    panelItemList.Style.BackgroundImage = Properties.Resources.btn_bg_300X100;
                    panelItemList.Style.ForeColor.Color = Color.White;
                    lblDash.Visible = false;
                    lblEndDate.Visible = false;
                    nSelectIndex = 9;
                    GetMenuList();
                    txtBoxReport.Clear();

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

                case "panelSearch":
                    switch (nSelectIndex)
                    {
                        case 0:
                            txtBoxReport.Text = string.Empty;
                            TheDaySalesSearch();
                            break;

                        case 1:
                            txtBoxReport.Text = string.Empty;
                            OneDaySalesSearch();
                            break;

                        case 2:
                            txtBoxReport.Text = string.Empty;
                            MonthSalesSearch();
                            break;

                        case 3:
                            txtBoxReport.Text = string.Empty;
                            ItemSalesSearch();
                            break;

                        case 4:
                            txtBoxReport.Text = string.Empty;
                            GrpSalesSearch();
                            break;

                        case 5:
                            txtBoxReport.Text = string.Empty;
                            SalesCancelSearch();
                            break;

                        case 6:
                            txtBoxReport.Text = string.Empty;
                            InpNmSalesSearch();
                            break;

                        case 7:
                            break;

                        case 8:
                            txtBoxReport.Text = string.Empty;
                            AbcReportSearch();
                            break;
                    }
                    break;

                case "picBoxReportPrint":
                    switch (nSelectIndex)
                    {
                        case 0:
                            TheDaySalesPrint();
                            break;

                        case 1:
                            OneDaySalesPrint();
                            break;

                        case 2:
                            MonthSalesPrint();
                            break;

                        case 3:
                            ItemSalesPrint();
                            break;

                        case 4:
                            GrpSalesPrint();
                            break;

                        case 5:
                            SalesCancelPrint();
                            break;

                        case 6:
                            InpNmSalesPrint();
                            break;

                        case 7:
                            break;

                        case 8:
                            ABCReportPrint();
                            break;

                    }
                    break;
            }
        }

        private void DateClick_Proc()
        {
            switch (ctrlName)
            {
                case "lblStartDate":
                    FrmCalendar sDate = new FrmCalendar(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location), lblStartDate.Text);

                    sDate.TopMost = true;

                    if (sDate.ShowDialog() == DialogResult.OK)
                    {
                        lblStartDate.BackColor = Color.White;
                        lblStartDate.Text = sDate.monthCalendarAdv1.SelectedDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        lblStartDate.BackColor = Color.White;
                    }
                    break;

                case "lblEndDate":
                    FrmCalendar eDate = new FrmCalendar(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location), lblEndDate.Text);

                    eDate.TopMost = true;

                    if (eDate.ShowDialog() == DialogResult.OK)
                    {
                        lblEndDate.BackColor = Color.White;
                        lblEndDate.Text = eDate.monthCalendarAdv1.SelectedDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        lblEndDate.BackColor = Color.White;
                    }
                    break;
            }
        }

        private void PanelListInit()
        {
            panelTheDaySales.Style.BackgroundImage = null;
            panelOneDaySales.Style.BackgroundImage = null;
            panelMonthSales.Style.BackgroundImage = null;
            panelItemSales.Style.BackgroundImage = null;
            panelGrpSales.Style.BackgroundImage = null;
            panelSalesCancel.Style.BackgroundImage = null;
            panelBuySales.Style.BackgroundImage = null;
            panelABC.Style.BackgroundImage = null;
            panelItemList.Style.BackgroundImage = null;

            panelTheDaySales.Style.ForeColor.Color = Color.RoyalBlue;
            panelOneDaySales.Style.ForeColor.Color = Color.RoyalBlue;
            panelMonthSales.Style.ForeColor.Color = Color.RoyalBlue;
            panelItemSales.Style.ForeColor.Color = Color.RoyalBlue;
            panelGrpSales.Style.ForeColor.Color = Color.RoyalBlue;
            panelSalesCancel.Style.ForeColor.Color = Color.RoyalBlue;
            panelBuySales.Style.ForeColor.Color = Color.RoyalBlue;
            panelABC.Style.ForeColor.Color = Color.RoyalBlue;
            panelItemList.Style.ForeColor.Color = Color.RoyalBlue;

            picBoxReportPrint.Enabled = true;
            dgvMenuList.Visible = false;
        }

        private void DgvMenuListInit()
        {
            DataGridViewTextBoxCell txtBoxCell = new DataGridViewTextBoxCell();

            foreach (DataGridViewColumn column in dgvMenuList.Columns)
            {
                Font font = new Font("나눔스퀘어", 12, FontStyle.Bold);

                column.DefaultCellStyle.Font = font;

                switch (column.Index)
                {
                    case 0:
                    case 1:
                    case 2:
                        column.HeaderCell.Style.Font = font;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case 3:
                    case 4:
                        column.HeaderCell.Style.Font = font;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                }
            }
        }

        #region 조회 출력 함수
        private void TheDaySalesSearch()
        {
            bool isEmpty = false;

            int nSpace = 30;

            txtBoxReport.Text = string.Format(PaddingBySpace("** 당일 매출 현황 ** \r\n\r\n", 72, true));
            txtBoxReport.AppendText(string.Format("{0, -60}{1, 10}", "영업일자 : " +
                string.Format("{0}.{1}.{2}", StoreInfo.StoreOpen.Substring(0, 4),
                StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2)),
                "KIOSK: " + StoreInfo.StoreDesk));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));
            txtBoxReport.AppendText(string.Format("\r\n{0}{1}{2}", PaddingBySpace("구분", 40, false),
                PaddingBySpace("건수", 15, true), PaddingBySpace("금액", 33, true)));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            using (DataTable dt = salehDao.TheDayCardSearch())
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr["건수"].ToString() != "0")
                            {
                                isEmpty = false;
                                theDaySales.cardCnt = string.Format("{0:#,#0}", dtr["건수"].ToDecimal());
                                theDaySales.cardAmt = string.Format("{0:#,#0}", dtr["금액"].ToDecimal());

                                string[] tmpAmt = theDaySales.cardAmt.Split(',');
                                int nCount = tmpAmt.Length - 1;

                                switch (nCount)
                                {
                                    case 0:
                                        nSpace = 28; break;

                                    case 1:
                                        nSpace = 30; break;

                                    case 2:
                                        nSpace = 32; break;

                                    case 3:
                                        nSpace = 34; break;
                                }
                            }
                            else
                            {
                                isEmpty = true;
                                nSpace = 28;
                                txtBoxReport.AppendText("조회된 목록이 없습니다.");
                            }
                        }
                        txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("카드", 40, false),
                        PaddingBySpace(theDaySales.cardCnt, 10, true), PaddingBySpace(theDaySales.cardAmt, nSpace, true)));
                    }
                }

            }

            if (isEmpty == false)
            {
                theDaySales.totCnt = string.Format("{0:##,##0}", Convert.ToInt32(theDaySales.cardCnt));
                theDaySales.totAmt = string.Format("{0:##,##0}", Convert.ToDecimal(theDaySales.cardAmt));

                if (theDaySales.totAmt == "0")
                    nSpace = 28;
                else
                {
                    string[] tmpAmt = theDaySales.totAmt.Split(',');
                    int Count = tmpAmt.Length - 1;

                    switch (Count)
                    {
                        case 0:
                            nSpace = 28; break;

                        case 1:
                            nSpace = 30; break;

                        case 2:
                            nSpace = 32; break;

                        case 3:
                            nSpace = 34; break;
                    }
                }
                txtBoxReport.AppendText("\r\n" + EqualGrid(68, "-"));
                txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("합계", 40, false),
                    PaddingBySpace(theDaySales.totCnt, 10, true), PaddingBySpace(theDaySales.totAmt, nSpace, true)));

                theDaySales.searchTime = DateTime.Now;
                txtBoxReport.AppendText("\r\n\r\n");
                txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + theDaySales.searchTime.ToString(
                    "yyyy.MM.dd HH:mm:ss") + " **", 86, true));
            }
        }

        private void OneDaySalesSearch()
        {
            oneDaySales.OneDaySalesClear();

            string[] sTmpDate = lblStartDate.Text.Split('-');
            string[] eTmpDate = lblEndDate.Text.Split('-');

            string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
            string eDate = eTmpDate[0] + eTmpDate[2] + eTmpDate[2];

            int nSpace = 30;

            txtBoxReport.AppendText(string.Format(PaddingBySpace("** 일별 매출 현황 **\r\n\r\n", 72, true)));
            txtBoxReport.AppendText(string.Format("{0, -54}{1, 10}", "조회일자 : " +
                string.Format("{0}.{1}.{2}~{3}.{4}.{5}", sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0],
                eTmpDate[1], eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            txtBoxReport.AppendText(string.Format("\r\n{0}{1}{2}", PaddingBySpace("구분", 40, false),
                PaddingBySpace("건수", 15, true), PaddingBySpace("금액", 33, true)));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            using (DataTable dt = salehDao.OneDayCardSearch(sDate, eDate))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        int i = 0;

                        while (dtr.Read())
                        {
                            txtBoxReport.AppendText(PaddingBySpace("[" + dtr["일자"].ToString().Substring(6, 2) + " 일]",
                            60, false));

                            oneDaySales.date.Add(dtr["일자"].ToString());
                            oneDaySales.cardCnt.Add(dtr["건수"].ToString());
                            oneDaySales.cardAmt.Add(string.Format("{0:##,##0}", Convert.ToDecimal(dtr["금액"].ToString())));

                            if (oneDaySales.cardAmt[i] == "0")
                            {
                                nSpace = 28;
                            }
                            else
                            {
                                string[] tmpAmt = oneDaySales.cardAmt[i].Split(',');
                                int nCount = tmpAmt.Length - 1;

                                switch (nCount)
                                {
                                    case 0:
                                        nSpace = 28; break;

                                    case 1:
                                        nSpace = 30; break;

                                    case 2:
                                        nSpace = 32; break;

                                    case 3:
                                        nSpace = 34; break;
                                }
                            }
                            txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("카드", 40, false),
                            PaddingBySpace(oneDaySales.cardCnt[i], 10, true), PaddingBySpace(
                                oneDaySales.cardAmt[i], nSpace, true)));

                            oneDaySales.totCnt.Add(string.Format("{0:##,##0}", Convert.ToInt32(oneDaySales.cardCnt[i])));
                            oneDaySales.totAmt.Add(string.Format("{0:##,##0}", Convert.ToDecimal(oneDaySales.cardAmt[i])));

                            if (oneDaySales.totAmt[i] == "0")
                                nSpace = 28;
                            else
                            {
                                string[] tmpAmt = oneDaySales.totAmt[i].Split(',');
                                int Count = tmpAmt.Length - 1;

                                switch (Count)
                                {
                                    case 0:
                                        nSpace = 28; break;

                                    case 1:
                                        nSpace = 30; break;

                                    case 2:
                                        nSpace = 32; break;

                                    case 3:
                                        nSpace = 34; break;
                                }
                            }
                            txtBoxReport.AppendText("\r\n" + EqualGrid(68, "-"));
                            txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("합계", 40, false),
                                PaddingBySpace(oneDaySales.totCnt[i], 10, true), PaddingBySpace(
                                    oneDaySales.totAmt[i], nSpace, true)));
                            i++;
                        }

                        oneDaySales.searchTime = DateTime.Now;
                        txtBoxReport.AppendText("\r\n\r\n");
                        txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + oneDaySales.searchTime.ToString(
                            "yyyy.MM.dd HH:mm:ss") + " **", 86, true));

                        return;
                    }

                }
                txtBoxReport.AppendText(PaddingBySpace("** 조회내역이 없습니다" + " **", 68, true));
                txtBoxReport.AppendText("\r\n\r\n");
                txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + DateTime.Now.ToString(
                    "yyyy.MM.dd HH:mm:ss") + " **", 86, true));
            }
        }


        private void MonthSalesSearch()
        {
            monthSales.MonthDaySalesClear();

            string[] sTmpDate = lblStartDate.Text.Split('-');
            string[] eTmpDate = lblEndDate.Text.Split('-');
            DateTime eTimeTmp = Convert.ToDateTime(lblEndDate.Text);
            int sLatDay = eTimeTmp.AddDays(1 - eTimeTmp.Day).AddMonths(1).AddDays(-1).Day;

            string sDate = sTmpDate[0] + sTmpDate[1] + string.Format("{0:D2}", 1);
            string eDate = eTmpDate[0] + eTmpDate[1] + string.Format("{0:D2}", sLatDay);

            int nSpace = 30;

            txtBoxReport.AppendText(string.Format(PaddingBySpace("** 월별 매출 현황 **\r\n\r\n", 72, true)));
            txtBoxReport.AppendText(string.Format("{0, -54}{1, 12}", "조회일자 : " + string.Format(
                "{0}.{1}~{2}.{3}", sTmpDate[0], sTmpDate[1], eTmpDate[0], eTmpDate[1]),
                "KIOSK: " + StoreInfo.StoreDesk));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            txtBoxReport.AppendText(string.Format("\r\n{0}{1}{2}", PaddingBySpace("구분", 40, false),
                PaddingBySpace("건수", 15, true), PaddingBySpace("금액", 33, true)));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            using (DataTable dt = salehDao.MonthCardSearch(sDate, eDate))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        int i = 0;

                        while (dtr.Read())
                        {
                            txtBoxReport.AppendText(PaddingBySpace("[" + dtr["일자"].ToString() + " 월]", 60, false));

                            monthSales.date.Add(dtr["일자"].ToString());
                            monthSales.cardCnt.Add(dtr["건수"].ToString());
                            monthSales.cardAmt.Add(string.Format("{0:##,##0}", Convert.ToDecimal(dtr["금액"].ToString())));

                            if (monthSales.cardAmt[i] == "0")
                                nSpace = 28;
                            else
                            {
                                string[] tmpAmt = monthSales.cardAmt[i].Split(',');
                                int Count = tmpAmt.Length - 1;

                                switch (Count)
                                {
                                    case 0:
                                        nSpace = 28; break;

                                    case 1:
                                        nSpace = 30; break;

                                    case 2:
                                        nSpace = 32; break;

                                    case 3:
                                        nSpace = 34; break;
                                }
                            }

                            txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("카드", 40, false),
                                PaddingBySpace(monthSales.cardCnt[i], 10, true),
                                PaddingBySpace(monthSales.cardAmt[i], nSpace, true)));

                            monthSales.totCnt.Add(string.Format("{0:##,##0}", Convert.ToInt32(monthSales.cardCnt[i])));
                            monthSales.totAmt.Add(string.Format("{0:##,##0}", Convert.ToDecimal(monthSales.cardAmt[i])));

                            if (monthSales.totAmt[i] == "0")
                                nSpace = 28;
                            else
                            {
                                string[] tmpAmt = monthSales.totAmt[i].Split(',');
                                int Count = tmpAmt.Length - 1;

                                switch (Count)
                                {
                                    case 0:
                                        nSpace = 28; break;

                                    case 1:
                                        nSpace = 30; break;

                                    case 2:
                                        nSpace = 32; break;

                                    case 3:
                                        nSpace = 34; break;
                                }
                            }
                            txtBoxReport.AppendText("\r\n" + EqualGrid(68, "-"));
                            txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("합계", 40, false),
                                PaddingBySpace(monthSales.totCnt[i], 10, true),
                                PaddingBySpace(monthSales.totAmt[i], nSpace, true)));

                            i++;
                        }
                        monthSales.searchTime = DateTime.Now;
                        txtBoxReport.AppendText("\r\n\r\n");
                        txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + DateTime.Now.ToString(
                            "yyyy.MM.dd HH:mm:ss") + " **", 86, true));

                        return;
                    }
                }
                txtBoxReport.AppendText(PaddingBySpace("** 조회내역이 없습니다" + " **", 68, true));
                txtBoxReport.AppendText("\r\n\r\n");
                txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + DateTime.Now.ToString(
                    "yyyy.MM.dd HH:mm:ss") + " **", 86, true));
            }
        }

        private void ItemSalesSearch()
        {
            itemSales.ItemSalesClear();

            string[] sTmpDate = lblStartDate.Text.Split('-');
            string[] eTmpDate = lblEndDate.Text.Split('-');

            string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
            string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

            int nSpace = 30;

            txtBoxReport.Text = string.Format(PaddingBySpace("** 품목별 매출집계 현황 ** \r\n\r\n", 80, true));
            txtBoxReport.AppendText(string.Format("{0, -54}{1, 10}", "조회일자 : " +
                string.Format("{0}.{1}.{2}~{3}.{4}.{5}", sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1],
                eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));

            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));
            txtBoxReport.AppendText(string.Format("\r\n{0}{1}{2}", PaddingBySpace("구분", 40, false),
                PaddingBySpace("건수", 15, true), PaddingBySpace("금액", 33, true)));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            using (DataTable dt = saledDao.ItemSearch(sDate, eDate))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        int i = 0;

                        while (dtr.Read())
                        {
                            if (dtr["수량"].ToString().CompareTo("0") != 0)
                            {
                                int tmpCnt = dtr["수량"].ToInteger();
                                decimal tmpAmt = dtr["금액"].ToDecimal();

                                itemSales.totCnt += tmpCnt;
                                itemSales.totAmt += tmpAmt;

                                itemSales.itemName.Add(dtr["품목"].ToString());

                                itemSales.quan.Add(string.Format("{0:##,#0}", tmpCnt));
                                itemSales.amt.Add(string.Format("{0:##,#0}", tmpAmt));

                                if (itemSales.amt[i].CompareTo("0") == 0)
                                    nSpace = 28;
                                else
                                {
                                    string[] spaceAmt = itemSales.amt[i].Split(',');
                                    int count = spaceAmt.Length - 1;

                                    switch (count)
                                    {
                                        case 0:
                                            nSpace = 28; break;

                                        case 1:
                                            nSpace = 30; break;

                                        case 2:
                                            nSpace = 32; break;

                                        case 3:
                                            nSpace = 34; break;
                                    }
                                }
                                txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}",
                                PaddingBySpace(itemSales.itemName[i], 40, false),
                                PaddingBySpace(itemSales.quan[i], 10, true),
                                PaddingBySpace(itemSales.amt[i], nSpace, true)));
                                i++;
                            }
                        }
                        string totalAmt = string.Format("{0:##,##0}", itemSales.totAmt);

                        if (totalAmt == "0")
                            nSpace = 28;
                        else
                        {
                            string[] spaceAmt = totalAmt.Split(',');
                            int count = spaceAmt.Length - 1;

                            switch (count)
                            {
                                case 0:
                                    nSpace = 28; break;

                                case 1:
                                    nSpace = 30; break;

                                case 2:
                                    nSpace = 32; break;

                                case 3:
                                    nSpace = 34; break;
                            }
                        }

                        txtBoxReport.AppendText("\r\n" + EqualGrid(68, "-"));
                        txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("합계", 40, false),
                            PaddingBySpace(string.Format("{0:##,##0}", itemSales.totCnt), 10, true),
                            PaddingBySpace(string.Format("{0:##,##0}", itemSales.totAmt), nSpace, true)));

                        itemSales.searchTime = DateTime.Now;
                        txtBoxReport.AppendText("\r\n\r\n");
                        txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " +
                            itemSales.searchTime.ToString("yyyy.MM.dd HH:mm:ss") + " **", 86, true));

                        return;
                    }
                }
                txtBoxReport.AppendText(PaddingBySpace("** 조회내역이 없습니다" + " **", 68, true));
                txtBoxReport.AppendText("\r\n\r\n");
                txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") +
                    " **", 86, true));
            }
        }

        private void GrpSalesSearch()
        {
            grpSales.GrpSalesClear();

            string[] sTmpDate = lblStartDate.Text.Split('-');
            string[] eTmpDate = lblEndDate.Text.Split('-');
            string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
            string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

            int nSpace = 30;

            txtBoxReport.Text = string.Format(PaddingBySpace("** 부분별 매출 조회 **\r\n\r\n", 75, true));
            txtBoxReport.AppendText(string.Format("{0, -54}{1, 10}", "조회일자 : " +
                string.Format("{0}.{1}.{2}~{3}.{4}.{5}", sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0],
                eTmpDate[1], eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));

            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));
            txtBoxReport.AppendText(string.Format("\r\n{0}{1}{2}", PaddingBySpace("부문", 40, false),
                PaddingBySpace("수량", 15, true), PaddingBySpace("금액", 33, true)));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            using (DataTable dt = saledDao.GrpSearch(sDate, eDate))
            {
                if (dt.Rows.Count != 0)
                {
                    DataTableReader dtr = new DataTableReader(dt);
                    int i = 0;

                    while (dtr.Read())
                    {
                        if (dtr["수량"].ToString() != "0")
                        {
                            int tmpCnt = Convert.ToInt32(dtr["수량"].ToString());
                            decimal tmpAmt = Convert.ToDecimal(dtr["금액"].ToString());

                            grpSales.totCnt += tmpCnt;
                            grpSales.totAmt += tmpAmt;

                            grpSales.grpName.Add(dtr["그룹"].ToString());
                            grpSales.quan.Add(string.Format("{0:##,##0}", tmpCnt));
                            grpSales.amt.Add(string.Format("{0:##,##0}", tmpAmt));

                            if (grpSales.amt[i] == "0")
                                nSpace = 28;
                            else
                            {
                                string[] spaceAmt = grpSales.amt[i].Split(',');
                                int Count = spaceAmt.Length - 1;

                                switch (Count)
                                {
                                    case 0:
                                        nSpace = 28; break;

                                    case 1:
                                        nSpace = 30; break;

                                    case 2:
                                        nSpace = 32; break;

                                    case 3:
                                        nSpace = 34; break;
                                }
                            }
                            txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace(grpSales.grpName[i], 40, false),
                                PaddingBySpace(grpSales.quan[i], 10, true), PaddingBySpace(grpSales.amt[i], nSpace, true)));
                            i++;
                        }
                    }
                    string totalAmt = string.Format("{0:##,##0}", grpSales.totAmt);

                    if (totalAmt == "0")
                        nSpace = 28;
                    else
                    {
                        string[] spaceAmt = totalAmt.Split(',');
                        int Count = spaceAmt.Length - 1;

                        switch (Count)
                        {
                            case 0:
                                nSpace = 28; break;

                            case 1:
                                nSpace = 30; break;

                            case 2:
                                nSpace = 32; break;

                            case 3:
                                nSpace = 34; break;
                        }
                    }
                    txtBoxReport.AppendText("\r\n" + EqualGrid(68, "-"));
                    txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("합계", 40, false),
                        PaddingBySpace(string.Format("{0:##,##0}", grpSales.totCnt), 10, true), PaddingBySpace(totalAmt, nSpace, true)));

                    grpSales.searchTime = DateTime.Now;
                    txtBoxReport.AppendText("\r\n\r\n");
                    txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + grpSales.searchTime.ToString("yyyy.MM.dd HH:mm:ss") +
                        " **", 86, true));

                    return;
                }

                txtBoxReport.AppendText(PaddingBySpace("** 조회내역이 없습니다" + " **", 68, true));
                txtBoxReport.AppendText("\r\n\r\n");
                txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") +
                    " **", 86, true));
            }
        }

        private void SalesCancelSearch()
        {
            salesCancel.SaleCancelClear();

            string[] sTmpDate = lblStartDate.Text.Split('-');
            string[] eTmpDate = lblEndDate.Text.Split('-');
            string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
            string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

            int nSpace = 30;

            txtBoxReport.Text = string.Format(PaddingBySpace("** 매출취소 현황 **\r\n\r\n", 72, true));
            txtBoxReport.AppendText(string.Format("{0, -60}{1, 10}", "영업일자 : " +
                string.Format("{0}.{1}.{2}", StoreInfo.StoreOpen.Substring(0, 4),
                StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2)),
                "KIOSK: " + StoreInfo.StoreDesk));

            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));
            txtBoxReport.AppendText(string.Format("\r\n{0}{1}{2}{3}", PaddingBySpace("일자", 15, false),
                PaddingBySpace("영수증번호", 20, false), PaddingBySpace("카드사", 15, true),
                PaddingBySpace("금액", 33, true)));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            using (DataTable dt = vanDataDao.SalesCancelSearch(sDate, eDate))
            {
                if (dt.Rows.Count != 0)
                {
                    int i = 0;
                    DataTableReader dtr = new DataTableReader(dt);

                    while (dtr.Read())
                    {
                        decimal tmpAmt = Convert.ToDecimal(dtr["금액"].ToString());

                        salesCancel.totAmt += tmpAmt;
                        salesCancel.date.Add(dtr["일자"].ToString());
                        salesCancel.receNo.Add(dtr["영수증번호"].ToString());
                        salesCancel.cardName.Add(dtr["카드사명"].ToString().Trim());
                        salesCancel.amt.Add(string.Format("{0:##,##0}", tmpAmt));

                        if (salesCancel.amt[i] == "0")
                            nSpace = 13;
                        else
                        {
                            string[] spaceAmt = salesCancel.amt[i].Split(',');
                            int Count = spaceAmt.Length - 1;

                            switch (Count)
                            {
                                case 0:
                                    nSpace = 13; break;

                                case 1:
                                    nSpace = 15; break;

                                case 2:
                                    nSpace = 17; break;

                                case 3:
                                    nSpace = 19; break;
                            }
                        }
                        if (salesCancel.cardName[i].Length <= 10)
                            salesCancel.cardName[i] += "\t";

                        txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}\t{3}",
                            PaddingBySpace(salesCancel.date[i], 15, false),
                            PaddingBySpace(salesCancel.receNo[i], 15, true),
                            PaddingBySpace("      " + salesCancel.cardName[i], 30, true),
                            PaddingBySpace(salesCancel.amt[i], nSpace, true)));
                        i++;
                    }
                    salesCancel.totCnt = dt.Rows.Count;

                    string totalAmt = string.Format("{0:##,##0}", salesCancel.totAmt);

                    if (totalAmt == "0")
                        nSpace = 28;
                    else
                    {
                        string[] spaceAmt = totalAmt.Split(',');
                        int Count = spaceAmt.Length - 1;

                        switch (Count)
                        {
                            case 0:
                                nSpace = 28; break;

                            case 1:
                                nSpace = 30; break;

                            case 2:
                                nSpace = 32; break;

                            case 3:
                                nSpace = 34; break;
                        }
                    }
                    txtBoxReport.AppendText("\r\n" + EqualGrid(68, "-"));

                    txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("합계", 40, false),
                        PaddingBySpace(string.Format("{0:##,##0}", salesCancel.totCnt), 10, false),
                        PaddingBySpace(totalAmt, nSpace, true)));

                    salesCancel.searchTime = DateTime.Now;
                    txtBoxReport.AppendText("\r\n\r\n");
                    txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " +
                        salesCancel.searchTime.ToString("yyyy.MM.dd HH:mm:ss") + " **", 86, true));

                    return;
                }

                txtBoxReport.AppendText(PaddingBySpace("** 조회내역이 없습니다" + " **", 68, true));
                txtBoxReport.AppendText("\r\n\r\n");
                txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") +
                    " **", 86, true));
            }

        }

        private void InpNmSalesSearch()
        {
            List<int> nCancelCnt = new List<int>();
            List<decimal> nCancelAmt = new List<decimal>();

            //int nTotCancelCnt = 0;
            decimal nTotCancelAmt = 0;

            inpNm.InpNmClear();
            string[] sTmpDate = lblStartDate.Text.Split('-');
            string[] eTmpDate = lblEndDate.Text.Split('-');
            string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
            string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

            int nSpace = 30;

            txtBoxReport.Text = string.Format(PaddingBySpace("** 매입사별 매출 현황 **\r\n\r\n", 75, true));
            txtBoxReport.AppendText(string.Format("{0, -52}{1, 12}", "영업일자 : " +
                string.Format("{0}.{1}.{2}~{3}.{4}.{5}",
                sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1], eTmpDate[2]), "KIOSK: " +
                StoreInfo.StoreDesk));

            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));
            txtBoxReport.AppendText(string.Format("\r\n{0}{1}{2}", PaddingBySpace("매입사", 40, false),
                PaddingBySpace("승인건수", 20, true), PaddingBySpace("금액", 25, true)));
            txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

            using (DataTable dt = vanDataDao.InpnmSearch(sDate, eDate))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        int i = 0;

                        while (dtr.Read())
                        {
                            int tmpCnt = Convert.ToInt32(dtr["건수"].ToString());
                            decimal tmpAmt = Convert.ToDecimal(dtr["금액"].ToString());

                            inpNm.totCnt += tmpCnt;
                            inpNm.totAmt += tmpAmt;

                            inpNm.inpName.Add(dtr["매입사명"].ToString());

                            inpNm.quan.Add(string.Format("{0:##,##0}", tmpCnt));
                            inpNm.amt.Add(string.Format("{0:##,##0}", tmpAmt));

                            if (inpNm.amt[i] == "0")
                                nSpace = 28;
                            else
                            {
                                string[] spaceAmt = inpNm.amt[i].Split(',');
                                int Count = spaceAmt.Length - 1;

                                switch (Count)
                                {
                                    case 0:
                                        nSpace = 28; break;

                                    case 1:
                                        nSpace = 30; break;

                                    case 2:
                                        nSpace = 32; break;

                                    case 3:
                                        nSpace = 34; break;
                                }
                            }
                            Encoding Encod = Encoding.Default;
                            byte[] bTmpName = Encod.GetBytes(inpNm.inpName[i]);
                            string strTab;

                            if (bTmpName.Length < 7)
                                strTab = "\t\t";
                            else
                                strTab = "\t";
                            txtBoxReport.AppendText(string.Format("\r\n{0}{1}{2}\t{3}",
                                PaddingBySpace(inpNm.inpName[i], 42, false), strTab,
                                PaddingBySpace(inpNm.quan[i], 10, true),
                                PaddingBySpace(inpNm.amt[i], nSpace, true)));
                            i++;
                        }
                        string totalAmt = string.Format("{0:##,##0}", inpNm.totAmt - nTotCancelAmt);

                        if (totalAmt == "0")
                            nSpace = 28;
                        else
                        {
                            string[] spaceAmt = totalAmt.Split(',');
                            int Count = spaceAmt.Length - 1;

                            switch (Count)
                            {
                                case 0:
                                    nSpace = 28;
                                    break;

                                case 1:
                                    nSpace = 30;
                                    break;

                                case 2:
                                    nSpace = 32;
                                    break;

                                case 3:
                                    nSpace = 34;
                                    break;
                            }
                        }


                        txtBoxReport.AppendText("\r\n" + EqualGrid(68, "-"));


                        txtBoxReport.AppendText(string.Format("\r\n{0}\t{1}\t{2}", PaddingBySpace("합계", 40, false),
                            PaddingBySpace(string.Format("{0:##,##0}", inpNm.totCnt), 10, true),
                            PaddingBySpace(totalAmt, nSpace, true)));

                        inpNm.searchTime = DateTime.Now;
                        txtBoxReport.AppendText("\r\n\r\n");
                        txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " +
                            inpNm.searchTime.ToString("yyyy.MM.dd HH:mm:ss") + " **", 86, true));

                        return;
                    }
                }
                txtBoxReport.AppendText(PaddingBySpace("** 조회내역이 없습니다" + " **", 68, true));
                txtBoxReport.AppendText("\r\n\r\n");
                txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") +
                    " **", 86, true));
            }
        }

        private void AbcReportSearch()
        {
            abcReport.ABCReportClear();
            string[] sTmpDate = lblStartDate.Text.Split('-');
            string[] eTmpDate = lblEndDate.Text.Split('-');
            string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
            string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

            using (DataTable dt = saledDao.ABCReport(sDate, eDate))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        int i = 0;
                        while (dtr.Read())
                        {
                            switch (i)
                            {
                                case 0:
                                    abcReport.abc.Add("A");
                                    break;

                                case 1:
                                    abcReport.abc.Add("B");
                                    break;

                                case 2:
                                    abcReport.abc.Add("C");
                                    break;
                            }

                            abcReport.itemName.Add(dtr["SD_PNAM"].ToString());
                            abcReport.quan.Add(dtr["CNT"].ToString());
                            abcReport.amt.Add(string.Format("{0:##,##0}", Convert.ToDecimal(dtr["AMT"].ToString())));

                            abcReport.per.Add(string.Format("{0}%", GetPercentage(Convert.ToDouble(dtr["CNT"].ToString()),
                                Convert.ToInt32(dtr["TOT"].ToString()), 0)));

                            i++;
                        }
                    }
                    int nSpace = 30;

                    txtBoxReport.Text = string.Format(PaddingBySpace("** ABC 리포트 **\r\n\r\n", 70, true));
                    txtBoxReport.AppendText(string.Format("{0, -52}{1, 12}", "영업일자 : " +
                        string.Format("{0}.{1}.{2}~{3}.{4}.{5}",
                        sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1], eTmpDate[2]), "KIOSK: " +
                        StoreInfo.StoreDesk));

                    txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));
                    txtBoxReport.AppendText(string.Format("\r\n{0}{1}\t{2}{3}{4}", PaddingBySpace("ABC", 10, false),
                        PaddingBySpace("품목", 15, true), PaddingBySpace("수량", 32, true), PaddingBySpace("금액", 13, true),
                        PaddingBySpace("%", 10, true)));
                    txtBoxReport.AppendText("\r\n" + EqualGrid(41, "="));

                    if (abcReport.itemName.Count != 0)
                    {
                        for (int i = 0; i < abcReport.itemName.Count; i++)
                        {
                            string[] spaceAmt = abcReport.amt[i].Split(',');
                            int Count = spaceAmt.Length - 1;

                            switch (Count)
                            {
                                case 0:
                                    nSpace = 16;
                                    break;

                                case 1:
                                    nSpace = 18;
                                    break;

                                case 2:
                                    nSpace = 20;
                                    break;

                                case 3:
                                    nSpace = 22;
                                    break;

                            }
                            Encoding Encod = Encoding.Default;
                            byte[] bTmpName = Encod.GetBytes(abcReport.itemName[i]);
                            string strTab;

                            if (bTmpName.Length > 24)
                                strTab = "\t";
                            else
                                strTab = "\t\t";

                            txtBoxReport.AppendText(string.Format("\r\n{0, -3}{1}{2}{3:3}{4}{5}",
                                    PaddingBySpace(abcReport.abc[i], 1, false),
                                    PaddingBySpace(abcReport.itemName[i], 30, false), strTab,
                                    PaddingBySpace(abcReport.quan[i], 3, true),
                                    PaddingBySpace(abcReport.amt[i], nSpace, true),
                                    PaddingBySpace(abcReport.per[i], 10, true)));
                        }

                        txtBoxReport.AppendText("\r\n" + EqualGrid(68, "-"));

                        abcReport.searchTime = DateTime.Now;
                        txtBoxReport.AppendText("\r\n\r\n");
                        txtBoxReport.AppendText(PaddingBySpace("** 조회시간 : " +
                            abcReport.searchTime.ToString("yyyy.MM.dd HH:mm:ss") + " **", 86, true));

                    }
                }
            }
        }
        #endregion

        private void GetMenuList()
        {
            DataTable dt = null;
            DataTableReader dtr = null;

            using (dt = menuListCfgDao.MenuListSelectL())
            {
                if (dt.Rows.Count != 0)
                {
                    dgvMenuList.Rows.Clear();

                    int i = 0;

                    using (dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            string page = (dtr["MT_PAGE"].ToInteger() + 1).ToString();
                            string no = (dtr["MT_COLROW"].ToInteger() + 1).ToString();
                            string grp = dtr["MT_GNAM"].ToString();
                            string name = dtr["MT_TNAM"].ToString();
                            string soldYn = "";

                            if (dtr["MT_SOLDYN"].ToString().CompareTo("N") == 0)
                                soldYn = "정상";
                            else
                                soldYn = "품절";
                           
                            string[] rows = { page, no, grp, name, soldYn};
                            dgvMenuList.Rows.Add(rows);
                            dgvMenuList.Rows[i].Height = 50;

                            if (soldYn.CompareTo("품절") == 0)
                                dgvMenuList.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                            else
                                dgvMenuList.Rows[i].Cells[4].Style.ForeColor = Color.Blue;
                            i++;
                        }
                        dgvMenuList.ClearSelection();
                    }
                }
            }
        }

        private double GetPercentage(double value, double total, int decimalplaces)
        {
            return System.Math.Round(value * 100 / total, decimalplaces);
        }

        private string PaddingBySpace(string name, int cnt, bool left)
        {
            Encoding Encod = Encoding.GetEncoding("ks_c_5601-1987");
            string tmpName = string.Empty;
            byte[] buf = Encod.GetBytes(name);
            string[] tmps = tmpName.Split(new string[] { "," }, StringSplitOptions.None);
            int Count = tmps.Length - 1;

            //if (Count > 1)
            //    cnt += 2;
            int Length = cnt - buf.Length;

            tmpName = Encod.GetString(buf);


            if (left)
                return tmpName.PadLeft(Length, ' ');
            else
                return tmpName.PadRight(Length, ' ');
        }

        private string EqualGrid(int cnt, string sy)
        {
            string strEqual = string.Empty;

            for (int i = 0; i < cnt; i++)
                strEqual += sy;

            return strEqual;
        }

        #region 프린터 출력함수
        private void TheDaySalesPrint()
        {
            try
            {
                serialPortReport.PortName = StoreInfo.ReceiptPrn;
                serialPortReport.BaudRate = StoreInfo.ReceiptRate;
                serialPortReport.DataBits = 8;
                serialPortReport.Parity = System.IO.Ports.Parity.None;
                serialPortReport.StopBits = System.IO.Ports.StopBits.One;
                serialPortReport.Encoding = System.Text.Encoding.Default;

                if (serialPortReport.IsOpen == false)
                    serialPortReport.Open();

                serialPortReport.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));

                serialPortReport.WriteLine("** 당일 매출 현황 **\r\n");
                serialPortReport.WriteLine(string.Format("{0, -32}{1, 10}", "영업일자 : " + string.Format("{0}.{1}.{2}",
                StoreInfo.StoreOpen.Substring(0, 4), StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2)),
               "KIOSK: " + StoreInfo.StoreDesk));
                serialPortReport.WriteLine(EqualGrid(46, "="));

                serialPortReport.WriteLine(string.Format("{0}{1}{2}", PaddingBySpace("구분", 23, false),
                        PaddingBySpace("건수", 8, false), PaddingBySpace("금액", 20, true)));
                serialPortReport.WriteLine(EqualGrid(46, "="));

                serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "카드", theDaySales.cardCnt, string.Format("{0:##,##0}", Convert.ToDecimal(theDaySales.cardAmt))));
                //serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "티머니", theDaySales.TMoneyCnt, string.Format("{0:##,##0}", Convert.ToDecimal(theDaySales.TMoneyAmt))));
                serialPortReport.WriteLine(EqualGrid(46, "-"));
                serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "합계", theDaySales.totCnt, string.Format("{0:##,##0}", Convert.ToDecimal(theDaySales.totAmt))));

                serialPortReport.WriteLine("\r\n");
                serialPortReport.WriteLine("**조회시간 : " + theDaySales.searchTime.ToString("yyyy.MM.dd HH: mm:ss") + " **\r\n");

                serialPortReport.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReport.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReport.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1B) + "S");
            }
            catch
            {
                serialPortReport.Close();
            }

            finally
            {
                serialPortReport.Close();
            }
        }

        private void OneDaySalesPrint()
        {
            if (oneDaySales.cardCnt.Count == 0)
                return;

            try
            {
                string[] sTmpDate = lblStartDate.Text.Split('-');
                string[] eTmpDate = lblEndDate.Text.Split('-');

                string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
                string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

                serialPortReport.PortName = StoreInfo.ReceiptPrn;
                serialPortReport.BaudRate = StoreInfo.ReceiptRate;
                serialPortReport.DataBits = 8;
                serialPortReport.Parity = System.IO.Ports.Parity.None;
                serialPortReport.StopBits = System.IO.Ports.StopBits.One;
                serialPortReport.Encoding = System.Text.Encoding.Default;

                if (serialPortReport.IsOpen == false)
                    serialPortReport.Open();

                serialPortReport.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));

                serialPortReport.WriteLine("** 일별 매출 현황 **\r\n");
                serialPortReport.WriteLine(string.Format("{0, -32}{1, 10}", "조회일자 : " + string.Format("{0}.{1}.{2}~{3}.{4}.{5}",
                sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1], eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));
                serialPortReport.WriteLine(EqualGrid(46, "="));
                serialPortReport.WriteLine(string.Format("{0}{1}{2}", PaddingBySpace("구분", 23, false),
                       PaddingBySpace("건수", 8, false), PaddingBySpace("금액", 20, true)));
                serialPortReport.WriteLine(EqualGrid(46, "="));

                for (int i = 0; i < oneDaySales.date.Count; i++)
                {
                    serialPortReport.WriteLine(string.Format("{0, -50}", "[" + oneDaySales.date[i].Substring(6, 2) + " 일]"));
                    serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "카드", oneDaySales.cardCnt[i], string.Format("{0:##,##0}", Convert.ToDecimal(oneDaySales.cardAmt[i]))));
                    //serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "티머니", oneDaySales.TMoneyCnt[i], string.Format("{0:##,##0}", Convert.ToDecimal(oneDaySales.TMoneyAmt[i]))));
                    serialPortReport.WriteLine(EqualGrid(46, "-"));
                    serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "합계", oneDaySales.totCnt[i], string.Format("{0:##,##0}", Convert.ToDecimal(oneDaySales.totAmt[i]))));
                }

                serialPortReport.WriteLine("\r\n");
                serialPortReport.WriteLine("**조회시간 : " + theDaySales.searchTime.ToString("yyyy.MM.dd HH: mm:ss") + " **\r\n");

                serialPortReport.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReport.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReport.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1B) + "S");
            }
            catch
            {
                serialPortReport.Close();
            }

            finally
            {
                serialPortReport.Close();
            }

        }

        private void MonthSalesPrint()
        {
            if (monthSales.cardCnt.Count == 0)
                return;

            try
            {
                string[] sTmpDate = lblStartDate.Text.Split('-');
                string[] eTmpDate = lblEndDate.Text.Split('-');

                string sDate = sTmpDate[0] + sTmpDate[1];
                string eDate = eTmpDate[0] + eTmpDate[1];

                serialPortReport.PortName = StoreInfo.ReceiptPrn;
                serialPortReport.BaudRate = StoreInfo.ReceiptRate;
                serialPortReport.DataBits = 8;
                serialPortReport.Parity = System.IO.Ports.Parity.None;
                serialPortReport.StopBits = System.IO.Ports.StopBits.One;
                serialPortReport.Encoding = System.Text.Encoding.Default;

                if (serialPortReport.IsOpen == false)
                    serialPortReport.Open();

                serialPortReport.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));

                serialPortReport.WriteLine("** 월별 매출 현황 **\r\n");
                serialPortReport.WriteLine(string.Format("{0, -32}{1, 10}", "조회일자 : " + string.Format("{0}.{1}~{2}.{3}",
                sTmpDate[0], sTmpDate[1], eTmpDate[0], eTmpDate[1]), "KIOSK: " + StoreInfo.StoreDesk));
                serialPortReport.WriteLine(EqualGrid(50, "="));
                serialPortReport.WriteLine(string.Format("{0}{1}{2}", PaddingBySpace("구분", 23, false),
                       PaddingBySpace("건수", 8, false), PaddingBySpace("금액", 20, true)));
                serialPortReport.WriteLine(EqualGrid(50, "="));

                for (int i = 0; i < monthSales.date.Count; i++)
                {
                    serialPortReport.WriteLine(string.Format("{0, -50}", "[" + monthSales.date[i] + " 월]"));
                    serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "카드", monthSales.cardCnt[i], string.Format("{0:##,##0}", Convert.ToDecimal(monthSales.cardAmt[i]))));
                    //serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "티머니", monthSales.TMoneyCnt[i], string.Format("{0:##,##0}", Convert.ToDecimal(monthSales.TMoneyAmt[i]))));
                    serialPortReport.WriteLine(EqualGrid(50, "-"));
                    serialPortReport.WriteLine(string.Format("{0, -10}\t{1, 8}\t{2, 15}", "합계", monthSales.totCnt[i], string.Format("{0:##,##0}", Convert.ToDecimal(monthSales.totAmt[i]))));
                }

                serialPortReport.WriteLine("\r\n");
                serialPortReport.WriteLine("**조회시간 : " + monthSales.searchTime.ToString("yyyy.MM.dd HH: mm:ss") + " **\r\n");

                serialPortReport.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReport.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReport.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1B) + "S");
            }
            catch
            {
                serialPortReport.Close();
            }

            finally
            {
                serialPortReport.Close();
            }
        }

        private void ItemSalesPrint()
        {
            if (itemSales.quan.Count == 0)
                return;

            try
            {
                string[] sTmpDate = lblStartDate.Text.Split('-');
                string[] eTmpDate = lblEndDate.Text.Split('-');

                string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
                string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

                serialPortReport.PortName = StoreInfo.ReceiptPrn;
                serialPortReport.BaudRate = StoreInfo.ReceiptRate;
                serialPortReport.DataBits = 8;
                serialPortReport.Parity = System.IO.Ports.Parity.None;
                serialPortReport.StopBits = System.IO.Ports.StopBits.One;
                serialPortReport.Encoding = System.Text.Encoding.Default;

                if (serialPortReport.IsOpen == false)
                    serialPortReport.Open();

                serialPortReport.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));

                serialPortReport.WriteLine("** 품목별 매출 현황 **\r\n");
                serialPortReport.WriteLine(string.Format("{0, -32}{1, 10}", "조회일자 : " + string.Format("{0}.{1}.{2}~{3}.{4}.{5}",
                sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1], eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));
                serialPortReport.WriteLine(EqualGrid(46, "="));
                serialPortReport.WriteLine(string.Format("{0}{1}{2}", PaddingBySpace("품목", 23, false),
                       PaddingBySpace("수량", 8, false), PaddingBySpace("금액", 20, true)));
                serialPortReport.WriteLine(EqualGrid(46, "="));

                for (int i = 0; i < itemSales.itemName.Count; i++)
                {
                    Encoding Encod = Encoding.Default;
                    byte[] bTmpName = Encod.GetBytes(itemSales.itemName[i]);
                    string strTab;

                    if (bTmpName.Length > 28)
                        strTab = "\t";
                    else if (bTmpName.Length > 19)
                        strTab = "\t\t";
                    else
                        strTab = "\t\t\t";

                    //if (itemSales.ItemName[i].Length < 10)
                    //    itemSales.ItemName[i] += "\t";
                    serialPortReport.WriteLine(string.Format("{0, 24}{1, 3}\t{2, 8}", UtilHelper.PadingLeftBySpace(itemSales.itemName[i], 24),
                        itemSales.quan[i], string.Format("{0:##,##0}", itemSales.amt[i])));
                }
                serialPortReport.WriteLine(EqualGrid(46, "-"));
                serialPortReport.WriteLine(string.Format("{0, -20}\t{1, 3}\t{2, 5}", "합계\t", itemSales.totCnt, string.Format("{0:##,##0}", Convert.ToDecimal(itemSales.totAmt))));
                serialPortReport.WriteLine("\r\n");
                serialPortReport.WriteLine("**조회시간 : " + itemSales.searchTime.ToString("yyyy.MM.dd HH: mm:ss") + " **\r\n");

                serialPortReport.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReport.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReport.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1B) + "S");
            }
            catch
            {
                serialPortReport.Close();
            }
            finally
            {
                serialPortReport.Close();
            }
        }

        private void GrpSalesPrint()
        {
            if (grpSales.quan.Count == 0)
                return;

            try
            {
                string[] sTmpDate = lblStartDate.Text.Split('-');
                string[] eTmpDate = lblEndDate.Text.Split('-');

                string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
                string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

                serialPortReport.PortName = StoreInfo.ReceiptPrn;
                serialPortReport.BaudRate = StoreInfo.ReceiptRate;
                serialPortReport.DataBits = 8;
                serialPortReport.Parity = System.IO.Ports.Parity.None;
                serialPortReport.StopBits = System.IO.Ports.StopBits.One;
                serialPortReport.Encoding = System.Text.Encoding.Default;

                if (serialPortReport.IsOpen == false)
                    serialPortReport.Open();

                serialPortReport.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));

                serialPortReport.WriteLine("** 부문별 매출 현황 **\r\n");
                serialPortReport.WriteLine(string.Format("{0, -32}{1, 10}", "조회일자 : " + string.Format("{0}.{1}.{2}~{3}.{4}.{5}",
                sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1], eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));
                serialPortReport.WriteLine(EqualGrid(46, "="));
                serialPortReport.WriteLine(string.Format("{0}{1}{2}", PaddingBySpace("품목", 23, false),
                       PaddingBySpace("수량", 8, false), PaddingBySpace("금액", 20, true)));
                serialPortReport.WriteLine(EqualGrid(46, "="));

                for (int i = 0; i < grpSales.grpName.Count; i++)
                {
                    if (grpSales.grpName[i].Length < 10)
                        grpSales.grpName[i] += "\t";
                    serialPortReport.WriteLine(string.Format("{0, -12}\t{1, 3}\t{2, 15}", grpSales.grpName[i], grpSales.quan[i],
                        string.Format("{0:##,##0}", grpSales.amt[i])));
                }
                serialPortReport.WriteLine(EqualGrid(46, "-"));
                serialPortReport.WriteLine(string.Format("{0, -12}\t{1, 3}\t{2, 15}", "합계\t", grpSales.totCnt, string.Format("{0:##,##0}", Convert.ToDecimal(grpSales.totAmt))));
                serialPortReport.WriteLine("\r\n");
                serialPortReport.WriteLine("**조회시간 : " + grpSales.searchTime.ToString("yyyy.MM.dd HH: mm:ss") + " **\r\n");

                serialPortReport.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReport.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReport.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1B) + "S");
            }
            catch
            {
                serialPortReport.Close();
            }
            finally
            {
                serialPortReport.Close();
            }
        }

        private void SalesCancelPrint()
        {
            if (salesCancel.receNo.Count == 0)
                return;

            try
            {
                string[] sTmpDate = lblStartDate.Text.Split('-');
                string[] eTmpDate = lblEndDate.Text.Split('-');

                string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
                string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

                serialPortReport.PortName = StoreInfo.ReceiptPrn;
                serialPortReport.BaudRate = StoreInfo.ReceiptRate;
                serialPortReport.DataBits = 8;
                serialPortReport.Parity = System.IO.Ports.Parity.None;
                serialPortReport.StopBits = System.IO.Ports.StopBits.One;
                serialPortReport.Encoding = System.Text.Encoding.Default;

                if (serialPortReport.IsOpen == false)
                    serialPortReport.Open();

                serialPortReport.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));

                serialPortReport.WriteLine("** 매출취소 현황 **\r\n");
                serialPortReport.WriteLine(string.Format("{0, -32}{1, 10}", "조회일자 : " + string.Format("{0}.{1}.{2}~{3}.{4}.{5}",
                sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1], eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));
                serialPortReport.WriteLine(EqualGrid(46, "="));
                serialPortReport.WriteLine(string.Format("{0}{1}  {2}{3}", PaddingBySpace("일자", 10, false), PaddingBySpace("영수증번호", 10, false),
                       PaddingBySpace("카드사명", 12, false), PaddingBySpace("금액", 20, true)));
                serialPortReport.WriteLine(EqualGrid(46, "="));

                for (int i = 0; i < salesCancel.receNo.Count; i++)
                {
                    //if (salesCancel.CardName[i].Length < 5)
                    //    salesCancel.CardName[i] = "  " + salesCancel.CardName[i];
                    serialPortReport.WriteLine(string.Format("{0}    {1}    {2}{3, 10}", salesCancel.date[i], salesCancel.receNo[i], salesCancel.cardName[i],
                        salesCancel.amt[i]));
                }

                serialPortReport.WriteLine(EqualGrid(46, "-"));
                serialPortReport.WriteLine(string.Format("{0, -12}\t{1, 3}\t{2, 15}", "합계\t", salesCancel.totCnt, string.Format("{0:##,##0}", Convert.ToDecimal(salesCancel.totAmt))));
                serialPortReport.WriteLine("\r\n");
                serialPortReport.WriteLine("**조회시간 : " + salesCancel.searchTime.ToString("yyyy.MM.dd HH: mm:ss") + " **\r\n");

                serialPortReport.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReport.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReport.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1B) + "S");
            }
            catch (Exception e)
            {
                serialPortReport.Close();
            }
            finally
            {
                serialPortReport.Close();
            }
        }

        private void InpNmSalesPrint()
        {
            if (inpNm.quan.Count == 0)
                return;

            try
            {
                string[] sTmpDate = lblStartDate.Text.Split('-');
                string[] eTmpDate = lblEndDate.Text.Split('-');

                string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
                string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

                serialPortReport.PortName = StoreInfo.ReceiptPrn;
                serialPortReport.BaudRate = StoreInfo.ReceiptRate;
                serialPortReport.DataBits = 8;
                serialPortReport.Parity = System.IO.Ports.Parity.None;
                serialPortReport.StopBits = System.IO.Ports.StopBits.One;
                serialPortReport.Encoding = System.Text.Encoding.Default;

                if (serialPortReport.IsOpen == false)
                    serialPortReport.Open();

                serialPortReport.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));

                serialPortReport.WriteLine("** 매입사별 매출 현황 **\r\n");
                serialPortReport.WriteLine(string.Format("{0, -32}{1, 10}", "조회일자 : " + string.Format("{0}.{1}.{2}~{3}.{4}.{5}",
                sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1], eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));
                serialPortReport.WriteLine(EqualGrid(46, "="));
                serialPortReport.WriteLine(string.Format("{0,-14}{1, 12}{2, 10}", PaddingBySpace("매입사", 20, false),
                           PaddingBySpace("승인건수", 10, false), PaddingBySpace("금액", 20, true)));
                serialPortReport.WriteLine(EqualGrid(46, "="));

                for (int i = 0; i < inpNm.inpName.Count; i++)
                {
                    serialPortReport.WriteLine(string.Format("{0, -20}\t{1, 4}\t{2, 10}", inpNm.inpName[i].Trim(), inpNm.quan[i].ToString(),
                        string.Format("{0:##,##0}", Convert.ToDecimal(inpNm.amt[i]))));
                }
                serialPortReport.WriteLine(EqualGrid(46, "-"));
                serialPortReport.WriteLine(string.Format("{0, -14}\t{1, 12}\t{2, 10}", "합계", inpNm.totCnt, string.Format("{0:##,##0}", Convert.ToDecimal(inpNm.totAmt))));

                serialPortReport.WriteLine("\r\n");
                serialPortReport.WriteLine("**조회시간 : " + inpNm.searchTime.ToString("yyyy.MM.dd HH: mm:ss") + " **\r\n");

                serialPortReport.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReport.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReport.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1B) + "S");
            }
            catch (Exception e)
            {
                serialPortReport.Close();
            }
            finally
            {
                serialPortReport.Close();
            }
        }

        private void ABCReportPrint()
        {
            if (abcReport.itemName.Count == 0)
                return;

            try
            {
                string[] sTmpDate = lblStartDate.Text.Split('-');
                string[] eTmpDate = lblEndDate.Text.Split('-');

                string sDate = sTmpDate[0] + sTmpDate[1] + sTmpDate[2];
                string eDate = eTmpDate[0] + eTmpDate[1] + eTmpDate[2];

                serialPortReport.PortName = StoreInfo.ReceiptPrn;
                serialPortReport.BaudRate = StoreInfo.ReceiptRate;
                serialPortReport.DataBits = 8;
                serialPortReport.Parity = System.IO.Ports.Parity.None;
                serialPortReport.StopBits = System.IO.Ports.StopBits.One;
                serialPortReport.Encoding = System.Text.Encoding.Default;

                if (serialPortReport.IsOpen == false)
                    serialPortReport.Open();

                serialPortReport.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));

                serialPortReport.WriteLine("** ABC 리포트 **\r\n");
                serialPortReport.WriteLine(string.Format("{0, -32}{1, 10}", "조회일자 : " + string.Format("{0}.{1}.{2}~{3}.{4}.{5}",
                sTmpDate[0], sTmpDate[1], sTmpDate[2], eTmpDate[0], eTmpDate[1], eTmpDate[2]), "KIOSK: " + StoreInfo.StoreDesk));
                serialPortReport.WriteLine(EqualGrid(46, "="));
                serialPortReport.WriteLine(string.Format("{0, -3}\t{1}\t\t  {2}\t {3}\t  {4}", "ABC", "품목", "수량", "금액", "%"));

                serialPortReport.WriteLine(EqualGrid(46, "="));

                for (int i = 0; i < abcReport.itemName.Count; i++)
                {
                    serialPortReport.WriteLine(string.Format("{0, -2}{1:25}{2, 3}\t{3, 8}   {4, 3}", abcReport.abc[i],
                        UtilHelper.PadingLeftBySpace(abcReport.itemName[i], 24),
                        abcReport.quan[i], abcReport.amt[i], abcReport.per[i]));
                }

                serialPortReport.WriteLine(EqualGrid(46, "-"));

                serialPortReport.WriteLine("**조회시간 : " + abcReport.searchTime.ToString("yyyy.MM.dd HH:mm:ss") + " **\r\n");

                serialPortReport.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReport.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReport.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReport.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReport.Write(Convert.ToChar(0x1B) + "S");
            }
            catch (Exception e)
            {
                serialPortReport.Close();
            }
            finally
            {
                serialPortReport.Close();
            }
        }

        public void Receive(Form frm, string msg)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void dgvMenuList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvMenuList.Rows.Count)
            {
                int page = dgvMenuList.Rows[e.RowIndex].Cells[0].Value.ToInteger() - 1;
                int no = dgvMenuList.Rows[e.RowIndex].Cells[1].Value.ToInteger() - 1;
                string grpName = dgvMenuList.Rows[e.RowIndex].Cells[2].Value.ToString();
                string proName = dgvMenuList.Rows[e.RowIndex].Cells[3].Value.ToString();
                string soldYn = dgvMenuList.Rows[e.RowIndex].Cells[4].Value.ToString();

                if (soldYn.CompareTo("정상") == 0)
                {
                    FrmMsgBox msgBox = new FrmMsgBox(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location),
                        "품절설정", proName, "품목을 품절처리 하시겠습니까? ");

                    msgBox.TopMost = true;

                    if (msgBox.ShowDialog() == DialogResult.OK)
                    {
                        if (menuListCfgDao.MenuListCfgSoldOutUpdateL("Y", grpName, proName))
                        {
                            if (menuListCfgDao.MenuListCfgSoldOutUpdate("Y", grpName, proName))
                            {
                                msgBox.BackgroundImage = null;
                                GetMenuList();

                                if (FrmMain.menuListCfg is IFormMessage)
                                    (FrmMain.menuListCfg as IFormMessage).Receive(null, "SOLDOUT-" +
                                        string.Format("{0}:{1}:{2}:{3}:{4}", page, no, grpName, proName, "Y"));
                            }
                            
                        }
                    }
                    msgBox = null;
                }
                else
                {
                    FrmMsgBox msgBox = new FrmMsgBox(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location),
                        "품절해제", proName, "품목을 품절해제 하시겠습니까? ");

                    msgBox.TopMost = true;

                    if (msgBox.ShowDialog() == DialogResult.OK)
                    {
                        if (menuListCfgDao.MenuListCfgSoldOutUpdateL("N", grpName, proName))
                        {
                            if (menuListCfgDao.MenuListCfgSoldOutUpdate("N", grpName, proName))
                            {
                                msgBox.BackgroundImage = null;
                                GetMenuList();

                                if (FrmMain.menuListCfg is IFormMessage)
                                    (FrmMain.menuListCfg as IFormMessage).Receive(null, "SOLDCANCEL-" +
                                        string.Format("{0}:{1}:{2}:{3}:{4}", page, no, grpName, proName, "N"));
                            }
                        }
                    }
                    msgBox = null;
                }
            }

        }
    }
}
