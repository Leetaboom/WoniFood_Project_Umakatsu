namespace Kiosk
{
    partial class FrmKioskClose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKioskClose));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.serialPortClose = new System.IO.Ports.SerialPort(this.components);
            this.dbpMain = new Kiosk.DoubleBuffer_Panel();
            this.lblEndAmt = new System.Windows.Forms.Label();
            this.picBoxClose = new System.Windows.Forms.PictureBox();
            this.picBoxCancel = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvEtcList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSalesList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKioskNum = new System.Windows.Forms.Label();
            this.lblSalesDate = new System.Windows.Forms.Label();
            this.labelX2 = new System.Windows.Forms.Label();
            this.labelX1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesList)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_deadline;
            this.dbpMain.Controls.Add(this.lblEndAmt);
            this.dbpMain.Controls.Add(this.picBoxClose);
            this.dbpMain.Controls.Add(this.picBoxCancel);
            this.dbpMain.Controls.Add(this.label6);
            this.dbpMain.Controls.Add(this.dgvEtcList);
            this.dbpMain.Controls.Add(this.dgvSalesList);
            this.dbpMain.Controls.Add(this.label5);
            this.dbpMain.Controls.Add(this.label4);
            this.dbpMain.Controls.Add(this.lblKioskNum);
            this.dbpMain.Controls.Add(this.lblSalesDate);
            this.dbpMain.Controls.Add(this.labelX2);
            this.dbpMain.Controls.Add(this.labelX1);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Location = new System.Drawing.Point(76, 183);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(900, 1200);
            this.dbpMain.TabIndex = 0;
            // 
            // lblEndAmt
            // 
            this.lblEndAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblEndAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblEndAmt.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblEndAmt.Location = new System.Drawing.Point(263, 935);
            this.lblEndAmt.Name = "lblEndAmt";
            this.lblEndAmt.Size = new System.Drawing.Size(588, 80);
            this.lblEndAmt.TabIndex = 345;
            this.lblEndAmt.Text = "\\ 0";
            this.lblEndAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picBoxClose
            // 
            this.picBoxClose.BackgroundImage = global::Kiosk.Properties.Resources.btn_continue;
            this.picBoxClose.Location = new System.Drawing.Point(450, 1045);
            this.picBoxClose.Name = "picBoxClose";
            this.picBoxClose.Size = new System.Drawing.Size(300, 117);
            this.picBoxClose.TabIndex = 344;
            this.picBoxClose.TabStop = false;
            this.picBoxClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxCancel
            // 
            this.picBoxCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBoxCancel.BackgroundImage")));
            this.picBoxCancel.Location = new System.Drawing.Point(150, 1045);
            this.picBoxCancel.Name = "picBoxCancel";
            this.picBoxCancel.Size = new System.Drawing.Size(200, 117);
            this.picBoxCancel.TabIndex = 343;
            this.picBoxCancel.TabStop = false;
            this.picBoxCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(12, 935);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(245, 80);
            this.label6.TabIndex = 342;
            this.label6.Text = "최종영업 :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvEtcList
            // 
            this.dgvEtcList.AllowUserToAddRows = false;
            this.dgvEtcList.AllowUserToResizeColumns = false;
            this.dgvEtcList.AllowUserToResizeRows = false;
            this.dgvEtcList.BackgroundColor = System.Drawing.Color.White;
            this.dgvEtcList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvEtcList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEtcList.ColumnHeadersVisible = false;
            this.dgvEtcList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEtcList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEtcList.Enabled = false;
            this.dgvEtcList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvEtcList.Location = new System.Drawing.Point(466, 421);
            this.dgvEtcList.Name = "dgvEtcList";
            this.dgvEtcList.ReadOnly = true;
            this.dgvEtcList.RowHeadersVisible = false;
            this.dgvEtcList.RowTemplate.Height = 23;
            this.dgvEtcList.Size = new System.Drawing.Size(420, 500);
            this.dgvEtcList.TabIndex = 341;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "타이틀";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 215;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "금액";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dgvSalesList
            // 
            this.dgvSalesList.AllowUserToAddRows = false;
            this.dgvSalesList.AllowUserToResizeColumns = false;
            this.dgvSalesList.AllowUserToResizeRows = false;
            this.dgvSalesList.BackgroundColor = System.Drawing.Color.White;
            this.dgvSalesList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvSalesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesList.ColumnHeadersVisible = false;
            this.dgvSalesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSalesList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSalesList.Enabled = false;
            this.dgvSalesList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvSalesList.Location = new System.Drawing.Point(13, 421);
            this.dgvSalesList.Name = "dgvSalesList";
            this.dgvSalesList.ReadOnly = true;
            this.dgvSalesList.RowHeadersVisible = false;
            this.dgvSalesList.RowTemplate.Height = 23;
            this.dgvSalesList.Size = new System.Drawing.Size(420, 500);
            this.dgvSalesList.TabIndex = 340;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "타이틀";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "금액";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 217;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(465, 338);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(420, 80);
            this.label5.TabIndex = 339;
            this.label5.Text = "기 타 내 역";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(12, 338);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(420, 80);
            this.label4.TabIndex = 338;
            this.label4.Text = "매 출 내 역";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKioskNum
            // 
            this.lblKioskNum.BackColor = System.Drawing.Color.Transparent;
            this.lblKioskNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblKioskNum.ForeColor = System.Drawing.Color.Salmon;
            this.lblKioskNum.Location = new System.Drawing.Point(323, 227);
            this.lblKioskNum.Name = "lblKioskNum";
            this.lblKioskNum.Size = new System.Drawing.Size(562, 80);
            this.lblKioskNum.TabIndex = 336;
            this.lblKioskNum.Text = "1";
            this.lblKioskNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSalesDate
            // 
            this.lblSalesDate.BackColor = System.Drawing.Color.Transparent;
            this.lblSalesDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSalesDate.ForeColor = System.Drawing.Color.Salmon;
            this.lblSalesDate.Location = new System.Drawing.Point(323, 141);
            this.lblSalesDate.Name = "lblSalesDate";
            this.lblSalesDate.Size = new System.Drawing.Size(562, 80);
            this.lblSalesDate.TabIndex = 335;
            this.lblSalesDate.Text = "2017-06-03";
            this.lblSalesDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Beige;
            this.labelX2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(13, 227);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(304, 80);
            this.labelX2.TabIndex = 334;
            this.labelX2.Text = "Kiosk.";
            this.labelX2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Beige;
            this.labelX1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(13, 141);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(304, 80);
            this.labelX1.TabIndex = 333;
            this.labelX1.Text = "영업일자";
            this.labelX1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(3, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 71);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "마감";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmKioskClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmKioskClose";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmKioskClose";
            this.Load += new System.EventHandler(this.FrmKioskClose_Load);
            this.dbpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.Label lblEndAmt;
        private System.Windows.Forms.PictureBox picBoxClose;
        private System.Windows.Forms.PictureBox picBoxCancel;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvEtcList;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvSalesList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblKioskNum;
        private System.Windows.Forms.Label lblSalesDate;
        private System.Windows.Forms.Label labelX2;
        private System.Windows.Forms.Label labelX1;
        private System.Windows.Forms.Label lblTitle;
        private System.IO.Ports.SerialPort serialPortClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}