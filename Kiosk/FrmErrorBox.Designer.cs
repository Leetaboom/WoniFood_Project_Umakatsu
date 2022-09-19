namespace Kiosk
{
    partial class FrmErrorBox
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
            this.picBoxConfirm = new System.Windows.Forms.PictureBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxConfirm)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.dbpMain.Controls.Add(this.picBoxConfirm);
            this.dbpMain.Controls.Add(this.lblMsg);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(60, 268);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(900, 900);
            this.dbpMain.TabIndex = 0;
            // 
            // picBoxConfirm
            // 
            this.picBoxConfirm.BackgroundImage = global::Kiosk.Properties.Resources.btn_confirm;
            this.picBoxConfirm.Location = new System.Drawing.Point(300, 706);
            this.picBoxConfirm.Name = "picBoxConfirm";
            this.picBoxConfirm.Size = new System.Drawing.Size(300, 117);
            this.picBoxConfirm.TabIndex = 4;
            this.picBoxConfirm.TabStop = false;
            this.picBoxConfirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxConfirm_MouseDown);
            this.picBoxConfirm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxConfirm_MouseUp);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsg.Location = new System.Drawing.Point(0, 300);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(900, 264);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "에러 메시지";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 38);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "타이틀";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmErrorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmErrorBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmErrorBox";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmErrorBox_Load);
            this.dbpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxConfirm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.PictureBox picBoxConfirm;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblTitle;
    }
}