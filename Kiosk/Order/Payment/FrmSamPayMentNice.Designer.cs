using System.Data.Common;

namespace Kiosk
{
    partial class FrmSamPayMentNice
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
            this.dbpMain = new Kiosk.DoubleBuffer_Panel();
            this.pbGif = new System.Windows.Forms.PictureBox();
            this.picBoxPrev = new System.Windows.Forms.PictureBox();
            this.progressIndicator1 = new ProgressIndicator();
            this.lblCardMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.serialPortCounter = new System.IO.Ports.SerialPort(this.components);
            this.serialPortKitchen = new System.IO.Ports.SerialPort(this.components);
            this.serialPortReceipt = new System.IO.Ports.SerialPort(this.components);
            this.dbpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.dbpMain.Controls.Add(this.pbGif);
            this.dbpMain.Controls.Add(this.picBoxPrev);
            this.dbpMain.Controls.Add(this.progressIndicator1);
            this.dbpMain.Controls.Add(this.lblCardMsg);
            this.dbpMain.Controls.Add(this.label1);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(90, 510);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(900, 900);
            this.dbpMain.TabIndex = 8;
            // 
            // pbGif
            // 
            this.pbGif.Location = new System.Drawing.Point(51, 168);
            this.pbGif.Name = "pbGif";
            this.pbGif.Size = new System.Drawing.Size(800, 367);
            this.pbGif.TabIndex = 49;
            this.pbGif.TabStop = false;
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
            this.progressIndicator1.Location = new System.Drawing.Point(685, 638);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(107, 107);
            this.progressIndicator1.TabIndex = 47;
            this.progressIndicator1.Text = "progressIndicator1";
            // 
            // lblCardMsg
            // 
            this.lblCardMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblCardMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCardMsg.ForeColor = System.Drawing.Color.DimGray;
            this.lblCardMsg.Location = new System.Drawing.Point(0, 547);
            this.lblCardMsg.Name = "lblCardMsg";
            this.lblCardMsg.Size = new System.Drawing.Size(900, 92);
            this.lblCardMsg.TabIndex = 45;
            this.lblCardMsg.Text = "삼성페이 스마트폰을 접촉해주세요";
            this.lblCardMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(103, 638);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(555, 107);
            this.label1.TabIndex = 46;
            this.label1.Text = "삼성페이 확인 중 입니다";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 36);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "결제안내";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmSamPayMentNice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSamPayMentNice";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSamPayMentNice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSamPayMentNice_FormClosing);
            this.Load += new System.EventHandler(this.FrmSamPayMentNice_Load);
            this.dbpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.PictureBox picBoxPrev;
        private ProgressIndicator progressIndicator1;
        private System.Windows.Forms.Label lblCardMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.IO.Ports.SerialPort serialPortCounter;
        private System.IO.Ports.SerialPort serialPortKitchen;
        private System.IO.Ports.SerialPort serialPortReceipt;
        private System.Windows.Forms.PictureBox pbGif;
    }
}