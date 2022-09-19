using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.IO;
using System.Drawing.Imaging;

namespace Kiosk
{
    public partial class FrmCardPayMentNice : Form
    {
        /* [DllImport(@"C:\NICEVCAT\NVCAT.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int NICEVCAT(string send_data, byte[] recv_data);

        [DllImport(@"C:\NICEVCAT\NVCAT.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int REQ_STOP();

        [DllImport(@"C:\NICEVCAT\NVCAT.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int RESTART();

        [DllImport(@"C:\NICEVCAT\NVCAT.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int READER_RESET(string time);

        FrmErrorBox errBox = null;

        private Encoding Encod = Encoding.GetEncoding("ks_c_5601-1987");

        private SplashPayMentProc splashPayMentProc;
        private SplashPayMentComple splashPayMentComple;

        private DBControl.SaleDDAO saledDao = new DBControl.SaleDDAO();
        private DBControl.SaleHDAO salehDao = new DBControl.SaleHDAO();
        private DBControl.VanDataDAO vanDataDao = new DBControl.VanDataDAO();

        string month;
        //string totalAmt;
        string receNo = string.Empty;
        string turnNum = string.Empty;
        string logMsg = string.Empty;
        string errMsg = string.Empty;
        string cardMsg = string.Empty;
        string prnSpace = string.Empty;
        string prnSpace1 = string.Empty;
        decimal dTotalAmt;

        byte[] bRecvData = new byte[1024];
        char FS = Convert.ToChar(0x1C);

        List<string> prnRemark = new List<string>();
        List<decimal> danga = new List<decimal>();

        Bitmap bgBitmap;

        public FrmCardPayMentNice()
        {
            InitializeComponent();
        }

        public FrmCardPayMentNice(Bitmap bitmap, List<decimal> danga, string totalAmt, string month, string msg)
        {
            InitializeComponent();
            bgBitmap = bitmap;
            this.BackgroundImage = bitmap;
            this.danga = danga;
            this.dTotalAmt = totalAmt.ToDecimal();
            this.month = month;

            saledDao.BRAND = salehDao.BRAND = vanDataDao.BRAND = StoreInfo.BrnadCode;
            saledDao.STORE = salehDao.STORE = vanDataDao.STORE = StoreInfo.StoreCode;
            saledDao.DESK = salehDao.DESK = vanDataDao.DESK = StoreInfo.StoreDesk;
            saledDao.DATE = salehDao.DATE = vanDataDao.DATE = StoreInfo.StoreOpen;
        }

        private void FrmCardPayMentNice_Load(object sender, EventArgs e)
        {
            woniPanel1.Left = (this.ClientSize.Width - woniPanel1.Width) / 2;
            woniPanel1.Top = (this.ClientSize.Height - woniPanel1.Height) / 2;

            axShockwaveFlash1.Movie = System.IO.Directory.GetCurrentDirectory() + @"\System\swf\card_in.swf";
            ThreadPool.QueueUserWorkItem(CardApp, true);
        }

        private void picBoxPrev_MouseDown(object sender, MouseEventArgs e)
        {
            UtilHelper.spWav.Play();

            Bitmap bitmap = UtilHelper.ChangeOpacity(picBoxPrev.BackgroundImage, 1f);
            picBoxPrev.BackgroundImage.Dispose();
            picBoxPrev.BackgroundImage = bitmap;
            bitmap = null;
        }

        private void picBoxPrev_MouseUp(object sender, MouseEventArgs e)
        {
            picBoxPrev.BackgroundImage.Dispose();
            picBoxPrev.BackgroundImage = Properties.Resources.btn_back;
            UtilHelper.Delay(50);

            REQ_STOP();

            this.DialogResult = DialogResult.Cancel;
        }

        private void CardApp(object sender)
        {
            UtilHelper.cardInfo.URL = @"System\wav\card.mp3";
            UtilHelper.cardInfo.controls.play();

            int rtn;

            try
            {
                StringBuilder send_data = new StringBuilder();
                send_data.Clear();
                send_data.Append("0200");
                send_data.Append(FS);
                send_data.Append("10");
                send_data.Append(FS);
                send_data.Append("C");
                send_data.Append(FS);
                send_data.Append(dTotalAmt);
                send_data.Append(FS);
                send_data.Append("0");
                send_data.Append(FS);
                send_data.Append("0");
                send_data.Append(FS);
                send_data.Append(month);
                send_data.Append(FS);
                send_data.Append("");
                send_data.Append(FS);
                send_data.Append("");
                send_data.Append(FS);
                send_data.Append("");
                send_data.Append(FS);
                send_data.Append(FS);
                send_data.Append(FS);
                send_data.Append(FS);
                send_data.Append("0");
                send_data.Append(FS);

                UtilHelper.Delay(50);

                LogMenager.LogWriter(UtilHelper.Root + @"\Log\send\payMentSend", send_data.ToString());

                rtn = NICEVCAT(send_data.ToString(), bRecvData);

                if (rtn == 1)
                {
                    string temp = Encoding.Default.GetString(bRecvData);
                    string recvMsg = string.Empty;
                    string valid = string.Empty;
                    byte[] buf = Encod.GetBytes(temp);

                    valid = Encod.GetString(buf, 8, 4);
                    recvMsg = Encod.GetString(buf, 0, 1024);
                    cardMsg = Encod.GetString(buf, 166, 112);

                    LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + temp);

                    if (valid.CompareTo("0000") == 0)
                    {
                        
                        this.Invoke(new MethodInvoker(
                            delegate ()
                            {
                                this.Visible = false;

                                splashPayMentProc = new SplashPayMentProc(UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), 0);
                                splashPayMentProc.Open();

                                //로그기록
                                if (VanDataInsert(bRecvData))
                                {
                                    if (SaleDInsert())
                                    {
                                        if (SaleHInsert())
                                        {
                                            splashPayMentProc.Close();
                                            OrderListPrintKitchen();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {

                                }
                            }));
                    }
                    else
                    {
                        //카드결제 실패
                        errBox = new FrmErrorBox("결제실패", cardMsg.Trim('\0'));

                        if (errBox.ShowDialog() == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.Cancel;
                        }
                    }
                }
                else if (rtn == -20)
                {
                    errBox = new FrmErrorBox("결제시간초과", "올바른 카드가 아니거나\r\n카드가 삽입되지 않았습니다.");

                    if (errBox.ShowDialog() == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
            }
            catch (Exception e)
            {
                errBox = new FrmErrorBox("네크워크 오류", "발행된 영수증을 가지고\r\n\r\n 카운터에 문의 바랍니다.");

                if (errBox.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            finally
            {
                REQ_STOP();
            }
        }
        */

