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
    public partial class FrmPayMentComple : Form
    {
        Form parent;

        public FrmPayMentComple(Form parent, Bitmap bitmap)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            this.parent = parent;
        }

        private void FrmPayMentComple_Load(object sender, EventArgs e)
        {
            this.Location = parent.Location;
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;
        }

        private void picBoxLogo_DoubleClick(object sender, EventArgs e)
        {


            this.TopMost = false;

            FrmAdminLogin login = new FrmAdminLogin(UtilHelper.ScreenCapture(
                this.Width, this.Height, this.Location));

            //이미지슬라이드 스톱
            //timerImage.Stop();

            if (login.ShowDialog() == DialogResult.OK)
            {
                FrmAdmin admin = new FrmAdmin();

                if (admin.ShowDialog() == DialogResult.OK)
                {
                    if (StoreInfo.IsHoldMode)
                    {
                    }

                    this.TopMost = true;

                }
                else if (admin.DialogResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    try
                    {
                        this.TopMost = true;
                    }
                    catch { }
                }
            }
            else
            {
                this.TopMost = true;
            }
        }
    }
}
