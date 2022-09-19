namespace KioskUpdater
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.updateListDataSet = new KioskUpdater.Controler.UpdateListDataSet();
            this.woniPanel1 = new KioskUpdater.Controler.WoniPanel();
            this.btnOk = new KioskUpdater.Controler.WoniButton();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblUpdatableList = new System.Windows.Forms.Label();
            this.lsbFiles = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.autoUpdater = new KioskUpdater.Controler.AutoUpdater(this.components);
            this.timerUpdate_Comple = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.updateListDataSet)).BeginInit();
            this.woniPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // updateListDataSet
            // 
            this.updateListDataSet.DataSetName = "UpdateListDataSet";
            this.updateListDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // woniPanel1
            // 
            this.woniPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.woniPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.woniPanel1.Controls.Add(this.btnOk);
            this.woniPanel1.Controls.Add(this.lblLength);
            this.woniPanel1.Controls.Add(this.lblFile);
            this.woniPanel1.Controls.Add(this.lblUpdatableList);
            this.woniPanel1.Controls.Add(this.lsbFiles);
            this.woniPanel1.Controls.Add(this.progressBar1);
            this.woniPanel1.Controls.Add(this.label1);
            this.woniPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.woniPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.woniPanel1.Location = new System.Drawing.Point(0, 0);
            this.woniPanel1.Name = "woniPanel1";
            this.woniPanel1.Size = new System.Drawing.Size(900, 600);
            this.woniPanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.woniPanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.woniPanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.woniPanel1.Style.BackgroundImage = global::KioskUpdater.Properties.Resources.popup_s;
            this.woniPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.DoubleLine;
            this.woniPanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.woniPanel1.Style.BorderWidth = 8;
            this.woniPanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.woniPanel1.Style.GradientAngle = 90;
            this.woniPanel1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
            this.btnOk.Font = new System.Drawing.Font("나눔고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOk.Location = new System.Drawing.Point(756, 118);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 70);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "확 인";
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Font = new System.Drawing.Font("나눔고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLength.Location = new System.Drawing.Point(12, 176);
            this.lblLength.Name = "lblLength";
            this.lblLength.Padding = new System.Windows.Forms.Padding(3);
            this.lblLength.Size = new System.Drawing.Size(136, 37);
            this.lblLength.TabIndex = 14;
            this.lblLength.Text = "lblLength";
            this.lblLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("나눔고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblFile.Location = new System.Drawing.Point(12, 135);
            this.lblFile.Name = "lblFile";
            this.lblFile.Padding = new System.Windows.Forms.Padding(3);
            this.lblFile.Size = new System.Drawing.Size(93, 37);
            this.lblFile.TabIndex = 13;
            this.lblFile.Text = "lblFile";
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUpdatableList
            // 
            this.lblUpdatableList.AutoSize = true;
            this.lblUpdatableList.Font = new System.Drawing.Font("나눔고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblUpdatableList.Location = new System.Drawing.Point(12, 94);
            this.lblUpdatableList.Name = "lblUpdatableList";
            this.lblUpdatableList.Padding = new System.Windows.Forms.Padding(3);
            this.lblUpdatableList.Size = new System.Drawing.Size(221, 37);
            this.lblUpdatableList.TabIndex = 12;
            this.lblUpdatableList.Text = "lblUpdatableList";
            this.lblUpdatableList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lsbFiles
            // 
            this.lsbFiles.Font = new System.Drawing.Font("나눔고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lsbFiles.ItemHeight = 28;
            this.lsbFiles.Location = new System.Drawing.Point(9, 282);
            this.lsbFiles.Name = "lsbFiles";
            this.lsbFiles.Size = new System.Drawing.Size(883, 312);
            this.lsbFiles.TabIndex = 11;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 226);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(884, 50);
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(894, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kiosk Updater";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autoUpdater
            // 
            this.autoUpdater.LocalRoot = "c:\\Temp\\";
            this.autoUpdater.RootUri = "ftp://ifou.co.kr/Kiosk/UpdateData\\test";
            this.autoUpdater.UpdateListFileName = "UpdateList.xml";
            this.autoUpdater.UpdateCompleted += new System.EventHandler<KioskUpdater.Controler.AutoUpdater.UpdateCompletedEventArgs>(this.autoUpdater_UpdateCompleted);
            this.autoUpdater.UpdatableListFound += new System.EventHandler<KioskUpdater.Controler.AutoUpdater.UpdatableListFoundEventArgs>(this.autoUpdater_UpdatableListFound);
            this.autoUpdater.UpdatableFileFound += new System.EventHandler<KioskUpdater.Controler.AutoUpdater.UpdatableFileFoundEventArgs>(this.autoUpdater_UpdatableFileFound);
            this.autoUpdater.FileTransfering += new System.EventHandler<KioskUpdater.Controler.AutoUpdater.FileTransferingEventArgs>(this.autoUpdater_FileTransfering);
            this.autoUpdater.FileTransfered += new System.EventHandler<KioskUpdater.Controler.AutoUpdater.FileTransferedEventArgs>(this.autoUpdater_FileTransfered);
            this.autoUpdater.UpdateProgressChanged += new System.EventHandler<KioskUpdater.Controler.AutoUpdater.UpdateProgressChangedEventArgs>(this.autoUpdater_UpdateProgressChanged);
            // 
            // timerUpdate_Comple
            // 
            this.timerUpdate_Comple.Interval = 1000;
            this.timerUpdate_Comple.Tick += new System.EventHandler(this.timerUpdate_Comple_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.woniPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.updateListDataSet)).EndInit();
            this.woniPanel1.ResumeLayout(false);
            this.woniPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controler.WoniPanel woniPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblUpdatableList;
        private System.Windows.Forms.ListBox lsbFiles;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Controler.UpdateListDataSet updateListDataSet;
        private Controler.AutoUpdater autoUpdater;
        private Controler.WoniButton btnOk;
        private System.Windows.Forms.Timer timerUpdate_Comple;
    }
}

