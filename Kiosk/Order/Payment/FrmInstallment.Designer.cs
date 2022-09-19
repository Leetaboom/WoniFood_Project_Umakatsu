namespace Kiosk
{
    partial class FrmInstallment
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
            this.dbpMain = new Kiosk.DoubleBuffer_Panel();
            this.panelMonth = new Kiosk.DoubleBuffer_Panel();
            this.panelLump = new Kiosk.DoubleBuffer_Panel();
            this.lblLump = new System.Windows.Forms.Label();
            this.picBoxBack = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            this.panelLump.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBack)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.dbpMain.Controls.Add(this.panelMonth);
            this.dbpMain.Controls.Add(this.panelLump);
            this.dbpMain.Controls.Add(this.picBoxBack);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(100, 408);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(900, 900);
            this.dbpMain.TabIndex = 0;
            // 
            // panelMonth
            // 
            this.panelMonth.BackColor = System.Drawing.Color.White;
            this.panelMonth.Location = new System.Drawing.Point(10, 200);
            this.panelMonth.Name = "panelMonth";
            this.panelMonth.Size = new System.Drawing.Size(880, 450);
            this.panelMonth.TabIndex = 13;
            // 
            // panelLump
            // 
            this.panelLump.BackColor = System.Drawing.Color.White;
            this.panelLump.BackgroundImage = global::Kiosk.Properties.Resources.btn_bg_orange;
            this.panelLump.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelLump.Controls.Add(this.lblLump);
            this.panelLump.Location = new System.Drawing.Point(518, 737);
            this.panelLump.Name = "panelLump";
            this.panelLump.Size = new System.Drawing.Size(300, 117);
            this.panelLump.TabIndex = 9;
            // 
            // lblLump
            // 
            this.lblLump.BackColor = System.Drawing.Color.Transparent;
            this.lblLump.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLump.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLump.ForeColor = System.Drawing.Color.White;
            this.lblLump.Location = new System.Drawing.Point(0, 0);
            this.lblLump.Name = "lblLump";
            this.lblLump.Size = new System.Drawing.Size(300, 117);
            this.lblLump.TabIndex = 1;
            this.lblLump.Text = "일시불";
            this.lblLump.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLump.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.lblLump.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxBack
            // 
            this.picBoxBack.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxBack.Location = new System.Drawing.Point(75, 737);
            this.picBoxBack.Name = "picBoxBack";
            this.picBoxBack.Size = new System.Drawing.Size(250, 117);
            this.picBoxBack.TabIndex = 8;
            this.picBoxBack.TabStop = false;
            this.picBoxBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 37);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "할부 개월수 를 선택하세요";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmInstallment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInstallment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmInstallment_Load);
            this.dbpMain.ResumeLayout(false);
            this.panelLump.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.PictureBox picBoxBack;
        private System.Windows.Forms.Label lblTitle;
        private DoubleBuffer_Panel panelMonth;
        private DoubleBuffer_Panel panelLump;
        private System.Windows.Forms.Label lblLump;
    }
}