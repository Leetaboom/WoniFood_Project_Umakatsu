namespace Kiosk
{
    partial class FrmOrderList
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
            this.picBoxSpay = new System.Windows.Forms.PictureBox();
            this.picBoxCard = new System.Windows.Forms.PictureBox();
            this.picBoxBack = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalAmt = new System.Windows.Forms.Label();
            this.woniPanOrderList = new Kiosk.Controler.WoniFillList();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSpay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBack)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_order2;
            this.dbpMain.Controls.Add(this.picBoxSpay);
            this.dbpMain.Controls.Add(this.picBoxCard);
            this.dbpMain.Controls.Add(this.picBoxBack);
            this.dbpMain.Controls.Add(this.label2);
            this.dbpMain.Controls.Add(this.lblTotalAmt);
            this.dbpMain.Controls.Add(this.woniPanOrderList);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(90, 157);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(900, 1600);
            this.dbpMain.TabIndex = 0;
            // 
            // picBoxSpay
            // 
            this.picBoxSpay.BackgroundImage = global::Kiosk.Properties.Resources.btnSamsungPay;
            this.picBoxSpay.Location = new System.Drawing.Point(578, 1394);
            this.picBoxSpay.Name = "picBoxSpay";
            this.picBoxSpay.Size = new System.Drawing.Size(300, 117);
            this.picBoxSpay.TabIndex = 61;
            this.picBoxSpay.TabStop = false;
            this.picBoxSpay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxSpay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxCard
            // 
            this.picBoxCard.BackgroundImage = global::Kiosk.Properties.Resources.btn_card;
            this.picBoxCard.Location = new System.Drawing.Point(250, 1394);
            this.picBoxCard.Name = "picBoxCard";
            this.picBoxCard.Size = new System.Drawing.Size(300, 117);
            this.picBoxCard.TabIndex = 60;
            this.picBoxCard.TabStop = false;
            this.picBoxCard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxCard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxBack
            // 
            this.picBoxBack.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxBack.Location = new System.Drawing.Point(21, 1394);
            this.picBoxBack.Name = "picBoxBack";
            this.picBoxBack.Size = new System.Drawing.Size(200, 117);
            this.picBoxBack.TabIndex = 59;
            this.picBoxBack.TabStop = false;
            this.picBoxBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(0, 1209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 90);
            this.label2.TabIndex = 55;
            this.label2.Text = "합계금액";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAmt
            // 
            this.lblTotalAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTotalAmt.Location = new System.Drawing.Point(227, 1219);
            this.lblTotalAmt.Name = "lblTotalAmt";
            this.lblTotalAmt.Size = new System.Drawing.Size(670, 70);
            this.lblTotalAmt.TabIndex = 58;
            this.lblTotalAmt.Text = "\\ 0";
            this.lblTotalAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // woniPanOrderList
            // 
            this.woniPanOrderList.AutoScroll = true;
            this.woniPanOrderList.BackColor = System.Drawing.Color.White;
            this.woniPanOrderList.Location = new System.Drawing.Point(0, 129);
            this.woniPanOrderList.MinimumSize = new System.Drawing.Size(200, 200);
            this.woniPanOrderList.Name = "woniPanOrderList";
            this.woniPanOrderList.Padding = new System.Windows.Forms.Padding(10);
            this.woniPanOrderList.Size = new System.Drawing.Size(900, 1074);
            this.woniPanOrderList.Space = 0;
            this.woniPanOrderList.TabIndex = 4;
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
            this.lblTitle.Text = "주문내역";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmOrderList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOrderList";
            this.Load += new System.EventHandler(this.FrmOrderList_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            this.dbpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSpay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.PictureBox picBoxSpay;
        private System.Windows.Forms.PictureBox picBoxCard;
        private System.Windows.Forms.PictureBox picBoxBack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalAmt;
        private Controler.WoniFillList woniPanOrderList;
        private System.Windows.Forms.Label lblTitle;
    }
}