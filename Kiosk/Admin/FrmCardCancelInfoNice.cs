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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Kiosk
{
    public partial class FrmCardCancelInfoNice : Form
    {
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

        private Encoding Encod = Encoding.GetEncoding("ks_c_5601-1987");

        char FS = Convert.ToChar(0x1C);

        FrmErrorBox errBox = null;

        DBControl.SaleDDAO saledDao = new DBControl.SaleDDAO();
        DBControl.SaleHDAO salehDao = new DBControl.SaleHDAO();
        DBControl.VanDataDAO vanDataDao = new DBControl.VanDataDAO();

        SplashCardCancelProc cardCancelProc = new SplashCardCancelProc();
        SplashCardCancelOk cardCancelOk = new SplashCardCancelOk();

        string appNo = string.Empty;
        string appDate = string.Empty;
        string appRecNo = string.Empty;
        string appAmt = string.Empty;
        string appMonth = string.Empty;
        string storeFlag = string.Empty;
        string cardMsg = string.Empty;

        byte[] bRecvData = new byte[1024];

        public FrmCardCancelInfoNice()
        {
            InitializeComponent();
        }

        public FrmCardCancelInfoNice(string appDate, string appRecNo, string appNo, string appMonth, string appAmt, string salesDate)
        {
            InitializeComponent();
            saledDao.BRAND = salehDao.BRAND = vanDataDao.BRAND = StoreInfo.BrnadCode;
            //saledDao.BRAND = salehDao.BRAND = vanDataDao.BRAND = "00003";
            saledDao.STORE = salehDao.STORE = vanDataDao.STORE = StoreInfo.StoreCode;
            saledDao.DESK = salehDao.DESK = vanDataDao.DESK = StoreInfo.StoreDesk;
            if (salesDate != "")
                saledDao.DATE = salehDao.DATE = salesDate;
            else
                saledDao.DATE = salehDao.DATE = StoreInfo.StoreOpen;

            this.appDate = appDate;
            this.appRecNo = appRecNo;
            this.appNo = appNo;
            this.appMonth = appMonth;
            this.appAmt = appAmt;
        }

        private void FrmCardCancelInfo_Load(object sender, EventArgs e)
        {
            //axShockwaveFlash1.Movie = Directory.GetCurrentDirectory() + @"\System\swf\card_in.swf";
            pbGif.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\System\swf\card_in_crop.gif");
            UtilHelper.spCardWav.Play();

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

        private void MouseUp_Proc()
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void CardCancelApp(object sender)
        {
            int rtn;

            StringBuilder send_data = new StringBuilder();
            send_data.Clear();
            send_data.Append("0420");
            send_data.Append(FS);
            send_data.Append("10");
            send_data.Append(FS);
            send_data.Append("C");
            send_data.Append(FS);
            send_data.Append(appAmt);
            send_data.Append(FS);
            send_data.Append("0");
            send_data.Append(FS);
            send_data.Append("0");
            send_data.Append(FS);
            send_data.Append(appMonth);
            send_data.Append(FS);
            send_data.Append(appNo);
            send_data.Append(FS);
            send_data.Append(appDate);
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
                                //this.Visible = false;

                                cardCancelProc = new SplashCardCancelProc();
                                cardCancelProc.Open();

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
                            }));
                    }
                    else
                    {
                        //카드결제 실패
                        errBox = new FrmErrorBox("취소실패", cardMsg.Trim('\0'));

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
                    errBox = new FrmErrorBox("취소불가카드", "올바른 카드가 아니거나\r\n카드방향을 확인해주세요");

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
                    cardCancelOk.Close();
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
            string[] sRecvData = Encod.GetString(recvData).Split(FS);

            vanDataDao.VANGB = StoreInfo.VanSelect;
            vanDataDao.DATE = StoreInfo.StoreOpen;
            vanDataDao.MOMTH = string.Format("{0:D2}", appMonth.ToInteger());
            vanDataDao.RECENO = appRecNo;
            vanDataDao.APPAMT = appAmt.ToDecimal();
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

                byte[] bTmpAdd = Encoding.Default.GetBytes(StoreInfo.StoreAdd2);

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
                serialPortReceipt.WriteLine(string.Format("{0,-19}{1,26}", strDate + " " + strTime, "NO:K" +
                    string.Format("{0:D2}-{1, 5}", Convert.ToInt32(StoreInfo.StoreDesk), appRecNo)));

                serialPortReceipt.Write(PrintHelper.Command.AlignCenter);
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(1, 0));
                serialPortReceipt.WriteLine(string.Format("총매출액{0,15:##,##0}", Convert.ToDecimal(appAmt)));
                serialPortReceipt.WriteLine(string.Format("합계금액{0,15:##,##0}", Convert.ToDecimal(appAmt)));
                serialPortReceipt.WriteLine(string.Format("취소금액{0,15:##,##0}", Convert.ToDecimal(appAmt)));
                serialPortReceipt.Write(PrintHelper.Command.ConvertFontSize(0, 0));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                serialPortReceipt.WriteLine(string.Format("                [카드]{0,24:##,##0}", Convert.ToDecimal(appAmt)));
                serialPortReceipt.WriteLine(UtilHelper.HyphepMake("-", 46));
                //serialPortReceipt.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1));
                serialPortReceipt.WriteLine("**" + vanDataDao.ORDNM + "**");
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
            saledDao.GUBN = "취소";

            if (saledDao.Saled_Update())
                return saledDao.Saled_UpdateL();

            return false;
        }

        private bool SaleH_CancelUpdate()
        {
            salehDao.RECENO = appRecNo;
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
