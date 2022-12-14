namespace Kiosk
{
    partial class FrmCardCancelDaou
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.woniPanel1 = new Kiosk.Controler.WoniPanel();
            this.panelSearch = new Kiosk.Controler.WoniPanel();
            this.picBoxContinue = new System.Windows.Forms.PictureBox();
            this.picBoxCancel = new System.Windows.Forms.PictureBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.picBoxNextPage = new System.Windows.Forms.PictureBox();
            this.picBoxPrevPage = new System.Windows.Forms.PictureBox();
            this.dgvSalesCancel = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSalesDate = new DevComponents.DotNetBar.LabelX();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKNum = new DevComponents.DotNetBar.LabelX();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStore = new DevComponents.DotNetBar.LabelX();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.woniPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxContinue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxNextPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrevPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // woniPanel1
            // 
            this.woniPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.woniPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.woniPanel1.Controls.Add(this.panelSearch);
            this.woniPanel1.Controls.Add(this.picBoxContinue);
            this.woniPanel1.Controls.Add(this.picBoxCancel);
            this.woniPanel1.Controls.Add(this.lblPage);
            this.woniPanel1.Controls.Add(this.picBoxNextPage);
            this.woniPanel1.Controls.Add(this.picBoxPrevPage);
            this.woniPanel1.Controls.Add(this.dgvSalesCancel);
            this.woniPanel1.Controls.Add(this.lblSalesDate);
            this.woniPanel1.Controls.Add(this.label4);
            this.woniPanel1.Controls.Add(this.lblKNum);
            this.woniPanel1.Controls.Add(this.label3);
            this.woniPanel1.Controls.Add(this.lblStore);
            this.woniPanel1.Controls.Add(this.label2);
            this.woniPanel1.Controls.Add(this.lblTitle);
            this.woniPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.woniPanel1.Location = new System.Drawing.Point(40, 57);
            this.woniPanel1.Name = "woniPanel1";
            this.woniPanel1.Size = new System.Drawing.Size(1000, 1800);
            this.woniPanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.woniPanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.woniPanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.woniPanel1.Style.BackgroundImage = global::Kiosk.Properties.Resources.popup_lookup;
            this.woniPanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.woniPanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.woniPanel1.Style.GradientAngle = 90;
            this.woniPanel1.TabIndex = 0;
            // 
            // panelSearch
            // 
            this.panelSearch.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelSearch.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelSearch.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelSearch.Font = new System.Drawing.Font("나눔고딕", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelSearch.Location = new System.Drawing.Point(643, 243);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(170, 57);
            this.panelSearch.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelSearch.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelSearch.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelSearch.Style.BackgroundImage = global::Kiosk.Properties.Resources.btn_lookup;
            this.panelSearch.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelSearch.Style.ForeColor.Color = System.Drawing.Color.White;
            this.panelSearch.Style.GradientAngle = 90;
            this.panelSearch.TabIndex = 422;
            this.panelSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.panelSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxContinue
            // 
            this.picBoxContinue.BackgroundImage = global::Kiosk.Properties.Resources.btn_continue;
            this.picBoxContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBoxContinue.Location = new System.Drawing.Point(567, 1644);
            this.picBoxContinue.Name = "picBoxContinue";
            this.picBoxContinue.Size = new System.Drawing.Size(300, 117);
            this.picBoxContinue.TabIndex = 421;
            this.picBoxContinue.TabStop = false;
            this.picBoxContinue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxContinue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxCancel
            // 
            this.picBoxCancel.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxCancel.Location = new System.Drawing.Point(161, 1644);
            this.picBoxCancel.Name = "picBoxCancel";
            this.picBoxCancel.Size = new System.Drawing.Size(200, 117);
            this.picBoxCancel.TabIndex = 420;
            this.picBoxCancel.TabStop = false;
            this.picBoxCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblPage
            // 
            this.lblPage.BackColor = System.Drawing.Color.Transparent;
            this.lblPage.Font = new System.Drawing.Font("나눔고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPage.Location = new System.Drawing.Point(428, 1557);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(145, 43);
            this.lblPage.TabIndex = 418;
            this.lblPage.Text = "1 / 1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBoxNextPage
            // 
            this.picBoxNextPage.BackgroundImage = global::Kiosk.Properties.Resources.btn_page_next;
            this.picBoxNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBoxNextPage.Location = new System.Drawing.Point(579, 1557);
            this.picBoxNextPage.Name = "picBoxNextPage";
            this.picBoxNextPage.Size = new System.Drawing.Size(42, 43);
            this.picBoxNextPage.TabIndex = 417;
            this.picBoxNextPage.TabStop = false;
            this.picBoxNextPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxNextPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxPrevPage
            // 
            this.picBoxPrevPage.BackgroundImage = global::Kiosk.Properties.Resources.btn_page_back;
            this.picBoxPrevPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBoxPrevPage.Location = new System.Drawing.Point(380, 1557);
            this.picBoxPrevPage.Name = "picBoxPrevPage";
            this.picBoxPrevPage.Size = new System.Drawing.Size(42, 43);
            this.picBoxPrevPage.TabIndex = 416;
            this.picBoxPrevPage.TabStop = false;
            this.picBoxPrevPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxPrevPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // dgvSalesCancel
            // 
            this.dgvSalesCancel.AllowUserToAddRows = false;
            this.dgvSalesCancel.AllowUserToResizeColumns = false;
            this.dgvSalesCancel.AllowUserToResizeRows = false;
            this.dgvSalesCancel.BackgroundColor = System.Drawing.Color.White;
            this.dgvSalesCancel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSalesCancel.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSalesCancel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesCancel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column7});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSalesCancel.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSalesCancel.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSalesCancel.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvSalesCancel.Location = new System.Drawing.Point(3, 377);
            this.dgvSalesCancel.MultiSelect = false;
            this.dgvSalesCancel.Name = "dgvSalesCancel";
            this.dgvSalesCancel.RowHeadersVisible = false;
            this.dgvSalesCancel.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSalesCancel.RowTemplate.Height = 23;
            this.dgvSalesCancel.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvSalesCancel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSalesCancel.Size = new System.Drawing.Size(994, 1165);
            this.dgvSalesCancel.TabIndex = 415;
            this.dgvSalesCancel.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSalesCancel_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "승인일자";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "승인시간";
            this.Column2.Name = "Column2";
            this.Column2.Width = 140;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "영수증번호";
            this.Column3.Name = "Column3";
            this.Column3.Width = 160;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "승인번호";
            this.Column4.Name = "Column4";
            this.Column4.Width = 120;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "승인금액";
            this.Column5.Name = "Column5";
            this.Column5.Width = 142;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "카드명";
            this.Column6.Name = "Column6";
            this.Column6.Width = 170;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "승인여부";
            this.Column8.Name = "Column8";
            this.Column8.Width = 120;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "할부개월";
            this.Column7.Name = "Column7";
            // 
            // lblSalesDate
            // 
            this.lblSalesDate.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblSalesDate.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblSalesDate.BackgroundStyle.BorderBottomColor = System.Drawing.Color.DimGray;
            this.lblSalesDate.BackgroundStyle.BorderBottomWidth = 2;
            this.lblSalesDate.BackgroundStyle.BorderColor = System.Drawing.Color.DimGray;
            this.lblSalesDate.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblSalesDate.BackgroundStyle.BorderLeftWidth = 2;
            this.lblSalesDate.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblSalesDate.BackgroundStyle.BorderRightWidth = 2;
            this.lblSalesDate.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblSalesDate.BackgroundStyle.BorderTopWidth = 2;
            this.lblSalesDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSalesDate.Font = new System.Drawing.Font("나눔고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSalesDate.Location = new System.Drawing.Point(186, 243);
            this.lblSalesDate.Name = "lblSalesDate";
            this.lblSalesDate.Size = new System.Drawing.Size(451, 57);
            this.lblSalesDate.TabIndex = 414;
            this.lblSalesDate.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblSalesDate.Click += new System.EventHandler(this.lblSalesDate_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("나눔스퀘어 Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(3, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 57);
            this.label4.TabIndex = 413;
            this.label4.Text = "영업일자";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKNum
            // 
            this.lblKNum.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblKNum.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblKNum.BackgroundStyle.BorderBottomColor = System.Drawing.Color.DimGray;
            this.lblKNum.BackgroundStyle.BorderBottomWidth = 2;
            this.lblKNum.BackgroundStyle.BorderColor = System.Drawing.Color.DimGray;
            this.lblKNum.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblKNum.BackgroundStyle.BorderLeftWidth = 2;
            this.lblKNum.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblKNum.BackgroundStyle.BorderRightWidth = 2;
            this.lblKNum.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblKNum.BackgroundStyle.BorderTopWidth = 2;
            this.lblKNum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblKNum.Font = new System.Drawing.Font("나눔고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblKNum.Location = new System.Drawing.Point(873, 169);
            this.lblKNum.Name = "lblKNum";
            this.lblKNum.Size = new System.Drawing.Size(68, 57);
            this.lblKNum.TabIndex = 412;
            this.lblKNum.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("나눔스퀘어 Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(700, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 57);
            this.label3.TabIndex = 411;
            this.label3.Text = "키오스크";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStore
            // 
            this.lblStore.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblStore.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblStore.BackgroundStyle.BorderBottomColor = System.Drawing.Color.DimGray;
            this.lblStore.BackgroundStyle.BorderBottomWidth = 2;
            this.lblStore.BackgroundStyle.BorderColor = System.Drawing.Color.DimGray;
            this.lblStore.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblStore.BackgroundStyle.BorderLeftWidth = 2;
            this.lblStore.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblStore.BackgroundStyle.BorderRightWidth = 2;
            this.lblStore.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.lblStore.BackgroundStyle.BorderTopWidth = 2;
            this.lblStore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblStore.Font = new System.Drawing.Font("나눔고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStore.Location = new System.Drawing.Point(186, 169);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(451, 57);
            this.lblStore.TabIndex = 410;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("나눔스퀘어 Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(3, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 57);
            this.label2.TabIndex = 409;
            this.label2.Text = "가맹점";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("나눔스퀘어 Bold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(3, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(994, 80);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "카드취소";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmCardCancelDaou
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.woniPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCardCancelDaou";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCardCancel";
            this.Load += new System.EventHandler(this.FrmCardCancel_Load);
            this.woniPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxContinue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxNextPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrevPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controler.WoniPanel woniPanel1;
        private System.Windows.Forms.PictureBox picBoxContinue;
        private System.Windows.Forms.PictureBox picBoxCancel;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.PictureBox picBoxNextPage;
        private System.Windows.Forms.PictureBox picBoxPrevPage;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvSalesCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private DevComponents.DotNetBar.LabelX lblSalesDate;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.LabelX lblKNum;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.LabelX lblStore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private Controler.WoniPanel panelSearch;
    }
}