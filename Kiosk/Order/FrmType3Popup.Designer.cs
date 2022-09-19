namespace Kiosk
{
    partial class FrmType3Popup
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
            this.panelSingle = new Kiosk.DoubleBuffer_Panel();
            this.lblSingleAmt = new System.Windows.Forms.Label();
            this.lblSingleTitle = new System.Windows.Forms.Label();
            this.picBoxBack = new System.Windows.Forms.PictureBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.picBoxProductImg = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProductMemo = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            this.panelSingle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxProductImg)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgrounColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_s;
            this.dbpMain.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.dbpMain.BorderRadius = 0;
            this.dbpMain.BorderSize = 0;
            this.dbpMain.Controls.Add(this.lblProductMemo);
            this.dbpMain.Controls.Add(this.panelSingle);
            this.dbpMain.Controls.Add(this.picBoxBack);
            this.dbpMain.Controls.Add(this.lblProductName);
            this.dbpMain.Controls.Add(this.picBoxProductImg);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(96, 208);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Opacity = 100;
            this.dbpMain.Size = new System.Drawing.Size(900, 933);
            this.dbpMain.TabIndex = 0;
            this.dbpMain.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // panelSingle
            // 
            this.panelSingle.BackColor = System.Drawing.Color.White;
            this.panelSingle.BackgrounColor = System.Drawing.Color.White;
            this.panelSingle.BackgroundImage = global::Kiosk.Properties.Resources.btn_bg_orange;
            this.panelSingle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSingle.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.panelSingle.BorderRadius = 0;
            this.panelSingle.BorderSize = 0;
            this.panelSingle.Controls.Add(this.lblSingleAmt);
            this.panelSingle.Controls.Add(this.lblSingleTitle);
            this.panelSingle.Location = new System.Drawing.Point(621, 793);
            this.panelSingle.Name = "panelSingle";
            this.panelSingle.Opacity = 100;
            this.panelSingle.Size = new System.Drawing.Size(250, 117);
            this.panelSingle.TabIndex = 8;
            this.panelSingle.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblSingleAmt
            // 
            this.lblSingleAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblSingleAmt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSingleAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSingleAmt.ForeColor = System.Drawing.Color.White;
            this.lblSingleAmt.Location = new System.Drawing.Point(0, 59);
            this.lblSingleAmt.Name = "lblSingleAmt";
            this.lblSingleAmt.Size = new System.Drawing.Size(250, 58);
            this.lblSingleAmt.TabIndex = 3;
            this.lblSingleAmt.Text = "3,200원";
            this.lblSingleAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSingleAmt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.lblSingleAmt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblSingleTitle
            // 
            this.lblSingleTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSingleTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSingleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSingleTitle.ForeColor = System.Drawing.Color.White;
            this.lblSingleTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSingleTitle.Name = "lblSingleTitle";
            this.lblSingleTitle.Size = new System.Drawing.Size(250, 58);
            this.lblSingleTitle.TabIndex = 2;
            this.lblSingleTitle.Text = "단품";
            this.lblSingleTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSingleTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.lblSingleTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxBack
            // 
            this.picBoxBack.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxBack.Location = new System.Drawing.Point(28, 793);
            this.picBoxBack.Name = "picBoxBack";
            this.picBoxBack.Size = new System.Drawing.Size(200, 117);
            this.picBoxBack.TabIndex = 7;
            this.picBoxBack.TabStop = false;
            this.picBoxBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProductName.Location = new System.Drawing.Point(0, 553);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(900, 75);
            this.lblProductName.TabIndex = 6;
            this.lblProductName.Text = "버거 대표명";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBoxProductImg
            // 
            this.picBoxProductImg.BackColor = System.Drawing.Color.Transparent;
            this.picBoxProductImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBoxProductImg.Location = new System.Drawing.Point(250, 200);
            this.picBoxProductImg.Name = "picBoxProductImg";
            this.picBoxProductImg.Size = new System.Drawing.Size(400, 350);
            this.picBoxProductImg.TabIndex = 5;
            this.picBoxProductImg.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 41);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Type3 Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProductMemo
            // 
            this.lblProductMemo.BackColor = System.Drawing.Color.Transparent;
            this.lblProductMemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lblProductMemo.Location = new System.Drawing.Point(0, 628);
            this.lblProductMemo.Name = "lblProductMemo";
            this.lblProductMemo.Size = new System.Drawing.Size(900, 158);
            this.lblProductMemo.TabIndex = 9;
            this.lblProductMemo.Text = "메뉴 대표명";
            this.lblProductMemo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FrmType3Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmType3Popup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmType3PopUp";
            this.Load += new System.EventHandler(this.FrmType3Popup_Load);
            this.dbpMain.ResumeLayout(false);
            this.panelSingle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxProductImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.Label lblTitle;
        private DoubleBuffer_Panel panelSingle;
        private System.Windows.Forms.Label lblSingleAmt;
        private System.Windows.Forms.Label lblSingleTitle;
        private System.Windows.Forms.PictureBox picBoxBack;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.PictureBox picBoxProductImg;
        private System.Windows.Forms.Label lblProductMemo;
    }
}