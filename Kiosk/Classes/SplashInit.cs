using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Kiosk
{
    class SplashInit
    {
        Thread thread;
        FrmLoadingBox initBox = new FrmLoadingBox();
        EventWaitHandle loaded;

        Form parent;

        string strMsg = string.Empty;
        
        public SplashInit()
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);

        }

        public SplashInit(Form parent, string msg)
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);

            this.parent = parent;
            this.strMsg = msg;
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
                initBox.Invoke(new CloseCallback(initBox.Close));          
            }
            catch { }
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
            initBox = new FrmLoadingBox();
            initBox.Load += new EventHandler(OnLoad);
            initBox.TopMost = true;
            initBox.ShowDialog();
        }

        void OnLoad(object sender, EventArgs e)
        {
            loaded.Set();
        }

        private delegate void CloseCallback();
    }
}
