using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.IO;
namespace Kiosk
{
    public partial class FrmCardCancelInfoDaou : Form
    {
        DBControl.SaleDDAO saledDao = new DBControl.SaleDDAO();
        DBControl.SaleHDAO salehDao = new DBControl.SaleHDAO();
        DBControl.VanDataDAO vanDataDao = new DBControl.VanDataDAO();

        SplashCardCancelProc cardCancelProc = new SplashCardCancelProc();
        SplashCardCancelOk cardCancelOk = new SplashCardCancelOk();

        TcpClient client;

        StreamReader reader;
        StreamWriter writer;

        NetworkStream netStream;
        Thread receiveThread;

        bool isConnected;

        string appNo = string.Empty;
        string appDate = string.Empty;
        string appRecNo = string.Empty;
        string appAmt = string.Empty;
        string appMonth = string.Empty;
        string storeFlag = string.Empty;

        byte[] bRecvData = new byte[1024];

        public FrmCardCancelInfoDaou()
        {
            InitializeComponent();
        }

        public FrmCardCancelInfoDaou(string appDate, string appRecNo, string appNo, string appMonth, string appAmt)
        {
            InitializeComponent();
            saledDao.BRAND = salehDao.BRAND = vanDataDao.BRAND = "00003";
            saledDao.STORE = salehDao.STORE = vanDataDao.STORE = StoreInfo.StoreCode;
            saledDao.DESK = salehDao.DESK = vanDataDao.DESK = StoreInfo.StoreDesk;
             
            this.appDate = appDate;
            this.appRecNo = appRecNo;
            this.appNo = appNo;
            this.appMonth = appMonth;
            this.appAmt = appAmt;
        }

        private void FrmCardCancelInfo_Load(object sender, EventArgs e)
        {
            //axShockwaveFlash1.Movie = Directory.GetCurrentDirectory() + @"\System\swf\card_in.swf";
            ThreadPool.QueueUserWorkItem(CardCancelApp, true);
        }

        private void picBoxPrev_MouseDown(object sender, MouseEventArgs e)
        {
            UtilHelper.spWav.Play();

            Bitmap bitmap = UtilHelper.ChangeOpacity(picBoxPrev.BackgroundImage, 1f);
            picBoxPrev.BackgroundImage = null;
            picBoxPrev.BackgroundImage = bitmap;
        }

        private void picBoxPrev_MouseUp(object sender, MouseEventArgs e)
        {
            picBoxPrev.BackgroundImage = null;
            picBoxPrev.BackgroundImage = Properties.Resources.btn_back;
            UtilHelper.Delay(50);

            MouseUp_Proc();
        }

        private void FrmCardCancelInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            isConnected = false;

            if (reader != null)
                reader.Close();

            if (writer != null)
                writer.Close();

            if (client != null)
                client.Close();

            if (receiveThread != null)
                receiveThread.Abort();

            //if (axShockwaveFlash1 != null)
            //{
            //    axShockwaveFlash1.Dispose();
            //    axShockwaveFlash1 = null;
            //}
        }

        private void MouseUp_Proc()
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void CardCancelApp(object sender)
        {
            try
            {
                if (isConnected == true)
                {
                    if (reader != null)
                        reader.Close();

                    if (writer != null)
                        writer.Close();

                    if (client != null)
                        client.Close();

                    if (receiveThread != null)
                        receiveThread.Abort();
                }

                string ip = "127.0.0.1";
                int port = StoreInfo.Port.ToInteger();

                client = new TcpClient();
                client.Connect(ip, port);
                netStream = client.GetStream();

                isConnected = true;

                reader = new StreamReader(netStream, Encoding.Default);
                writer = new StreamWriter(netStream, Encoding.Default);

                StringBuilder req = new StringBuilder();
                req.Clear();

                req = InsertLeftJustify(req, "K100", 4);
                req = InsertLeftJustify(req, StoreInfo.Tid, 8);
                req = InsertLeftJustify(req, appAmt, 12);
                req = InsertLeftJustify(req, "1", 1);

                writer.Write(req.ToString());
                writer.Flush();

                receiveThread = new Thread(new ThreadStart(Receive));
                receiveThread.Start();
            }
            catch (Exception e)
            {
                
            }
        }

