using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;

namespace Kiosk
{
    public partial class FrmCardPayMentDaou : Form
    {
        TcpClient client;

        StreamReader reader;
        StreamWriter writer;

        NetworkStream netStream;
        Thread receiveThread;

        bool isConnected;

        //private Encoding Encod = Encoding.GetEncoding("ks_c_5601-1987");

        private SplashPayMentProc splashPayMentProc;
        private SplashPayMentComple splashPayMentComple;

        private DBControl.SaleDDAO saledDao = new DBControl.SaleDDAO();
        private DBControl.SaleHDAO salehDao = new DBControl.SaleHDAO();
        private DBControl.VanDataDAO vanDataDao = new DBControl.VanDataDAO();

        string month;
        string receNo = string.Empty;
        string turnNum = string.Empty;
        string sTime = string.Empty;
        string logMsg = string.Empty;
        string errMsg = string.Empty;
        string cardMsg = string.Empty;
        decimal dTotalAmt;

        byte[] bRecvData = new byte[1024];
        char FS = Convert.ToChar(0x1C);

        List<string> prnRemark = new List<string>();
        List<decimal> danga = new List<decimal>();

        Bitmap bgBitmap;

        public FrmCardPayMentDaou()
        {
            InitializeComponent();
        }

        public FrmCardPayMentDaou(Bitmap bitmap, List<decimal> danga, string totalAmt, string month, string msg)
        {
            InitializeComponent();
            bgBitmap = bitmap;
            this.BackgroundImage = bgBitmap;
            this.danga = danga;
            this.dTotalAmt = totalAmt.ToDecimal();
            this.month = month;

            saledDao.BRAND = salehDao.BRAND = vanDataDao.BRAND = StoreInfo.BrnadCode;
            saledDao.STORE = salehDao.STORE = vanDataDao.STORE = StoreInfo.StoreCode;
            saledDao.DESK = salehDao.DESK = vanDataDao.DESK = StoreInfo.StoreDesk;
            saledDao.DATE = salehDao.DATE = vanDataDao.DATE = StoreInfo.StoreOpen;
        }

        private void FrmCardPayMentDaou_Load(object sender, EventArgs e)
        {
            woniPanel1.Left = (this.ClientSize.Width - woniPanel1.Width) / 2;
            woniPanel1.Top = (this.ClientSize.Height - woniPanel1.Height) / 2;

            //axShockwaveFlash1.Movie = System.IO.Directory.GetCurrentDirectory() + @"\System\swf\card_in.swf";
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
            //UtilHelper.cardInfo.controls.stop();

            this.DialogResult = DialogResult.Cancel;
        }

        private void CardApp(object sender)
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

                //UtilHelper.cardInfo.URL = @"System\wav\card.mp3";
                //UtilHelper.cardInfo.controls.play();

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
                req = InsertLeftJustify(req, dTotalAmt.ToString(), 12);
                req = InsertLeftJustify(req, "1", 1);

                writer.Write(req.ToString());
                writer.Flush();

                UtilHelper.Delay(50);

                receiveThread = new Thread(new ThreadStart(Receive));
                receiveThread.Start();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        private void FrmCardPayMentDaou_FormClosing(object sender, FormClosingEventArgs e)
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
                            LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + tmpStr + tmpStr1);

