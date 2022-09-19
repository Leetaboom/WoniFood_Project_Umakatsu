using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Kiosk
{

    class SplashPayMentComple
    {
        Form parent;

        private Thread thread;
        private FrmPayMentComple payMentComple;
        private EventWaitHandle loaded;
        private Bitmap bitmap;
        private int flag = 0;

        public SplashPayMentComple(Form parent, Bitmap bitmap)
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);
            this.bitmap = bitmap;
            this.parent = parent;
        }

        public SplashPayMentComple(int i)
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);
            flag = i;
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
                UtilHelper.Delay(3000);
                payMentComple.Invoke(new CloseCallback(payMentComple.Close));
            }
            catch { }
        }
        
        public void Join()
        {
            thread.Join();
        }

        public void UpdateProgress()
        {
            
        }

        private void RunSplash()
        {
            payMentComple = new FrmPayMentComple(parent, bitmap);
            payMentComple.Load += new EventHandler(OnLoad);
            payMentComple.TopMost = true;
            payMentComple.TopLevel = true;
            payMentComple.ShowDialog();
        }

        void OnLoad(object sender, EventArgs e)
        {
            loaded.Set();
        }

        private delegate void CloseCallback();
    }
}
