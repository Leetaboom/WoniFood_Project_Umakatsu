namespace Kiosk
{
    partial class FrmCardPayMentDaou
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardPayMentDaou));
            this.woniPanel1 = new Kiosk.Controler.WoniPanel();
            //this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.picBoxPrev = new System.Windows.Forms.PictureBox();
            this.progressIndicator1 = new ProgressIndicator();
            this.lblCardMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.serialPortReceipt = new System.IO.Ports.SerialPort(this.components);
            this.serialPortKitchen = new System.IO.Ports.SerialPort(this.components);
            this.serialPortCounter = new System.IO.Ports.SerialPort(this.components);
            this.woniPanel1.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).BeginInit();
            this.SuspendLayout();
            // 
            // woniPanel1
            // 
            this.woniPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.woniPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            //this.woniPanel1.Controls.Add(this.axShockwaveFlash1);
            this.woniPanel1.Controls.Add(this.picBoxPrev);
            this.woniPanel1.Controls.Add(this.progressIndicator1);
            this.woniPanel1.Controls.Add(this.lblCardMsg);
            this.woniPanel1.Controls.Add(this.label1);
            this.woniPanel1.Controls.Add(this.lblTitle);
            this.woniPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.woniPanel1.Location = new System.Drawing.Point(90, 510);
            this.woniPanel1.Name = "woniPanel1";
            this.woniPanel1.Size = new System.Drawing.Size(900, 900);
            this.woniPanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.woniPanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.woniPanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.woniPanel1.Style.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.woniPanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.woniPanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.woniPanel1.Style.GradientAngle = 90;
            this.woniPanel1.TabIndex = 4;
            // 
            // axShockwaveFlash1
            // 
            //this.axShockwaveFlash1.Enabled = true;
            //this.axShockwaveFlash1.Location = new System.Drawing.Point(77, 197);
            //this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            //this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            //this.axShockwaveFlash1.Size = new System.Drawing.Size(760, 330);
            //this.axShockwaveFlash1.TabIndex = 50;
            // 
            // picBoxPrev
            // 
            this.picBoxPrev.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxPrev.Location = new System.Drawing.Point(349, 730);
            this.picBoxPrev.Name = "picBoxPrev";
            this.picBoxPrev.Size = new System.Drawing.Size(200, 117);
            this.picBoxPrev.TabIndex = 48;
            this.picBoxPrev.TabStop = false;
            this.picBoxPrev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxPrev_MouseDown);
            this.picBoxPrev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxPrev_MouseUp);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.AutoStart = true;
            this.progressIndicator1.BackColor = System.Drawing.Color.Transparent;
            this.progressIndicator1.CircleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.progressIndicator1.Location = new System.Drawing.Point(632, 638);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(107, 107);
            this.progressIndicator1.TabIndex = 47;
            this.progressIndicator1.Text = "progressIndicator1";
            // 
            // lblCardMsg
            // 
            this.lblCardMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblCardMsg.Font = new System.Drawing.Font("나눔스퀘어", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCardMsg.ForeColor = System.Drawing.Color.DimGray;
            this.lblCardMsg.Location = new System.Drawing.Point(0, 557);
            this.lblCardMsg.Name = "lblCardMsg";
            this.lblCardMsg.Size = new System.Drawing.Size(900, 92);
            this.lblCardMsg.TabIndex = 45;
            this.lblCardMsg.Text = "카드를 끝까지 밀어 넣어주세요";
            this.lblCardMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("나눔스퀘어", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(116, 638);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(510, 107);
            this.label1.TabIndex = 46;
            this.label1.Text = "카드를 확인중입니다";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("나눔스퀘어 Bold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 41);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "결제안내";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmCardPayMentDaou
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.woniPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCardPayMentDaou";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCardPayMentDaou";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardPayMentDaou_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardPayMentDaou_Load);
            this.woniPanel1.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controler.WoniPanel woniPanel1;
        private System.Windows.Forms.PictureBox picBoxPrev;
        private ProgressIndicator progressIndicator1;
        private System.Windows.Forms.Label lblCardMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.IO.Ports.SerialPort serialPortReceipt;
        private System.IO.Ports.SerialPort serialPortKitchen;
        private System.IO.Ports.SerialPort serialPortCounter;
        //private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
    }
}