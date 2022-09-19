namespace Kiosk
{
    partial class FrmMenuListToGo
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
            this.picBoxPageNext = new System.Windows.Forms.PictureBox();
            this.picBoxPagePrev = new System.Windows.Forms.PictureBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.picBoxPayMent = new System.Windows.Forms.PictureBox();
            this.picBoxAllCancel = new System.Windows.Forms.PictureBox();
            this.lblTotalAmt = new System.Windows.Forms.Label();
            this.picBoxEatin = new System.Windows.Forms.PictureBox();
            this.picBoxTakeout = new System.Windows.Forms.PictureBox();
            this.picBoxMain = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.woniFillList1 = new Kiosk.Controler.WoniFillList();
            this.panelMenuList = new Kiosk.DoubleBuffer_Panel();
            this.panelMenuGrp = new Kiosk.DoubleBuffer_Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPageNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPagePrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPayMent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAllCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEatin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTakeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxPageNext
            // 
            this.picBoxPageNext.BackgroundImage = global::Kiosk.Properties.Resources.btn_right;
            this.picBoxPageNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxPageNext.Location = new System.Drawing.Point(603, 1342);
            this.picBoxPageNext.Name = "picBoxPageNext";
            this.picBoxPageNext.Size = new System.Drawing.Size(84, 43);
            this.picBoxPageNext.TabIndex = 42;
            this.picBoxPageNext.TabStop = false;
            this.picBoxPageNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxPageNext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxPagePrev
            // 
            this.picBoxPagePrev.BackgroundImage = global::Kiosk.Properties.Resources.btn_left;
            this.picBoxPagePrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxPagePrev.Location = new System.Drawing.Point(396, 1342);
            this.picBoxPagePrev.Name = "picBoxPagePrev";
            this.picBoxPagePrev.Size = new System.Drawing.Size(84, 43);
            this.picBoxPagePrev.TabIndex = 41;
            this.picBoxPagePrev.TabStop = false;
            this.picBoxPagePrev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxPagePrev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblPage
            // 
            this.lblPage.BackColor = System.Drawing.Color.Transparent;
            this.lblPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPage.Location = new System.Drawing.Point(483, 1342);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(117, 43);
            this.lblPage.TabIndex = 40;
            this.lblPage.Text = "1 / 1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBoxPayMent
            // 
            this.picBoxPayMent.BackgroundImage = global::Kiosk.Properties.Resources.btn_payment;
            this.picBoxPayMent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxPayMent.Location = new System.Drawing.Point(741, 1790);
            this.picBoxPayMent.Name = "picBoxPayMent";
            this.picBoxPayMent.Size = new System.Drawing.Size(250, 85);
            this.picBoxPayMent.TabIndex = 50;
            this.picBoxPayMent.TabStop = false;
            this.picBoxPayMent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxPayMent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxAllCancel
            // 
            this.picBoxAllCancel.BackgroundImage = global::Kiosk.Properties.Resources.btn_cancel_all;
            this.picBoxAllCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxAllCancel.Location = new System.Drawing.Point(368, 1790);
            this.picBoxAllCancel.Name = "picBoxAllCancel";
            this.picBoxAllCancel.Size = new System.Drawing.Size(250, 85);
            this.picBoxAllCancel.TabIndex = 49;
            this.picBoxAllCancel.TabStop = false;
            this.picBoxAllCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxAllCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblTotalAmt
            // 
            this.lblTotalAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTotalAmt.Location = new System.Drawing.Point(849, 1707);
            this.lblTotalAmt.Name = "lblTotalAmt";
            this.lblTotalAmt.Size = new System.Drawing.Size(196, 62);
            this.lblTotalAmt.TabIndex = 51;
            this.lblTotalAmt.Text = "\\ 0";
            this.lblTotalAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBoxEatin
            // 
            this.picBoxEatin.BackgroundImage = global::Kiosk.Properties.Resources.icon_title_eatin_dis;
            this.picBoxEatin.Location = new System.Drawing.Point(12, 54);
            this.picBoxEatin.Name = "picBoxEatin";
            this.picBoxEatin.Size = new System.Drawing.Size(68, 68);
            this.picBoxEatin.TabIndex = 67;
            this.picBoxEatin.TabStop = false;
            // 
            // picBoxTakeout
            // 
            this.picBoxTakeout.BackgroundImage = global::Kiosk.Properties.Resources.icon_title_takeout_dis;
            this.picBoxTakeout.Location = new System.Drawing.Point(86, 54);
            this.picBoxTakeout.Name = "picBoxTakeout";
            this.picBoxTakeout.Size = new System.Drawing.Size(68, 68);
            this.picBoxTakeout.TabIndex = 66;
            this.picBoxTakeout.TabStop = false;
            // 
            // picBoxMain
            // 
            this.picBoxMain.BackgroundImage = global::Kiosk.Properties.Resources.btn_title_home4;
            this.picBoxMain.Location = new System.Drawing.Point(160, 52);
            this.picBoxMain.Name = "picBoxMain";
            this.picBoxMain.Size = new System.Drawing.Size(72, 72);
            this.picBoxMain.TabIndex = 65;
            this.picBoxMain.TabStop = false;
            this.picBoxMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // woniFillList1
            // 
            this.woniFillList1.AutoScroll = true;
            this.woniFillList1.BackColor = System.Drawing.Color.White;
            this.woniFillList1.Location = new System.Drawing.Point(31, 1499);
            this.woniFillList1.MinimumSize = new System.Drawing.Size(200, 200);
            this.woniFillList1.Name = "woniFillList1";
            this.woniFillList1.Padding = new System.Windows.Forms.Padding(10);
            this.woniFillList1.Size = new System.Drawing.Size(643, 271);
            this.woniFillList1.Space = 0;
            this.woniFillList1.TabIndex = 58;
            // 
            // panelMenuList
            // 
            this.panelMenuList.BackColor = System.Drawing.Color.White;
            this.panelMenuList.BorderColor = System.Drawing.Color.Black;
            this.panelMenuList.Location = new System.Drawing.Point(21, 300);
            this.panelMenuList.Name = "panelMenuList";
            this.panelMenuList.Size = new System.Drawing.Size(1040, 1032);
            this.panelMenuList.TabIndex = 11;
            // 
            // panelMenuGrp
            // 
            this.panelMenuGrp.BackColor = System.Drawing.Color.White;
            this.panelMenuGrp.BorderColor = System.Drawing.Color.Black;
            this.panelMenuGrp.Location = new System.Drawing.Point(21, 203);
            this.panelMenuGrp.Name = "panelMenuGrp";
            this.panelMenuGrp.Size = new System.Drawing.Size(1040, 95);
            this.panelMenuGrp.TabIndex = 7;
            // 
            // FrmMenuListToGo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Kiosk.Properties.Resources.bg_menu;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.picBoxEatin);
            this.Controls.Add(this.picBoxTakeout);
            this.Controls.Add(this.picBoxMain);
            this.Controls.Add(this.woniFillList1);
            this.Controls.Add(this.lblTotalAmt);
            this.Controls.Add(this.picBoxPayMent);
            this.Controls.Add(this.picBoxAllCancel);
            this.Controls.Add(this.picBoxPageNext);
            this.Controls.Add(this.picBoxPagePrev);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.panelMenuList);
            this.Controls.Add(this.panelMenuGrp);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMenuListToGo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMenuListToGo";
            this.Load += new System.EventHandler(this.FrmMenuList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPageNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPagePrev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPayMent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxAllCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEatin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTakeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DoubleBuffer_Panel panelMenuGrp;
        private DoubleBuffer_Panel panelMenuList;
        private System.Windows.Forms.PictureBox picBoxPageNext;
        private System.Windows.Forms.PictureBox picBoxPagePrev;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.PictureBox picBoxPayMent;
        private System.Windows.Forms.PictureBox picBoxAllCancel;
        private System.Windows.Forms.Label lblTotalAmt;
        private Controler.WoniFillList woniFillList1;
        private System.Windows.Forms.PictureBox picBoxEatin;
        private System.Windows.Forms.PictureBox picBoxTakeout;
        private System.Windows.Forms.PictureBox picBoxMain;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}