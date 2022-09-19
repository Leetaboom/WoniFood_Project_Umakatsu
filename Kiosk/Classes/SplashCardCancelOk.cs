using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Kiosk
{
    class SplashCardCancelOk
    {
        Thread thread;
        EventWaitHandle loaded;
        FrmCardCancelOk cardCancelOk;

        public SplashCardCancelOk()
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
                UtilHelper.Delay(2500);
                cardCancelOk.Invoke(new CloseCallBack(cardCancelOk.Close));
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
            cardCancelOk = new FrmCardCancelOk();
            cardCancelOk.Load += new EventHandler(OnLoad);
            cardCancelOk.TopMost = true;
            cardCancelOk.TopLevel = true;
            cardCancelOk.ShowDialog();
        }

        void OnLoad(object sender, EventArgs e)
        {
            loaded.Set();
        }

        private delegate void CloseCallBack();
    }
}