                            if ((tmpStr.Substring(0, 4).CompareTo("1000") == 0) || (tmpStr.Substring(0, 4).CompareTo("2000") == 0))
                            {
                                bRecvData = Encoding.Default.GetBytes(tmpStr);
                                string appNo = Encoding.Default.GetString(bRecvData, 68, 12).Trim();

                                if (appNo.CompareTo("") != 0)
                                {
                                    if (VanDataInsert(bRecvData))
                                    {
                                        if (SaleDInsert())
                                        {
                                            if (SaleHInsert())
                                            {
                                                UtilHelper.Delay(1000);
                                                splashPayMentProc.Close();
                                                OrderListPrintKitchen();
                                                this.DialogResult = DialogResult.OK;
                                            }
                                            else
                                            {
                                                //SaleHInsert 실패
                                            }
                                        }
                                        else
                                        {
                                            //SaleDInsert 실패
                                        }

                                    }
                                    else
                                    {
                                        //VanDataInsert 실패
                                    }
                                }
                                else
                                {
                                    splashPayMentProc.Close();

                                    string outMsg1 = Encoding.Default.GetString(bRecvData, 170, 67);

                                    errBox = new FrmErrorBox("카드에러", outMsg1);
                                    errBox.TopMost = true;
                                    //UtilHelper.cardEnd.controls.stop();

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
                                        //결재진행
                                        this.Invoke(new MethodInvoker(delegate ()
                                        {
                                            //this.Visible = false;

                                            splashPayMentProc = new SplashPayMentProc(this, bgBitmap, 0);
                                            splashPayMentProc.Open();

                                            if (isConnected == true)
                                            {
                                                StringBuilder req = new StringBuilder();
                                                req.Clear();

                                                req = InsertLeftJustify(req, "0100", 4);
                                                req = InsertLeftJustify(req, "10", 2);
                                                req = InsertLeftJustify(req, StoreInfo.Tid, 8);                                    //단말기 번호
                                                req = InsertLeftJustify(req, "0000", 4);
                                                req = InsertLeftJustify(req, "0", 14);
                                                req = InsertLeftJustify(req, month, 2);                                          //할부
                                                req = InsertLeftJustify(req, dTotalAmt.ToString(), 12);                         //거래금액
                                                req = InsertLeftJustify(req, "0", 12);                                          //봉사료
                                                req = InsertLeftJustify(req, string.Format("{0}", 
                                                    GetVat(GetSupply(dTotalAmt.ToDouble()))), 12);                                          //세금
                                                req = InsertLeftJustify(req, "", 12);                                           //비과세
                                                req = InsertLeftJustify(req, DateTime.Now.ToString("yyyyMMdd"), 8);             //원승인일자
                                                req = InsertLeftJustify(req, "", 12);                                           //원승인번호
                                                req = InsertLeftJustify(req, "", 42);                                           //가맹점데이타

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
                                                req = InsertLeftJustify(req, dTotalAmt.ToString(), 12);
                                                req = InsertLeftJustify(req, "1", 1);

                                                writer.Write(req.ToString());
                                                writer.Flush();

                                            }
                                            catch (Exception e)
                                            {
                                            }
                                        }
                                        break;

                                    case "K990":
                                        UtilHelper.Delay(200);
                                        string outMsg = Message(tmpStr);

                                        LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + outMsg);
                                        //UtilHelper.cardInfo.controls.stop();
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
                                splashPayMentProc = new SplashPayMentProc(this, bgBitmap, 0);
                                splashPayMentProc.Open();

                                if (isConnected == true)
                                {
                                    StringBuilder req = new StringBuilder();
                                    req.Clear();

                                    req = InsertLeftJustify(req, "0100", 4);
                                    req = InsertLeftJustify(req, "10", 2);
                                    req = InsertLeftJustify(req, StoreInfo.Tid, 8);                                    //단말기 번호
                                    req = InsertLeftJustify(req, "0000", 4);
                                    req = InsertLeftJustify(req, "0", 14);
                                    req = InsertLeftJustify(req, month, 2);                                          //할부
                                    req = InsertLeftJustify(req, dTotalAmt.ToString(), 12);                         //거래금액
                                    req = InsertLeftJustify(req, "0", 12);                                          //봉사료
                                    req = InsertLeftJustify(req,
                                        string.Format("{0}", GetVat(GetSupply(dTotalAmt.ToDouble()))), 12);                                          //세금
                                    req = InsertLeftJustify(req, "", 12);                                           //비과세
                                    req = InsertLeftJustify(req, DateTime.Now.ToString("yyyyMMdd"), 8);             //원승인일자
                                    req = InsertLeftJustify(req, "", 12);                                           //원승인번호
                                    req = InsertLeftJustify(req, "", 42);                                           //가맹점데이타

                                    LogMenager.LogWriter(UtilHelper.Root + @"\Log\send\payMentSend", req.ToString());
                                    writer.Write(req.ToString());
                                    writer.Flush();
                                }
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
                                        //Invoke(AddLog, "[RECV]: " + outMsg);
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
                            else if (tmpStr.Substring(0, 4).CompareTo("0004") == 0)
                            {
                                if (splashPayMentProc != null)
                                {
                                    splashPayMentProc.Close();
                                    splashPayMentProc.Join();
                                }

                                LogMenager.LogWriter(UtilHelper.Root + @"\Log\recv\recv", "[RECV]:" + "사용자 사인입력 취소");

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

        private bool SaleDInsert()
        {
            saledDao.TIME = vanDataDao.APPTIME;

            for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
            {
                OrderListInfo.ItemOptAmt1[i] = OrderListInfo.ItemOptAmt1[i] * OrderListInfo.ItemQuan[i];
                OrderListInfo.ItemOptAmt2[i] = OrderListInfo.ItemOptAmt2[i] * OrderListInfo.ItemQuan[i];
   
                saledDao.RECENO = receNo;
                saledDao.GNAM = OrderListInfo.ItemGNam[i];
                saledDao.PNAM = OrderListInfo.ItemName[i] + " " + OrderListInfo.ItemOpNm[i];
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
            salehDao.TMON = GetVat(GetSupply(Convert.ToDouble(dTotalAmt))).ToDecimal();
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

        private bool VanDataInsert(byte[] recvData)
        {
            try
            {
                byte[] bRecvData = recvData;

                if (saledDao.DESK.CompareTo("1") == 0)
                    UtilHelper.Delay(30);
                if (saledDao.DESK.CompareTo("2") == 0)
                    UtilHelper.Delay(100);

                saledDao.SalesNumMaker();
                saledDao.TurnIndexMaker();

                receNo = saledDao.GetSalesNum();
                turnNum = saledDao.GetTurnNum();

                vanDataDao.VANGB = StoreInfo.VanSelect;
                vanDataDao.DATE = StoreInfo.StoreOpen;
                vanDataDao.MOMTH = string.Format("{0:D2}", month.ToInteger());  //할부
                vanDataDao.RECENO = receNo;
                vanDataDao.APPAMT = dTotalAmt;
                vanDataDao.TAX = GetVat(GetSupply(Convert.ToDouble(dTotalAmt))).ToDecimal();
                vanDataDao.BONG = 0;
                vanDataDao.VALID = Encoding.Default.GetString(bRecvData, 0, 4);
                vanDataDao.GUBN = Encoding.Default.GetString(bRecvData, 4, 4);
                vanDataDao.TYPE = "10";//Encoding.Default.GetString(bRecvData, 8, 2);
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
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                return false;
            }
        }
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
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(1, 1));
                //serialPortReceipt.WriteLine(StoreInfo.StoreName + "\r\n");

                serialPortReceipt.WriteLine("*** 영 수 증 ***\r\n");

                //serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(1, 1));
                serialPortReceipt.WriteLine(string.Format("{0:-39}", "[" + StoreInfo.StoreName + "]"));

                byte[] bTmpAdd = encod.GetBytes(StoreInfo.StoreAdd2);

                if (bTmpAdd.Length > 20)
                {
                    //serialPortReceipt.WriteLine(string.Format("{0, -35}", StoreInfo.StoreAdd1));
                    //serialPortReceipt.WriteLine(string.Format("{0, -36}", StoreInfo.StoreAdd2));
                    serialPortReceipt.WriteLine(string.Format("{0, -35}", StoreInfo.StoreAdd1));
                    serialPortReceipt.WriteLine(string.Format("{0, -36}", StoreInfo.StoreAdd2));
                }
                else
                {
                    //serialPortReceipt.WriteLine(string.Format("{0, -35}", StoreInfo.StoreAdd1 + " " + StoreInfo.StoreAdd2));
                    serialPortReceipt.WriteLine(string.Format("{0, -35}", StoreInfo.StoreAdd1 + " " + StoreInfo.StoreAdd2));
                }

                //serialPortReceipt.WriteLine(string.Format("{0, -6}{1, 10}{2, 23}", StoreInfo.StoreCeo, "[" + StoreInfo.StoreSano + "]",
                //    "T:" + StoreInfo.StorePhon));
                serialPortReceipt.WriteLine(string.Format("{0, -6}{1, 10}{2, 23}", StoreInfo.StoreCeo, "[" + StoreInfo.StoreSano + "]",
                    "T:" + StoreInfo.StorePhon));
                //serialPortReceipt.WriteLine(string.Format("Kiosk{0:D2}-{1}", StoreInfo.StoreDesk.ToInteger(), receNo));
                serialPortReceipt.WriteLine(string.Format("{0,-19}{1,26}", strDate + " " + strTime, "NO:K" +
                    string.Format("{0:D2}-", StoreInfo.StoreDesk.ToInteger()) + receNo));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortReceipt.WriteLine(string.Format("{0, -16}{1, 12}{2, 5}{3, 5}", "  품목", "       단가", "수량", "금액"));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));

                for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
                {
                    byte[] bTmpName = encod.GetBytes(OrderListInfo.ItemName[i] + OrderListInfo.ItemOpNm[i]);

                    if (bTmpName.Length > 22)
                    {
                        serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                        serialPortReceipt.WriteLine(string.Format("{0:46}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i], 46)));
                        serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                        bOpnm1 = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                        if (bOpnm1.Length < 4)
                            strTab1 = "\t\t";
                        else
                            strTab1 = "\t";

                        serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2, 8}{3, 4}{4, 10}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemOpNm[i], 20), strTab1,
                            string.Format("{0:##,##0}", (OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i]) / OrderListInfo.ItemQuan[i]),
                            string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                            string.Format("{0:##,##0}", OrderListInfo.ItemAmt[i] - OrderListInfo.ItemOptAmt1[i] - OrderListInfo.ItemOptAmt2[i])));

                        if (OrderListInfo.Memo1[i].Trim().Length == 0 && OrderListInfo.Memo2[i].Trim().Length == 0)
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

                            //bOpnm1 = encod.GetBytes("└" + OrderListInfo.Memo1[i]);
                            //bOpnm2 = encod.GetBytes("└" + OrderListInfo.Memo2[i]);

                            if (bOpnm1.Length >= 24)
                                strTab1 = "";
                            else
                                strTab1 = "\t";

                            //if (bOpnm2.Length >= 24)
                            //    strTab2 = "";
                            //else
                            //    strTab2 = "\t";

                            //serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2,8}{3,4}{4,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 20), strTab1,
                            //    string.Format("{0:##,##0}", opDanga1),
                            //    string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                            //    string.Format("{0:##,##0}", OrderListInfo.ItemOptAmt1[i])));

                            //serialPortReceipt.WriteLine(string.Format("{0:20}{1}{2,8}{3,4}{4,10}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 20), strTab2,
                            //    string.Format("{0:##,##0}", opDanga2),
                            //    string.Format("{0:##,##0}", OrderListInfo.ItemQuan[i]),
                            //    string.Format("{0:##,##0}", OrderListInfo.ItemOptAmt2[i])));
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
                //serialPortReceipt.WriteLine(string.Format("                          과세금액{0,12:##,##0}", GetAmt()));
                //serialPortReceipt.WriteLine(string.Format("                          부 가 세{0,12:##,##0}", GetVat()));
                serialPortReceipt.WriteLine(string.Format("                          과세금액{0,12:##,##0}", GetSupply(dTotalAmt.ToDouble())));
                serialPortReceipt.WriteLine(string.Format("                          부 가 세{0,12:##,##0}", GetVat(GetSupply(dTotalAmt.ToDouble()))));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));

                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                //serialPortReceipt.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(16 + 0));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(2, 0));
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
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));

                serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 카드번호 ]", string.Format("{0}", vanDataDao.CARDNO.Trim())));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 승인시간 ]", strDate + " " + strTime));
                serialPortReceipt.WriteLine(string.Format("{0}{1, 31}", "[ 승인금액 ]", string.Format("{0:##,##0}", dTotalAmt) + "[ 일시불 ]"));
                serialPortReceipt.WriteLine(string.Format("{0, -10}{1, 32}", "[ 승인번호 ]", vanDataDao.APPNO.Trim()));
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));

                if (StoreInfo.IsTakeOut)
                    serialPortReceipt.WriteLine("** 포장 판매 **");
                else
                    serialPortReceipt.WriteLine("** 매장 식사 **");

                /*serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "1.닭고기 원산지");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "  치킨(마리 & 순살) (국내산)");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "  떡(쌀) (외국산)");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "  싸이패티 (브라질산)");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "  바베큐치킨패티 (국내산)");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "  할라피뇨너겟 (국내산");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "2.불고기패티(우육:호주산/돈육,계육:국내산)");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "3.통새우패티 (외국산)");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "4.팝콘만두 (계육:국내산/돈육:국내산)");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "5.슬라이스햄 (돈육 : 외국산, 국내산 혼용)");
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake(" ", 4) + "6.새우후라이드 (베트남산)");

                serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));*/
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
                serialPortReceipt.Encoding = System.Text.Encoding.Default;

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
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(8, 8));
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
                buff = encod.GetBytes(UtilHelper.HyphepMake("=", 46) + "\r\n");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes("주문시간 : ");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(strDate + " ");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(strTime + " Kiosk : " + StoreInfo.StoreDesk + "\r\n");
                stream.Write(buff, 0, buff.Length);
                buff = encod.GetBytes(UtilHelper.HyphepMake("=", 46) + "\r\n");
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

                    if (prnRemark[i] != string.Empty)
                    {
                        string[] temp = prnRemark[i].Split(Convert.ToChar(0xA));

                        buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                        stream.Write(buff, 0, buff.Length);

                        buff = encod.GetBytes("  > (");
                        stream.Write(buff, 0, buff.Length);

                        for (int j = 0; j < temp.Length; j++)
                        {
                            buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            stream.Write(buff, 0, buff.Length);
                            buff = encod.GetBytes(temp[j]);
                            stream.Write(buff, 0, buff.Length);

                            if (temp.Length != i)
                            {
                                buff = encod.GetBytes(" ");
                                stream.Write(buff, 0, buff.Length);
                            }
                        }
                        buff = encod.GetBytes(")\r\n");
                        stream.Write(buff, 0, buff.Length);
                    }

                    if (i == OrderListInfo.ItemName.Count - 1)
                    {
                        buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(UtilHelper.HyphepMake("=", 46) + "\r\n");
                        stream.Write(buff, 0, buff.Length);
                    }
                    else
                    {
                        buff = encod.GetBytes(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                        stream.Write(buff, 0, buff.Length);
                        buff = encod.GetBytes(UtilHelper.HyphepMake("-", 46) + "\r\n");
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

            Encoding encod = Encoding.Default;
            string strTab;
            byte[] bOpnm;

            if (StoreInfo.Kitchen1Prn.CompareTo("사용안함") == 0)
            {
                if (StoreInfo.KitchenPrnIP1.CompareTo("...") != 0)
                {
                    
                    if (StoreInfo.KitchenPrnIP2.CompareTo("...") != 0)
                        IpPrint("K1");
                    IpPrint("K");

                    CounterPrint();
                    ReceiptPrint();
                    TurnNumPrint();

                    splashPayMentComple.Close();
                    return;
                }
                else if (StoreInfo.KitchenPrnIP2.CompareTo("...") != 0)
                {
                    IpPrint("K1");
                    CounterPrint();
                    ReceiptPrint();
                    TurnNumPrint();

                    splashPayMentComple.Close();
                    return;
                }
            }
            try
            {
                serialPortKitchen.PortName = StoreInfo.Kitchen1Prn;
                serialPortKitchen.BaudRate = StoreInfo.Kitchen1Rate;
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

                    if (StoreInfo.Kitchen1Rate.CompareTo("9600") == 0)
                    {
                        if (bTmpName.Length > 20)
                        {
                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i], 40)));

                            bOpnm = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                            if (StoreInfo.Kitchen1Type.CompareTo("2") == 0)
                                strTab = "\t";
                            else
                                strTab = "\t\t\t";

                            serialPortKitchen.WriteLine(string.Format("{0:20}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(
                                OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                            if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                                if (StoreInfo.Kitchen1Type.CompareTo("2") == 0)
                                {
                                    serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                    serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                }
                                else
                                {
                                    serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                                    serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                                }
                            }

                            if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != "")
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                                if (StoreInfo.Kitchen1Type.CompareTo("2") == 0)
                                    serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                else
                                    serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));

                            }

                            if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == "")
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                                if (StoreInfo.Kitchen1Type.CompareTo("2") == 0)
                                    serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                else
                                    serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                            }
                        }
                        else
                        {
                            if (StoreInfo.Kitchen1Type.CompareTo("2") == 0)
                                strTab = "\t";
                            else
                                strTab = "\t\t\t";

                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                            serialPortKitchen.WriteLine(string.Format("{0:20}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " +  //일반 9600프린터
                                OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                            if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                                if (StoreInfo.Kitchen1Type.CompareTo("2") == 0)
                                {
                                    serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                    serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                }
                                else
                                {
                                    serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                                    serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                                }
                            }

                            if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == string.Empty)
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                                if (StoreInfo.Kitchen1Type.CompareTo("2") == 0)
                                    serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                else
                                    serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                            }

                            if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != string.Empty)
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                                if (StoreInfo.Kitchen1Type.CompareTo("2") == 0)
                                    serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                                else
                                    serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));

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
                            serialPortKitchen.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i], 40)));

                            bOpnm = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                            if (bOpnm.Length < 4)
                                strTab = "\t\t";
                            else
                                strTab = "\t";
                            serialPortKitchen.WriteLine(string.Format("{0:20}{1}{2,12}{3,5}", UtilHelper.PadingLeftBySpace(
                                OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                            if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                                serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                                serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                            }

                            if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != "")
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                                serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                            }

                            if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == "")
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                                serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                            }
                        }
                        else
                        {
                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                            serialPortKitchen.WriteLine(string.Format("{0:14}{1,12}{2,5}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " +
                                OrderListInfo.ItemOpNm[i], 14), OrderListInfo.ItemQuan[i], "주문"));

                            if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                                serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                                serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                            }

                            if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == string.Empty)
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                                serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                            }

                            if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != string.Empty)
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                                serialPortKitchen.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                            }
                        }
                        if (prnRemark[i] != string.Empty)
                        {
                            string[] temp = prnRemark[i].Split(Convert.ToChar(0xA));

                            serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                            serialPortKitchen.Write("> (");
                            for (int j = 0; j < temp.Length; j++)
                            {
                                serialPortKitchen.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                                serialPortKitchen.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                                serialPortKitchen.Write(temp[j]);
                                if (temp.Length != i)
                                    serialPortKitchen.Write(" ");
                            }
                            serialPortKitchen.WriteLine(")");
                        }
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

            CounterPrint();
            ReceiptPrint();
            TurnNumPrint();

            splashPayMentComple.Close();
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
                {
                    serialPortCounter.Open();
                }

                serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                serialPortCounter.WriteLine(StoreInfo.StoreName + "\r\n\r\n");
                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", StoreInfo.StoreName);
                serialPortCounter.WriteLine("[카운터]");
                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", "[카운터]");
                serialPortCounter.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                serialPortCounter.WriteLine(UtilHelper.HyphepMake("=", 42));
                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", UtilHelper.HyphepMake("=", 42));
                serialPortCounter.Write("주문시간 : ");
                serialPortCounter.Write(strDate + " ");
                serialPortCounter.WriteLine(strTime + "  Kiosk : " + StoreInfo.StoreDesk);
                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", "주문시간 : " + strDate +
                //    "  Kiosk : " + StoreInfo.StoreDesk);
                serialPortCounter.WriteLine(UtilHelper.HyphepMake("=", 42));
                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", UtilHelper.HyphepMake("=", 42));

                for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
                {
                    byte[] bTmpName = encod.GetBytes(OrderListInfo.ItemName[i] + OrderListInfo.ItemOpNm[i]);

                    if (bTmpName.Length > 20)
                    {
                        serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                        serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i], 40)));

                        //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", 
                        //   UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i], 40));

                        bOpnm = encod.GetBytes(OrderListInfo.ItemOpNm[i]);

                        if (StoreInfo.CounterType.CompareTo("2") == 0)
                            strTab = "\t";
                        else 
                            strTab = "\t\t\t";

                        serialPortCounter.WriteLine(string.Format("{0:20}{1}{2, 4}{3,9}", UtilHelper.PadingLeftBySpace(
                            OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                        //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", string.Format("{0:20}{1}{2, 4}{3,9}", UtilHelper.PadingLeftBySpace(
                        //   OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                        if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                        {
                            serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                            if (StoreInfo.CounterType.CompareTo("2") == 0)
                            {
                                serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                            }
                            else
                            {
                                serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                                serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                            }
                        }

                        if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == string.Empty)
                        {
                            serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                            if (StoreInfo.CounterType.CompareTo("2") == 0)
                                serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                            else
                                serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                        }

                        if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != string.Empty)
                        {
                            serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                            if (StoreInfo.CounterType.CompareTo("2") == 0)
                                serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                            else
                                serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                        }
                    }
                    else
                    {
                        if (StoreInfo.CounterType.CompareTo("2") == 0)
                            strTab = "\t";
                        else
                            strTab = "\t\t\t";

                        serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                        serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));
                        serialPortCounter.WriteLine(string.Format("{0:20}{1}{2,4}{3,9}", UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " +
                            OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                        //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", string.Format("{0:20}{1}{2,4}{3,9}", 
                        //   UtilHelper.PadingLeftBySpace(OrderListInfo.ItemName[i] + " " +
                        //   OrderListInfo.ItemOpNm[i], 20), strTab, OrderListInfo.ItemQuan[i], "주문"));

                        if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() != "")
                        {
                            serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                            if (StoreInfo.CounterType.CompareTo("2") == 0)
                            {
                                serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                                serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                            }
                            else
                            {
                                serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                                serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                            }
                        }

                        if (OrderListInfo.Memo1[i].Trim() != "" && OrderListInfo.Memo2[i].Trim() == string.Empty)
                        {
                            serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                            if (StoreInfo.CounterType.CompareTo("2") == 0)
                                serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 40)));
                            else
                                serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo1[i].Trim(), 45)));
                        }

                        if (OrderListInfo.Memo1[i].Trim() == "" && OrderListInfo.Memo2[i].Trim() != string.Empty)
                        {
                            serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 1));
                            serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(0));

                            if (StoreInfo.CounterType.CompareTo("2") == 0)
                                serialPortCounter.WriteLine(string.Format("{0:40}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 40)));
                            else
                                serialPortCounter.WriteLine(string.Format("{0:45}", UtilHelper.PadingLeftBySpace("└" + OrderListInfo.Memo2[i].Trim(), 45)));
                        }
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
                        //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", UtilHelper.HyphepMake("=", 42));
                    }
                    else
                    {
                        serialPortCounter.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0));
                        serialPortCounter.Write(Convert.ToChar(0X1B) + "a" + Convert.ToChar(1));
                        serialPortCounter.WriteLine(UtilHelper.HyphepMake("-", 42));
                        //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", UtilHelper.HyphepMake("-", 42));
                    }
                }

                serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortCounter.Write(Convert.ToChar(0X1D) + "!" + Convert.ToChar(0 + 1));
                serialPortCounter.WriteLine("");
                serialPortCounter.WriteLine("키오스크 : [ 주문번호 : " + string.Format("{0:D3} ]\r\n", Convert.ToInt64(turnNum)));

                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", "키오스크 : [ 주문번호 : " +
                //    string.Format("{0:D3} ]", turnNum.ToInteger()));

                if (StoreInfo.IsTakeOut)
                {
                    serialPortCounter.WriteLine("<< 포장판매 >> \r\n\r\n\r\n");
                    //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", "<< 포장판매 >>");
                }
                else
                {
                    serialPortCounter.WriteLine("<< 매장식사 >> \r\n\r\n\r\n");
                    //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", "<< 매장식사 >>");
                }

                serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(1 + 1));
                serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortCounter.WriteLine("");
                serialPortCounter.Write(Convert.ToChar(0x1B) + "" + Convert.ToChar(0x0C));
                serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(0 + 0));
                serialPortCounter.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortCounter.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortCounter.Write(Convert.ToChar(0x1B) + "S");

                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", "Cutting OK");
                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", UtilHelper.HyphepMake("-", 42));
                //LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printSend", UtilHelper.HyphepMake("-", 42) + "\r\n");

            }
            catch (Exception e)
            {
                serialPortCounter.Close();
                LogMenager.LogWriter(UtilHelper.Root + @"\Log\cPrintSend\printError", e.ToString() + "신용카드");
            }
            finally
            {
                serialPortCounter.Close();
            }
        }

        private double GetVat(double amt)
        {
            return (Convert.ToInt32(Math.Truncate(amt * 0.1)));
        }

        private double GetSupply(double amt)
        {
            return (Convert.ToInt32(Math.Round(amt / 1.1)));
        }

        private decimal GetVat()
        {
            return dTotalAmt.ToInteger() / 11;
        }

        private decimal GetAmt()
        {
            return dTotalAmt - GetVat();
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

        private void serialPortCounter_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //if (serialPortCounter.IsOpen)
            //{
            //    string str = serialPortCounter.ReadLine();

            //    LogMenager.LogWriter(UtilHelper.Root + @"\Log\printSend\printSend", "test");
            //    writer.Write(str.ToString());
            //    writer.Flush();
            //}

            try
            {
                System.IO.Ports.SerialPort recvPort = (System.IO.Ports.SerialPort)sender;

                string recvData = recvPort.ReadExisting();

                this.Invoke(new MethodInvoker(delegate
                {
                    MessageBox.Show(recvData);
                }));
            }
            catch { }
        }
    }
}
