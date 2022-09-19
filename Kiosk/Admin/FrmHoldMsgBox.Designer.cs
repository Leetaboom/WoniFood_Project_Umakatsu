namespace Kiosk
{
    partial class FrmHoldMsgBox
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
            this.picBoxCancel = new System.Windows.Forms.PictureBox();
            this.picBoxConfirm = new System.Windows.Forms.PictureBox();
            this.lblMsg2 = new System.Windows.Forms.Label();
            this.lblMsg1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxConfirm)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.dbpMain.Controls.Add(this.picBoxCancel);
            this.dbpMain.Controls.Add(this.picBoxConfirm);
            this.dbpMain.Controls.Add(this.lblMsg2);
            this.dbpMain.Controls.Add(this.lblMsg1);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(90, 510);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(900, 900);
            this.dbpMain.TabIndex = 0;
            // 
            // picBoxCancel
            // 
            this.picBoxCancel.BackgroundImage = global::Kiosk.Properties.Resources.btn_cancel;
            this.picBoxCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBoxCancel.Location = new System.Drawing.Point(150, 700);
            this.picBoxCancel.Name = "picBoxCancel";
            this.picBoxCancel.Size = new System.Drawing.Size(200, 117);
            this.picBoxCancel.TabIndex = 42;
            this.picBoxCancel.TabStop = false;
            this.picBoxCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxConfirm
            // 
            this.picBoxConfirm.BackgroundImage = global::Kiosk.Properties.Resources.btn_confirm;
            this.picBoxConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBoxConfirm.Location = new System.Drawing.Point(450, 700);
            this.picBoxConfirm.Name = "picBoxConfirm";
            this.picBoxConfirm.Size = new System.Drawing.Size(300, 117);
            this.picBoxConfirm.TabIndex = 41;
            this.picBoxConfirm.TabStop = false;
            this.picBoxConfirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxConfirm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblMsg2
            // 
            this.lblMsg2.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsg2.ForeColor = System.Drawing.Color.Red;
            this.lblMsg2.Location = new System.Drawing.Point(0, 431);
            this.lblMsg2.Name = "lblMsg2";
            this.lblMsg2.Size = new System.Drawing.Size(900, 100);
            this.lblMsg2.TabIndex = 40;
            this.lblMsg2.Text = "HOLD모드로 실행합니다.";
            this.lblMsg2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMsg1
            // 
            this.lblMsg1.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsg1.Location = new System.Drawing.Point(0, 317);
            this.lblMsg1.Name = "lblMsg1";
            this.lblMsg1.Size = new System.Drawing.Size(900, 100);
            this.lblMsg1.TabIndex = 39;
            this.lblMsg1.Text = "키오스크 사용을 중단하고";
            this.lblMsg1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 41);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "HOLD 모드";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmHoldMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmHoldMsgBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmHoldMsgBox";
            this.Load += new System.EventHandler(this.FrmHoldMsgBox_Load);
            this.dbpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxConfirm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.PictureBox picBoxCancel;
        private System.Windows.Forms.PictureBox picBoxConfirm;
        private System.Windows.Forms.Label lblMsg2;
        private System.Windows.Forms.Label lblMsg1;
        private System.Windows.Forms.Label lblTitle;
    }
}