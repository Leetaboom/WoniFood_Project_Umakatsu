namespace Kiosk
{
    partial class FrmOrderCancelBox
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
            this.woniPanel1 = new Kiosk.DoubleBuffer_Panel();
            this.picBoxContinue = new System.Windows.Forms.PictureBox();
            this.picBoxCancel = new System.Windows.Forms.PictureBox();
            this.lblMent = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.woniPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxContinue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // woniPanel1
            // 
            this.woniPanel1.BackColor = System.Drawing.Color.White;
            this.woniPanel1.BackgroundImage = global::Kiosk.Properties.Resources.popup_order_cancel;
            this.woniPanel1.Controls.Add(this.picBoxContinue);
            this.woniPanel1.Controls.Add(this.picBoxCancel);
            this.woniPanel1.Controls.Add(this.lblMent);
            this.woniPanel1.Controls.Add(this.lblProductName);
            this.woniPanel1.Controls.Add(this.label2);
            this.woniPanel1.Controls.Add(this.lblTitle);
            this.woniPanel1.Location = new System.Drawing.Point(94, 496);
            this.woniPanel1.Name = "woniPanel1";
            this.woniPanel1.Size = new System.Drawing.Size(900, 900);
            this.woniPanel1.TabIndex = 0;
            // 
            // picBoxContinue
            // 
            this.picBoxContinue.BackgroundImage = global::Kiosk.Properties.Resources.btn_continue;
            this.picBoxContinue.Location = new System.Drawing.Point(423, 732);
            this.picBoxContinue.Name = "picBoxContinue";
            this.picBoxContinue.Size = new System.Drawing.Size(300, 117);
            this.picBoxContinue.TabIndex = 48;
            this.picBoxContinue.TabStop = false;
            this.picBoxContinue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxContinue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxCancel
            // 
            this.picBoxCancel.BackgroundImage = global::Kiosk.Properties.Resources.btn_cancel;
            this.picBoxCancel.Location = new System.Drawing.Point(174, 732);
            this.picBoxCancel.Name = "picBoxCancel";
            this.picBoxCancel.Size = new System.Drawing.Size(200, 117);
            this.picBoxCancel.TabIndex = 47;
            this.picBoxCancel.TabStop = false;
            this.picBoxCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // lblMent
            // 
            this.lblMent.BackColor = System.Drawing.Color.Transparent;
            this.lblMent.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMent.ForeColor = System.Drawing.Color.DimGray;
            this.lblMent.Location = new System.Drawing.Point(3, 560);
            this.lblMent.Name = "lblMent";
            this.lblMent.Size = new System.Drawing.Size(900, 80);
            this.lblMent.TabIndex = 46;
            this.lblMent.Text = "상품을 선택 취소합니다.";
            this.lblMent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProductName.ForeColor = System.Drawing.Color.Red;
            this.lblProductName.Location = new System.Drawing.Point(0, 385);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(900, 175);
            this.lblProductName.TabIndex = 45;
            this.lblProductName.Text = "불고기버거";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(0, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(900, 80);
            this.label2.TabIndex = 44;
            this.label2.Text = "선택하신";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.lblTitle.Text = "주문취소";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmOrderCancelBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.woniPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmOrderCancelBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOrderCancelBox";
            this.Load += new System.EventHandler(this.FrmOrderCancelBox_Load);
            this.woniPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxContinue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel woniPanel1;
        private System.Windows.Forms.PictureBox picBoxContinue;
        private System.Windows.Forms.PictureBox picBoxCancel;
        private System.Windows.Forms.Label lblMent;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
    }
}