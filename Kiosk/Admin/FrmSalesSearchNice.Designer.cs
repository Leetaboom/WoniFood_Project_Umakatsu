namespace Kiosk
{
    partial class FrmSalesSearchNice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.serialPortReport = new System.IO.Ports.SerialPort(this.components);
            this.dbpMain = new Kiosk.DoubleBuffer_Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtBoxReport = new System.Windows.Forms.TextBox();
            this.panelItemList = new Kiosk.DoubleBuffer_Panel();
            this.lblItemList = new System.Windows.Forms.Label();
            this.panelABC = new Kiosk.DoubleBuffer_Panel();
            this.lblABC = new System.Windows.Forms.Label();
            this.panelBuySales = new Kiosk.DoubleBuffer_Panel();
            this.lblBuySales = new System.Windows.Forms.Label();
            this.panelSalesCancel = new Kiosk.DoubleBuffer_Panel();
            this.lblSalesCancel = new System.Windows.Forms.Label();
            this.panelGrpSales = new Kiosk.DoubleBuffer_Panel();
            this.lblGrpSales = new System.Windows.Forms.Label();
            this.panelItemSales = new Kiosk.DoubleBuffer_Panel();
            this.lblItemSales = new System.Windows.Forms.Label();
            this.panelMonthSales = new Kiosk.DoubleBuffer_Panel();
            this.lblMonthSales = new System.Windows.Forms.Label();
            this.panelOneDaySales = new Kiosk.DoubleBuffer_Panel();
            this.lblOneDaySales = new System.Windows.Forms.Label();
            this.panelTheDaySales = new Kiosk.DoubleBuffer_Panel();
            this.lblTheDaySales = new System.Windows.Forms.Label();
            this.dgvMenuList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picBoxReportPrint = new System.Windows.Forms.Label();
            this.picBoxCancel = new System.Windows.Forms.PictureBox();
            this.line2 = new DevComponents.DotNetBar.Controls.Line();
            this.panelSearch = new Kiosk.DoubleBuffer_Panel();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblDash = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSalesDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKNum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStore = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.dbpMain.SuspendLayout();
            this.panelItemList.SuspendLayout();
            this.panelABC.SuspendLayout();
            this.panelBuySales.SuspendLayout();
            this.panelSalesCancel.SuspendLayout();
            this.panelGrpSales.SuspendLayout();
            this.panelItemSales.SuspendLayout();
            this.panelMonthSales.SuspendLayout();
            this.panelOneDaySales.SuspendLayout();
            this.panelTheDaySales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_lookup;
            this.dbpMain.BorderColor = System.Drawing.Color.Black;
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.Controls.Add(this.txtBoxReport);
            this.dbpMain.Controls.Add(this.panelItemList);
            this.dbpMain.Controls.Add(this.panelABC);
            this.dbpMain.Controls.Add(this.panelBuySales);
            this.dbpMain.Controls.Add(this.panelSalesCancel);
            this.dbpMain.Controls.Add(this.panelGrpSales);
            this.dbpMain.Controls.Add(this.panelItemSales);
            this.dbpMain.Controls.Add(this.panelMonthSales);
            this.dbpMain.Controls.Add(this.panelOneDaySales);
            this.dbpMain.Controls.Add(this.panelTheDaySales);
            this.dbpMain.Controls.Add(this.dgvMenuList);
            this.dbpMain.Controls.Add(this.picBoxReportPrint);
            this.dbpMain.Controls.Add(this.picBoxCancel);
            this.dbpMain.Controls.Add(this.line2);
            this.dbpMain.Controls.Add(this.panelSearch);
            this.dbpMain.Controls.Add(this.lblEndDate);
            this.dbpMain.Controls.Add(this.lblDash);
            this.dbpMain.Controls.Add(this.lblStartDate);
            this.dbpMain.Controls.Add(this.label5);
            this.dbpMain.Controls.Add(this.lblSalesDate);
            this.dbpMain.Controls.Add(this.label4);
            this.dbpMain.Controls.Add(this.lblKNum);
            this.dbpMain.Controls.Add(this.label3);
            this.dbpMain.Controls.Add(this.lblStore);
            this.dbpMain.Controls.Add(this.label2);
            this.dbpMain.Controls.Add(this.line1);
            this.dbpMain.Location = new System.Drawing.Point(40, 57);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Size = new System.Drawing.Size(1000, 1800);
            this.dbpMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(3, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 86);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "매출조회";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxReport
            // 
            this.txtBoxReport.BackColor = System.Drawing.Color.Beige;
            this.txtBoxReport.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBoxReport.Location = new System.Drawing.Point(336, 399);
            this.txtBoxReport.Multiline = true;
            this.txtBoxReport.Name = "txtBoxReport";
            this.txtBoxReport.ReadOnly = true;
            this.txtBoxReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxReport.Size = new System.Drawing.Size(649, 1186);
            this.txtBoxReport.TabIndex = 525;
            // 
            // panelItemList
            // 
            this.panelItemList.BackColor = System.Drawing.Color.White;
            this.panelItemList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelItemList.BorderColor = System.Drawing.Color.Black;
            this.panelItemList.Controls.Add(this.lblItemList);
            this.panelItemList.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelItemList.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelItemList.Location = new System.Drawing.Point(13, 1458);
            this.panelItemList.Name = "panelItemList";
            this.panelItemList.Size = new System.Drawing.Size(300, 100);
            this.panelItemList.TabIndex = 465;
            this.panelItemList.Text = "품목 리스트 설정";
            this.panelItemList.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblItemList
            // 
            this.lblItemList.BackColor = System.Drawing.Color.Transparent;
            this.lblItemList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemList.Location = new System.Drawing.Point(0, 0);
            this.lblItemList.Name = "lblItemList";
            this.lblItemList.Size = new System.Drawing.Size(300, 100);
            this.lblItemList.TabIndex = 1;
            this.lblItemList.Text = "품목 리스트 설정";
            this.lblItemList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblItemList.Click += new System.EventHandler(this.panelList_Click);
            // 
            // panelABC
            // 
            this.panelABC.BackColor = System.Drawing.Color.White;
            this.panelABC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelABC.BorderColor = System.Drawing.Color.Black;
            this.panelABC.Controls.Add(this.lblABC);
            this.panelABC.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelABC.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelABC.Location = new System.Drawing.Point(13, 1340);
            this.panelABC.Name = "panelABC";
            this.panelABC.Size = new System.Drawing.Size(300, 100);
            this.panelABC.TabIndex = 461;
            this.panelABC.Text = "ABC 보고서";
            this.panelABC.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblABC
            // 
            this.lblABC.BackColor = System.Drawing.Color.Transparent;
            this.lblABC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblABC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblABC.Location = new System.Drawing.Point(0, 0);
            this.lblABC.Name = "lblABC";
            this.lblABC.Size = new System.Drawing.Size(300, 100);
            this.lblABC.TabIndex = 1;
            this.lblABC.Text = "ABC 보고서";
            this.lblABC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblABC.Click += new System.EventHandler(this.panelList_Click);
            // 
            // panelBuySales
            // 
            this.panelBuySales.BackColor = System.Drawing.Color.White;
            this.panelBuySales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBuySales.BorderColor = System.Drawing.Color.Black;
            this.panelBuySales.Controls.Add(this.lblBuySales);
            this.panelBuySales.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelBuySales.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelBuySales.Location = new System.Drawing.Point(13, 1101);
            this.panelBuySales.Name = "panelBuySales";
            this.panelBuySales.Size = new System.Drawing.Size(300, 100);
            this.panelBuySales.TabIndex = 457;
            this.panelBuySales.Text = "매입사별 매출 현황";
            this.panelBuySales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblBuySales
            // 
            this.lblBuySales.BackColor = System.Drawing.Color.Transparent;
            this.lblBuySales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBuySales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBuySales.Location = new System.Drawing.Point(0, 0);
            this.lblBuySales.Name = "lblBuySales";
            this.lblBuySales.Size = new System.Drawing.Size(300, 100);
            this.lblBuySales.TabIndex = 1;
            this.lblBuySales.Text = "매입사별 매출 현황";
            this.lblBuySales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBuySales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // panelSalesCancel
            // 
            this.panelSalesCancel.BackColor = System.Drawing.Color.White;
            this.panelSalesCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSalesCancel.BorderColor = System.Drawing.Color.Black;
            this.panelSalesCancel.Controls.Add(this.lblSalesCancel);
            this.panelSalesCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelSalesCancel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelSalesCancel.Location = new System.Drawing.Point(13, 984);
            this.panelSalesCancel.Name = "panelSalesCancel";
            this.panelSalesCancel.Size = new System.Drawing.Size(300, 100);
            this.panelSalesCancel.TabIndex = 453;
            this.panelSalesCancel.Text = "매출 취소 현황";
            this.panelSalesCancel.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblSalesCancel
            // 
            this.lblSalesCancel.BackColor = System.Drawing.Color.Transparent;
            this.lblSalesCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSalesCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSalesCancel.Location = new System.Drawing.Point(0, 0);
            this.lblSalesCancel.Name = "lblSalesCancel";
            this.lblSalesCancel.Size = new System.Drawing.Size(300, 100);
            this.lblSalesCancel.TabIndex = 1;
            this.lblSalesCancel.Text = "매출 취소 현황";
            this.lblSalesCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSalesCancel.Click += new System.EventHandler(this.panelList_Click);
            // 
            // panelGrpSales
            // 
            this.panelGrpSales.BackColor = System.Drawing.Color.White;
            this.panelGrpSales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelGrpSales.BorderColor = System.Drawing.Color.Black;
            this.panelGrpSales.Controls.Add(this.lblGrpSales);
            this.panelGrpSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelGrpSales.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelGrpSales.Location = new System.Drawing.Point(13, 867);
            this.panelGrpSales.Name = "panelGrpSales";
            this.panelGrpSales.Size = new System.Drawing.Size(300, 100);
            this.panelGrpSales.TabIndex = 449;
            this.panelGrpSales.Text = "부문별 매출 현황";
            this.panelGrpSales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblGrpSales
            // 
            this.lblGrpSales.BackColor = System.Drawing.Color.Transparent;
            this.lblGrpSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGrpSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGrpSales.Location = new System.Drawing.Point(0, 0);
            this.lblGrpSales.Name = "lblGrpSales";
            this.lblGrpSales.Size = new System.Drawing.Size(300, 100);
            this.lblGrpSales.TabIndex = 1;
            this.lblGrpSales.Text = "부문별 매출 현황";
            this.lblGrpSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGrpSales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // panelItemSales
            // 
            this.panelItemSales.BackColor = System.Drawing.Color.White;
            this.panelItemSales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelItemSales.BorderColor = System.Drawing.Color.Black;
            this.panelItemSales.Controls.Add(this.lblItemSales);
            this.panelItemSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelItemSales.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelItemSales.Location = new System.Drawing.Point(13, 750);
            this.panelItemSales.Name = "panelItemSales";
            this.panelItemSales.Size = new System.Drawing.Size(300, 100);
            this.panelItemSales.TabIndex = 445;
            this.panelItemSales.Text = "품목별 매출 현황";
            this.panelItemSales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblItemSales
            // 
            this.lblItemSales.BackColor = System.Drawing.Color.Transparent;
            this.lblItemSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblItemSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemSales.Location = new System.Drawing.Point(0, 0);
            this.lblItemSales.Name = "lblItemSales";
            this.lblItemSales.Size = new System.Drawing.Size(300, 100);
            this.lblItemSales.TabIndex = 1;
            this.lblItemSales.Text = "품목별 매출 현황";
            this.lblItemSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblItemSales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // panelMonthSales
            // 
            this.panelMonthSales.BackColor = System.Drawing.Color.White;
            this.panelMonthSales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMonthSales.BorderColor = System.Drawing.Color.Black;
            this.panelMonthSales.Controls.Add(this.lblMonthSales);
            this.panelMonthSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelMonthSales.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelMonthSales.Location = new System.Drawing.Point(13, 633);
            this.panelMonthSales.Name = "panelMonthSales";
            this.panelMonthSales.Size = new System.Drawing.Size(300, 100);
            this.panelMonthSales.TabIndex = 441;
            this.panelMonthSales.Text = "월별 매출 현황";
            this.panelMonthSales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblMonthSales
            // 
            this.lblMonthSales.BackColor = System.Drawing.Color.Transparent;
            this.lblMonthSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMonthSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonthSales.Location = new System.Drawing.Point(0, 0);
            this.lblMonthSales.Name = "lblMonthSales";
            this.lblMonthSales.Size = new System.Drawing.Size(300, 100);
            this.lblMonthSales.TabIndex = 1;
            this.lblMonthSales.Text = "월별 매출 현황";
            this.lblMonthSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMonthSales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // panelOneDaySales
            // 
            this.panelOneDaySales.BackColor = System.Drawing.Color.White;
            this.panelOneDaySales.BackgroundImage = global::Kiosk.Properties.Resources.btn_rad3;
            this.panelOneDaySales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelOneDaySales.BorderColor = System.Drawing.Color.Black;
            this.panelOneDaySales.Controls.Add(this.lblOneDaySales);
            this.panelOneDaySales.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelOneDaySales.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelOneDaySales.Location = new System.Drawing.Point(13, 516);
            this.panelOneDaySales.Name = "panelOneDaySales";
            this.panelOneDaySales.Size = new System.Drawing.Size(300, 100);
            this.panelOneDaySales.TabIndex = 437;
            this.panelOneDaySales.Text = "일별 매출 현황";
            this.panelOneDaySales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblOneDaySales
            // 
            this.lblOneDaySales.BackColor = System.Drawing.Color.Transparent;
            this.lblOneDaySales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOneDaySales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOneDaySales.Location = new System.Drawing.Point(0, 0);
            this.lblOneDaySales.Name = "lblOneDaySales";
            this.lblOneDaySales.Size = new System.Drawing.Size(300, 100);
            this.lblOneDaySales.TabIndex = 1;
            this.lblOneDaySales.Text = "일별 매출 현황";
            this.lblOneDaySales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOneDaySales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // panelTheDaySales
            // 
            this.panelTheDaySales.BackColor = System.Drawing.Color.White;
            this.panelTheDaySales.BackgroundImage = global::Kiosk.Properties.Resources.btn_bg_300X100;
            this.panelTheDaySales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTheDaySales.BorderColor = System.Drawing.Color.Black;
            this.panelTheDaySales.Controls.Add(this.lblTheDaySales);
            this.panelTheDaySales.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelTheDaySales.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panelTheDaySales.Location = new System.Drawing.Point(13, 399);
            this.panelTheDaySales.Name = "panelTheDaySales";
            this.panelTheDaySales.Size = new System.Drawing.Size(300, 100);
            this.panelTheDaySales.TabIndex = 430;
            this.panelTheDaySales.Text = "당일 매출 현황";
            this.panelTheDaySales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // lblTheDaySales
            // 
            this.lblTheDaySales.BackColor = System.Drawing.Color.Transparent;
            this.lblTheDaySales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTheDaySales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTheDaySales.Location = new System.Drawing.Point(0, 0);
            this.lblTheDaySales.Name = "lblTheDaySales";
            this.lblTheDaySales.Size = new System.Drawing.Size(300, 100);
            this.lblTheDaySales.TabIndex = 0;
            this.lblTheDaySales.Text = "당일 매출 현황";
            this.lblTheDaySales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTheDaySales.Click += new System.EventHandler(this.panelList_Click);
            // 
            // dgvMenuList
            // 
            this.dgvMenuList.AllowUserToAddRows = false;
            this.dgvMenuList.AllowUserToResizeColumns = false;
            this.dgvMenuList.AllowUserToResizeRows = false;
            this.dgvMenuList.BackgroundColor = System.Drawing.Color.Beige;
            this.dgvMenuList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMenuList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMenuList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenuList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMenuList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMenuList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMenuList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvMenuList.Location = new System.Drawing.Point(336, 399);
            this.dgvMenuList.MultiSelect = false;
            this.dgvMenuList.Name = "dgvMenuList";
            this.dgvMenuList.RowHeadersVisible = false;
            this.dgvMenuList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMenuList.RowTemplate.Height = 23;
            this.dgvMenuList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvMenuList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenuList.Size = new System.Drawing.Size(649, 1186);
            this.dgvMenuList.TabIndex = 429;
            this.dgvMenuList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMenuList_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "P";
            this.Column1.Name = "Column1";
            this.Column1.ToolTipText = "페이지";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "N";
            this.Column2.Name = "Column2";
            this.Column2.ToolTipText = "순번";
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "그룹";
            this.Column3.Name = "Column3";
            this.Column3.ToolTipText = "그룸명";
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "품목";
            this.Column4.Name = "Column4";
            this.Column4.Width = 300;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "상태";
            this.Column5.Name = "Column5";
            this.Column5.Width = 110;
            // 
            // picBoxReportPrint
            // 
            this.picBoxReportPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.picBoxReportPrint.ForeColor = System.Drawing.Color.White;
            this.picBoxReportPrint.Image = global::Kiosk.Properties.Resources.btn_print;
            this.picBoxReportPrint.Location = new System.Drawing.Point(567, 1644);
            this.picBoxReportPrint.Name = "picBoxReportPrint";
            this.picBoxReportPrint.Size = new System.Drawing.Size(300, 117);
            this.picBoxReportPrint.TabIndex = 428;
            this.picBoxReportPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.picBoxReportPrint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxReportPrint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxCancel
            // 
            this.picBoxCancel.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxCancel.Location = new System.Drawing.Point(161, 1644);
            this.picBoxCancel.Name = "picBoxCancel";
            this.picBoxCancel.Size = new System.Drawing.Size(200, 117);
            this.picBoxCancel.TabIndex = 427;
            this.picBoxCancel.TabStop = false;
            this.picBoxCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // line2
            // 
            this.line2.BackColor = System.Drawing.Color.Orange;
            this.line2.ForeColor = System.Drawing.Color.Transparent;
            this.line2.Location = new System.Drawing.Point(0, 1608);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(1000, 10);
            this.line2.TabIndex = 426;
            this.line2.Text = "line2";
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.BackgroundImage = global::Kiosk.Properties.Resources.btn_lookup;
            this.panelSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSearch.BorderColor = System.Drawing.Color.Black;
            this.panelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelSearch.Location = new System.Drawing.Point(803, 286);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(170, 57);
            this.panelSearch.TabIndex = 422;
            this.panelSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.panelSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblEndDate
            // 
            this.lblEndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblEndDate.Location = new System.Drawing.Point(527, 286);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(270, 57);
            this.lblEndDate.TabIndex = 421;
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEndDate.Click += new System.EventHandler(this.lblDate_Click);
            // 
            // lblDash
            // 
            this.lblDash.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDash.Location = new System.Drawing.Point(464, 298);
            this.lblDash.Name = "lblDash";
            this.lblDash.Size = new System.Drawing.Size(57, 57);
            this.lblDash.TabIndex = 420;
            this.lblDash.Text = "~";
            this.lblDash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStartDate
            // 
            this.lblStartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStartDate.Location = new System.Drawing.Point(188, 284);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(270, 57);
            this.lblStartDate.TabIndex = 419;
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStartDate.Click += new System.EventHandler(this.lblDate_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(5, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 57);
            this.label5.TabIndex = 418;
            this.label5.Text = "조회일자";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSalesDate
            // 
            this.lblSalesDate.BackColor = System.Drawing.Color.Transparent;
            this.lblSalesDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSalesDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSalesDate.Location = new System.Drawing.Point(188, 209);
            this.lblSalesDate.Name = "lblSalesDate";
            this.lblSalesDate.Size = new System.Drawing.Size(451, 57);
            this.lblSalesDate.TabIndex = 417;
            this.lblSalesDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(5, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 57);
            this.label4.TabIndex = 416;
            this.label4.Text = "영업일자";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKNum
            // 
            this.lblKNum.BackColor = System.Drawing.Color.Transparent;
            this.lblKNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblKNum.Location = new System.Drawing.Point(875, 135);
            this.lblKNum.Name = "lblKNum";
            this.lblKNum.Size = new System.Drawing.Size(68, 57);
            this.lblKNum.TabIndex = 415;
            this.lblKNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(702, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 57);
            this.label3.TabIndex = 414;
            this.label3.Text = "키오스크";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStore
            // 
            this.lblStore.BackColor = System.Drawing.Color.Transparent;
            this.lblStore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStore.Location = new System.Drawing.Point(188, 135);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(451, 57);
            this.lblStore.TabIndex = 413;
            this.lblStore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(5, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 57);
            this.label2.TabIndex = 412;
            this.label2.Text = "가맹점";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.Color.Orange;
            this.line1.ForeColor = System.Drawing.Color.Transparent;
            this.line1.Location = new System.Drawing.Point(0, 362);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(1000, 10);
            this.line1.TabIndex = 411;
            this.line1.Text = "line1";
            // 
            // FrmSalesSearchNice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSalesSearchNice";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSalesSearch";
            this.Load += new System.EventHandler(this.FrmSalesSearch_Load);
            this.dbpMain.ResumeLayout(false);
            this.dbpMain.PerformLayout();
            this.panelItemList.ResumeLayout(false);
            this.panelABC.ResumeLayout(false);
            this.panelBuySales.ResumeLayout(false);
            this.panelSalesCancel.ResumeLayout(false);
            this.panelGrpSales.ResumeLayout(false);
            this.panelItemSales.ResumeLayout(false);
            this.panelMonthSales.ResumeLayout(false);
            this.panelOneDaySales.ResumeLayout(false);
            this.panelTheDaySales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.TextBox txtBoxReport;
        private DoubleBuffer_Panel panelItemList;
        private DoubleBuffer_Panel panelABC;
        private DoubleBuffer_Panel panelBuySales;
        private DoubleBuffer_Panel panelSalesCancel;
        private DoubleBuffer_Panel panelGrpSales;
        private DoubleBuffer_Panel panelItemSales;
        private DoubleBuffer_Panel panelMonthSales;
        private DoubleBuffer_Panel panelOneDaySales;
        private DoubleBuffer_Panel panelTheDaySales;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvMenuList;
        private System.Windows.Forms.Label picBoxReportPrint;
        private System.Windows.Forms.PictureBox picBoxCancel;
        private DevComponents.DotNetBar.Controls.Line line2;
        private DoubleBuffer_Panel panelSearch;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblDash;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSalesDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblKNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.Line line1;
        private System.IO.Ports.SerialPort serialPortReport;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label lblItemList;
        private System.Windows.Forms.Label lblABC;
        private System.Windows.Forms.Label lblBuySales;
        private System.Windows.Forms.Label lblSalesCancel;
        private System.Windows.Forms.Label lblGrpSales;
        private System.Windows.Forms.Label lblItemSales;
        private System.Windows.Forms.Label lblMonthSales;
        private System.Windows.Forms.Label lblOneDaySales;
        private System.Windows.Forms.Label lblTheDaySales;
    }
}