namespace Kiosk
{
    partial class FrmSetMenuOptPopup
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
            this.panelOk = new Kiosk.DoubleBuffer_Panel();
            this.lblSelectOk = new System.Windows.Forms.Label();
            this.picBoxBack = new System.Windows.Forms.PictureBox();
            this.line4 = new DevComponents.DotNetBar.Controls.Line();
            this.lblPage = new System.Windows.Forms.Label();
            this.picBoxNext = new System.Windows.Forms.PictureBox();
            this.picBoxPrev = new System.Windows.Forms.PictureBox();
            this.panelOptItem = new Kiosk.DoubleBuffer_Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.line3 = new DevComponents.DotNetBar.Controls.Line();
            this.panelBasic = new Kiosk.DoubleBuffer_Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            this.panelOk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_b;
            this.dbpMain.Controls.Add(this.panelOk);
            this.dbpMain.Controls.Add(this.picBoxBack);
            this.dbpMain.Controls.Add(this.line4);
            this.dbpMain.Controls.Add(this.lblPage);
            this.dbpMain.Controls.Add(this.picBoxNext);
            this.dbpMain.Controls.Add(this.picBoxPrev);
            this.dbpMain.Controls.Add(this.panelOptItem);
            this.dbpMain.Controls.Add(this.label5);
            this.dbpMain.Controls.Add(this.line3);
            this.dbpMain.Controls.Add(this.panelBasic);
            this.dbpMain.Controls.Add(this.label4);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(91, 153);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(900, 1600);
            this.dbpMain.TabIndex = 0;
            // 
            // panelOk
            // 
            this.panelOk.BackColor = System.Drawing.Color.White;
            this.panelOk.BackgroundImage = global::Kiosk.Properties.Resources.btn_rad3;
            this.panelOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelOk.Controls.Add(this.lblSelectOk);
            this.panelOk.Location = new System.Drawing.Point(463, 1457);
            this.panelOk.Name = "panelOk";
            this.panelOk.Size = new System.Drawing.Size(400, 117);
            this.panelOk.TabIndex = 43;
            // 
            // lblSelectOk
            // 
            this.lblSelectOk.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSelectOk.ForeColor = System.Drawing.Color.White;
            this.lblSelectOk.Location = new System.Drawing.Point(0, 0);
            this.lblSelectOk.Name = "lblSelectOk";
            this.lblSelectOk.Size = new System.Drawing.Size(400, 117);
            this.lblSelectOk.TabIndex = 1;
            this.lblSelectOk.Text = "세트선택완료";
            this.lblSelectOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSelectOk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.lblSelectOk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxBack
            // 
            this.picBoxBack.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxBack.Location = new System.Drawing.Point(42, 1457);
            this.picBoxBack.Name = "picBoxBack";
            this.picBoxBack.Size = new System.Drawing.Size(200, 117);
            this.picBoxBack.TabIndex = 42;
            this.picBoxBack.TabStop = false;
            this.picBoxBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // line4
            // 
            this.line4.BackgroundImage = global::Kiosk.Properties.Resources.btn_rad3;
            this.line4.ForeColor = System.Drawing.Color.Transparent;
            this.line4.Location = new System.Drawing.Point(0, 1413);
            this.line4.Name = "line4";
            this.line4.Size = new System.Drawing.Size(900, 10);
            this.line4.TabIndex = 41;
            this.line4.Text = "line4";
            // 
            // lblPage
            // 
            this.lblPage.BackColor = System.Drawing.Color.Transparent;
            this.lblPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPage.Location = new System.Drawing.Point(392, 1350);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(117, 43);
            this.lblPage.TabIndex = 40;
            this.lblPage.Text = "1 / 1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBoxNext
            // 
            this.picBoxNext.BackgroundImage = global::Kiosk.Properties.Resources.btn_page_next;
            this.picBoxNext.Location = new System.Drawing.Point(512, 1350);
            this.picBoxNext.Name = "picBoxNext";
            this.picBoxNext.Size = new System.Drawing.Size(42, 43);
            this.picBoxNext.TabIndex = 39;
            this.picBoxNext.TabStop = false;
            this.picBoxNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxNext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxPrev
            // 
            this.picBoxPrev.BackgroundImage = global::Kiosk.Properties.Resources.btn_page_back;
            this.picBoxPrev.Location = new System.Drawing.Point(347, 1350);
            this.picBoxPrev.Name = "picBoxPrev";
            this.picBoxPrev.Size = new System.Drawing.Size(42, 43);
            this.picBoxPrev.TabIndex = 38;
            this.picBoxPrev.TabStop = false;
            this.picBoxPrev.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxPrev.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // panelOptItem
            // 
            this.panelOptItem.BackColor = System.Drawing.Color.White;
            this.panelOptItem.Location = new System.Drawing.Point(0, 497);
            this.panelOptItem.Name = "panelOptItem";
            this.panelOptItem.Size = new System.Drawing.Size(900, 840);
            this.panelOptItem.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(0, 444);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(900, 50);
            this.label5.TabIndex = 31;
            this.label5.Text = "세트 메뉴 음료를 선택해주세요";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // line3
            // 
            this.line3.BackgroundImage = global::Kiosk.Properties.Resources.btn_rad3;
            this.line3.ForeColor = System.Drawing.Color.Transparent;
            this.line3.Location = new System.Drawing.Point(0, 390);
            this.line3.Name = "line3";
            this.line3.Size = new System.Drawing.Size(900, 10);
            this.line3.TabIndex = 30;
            this.line3.Text = "line3";
            // 
            // panelBasic
            // 
            this.panelBasic.BackColor = System.Drawing.Color.White;
            this.panelBasic.Location = new System.Drawing.Point(0, 179);
            this.panelBasic.Name = "panelBasic";
            this.panelBasic.Size = new System.Drawing.Size(900, 210);
            this.panelBasic.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(0, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(900, 50);
            this.label4.TabIndex = 25;
            this.label4.Text = "세트 메뉴 감자 사이즈를 선택해주세요";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Location = new System.Drawing.Point(0, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 24;
            this.lblTitle.Text = "세트메뉴 선택";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmSetMenuOptPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSetMenuOptPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSetMentOptPopup";
            this.Load += new System.EventHandler(this.FrmSetMentOptPopup_Load);
            this.dbpMain.ResumeLayout(false);
            this.panelOk.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private DoubleBuffer_Panel panelOk;
        private System.Windows.Forms.Label lblSelectOk;
        private System.Windows.Forms.PictureBox picBoxBack;
        private DevComponents.DotNetBar.Controls.Line line4;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.PictureBox picBoxNext;
        private System.Windows.Forms.PictureBox picBoxPrev;
        private DoubleBuffer_Panel panelOptItem;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.Line line3;
        private DoubleBuffer_Panel panelBasic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTitle;
    }
}