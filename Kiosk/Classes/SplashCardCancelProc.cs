using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Kiosk
{
    class SplashCardCancelProc
    {
        Thread thread;
        EventWaitHandle loaded;
        FrmCardCancelProc cardCancelProc;

        public SplashCardCancelProc()
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);
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
                UtilHelper.Delay(3000);
                cardCancelProc.Invoke(new CloseCallBack(cardCancelProc.Close));
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
            cardCancelProc = new FrmCardCancelProc();
            cardCancelProc.Load += new EventHandler(OnLoad);
            cardCancelProc.TopMost = true;
            cardCancelProc.TopLevel = true;
            cardCancelProc.ShowDialog();
        }

        void OnLoad(object sender, EventArgs e)
        {
            loaded.Set();
        }

        private delegate void CloseCallBack();
    }
}
