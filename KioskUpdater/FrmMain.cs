using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace KioskUpdater
{
    public partial class FrmMain : Form
    {
        Mutex mtx;

        int count = 5;

        string startProcessName = string.Empty;

        public FrmMain()
        {
            InitializeComponent();

            autoUpdater.LocalRoot = UtilHelper.Root;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            string mtxName = "WONI_FOOD_KIOSK";
            mtx = new Mutex(true, mtxName);

            TimeSpan tsWait = new TimeSpan(0, 0, 1);
            bool success = mtx.WaitOne(tsWait);

            if (!success)
            {
                Application.ExitThread();
                Environment.Exit(0);
            }
            autoUpdater.Run();
        }

        private void autoUpdater_FileTransfered(object sender, Controler.AutoUpdater.FileTransferedEventArgs e)
        {
            lsbFiles.Items.Add(e.RemoteFile.LocalPath);
            lsbFiles.SelectedIndex = lsbFiles.Items.Count - 1;
        }

        private void autoUpdater_FileTransfering(object sender, Controler.AutoUpdater.FileTransferingEventArgs e)
        {
            lblFile.Text = string.Format("{0} ({1:N0} / {2:N0})", e.RemoteFile.LocalPath,
                e.TransferingInfo.TransferedFileCount + 1, e.TransferingInfo.TotalFileCount);
        }

        private void autoUpdater_UpdatableFileFound(object sender, Controler.AutoUpdater.UpdatableFileFoundEventArgs e)
        {
            
        }

        private void autoUpdater_UpdateProgressChanged(object sender, Controler.AutoUpdater.UpdateProgressChangedEventArgs e)
        {
            int percent = e.TransferingInfo.LengthPercent;
            progressBar1.Value = percent;
            lblLength.Text = string.Format("{0:N0} / {1:N0} ({2}%), 남은 시간 : {3:N0} 초",
                e.TransferingInfo.TotalTransferedLength, e.TransferingInfo.TotalLength, percent,
                e.TransferingInfo.RemainingSeconds);
        }

        private void autoUpdater_UpdatableListFound(object sender, Controler.AutoUpdater.UpdatableListFoundEventArgs e)
        {
            if (e.RemoteFiles.Count > 0)
            {
                lblUpdatableList.Text = string.Format("업데이트 대상 파일이 {0}개 있습니다.", e.RemoteFiles.Count);
            }
            else
            {
                lblUpdatableList.Text = "현재 최신 버전 입니다.";
                lblUpdatableList.ForeColor = Color.RoyalBlue;
                lblFile.Visible = false;
                lblLength.Visible = false;
            }
        }

        private void autoUpdater_UpdateCompleted(object sender, Controler.AutoUpdater.UpdateCompletedEventArgs e)
        {
            startProcessName = e.LocalRoot + "Kiosk.exe";
            btnOk.Visible = true;
            timerUpdate_Comple.Start();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Process.Start(startProcessName);

            Application.Exit();
        }

        private void timerUpdate_Comple_Tick(object sender, EventArgs e)
        {
            if (count == 0)
            {
                timerUpdate_Comple.Stop();
                timerUpdate_Comple.Dispose();

                btnOk_Click(sender, null);
            }
            btnOk.Text = string.Format("확 인(" + count + ")");
            count--;
        }
    }
}