        /*
        private bool SaleDInsert()
        {
            saledDao.TIME = vanDataDao.APPTIME;

            for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
            {
                OrderListInfo.ItemOptAmt1[i] = OrderListInfo.ItemOptAmt1[i] * OrderListInfo.ItemQuan[i];
                OrderListInfo.ItemOptAmt2[i] = OrderListInfo.ItemOptAmt2[i] * OrderListInfo.ItemQuan[i];

                saledDao.RECENO = receNo;
                saledDao.GNAM = OrderListInfo.ItemGNam[i];
                saledDao.PNAM = OrderListInfo.ItemName[i]; //+ " " + OrderListInfo.ItemOpNm[i];
                saledDao.CONT = OrderListInfo.ItemQuan[i];
                saledDao.UNIT = danga[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i];
                saledDao.PAMT = saledDao.PAMT = OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i];
                saledDao.OPNM = saledDao.OPNM = OrderListInfo.ItemOpNm[i];
                saledDao.TURN = turnNum;
                saledDao.TYPE = "C";

                if (StoreInfo.IsTakeOut)
                    saledDao.REMARK = "포장";
                else
                    saledDao.REMARK = "매장식사";

                saledDao.GUBN = "정상";

                if (saledDao.SaleDSave())
                {
                    if (saledDao.SaleDSaveL())
                    {

                        if (OrderListInfo.Memo1[i].Trim().Length != 0 && OrderListInfo.Memo2[i].Trim().Length != 0)
                        {
                            saledDao.PNAM = OrderListInfo.Memo1[i].Trim();
                            saledDao.CONT = OrderListInfo.ItemQuan[i];

                            if (OrderListInfo.ItemOptAmt1[i] != 0)
                                saledDao.UNIT = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                            else
                                saledDao.UNIT = OrderListInfo.ItemOptAmt1[i];

                            saledDao.PAMT = OrderListInfo.ItemOptAmt1[i];

                            if (StoreInfo.IsTakeOut)
                                saledDao.REMARK = "포장";
                            else
                                saledDao.REMARK = "매장식사";

                            saledDao.GUBN = "정상";

                            if (saledDao.SaleDSave())
                            {
                                if (saledDao.SaleDSaveL())
                                {
                                    saledDao.PNAM = OrderListInfo.Memo2[i].Trim();
                                    saledDao.CONT = OrderListInfo.ItemQuan[i];

                                    if (OrderListInfo.ItemOptAmt2[i] != 0)
                                        saledDao.UNIT = OrderListInfo.ItemOptAmt2[i] / OrderListInfo.ItemQuan[i];
                                    else
                                        saledDao.UNIT = OrderListInfo.ItemOptAmt2[i];

                                    saledDao.PAMT = OrderListInfo.ItemOptAmt2[i];
                                    saledDao.OPNM = OrderListInfo.ItemOpNm[i];

                                    if (StoreInfo.IsTakeOut)
                                        saledDao.REMARK = "포장";
                                    else
                                        saledDao.REMARK = "매장식사";

                                    saledDao.GUBN = "정상";

                                    if (saledDao.SaleDSave())
                                    {
                                        if (saledDao.SaleDSaveL())
                                        {

                                        }
                                    }
                                }
                            }
                        }
                        else if (OrderListInfo.Memo1[i].Trim().Length == 0 && OrderListInfo.Memo2[i].Trim().Length != 0)
                        {
                            saledDao.PNAM = OrderListInfo.Memo2[i].Trim();
                            saledDao.CONT = OrderListInfo.ItemQuan[i];

                            if (OrderListInfo.ItemOptAmt2[i] != 0)
                                saledDao.UNIT = OrderListInfo.ItemOptAmt2[i] / OrderListInfo.ItemQuan[i];
                            else
                                saledDao.UNIT = OrderListInfo.ItemOptAmt2[i];

                            saledDao.PAMT = OrderListInfo.ItemOptAmt2[i];
                            saledDao.OPNM = OrderListInfo.ItemOpNm[i];

                            if (StoreInfo.IsTakeOut)
                                saledDao.REMARK = saledDao.REMARK = "포장";
                            else
                                saledDao.REMARK = saledDao.REMARK = "매장식사";

                            saledDao.GUBN = "정상";

                            if (saledDao.SaleDSave())
                            {
                                if (saledDao.SaleDSaveL())
                                {

                                }
                            }
                        }
                        else if (OrderListInfo.Memo1[i].Trim().Length != 0 && OrderListInfo.Memo2[i].Trim().Length == 0)
                        {
                            saledDao.PNAM = OrderListInfo.Memo1[i].Trim();
                            saledDao.CONT = OrderListInfo.ItemQuan[i];

                            if (OrderListInfo.ItemOptAmt1[i] != 0)
                                saledDao.UNIT = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                            else
                                saledDao.UNIT = OrderListInfo.ItemOptAmt1[i];

                            saledDao.PAMT = OrderListInfo.ItemOptAmt1[i];
                            saledDao.OPNM = OrderListInfo.ItemOpNm[i];

                            if (StoreInfo.IsTakeOut)
                                saledDao.REMARK = "포장";
                            else
                                saledDao.REMARK = "매장식사";

                            saledDao.GUBN = "정상";

                            if (saledDao.SaleDSave())
                            {
                                if (saledDao.SaleDSaveL())
                                {

                                }
                            }
                        }
                    }

                }
                else
                {

                }
            }

            return true;
        }

       
        private bool SaleHInsert()
        {
            salehDao.TIME = vanDataDao.APPTIME;
            salehDao.RECENO = receNo;
            salehDao.PAMT = dTotalAmt;
            salehDao.CARD = dTotalAmt;
            salehDao.SPAY = 0;
            salehDao.TYPE = "C";
            salehDao.REMARK = "";
            salehDao.TURN = turnNum;

            if (StoreInfo.IsTakeOut)
                salehDao.MEMO = "포장";
            else
                salehDao.MEMO = "매장식사";

            salehDao.GUBN = "정상";

            if (salehDao.SaleHSave())
            {
                if (salehDao.SaleHSaveL())
                    return true;
            }
            else
                return false;

            return false;
        }

        private bool VanDataInsert(byte[] buf)
        {
            try
            {
                byte[] bRecvData = buf;

                saledDao.SalesNumMaker();
                saledDao.TurnIndexMaker();

                receNo = saledDao.GetSalesNum();
                turnNum = saledDao.GetTurnNum();

                vanDataDao.VANGB = StoreInfo.VanSelect;
                vanDataDao.DATE = StoreInfo.StoreOpen;
                vanDataDao.MOMTH = string.Format("{0:D2}", month.ToInteger());
                vanDataDao.RECENO = receNo;
                vanDataDao.APPAMT = dTotalAmt;
                vanDataDao.TAX = 0;
                vanDataDao.BONG = 0;
                vanDataDao.VALID = Encod.GetString(bRecvData, 8, 4);
                vanDataDao.GUBN = Encod.GetString(bRecvData, 0, 4);
                vanDataDao.TYPE = Encod.GetString(bRecvData, 5, 2);
                vanDataDao.APPDATE = Encod.GetString(bRecvData, 68, 6);
                vanDataDao.APPTIME = Encod.GetString(bRecvData, 74, 6);
                vanDataDao.VANKEY = "";
                vanDataDao.APPNO = Encod.GetString(bRecvData, 55, 12);
                vanDataDao.ORDCD = Encod.GetString(bRecvData, 81, 2);
                vanDataDao.ORDNM = Encod.GetString(bRecvData, 84, 20);
                vanDataDao.INPCD = Encod.GetString(bRecvData, 105, 2);
                vanDataDao.INPNM = Encod.GetString(bRecvData, 108, 20);
                vanDataDao.STOCD = Encod.GetString(bRecvData, 129, 15);
                vanDataDao.CDTYPE = Encod.GetString(bRecvData, 296, 1);
                vanDataDao.CARDNO = Encod.GetString(bRecvData, 279, 12);
                vanDataDao.MSG = Encod.GetString(bRecvData, 166, 112);
                vanDataDao.CATID = Encod.GetString(bRecvData, 145, 10);
                vanDataDao.REMARK = "C";

                if (vanDataDao.VanDataSave())
                    return vanDataDao.VanDataSaveL();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                    return false;
            }
        }
        */

