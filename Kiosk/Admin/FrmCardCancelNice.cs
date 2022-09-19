using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Kiosk.DBControl;

namespace Kiosk
{
    public partial class FrmCardCancelNice : Form
    {
        string ctrlName = string.Empty;
        string strDate = string.Empty;
        string appDate = string.Empty;
        string appNo = string.Empty;
        string appAmt = string.Empty;
        string appRecNo = string.Empty;
        string appMonth = "00";
        string cardGubn = string.Empty;


        public FrmCardCancelNice()
        {
            InitializeComponent();
        }

        public FrmCardCancelNice(Bitmap bitmap)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
        }

        private void FrmCardCancel_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            lblStore.Text = StoreInfo.StoreName;
            lblKNum.Text = StoreInfo.StoreDesk;
            lblSalesDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2));
            DBControl.CardAppListDAO.sSearchDate = string.Empty;
            DgvSalesInit();
            GetCardAppList();
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;

            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "picBoxPrevPage":
                    bitmap = UtilHelper.ChangeOpacity(picBoxPrevPage.BackgroundImage, 1f);
                    picBoxPrevPage.BackgroundImage.Dispose();
                    picBoxPrevPage.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxNextPage":
                    bitmap = UtilHelper.ChangeOpacity(picBoxNextPage.BackgroundImage, 1f);
                    picBoxNextPage.BackgroundImage.Dispose();
                    picBoxNextPage.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxContinue":
                    bitmap = UtilHelper.ChangeOpacity(picBoxContinue.BackgroundImage, 1f);
                    picBoxContinue.BackgroundImage.Dispose();
                    picBoxContinue.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxCancel":
                    bitmap = UtilHelper.ChangeOpacity(picBoxCancel.BackgroundImage, 1f);
                    picBoxCancel.BackgroundImage.Dispose();
                    picBoxCancel.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "panelSearch":
                    bitmap = UtilHelper.ChangeOpacity(panelSearch.BackgroundImage, 1f);
                    panelSearch.BackgroundImage.Dispose();
                    panelSearch.BackgroundImage = bitmap;
                    bitmap = null;
                    break;
            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (ctrlName)
            {
                case "picBoxPrevPage":
                    picBoxPrevPage.BackgroundImage.Dispose();
                    picBoxPrevPage.BackgroundImage = Properties.Resources.btn_page_back;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxNextPage":
                    picBoxNextPage.BackgroundImage.Dispose();
                    picBoxNextPage.BackgroundImage = Properties.Resources.btn_page_next;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxContinue":
                    picBoxContinue.BackgroundImage.Dispose();
                    picBoxContinue.BackgroundImage = Properties.Resources.btn_continue;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "picBoxCancel":
                    picBoxCancel.BackgroundImage.Dispose();
                    picBoxCancel.BackgroundImage = Properties.Resources.btn_back;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "panelSearch":
                    panelSearch.BackgroundImage.Dispose();
                    panelSearch.BackgroundImage = Properties.Resources.btn_lookup;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;
            }
        }

        private void lblSalesDate_Click(object sender, EventArgs e)
        {
            lblSalesDate.BackColor = Color.Orange;
            UtilHelper.Delay(50);

            Click_Proc();
        }

        private void dgvSalesCancel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSalesCancel.Rows.Count)
            {
                string[] tmpDate = dgvSalesCancel.Rows[e.RowIndex].Cells[0].Value.ToString().Split('-');
                appDate = tmpDate[0] + tmpDate[1] + tmpDate[2];
                appRecNo = dgvSalesCancel.Rows[e.RowIndex].Cells[2].Value.ToString();
                appNo = dgvSalesCancel.Rows[e.RowIndex].Cells[3].Value.ToString();
                appAmt = Regex.Replace(dgvSalesCancel.Rows[e.RowIndex].Cells[4].Value.ToString(), @"\D", "");
                appMonth = dgvSalesCancel.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
        }

        private void MouseUp_Proc()
        {
            switch (ctrlName)
            {
                case "picBoxPrevPage":
                    if (DBControl.CardAppListDAO.CardAppAllListPage > 1)
                    {
                        DBControl.CardAppListDAO.CardAppAllListPage--;
                        GetCardAppList();
                    }
                    break;

                case "picBoxNextPage":
                    if (DBControl.CardAppListDAO.CardAppAllListPage < DBControl.CardAppListDAO.CardAppAllListEndPage)
                    {
                        DBControl.CardAppListDAO.CardAppAllListPage++;
                        GetCardAppList();
                    }
                    break;

                case "picBoxContinue":
                    if (appDate.CompareTo("") != 0)
                    {
                        if (CardCancelCheck())
                        {
                            dbpMain.Visible = false;
                            FrmErrorBox cancelErrBox = new FrmErrorBox("취소실패", "이미 취소된 거래입니다.");

                            cancelErrBox.TopMost = true;
                            if (cancelErrBox.ShowDialog() == DialogResult.OK)
                            {
                                dbpMain.Visible = true;
                            }
                        }
                        else if (SamsungPayCheck())
                        {
                            dbpMain.Visible = false;

                            FrmSamPayCancelInfoNice sPayCancel = new FrmSamPayCancelInfoNice(appDate, appRecNo, appNo, appMonth, appAmt, strDate);
                            sPayCancel.TopMost = true;

                            if (sPayCancel.ShowDialog() == DialogResult.OK)
                                DialogResult = DialogResult.OK;
                            else
                                dbpMain.Visible = true;
                        }  
                        else
                        {
                            dbpMain.Visible = false;

                            FrmCardCancelInfoNice cardCancel = new FrmCardCancelInfoNice(appDate, appRecNo, appNo, appMonth, appAmt, strDate);
                            cardCancel.TopMost = true;

                            if (cardCancel.ShowDialog() == DialogResult.OK)
                                DialogResult = DialogResult.OK;
                            else
                                dbpMain.Visible = true;
                        }

                    }
                    break;

                case "picBoxCancel":
                    DialogResult = DialogResult.Cancel;
                    break;

                case "panelSearch":
                    CardAppListDAO.sSearchDate = strDate;
                    GetCardAppList();
                    break;
            }
        }

        private void Click_Proc()
        {
            FrmCalendar salesDate = new FrmCalendar(UtilHelper.ScreenCapture(
                this.Width, this.Height, this.Location), lblSalesDate.Text);

            salesDate.TopMost = true;

            if (salesDate.ShowDialog() == DialogResult.OK)
            {
                lblSalesDate.BackColor = Color.White;
                lblSalesDate.Text = salesDate.monthCalendarAdv1.SelectedDate.ToString("yyyy-MM-dd");

                strDate = salesDate.monthCalendarAdv1.SelectedDate.ToString("yyyyMMdd");
            }
            else
            {
                lblSalesDate.BackColor = Color.White;
            }
        }

        private void DgvSalesInit()
        {
            DataGridViewTextBoxCell txtBoxCell = new DataGridViewTextBoxCell();

            foreach (DataGridViewColumn column in dgvSalesCancel.Columns)
            {
                Font font = new Font("나눔스퀘어", 14, FontStyle.Bold);

                column.DefaultCellStyle.Font = font;

                switch (column.Index)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 5:
                    case 6:
                        column.HeaderCell.Style.Font = font;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case 4:
                        column.HeaderCell.Style.Font = font;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                }
            }
        }

        private void GetCardAppList()
        {
            using (DataTable dt = DBControl.CardAppListDAO.CardAppList())
            {
                if (dt.Rows.Count != 0)
                {
                    dgvSalesCancel.Rows.Clear();
                    lblPage.Text = string.Format("{0} / {1}", DBControl.CardAppListDAO.CardAppAllListPage,
                        DBControl.CardAppListDAO.CardAppAllListEndPage);

                    int i = 0;
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            string appGubn = dtr["VD_GUBN"].ToString();
                            string appDate = dtr["승인일자"].ToString();
                            string appTime = dtr["승인시간"].ToString();
                            string appReNo = dtr["영수증번호"].ToString();
                            string appNo = dtr["승인번호"].ToString().Trim();
                            string cardNM = dtr["카드사명"].ToString().Trim();
                            string month = dtr["할부개월"].ToString().Trim();
                            decimal appAmt = Convert.ToDecimal(dtr["승인금액"].ToString());
                            string cardGb = dtr["VD_REMARK"].ToString();
                            string strFlag = "정상승인";


                            if (appGubn.CompareTo("0430") == 0 || appGubn.CompareTo("0410") == 0)
                                strFlag = "승인취소";

                            string[] rows = { string.Format("{0}-{1}-{2}", appDate.Substring(0, 2), appDate.Substring(2, 2), appDate.Substring(4, 2)),
                            string.Format("{0}:{1}:{2}", appTime.Substring(0, 2), appTime.Substring(2, 2), appTime.Substring(4, 2)),
                            appReNo, appNo, string.Format("{0:##,##0}", appAmt), cardNM, strFlag, month };

                            dgvSalesCancel.Rows.Add(rows);


                            

                            if (appGubn.CompareTo("0430") == 0 || appGubn.CompareTo("0410") == 0)
                                dgvSalesCancel.Rows[i].DefaultCellStyle.ForeColor = Color.Red;

                            if (cardGb.CompareTo("S") == 0 && appGubn.CompareTo("0430") != 0)
                                dgvSalesCancel.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;

                            dgvSalesCancel.Rows[i].Height = 77;
                            i++;


                        }
                        dgvSalesCancel.ClearSelection();
                    }
                }
            }
        }

        private bool CardCancelCheck()
        {
            bool bCancel = false;

            using (DataTable dt = DBControl.CardAppListDAO.GetCardCancel(appDate, appRecNo, appNo, appAmt))
            {
                if (dt.Rows.Count != 0)
                    bCancel = true;

                return bCancel;
            }
        }

        private bool SamsungPayCheck()
        {
            bool bSamsungPay = false;

            using (DataTable dt = DBControl.CardAppListDAO.GetSamsungPay(appDate, appRecNo, appNo, appAmt))
            {
                if (dt.Rows.Count != 0)
                    return bSamsungPay = true;

                return bSamsungPay;
            }
        }
    }
}
