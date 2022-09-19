using Kiosk.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kiosk
{
    internal class MenuGrpInfo
    {
        public List<PictureBox> grpBoxList = new List<PictureBox>();
        public List<Label> grpBoxLabelList = new List<Label>();
        public List<string> grpBoxType = new List<string>();

        public void Add(PictureBox btn,int num, MouseEventHandler mouseDown, MouseEventHandler mouseUp)
        {
            btn.Name = "grpBox_" + num;
            btn.BackgroundImage = Resources.btn_menu_bg;
            btn.MouseDown += new MouseEventHandler(mouseDown);
            btn.MouseUp += new MouseEventHandler(mouseUp);

            Label lbl = new Label()
            {
                Name = "grpLbl_" + num,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("맑은 고딕", 18, FontStyle.Bold),
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(132, 92, 85),
                BackColor = Color.Transparent
            };
            lbl.MouseDown += new MouseEventHandler(mouseDown);
            lbl.MouseUp += new MouseEventHandler(mouseUp);

            btn.Controls.Add(lbl);
            grpBoxLabelList.Add(lbl);
            grpBoxList.Add(btn);
            grpBoxType.Add("0");
        }
    }
}
