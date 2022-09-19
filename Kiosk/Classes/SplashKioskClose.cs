using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Kiosk
{
    class SplashKioskClose
    {
        private Thread thread;
        private FrmPayMentProc closeOnGoing;
        private EventWaitHandle loaded;
        private Bitmap bitmap;

        Form parent; 

        public SplashKioskClose(Form parent, Bitmap bitmap)
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);
            this.bitmap = bitmap;
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
                loaded.WaitOne();
                UtilHelper.Delay(2000);
                closeOnGoing.Invoke(new CloseCallBack(closeOnGoing.Close));
            }
            catch
            { }
        }

        public void Join()
        {
            thread.Join();
        }

        public void UpdateProgress()
        {
            loaded.WaitOne();
        }

        private void RunSplash()
        {
            closeOnGoing = new FrmPayMentProc(parent, bitmap, "마감 진행중", "마감 진행 중 입니다");
            closeOnGoing.Load += new EventHandler(OnLoad);
            closeOnGoing.TopMost = true;
            closeOnGoing.TopLevel = true;
            closeOnGoing.ShowDialog();
        }

        void OnLoad(object sender, EventArgs e)
        {
            loaded.Set();
        }

        private delegate void CloseCallBack();
    }
}
