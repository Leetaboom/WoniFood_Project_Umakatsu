namespace Kiosk
{
    partial class FrmNetworkErr
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
            this.picBoxCconfirm = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCconfirm)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.dbpMain.Controls.Add(this.picBoxCconfirm);
            this.dbpMain.Controls.Add(this.label4);
            this.dbpMain.Controls.Add(this.label3);
            this.dbpMain.Controls.Add(this.label2);
            this.dbpMain.Controls.Add(this.label5);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(96, 370);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(900, 900);
            this.dbpMain.TabIndex = 0;
            // 
            // picBoxCconfirm
            // 
            this.picBoxCconfirm.BackgroundImage = global::Kiosk.Properties.Resources.btn_confirm;
            this.picBoxCconfirm.Location = new System.Drawing.Point(296, 716);
            this.picBoxCconfirm.Name = "picBoxCconfirm";
            this.picBoxCconfirm.Size = new System.Drawing.Size(300, 117);
            this.picBoxCconfirm.TabIndex = 47;
            this.picBoxCconfirm.TabStop = false;
            this.picBoxCconfirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxCconfirm_MouseDown);
            this.picBoxCconfirm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxCconfirm_MouseUp);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(0, 585);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(900, 80);
            this.label4.TabIndex = 46;
            this.label4.Text = "인터넷 연결 확인 후 다시 실행 해주세요.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(0, 432);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(900, 80);
            this.label3.TabIndex = 45;
            this.label3.Text = "인터넷 서비스 문제 일 수 있습니다.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(0, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(900, 80);
            this.label2.TabIndex = 44;
            this.label2.Text = "사용자 네트워크 연결이 불안정 하거나";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(0, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(900, 80);
            this.label5.TabIndex = 43;
            this.label5.Text = "서버에 연결 할 수 없습니다.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 37);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "네트워크 연결오류";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmNetworkErr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmNetworkErr";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmNetworkErr";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmNetworkErr_Load);
            this.dbpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCconfirm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.PictureBox picBoxCconfirm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTitle;
    }
}