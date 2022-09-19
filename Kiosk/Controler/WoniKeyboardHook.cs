using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Kiosk.Controler
{
    class WoniKeyboardHook
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        public delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);
        public KeyboardHookProc _keyboardHookProc;

        public struct KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;

        public List<Keys> HookedKeys = new List<Keys>();
        IntPtr hhok = IntPtr.Zero;

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        public WoniKeyboardHook()
        {
            _keyboardHookProc = new KeyboardHookProc(HookProc);

            Hook();
        }

        ~WoniKeyboardHook()
        {
            Unhook();
        }

        public void Hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhok = SetWindowsHookEx(WH_KEYBOARD_LL, _keyboardHookProc, hInstance, 0);
        }

        public void Unhook()
        {
            UnhookWindowsHookEx(hhok);
        }

        public int HookProc(int code, int wParam, ref KeyboardHookStruct lParam)
        {
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;

                if (HookedKeys.Contains(key))
                {
                    KeyEventArgs kea = new KeyEventArgs(key);

                    if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
                    {
                        KeyDown(this, kea);
                    }
                    else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
                    {
                        KeyUp(this, kea);
                    }

                    if (kea.Handled)
                        return 1;
                }
            }

            return CallNextHookEx(hhok, code, wParam, ref lParam);
        }
    }
}
