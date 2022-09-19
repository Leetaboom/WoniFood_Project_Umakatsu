namespace Kiosk
{
    partial class FrmPayMentProc
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
            this.woniPanel1 = new Kiosk.DoubleBuffer_Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressIndicator = new ProgressIndicator();
            this.lblMsg = new System.Windows.Forms.Label();
            this.picBoxLogo = new System.Windows.Forms.PictureBox();
            this.woniPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // woniPanel1
            // 
            this.woniPanel1.BackColor = System.Drawing.Color.Transparent;
            this.woniPanel1.BackgrounColor = System.Drawing.Color.Transparent;
            this.woniPanel1.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.woniPanel1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.woniPanel1.BorderRadius = 0;
            this.woniPanel1.BorderSize = 0;
            this.woniPanel1.Controls.Add(this.lblTitle);
            this.woniPanel1.Controls.Add(this.label1);
            this.woniPanel1.Controls.Add(this.progressIndicator);
            this.woniPanel1.Controls.Add(this.lblMsg);
            this.woniPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.woniPanel1.Location = new System.Drawing.Point(88, 390);
            this.woniPanel1.Name = "woniPanel1";
            this.woniPanel1.Opacity = 100;
            this.woniPanel1.Size = new System.Drawing.Size(900, 900);
            this.woniPanel1.TabIndex = 0;
            this.woniPanel1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 37);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 38;
            this.lblTitle.Text = "결제 진행중";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(0, 717);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(900, 80);
            this.label1.TabIndex = 37;
            this.label1.Text = "잠시만 기다려 주세요";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressIndicator
            // 
            this.progressIndicator.AutoStart = true;
            this.progressIndicator.BackColor = System.Drawing.Color.Transparent;
            this.progressIndicator.CircleColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.progressIndicator.Location = new System.Drawing.Point(363, 355);
            this.progressIndicator.Name = "progressIndicator";
            this.progressIndicator.Percentage = 0F;
            this.progressIndicator.Size = new System.Drawing.Size(170, 170);
            this.progressIndicator.TabIndex = 35;
            this.progressIndicator.Text = "progressIndicator1";
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsg.ForeColor = System.Drawing.Color.DimGray;
            this.lblMsg.Location = new System.Drawing.Point(0, 637);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(900, 80);
            this.lblMsg.TabIndex = 36;
            this.lblMsg.Text = "결제 진행 중 입니다";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBoxLogo
            // 
            this.picBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.picBoxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxLogo.Location = new System.Drawing.Point(269, -3);
            this.picBoxLogo.Name = "picBoxLogo";
            this.picBoxLogo.Size = new System.Drawing.Size(536, 140);
            this.picBoxLogo.TabIndex = 6;
            this.picBoxLogo.TabStop = false;
            this.picBoxLogo.DoubleClick += new System.EventHandler(this.picBoxLogo_DoubleClick);
            // 
            // FrmPayMentProc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.picBoxLogo);
            this.Controls.Add(this.woniPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPayMentProc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FrmPayMentProc";
            this.Load += new System.EventHandler(this.FrmPayMentProc_Load);
            this.woniPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel woniPanel1;
        private System.Windows.Forms.Label label1;
        private ProgressIndicator progressIndicator;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picBoxLogo;
    }
}