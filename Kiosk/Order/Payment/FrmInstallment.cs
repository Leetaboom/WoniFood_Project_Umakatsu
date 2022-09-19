using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk
{
    public partial class FrmInstallment : Form
    {
        string ctrlName;
        public string month;

        public FrmInstallment()
        {
            InitializeComponent();
        }

        public FrmInstallment(Bitmap bitmap)
        {
            InitializeComponent();

            this.Size = new Size(900, 900);
        }

        private void FrmInstallment_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            CreateMonthItemList();
        }
        
        private void picBoxBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ctrlName = ((Control)sender).Name;
            Bitmap bitmap;

            UtilHelper.spWav.Play();

            switch (ctrlName)
            {
                case "picBoxBack":
                    bitmap = UtilHelper.ChangeOpacity(picBoxBack.BackgroundImage, 1f);
                    picBoxBack.BackgroundImage.Dispose();
                    picBoxBack.BackgroundImage = bitmap;
                    bitmap = null;

                    break;

                case "lblLump":
                    bitmap = UtilHelper.ChangeOpacity(panelLump.BackgroundImage, 1f);
                    panelLump.BackgroundImage.Dispose();
                    panelLump.BackgroundImage = bitmap;
                    bitmap = null;

                    break;
            }
            
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (ctrlName)
            {
                case "picBoxBack":
                    picBoxBack.BackgroundImage.Dispose();
                    picBoxBack.BackgroundImage = Properties.Resources.btn_back1;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;

                case "lblLump":
                    panelLump.BackgroundImage.Dispose();
                    panelLump.BackgroundImage = Properties.Resources.btn_bg_orange;
                    UtilHelper.Delay(50);

                    MouseUp_Proc();
                    break;
            }
        }


        private void monthBtn_Click(object sender, EventArgs e)
        {
            string[] tmp = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            month = tmp[1];

            this.DialogResult = DialogResult.OK;
        }

        private void MouseUp_Proc()
        {
            switch (ctrlName)
            {
                case "picBoxBack":
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case "lblLump":
                    month = "00";
                    this.DialogResult = DialogResult.OK;
                    break;
            }
        }

        private void CreateMonthItemList()
        {
            Point monthBoxLoc = new Point(8, 8);
            Size monthBoxSize = new Size(panelMonth.Width, panelMonth.Height);

            int j = 0;
            int k = 0;

            for (int i = 0; i < 23; i++, monthBoxLoc.X += monthBoxSize.Width / 8)
            {
                if (j == 8)
                {
                    monthBoxLoc.Y += monthBoxSize.Height / 3;
                    monthBoxLoc.X = 8;
                    j = 0;
                    k++;
                }

                if (k == 3)
                    break;
                j++;

                Kiosk.Controler.WButton monthBtn = new Kiosk.Controler.WButton();
                monthBtn.Location = monthBoxLoc;
                monthBtn.Size = new Size(monthBoxSize.Width / 8 - 12, monthBoxSize.Height / 3 - 12);
                monthBtn.Name = "monthBtn_" + (i + 2);
                monthBtn.BackColor = Color.RoyalBlue;
                monthBtn.ForeColor = Color.White;
                monthBtn.FlatStyle = FlatStyle.Flat;
                monthBtn.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
                monthBtn.Text = string.Format("{0:D2}", i + 2);
                monthBtn.Font = new Font("나눔스퀘어", 20, FontStyle.Bold);
                monthBtn.Click += new EventHandler(monthBtn_Click);

                panelMonth.Controls.Add(monthBtn);
            }
        }
    }
}
