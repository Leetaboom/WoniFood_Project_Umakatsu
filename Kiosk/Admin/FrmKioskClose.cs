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
    public partial class FrmKioskClose : Form
    {
        DBControl.CloseKioskDAO closeKioskDao = new DBControl.CloseKioskDAO();
        DBControl.SaleHDAO salehDao = new DBControl.SaleHDAO();

        List<string> salesTitle = new List<string>();
        List<string> etcTitle = new List<string>();
        List<decimal> salesAmt = new List<decimal>();
        List<decimal> etcAmt = new List<decimal>();

        List<string> closeCode = new List<string>();

        SplashKioskClose closeSplash;

        Bitmap backImg;

        string ctrlName = string.Empty;

        public FrmKioskClose()
        {
            InitializeComponent();
        }

        public FrmKioskClose(Bitmap bitmap)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            backImg = bitmap;
            salehDao.BRAND = closeKioskDao.BRAND = StoreInfo.BrnadCode;
            //salehDao.BRAND = closeKioskDao.BRAND = "00003";
            salehDao.STORE = closeKioskDao.STORE = StoreInfo.StoreCode;
            salehDao.DESK = closeKioskDao.DESK = StoreInfo.StoreDesk;
        }

        private void FrmKioskClose_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            CloseTitleAdd();
            CloseValueAdd();
            DgvSalesListInit();
            DgvEtcListInit();
            GetSalesListDgv();
            GetEtcListDgv();

            lblSalesDate.Text = string.Format("{0}-{1}-{2}", StoreInfo.StoreOpen.Substring(0, 4),
                StoreInfo.StoreOpen.Substring(4, 2), StoreInfo.StoreOpen.Substring(6, 2));
            lblKioskNum.Text = StoreInfo.StoreDesk;
            lblEndAmt.Text = string.Format(@"\ {0:#,##0}", salesAmt[7]);
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

                case "picBoxClose":
                    bitmap = UtilHelper.ChangeOpacity(picBoxClose.BackgroundImage, 1f);
                    picBoxClose.BackgroundImage.Dispose();
                    picBoxClose.BackgroundImage = bitmap;
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

                case "picBoxClose":
                    picBoxClose.BackgroundImage.Dispose();
                    picBoxClose.BackgroundImage = Properties.Resources.btn_continue;
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
                    backImg = null;
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case "picBoxClose":
                    this.dbpMain.Visible = false;

                    FrmKisokCloseOK closeOk = new FrmKisokCloseOK(backImg);
                    closeOk.TopMost = true;

                    if (closeOk.ShowDialog() == DialogResult.OK)
                    {
                        closeSplash = new SplashKioskClose(this, backImg);
                        closeSplash.Open();

                        KioskCloseSave();
                        KioskCloseInfoPrint();
                        closeSplash.Close();

                        closeOk = new FrmKisokCloseOK(backImg, "마감완료 안내", "마감이 완료 되었습니다.", "키오스크 전원을 끄시겠습니까?");
                        closeOk.TopMost = true;

                        if (closeOk.ShowDialog() == DialogResult.OK)
                        {
                            System.Diagnostics.Process.Start("ShutDown", "-s");
                        }
                        else
                        {
                            Application.Exit();
                        }

                    }
                    else
                    {
                        this.dbpMain.Visible = true;
                    }
                    break;
            }
        }

        private void DgvSalesListInit()
        {
            foreach (DataGridViewColumn column in dgvSalesList.Columns)
            {
                Font font = new Font("나눔스퀘어", 24, FontStyle.Bold);
                column.DefaultCellStyle.BackColor = Color.White;
                column.DefaultCellStyle.Font = font;

                switch (column.Index)
                {
                    case 0:
                    case 1:
                        column.HeaderCell.Style.Font = font;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }
            }
        }

        private void DgvEtcListInit()
        {
            foreach (DataGridViewColumn column in dgvEtcList.Columns)
            {
                Font font = new Font("나눔스퀘어", 20, FontStyle.Bold);
                column.DefaultCellStyle.BackColor = Color.White;
                column.DefaultCellStyle.Font = font;

                switch (column.Index)
                {
                    case 0:
                    case 1:
                        column.HeaderCell.Style.Font = font;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }
            }
        }

        private void GetSalesListDgv()
        {
            for (int i = 0; i < salesTitle.Count; i++)
            {
                string[] rows = { salesTitle[i], string.Format("{0:#,##0}", salesAmt[i]) };

                dgvSalesList.Rows.Add(rows);
                dgvSalesList.Rows[i].Height = 55;
            }
        }

        private void GetEtcListDgv()
        {
            for (int i = 0; i < etcTitle.Count; i++)
            {
                string[] rows = { etcTitle[i], string.Format("{0:#,##0}", etcAmt[i]) };

                dgvEtcList.Rows.Add(rows);
                dgvEtcList.Rows[i].Height = 41;

                if (i % 2 == 0)
                    dgvEtcList.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
            }
        }

        private void CloseTitleAdd()
        {
            salesTitle.Add("출 고 액 :");
            salesTitle.Add("반 품 액 :");
            salesTitle.Add("매 출 액 :");
            salesTitle.Add("신용카드 :");
            salesTitle.Add("삼성페이 :");
            salesTitle.Add("총취소건 :");
            salesTitle.Add("총취소액 :");
            salesTitle.Add("최종영업 :");

            etcTitle.Add("신용카드승인건 :");
            etcTitle.Add("신용카드승인액 :");
            etcTitle.Add("신용카드취소건 :");
            etcTitle.Add("신용카드취소액 :");
            etcTitle.Add("삼성페이승인건 :");
            etcTitle.Add("삼성페이승인액 :");
            etcTitle.Add("삼성페이취소건 :");
            etcTitle.Add("삼성페이취소액 :");

            for (int i = 0; i < salesTitle.Count; i++)
            {
                salesAmt.Add(0);
                closeCode.Add(string.Format("E{0:D2}", i + 1));
            }

            for (int i = 0; i < etcTitle.Count; i++)
            {
                etcAmt.Add(0);
                closeCode.Add(string.Format("E{0:D2}", closeCode.Count + 1));
            }
        }

        private void CloseValueAdd()
        {
            salesAmt[0] = salehDao.ChulGoaek(StoreInfo.StoreOpen);
            salesAmt[1] = salehDao.SalesReturn(StoreInfo.StoreOpen);
            salesAmt[2] = salesAmt[0] - salesAmt[1];
            salesAmt[3] = etcAmt[1] = salehDao.SalesCard(StoreInfo.StoreOpen);
            salesAmt[4] = etcAmt[5] = salehDao.SalesSPay(StoreInfo.StoreOpen);
            salesAmt[5] = salehDao.SalesTotalCancel(StoreInfo.StoreOpen);
            salesAmt[6] = salesAmt[1];
            salesAmt[7] = salesAmt[2];

            etcAmt[0] = salehDao.SalesCardAppNum(StoreInfo.StoreOpen);
            etcAmt[2] = salehDao.SalesCardCancelNum(StoreInfo.StoreOpen);
            etcAmt[3] = salehDao.SalesCardCancelAmt(StoreInfo.StoreOpen);
            etcAmt[4] = salehDao.SalesSpayAppNum(StoreInfo.StoreOpen);
            etcAmt[6] = salehDao.SalesSpayCancelNum(StoreInfo.StoreOpen);
            etcAmt[7] = salehDao.SalesSpayCancelAmt(StoreInfo.StoreOpen);
        }

        private void KioskCloseSave()
        {
            closeKioskDao.DESK = StoreInfo.StoreDesk;

            for (int i = 0; i < salesAmt.Count; i++)
            {
                closeKioskDao.IDXCD = closeCode[i];
                closeKioskDao.IDXNM = salesTitle[i];
                closeKioskDao.CNT = 0;
                closeKioskDao.AMT = salesAmt[i];
                closeKioskDao.DATE = StoreInfo.StoreOpen;

                if (closeKioskDao.ColoseKioskSave())
                {
                    if (closeKioskDao.ColoseKioskSaveL())
                    {

                    }
                }
            }

            for (int i = 0; i < etcAmt.Count; i++)
            {
                closeKioskDao.IDXCD = closeCode[salesAmt.Count + i];
                closeKioskDao.IDXNM = etcTitle[i];
                closeKioskDao.CNT = 0;
                closeKioskDao.AMT = etcAmt[i];

                if (closeKioskDao.ColoseKioskSave())
                {
                    if (closeKioskDao.ColoseKioskSaveL())
                    {

                    }
                }

            }

            StringBuilder sql = new StringBuilder();

            sql.Append(@"UPDATE CF_OPENKIOSK SET OK_STOP = GETDATE(), OK_ENDPAY = ");
            sql.Append(salesAmt[7]);
            sql.Append(", OK_MEMO = '마감' WHERE OK_BRAND = '");
            sql.Append(StoreInfo.BrnadCode);
            sql.Append("' AND OK_STORE = '");
            sql.Append(StoreInfo.StoreCode);
            sql.Append("' AND OK_DESK = '");
            sql.Append(StoreInfo.StoreDesk);
            sql.Append("' AND OK_MEMO = ''");

            if (DBControl.DBConnection.DoSqlCommand(sql.ToString()))
            {
                if (DBControl.DBConnectionL.DoSqlCommand(sql.ToString()))
                {

                }
            }
        }

        private void KioskCloseInfoPrint()
        {
            try
            {
                serialPortClose.PortName = StoreInfo.ReceiptPrn;
                serialPortClose.BaudRate = StoreInfo.ReceiptRate;
                serialPortClose.DataBits = 8;
                serialPortClose.Parity = System.IO.Ports.Parity.None;
                serialPortClose.StopBits = System.IO.Ports.StopBits.One;
                serialPortClose.Encoding = System.Text.Encoding.Default;

                string strDate = string.Format("{0:0000}년 {1:00}월 {2:00}일 ", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                string strDate1 = string.Format(" {0:00}시 {1:00}분", DateTime.Now.Hour, DateTime.Now.Minute);

                if (serialPortClose.IsOpen == false)
                    serialPortClose.Open();

                serialPortClose.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1)); // 0:왼쪽 정렬, 1:중앙정렬 2:오른쪽정렬
                serialPortClose.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                serialPortClose.WriteLine("영업마감현황");
                serialPortClose.WriteLine("");
                serialPortClose.WriteLine("마감출력일자:" + strDate + strDate1);
                serialPortClose.WriteLine(UtilHelper.HyphepMake("=", 46));
                serialPortClose.WriteLine(string.Format("출    고    액{0,32}", string.Format("{0:##,##0}", salesAmt[0])));
                serialPortClose.WriteLine(string.Format("반    품    액{0,32}", string.Format("{0:##,##0}", salesAmt[1])));
                serialPortClose.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortClose.WriteLine(string.Format("매    출    액{0,32}", string.Format("{0:##,##0}", salesAmt[2])));
                serialPortClose.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortClose.WriteLine(string.Format("신  용  카  드{0,32}", string.Format("{0:##,##0}", salesAmt[3])));
                serialPortClose.WriteLine(string.Format("삼  성  페  이{0,32}", string.Format("{0:##,##0}", salesAmt[4])));
                serialPortClose.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortClose.WriteLine(string.Format("총  취  소  건{0,32}", string.Format("{0:##,##0}", salesAmt[5])));
                serialPortClose.WriteLine(string.Format("총  취  소  액{0,32}", string.Format("{0:##,##0}", salesAmt[6])));
                serialPortClose.WriteLine(UtilHelper.HyphepMake("=", 46));
                serialPortClose.WriteLine(string.Format("최  종  영  업{0,32}", string.Format("{0:##,##0}", salesAmt[7])));
                serialPortClose.WriteLine(UtilHelper.HyphepMake("=", 46));
                serialPortClose.WriteLine(string.Format("신용카드승인건{0,32}", string.Format("{0:##,##0}", etcAmt[0])));
                serialPortClose.WriteLine(string.Format("신용카드승인액{0,32}", string.Format("{0:##,##0}", etcAmt[1])));
                serialPortClose.WriteLine(string.Format("신용카드취소건{0,32}", string.Format("{0:##,##0}", etcAmt[2])));
                serialPortClose.WriteLine(string.Format("신용카드취소액{0,32}", string.Format("{0:##,##0}", etcAmt[3])));
                serialPortClose.WriteLine(string.Format("삼성페이승인건{0,32}", string.Format("{0:##,##0}", etcAmt[4])));
                serialPortClose.WriteLine(string.Format("삼성페이승인액{0,32}", string.Format("{0:##,##0}", etcAmt[5])));
                serialPortClose.WriteLine(string.Format("삼성페이취소건{0,32}", string.Format("{0:##,##0}", etcAmt[6])));
                serialPortClose.WriteLine(string.Format("삼성페이취소액{0,32}", string.Format("{0:##,##0}", etcAmt[7])));
                serialPortClose.WriteLine(UtilHelper.HyphepMake("=", 46));

                serialPortClose.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortClose.WriteLine("");
                serialPortClose.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortClose.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortClose.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortClose.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortClose.Write(Convert.ToChar(0x1B) + "S");
            }
            catch
            {
                serialPortClose.Close();
            }
            finally
            {
                serialPortClose.Close();
            }
        }
    }
}
