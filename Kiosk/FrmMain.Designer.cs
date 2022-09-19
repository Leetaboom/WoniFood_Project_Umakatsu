namespace Kiosk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.picBoxEatin = new System.Windows.Forms.PictureBox();
            this.picBoxTakeout = new System.Windows.Forms.PictureBox();
            this.picBoxLogo = new System.Windows.Forms.PictureBox();
            this.timerHoldMent = new System.Windows.Forms.Timer(this.components);
            this.timerImage = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.timerReal = new System.Windows.Forms.Timer(this.components);
            this.serialPortCounter = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEatin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTakeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxEatin
            // 
            this.picBoxEatin.BackColor = System.Drawing.Color.Transparent;
            this.picBoxEatin.BackgroundImage = global::Kiosk.Properties.Resources.btn_eatin;
            this.picBoxEatin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxEatin.Location = new System.Drawing.Point(189, 732);
            this.picBoxEatin.Name = "picBoxEatin";
            this.picBoxEatin.Size = new System.Drawing.Size(308, 282);
            this.picBoxEatin.TabIndex = 3;
            this.picBoxEatin.TabStop = false;
            this.picBoxEatin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxEatin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxTakeout
            // 
            this.picBoxTakeout.BackColor = System.Drawing.Color.Transparent;
            this.picBoxTakeout.BackgroundImage = global::Kiosk.Properties.Resources.btn_takeout;
            this.picBoxTakeout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxTakeout.Location = new System.Drawing.Point(584, 732);
            this.picBoxTakeout.Name = "picBoxTakeout";
            this.picBoxTakeout.Size = new System.Drawing.Size(308, 282);
            this.picBoxTakeout.TabIndex = 4;
            this.picBoxTakeout.TabStop = false;
            this.picBoxTakeout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxTakeout.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxLogo
            // 
            this.picBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.picBoxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxLogo.Location = new System.Drawing.Point(271, 21);
            this.picBoxLogo.Name = "picBoxLogo";
            this.picBoxLogo.Size = new System.Drawing.Size(536, 140);
            this.picBoxLogo.TabIndex = 5;
            this.picBoxLogo.TabStop = false;
            this.picBoxLogo.DoubleClick += new System.EventHandler(this.picBoxLogo_DoubleClick);
            // 
            // timerHoldMent
            // 
            this.timerHoldMent.Interval = 3000;
            this.timerHoldMent.Tick += new System.EventHandler(this.timerHoldMent_Tick);
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTime.ForeColor = System.Drawing.Color.Black;
            this.lblTime.Location = new System.Drawing.Point(1011, 140);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(69, 21);
            this.lblTime.TabIndex = 14;
            this.lblTime.Text = "18:19";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDate.ForeColor = System.Drawing.Color.Black;
            this.lblDate.Location = new System.Drawing.Point(851, 138);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(166, 23);
            this.lblDate.TabIndex = 13;
            this.lblDate.Text = "2019-11-11(금)";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timerReal
            // 
            this.timerReal.Enabled = true;
            this.timerReal.Interval = 1000;
            this.timerReal.Tick += new System.EventHandler(this.timerReal_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Properties.Resources.bg_main;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.picBoxLogo);
            this.Controls.Add(this.picBoxTakeout);
            this.Controls.Add(this.picBoxEatin);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KioskMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEatin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTakeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picBoxEatin;
        private System.Windows.Forms.PictureBox picBoxTakeout;
        private System.Windows.Forms.PictureBox picBoxLogo;
        private System.Windows.Forms.Timer timerHoldMent;
        private System.Windows.Forms.Timer timerImage;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Timer timerReal;
        private System.IO.Ports.SerialPort serialPortCounter;
    }
}

