namespace Kiosk
{
    partial class FrmSamPayCancelInfoNice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picBoxPrev = new System.Windows.Forms.PictureBox();
            this.lblCardMsg = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.serialPortReceipt = new System.IO.Ports.SerialPort(this.components);
            this.pbGif = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGif)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxPrev
            // 
            this.picBoxPrev.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxPrev.Location = new System.Drawing.Point(349, 730);
            this.picBoxPrev.Name = "picBoxPrev";
            this.picBoxPrev.Size = new System.Drawing.Size(200, 117);
            this.picBoxPrev.TabIndex = 46;
            this.picBoxPrev.TabStop = false;
            this.picBoxPrev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxPrev_MouseDown);
            this.picBoxPrev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxPrev_MouseUp);
            // 
            // lblCardMsg
            // 
            this.lblCardMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblCardMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCardMsg.ForeColor = System.Drawing.Color.DimGray;
            this.lblCardMsg.Location = new System.Drawing.Point(0, 626);
            this.lblCardMsg.Name = "lblCardMsg";
            this.lblCardMsg.Size = new System.Drawing.Size(900, 80);
            this.lblCardMsg.TabIndex = 45;
            this.lblCardMsg.Text = "스마트폰을 접촉해주세요";
            this.lblCardMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 41);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 44;
            this.lblTitle.Text = "결제취소";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbGif
            // 
            this.pbGif.Location = new System.Drawing.Point(50, 197);
            this.pbGif.Name = "pbGif";
            this.pbGif.Size = new System.Drawing.Size(800, 367);
            this.pbGif.TabIndex = 47;
            this.pbGif.TabStop = false;
            // 
            // FrmSamPayCancelInfoNice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.ClientSize = new System.Drawing.Size(900, 900);
            this.Controls.Add(this.pbGif);
            this.Controls.Add(this.picBoxPrev);
            this.Controls.Add(this.lblCardMsg);
            this.Controls.Add(this.lblTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSamPayCancelInfoNice";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSamPayCancelInfo";
            this.Load += new System.EventHandler(this.FrmSamPayCancelInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
        private System.Windows.Forms.PictureBox picBoxPrev;
        private System.Windows.Forms.Label lblCardMsg;
        private System.Windows.Forms.Label lblTitle;
        private System.IO.Ports.SerialPort serialPortReceipt;
        private System.Windows.Forms.PictureBox pbGif;
    }
}