        [DllImport(@"C:\NICEVCAT\NVCAT.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern int NICEVCAT(string send_data, byte[] recv_data);

        [DllImport(@"C:\NICEVCAT\NVCAT.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int REQ_STOP();

        [DllImport(@"C:\NICEVCAT\NVCAT.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int RESTART();

        [DllImport(@"C:\NICEVCAT\NVCAT.dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int READER_RESET(string time);

        FrmErrorBox errBox = null;

        private Encoding Encod = Encoding.GetEncoding("ks_c_5601-1987");

        private SplashPayMentProc splashPayMentProc;
        private SplashPayMentComple splashPayMentComple;

        private DBControl.SaleDDAO saledDao = new DBControl.SaleDDAO();
        private DBControl.SaleHDAO salehDao = new DBControl.SaleHDAO();
        private DBControl.VanDataDAO vanDataDao = new DBControl.VanDataDAO();

        string month;
        //string totalAmt;
        string receNo = string.Empty;
        string turnNum = "001";
        string logMsg = string.Empty;
        string errMsg = string.Empty;
        string cardMsg = string.Empty;
        string prnSpace = string.Empty;
        string prnSpace1 = string.Empty;
        decimal dTotalAmt;

        byte[] bRecvData = new byte[1024];
        char FS = Convert.ToChar(0x1C);

        List<string> prnRemark = new List<string>();
        List<decimal> danga = new List<decimal>();

        Bitmap bgBitmap;

        public FrmCardPayMentNice()
        {
            InitializeComponent();
        }

        public FrmCardPayMentNice(Bitmap bitmap, List<decimal> danga, string totalAmt, string month, string msg)
        {
            InitializeComponent();
            bgBitmap = bitmap;
            this.danga = danga;
            this.dTotalAmt = totalAmt.ToDecimal();
            this.month = month;
            this.BackgroundImage = bitmap;

            saledDao.BRAND = salehDao.BRAND = vanDataDao.BRAND = StoreInfo.BrnadCode;
            //saledDao.BRAND = salehDao.BRAND = vanDataDao.BRAND = "00003";
            saledDao.STORE = salehDao.STORE = vanDataDao.STORE = StoreInfo.StoreCode;
            saledDao.DESK = salehDao.DESK = vanDataDao.DESK = StoreInfo.StoreDesk;
            saledDao.DATE = salehDao.DATE = vanDataDao.DATE = StoreInfo.StoreOpen;
        }

        private void FrmCardPayMentNice_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            pbGif.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\System\swf\card_in_crop.gif");
            UtilHelper.spCardWav.Play();

            ThreadPool.QueueUserWorkItem(CardApp, true);
        }

        private void picBoxPrev_MouseDown(object sender, MouseEventArgs e)
        {
            UtilHelper.spWav.Play();

            Bitmap bitmap = UtilHelper.ChangeOpacity(picBoxPrev.BackgroundImage, 1f);

            if (picBoxPrev.BackgroundImage != null)
            {
                picBoxPrev.BackgroundImage.Dispose();
                picBoxPrev.BackgroundImage = null;
            }
            picBoxPrev.BackgroundImage = bitmap;
            bitmap = null;
        }

        private void picBoxPrev_MouseUp(object sender, MouseEventArgs e)
        {
            if (picBoxPrev.BackgroundImage != null)
            {
                picBoxPrev.BackgroundImage.Dispose();
                picBoxPrev.BackgroundImage = null;
            }
            picBoxPrev.BackgroundImage = Properties.Resources.btn_back;
            UtilHelper.Delay(50);

            UtilHelper.spCardWav.Stop();
            progressIndicator1.Stop();

            REQ_STOP();

            this.DialogResult = DialogResult.Cancel;
        }

        private void CardApp(object sender)
        {
            int rtn;

            StringBuilder send_data = new StringBuilder();
            send_data.Clear();
            send_data.Append("0200");
            send_data.Append(FS);
            send_data.Append("10");
            send_data.Append(FS);
            send_data.Append("C");
            send_data.Append(FS);
            send_data.Append(dTotalAmt);
            send_data.Append(FS);
            send_data.Append("0");
            send_data.Append(FS);
            send_data.Append("0");
            send_data.Append(FS);
            send_data.Append(month);
            send_data.Append(FS);
            send_data.Append("");
            send_data.Append(FS);
            send_data.Append("");
            send_data.Append(FS);
            send_data.Append("");
            send_data.Append(FS);
            send_data.Append(FS);
            send_data.Append(FS);
            send_data.Append(FS);
            send_data.Append("0");
            send_data.Append(FS);

            UtilHelper.Delay(50);

            LogMenager.LogWriter(UtilHelper.Root + @"\Log\send\payMentSend", send_data.ToString());
            try
            {
                ReceiptPrint();
                return;

                rtn = NICEVCAT(send_data.ToString(), bRecvData);


                if (rtn == 1)
                {
                    string temp = Encoding.Default.GetString(bRecvData);
                    string recvMsg = string.Empty;
                    string valid = string.Empty;
                    byte[] buf = Encod.GetBytes(temp);

                    valid = Encod.GetString(buf, 8, 4);
                    recvMsg = Encod.GetString(buf, 0, 1024);
                    cardMsg = Encod.GetString(buf).Split(FS)[16];

                    LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + temp);

                    if (valid.CompareTo("0000") == 0)
                    {

                        this.Invoke(new MethodInvoker(
                            delegate ()
                            {
                                //this.Visible = false;

                                splashPayMentProc = new SplashPayMentProc(this, bgBitmap, 0);
                                splashPayMentProc.Open();

                                //로그기록
                                if (VanDataInsert(bRecvData))
                                {
                                    LogMenager.LogWriter(UtilHelper.Root + @"\Log\DB\Van", "[VAN]: VanData Insert Complete");
                                    if (SaleDInsert())
                                    {
                                        LogMenager.LogWriter(UtilHelper.Root + @"\Log\DB\SaleD", "[SaleD]: SaleD Data Insert Complete");
                                        if (SaleHInsert())
                                        {
                                            LogMenager.LogWriter(UtilHelper.Root + @"\Log\DB\SaleH", "[SaleH]: SaleH Data Insert Complete");
                                            splashPayMentProc.Close();
                                            OrderListPrintKitchen();
                                            this.DialogResult = DialogResult.OK;
                                        }
                                        else
                                        {
                                            LogMenager.LogWriter(UtilHelper.Root + @"\Log\DB\SaleH", "[SaleH]: SaleH Data Insert Failed");
                                        }
                                    }
                                    else
                                    {
                                        LogMenager.LogWriter(UtilHelper.Root + @"\Log\DB\SaleD", "[SaleD]: SaleD Data Insert Failed");
                                    }
                                }
                                else
                                {
                                    LogMenager.LogWriter(UtilHelper.Root + @"\Log\DB\Van", "[VAN]: VanData Insert Failed");
                                }
                            }));
                    }
                    else
                    {
                        //카드결제 실패
                        errBox = new FrmErrorBox("결제실패", cardMsg.Trim('\0'));

                        if (errBox.ShowDialog() == DialogResult.OK)
                        {
                            //REQ_STOP();
                            this.DialogResult = DialogResult.Cancel;
                        }
                    }
                }
                else if (rtn == -20)
                {
                    //errBox = new FrmErrorBox("결제시간초과", "올바른 카드가 아니거나\r\n카드가 삽입되지 않았습니다.");

                    //if (errBox.ShowDialog() == DialogResult.OK)
                    //{
                    //REQ_STOP();
                    this.DialogResult = DialogResult.Cancel;
                    //}
                }
                else if (rtn == -12)
                {
                    errBox = new FrmErrorBox("거래불가카드", "올바른 카드가 아니거나\r\n카드방향을 확인해주세요");

                    if (errBox.ShowDialog() == DialogResult.OK)
                    {
                        //REQ_STOP();
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
            }
            catch (Exception e)
            {
                //errBox = new FrmErrorBox("결제오류", "알수없는 오류 : " + e.ToString() + " 가 발생하였습니다.");
                errBox = new FrmErrorBox("네크워크 오류", "발행된 영수증을 가지고\r\n\r\n 카운터에 문의 바랍니다.");

                if (errBox.ShowDialog() == DialogResult.OK)
                {
                   // REQ_STOP();
                    splashPayMentComple.Close();
                    this.DialogResult = DialogResult.OK;
                }
                //REQ_STOP();
                //this.DialogResult = DialogResult.Cancel;
            }
            finally
            {
                REQ_STOP();
            }
        }

        private bool SaleDInsert()
        {
            saledDao.TIME = vanDataDao.APPTIME;

            for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
            {
                OrderListInfo.ItemOptAmt1[i] = OrderListInfo.ItemOptAmt1[i] * OrderListInfo.ItemQuan[i];
                OrderListInfo.ItemOptAmt2[i] = OrderListInfo.ItemOptAmt2[i] * OrderListInfo.ItemQuan[i];

                saledDao.RECENO = receNo;
                saledDao.GNAM = OrderListInfo.ItemGNam[i];
                saledDao.PNAM = OrderListInfo.ItemName[i]; // + " " + OrderListInfo.ItemOpNm[i];
                saledDao.CONT = OrderListInfo.ItemQuan[i];
                saledDao.UNIT = danga[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i];
                saledDao.PAMT = saledDao.PAMT = OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i];
                saledDao.OPNM = saledDao.OPNM = OrderListInfo.ItemOpNm[i];
                saledDao.TURN = turnNum;
                saledDao.TYPE = "C";

                if (StoreInfo.IsTakeOut)
                    saledDao.REMARK = "포장";
                else
                    saledDao.REMARK = "매장식사";

                saledDao.GUBN = "정상";

                if (saledDao.SaleDSave())
                {
                    if (saledDao.SaleDSaveL())
                    {
                        /*if (dbSaleD.GNAM == "맘스세트")
                        {
                            DBSelectSetList(dbSaleD.PNAM, i);
                            grpName = "맘스세트";
                        }
                        else
                            PrnRemark.Add(string.Empty);*/

                        if (OrderListInfo.Memo1[i].Trim().Length != 0 && OrderListInfo.Memo2[i].Trim().Length != 0)
                        {
                            saledDao.PNAM = OrderListInfo.Memo1[i].Trim();
                            saledDao.CONT = OrderListInfo.ItemQuan[i];

                            if (OrderListInfo.ItemOptAmt1[i] != 0)
                                saledDao.UNIT = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                            else
                                saledDao.UNIT = OrderListInfo.ItemOptAmt1[i];

                            saledDao.PAMT = OrderListInfo.ItemOptAmt1[i];

                            if (StoreInfo.IsTakeOut)
                                saledDao.REMARK = "포장";
                            else
                                saledDao.REMARK = "매장식사";

                            saledDao.GUBN = "정상";

                            if (saledDao.SaleDSave())
                            {
                                if (saledDao.SaleDSaveL())
                                {
                                    saledDao.PNAM = OrderListInfo.Memo2[i].Trim();
                                    saledDao.CONT = OrderListInfo.ItemQuan[i];

                                    if (OrderListInfo.ItemOptAmt2[i] != 0)
                                        saledDao.UNIT = OrderListInfo.ItemOptAmt2[i] / OrderListInfo.ItemQuan[i];
                                    else
                                        saledDao.UNIT = OrderListInfo.ItemOptAmt2[i];

                                    saledDao.PAMT = OrderListInfo.ItemOptAmt2[i];
                                    saledDao.OPNM = OrderListInfo.ItemOpNm[i];

                                    if (StoreInfo.IsTakeOut)
                                        saledDao.REMARK = "포장";
                                    else
                                        saledDao.REMARK = "매장식사";

                                    saledDao.GUBN = "정상";

                                    if (saledDao.SaleDSave())
                                    {
                                        if (saledDao.SaleDSaveL())
                                        {

                                        }
                                    }
                                }
                            }
                        }
                        else if (OrderListInfo.Memo1[i].Trim().Length == 0 && OrderListInfo.Memo2[i].Trim().Length != 0)
                        {
                            saledDao.PNAM = OrderListInfo.Memo2[i].Trim();
                            saledDao.CONT = OrderListInfo.ItemQuan[i];

                            if (OrderListInfo.ItemOptAmt2[i] != 0)
                                saledDao.UNIT = OrderListInfo.ItemOptAmt2[i] / OrderListInfo.ItemQuan[i];
                            else
                                saledDao.UNIT = OrderListInfo.ItemOptAmt2[i];

                            saledDao.PAMT = OrderListInfo.ItemOptAmt2[i];
                            saledDao.OPNM = OrderListInfo.ItemOpNm[i];

                            if (StoreInfo.IsTakeOut)
                                saledDao.REMARK = saledDao.REMARK = "포장";
                            else
                                saledDao.REMARK = saledDao.REMARK = "매장식사";

                            saledDao.GUBN = "정상";

                            if (saledDao.SaleDSave())
                            {
                                if (saledDao.SaleDSaveL())
                                {

                                }
                            }
                        }
                        else if (OrderListInfo.Memo1[i].Trim().Length != 0 && OrderListInfo.Memo2[i].Trim().Length == 0)
                        {
                            saledDao.PNAM = OrderListInfo.Memo1[i].Trim();
                            saledDao.CONT = OrderListInfo.ItemQuan[i];

                            if (OrderListInfo.ItemOptAmt1[i] != 0)
                                saledDao.UNIT = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                            else
                                saledDao.UNIT = OrderListInfo.ItemOptAmt1[i];

                            saledDao.PAMT = OrderListInfo.ItemOptAmt1[i];
                            saledDao.OPNM = OrderListInfo.ItemOpNm[i];

                            if (StoreInfo.IsTakeOut)
                                saledDao.REMARK = "포장";
                            else
                                saledDao.REMARK = "매장식사";

                            saledDao.GUBN = "정상";

                            if (saledDao.SaleDSave())
                            {
                                if (saledDao.SaleDSaveL())
                                {

                                }
                            }
                        }
                    }

                }
                else
                {

                }
            }

            return true;
        }

        private bool SaleHInsert()
        {
            salehDao.TIME = vanDataDao.APPTIME;
            salehDao.RECENO = receNo;
            salehDao.PAMT = dTotalAmt;
            salehDao.CARD = dTotalAmt;
            salehDao.SPAY = 0;
            salehDao.TYPE = "C";
            salehDao.REMARK = "";
            salehDao.TURN = turnNum;

            if (StoreInfo.IsTakeOut)
                salehDao.MEMO = "포장";
            else
                salehDao.MEMO = "매장식사";

            salehDao.GUBN = "정상";

            if (salehDao.SaleHSave())
            {
                if (salehDao.SaleHSaveL())
                    return true;
            }
            else
                return false;

            return false;
        }

        private bool VanDataInsert(byte[] buf)
        {
            try
            {
                byte[] bRecvData = buf;

                saledDao.SalesNumMaker();
                saledDao.TurnIndexMaker();

                receNo = saledDao.GetSalesNum();
                turnNum = saledDao.GetTurnNum();

                string[] sRecvData = Encod.GetString(bRecvData).Split(FS);

                vanDataDao.VANGB = StoreInfo.VanSelect;
                vanDataDao.DATE = StoreInfo.StoreOpen;
                vanDataDao.MOMTH = string.Format("{0:D2}", month.ToInteger());
                vanDataDao.RECENO = receNo;
                vanDataDao.APPAMT = dTotalAmt;
                vanDataDao.GUBN = sRecvData[0].Trim();
                vanDataDao.TYPE = sRecvData[1].Trim();
                vanDataDao.VALID = sRecvData[2].Trim();
                vanDataDao.TAX = sRecvData[4].ToDecimal();
                vanDataDao.BONG = sRecvData[5].ToDecimal();
                vanDataDao.APPNO = sRecvData[7].Trim();
                vanDataDao.APPDATE = sRecvData[8].Substring(0, 6);
                vanDataDao.APPTIME = sRecvData[8].Substring(6, 6);
                vanDataDao.ORDCD = sRecvData[9].Trim();
                vanDataDao.ORDNM = sRecvData[10].Trim();
                vanDataDao.VANKEY = "";
                vanDataDao.INPCD = sRecvData[11].Trim();
                vanDataDao.INPNM = sRecvData[12].Trim();
                vanDataDao.STOCD = sRecvData[13].Trim();
                vanDataDao.CATID = sRecvData[14].Trim();
                vanDataDao.MSG = sRecvData[16].Trim();
                vanDataDao.CARDNO = sRecvData[17].Trim();
                vanDataDao.CDTYPE = sRecvData[18].Trim();
                vanDataDao.REMARK = "C";

                if (vanDataDao.VanDataSave())
                    return vanDataDao.VanDataSaveL();

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        #region 프린트 함수

        private void ReceiptPrint()
        {
            try
            {
                Encoding encod = Encoding.Default;
                string strTab1;
                string strTab2;
                byte[] bOpnm1;
                byte[] bOpnm2;

                serialPortReceipt.PortName = StoreInfo.ReceiptPrn;
                serialPortReceipt.BaudRate = StoreInfo.ReceiptRate;
                serialPortReceipt.DataBits = 8;
                serialPortReceipt.Parity = System.IO.Ports.Parity.None;
                serialPortReceipt.StopBits = System.IO.Ports.StopBits.One;
                serialPortReceipt.Encoding = encod;

                string strDate = string.Format("{0:D4}.{1:D2}.{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                string strTime = string.Format("{0:D2}:{1:D2}:{2:D2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                if (serialPortReceipt.IsOpen == false)
                    serialPortReceipt.Open();

                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                //serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(0, 1));
                serialPortReceipt.WriteLine(StoreInfo.StoreName + "\r\n");

                serialPortReceipt.WriteLine("*** 영 수 증 ***\r\n");

                //serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(0, 0));
                serialPortReceipt.WriteLine(string.Format("{0:-39}", "[" + StoreInfo.StoreName + "]"));

                byte[] bTmpAdd = encod.GetBytes(StoreInfo.StoreAdd2);

                if (bTmpAdd.Length > 20)
                {
                    serialPortReceipt.WriteLine(string.Format("{0, -35}", StoreInfo.StoreAdd1));
                    serialPortReceipt.WriteLine(string.Format("{0, -40}", StoreInfo.StoreAdd2));
                }
                else
                {
                    serialPortReceipt.WriteLine(string.Format("{0, -35}", StoreInfo.StoreAdd1 + " " + StoreInfo.StoreAdd2));
                }

                serialPortReceipt.WriteLine(string.Format("{0, -6}{1, 12}{2, 23}", StoreInfo.StoreCeo, "[" + StoreInfo.StoreSano + "]",
                    "T:" + StoreInfo.StorePhon));
                serialPortReceipt.WriteLine(string.Format("{0:D2}-{1,5}", StoreInfo.StoreDesk.ToInteger(), receNo));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortReceipt.WriteLine(string.Format("{0, -16}{1, 12}{2, 5}{3, 5}", "  품목", "       단가", "수량", "금액"));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));

                for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
                {
                    byte[] bTmpName = encod.GetBytes(OrderListInfo.ItemName[i] + OrderListInfo.ItemOpNm[i]);

                    if (bTmpName.Length > 22)
                    {
                        serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                        bOpnm1 = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                        if (bOpnm1.Length < 4)
                            strTab1 = "\t";
                        else
                            strTab1 = "\t";

                        byte[] bTmpItemName = encod.GetBytes(OrderListInfo.ItemName[i]);

                        if(bTmpItemName.Length >= 20)
                        {
                            string tempName = "";
                            string tempLastName = "";
                            foreach (string str in OrderListInfo.ItemName[i].Split(' '))
                            {
                                if (str == OrderListInfo.ItemName[i].Split(' ')[OrderListInfo.ItemName[i].Split(' ').Length - 1])
                                {
                                    tempLastName = str;
                                    break;
                                }
                                tempName += " " + str;
                            }

                            serialPortReceipt.WriteLine(string.Format("{0:50}", UtilHelper.PadingLeftBySpace(tempName, 50)));
                            serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2, 8}{3, 4}{4, 10}", UtilHelper.PadingLeftBySpace(tempLastName, 20), "\t",
                                string.Format("{0:##,##0}", (OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i]) / OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i])));

                            //serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2, 8}{3, 4}{4, 10}", UtilHelper.PadingLeftBySpace(tempName.Trim(), 20), strTab1,
                            //    string.Format("{0:##,##0}", (OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i]) / OrderListInfo.ItemQuan[i]),
                            //    string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                            //    string.Format("{0:##,##0}", OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i])));

                            //serialPortReceipt.WriteLine(string.Format("{0:48}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i], 48)));
                            //serialPortReceipt.WriteLine(string.Format("{0:33}{1, 4}{2, 10}",
                            //    UtilHelper.PadingRightBySpace(string.Format("{0:##,##0}", (OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i]) / OrderListInfo.ItemQuan[i]),33),
                            //    string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                            //    string.Format("{0:##,##0}", OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i])));
                        }
                        else
                        {
                            serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2, 8}{3, 4}{4, 10}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i], 20), strTab1,
                                string.Format("{0:##,##0}", (OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i]) / OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i])));
                        }

                        switch(OrderListInfo.ItemType[i])
                        {
                            case "1":
                                serialPortReceipt.WriteLine(string.Format("{0:44}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemOpNm[i], 44)));
                                break;
                            case "2":
                            case "3":
                                if (OrderListInfo.ItemOpNm[i].Trim() != "")
                                {
                                    if (bOpnm1.Length >= 18)
                                        serialPortReceipt.WriteLine(string.Format("{0:46}", "└" + UtilHelper.PadingLeftBySpace(OrderListInfo.ItemOpNm[i], 50)));
                                    else
                                        serialPortReceipt.WriteLine(string.Format("{0:46}", "└" + UtilHelper.PadingLeftBySpace(OrderListInfo.ItemOpNm[i], 46)));
                                }
                                break;
                        }

                        if (OrderListInfo.Memo1[i].Trim().Length != 0 && OrderListInfo.Memo2[i].Trim().Length != 0)
                        {
                            decimal opDanga1 = 0;
                            decimal opDanga2 = 0;
                            decimal opTotAmt1 = 0;
                            decimal opTotAmt2 = 0;

                            if (OrderListInfo.ItemOptAmt1[i] != 0)
                            {
                                opDanga1 = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                                opTotAmt1 = OrderListInfo.ItemOptAmt1[i];
                            }
                            else
                            {
                                opDanga1 = 0;
                                opTotAmt1 = 0;
                            }

                            if (OrderListInfo.ItemOptAmt2[i] != 0)
                            {
                                opDanga2 = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                                opTotAmt2 = OrderListInfo.ItemOptAmt2[i];
                            }
                            else
                            {
                                opDanga2 = 0;
                                opTotAmt2 = 0;
                            }

                            bOpnm1 = encod.GetBytes("└" + OrderListInfo.Memo1[i]);
                            bOpnm2 = encod.GetBytes("└" + OrderListInfo.Memo2[i]);

                            if (bOpnm1.Length >= 24)
                                strTab1 = "";
                            else
                                strTab1 = "\t";

                            if (bOpnm2.Length >= 24)
                                strTab2 = "";
                            else
                                strTab2 = "\t";

                            serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2,8}{3,4}{4,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 20), strTab1,
                                string.Format("{0:##,##0}", opDanga1),
                                string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemOptAmt1[i])));

