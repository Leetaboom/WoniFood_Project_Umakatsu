namespace Kiosk
{
    partial class FrmAdminLogin
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
            this.picBoxCancel = new System.Windows.Forms.PictureBox();
            this.picBoxOK = new System.Windows.Forms.PictureBox();
            this.dbpKeypad = new Kiosk.DoubleBuffer_Panel();
            this.btnKeyPad_Ent = new Kiosk.Controler.WButton();
            this.btnKeyPad_3 = new Kiosk.Controler.WButton();
            this.btnKeyPad_2 = new Kiosk.Controler.WButton();
            this.btnKeyPad_1 = new Kiosk.Controler.WButton();
            this.btnKeyPad_Bsp = new Kiosk.Controler.WButton();
            this.btnKeyPad_6 = new Kiosk.Controler.WButton();
            this.btnKeyPad_5 = new Kiosk.Controler.WButton();
            this.btnKeyPad_4 = new Kiosk.Controler.WButton();
            this.btnKeyPad_0 = new Kiosk.Controler.WButton();
            this.btnKeyPad_9 = new Kiosk.Controler.WButton();
            this.btnKeyPad_8 = new Kiosk.Controler.WButton();
            this.btnKeyPad_7 = new Kiosk.Controler.WButton();
            this.txtBoxAdminPw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dbpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOK)).BeginInit();
            this.dbpKeypad.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbpMain
            // 
            this.dbpMain.BackColor = System.Drawing.Color.White;
            this.dbpMain.BackgrounColor = System.Drawing.Color.White;
            this.dbpMain.BackgroundImage = global::Kiosk.Properties.Resources.popup_n;
            this.dbpMain.BorderColor = System.Drawing.Color.Black;
            this.dbpMain.BorderRadius = 0;
            this.dbpMain.BorderSize = 0;
            this.dbpMain.Controls.Add(this.picBoxCancel);
            this.dbpMain.Controls.Add(this.picBoxOK);
            this.dbpMain.Controls.Add(this.dbpKeypad);
            this.dbpMain.Controls.Add(this.txtBoxAdminPw);
            this.dbpMain.Controls.Add(this.label1);
            this.dbpMain.Controls.Add(this.lblTitle);
            this.dbpMain.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dbpMain.Location = new System.Drawing.Point(90, 410);
            this.dbpMain.Name = "dbpMain";
            this.dbpMain.Opacity = 100;
            this.dbpMain.Size = new System.Drawing.Size(900, 1100);
            this.dbpMain.TabIndex = 0;
            this.dbpMain.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // picBoxCancel
            // 
            this.picBoxCancel.BackgroundImage = global::Kiosk.Properties.Resources.btn_back;
            this.picBoxCancel.Location = new System.Drawing.Point(150, 933);
            this.picBoxCancel.Name = "picBoxCancel";
            this.picBoxCancel.Size = new System.Drawing.Size(200, 117);
            this.picBoxCancel.TabIndex = 51;
            this.picBoxCancel.TabStop = false;
            this.picBoxCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // picBoxOK
            // 
            this.picBoxOK.BackgroundImage = global::Kiosk.Properties.Resources.btn_confirm;
            this.picBoxOK.Location = new System.Drawing.Point(450, 933);
            this.picBoxOK.Name = "picBoxOK";
            this.picBoxOK.Size = new System.Drawing.Size(300, 117);
            this.picBoxOK.TabIndex = 50;
            this.picBoxOK.TabStop = false;
            this.picBoxOK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseDown);
            this.picBoxOK.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxBtn_MouseUp);
            // 
            // dbpKeypad
            // 
            this.dbpKeypad.BackColor = System.Drawing.Color.White;
            this.dbpKeypad.BackgrounColor = System.Drawing.Color.White;
            this.dbpKeypad.BorderColor = System.Drawing.Color.Black;
            this.dbpKeypad.BorderRadius = 0;
            this.dbpKeypad.BorderSize = 0;
            this.dbpKeypad.Controls.Add(this.btnKeyPad_Ent);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_3);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_2);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_1);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_Bsp);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_6);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_5);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_4);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_0);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_9);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_8);
            this.dbpKeypad.Controls.Add(this.btnKeyPad_7);
            this.dbpKeypad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dbpKeypad.Location = new System.Drawing.Point(140, 423);
            this.dbpKeypad.Name = "dbpKeypad";
            this.dbpKeypad.Opacity = 100;
            this.dbpKeypad.Size = new System.Drawing.Size(620, 460);
            this.dbpKeypad.TabIndex = 41;
            this.dbpKeypad.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnKeyPad_Ent
            // 
            this.btnKeyPad_Ent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_Ent.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_Ent.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_Ent.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_Ent.BorderRadius = 40;
            this.btnKeyPad_Ent.BorderSize = 0;
            this.btnKeyPad_Ent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_Ent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_Ent.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeyPad_Ent.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_Ent.Location = new System.Drawing.Point(459, 304);
            this.btnKeyPad_Ent.Name = "btnKeyPad_Ent";
            this.btnKeyPad_Ent.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_Ent.TabIndex = 11;
            this.btnKeyPad_Ent.Text = "ENT";
            this.btnKeyPad_Ent.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_Ent.UseVisualStyleBackColor = false;
            this.btnKeyPad_Ent.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_3
            // 
            this.btnKeyPad_3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_3.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_3.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_3.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_3.BorderRadius = 40;
            this.btnKeyPad_3.BorderSize = 0;
            this.btnKeyPad_3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_3.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_3.Location = new System.Drawing.Point(313, 304);
            this.btnKeyPad_3.Name = "btnKeyPad_3";
            this.btnKeyPad_3.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_3.TabIndex = 10;
            this.btnKeyPad_3.Text = "3";
            this.btnKeyPad_3.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_3.UseVisualStyleBackColor = false;
            this.btnKeyPad_3.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_2
            // 
            this.btnKeyPad_2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_2.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_2.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_2.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_2.BorderRadius = 40;
            this.btnKeyPad_2.BorderSize = 0;
            this.btnKeyPad_2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_2.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_2.Location = new System.Drawing.Point(167, 304);
            this.btnKeyPad_2.Name = "btnKeyPad_2";
            this.btnKeyPad_2.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_2.TabIndex = 9;
            this.btnKeyPad_2.Text = "2";
            this.btnKeyPad_2.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_2.UseVisualStyleBackColor = false;
            this.btnKeyPad_2.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_1
            // 
            this.btnKeyPad_1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_1.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_1.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_1.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_1.BorderRadius = 40;
            this.btnKeyPad_1.BorderSize = 0;
            this.btnKeyPad_1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_1.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_1.Location = new System.Drawing.Point(21, 304);
            this.btnKeyPad_1.Name = "btnKeyPad_1";
            this.btnKeyPad_1.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_1.TabIndex = 8;
            this.btnKeyPad_1.Text = "1";
            this.btnKeyPad_1.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_1.UseVisualStyleBackColor = false;
            this.btnKeyPad_1.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_Bsp
            // 
            this.btnKeyPad_Bsp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_Bsp.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_Bsp.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_Bsp.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_Bsp.BorderRadius = 40;
            this.btnKeyPad_Bsp.BorderSize = 0;
            this.btnKeyPad_Bsp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_Bsp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_Bsp.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeyPad_Bsp.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_Bsp.Location = new System.Drawing.Point(459, 158);
            this.btnKeyPad_Bsp.Name = "btnKeyPad_Bsp";
            this.btnKeyPad_Bsp.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_Bsp.TabIndex = 7;
            this.btnKeyPad_Bsp.Text = "BSP";
            this.btnKeyPad_Bsp.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_Bsp.UseVisualStyleBackColor = false;
            this.btnKeyPad_Bsp.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_6
            // 
            this.btnKeyPad_6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_6.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_6.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_6.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_6.BorderRadius = 40;
            this.btnKeyPad_6.BorderSize = 0;
            this.btnKeyPad_6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_6.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_6.Location = new System.Drawing.Point(313, 158);
            this.btnKeyPad_6.Name = "btnKeyPad_6";
            this.btnKeyPad_6.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_6.TabIndex = 6;
            this.btnKeyPad_6.Text = "6";
            this.btnKeyPad_6.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_6.UseVisualStyleBackColor = false;
            this.btnKeyPad_6.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_5
            // 
            this.btnKeyPad_5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_5.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_5.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_5.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_5.BorderRadius = 40;
            this.btnKeyPad_5.BorderSize = 0;
            this.btnKeyPad_5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_5.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_5.Location = new System.Drawing.Point(167, 158);
            this.btnKeyPad_5.Name = "btnKeyPad_5";
            this.btnKeyPad_5.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_5.TabIndex = 5;
            this.btnKeyPad_5.Text = "5";
            this.btnKeyPad_5.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_5.UseVisualStyleBackColor = false;
            this.btnKeyPad_5.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_4
            // 
            this.btnKeyPad_4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_4.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_4.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_4.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_4.BorderRadius = 40;
            this.btnKeyPad_4.BorderSize = 0;
            this.btnKeyPad_4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_4.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_4.Location = new System.Drawing.Point(21, 158);
            this.btnKeyPad_4.Name = "btnKeyPad_4";
            this.btnKeyPad_4.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_4.TabIndex = 4;
            this.btnKeyPad_4.Text = "4";
            this.btnKeyPad_4.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_4.UseVisualStyleBackColor = false;
            this.btnKeyPad_4.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_0
            // 
            this.btnKeyPad_0.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_0.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_0.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_0.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_0.BorderRadius = 40;
            this.btnKeyPad_0.BorderSize = 0;
            this.btnKeyPad_0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_0.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_0.Location = new System.Drawing.Point(459, 12);
            this.btnKeyPad_0.Name = "btnKeyPad_0";
            this.btnKeyPad_0.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_0.TabIndex = 3;
            this.btnKeyPad_0.Text = "0";
            this.btnKeyPad_0.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_0.UseVisualStyleBackColor = false;
            this.btnKeyPad_0.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_9
            // 
            this.btnKeyPad_9.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_9.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_9.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_9.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_9.BorderRadius = 40;
            this.btnKeyPad_9.BorderSize = 0;
            this.btnKeyPad_9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_9.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_9.Location = new System.Drawing.Point(313, 12);
            this.btnKeyPad_9.Name = "btnKeyPad_9";
            this.btnKeyPad_9.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_9.TabIndex = 2;
            this.btnKeyPad_9.Text = "9";
            this.btnKeyPad_9.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_9.UseVisualStyleBackColor = false;
            this.btnKeyPad_9.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_8
            // 
            this.btnKeyPad_8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_8.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_8.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_8.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_8.BorderRadius = 40;
            this.btnKeyPad_8.BorderSize = 0;
            this.btnKeyPad_8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_8.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_8.Location = new System.Drawing.Point(167, 12);
            this.btnKeyPad_8.Name = "btnKeyPad_8";
            this.btnKeyPad_8.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_8.TabIndex = 1;
            this.btnKeyPad_8.Text = "8";
            this.btnKeyPad_8.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_8.UseVisualStyleBackColor = false;
            this.btnKeyPad_8.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // btnKeyPad_7
            // 
            this.btnKeyPad_7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeyPad_7.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_7.BackgrounColor = System.Drawing.Color.RoyalBlue;
            this.btnKeyPad_7.BorderColor = System.Drawing.Color.PapayaWhip;
            this.btnKeyPad_7.BorderRadius = 40;
            this.btnKeyPad_7.BorderSize = 0;
            this.btnKeyPad_7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.btnKeyPad_7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyPad_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyPad_7.ForeColor = System.Drawing.Color.White;
            this.btnKeyPad_7.Location = new System.Drawing.Point(21, 12);
            this.btnKeyPad_7.Name = "btnKeyPad_7";
            this.btnKeyPad_7.Size = new System.Drawing.Size(140, 140);
            this.btnKeyPad_7.TabIndex = 0;
            this.btnKeyPad_7.Text = "7";
            this.btnKeyPad_7.TextColor = System.Drawing.Color.White;
            this.btnKeyPad_7.UseVisualStyleBackColor = false;
            this.btnKeyPad_7.Click += new System.EventHandler(this.btnKeyPad_Click);
            // 
            // txtBoxAdminPw
            // 
            this.txtBoxAdminPw.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBoxAdminPw.Location = new System.Drawing.Point(43, 311);
            this.txtBoxAdminPw.Name = "txtBoxAdminPw";
            this.txtBoxAdminPw.PasswordChar = '●';
            this.txtBoxAdminPw.ReadOnly = true;
            this.txtBoxAdminPw.Size = new System.Drawing.Size(816, 80);
            this.txtBoxAdminPw.TabIndex = 40;
            this.txtBoxAdminPw.TextChanged += new System.EventHandler(this.txtBoxAdminPw_TextChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(0, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(900, 100);
            this.label1.TabIndex = 39;
            this.label1.Text = "관리자 비밀번호를 입력하세요";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 38);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 76);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "관리모드 로그인";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmAdminLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 1920);
            this.Controls.Add(this.dbpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAdminLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAdminLogin";
            this.Load += new System.EventHandler(this.FrmAdminLogin_Load);
            this.dbpMain.ResumeLayout(false);
            this.dbpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOK)).EndInit();
            this.dbpKeypad.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBuffer_Panel dbpMain;
        private System.Windows.Forms.PictureBox picBoxCancel;
        private System.Windows.Forms.PictureBox picBoxOK;
        private DoubleBuffer_Panel dbpKeypad;
        private Kiosk.Controler.WButton btnKeyPad_Ent;
        private Kiosk.Controler.WButton btnKeyPad_3;
        private Kiosk.Controler.WButton btnKeyPad_2;
        private Kiosk.Controler.WButton btnKeyPad_1;
        private Kiosk.Controler.WButton btnKeyPad_Bsp;
        private Kiosk.Controler.WButton btnKeyPad_6;
        private Kiosk.Controler.WButton btnKeyPad_5;
        private Kiosk.Controler.WButton btnKeyPad_4;
        private Kiosk.Controler.WButton btnKeyPad_0;
        private Kiosk.Controler.WButton btnKeyPad_9;
        private Kiosk.Controler.WButton btnKeyPad_8;
        private Kiosk.Controler.WButton btnKeyPad_7;
        private System.Windows.Forms.TextBox txtBoxAdminPw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
    }
}