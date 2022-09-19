namespace Kiosk
{
    partial class FrmCalendar
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
            this.dbpMain = new DoubleBuffer_Panel();
            this.picBoxCancel = new System.Windows.Forms.PictureBox();
            this.picBoxConfirm = new System.Windows.Forms.PictureBox();
            this.monthCalendarAdv1 = new DevComponents.Editors.DateTimeAdv.MonthCalendarAdv();
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
            this.dbpMain.Controls.Add(this.monthCalendarAdv1);
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
            this.picBoxCancel.Location = new System.Drawing.Point(151, 750);
            this.picBoxCancel.Name = "picBoxCancel";
            this.picBoxCancel.Size = new System.Drawing.Size(200, 117);
            this.picBoxCancel.TabIndex = 12;
            this.picBoxCancel.TabStop = false;
            this.picBoxCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxConfirm
            // 
            this.picBoxConfirm.BackgroundImage = global::Kiosk.Properties.Resources.btn_confirm;
            this.picBoxConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBoxConfirm.Location = new System.Drawing.Point(451, 750);
            this.picBoxConfirm.Name = "picBoxConfirm";
            this.picBoxConfirm.Size = new System.Drawing.Size(300, 117);
            this.picBoxConfirm.TabIndex = 11;
            this.picBoxConfirm.TabStop = false;
            this.picBoxConfirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxConfirm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // monthCalendarAdv1
            // 
            this.monthCalendarAdv1.AutoSize = true;
            this.monthCalendarAdv1.BackgroundStyle.Class = "MonthCalendarAdv";
            this.monthCalendarAdv1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.monthCalendarAdv1.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.monthCalendarAdv1.ContainerControlProcessDialogKey = true;
            this.monthCalendarAdv1.DaySize = new System.Drawing.Size(127, 75);
            this.monthCalendarAdv1.DisplayMonth = new System.DateTime(2017, 9, 1, 0, 0, 0, 0);
            this.monthCalendarAdv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.monthCalendarAdv1.Location = new System.Drawing.Point(4, 135);
            this.monthCalendarAdv1.Name = "monthCalendarAdv1";
            this.monthCalendarAdv1.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.monthCalendarAdv1.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.monthCalendarAdv1.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.monthCalendarAdv1.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.monthCalendarAdv1.NavigationBackgroundStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 71.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.monthCalendarAdv1.NavigationButtonSize = new System.Drawing.Size(80, 50);
            this.monthCalendarAdv1.Size = new System.Drawing.Size(891, 583);
            this.monthCalendarAdv1.TabIndex = 4;
            this.monthCalendarAdv1.Text = "monthCalendarAdv1";
            this.monthCalendarAdv1.ItemClick += new System.EventHandler(this.monthCalendarAdv1_ItemClick);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 38);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "원하는 날짜를 입력하세요";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCalendar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCalendar";
            this.Load += new System.EventHandler(this.FrmCalendar_Load);
            this.dbpMain.ResumeLayout(false);
            this.dbpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxConfirm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.PictureBox picBoxCancel;
        private System.Windows.Forms.PictureBox picBoxConfirm;
        public DevComponents.Editors.DateTimeAdv.MonthCalendarAdv monthCalendarAdv1;
        private System.Windows.Forms.Label lblTitle;
    }
}