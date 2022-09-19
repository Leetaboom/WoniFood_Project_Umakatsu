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
    public partial class FrmOrderList : Form
    {
        FrmMenuList menuList;
        FrmMenuListToGo menuListToGo;
        string ctrlName;
        string month;
        int ctrlIndex;
        Bitmap backImg;

        public List<DoubleBuffer_Panel> panelMain = new List<DoubleBuffer_Panel>();
        public List<DoubleBuffer_Panel> panelTitle = new List<DoubleBuffer_Panel>();
        public List<PictureBox> btnPlus = new List<PictureBox>();
        public List<PictureBox> btnMinus = new List<PictureBox>();
        public List<Label> labelTitle = new List<Label>();
        public List<Label> labelMemo = new List<Label>();
        public List<Label> labelQuan = new List<Label>();
        public List<Label> labelAmt = new List<Label>();
        public List<decimal> danga = new List<decimal>();

        System.Timers.Timer waitTime = new System.Timers.Timer(StoreInfo.BWaitTime.ToInteger() * 1000);

        public FrmOrderList()
        {
            InitializeComponent();
        }

        public FrmOrderList(Bitmap bitmap, List<decimal> danga, Form menuList)
        {
            InitializeComponent();

            this.BackgroundImage = bitmap;
            this.backImg = bitmap;
            this.danga = danga;
            if (menuList is FrmMenuList)
                this.menuList = menuList as FrmMenuList;
            else if (menuList is FrmMenuListToGo)
                this.menuListToGo = menuList as FrmMenuListToGo;
            waitTime.Elapsed += OnTimedEvent;
        }

        private void FrmOrderList_Load(object sender, EventArgs e)
        {
            dbpMain.Left = (this.ClientSize.Width - dbpMain.Width) / 2;
            dbpMain.Top = (this.ClientSize.Height - dbpMain.Height) / 2;

            lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", 0);
            OrderListInit();

            waitTime.Start();
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            ClearMemory();
            this.DialogResult = DialogResult.OK;
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
                    if (picBoxBack.BackgroundImage != null)
                    {
                        picBoxBack.BackgroundImage.Dispose();
                        picBoxBack.BackgroundImage = null;
                    }
                    picBoxBack.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxCard":
                    bitmap = UtilHelper.ChangeOpacity(picBoxCard.BackgroundImage, 1f);
                    if (picBoxCard.BackgroundImage != null)
                    {
                        picBoxCard.BackgroundImage.Dispose();
                        picBoxCard.BackgroundImage = null;
                    }
                    picBoxCard.BackgroundImage = bitmap;
                    bitmap = null;
                    break;

                case "picBoxSpay":
                    bitmap = UtilHelper.ChangeOpacity(picBoxSpay.BackgroundImage, 1f);
                    if (picBoxSpay.BackgroundImage != null)
                    {
                        picBoxSpay.BackgroundImage.Dispose();
                        picBoxSpay.BackgroundImage = null;
                    }
                    picBoxSpay.BackgroundImage = bitmap;
                    bitmap = null;
                    break;
            }
        }

        private void picBoxBtn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (ctrlName)
            {
                case "picBoxBack":
                    if (picBoxBack.BackgroundImage != null)
                    {
                        picBoxBack.BackgroundImage.Dispose();
                        picBoxBack.BackgroundImage = null;
                    }
                    picBoxBack.BackgroundImage = Properties.Resources.btn_back;
                    UtilHelper.Delay(100);

                    MouseUp_Proc();
                    break;

                case "picBoxCard":
                    if (picBoxCard.BackgroundImage != null)
                    {
                        picBoxCard.BackgroundImage.Dispose();
                        picBoxCard.BackgroundImage = null;
                    }
                    picBoxCard.BackgroundImage = Properties.Resources.btn_card;
                    UtilHelper.Delay(100);

                    MouseUp_Proc();
                    break;

                case "picBoxSpay":
                    if (picBoxSpay.BackgroundImage != null)
                    {
                        picBoxSpay.BackgroundImage.Dispose();
                        picBoxSpay.BackgroundImage = null;
                    }
                    picBoxSpay.BackgroundImage = Properties.Resources.btnSamsungPay;
                    UtilHelper.Delay(100);

                    MouseUp_Proc();
                    break;
            }
        }

        private void MouseUp_Proc()
        {
            switch (ctrlName)
            {
                case "picBoxBack":
                    ClearMemory();
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case "picBoxCard":

                    switch (StoreInfo.VanSelect)
                    {
                        case "1":
                            break;

                        case "2":
                            dbpMain.Visible = false;
                            waitTime.Stop();

                            if (lblTotalAmt.Text.ToDecimal() >= 50000)
                            {
                                FrmInstallment install = new FrmInstallment(backImg);

                                if (install.ShowDialog() == DialogResult.OK)
                                {
                                    month = string.Format("{0:D2}", install.month.ToInteger());

                                    FrmCardPayMentNice cardPayMent = new FrmCardPayMentNice(backImg, danga, lblTotalAmt.Text, month, string.Empty);

                                    cardPayMent.TopMost = true;
                                    if (cardPayMent.ShowDialog() == DialogResult.OK)
                                        this.DialogResult = DialogResult.OK;
                                    else if (cardPayMent.DialogResult == DialogResult.Yes)
                                        this.DialogResult = DialogResult.Yes;
                                    else if (cardPayMent.DialogResult == DialogResult.Cancel) 
                                    {
                                        dbpMain.Visible = true;
                                        waitTime.Start();
                                    }
                                }
                                else if (install.DialogResult == DialogResult.Cancel)
                                {
                                    dbpMain.Visible = true;
                                    waitTime.Start();
                                }
                            }
                            else
                            {
                                month = "00";
                                FrmCardPayMentNice cardPayMent = new FrmCardPayMentNice(backImg, danga, lblTotalAmt.Text, month, string.Empty);

                                if (cardPayMent.ShowDialog() == DialogResult.OK)
                                    this.DialogResult = DialogResult.OK;
                                else if (cardPayMent.DialogResult == DialogResult.Yes)
                                    this.DialogResult = DialogResult.Yes;
                                else
                                {
                                    dbpMain.Visible = true;
                                    waitTime.Start();
                                }
                            }
                            break;

                        case "3":
                            break;

                        case "4":
                            break;

                        case "5":
                            dbpMain.Visible = false;
                            waitTime.Stop();

                            if (lblTotalAmt.Text.ToDecimal() >= 50000)
                            {
                                FrmInstallment install = new FrmInstallment(backImg);

                                if (install.ShowDialog() == DialogResult.OK)
                                {
                                    month = string.Format("{0:D2}", install.month.ToInteger());

                                    FrmCardPayMentDaou cardPayMent = new FrmCardPayMentDaou(backImg, danga, lblTotalAmt.Text, month, string.Empty);

                                    if (cardPayMent.ShowDialog() == DialogResult.OK)
                                        this.DialogResult = DialogResult.OK;
                                    else if (cardPayMent.DialogResult == DialogResult.Yes)
                                        this.DialogResult = DialogResult.Yes;
                                    else if (cardPayMent.DialogResult == DialogResult.Cancel)
                                    {
                                        dbpMain.Visible = true;
                                        waitTime.Start();
                                    }
                                }
                                else if (install.DialogResult == DialogResult.Cancel)
                                {
                                    dbpMain.Visible = true;
                                    waitTime.Start();
                                }
                            }
                            else
                            {
                                month = "00";
                                FrmCardPayMentDaou cardPayMent = new FrmCardPayMentDaou(backImg, danga, lblTotalAmt.Text, month, string.Empty);

                                if (cardPayMent.ShowDialog() == DialogResult.OK)
                                    this.DialogResult = DialogResult.OK;
                                else if (cardPayMent.DialogResult == DialogResult.Yes)
                                    this.DialogResult = DialogResult.Yes;
                                else
                                {
                                    dbpMain.Visible = true;
                                    waitTime.Start();
                                }
                            }
                            break;
                    }
                    
                    break;

                case "picBoxSpay":
                    dbpMain.Visible = false;
                    waitTime.Stop();

                    switch(StoreInfo.VanSelect)
                    {
                        case "1":
                            break;
                        case "2":
                            if (lblTotalAmt.Text.ToDecimal() >= 50000)
                            {
                                FrmInstallment monthInput = new FrmInstallment(backImg);

                                if (monthInput.ShowDialog() == DialogResult.OK)
                                {
                                    month = string.Format("{0:D2}", monthInput.month.ToInteger());
                                    FrmSamPayMentNice spay = new FrmSamPayMentNice(this.backImg, danga, lblTotalAmt.Text, month, string.Empty);

                                    if (spay.ShowDialog() == DialogResult.OK)
                                        DialogResult = DialogResult.OK;
                                    else if (spay.DialogResult == DialogResult.Yes)
                                        DialogResult = DialogResult.Yes;
                                    else if (spay.DialogResult == DialogResult.Cancel)
                                    {
                                        dbpMain.Visible = true;
                                        waitTime.Start();
                                    }
                                }
                                else if (monthInput.DialogResult == DialogResult.Cancel)
                                {
                                    dbpMain.Visible = true;
                                    waitTime.Start();
                                }

                            }
                            else
                            {
                                month = "00";
                                FrmSamPayMentNice spay = new FrmSamPayMentNice(this.backImg, danga, lblTotalAmt.Text, month, string.Empty);

                                if (spay.ShowDialog() == DialogResult.OK)
                                    DialogResult = DialogResult.OK;
                                else if (spay.DialogResult == DialogResult.Yes)
                                    DialogResult = DialogResult.Yes;
                                else
                                {
                                    dbpMain.Visible = true;
                                    waitTime.Start();
                                }
                            }
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                        case "5":
                            if (lblTotalAmt.Text.ToDecimal() >= 50000)
                            {
                                FrmInstallment monthInput = new FrmInstallment(backImg);

                                if (monthInput.ShowDialog() == DialogResult.OK)
                                {
                                    month = string.Format("{0:D2}", monthInput.month.ToInteger());
                                    FrmSamPayMentDaou spay = new FrmSamPayMentDaou(this.backImg, danga, lblTotalAmt.Text, month, string.Empty);

                                    if (spay.ShowDialog() == DialogResult.OK)
                                        DialogResult = DialogResult.OK;
                                    else if (spay.DialogResult == DialogResult.Yes)
                                        DialogResult = DialogResult.Yes;
                                    else if (spay.DialogResult == DialogResult.Cancel)
                                    {
                                        dbpMain.Visible = true;
                                        waitTime.Start();
                                    }
                                }
                                else if (monthInput.DialogResult == DialogResult.Cancel)
                                {
                                    dbpMain.Visible = true;
                                    waitTime.Start();
                                }

                            }
                            else
                            {
                                month = "00";
                                FrmSamPayMentDaou spay = new FrmSamPayMentDaou(this.backImg, danga, lblTotalAmt.Text, month, string.Empty);

                                if (spay.ShowDialog() == DialogResult.OK)
                                    DialogResult = DialogResult.OK;
                                else if (spay.DialogResult == DialogResult.Yes)
                                    DialogResult = DialogResult.Yes;
                                else
                                {
                                    dbpMain.Visible = true;
                                    waitTime.Start();
                                }
                            }
                            break;
                        case "6":
                            break;
                    }

                    break;
            }
        }

        private void picBoxPlusMinusBtn_MouseDown(object sender, MouseEventArgs e)
        {
            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            Bitmap bitmap;
            ctrlIndex = tmpName[1].ToInteger();
            
            UtilHelper.spWav.Play();

            if (tmpName[0].StartsWith("listPlus"))
            {
                bitmap = UtilHelper.ChangeOpacity(btnPlus[ctrlIndex].BackgroundImage, 1f);
                if (btnPlus[ctrlIndex].BackgroundImage != null)
                {
                    btnPlus[ctrlIndex].BackgroundImage.Dispose();
                    btnPlus[ctrlIndex].BackgroundImage = null;
                }
                btnPlus[ctrlIndex].BackgroundImage = bitmap;
                bitmap = null;
            }
            else
            {
                bitmap = UtilHelper.ChangeOpacity(btnMinus[ctrlIndex].BackgroundImage, 1f);
                if (btnMinus[ctrlIndex].BackgroundImage != null)
                {
                    btnMinus[ctrlIndex].BackgroundImage.Dispose();
                    btnMinus[ctrlIndex].BackgroundImage = null;
                }
                btnMinus[ctrlIndex].BackgroundImage = bitmap;
                bitmap = null;
            }
        }

        private void picBoxPlusMinusBtn_MouseUp(object sender, MouseEventArgs e)
        {
            string[] tmpName = ((Control)sender).Name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (tmpName[0].StartsWith("listPlus"))
            {
                if (btnPlus[ctrlIndex].BackgroundImage != null)
                {
                    btnPlus[ctrlIndex].BackgroundImage.Dispose();
                    btnPlus[ctrlIndex].BackgroundImage = null;
                }
                btnPlus[ctrlIndex].BackgroundImage = Properties.Resources.btn_plus_big;
                UtilHelper.Delay(50);

                labelQuan[ctrlIndex].Text = (labelQuan[ctrlIndex].Text.ToInteger() + 1).ToString();
                labelAmt[ctrlIndex].Text = string.Format(@"\ {0:#,##0}", labelAmt[ctrlIndex].Text.ToDecimal() + danga[ctrlIndex]);

                OrderListInfo.ItemQuan[ctrlIndex] = labelQuan[ctrlIndex].Text.ToInteger();
                OrderListInfo.ItemAmt[ctrlIndex] = labelAmt[ctrlIndex].Text.ToDecimal();

                lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", lblTotalAmt.Text.ToDecimal() + danga[ctrlIndex]);

                if(menuList != null)
                {
                    if (menuList is IFormMessage)
                        (menuList as IFormMessage).Receive(null, "Up-" + ctrlIndex + "-" + lblTotalAmt.Text);
                }
                else if(menuListToGo != null)
                {
                    if (menuListToGo is IFormMessage)
                        (menuListToGo as IFormMessage).Receive(null, "Up-" + ctrlIndex + "-" + lblTotalAmt.Text);
                }
            }
            else
            {
                if (btnMinus[ctrlIndex].BackgroundImage != null)
                {
                    btnMinus[ctrlIndex].BackgroundImage.Dispose();
                    btnMinus[ctrlIndex].BackgroundImage = null;
                }
                btnMinus[ctrlIndex].BackgroundImage = Properties.Resources.btn_minus_big;
                UtilHelper.Delay(50);

                if (labelQuan[ctrlIndex].Text.ToInteger() > 1)
                {
                    labelQuan[ctrlIndex].Text = (labelQuan[ctrlIndex].Text.ToInteger() - 1).ToString();
                    labelAmt[ctrlIndex].Text = string.Format(@"\ {0:#,##0}", labelAmt[ctrlIndex].Text.ToDecimal() - danga[ctrlIndex]);

                    OrderListInfo.ItemQuan[ctrlIndex] = labelQuan[ctrlIndex].Text.ToInteger();
                    OrderListInfo.ItemAmt[ctrlIndex] = labelAmt[ctrlIndex].Text.ToDecimal();

                    lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", lblTotalAmt.Text.ToDecimal() - danga[ctrlIndex]);
                    if (menuList != null)
                    {
                        if (menuList is IFormMessage)
                            (menuList as IFormMessage).Receive(null, "Down-" + ctrlIndex + "-" + lblTotalAmt.Text);
                    }
                    else if (menuListToGo != null)
                    {
                        if (menuListToGo is IFormMessage)
                            (menuListToGo as IFormMessage).Receive(null, "Down-" + ctrlIndex + "-" + lblTotalAmt.Text);
                    }
                }
                else
                {
                    FrmOrderCancelBox orderCancel = new FrmOrderCancelBox(UtilHelper.ScreenCapture(
                        this.Width, this.Height, this.Location), labelTitle[ctrlIndex].Text);

                    if (orderCancel.ShowDialog() == DialogResult.OK)
                    {
                        lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", lblTotalAmt.Text.ToDecimal() - danga[ctrlIndex]);

                        woniPanOrderList.Controls.Remove(panelMain[ctrlIndex]);
                        panelMain.RemoveAt(ctrlIndex);
                        panelTitle.RemoveAt(ctrlIndex);
                        labelTitle.RemoveAt(ctrlIndex);
                        labelMemo.RemoveAt(ctrlIndex);
                        labelAmt.RemoveAt(ctrlIndex);
                        labelQuan.RemoveAt(ctrlIndex);
                        btnMinus.RemoveAt(ctrlIndex);
                        btnPlus.RemoveAt(ctrlIndex);
                         
                        OrderListInfo.ItemName.RemoveAt(ctrlIndex);
                        OrderListInfo.ItemAmt.RemoveAt(ctrlIndex);
                        OrderListInfo.ItemOpNm.RemoveAt(ctrlIndex);
                        OrderListInfo.ItemOptAmt1.RemoveAt(ctrlIndex);
                        OrderListInfo.ItemOptAmt2.RemoveAt(ctrlIndex);
                        OrderListInfo.ItemQuan.RemoveAt(ctrlIndex);
                        OrderListInfo.Memo1.RemoveAt(ctrlIndex);
                        OrderListInfo.Memo2.RemoveAt(ctrlIndex);

                        for (int i = 0; i < btnPlus.Count; i++)
                        {
                            btnMinus[i].Name = "listMinusBtn_" + i;
                            btnPlus[i].Name = "listPlusBtn_" + i;
                        }


                        if (menuList != null)
                        {
                            if (menuList is IFormMessage)
                                (menuList as IFormMessage).Receive(null, "Down-" + ctrlIndex + "-" + lblTotalAmt.Text);
                        }
                        else if (menuListToGo != null)
                        {
                            if (menuListToGo is IFormMessage)
                                (menuListToGo as IFormMessage).Receive(null, "Down-" + ctrlIndex + "-" + lblTotalAmt.Text);
                        }

                        if (panelMain.Count == 0)
                        {
                            //ClearMemory();
                            this.DialogResult = DialogResult.Cancel;
                        }
                    }
                }
            }
        }


        private void OrderListInit()
        {
            for (int i = 0; i < OrderListInfo.ItemName.Count; i++)
                CreateCustomOrList(i);
        }

        private void CreateCustomOrList(int index)
        {
            DoubleBuffer_Panel listMainPanel = new DoubleBuffer_Panel();
            listMainPanel.Height = 90;

            DoubleBuffer_Panel listTitlePanel = new DoubleBuffer_Panel();
            listTitlePanel.Name = "listTitlePanel_" + panelTitle.Count;
            listTitlePanel.Size = new Size(480, 90);
            listTitlePanel.BackColor = Color.White;

            Label lblTitle = new Label();
            lblTitle.Name = "lblTitle_" + labelTitle.Count;
            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 70;
            lblTitle.Font = new Font("나눔스퀘어", 20, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            Label lblMemo = new Label();
            lblMemo.Name = "lblMemo_" + labelMemo.Count;
            lblMemo.AutoSize = false;
            lblMemo.Dock = DockStyle.Bottom;
            lblMemo.Height = 20;
            lblMemo.Font = new Font("나눔스퀘어", 12, FontStyle.Bold);
            lblMemo.TextAlign = ContentAlignment.MiddleLeft;
            lblMemo.ForeColor = Color.RoyalBlue;

            PictureBox listPlusBtn = new PictureBox();
            listPlusBtn.Name = "listPlusBtn_" + btnPlus.Count;
            listPlusBtn.Size = new Size(73, 73);
            listPlusBtn.Location = new Point(listTitlePanel.Width + 2, 3);
            listPlusBtn.BackgroundImage = Properties.Resources.btn_plus_big;
            listPlusBtn.BackgroundImageLayout = ImageLayout.Stretch;
            listPlusBtn.MouseDown += new MouseEventHandler(picBoxPlusMinusBtn_MouseDown);
            listPlusBtn.MouseUp += new MouseEventHandler(picBoxPlusMinusBtn_MouseUp);

            Label lblQuan = new Label();
            lblQuan.Name = "lblQuan_" + labelQuan.Count;
            lblQuan.AutoSize = false;
            lblQuan.Size = new Size(70, 90);
            lblQuan.Location = new Point(listTitlePanel.Width + listPlusBtn.Width + 4, 0);
            lblQuan.Font = new Font("나눔스퀘어", 20, FontStyle.Bold);
            lblQuan.TextAlign = ContentAlignment.MiddleCenter;

            PictureBox listMinusBtn = new PictureBox();
            listMinusBtn.Name = "listMinusBtn_" + btnMinus.Count;
            listMinusBtn.Size = new Size(73, 73);
            listMinusBtn.Location = new Point(listTitlePanel.Width + listPlusBtn.Width + lblQuan.Width + 6, 3);
            listMinusBtn.BackgroundImage = Properties.Resources.btn_minus_big;
            listMinusBtn.BackgroundImageLayout = ImageLayout.Stretch;
            listMinusBtn.MouseDown += new MouseEventHandler(picBoxPlusMinusBtn_MouseDown);
            listMinusBtn.MouseUp += new MouseEventHandler(picBoxPlusMinusBtn_MouseUp);

            Label lblAmount = new Label();
            lblAmount.Name = "lblAmount_" + labelAmt.Count;
            lblAmount.AutoSize = false;
            lblAmount.Size = new Size(200, 90);
            lblAmount.Location = new Point(listTitlePanel.Width + listPlusBtn.Width + lblQuan.Width + listMinusBtn.Width + 8, 0);
            lblAmount.Font = new Font("나눔스퀘어", 20, FontStyle.Bold);
            lblAmount.TextAlign = ContentAlignment.MiddleLeft;

            lblTitle.Text = OrderListInfo.ItemName[index] + " " + OrderListInfo.ItemOpNm[index];
            if (OrderListInfo.Memo1[index] != string.Empty && OrderListInfo.Memo2[index] != string.Empty)
                lblMemo.Text = OrderListInfo.Memo1[index] + "," + OrderListInfo.Memo2[index];
            else if (OrderListInfo.Memo1[index] != string.Empty && OrderListInfo.Memo2[index] == string.Empty)
                lblMemo.Text = OrderListInfo.Memo1[index];
            else
                lblMemo.Text = OrderListInfo.Memo2[index];

            lblQuan.Text = OrderListInfo.ItemQuan[index].ToString();
            lblAmount.Text = string.Format(@"\ {0:#,##0}", OrderListInfo.ItemAmt[index]);
            lblTotalAmt.Text = string.Format(@"\ {0:#,##0}", lblTotalAmt.Text.ToDecimal() +
                OrderListInfo.ItemAmt[index]);

            panelMain.Add(listMainPanel);
            panelTitle.Add(listTitlePanel);
            btnPlus.Add(listPlusBtn);
            btnMinus.Add(listMinusBtn);
            labelTitle.Add(lblTitle);
            labelMemo.Add(lblMemo);
            labelQuan.Add(lblQuan);
            labelAmt.Add(lblAmount);

            listTitlePanel.Controls.Add(lblTitle);
            listTitlePanel.Controls.Add(lblMemo);
            listMainPanel.Controls.Add(listTitlePanel);
            listMainPanel.Controls.Add(listPlusBtn);
            listMainPanel.Controls.Add(lblQuan);
            listMainPanel.Controls.Add(listMinusBtn);
            listMainPanel.Controls.Add(lblAmount);

            woniPanOrderList.Controls.Add(listMainPanel);
        }

        private void ClearMemory()
        {
            waitTime.Stop();
            waitTime.Dispose();

            panelMain = null;
            panelTitle = null;
            btnPlus = null;
            btnMinus = null;
            labelTitle = null;
            labelMemo = null;
            labelQuan = null;
            lblTotalAmt = null;
            danga = null;
            backImg = null;
        }
    }
}