        private void Receive()
        {
            FrmErrorBox errBox;

            try
            {
                while (isConnected)
                {
                    Thread.Sleep(1);

                    if (netStream.CanRead)
                    {
                        char[] c = null;
                        c = new char[5000];

                        char[] d = null;
                        d = new char[5000];

                        int adv;
                        adv = reader.Read(c, 0, c.Length);

                        string s = new string(c);
                        string tmpStr = s.Replace("\0", "");
                        string tmpStr1 = string.Empty;

                        if (netStream.DataAvailable)
                        {
                            adv = reader.Read(d, 0, d.Length);

                            string s1 = new string(d);
                            tmpStr1 = s1.Replace("\0", "");
                        }

                        if (tmpStr.Length > 0)
                        {
                            LogMenager.LogWriter(UtilHelper.Root + @"Log\recv\recv", "[RECV]:" + tmpStr + tmpStr1);

                            if ((tmpStr.Substring(0, 4).CompareTo("1000") == 0) || (tmpStr.Substring(0, 4).CompareTo("2000") == 0))
                            {
                                //this.Visible = false;

                                cardCancelProc.Open();

                                bRecvData = Encoding.Default.GetBytes(tmpStr);
                                string appNo = vanDataDao.APPNO = Encoding.Default.GetString(bRecvData, 68, 12).Trim();

                                if (appNo.CompareTo("") != 0)
                                {
                                    if (VanDataInsert(bRecvData))
                                    {
                                        if (SaleD_CancelUpdate())
                                        {
                                            if (SaleH_CancelUpdate())
                                            {
                                                UtilHelper.Delay(500);
                                                cardCancelProc.Close();
                                                ReceiptCancelPrint();
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
                                }
                                else
                                {
                                    cardCancelProc.Close();

                                    string outMsg1 = Encoding.Default.GetString(bRecvData, 170, 67);

                                    errBox = new FrmErrorBox("카드에러", outMsg1);
                                    errBox.TopMost = true;

                                    if (errBox.ShowDialog() == DialogResult.OK)
                                    {
                                        LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + outMsg1);
                                        this.DialogResult = DialogResult.Cancel;
                                    }
                                }
                               
                            }
                            else if (tmpStr.Substring(0, 1).CompareTo("K") == 0)
                            {
                                switch (tmpStr.Substring(0, 4))
                                {
                                    case "K110":
                                        //IC카드 리딩 완료
                                        //결제진행
                                        this.Invoke(new MethodInvoker(delegate ()
                                        {
                                            if (isConnected == true)
                                            {
                                                StringBuilder req = new StringBuilder();
                                                req.Clear();

                                                req = InsertLeftJustify(req, "0400", 4);
                                                req = InsertLeftJustify(req, "10", 2);
                                                req = InsertLeftJustify(req, StoreInfo.Tid, 8);
                                                req = InsertLeftJustify(req, "0000", 4);
                                                req = InsertLeftJustify(req, "0", 14);
                                                req = InsertLeftJustify(req, appMonth, 2);
                                                req = InsertLeftJustify(req, appAmt, 12);
                                                req = InsertLeftJustify(req, "0", 12);
                                                req = InsertLeftJustify(req, "0", 12);
                                                req = InsertLeftJustify(req, "", 12);
                                                req = InsertLeftJustify(req, appDate, 8);
                                                req = InsertLeftJustify(req, appNo, 12);
                                                req = InsertLeftJustify(req, "", 42);

                                                LogMenager.LogWriter(UtilHelper.Root + @"\Log\send\payMentSend", req.ToString());
                                                writer.Write(req.ToString());
                                                writer.Flush();
                                            }
                                        }));
                                        break;

                                    case "K005":
                                        errBox = new FrmErrorBox("카드 상태 확인", "카드 제거 후 다시 시도해주세요");
                                        errBox.TopMost = true;

                                        if (errBox.ShowDialog() == DialogResult.OK)
                                        {
                                            try
                                            {
                                                if (isConnected == true)
                                                {
                                                    if (reader != null)
                                                        reader.Close();

                                                    if (writer != null)
                                                        writer.Close();

                                                    if (client != null)
                                                        client.Close();
                                                }

                                                string ip = "127.0.0.1";
                                                int port = StoreInfo.Port.ToInteger();

                                                client = new TcpClient();
                                                client.Connect(ip, port);
                                                netStream = client.GetStream();

                                                isConnected = true;

                                                reader = new StreamReader(netStream, Encoding.Default);
                                                writer = new StreamWriter(netStream, Encoding.Default);

                                                StringBuilder req = new StringBuilder();
                                                req.Clear();

                                                req = InsertLeftJustify(req, "K100", 4);
                                                req = InsertLeftJustify(req, StoreInfo.Tid, 8);
                                                req = InsertLeftJustify(req, appAmt, 12);
                                                req = InsertLeftJustify(req, "1", 1);

                                                writer.Write(req.ToString());
                                                writer.Flush();
                                            }
                                            catch (Exception e)
                                            { }
                                        }
                                        break;

                                    case "K990":
                                        UtilHelper.Delay(200);
                                        string outMsg = Message(tmpStr);

                                        LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + outMsg);
                                        this.DialogResult = DialogResult.Cancel;
                                        break;

                                    default:
                                        string outMsg1 = Message(tmpStr);

                                        errBox = new FrmErrorBox("카드에러", outMsg1);
                                        errBox.TopMost = true;

                                        if (errBox.ShowDialog() == DialogResult.OK)
                                        {
                                            LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + outMsg1);
                                            this.DialogResult = DialogResult.Cancel;
                                        }
                                        break;
                                }
                            }
                            else if (tmpStr.Substring(0, 1).CompareTo("S") == 0)
                            {
                                StringBuilder req = new StringBuilder();
                                req.Clear();

                                req = InsertLeftJustify(req, "0400", 4);
                                req = InsertLeftJustify(req, "10", 2);
                                req = InsertLeftJustify(req, StoreInfo.Tid, 8);
                                req = InsertLeftJustify(req, "0000", 4);
                                req = InsertLeftJustify(req, "0", 14);
                                req = InsertLeftJustify(req, appMonth, 2);
                                req = InsertLeftJustify(req, appAmt, 12);
                                req = InsertLeftJustify(req, "0", 12);
                                req = InsertLeftJustify(req, "0", 12);
                                req = InsertLeftJustify(req, "", 12);
                                req = InsertLeftJustify(req, appDate, 8);
                                req = InsertLeftJustify(req, appNo, 12);
                                req = InsertLeftJustify(req, "", 42);

                                LogMenager.LogWriter(UtilHelper.Root + @"\Log\send\payMentSend", req.ToString());
                                writer.Write(req.ToString());
                                writer.Flush();
                            }
                            else if (tmpStr.Substring(0, 4).CompareTo("0000") == 0)
                            {
                                if (tmpStr.Substring(4, 1).CompareTo("C") == 0)
                                {
                                    string outMsg = Message(tmpStr);
                                    errBox = new FrmErrorBox("카드에러", outMsg);
                                    errBox.TopMost = true;

                                    if (errBox.ShowDialog() == DialogResult.OK)
                                    {
                                        LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + outMsg);
                                        this.DialogResult = DialogResult.Cancel;
                                    }
                                }
                            }
                            else if (tmpStr.Substring(0, 4).CompareTo("0046") == 0)
                            {
                                LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + "사용자 카드입력 타임아웃");
                                this.DialogResult = DialogResult.Cancel;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public StringBuilder InsertLeftJustify(StringBuilder target, string item, int maxLen)
        {
            int myLen = maxLen;

            if (item.Length < maxLen)
            {
                target.Append(item);
                myLen = myLen - item.Length;

                for (int i = 0; i < myLen; i++)
                    target.Append(" ");

                return target;
            }
            else if (item.Length == maxLen)
            {
                target.Append(item);

                return target;
            }
            else
            {
                for (int i = 0; i < myLen; i++)
                    target.Append(item[i]);

                return target;
            }
        }

        private bool VanDataInsert(byte[] recvData)
        {
            byte[] bRecvData = recvData;

            vanDataDao.VANGB = StoreInfo.VanSelect;
            vanDataDao.DATE = StoreInfo.StoreOpen;
            vanDataDao.MOMTH = string.Format("{0:D2}", appMonth.ToInteger());  //할부
            vanDataDao.RECENO = appRecNo;
            vanDataDao.APPAMT = appAmt.ToDecimal();
            vanDataDao.TAX = 0;
            vanDataDao.BONG = 0;
            vanDataDao.VALID = Encoding.Default.GetString(bRecvData, 0, 4);
            vanDataDao.GUBN = Encoding.Default.GetString(bRecvData, 4, 4);
            vanDataDao.TYPE = "30"; //Encoding.Default.GetString(bRecvData, 8, 2);
            vanDataDao.APPDATE = Encoding.Default.GetString(bRecvData, 42, 8);
            vanDataDao.APPTIME = Encoding.Default.GetString(bRecvData, 50, 6);
            vanDataDao.VANKEY = Encoding.Default.GetString(bRecvData, 56, 12);
            vanDataDao.APPNO = Encoding.Default.GetString(bRecvData, 68, 12);
            vanDataDao.ORDCD = Encoding.Default.GetString(bRecvData, 80, 3);
            vanDataDao.ORDNM = Encoding.Default.GetString(bRecvData, 83, 16);
            vanDataDao.INPCD = Encoding.Default.GetString(bRecvData, 99, 4);
            vanDataDao.INPNM = Encoding.Default.GetString(bRecvData, 103, 16);
            vanDataDao.STOCD = Encoding.Default.GetString(bRecvData, 119, 15);  
            vanDataDao.CDTYPE = Encoding.Default.GetString(bRecvData, 134, 1);
            vanDataDao.CARDNO = Encoding.Default.GetString(bRecvData, 135, 23);
            vanDataDao.MSG = Encoding.Default.GetString(bRecvData, 170, 67);
            vanDataDao.CATID = Encoding.Default.GetString(bRecvData, 10, 8);
            vanDataDao.REMARK = "C";

            if (vanDataDao.VanDataSave())
                return vanDataDao.VanDataSaveL();

            return false;
        }

        private void ReceiptCancelPrint()
        {
            cardCancelOk = new SplashCardCancelOk();
            cardCancelOk.Open();

            try
            {
                serialPortReceipt.PortName = StoreInfo.ReceiptPrn;
                serialPortReceipt.BaudRate = StoreInfo.ReceiptRate;
                serialPortReceipt.DataBits = 8;
                serialPortReceipt.Parity = System.IO.Ports.Parity.None;
                serialPortReceipt.StopBits = System.IO.Ports.StopBits.One;
                serialPortReceipt.Encoding = Encoding.Default;

                string strDate = string.Format("{0:D4}.{1:D2}.{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                string strTime = string.Format("{0:D2}:{1:D2}:{2:D2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                if (serialPortReceipt.IsOpen == false)
                    serialPortReceipt.Open();

                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(0, 1));
                serialPortReceipt.Write(StoreInfo.StoreName + "\r\n");
                serialPortReceipt.WriteLine("*** 영 수 증(취소) ***\r\n");
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(0, 0));
                serialPortReceipt.WriteLine(string.Format("{0:-39}", "[" + StoreInfo.StoreName + "]"));

                byte[] bTmpApp = Encoding.Default.GetBytes(StoreInfo.StoreAdd2);

                if (bTmpApp.Length > 20)
                {
                    serialPortReceipt.WriteLine(string.Format("{0,-35}", StoreInfo.StoreAdd1));
                    serialPortReceipt.WriteLine(string.Format("{0,-36}", StoreInfo.StoreAdd2));
                }
                else
                {
                    serialPortReceipt.WriteLine(string.Format("{0, -35}", StoreInfo.StoreAdd1 + " " + StoreInfo.StoreAdd2));
                }
                serialPortReceipt.WriteLine(string.Format("{0,-6}{1,10}{2,23}", StoreInfo.StoreCeo,
                   "[" + StoreInfo.StoreSano + "]", "T:" + StoreInfo.StorePhon));
                serialPortReceipt.WriteLine(string.Format("{0,-19}{1,26}", strDate + " " + strTime, "NO:K" +
                    string.Format("{0:D2}-", Convert.ToInt32(StoreInfo.StoreDesk)) + appRecNo));

                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(16, 0));
                serialPortReceipt.WriteLine(string.Format("총매출액{0,15:##,##0}", Convert.ToDecimal(appAmt)));
                serialPortReceipt.WriteLine(string.Format("합계금액{0,15:##,##0}", Convert.ToDecimal(appAmt)));
                serialPortReceipt.WriteLine(string.Format("취소금액{0,15:##,##0}", Convert.ToDecimal(appAmt)));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(0, 0));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortReceipt.WriteLine(string.Format("                [카드]{0,24:##,##0}", Convert.ToDecimal(appAmt)));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.WriteLine("**" + vanDataDao.ORDNM.Trim() + "**");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 카드번호 ]", string.Format("{0}", vanDataDao.CARDNO.Trim() + "**********")));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 취소시간 ]", strDate + " " + strTime));
                serialPortReceipt.WriteLine(string.Format("{0}{1, 31}", "[ 승인금액 ]", string.Format("{0:##,##0}", Convert.ToDecimal(appAmt)) + "[ 일시불 ]"));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 승인번호 ]", vanDataDao.APPNO.Trim()));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                serialPortReceipt.WriteLine("** " + StoreFlagEx() + " **");

                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46) + "\r\n\r\n");
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(1, 1));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
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
                cardCancelOk.Close();
            }
            finally
            {
                serialPortReceipt.Close();
                cardCancelOk.Close();
            }
        }

        private string StoreFlagEx()
        {
            string rtnVal = string.Empty;

            using (DataTable dt = salehDao.GetStoreFlag())
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                            rtnVal = dtr["SH_MEMO"].ToString();
                    }
                }
            }

            return rtnVal;
        }

        private bool SaleD_CancelUpdate()
        {
            saledDao.RECENO = appRecNo;
            saledDao.DATE = StoreInfo.StoreOpen;
            saledDao.GUBN = "취소";

            if (saledDao.Saled_Update())
                return saledDao.Saled_UpdateL();

            return false;
        }

        private bool SaleH_CancelUpdate()
        {
            salehDao.RECENO = appRecNo;
            salehDao.DATE = StoreInfo.StoreOpen;
            salehDao.GUBN = "취소";

            if (salehDao.SaleH_Update())
                return salehDao.SaleH_UpdateL();

            return false;
        }

        #region 에러메시지
        public string Message(string input)
        {
            string output = string.Empty;

            switch (input)
            {
                case "K001":
                    output = "[" + input + "]" + "리더기 에러" + "\r\n";
                    break;
                case "K002":
                    output = "[" + input + "]" + "IC 카드 거래 불가" + "\r\n";
                    break;
                case "K003":
                    output = "[" + input + "]" + "M/S카드 거래 불가" + "\r\n";
                    break;
                case "K004":
                    output = "[" + input + "]" + "마그네틱 카드를 읽혀주세요(FALL-BACK)" + "\r\n";
                    break;
                case "K005":
                    output = "[" + input + "]" + "IC카드가 삽입되어 있습니다. 제거해주세요." + "\r\n";
                    break;
                case "K006":
                    output = "[" + input + "]" + "카드거절" + "\r\n";
                    break;
                case "K007":
                    output = "[" + input + "]" + "결제도중 카드를 제거하였습니다." + "\r\n";
                    break;
                case "K008":
                    output = "[" + input + "]" + "결제도중 카드를 제거하였습니다." + "\r\n";
                    break;
                case "K011":
                    output = "[" + input + "]" + "IC리더기 키 다운로드 요망" + "\r\n";
                    break;
                case "K012":
                    output = "[" + input + "]" + "리더기 템퍼 오류" + "\r\n";
                    break;
                case "K013":
                    output = "[" + input + "]" + "상호인증오류" + "\r\n";
                    break;
                case "K014":
                    output = "[" + input + "]" + "암/복호와 오류" + "\r\n";
                    break;
                case "K015":
                    output = "[" + input + "]" + "무결성 검사 실패(다우데이타 관리자에게 문의 하시기 바랍니다.)" + "\r\n";
                    break;
                case "K101":
                    output = "[" + input + "]" + "커맨드 전달 실패" + "\r\n";
                    break;
                case "K102":
                    output = "[" + input + "]" + "키트로닉스 모듈 타임아웃" + "\r\n";
                    break;
                case "K901":
                    output = "[" + input + "]" + "카드사 파라미터 데이터 오류" + "\r\n";
                    break;
                case "K902":
                    output = "[" + input + "]" + "카드사 파라미터 반영불가" + "\r\n";
                    break;
                case "K990":
                    output = "[" + input + "]" + "사용자 강제종료" + "\r\n";
                    break;
                case "K997":
                    output = "[" + input + "]" + "리더기에서 전달받은 데이터이상" + "\r\n";
                    break;
                case "K998":
                    output = "[" + input + "]" + "리더기 타임아웃" + "\r\n";
                    break;
                case "K999":
                    output = "[" + input + "]" + "전문 오류" + "\r\n";
                    break;
                case "K081":
                    output = "[" + input + "]" + "IC카드를 넣어주세요" + "\r\n";
                    break;
                case "K082":
                    output = "[" + input + "]" + "처리불가 상태" + "\r\n";
                    break;
                case "K083":
                    output = "[" + input + "]" + "카드 입력 취소" + "\r\n";
                    break;
                case "S000":
                    output = "[" + input + "]" + "외부 서명 데이터 입력" + "\r\n";
                    break;
                case "S001":
                    output = "[" + input + "]" + "외부 서명 데이터 입력 실패 ( Timou Out )" + "\r\n";
                    break;
                case "S100":
                    output = "[" + input + "]" + "외부 서명 데이터 입력 성공" + "\r\n";
                    break;
                case "S111":
                    output = "[" + input + "]" + "외부 서명 데이터 입력 취소" + "\r\n";
                    break;
                case "S052":
                    output = "[" + input + "]" + "신용 카드 거래 취소" + "\r\n";
                    break;
                case "S053":
                    output = "[" + input + "]" + "현금 영수증 거래 취소" + "\r\n";
                    break;
                case "S054":
                    output = "[" + input + "]" + "현금 IC 거래 취소" + "\r\n";
                    break;
                case "K110":
                    output = "[" + input + "]" + "KIOSK 카드 리딩 완료" + "\r\n";
                    break;
                default:
                    output = "[" + input + "]" + "없는로그" + "\r\n";
                    break;
            }

            if (input.Length > 8)
            {
                if (input.Substring(4, 4) == "C010")
                {
                    string ErrCode = input.Substring(0, 4);     // 에러 코드

                    string ErrCnt = input.Substring(8, 2);      // 에러 갯수              00 : 정상 , 1 이상 에러 발생 갯수
                    string DllCode = input.Substring(10, 2);     // DLL 에러               00 정상, 01 에러
                    string NetCode = input.Substring(12, 2);     // 네트워크 상태          00 정상, 01 에러
                    string AppCode = input.Substring(14, 2);     // 클라이언트 연결 상태  00 정상, 01 에러
                    string ReaderCode = input.Substring(16, 2); // 리더기 상태            00 정상, 01 에러
                    string SignCode = input.Substring(18, 2);   // 서명패드 상태          00 정상, 01 에러
                    string IniCode = input.Substring(20, 2);    // INI 파일 상태          00 정상, 01 에러

                    string strResultStat = "에러: " + ErrCnt;

                    if (DllCode == "00")
                    {
                        strResultStat += " DLL 상태: OK" + "\n" + "\r";
                    }
                    else
                    {
                        strResultStat += " DLL 상태: FAIL" + "\n" + "\r";
                    }

                    if (NetCode == "00")
                    {
                        strResultStat += " 네트워크 상태: OK" + "\n" + "\r";
                    }
                    else
                    {
                        strResultStat += " 네트워크 상태: FAIL" + "\n" + "\r";
                    }



                    if (AppCode == "00")
                    {
                        strResultStat += " 어플리케이션 상태: OK" + "\n";
                    }
                    else
                    {
                        strResultStat += " 어플리케이션 상태: FAIL" + "\n";
                    }

                    if (ReaderCode == "00")
                    {
                        strResultStat += " 리더기 상태: OK" + "\n";
                    }
                    else
                    {
                        strResultStat += " 리더기 상태: FAIL" + "\n";
                    }

                    if (SignCode == "00")
                    {
                        strResultStat += " 서명패드 상태: OK" + "\n";
                    }
                    else
                    {
                        strResultStat += " 서명패드 상태: FAIL" + "\n";
                    }

                    if (IniCode == "00")
                    {
                        strResultStat += " INI 상태: OK" + "\n" + "\r";
                    }
                    else
                    {
                        strResultStat += " INI 상태: FAIL" + "\n" + "\r";
                    }

                    output = strResultStat;
                }
            }

            return output;
        }
        #endregion
    }
}
