using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Kiosk
{
    class SplashPayMentProc
    {
        private Thread thread;
        private EventWaitHandle loaded;
        private FrmPayMentProc cardOnGoing;
        private Bitmap bitmap = null;
        private int flag;

        Form parent;
        public SplashPayMentProc()
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);
        }

        public SplashPayMentProc(Form parent, Bitmap bitmap, int i)
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);
            this.bitmap = bitmap;
            this.flag = i;
            this.parent = parent;
        }

        public void Open()
        {
            thread.Start();
        }

        public void Close()
        {
            try
            {
                UpdateProgress();
                loaded.WaitOne();
                UtilHelper.Delay(500);
                cardOnGoing.Invoke(new CloseCallback(cardOnGoing.Close));
            }
            catch { }
        }

        public void Join()
        {
            thread.Join();
        }

        public void UpdateProgress()
        {
            //UtilHelper.cardProg.URL = @"System\wav\progress.mp3";
            //UtilHelper.cardProg.controls.play();
            //UtilHelper.Delay(4000);

            //switch (flag)
            //{
            //    case 0:
            //        UtilHelper.cardEnd.URL = @"System\wav\card_end.mp3";
            //        UtilHelper.cardEnd.controls.play();
            //        break;

            //    case 1:
            //        UtilHelper.cardEnd.URL = @"System\wav\samsung_end.mp3";
            //        UtilHelper.cardEnd.controls.play();
            //        break;
            //}
        }

        private void RunSplash()
        {
            cardOnGoing = new FrmPayMentProc(parent, bitmap);
            cardOnGoing.Load += new EventHandler(OnLoad);
            cardOnGoing.TopMost = true;
            cardOnGoing.TopLevel = true;
            cardOnGoing.ShowDialog();
        }

        void OnLoad(object sender, EventArgs e)
        {
            loaded.Set();
        }

        private delegate void CloseCallback();
    }
}
