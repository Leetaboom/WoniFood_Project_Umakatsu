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

namespace Kiosk
{
    public partial class FrmCardPayMent : Form
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

        FrmErrorBox errBox = null;

        private Encoding Encod = Encoding.GetEncoding("ks_c_5601-1987");

        private SplashPayMentProc splashPayMentProc;
        private SplashPayMentProc splashPayMentComple;

        private DBControl.SaleDDAO saledDao = new DBControl.SaleDDAO();
        private DBControl.SaleHDAO salehDao = new DBControl.SaleHDAO();

        StringBuilder send_data;

        string month;
        string totalAmt;
        string receNo = string.Empty;
        string trunNum = string.Empty;
        string logMsg = string.Empty;
        string errMsg = string.Empty;
        string cardMsg = string.Empty;
        string prnSpace = string.Empty;
        string prnSpace1 = string.Empty;
        decimal dTotalAmt;

        byte[] bRecvData = new byte[1024];
        char FS = Convert.ToChar(0x1C); 

        List<string> prnReamrk = new List<string>();
        List<decimal> danga = new List<decimal>();

        public FrmCardPayMent()
        {
            InitializeComponent();
        }

        public FrmCardPayMent(Bitmap bitmap, List<decimal> danga, string totalAmt, string month, string msg)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            this.danga = danga;
            this.dTotalAmt = totalAmt.ToDecimal();
            this.month = month;
        }

        private void FrmCardPayMent_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            //axShockwaveFlash1.Movie = System.IO.Directory.GetCurrentDirectory() + @"\System\swf\card_in.swf";
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
            try
            {
                //UtilHelper.cardInfo.URL = @"System\wav\card.mp3";
                //UtilHelper.cardInfo.controls.play();

                int rtn;

                send_data = new StringBuilder();
                send_data.Append("0200");
                send_data.Append(FS);
                send_data.Append("10");
                send_data.Append(FS);
                send_data.Append("C");
                send_data.Append(FS);
                send_data.Append(totalAmt);
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

                    if (valid.CompareTo("0000") == 0)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate ()
                            {
                                this.Visible = false;

                                splashPayMentProc = new SplashPayMentProc(this, UtilHelper.ScreenCapture(this.Width, this.Height, this.Location), 0);
                                splashPayMentProc.Open();

                                //로그기록

                                SaleD_Save();
                                SaleH_Save();

                                VanData_Save(buf);

                                splashPayMentProc.Close();
                                splashPayMentProc.Join();

                                this.DialogResult = DialogResult.OK;
                            }));
                    }
                    else
                    {
                        //카드결제 실패
                        errBox = new FrmErrorBox("결제실패", cardMsg.Trim('\0'));

                        if (errBox.ShowDialog() == DialogResult.OK)
                        {
                            REQ_STOP();
                            this.DialogResult = DialogResult.Cancel;
                        }
                    }
                }
                else if (rtn == -6)
                {
                    errBox = new FrmErrorBox("결제시간초과", "올바른 카드가 아니거나\r\n카드가 삽입되지 않았습니다.");

                    if (errBox.ShowDialog() == DialogResult.OK)
                    {
                        REQ_STOP();
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
            }
            catch (Exception e)
            {
                errBox = new FrmErrorBox("결제오류", "알수없는 오류 : " + e.ToString() + " 가 발생하였습니다.");

                if (errBox.ShowDialog() == DialogResult.OK)
                {
                    REQ_STOP();
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            finally
            {
                REQ_STOP();
            }
        }

        private void SaleD_Save()
        {

        }

        private void SaleH_Save()
        {

        }

        private void VanData_Save(byte[] buf)
        {

        }
    }
}
