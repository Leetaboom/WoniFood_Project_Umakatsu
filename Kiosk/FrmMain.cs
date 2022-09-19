using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Kiosk
{
    public partial class FrmMain : Form
    {
        public delegate void SendValueHandler(string msg);
        public event SendValueHandler sendValueEvent;
        public event SendValueHandler sendValueEventToGo;
        public static FrmSetMenuOptPopup setMenuOptPopup;
        public static FrmMenuList menuListCfg;
        public static FrmMenuListToGo menuListToGoCfg;
        DBControl.OpenKioskDAO openKioskDao = new DBControl.OpenKioskDAO();

        List<string> moveFileList;
        List<string> imageFileList;


        SplashInit initBox = new SplashInit();
        PictureBox picBoxHold;

        Mutex mtx;

        string ctrlName;
        //int imgSliderIndex = 0;

        bool isHoldPlaying = false;

        public FrmMain()
        {
            InitializeComponent();

            openKioskDao.BRAND = StoreInfo.BrnadCode;
            openKioskDao.STORE = StoreInfo.StoreCode;
            openKioskDao.DESK = StoreInfo.StoreDesk;
            this.Visible = false;
        }

        #region 메서드

        private void SendFormMsg(string msg)
        {
            if (!StoreInfo.IsTakeOut)
                sendValueEvent(msg);
            else
                sendValueEventToGo(msg);
        }

        private void HoldBoxInit()
        {
            picBoxHold = new PictureBox();
            picBoxHold.Location = new Point(0, 186);
            picBoxHold.Size = new Size(1080, 1734);
            picBoxHold.BackgroundImage = Properties.Resources.hold_no_time;
            picBoxHold.Click += new EventHandler(picBoxHold_Click);
            picBoxHold.Visible = false;

            this.Controls.Add(picBoxHold);
        }


        private void OpenKioskState()
        {
            using (DataTable dt = openKioskDao.KioskOpenStateL())
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                            StoreInfo.StoreOpen = dtr["OK_DATE"].ToString();
                    }
                }
                else
                {
                    openKioskDao.DATE = DateTime.Now.ToString("yyyyMMdd");

                    if (openKioskDao.KioskOpenSave())
                    {
                        openKioskDao.KioskOpenSaveL();
                    }

                    StoreInfo.StoreOpen = openKioskDao.DATE;
                }
            }
        }

        private void MenuListShow(object sender)
        {
            initBox.Open();

            this.Invoke(new MethodInvoker(
                delegate ()
                {
                    this.Visible = false;
                    FrmMenuList menuList = new FrmMenuList(this);
                    menuListCfg = menuList;
                    menuList.returnValueEvent += new FrmMenuList.ReturnValueHandler(menuList_returnValueEvent);
                    menuList.Owner = this;
                    menuList.Show();
                    menuList.Hide();
                }));

            this.Invoke(new MethodInvoker(
                delegate ()
                {
                    FrmMenuListToGo menuList = new FrmMenuListToGo(this);
                    menuListToGoCfg = menuList;
                    menuList.returnValueEvent += new FrmMenuListToGo.ReturnValueHandler(menuList_returnValueEvent);
                    menuList.Owner = this;
                    menuList.Show();
                    menuList.Hide();
                }));


            this.Invoke(new MethodInvoker(
                delegate ()
                {
                    setMenuOptPopup = new FrmSetMenuOptPopup();
                    setMenuOptPopup.Owner = this;
                    setMenuOptPopup.Show();
                    setMenuOptPopup.Hide();

                    UtilHelper.Delay(500);
                    this.Visible = true;
                }));

            initBox.Close();
        }

        private void EatinProc()
        {
            //axWindowsMediaPlayer1.Ctlcontrols.pause();
            StoreInfo.IsTakeOut = false;
            this.TopMost = false;

            SendFormMsg("SHOW");

        }

        private void TakeoutProc()
        {
            // axWindowsMediaPlayer1.Ctlcontrols.pause();
            StoreInfo.IsTakeOut = true;
            this.TopMost = false;

            SendFormMsg("SHOW");
        }

        private void PrintTestMsg()
        {
            try
            {
                serialPortCounter.PortName = StoreInfo.CounterPrn;
                serialPortCounter.BaudRate = StoreInfo.CounterRate;
                serialPortCounter.DataBits = 8;
                serialPortCounter.Parity = System.IO.Ports.Parity.None;
                serialPortCounter.StopBits = System.IO.Ports.StopBits.One;
                serialPortCounter.Encoding = System.Text.Encoding.Default;

                if (serialPortCounter.IsOpen == false)
                    serialPortCounter.Open();

                serialPortCounter.Write(Convert.ToChar(0x1B) + "a" + Convert.ToChar(1)); // 0:왼쪽 정렬, 1:중앙정렬 2:오른쪽정렬
                serialPortCounter.Write(Convert.ToChar(0x1C) + "p" + Convert.ToChar(0) + Convert.ToChar(3));
                serialPortCounter.Write(Convert.ToChar(0x1D) + "!" + Convert.ToChar(16 + 1));
                serialPortCounter.WriteLine("Print Init....\n\n\n");
                serialPortCounter.Write(Convert.ToChar(0x1B) + "J" + Convert.ToChar(125));
                serialPortCounter.Write(Convert.ToChar(0x1D) + "V" + Convert.ToChar(1));
                serialPortCounter.Write(Convert.ToChar(0x1B) + "S");
            }
            catch
            {
                serialPortCounter.Close();
            }
            finally
            {
                serialPortCounter.Close();
            }
        }

        private void GetPromotionFiles()
        {
            string[] moveFiles = Directory.GetFiles(UtilHelper.Root + "Video");
            string[] imageFiles = Directory.GetFiles(UtilHelper.Root + "image");

            if (moveFiles.Length != 0)
            {
                moveFileList = new List<string>();

                foreach (string s in moveFiles)
                    moveFileList.Add(s);
            }

            if (imageFiles.Length != 0)
            {
                imageFileList = new List<string>();

                foreach (string s in imageFiles)
                    imageFileList.Add(s);
            }
        }

        //private void BGPromotionPlay()
        //{
        //    playList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myPlayList");

        //    if (moveFileList == null)
        //        return;

        //    foreach (string fileName in moveFileList)
        //    {
        //        media = axWindowsMediaPlayer1.newMedia(fileName);
        //        playList.appendItem(media);
        //    }

        //    axWindowsMediaPlayer1.uiMode = "none";
        //    axWindowsMediaPlayer1.currentPlaylist = playList;
        //    axWindowsMediaPlayer1.enableContextMenu = true;
        //    axWindowsMediaPlayer1.Ctlenabled = false;
        //    //axWindowsMediaPlayer1.fullScreen = true;
        //    axWindowsMediaPlayer1.settings.autoStart = true;
        //    axWindowsMediaPlayer1.settings.setMode("loop", true);
        //    axWindowsMediaPlayer1.settings.volume = StoreInfo.MediaVolume;
        //}

        //private void BGImagePlay()
        //{
        //    if (imageFileList == null)
        //        return;
        //    if (imageFileList.Count != 0)
        //    {
        //        ImageShow(imageFileList[imgSliderIndex]);
        //        timerImage.Start();
        //    }
        //}

        //private void ImageShow(string path)
        //{
        //    Image imgTmp = Image.FromFile(path);
        //    picBoxImage.BackgroundImage = null;
        //    picBoxImage.BackgroundImage = imgTmp;
        //}

        //private void NextImage()
        //{
        //    if (imgSliderIndex == imageFileList.Count - 1)
        //    {
        //        imgSliderIndex = 0;
        //        ImageShow(imageFileList[imgSliderIndex]);
        //    }
        //    else
        //    {
        //        imgSliderIndex++;
        //        ImageShow(imageFileList[imgSliderIndex]);
        //    }
        //}

        //private void timerImage_Tick(object sender, EventArgs e)
        //{
        //    NextImage();
        //}

        #endregion

        #region 이벤트

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //this.Invoke(new MethodInvoker(
            //    delegate ()
            //    {
            //        initBox.Open();
            //    }));
            string mtxName = "WONI_FOOD_KIOSK";
            mtx = new Mutex(true, mtxName);

            TimeSpan tsWait = new TimeSpan(0, 0, 1);
            bool success = mtx.WaitOne(tsWait);

            if (!success)
            {
                Application.ExitThread();
                Environment.Exit(0);
            }

            //picBoxStoreLogo.Left = (this.ClientSize.Width - picBoxStoreLogo.Width) / 2;
            OpenKioskState();
            HoldBoxInit();
            ThreadPool.QueueUserWorkItem(MenuListShow, null);
            GetPromotionFiles();
            PrintTestMsg();
            //BGPromotionPlay();
            //BGImagePlay();

            if (StoreInfo.IsHoldMode)
            {
                picBoxHold.Visible = true;
                picBoxHold.BringToFront();
                UtilHelper.spHoldWav.Play();
                isHoldPlaying = true;
                timerHoldMent.Start();
            }

            TopMost = true;
            TopLevel = true;

            LogMenager.LogWriter(UtilHelper.Root + @"\log\main\event", "Kiosk Program Init......OK");
        }

        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;
            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "picBoxEatin":
                    bitmap = UtilHelper.ChangeOpacity(picBoxEatin.BackgroundImage, 1f);
                    if (picBoxEatin.BackgroundImage != null)
                    {
                        picBoxEatin.BackgroundImage.Dispose();
                        picBoxEatin.BackgroundImage = null;
                    }
                    picBoxEatin.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxTakeout":
                    bitmap = UtilHelper.ChangeOpacity(picBoxTakeout.BackgroundImage, 1f);

                    if (picBoxTakeout.BackgroundImage != null)
                    {
                        picBoxTakeout.BackgroundImage.Dispose();
                        picBoxTakeout.BackgroundImage = null;
                    }
                    picBoxTakeout.BackgroundImage = bitmap;
                    bitmap = null;
                    break;
            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (ctrlName)
            {
                case "picBoxEatin":
                    if (picBoxEatin.BackgroundImage != null)
                    {
                        picBoxEatin.BackgroundImage.Dispose();
                        picBoxEatin.BackgroundImage = null;
                    }
                    picBoxEatin.BackgroundImage = Properties.Resources.btn_eatin;
                    UtilHelper.Delay(50);
                    EatinProc();
                    break;

                case "picBoxTakeout":
                    if (picBoxTakeout.BackgroundImage != null)
                    {
                        picBoxTakeout.BackgroundImage.Dispose();
                        picBoxTakeout.BackgroundImage = null;
                    }

                    picBoxTakeout.BackgroundImage = Properties.Resources.btn_takeout;
                    UtilHelper.Delay(50);
                    TakeoutProc();
                    break;
            }
        }

        private void picBoxLogo_DoubleClick(object sender, EventArgs e)
        {
            this.TopMost = false;

            FrmAdminLogin login = new FrmAdminLogin(UtilHelper.ScreenCapture(
                this.Width, this.Height, this.Location));

            //이미지슬라이드 스톱
            timerImage.Stop();

            if (login.ShowDialog() == DialogResult.OK)
            {
                FrmAdmin admin = new FrmAdmin();

                if (admin.ShowDialog() == DialogResult.OK)
                {
                    if (StoreInfo.IsHoldMode)
                    {
                        picBoxHold.Visible = true;
                        picBoxHold.BringToFront();
                        UtilHelper.spHoldWav.Play();
                        isHoldPlaying = true;
                        timerHoldMent.Start();
                    }

                    timerImage.Start();
                    this.TopMost = true;

                }
                else if (admin.DialogResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    try
                    {
                        timerHoldMent.Stop();
                        timerImage.Start();
                        this.TopMost = true;
                    }
                    catch { }
                }
            }
            else
            {
                timerImage.Start();
                this.TopMost = true;
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

        private void menuList_returnValueEvent(string msg)
        {
            this.Invoke(new MethodInvoker(
                delegate ()
                {
                    //axWindowsMediaPlayer1.Ctlcontrols.play();
                    this.TopMost = true;
                }));
        }

        private void picBoxHold_Click(object sender, EventArgs e)
        {
            if (!isHoldPlaying)
            {
                UtilHelper.spHoldWav.Play();
                isHoldPlaying = true;
            }
        }

        private void timerHoldMent_Tick(object sender, EventArgs e)
        {
            isHoldPlaying = false;
            if (!StoreInfo.IsHoldMode)
            {
                timerHoldMent.Stop();
                UtilHelper.spHoldWav.Stop();
                picBoxHold.Visible = false;
            }
        }

        bool isComa = false;
        private void timerReal_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format("{0:D4}-{1:D2}-{2:D2}({3})", DateTime.Now.ToString("yyyy"),
                DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("ddd"));

            if (isComa)
            {
                lblTime.Text = string.Format("{0}:{1}", DateTime.Now.ToString("HH"), DateTime.Now.ToString("mm"));
                isComa = false;
            }
            else
            {
                lblTime.Text = string.Format("{0} {1}", DateTime.Now.ToString("HH"), DateTime.Now.ToString("mm"));
                isComa = true;
            }
        }

        #endregion

    }
}
