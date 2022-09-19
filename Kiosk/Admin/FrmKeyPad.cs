using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Kiosk
{
    public partial class FrmKeyPad : FrmKeyPadNoActive
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(uint vk, uint scan, uint flags, uint extraInfo);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtualKey);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("imm32.dll")]
        private static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);

        [DllImport("vkb.dll", CharSet = CharSet.Auto)]
        private static extern void InitHook(IntPtr hHandle);

        [DllImport("vkb.dll", CharSet = CharSet.Auto)]
        private static extern void InstallHook();

        const int WM_IME_CONTROL = 643;

        Controler.WoniKeyboardHook kbh;

        bool bHangeul = false;

        public FrmKeyPad()
        {
            InitializeComponent();
        }

        public FrmKeyPad(Bitmap bitmap, string title)
        {
            InitializeComponent();
            this.BackgroundImage = bitmap;
            //this.Size = new Size(woniPanel1.Width, woniPanel1.Height);
            InitHook(dbpKeypad.Handle);
            InstallHook();

            kbh = new Controler.WoniKeyboardHook();
            lblTitle.Text = title;
        }

        private void FrmKeyPad_Load(object sender, EventArgs e)
        {
            //woniPanel1.Left = (this.ClientSize.Width - woniPanel1.Width) / 2;
            //woniPanel1.Top = (this.ClientSize.Height - woniPanel1.Height) / 2;
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            Control KeyButton = (Control)sender;

            UtilHelper.spKWav.Play();
            txtBoxKeyValue.Focus();
            txtBoxKeyValue.Select(txtBoxKeyValue.Text.Length, 0);
            switch (KeyButton.Name)
            {
                case "btn0":
                    keybd_event((byte)Keys.D0, 0, 0, 0);
                    keybd_event((byte)Keys.D0, 0, 0x02, 0);
                    break;

                case "btn1":
                    keybd_event((byte)Keys.D1, 0, 0, 0);
                    keybd_event((byte)Keys.D1, 0, 0x02, 0);
                    break;

                case "btn2":
                    keybd_event((byte)Keys.D2, 0, 0, 0);
                    keybd_event((byte)Keys.D2, 0, 0x02, 0);
                    break;

                case "btn3":
                    keybd_event((byte)Keys.D3, 0, 0, 0);
                    keybd_event((byte)Keys.D3, 0, 0x02, 0);
                    break;

                case "btn4":
                    keybd_event((byte)Keys.D4, 0, 0, 0);
                    keybd_event((byte)Keys.D4, 0, 0x02, 0);
                    break;

                case "btn5":
                    keybd_event((byte)Keys.D5, 0, 0, 0);
                    keybd_event((byte)Keys.D5, 0, 0x02, 0);
                    break;

                case "btn6":
                    keybd_event((byte)Keys.D6, 0, 0, 0);
                    keybd_event((byte)Keys.D6, 0, 0x02, 0);
                    break;

                case "btn7":
                    keybd_event((byte)Keys.D7, 0, 0, 0);
                    keybd_event((byte)Keys.D7, 0, 0x02, 0);
                    break;

                case "btn8":
                    keybd_event((byte)Keys.D8, 0, 0, 0);
                    keybd_event((byte)Keys.D8, 0, 0x02, 0);
                    break;

                case "btn9":
                    keybd_event((byte)Keys.D9, 0, 0, 0);
                    keybd_event((byte)Keys.D9, 0, 0x02, 0);
                    break;

                case "btnA":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.A, 0, 0, 0);
                    keybd_event((byte)Keys.A, 0, 0x02, 0);
                    break;

                case "btnB":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.B, 0, 0, 0);
                    keybd_event((byte)Keys.B, 0, 0x02, 0);
                    break;

                case "btnC":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.C, 0, 0, 0);
                    keybd_event((byte)Keys.C, 0, 0x02, 0);
                    break;

                case "btnD":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.D, 0, 0, 0);
                    keybd_event((byte)Keys.D, 0, 0x02, 0);
                    break;

                case "btnE":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.E, 0, 0, 0);
                    keybd_event((byte)Keys.E, 0, 0x02, 0);
                    break;

                case "btnF":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.F, 0, 0, 0);
                    keybd_event((byte)Keys.F, 0, 0x02, 0);
                    break;

                case "btnG":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.G, 0, 0, 0);
                    keybd_event((byte)Keys.G, 0, 0x02, 0);
                    break;

                case "btnH":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.H, 0, 0, 0);
                    keybd_event((byte)Keys.H, 0, 0x02, 0);
                    break;

                case "btnI":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.I, 0, 0, 0);
                    keybd_event((byte)Keys.I, 0, 0x02, 0);
                    break;

                case "btnJ":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.J, 0, 0, 0);
                    keybd_event((byte)Keys.J, 0, 0x02, 0);
                    break;

                case "btnK":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.K, 0, 0, 0);
                    keybd_event((byte)Keys.K, 0, 0x02, 0);
                    break;

                case "btnL":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.L, 0, 0, 0);
                    keybd_event((byte)Keys.L, 0, 0x02, 0);
                    break;

                case "btnM":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.M, 0, 0, 0);
                    keybd_event((byte)Keys.M, 0, 0x02, 0);
                    break;

                case "btnN":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.N, 0, 0, 0);
                    keybd_event((byte)Keys.N, 0, 0x02, 0);
                    break;

                case "btnO":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.O, 0, 0, 0);
                    keybd_event((byte)Keys.O, 0, 0x02, 0);
                    break;

                case "btnP":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.P, 0, 0, 0);
                    keybd_event((byte)Keys.P, 0, 0x02, 0);
                    break;

                case "btnQ":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.Q, 0, 0, 0);
                    keybd_event((byte)Keys.Q, 0, 0x02, 0);
                    break;

                case "btnR":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.R, 0, 0, 0);
                    keybd_event((byte)Keys.R, 0, 0x02, 0);
                    break;

                case "btnS":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.S, 0, 0, 0);
                    keybd_event((byte)Keys.S, 0, 0x02, 0);
                    break;

                case "btnT":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.T, 0, 0, 0);
                    keybd_event((byte)Keys.T, 0, 0x02, 0);
                    break;

                case "btnU":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.U, 0, 0, 0);
                    keybd_event((byte)Keys.U, 0, 0x02, 0);
                    break;

                case "btnV":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.V, 0, 0, 0);
                    keybd_event((byte)Keys.V, 0, 0x02, 0);
                    break;

                case "btnW":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.W, 0, 0, 0);
                    keybd_event((byte)Keys.W, 0, 0x02, 0);
                    break;

                case "btnX":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.X, 0, 0, 0);
                    keybd_event((byte)Keys.X, 0, 0x02, 0);
                    break;

                case "btnY":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.Y, 0, 0, 0);
                    keybd_event((byte)Keys.Y, 0, 0x02, 0);
                    break;

                case "btnZ":
                    if (HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.Z, 0, 0, 0);
                    keybd_event((byte)Keys.Z, 0, 0x02, 0);
                    break;

                case "btnㄱ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.R, 0, 0, 0);
                    keybd_event((byte)Keys.R, 0, 0x02, 0);
                    break;

                case "btnㄴ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.S, 0, 0, 0);
                    keybd_event((byte)Keys.S, 0, 0x02, 0);
                    break;

                case "btnㄷ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.E, 0, 0, 0);
                    keybd_event((byte)Keys.E, 0, 0x02, 0);
                    break;

                case "btnㄹ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.F, 0, 0, 0);
                    keybd_event((byte)Keys.F, 0, 0x02, 0);
                    break;

                case "btnㅁ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.A, 0, 0, 0);
                    keybd_event((byte)Keys.A, 0, 0x02, 0);
                    break;

                case "btnㅂ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.Q, 0, 0, 0);
                    keybd_event((byte)Keys.Q, 0, 0x02, 0);
                    bHangeul = true;
                    break;

                case "btnㅅ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.T, 0, 0, 0);
                    keybd_event((byte)Keys.T, 0, 0x02, 0);
                    break;

                case "btnㅇ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.D, 0, 0, 0);
                    keybd_event((byte)Keys.D, 0, 0x02, 0);
                    break;

                case "btnㅈ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.W, 0, 0, 0);
                    keybd_event((byte)Keys.W, 0, 0x02, 0);
                    break;

                case "btnㅊ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.C, 0, 0, 0);
                    keybd_event((byte)Keys.C, 0, 0x02, 0);
                    break;

                case "btnㅋ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.Z, 0, 0, 0);
                    keybd_event((byte)Keys.Z, 0, 0x02, 0);
                    break;

                case "btnㅌ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.X, 0, 0, 0);
                    keybd_event((byte)Keys.X, 0, 0x02, 0);
                    break;

                case "btnㅍ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.V, 0, 0, 0);
                    keybd_event((byte)Keys.V, 0, 0x02, 0);
                    break;

                case "btnㅎ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.G, 0, 0, 0);
                    keybd_event((byte)Keys.G, 0, 0x02, 0);
                    break;

                case "btnㄲ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.R, 0, 0, 0);
                    keybd_event((byte)Keys.R, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnㄸ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.E, 0, 0, 0);
                    keybd_event((byte)Keys.E, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnㅃ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.Q, 0, 0, 0);
                    keybd_event((byte)Keys.Q, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnㅆ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.T, 0, 0, 0);
                    keybd_event((byte)Keys.T, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnㅉ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.W, 0, 0, 0);
                    keybd_event((byte)Keys.W, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnㅏ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.K, 0, 0, 0);
                    keybd_event((byte)Keys.K, 0, 0x02, 0);
                    break;

                case "btnㅑ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.I, 0, 0, 0);
                    keybd_event((byte)Keys.I, 0, 0x02, 0);
                    break;

                case "btnㅓ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.J, 0, 0, 0);
                    keybd_event((byte)Keys.J, 0, 0x02, 0);
                    break;

                case "btnㅕ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.U, 0, 0, 0);
                    keybd_event((byte)Keys.U, 0, 0x02, 0);
                    break;

                case "btnㅗ":
                    keybd_event((byte)Keys.H, 0, 0, 0);
                    keybd_event((byte)Keys.H, 0, 0x02, 0);
                    break;

                case "btnㅛ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.Y, 0, 0, 0);
                    keybd_event((byte)Keys.Y, 0, 0x02, 0);
                    break;

                case "btnㅜ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.N, 0, 0, 0);
                    keybd_event((byte)Keys.N, 0, 0x02, 0);
                    break;

                case "btnㅠ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.B, 0, 0, 0);
                    keybd_event((byte)Keys.B, 0, 0x02, 0);
                    break;

                case "btnㅡ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.M, 0, 0, 0);
                    keybd_event((byte)Keys.M, 0, 0x02, 0);
                    break;

                case "btnㅣ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.L, 0, 0, 0);
                    keybd_event((byte)Keys.L, 0, 0x02, 0);
                    break;

                case "btnㅐ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.O, 0, 0, 0);
                    keybd_event((byte)Keys.O, 0, 0x02, 0);
                    break;

                case "btnㅔ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.P, 0, 0, 0);
                    keybd_event((byte)Keys.P, 0, 0x02, 0);
                    break;

                case "btnㅒ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.O, 0, 0, 0);
                    keybd_event((byte)Keys.O, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnㅖ":
                    if (!HanguelModeCheck())
                    {
                        keybd_event((byte)Keys.HanguelMode, 0, 0, 0);
                        keybd_event((byte)Keys.HanguelMode, 0, 0x02, 0);
                    }
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.P, 0, 0, 0);
                    keybd_event((byte)Keys.P, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnMinus":
                    keybd_event((byte)Keys.OemMinus, 0, 0, 0);
                    keybd_event((byte)Keys.OemMinus, 0, 0x02, 0);
                    break;

                case "btnPlus":
                    keybd_event((byte)Keys.Add, 0, 0, 0);
                    keybd_event((byte)Keys.Add, 0, 0x02, 0);
                    break;

                case "btnEquals":
                    keybd_event((byte)Keys.Oemplus, 0, 0, 0);
                    keybd_event((byte)Keys.Oemplus, 0, 0x02, 0);
                    break;

                case "btnDivision":
                    keybd_event((byte)Keys.Divide, 0, 0, 0);
                    keybd_event((byte)Keys.Divide, 0, 0x02, 0);
                    break;

                case "btnMulti":
                    keybd_event((byte)Keys.Multiply, 0, 0, 0);
                    keybd_event((byte)Keys.Multiply, 0, 0x02, 0);
                    break;

                case "btnColon":
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.OemSemicolon, 0, 0, 0);
                    keybd_event((byte)Keys.OemSemicolon, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnAt":
                    keybd_event((byte)Keys.LShiftKey, 0, 0, 0);
                    keybd_event((byte)Keys.D2, 0, 0, 0);
                    keybd_event((byte)Keys.D2, 0, 0x02, 0);
                    keybd_event((byte)Keys.LShiftKey, 0, 0x02, 0);
                    break;

                case "btnBackslash":
                    keybd_event((byte)Keys.OemPipe, 0, 0, 0);
                    keybd_event((byte)Keys.OemPipe, 0, 0x02, 0);
                    break;

                case "btnPeriod":
                    keybd_event((byte)Keys.OemPeriod, 0, 0, 0);
                    keybd_event((byte)Keys.OemPeriod, 0, 0x02, 0);
                    break;

                case "btnSpace":
                    keybd_event((byte)Keys.Space, 0, 0, 0);
                    keybd_event((byte)Keys.Space, 0, 0x02, 0);
                    break;

                case "btnBsp":
                    keybd_event((byte)Keys.Back, 0, 0, 0);
                    keybd_event((byte)Keys.Back, 0, 0x02, 0);
                    break;

                case "btnCap":
                    keybd_event((byte)Keys.CapsLock, 0, 0, 0);
                    keybd_event((byte)Keys.CapsLock, 0, 0x02, 0);
                    break;

                case "btnCancel":
                    this.Dispose();
                    break;

                case "btnOK":
                    System.Threading.Thread.Sleep(200);
                    this.DialogResult = DialogResult.OK;
                    break;
            }
        }

        private void btnOK_MouseDown(object sender, MouseEventArgs e)
        {
            keybd_event((byte)Keys.Right, 0, 0, 0);
            keybd_event((byte)Keys.Right, 0, 0x02, 0);
        }

        private bool HanguelModeCheck()
        {
            IntPtr hwnd = GetForegroundWindow();
            IntPtr hime = ImmGetDefaultIMEWnd(hwnd);
            IntPtr status = SendMessage(hime, WM_IME_CONTROL, new IntPtr(0x5), new IntPtr(0));

            if (status.ToInt64() != 0)
                return true;
            else
                return false;
        }
    }
}