                            serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2,8}{3,4}{4,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 20), strTab2,
                                string.Format("{0:##,##0}", opDanga2),
                                string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemOptAmt2[i])));
                        }
                        else if (OrderListInfo.Memo1[i].Trim().Length != 0 && OrderListInfo.Memo2[i].Trim().Length == 0)
                        {
                            decimal opDanga1 = 0;
                            decimal opTotAmt1 = 0;

                            if (OrderListInfo.ItemOptAmt1[i] != 0)
                            {
                                opDanga1 = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                                opTotAmt1 = OrderListInfo.ItemAmt[i];
                            }
                            else
                            {
                                opDanga1 = 0;
                                opTotAmt1 = 0;
                            }
                            serialPortReceipt.WriteLine(string.Format("{0:20}\t{1,8}{2,4}{3,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 20),
                                string.Format("{0:##,##0}", opDanga1),
                                string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemOptAmt1[i])));
                        }
                        else if (OrderListInfo.Memo1[i].Trim().Length == 0 && OrderListInfo.Memo2[i].Trim().Length != 0)
                        {
                            decimal opDanga2 = 0;
                            decimal opTotAmt2 = 0;

                            if (OrderListInfo.ItemOptAmt2[i] != 0)
                            {
                                opDanga2 = OrderListInfo.ItemOptAmt2[i] / OrderListInfo.ItemQuan[i];
                                opTotAmt2 = OrderListInfo.ItemAmt[i];
                            }
                            else
                            {
                                opDanga2 = 0;
                                opTotAmt2 = 0;
                            }
                            serialPortReceipt.WriteLine(string.Format("{0:20}\t{1,8}{2,4}{3,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 20),
                                string.Format("{0:##,##0}", opDanga2),
                                string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemOptAmt2[i])));
                        }
                    }
                    else
                    {
                        serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                        serialPortReceipt.WriteLine(string.Format("{0:20}\t{1,8}{2,4}{3,10}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " + OrderListInfo.ItemOpNm[i], 20),
                            string.Format("{0:##,##0}", (OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i]) / OrderListInfo.ItemQuan[i]),
                            string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                            string.Format("{0:##,##0}", OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i])));

                        if (OrderListInfo.Memo1[i].Trim().Length != 0 && OrderListInfo.Memo2[i].Trim().Length != 0)
                        {
                            decimal opDanga1 = 0;
                            decimal opDanga2 = 0;
                            decimal opTotAmt1 = 0;
                            decimal opTotAmt2 = 0;

                            if (OrderListInfo.ItemOptAmt1[i] != 0)
                            {
                                opDanga1 = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                                opTotAmt1 = OrderListInfo.ItemOptAmt1[i];
                            }
                            else
                            {
                                opDanga1 = 0;
                                opTotAmt1 = 0;
                            }

                            if (OrderListInfo.ItemOptAmt2[i] != 0)
                            {
                                opDanga2 = OrderListInfo.ItemOptAmt2[i] / OrderListInfo.ItemQuan[i];
                                opTotAmt2 = OrderListInfo.ItemOptAmt2[i];
                            }
                            else
                            {
                                opDanga2 = 0;
                                opTotAmt2 = 0;
                            }

                            bOpnm1 = encod.GetBytes("└" + OrderListInfo.Memo1[i]);

                            if (bOpnm1.Length >= 24)
                                strTab1 = "";
                            else
                                strTab1 = "\t";

                            serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2,8}{3,4}{4,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 20), strTab1,
                            string.Format("{0:##,##0}", opDanga1),
                            string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                            string.Format("{0:##,##0}", opTotAmt1)));

                            bOpnm2 = encod.GetBytes("└" + OrderListInfo.Memo2[i]);

                            if (bOpnm2.Length >= 24)
                                strTab2 = "";
                            else
                                strTab2 = "\t";

                            serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2,8}{3,4}{4,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 20), strTab2,
                            string.Format("{0:##,##0}", opDanga2),
                            string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                            string.Format("{0:##,##0}", opTotAmt2)));
                        }
                        else if (OrderListInfo.Memo1[i].Trim().Length != 0 && OrderListInfo.Memo2[i].Trim().Length == 0)
                        {
                            decimal opDanga1 = 0;
                            decimal opTotamt1 = 0;

                            if (OrderListInfo.ItemOptAmt1[i] != 0)
                            {
                                opDanga1 = OrderListInfo.ItemOptAmt1[i] / OrderListInfo.ItemQuan[i];
                                opTotamt1 = OrderListInfo.ItemAmt[i];
                            }
                            else
                            {
                                opDanga1 = 0;
                                opTotamt1 = 0;
                            }
                            serialPortReceipt.WriteLine(string.Format("{0:20}\t{1,8}{2,4}{3,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 20),
                                string.Format("{0:##,##0}", opDanga1),
                                string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemOptAmt1[i])));
                        }
                        else if (OrderListInfo.Memo1[i].Trim().Length == 0 && OrderListInfo.Memo2[i].Trim().Length != 0)
                        {
                            decimal opDanga2 = 0;
                            decimal opTotamt2 = 0;

                            if (OrderListInfo.ItemOptAmt2[i] != 0)
                            {
                                opDanga2 = OrderListInfo.ItemOptAmt2[i] / OrderListInfo.ItemQuan[i];
                                opTotamt2 = OrderListInfo.ItemAmt[i];
                            }
                            else
                            {
                                opDanga2 = 0;
                                opTotamt2 = 0;
                            }
                            serialPortReceipt.WriteLine(string.Format("{0:20}\t{1,8}{2,4}{3,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 20),
                                string.Format("{0:##,##0}", opDanga2),
                                string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                                string.Format("{0:##,##0}", OrderListInfo.ItemOptAmt2[i])));
                        }
                    }
                }
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortReceipt.WriteLine(string.Format("                          과세금액{0,12:##,##0}", GetAmt()));
                serialPortReceipt.WriteLine(string.Format("                          부 가 세{0,12:##,##0}", GetVat()));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));

                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                //serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(16 + 0));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(1, 0));
                serialPortReceipt.WriteLine(string.Format("총매출액{0,15:##,###}", dTotalAmt));
                serialPortReceipt.WriteLine(string.Format("합계금액{0,15:##,###}", dTotalAmt));
                serialPortReceipt.WriteLine(string.Format("받은금액{0,15:##,###}", dTotalAmt));
                //serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(0, 0));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortReceipt.WriteLine(string.Format("                [카드]{0,24:##,###}", dTotalAmt));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                serialPortReceipt.WriteLine("**" + vanDataDao.ORDNM + "**");
                //serialPortReceipt.WriteLine("**" + "테스트1" + "**");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));

                serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 카드번호 ]", string.Format("{0}", vanDataDao.CARDNO.Trim())));
                //serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 카드번호 ]", "123456789"));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 승인시간 ]", strDate + " " + strTime));
                serialPortReceipt.WriteLine(string.Format("{0}{1, 31}", "[ 승인금액 ]", string.Format("{0:##,##0}", dTotalAmt) + "[ 일시불 ]"));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 승인번호 ]", vanDataDao.APPNO.Trim()));
                //serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 승인번호 ]", "987654321"));
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                if (StoreInfo.IsTakeOut)
                    serialPortReceipt.WriteLine("** 포장 판매 **");
                else
                    serialPortReceipt.WriteLine("** 매장 식사 **");

                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));

                serialPortReceipt.Write(Convert.ToChar(0X1D).ToString() + Convert.ToChar(0X48) + Convert.ToChar(0X2)); //바코드 문자 출력 위치 설정
                serialPortReceipt.Write(Convert.ToChar(0X1D).ToString() + Convert.ToChar(0X68) + Convert.ToChar(0X45)); //바코드 높이 설정
                serialPortReceipt.Write(Convert.ToChar(0X1D).ToString() + Convert.ToChar(0X6B) + Convert.ToChar(72) + Convert.ToChar(13)
                    + string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) + receNo);  //바코드 Code93
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46) + "\r\n\r\n");
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(1, 1));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                serialPortReceipt.WriteLine("");
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReceipt.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "S");

            }
            catch (Exception e)
            {
                serialPortReceipt.Close();
            }
            finally
            {
                serialPortReceipt.Close();
            }
        }

        private void TurnNumPrint()
        {
            try
            {
                serialPortReceipt.PortName = StoreInfo.ReceiptPrn;
                serialPortReceipt.BaudRate = StoreInfo.ReceiptRate;
                serialPortReceipt.DataBits = 8;
                serialPortReceipt.Parity = System.IO.Ports.Parity.None;
                serialPortReceipt.StopBits = System.IO.Ports.StopBits.One;
                serialPortReceipt.Encoding = Encoding.Default;

                string strPrintNum = string.Format("{0:D3}", Convert.ToInt64(turnNum));
                string strDate = DateTime.Now.ToString();

                if (serialPortReceipt.IsOpen == false)
                    serialPortReceipt.Open();

                serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1)); // 0:왼쪽 정렬, 1:중앙정렬 2:오른쪽정렬
                serialPortReceipt.Write(Convert.ToChar(0x1C) + "p" + Convert.ToChar(0) + Convert.ToChar(3));
                serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(16 + 1));
                serialPortReceipt.WriteLine("대기번호표");
                serialPortReceipt.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                serialPortReceipt.WriteLine("");
                serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                serialPortReceipt.WriteLine("[" + StoreInfo.StoreName + "]");
                serialPortReceipt.WriteLine("");
                serialPortReceipt.Write(Convert.ToChar(0x1C) + "W" + Convert.ToChar(0));
                //serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(32 + 1));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(7, 7));
                serialPortReceipt.WriteLine(strPrintNum);
                serialPortReceipt.Write(Convert.ToChar(0X1B) + "S");
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReceipt.WriteLine("");
                serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortReceipt.WriteLine("출력시간 : " + strDate);
                serialPortReceipt.WriteLine("");
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortReceipt.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "S");
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                serialPortReceipt.Close();
            }
            finally
            {
                serialPortReceipt.Close();
            }
        }

        private void IpPrint(string flag)
        {
            TcpClient tc;
            NetworkStream stream;

            string prnNm = string.Empty;

            if (flag.CompareTo("C") == 0)
            {
                tc = new TcpClient(StoreInfo.CounterPrnIP1, StoreInfo.CounterPrnPort1);
                stream = tc.GetStream();
                prnNm = "[카운터]";
            }
            else
            {
                if (flag.CompareTo("K") == 0)
                {
                    tc = new TcpClient(StoreInfo.KitchenPrnIP1, StoreInfo.KitchenPrnPort1);
                    stream = tc.GetStream();
                    prnNm = "[주방1]";
                }
                else
                {
                    tc = new TcpClient(StoreInfo.KitchenPrnIP2, StoreInfo.KitchenPrnPort2);
                    stream = tc.GetStream();
                    prnNm = "[주방2]";
                }
            }

            try
            {
                Encoding encod = Encoding.GetEncoding("ks_c_5601-1987");
                string strTab;
                byte[] bOpnm;
                byte[] buff;

                string strDate = string.Format("{0:D4}.{1:D2}.{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                string strTime = string.Format("{0:D2}:{1:D2}:{2:D2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(prnNm + "\r\n\r\n");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(UtilHelper.HyphepMake("=", 42) + "\r\n");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes("주문시간 : ");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(strDate + " ");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(strTime + " Kiosk : " + StoreInfo.StoreDesk + "\r\n");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(UtilHelper.HyphepMake("=", 42) + "\r\n");
                stream.Write(buff, 0, buff.Length);

                for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
                {
                    byte[] bTmpName = encod.GetBytes(OrderListInfo.ItemName[i] + OrderListInfo.ItemOpNm[i]);

                    if (bTmpName.Length > 20)
                    {
                        buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i], 40)));
                        stream.Write(buff, 0, buff.Length);

                        bOpnm = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                        strTab = "\t";
                        buff = encod.GetBytes(string.Format("  {0:20}{1}{2,4}{3,9}\r\n", UtilHelper.PadingLeftBySpace(
                            OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));
                        stream.Write(buff, 0, buff.Length);

                        if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                        {
                            buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            stream.Write(buff, 0, buff.Length);

                            buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                            stream.Write(buff, 0, buff.Length);
                        }

                        if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == string.Empty)
                        {
                            buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            stream.Write(buff, 0, buff.Length);

                            buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                            stream.Write(buff, 0, buff.Length);
                        }

                        if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != string.Empty)
                        {
                            buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            stream.Write(buff, 0, buff.Length);

                            buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                            stream.Write(buff, 0, buff.Length);
                        }
                    }
                    else
                    {
                        strTab = "\t";

                        buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                        stream.Write(buff, 0, buff.Length);

                        buff = encod.GetBytes(string.Format("  {0:20}{1}{2,4}{3,9}\r\n", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " +
                            OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));
                        stream.Write(buff, 0, buff.Length);

                        if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                        {
                            buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            stream.Write(buff, 0, buff.Length);

                            buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                            stream.Write(buff, 0, buff.Length);
                        }

                        if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == string.Empty)
                        {
                            buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            stream.Write(buff, 0, buff.Length);

                            buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                            stream.Write(buff, 0, buff.Length);
                        }

                        if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != string.Empty)
                        {
                            buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            stream.Write(buff, 0, buff.Length);

                            buff = encod.GetBytes(string.Format("  {0:40}\r\n", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                            stream.Write(buff, 0, buff.Length);
                        }
                    }

                    //if (prnRemark[i] != string.Empty)
                    //{
                    //    string[] temp = prnRemark[i].Split(Convert.ToChar(0xA));

                    //    buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                    //    stream.Write(buff, 0, buff.Length);
                    //    buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                    //    stream.Write(buff, 0, buff.Length);

                    //    buff = encod.GetBytes("  > (");
                    //    stream.Write(buff, 0, buff.Length);

                    //    for (int j = 0; j < temp.Length; j++)
                    //    {
                    //        buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                    //        stream.Write(buff, 0, buff.Length);
                    //        buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                    //        stream.Write(buff, 0, buff.Length);
                    //        buff = encod.GetBytes(temp[j]);
                    //        stream.Write(buff, 0, buff.Length);

                    //        if (temp.Length != i)
                    //        {
                    //            buff = encod.GetBytes(" ");
                    //            stream.Write(buff, 0, buff.Length);
                    //        }
                    //    }
                    //    buff = encod.GetBytes(")\r\n");
                    //    stream.Write(buff, 0, buff.Length);
                    //}

                    if (i == OrderListInfo.ItemName.Count - 1)
                    {
                        buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(UtilHelper.HyphepMake("=", 42) + "\r\n");
                        stream.Write(buff, 0, buff.Length);
                    }
                    else
                    {
                        buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(UtilHelper.HyphepMake("-", 42) + "\r\n");
                        stream.Write(buff, 0, buff.Length);
                    }
                }

                buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(1));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(16 + 1));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes("\r\n");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes("키오스크 : [ 주문번호 : " + string.Format("{0:D3} ]\r\n\r\n", Convert.ToInt64(turnNum)));
                stream.Write(buff, 0, buff.Length);

                if (StoreInfo.IsTakeOut)
                {
                    buff = encod.GetBytes("<< 포장판매 >> \r\n\r\n\r\n");
                    stream.Write(buff, 0, buff.Length);
                }
                else
                {
                    buff = encod.GetBytes("<< 매장식사 >> \r\n\r\n\r\n");
                    stream.Write(buff, 0, buff.Length);
                }

                buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(1 + 1));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes("\r\n");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(Convert.ToChar(0x1B) + "S");
                stream.Write(buff, 0, buff.Length);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message + "아이피 출력");
                stream.Close();
                tc.Close();
            }
            finally
            {
                stream.Close();
                tc.Close();
            }
        }

        private void OrderListPrintKitchen()
        {
            splashPayMentComple = new SplashPayMentComple(this, bgBitmap);
            splashPayMentComple.Open();

            for(int i = 1; i < 2; i++)
            {
                KitchenPrint(i);
            }
            CounterPrint();
            ReceiptPrint();
            TurnNumPrint();
            UtilHelper.spCardEndWav.Play();
            splashPayMentComple.Close();
        }

        private void KitchenPrint(int kitchenNo)
        {
            string kitchenPrn = string.Empty;
            int kitchenRate = 0;
            string kitchenPrnIP = string.Empty;
            string kitchenType = string.Empty;

            switch(kitchenNo)
            {
                case 1:
                    kitchenPrn = StoreInfo.Kitchen1Prn;
                    kitchenRate = StoreInfo.Kitchen1Rate;
                    kitchenPrnIP = StoreInfo.KitchenPrnIP1;
                    kitchenType = StoreInfo.Kitchen1Type;
                    break;
                case 2:
                    kitchenPrn = StoreInfo.Kitchen2Prn;
                    kitchenRate = StoreInfo.Kitchen2Rate;
                    kitchenPrnIP = StoreInfo.KitchenPrnIP2;
                    kitchenType = StoreInfo.Kitchen2Type;
                    break;
            }

            Encoding encod = Encoding.Default;
            string strTab;
            byte[] bOpnm;

            if (kitchenPrn.CompareTo("사용안함") == 0)
            {
                if (kitchenType == "1")
                    return;
                if (kitchenNo == 1 && kitchenPrnIP.CompareTo("...") != 0)
                {
                    IpPrint("K");
                }
                else if (kitchenNo == 2 && kitchenPrnIP.CompareTo("...") != 0)
                {
                    IpPrint("K1");
                }
            }

            try
            {
                serialPortKitchen.PortName = kitchenPrn;
                serialPortKitchen.BaudRate = kitchenRate;
                serialPortKitchen.DataBits = 8;
                serialPortKitchen.Parity = System.IO.Ports.Parity.None;
                serialPortKitchen.StopBits = System.IO.Ports.StopBits.One;
                serialPortKitchen.Encoding = encod;

                string strDate = string.Format("{0:D4}.{1:D2}.{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                string strTime = string.Format("{0:D2}:{1:D2}:{2:D2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                if (serialPortKitchen.IsOpen == false)
                    serialPortKitchen.Open();

                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                serialPortKitchen.WriteLine(StoreInfo.StoreName + "\r\n\r\n");
                serialPortKitchen.WriteLine("[주방]");
                serialPortKitchen.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                serialPortKitchen.WriteLine(UtilHelper.HyphepMake("=", 42));
                serialPortKitchen.Write("주문시간 : ");
                serialPortKitchen.Write(strDate + " ");
                serialPortKitchen.WriteLine(strTime + "  Kiosk : " + StoreInfo.StoreDesk);
                serialPortKitchen.WriteLine(UtilHelper.HyphepMake("=", 42));

                for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
                {
                    byte[] bTmpName = encod.GetBytes(OrderListInfo.ItemName[i] + OrderListInfo.ItemOpNm[i]);

                    if (kitchenRate.CompareTo(9600) == 0)
                    {
                        if (bTmpName.Length > 20)
                        {
                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                            bOpnm = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                            if (kitchenType.CompareTo("2") == 0)
                                strTab = "\t";
                            else
                                strTab = "\t";

                            byte[] bTempItemNm = encod.GetBytes(OrderListInfo.ItemName[i]);

                            if (bTempItemNm.Length > 20)
                                serialPortKitchen.WriteLine(string.Format("{0:20}{1,4}{2,9}", UtilHelper.PadingLeftBySpace(
                                    OrderListInfo.ItemName[i], 20), OrderListInfo.ItemQuan[i], "주문"));
                            else
                                serialPortKitchen.WriteLine(string.Format("{0:23}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(
                                    OrderListInfo.ItemName[i], 23), strTab, OrderListInfo.ItemQuan[i], "주문"));

                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                            switch (OrderListInfo.ItemType[i])
                            {
                                case "1":
                                    serialPortKitchen.WriteLine(string.Format("{0:42}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemOpNm[i].Trim(), 42)));
                                    if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                        {
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        }
                                        else
                                        {
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        }
                                    }

                                    if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        else
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));

                                    }

                                    if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                        else
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                    }
                                    break;
                                case "2":
                                case "3":
                                    if (OrderListInfo.ItemOpNm[i].Trim() != "")
                                        serialPortKitchen.WriteLine(string.Format("{0:43}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.ItemOpNm[i].Trim(), 43)));
                                    break;
                            }

                        }
                        else
                        {
                            if (kitchenType.CompareTo("2") == 0)
                                strTab = "\t";
                            else
                                strTab = "\t";

                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                            serialPortKitchen.WriteLine(string.Format("{0:20}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " +  //일반 9600프린터
                                OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                            switch (OrderListInfo.ItemType[i])
                            {
                                case "1":
                                    if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                        {
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        }
                                        else
                                        {
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        }
                                    }

                                    if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        else
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));

                                    }

                                    if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                        else
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                    }
                                    break;
                                case "2":
                                case "3":

                                    break;
                            }
                        }
                        //if (prnRemark[i] != string.Empty)
                        //{
                        //    string[] temp = prnRemark[i].Split(Convert.ToChar(0xA));

                        //    serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        //    serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                        //    serialPortKitchen.Write("> (");
                        //    for (int j = 0; j < temp.Length; j++)
                        //    {
                        //        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        //        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                        //        serialPortKitchen.Write(temp[j]);
                        //        if (temp.Length != i)
                        //            serialPortKitchen.Write(" ");
                        //    }
                        //    serialPortKitchen.WriteLine(")");
                        //}
                    }
                    else
                    {

                        if (bTmpName.Length > 20)
                        {
                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                            bOpnm = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                            if (kitchenType.CompareTo("2") == 0)
                                strTab = "\t";
                            else
                                strTab = "\t";

                            byte[] bTempItemNm = encod.GetBytes(OrderListInfo.ItemName[i]);

                            if (bTempItemNm.Length > 20)
                                serialPortKitchen.WriteLine(string.Format("{0:20}{1,4}{2,9}", UtilHelper.PadingLeftBySpace(
                                    OrderListInfo.ItemName[i], 20), OrderListInfo.ItemQuan[i], "주문"));
                            else
                                serialPortKitchen.WriteLine(string.Format("{0:23}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(
                                    OrderListInfo.ItemName[i], 23), strTab, OrderListInfo.ItemQuan[i], "주문"));

                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                            switch (OrderListInfo.ItemType[i])
                            {
                                case "1":
                                    serialPortKitchen.WriteLine(string.Format("{0:42}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemOpNm[i].Trim(), 42)));
                                    if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                        {
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        }
                                        else
                                        {
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        }
                                    }

                                    if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        else
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));

                                    }

                                    if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                        else
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                    }
                                    break;
                                case "2":
                                case "3":
                                    if (OrderListInfo.ItemOpNm[i].Trim() != "")
                                        serialPortKitchen.WriteLine(string.Format("{0:43}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.ItemOpNm[i].Trim(), 43)));
                                    break;
                            }
                        }
                        else
                        {
                            if (kitchenType.CompareTo("2") == 0)
                                strTab = "\t";
                            else
                                strTab = "\t";

                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                            serialPortKitchen.WriteLine(string.Format("{0:22}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " +  //일반 9600프린터
                                OrderListInfo.ItemOpNm[i], 22), strTab, OrderListInfo.ItemQuan[i], "주문"));

                            switch (OrderListInfo.ItemType[i])
                            {
                                case "1":
                                    if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                        {
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        }
                                        else
                                        {
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        }
                                    }

                                    if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                        else
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));

                                    }

                                    if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == "")
                                    {
                                        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                        if (kitchenType.CompareTo("2") == 0)
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                        else
                                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                    }
                                    break;
                                case "2":
                                case "3":

                                    break;
                            }
                        }
                        //if (prnRemark.Count > 0)
                        //{
                        //    if (prnRemark[i] != string.Empty)
                        //    {
                        //        string[] temp = prnRemark[i].Split(Convert.ToChar(0xA));

                        //        serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        //        serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                        //        serialPortKitchen.Write("> (");
                        //        for (int j = 0; j < temp.Length; j++)
                        //        {
                        //            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        //            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                        //            serialPortKitchen.Write(temp[j]);
                        //            if (temp.Length != i)
                        //                serialPortKitchen.Write(" ");
                        //        }
                        //        serialPortKitchen.WriteLine(")");
                        //    }
                        //}
                    }
                    if (i == OrderListInfo.ItemName.Count - 1)
                    {
                        serialPortKitchen.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                        serialPortKitchen.Write(Convert.ToChar(0X1B) + "a" + Convert.ToChar(1));
                        serialPortKitchen.WriteLine(UtilHelper.HyphepMake("=", 42));

                    }
                    else
                    {
                        serialPortKitchen.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                        serialPortKitchen.Write(Convert.ToChar(0X1B) + "a" + Convert.ToChar(1));
                        serialPortKitchen.WriteLine(UtilHelper.HyphepMake("-", 42));
                    }
                }

                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortKitchen.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0 + 1));
                serialPortKitchen.WriteLine("");
                serialPortKitchen.WriteLine(("키오스크 : [ 주문번호 : " + string.Format("{0:D3} ]\r\n", Convert.ToInt64(turnNum))));

                if (StoreInfo.IsTakeOut)
                    serialPortKitchen.WriteLine("<< 포장판매 >> \r\n\r\n\r\n");
                else
                    serialPortKitchen.WriteLine("<< 매장식사 >> \r\n\r\n\r\n");

                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(1 + 1));
                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortKitchen.WriteLine("");
                serialPortKitchen.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortKitchen.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortKitchen.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortKitchen.Write(Convert.ToChar(0x1B) + "S");
            }
            catch (Exception e)
            {
                serialPortKitchen.Close();
            }
            finally
            {
                serialPortKitchen.Close();
            }
        }

        private void CounterPrint()
        {
            if (StoreInfo.CounterPrn.CompareTo("사용안함") == 0)
            {
                if (StoreInfo.CounterPrnIP1.CompareTo("...") != 0)
                {
                    IpPrint("C");
                    return;
                }
            }
            try
            {
                Encoding encod = Encoding.Default;
                string strTab;
                byte[] bOpnm;

                serialPortCounter.PortName = StoreInfo.CounterPrn;
                serialPortCounter.BaudRate = StoreInfo.CounterRate;
                serialPortCounter.DataBits = 8;
                serialPortCounter.Parity = System.IO.Ports.Parity.None;
                serialPortCounter.StopBits = System.IO.Ports.StopBits.One;
                serialPortCounter.Encoding = encod;

                string strDate = string.Format("{0:D4}.{1:D2}.{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                string strTime = string.Format("{0:D2}:{1:D2}:{2:D2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                if (serialPortCounter.IsOpen == false)
                    serialPortCounter.Open();

                serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                serialPortCounter.WriteLine(StoreInfo.StoreName + "\r\n\r\n");
                serialPortCounter.WriteLine("[카운터]");
                serialPortCounter.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                serialPortCounter.WriteLine(UtilHelper.HyphepMake("=", 42));
                serialPortCounter.Write("주문시간 : ");
                serialPortCounter.Write(strDate + " ");
                serialPortCounter.WriteLine(strTime + "  Kiosk : " + StoreInfo.StoreDesk);
                serialPortCounter.WriteLine(UtilHelper.HyphepMake("=", 42));

                for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
                {
                    byte[] bTmpName = encod.GetBytes(OrderListInfo.ItemName[i] + OrderListInfo.ItemOpNm[i]);

                    if (bTmpName.Length > 20)
                    {
                        serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                        bOpnm = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                        if (StoreInfo.CounterType.CompareTo("2") == 0)
                            strTab = "\t";
                        else
                            strTab = "\t";

                        byte[] bTmpItemName = encod.GetBytes(OrderListInfo.ItemName[i]);

                        if (bTmpItemName.Length >= 20)
                            serialPortCounter.WriteLine(string.Format("{0:20}{1,4}{2,9}", UtilHelper.PadingLeftBySpace(
                                OrderListInfo.ItemName[i], 20), OrderListInfo.ItemQuan[i], "주문"));
                        else
                            serialPortCounter.WriteLine(string.Format("{0:22}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(
                                OrderListInfo.ItemName[i], 22), strTab, OrderListInfo.ItemQuan[i], "주문"));

                        switch (OrderListInfo.ItemType[i])
                        {
                            case "1":
                                serialPortCounter.WriteLine(string.Format("{0:42}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemOpNm[i].Trim(), 42)));
                                if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                                {
                                    serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                    serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                    if (StoreInfo.CounterType.CompareTo("2") == 0)
                                    {
                                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                    }
                                    else
                                    {
                                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                    }
                                }

                                if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != "")
                                {
                                    serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                    serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                    if (StoreInfo.CounterType.CompareTo("2") == 0)
                                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                    else
                                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));

                                }

                                if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == "")
                                {
                                    serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                    serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                                    if (StoreInfo.CounterType.CompareTo("2") == 0)
                                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                    else
                                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                }
                                break;
                            case "2":
                            case "3":
                                if (OrderListInfo.ItemOpNm[i].Trim() != "")
                                    serialPortCounter.WriteLine(string.Format("{0:43}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.ItemOpNm[i].Trim(), 43)));
                                break;
                        }
                    }
                    else
                    {
                        if (StoreInfo.CounterType.CompareTo("2") == 0)
                            strTab = "\t";
                        else
                            strTab = "\t";

                        serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                        serialPortCounter.WriteLine(string.Format("{0:20}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " +
                            OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                        //if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                        //{
                        //    serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        //    serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                        //    if (StoreInfo.CounterType.CompareTo("2") == 0)
                        //    {
                        //        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                        //        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                        //    }
                        //    else
                        //    {
                        //        serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                        //        serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                        //    }
                        //}

                        //if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == string.Empty)
                        //{
                        //    serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        //    serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                        //    if (StoreInfo.CounterType.CompareTo("2") == 0)
                        //        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                        //    else
                        //        serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                        //}

                        //if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != string.Empty)
                        //{
                        //    serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        //    serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                        //    if (StoreInfo.CounterType.CompareTo("2") == 0)
                        //        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                        //    else
                        //        serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                        //}
                    }

                    //if (prnRemark[i] != string.Empty)
                    //{
                    //    string[] temp = prnRemark[i].Split(Convert.ToChar(0xA));

                    //    serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                    //    serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                    //    serialPortCounter.Write("> (");
                    //    for (int j = 0; j < temp.Length; j++)
                    //    {
                    //        serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                    //        serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                    //        serialPortCounter.Write(temp[j]);
                    //        if (temp.Length != i)
                    //            serialPortCounter.Write(" ");
                    //    }
                    //    serialPortCounter.WriteLine(")");
                    //}

                    if (i == OrderListInfo.ItemName.Count - 1)
                    {
                        serialPortCounter.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                        serialPortCounter.Write(Convert.ToChar(0X1B) + "a" + Convert.ToChar(1));
                        serialPortCounter.WriteLine(UtilHelper.HyphepMake("=", 42));
                    }
                    else
                    {
                        serialPortCounter.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                        serialPortCounter.Write(Convert.ToChar(0X1B) + "a" + Convert.ToChar(1));
                        serialPortCounter.WriteLine(UtilHelper.HyphepMake("-", 42));
                    }
                }

                serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortCounter.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0 + 1));
                serialPortCounter.WriteLine("");
                serialPortCounter.WriteLine("키오스크 : [ 주문번호 : " + string.Format("{0:D3} ]\r\n", Convert.ToInt64(turnNum)));

                if (StoreInfo.IsTakeOut)
                    serialPortCounter.WriteLine("<< 포장판매 >> \r\n\r\n\r\n");
                else
                    serialPortCounter.WriteLine("<< 매장식사 >> \r\n\r\n\r\n");

                serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(1 + 1));
                serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortCounter.WriteLine("");
                serialPortCounter.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortCounter.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortCounter.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortCounter.Write(Convert.ToChar(0x1B) + "S");
            }
            catch (Exception e)
            {
                serialPortCounter.Close();
            }
            finally
            {
                serialPortCounter.Close();
            }
        }

        #endregion

        private decimal GetVat()
        {
            return (dTotalAmt.ToDouble() * 0.1).ToDecimal();
        }

        private decimal GetAmt()
        {
            return dTotalAmt - GetVat();
        }
    }
}
