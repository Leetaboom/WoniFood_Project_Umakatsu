using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Management;
using System.Media;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Kiosk
{
    static class UtilHelper
    {
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //[DllImport("user32.dll")]
        //private static extern uint SendInput

        public static SoundPlayer spWav = new SoundPlayer(@"System\wav\TicTic.wav");
        public static SoundPlayer spKWav = new SoundPlayer(@"System\wav\KeyBoard.wav");
        public static SoundPlayer spProgressWav = new SoundPlayer(@"System\wav\progress.wav");
        public static SoundPlayer spSamWav = new SoundPlayer(@"System\wav\samsung.wav");
        public static SoundPlayer spSamEndWav = new SoundPlayer(@"System\wav\samsung_end.wav");
        public static SoundPlayer spCardWav = new SoundPlayer(@"System\wav\card.wav");
        public static SoundPlayer spCardEndWav = new SoundPlayer(@"System\wav\card_end.wav");
        public static SoundPlayer spHoldWav = new SoundPlayer(@"System\wav\hold.wav");


        public static string Root
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        public static Bitmap ScreenCapture(int inBitmapWidth, int inBitmapHeight, Point ptSource)
        {
            Bitmap bitmap = new Bitmap(inBitmapWidth, inBitmapHeight);
            Graphics g = Graphics.FromImage(bitmap);

            g.CopyFromScreen(ptSource, new Point(0, 0), new Size(inBitmapWidth, inBitmapHeight));

            bitmap = ChangeOpacity(bitmap, 1f);

            return bitmap;
        }

        public static Bitmap ChangeOpacity(Image img, float opacityValue)
        {
            float[][] colorMarixElements =
            {
                new float[]{0.3f, 0.0f, 0.0f, 0.0f, 0.0f},
                new float[]{0.0f, 0.3f, 0.0f, 0.0f, 0.0f},
                new float[]{0.0f, 0.0f, 0.3f, 0.0f, 0.0f},
                new float[]{0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                new float[]{0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            };

            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix(colorMarixElements);
            colormatrix.Matrix33 = opacityValue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();

            return bmp;
        }

        public static string HyphepMake(string chr, int count)
        {
            StringBuilder rtnVal = new StringBuilder();
            rtnVal.Clear();

            for (int i = 0; i < count; i++)
                rtnVal.Append(chr);

            return rtnVal.ToString();
                
        }

        public static string PadingLeftBySpace(string val, int count)
        {
            string target = val;
            string tmp = Regex.Replace(target, @"\D", "");
            string rtnVal = string.Empty;
            int nCount = tmp.Length;

            if (nCount != 0)
            {
                if (target.Contains('('))
                    nCount++;

                if (target.Contains('-'))
                    nCount++;

                if (target.Contains(' '))
                    nCount++;

                for (int i = 0; i < nCount; i++)
                    count++;
            }
            int nLength = count - val.Length;

            rtnVal = val.PadRight(nLength, ' ');

            return rtnVal;
        }

        public static string PadingRightBySpace(string val, int count)
        {
            string target = val;
            string tmp = Regex.Replace(target, @"\D", "");
            string rtnVal = string.Empty;
            int nCount = tmp.Length;

            if (nCount != 0)
            {
                if (target.Contains('('))
                    nCount++;

                if (target.Contains('-'))
                    nCount++;

                if (target.Contains(' '))
                    nCount++;

                for (int i = 0; i < nCount; i++)
                    count++;
            }
            int nLength = count - val.Length;

            rtnVal = val.PadLeft(nLength, ' ');

            return rtnVal;
        }

        /*public static string HDDDiskSerial()
        {
            string volumn = string.Empty;
            string serial = string.Empty;
            try
            {
                ManagementObjectSearcher MSearch = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_LogicalDisk WHERE Name = 'C:'");

                foreach (ManagementObject hard in MSearch.Get())
                    volumn = hard["VolumeSerialNumber"].ToString();

                ManagementObjectSearcher MSearch2 = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject ser in MSearch2.Get())
                    serial = ser["SerialNumber"].ToString();

                return volumn + serial;
            }
            catch
            {
                return "";
            }
        }*/

        static public void Delay(int ms)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime dateTimeAdd = dateTimeNow.Add(duration);

            while (dateTimeAdd >= dateTimeNow)
            {
                System.Windows.Forms.Application.DoEvents();
                dateTimeNow = DateTime.Now;
            }
        }

        /*public static List<string> GetConnectComDevice()
        {
            List<string> lstPorts = new List<string>();
            RegistryKey rkRoot = Registry.LocalMachine.OpenSubKey("HARDWARE");
            RegistryKey rkSubKey = rkRoot.OpenSubKey("DEVICEMAP\\SERIALCOMM");

            //if (rkSubKey == null || rkSubKey.ValueCount == 0)
            //{
            //    lstPorts.Add("none");
            //}
            //else
            {
                string[] tmpCom = rkSubKey.GetValueNames();
                //lstPorts.Add("사용안함");
                for (int i = 0; i < rkSubKey.ValueCount; i++)
                {
                    lstPorts.Add((rkSubKey.GetValue(tmpCom[i]).ToString()));
                }
                lstPorts.Sort();
            }
            return lstPorts;
        }
        */

        public static string SizeCapacity(int size)
        {
            string retSize;

            if (size >= 1073741824)
                retSize = Math.Round(size / 1073741824d, 2) + "GB";
            else if (size >= 1048576)
                retSize = Math.Round(size / 1048576d, 2) + "MB";
            else if (size >= 1024)
                retSize = Math.Round(size / 1024d, 2) + "KB";
            else
                retSize = size + "Byte";

            return retSize;
        }

        ///<summary>
        ///임의의 값을 정수형으로 반환
        /// </summary>
        /// <param name="Int">문자열 숫자</param>
        /// <returns>정수</returns>
        /// 
        [System.ComponentModel.Description("문자열로 된 숫자를 정수형으로 반환하는 함수")]
        [System.ComponentModel.Category("Utility")]
        public static int ToInteger(this object Int)
        {
            if (Int == null || Int == DBNull.Value) return 0;

            int nNum = 0;

            try
            {
                string reStr = Regex.Replace(Int.ToString(), "[^-0-9]", "");
                nNum = int.Parse(reStr);
            }
            catch (Exception) { }

            return nNum;
        }

        ///<summary>
        ///임의의 값을 Boolean으로 반환
        ///</summary>
        ///<param name="Boolean">문자열 Boolean</param>
        ///<returns>true/false</returns>
        ///
        [System.ComponentModel.Description("실수형이 아닌 값을 Boolean으로 반환하는 함수")]
        [System.ComponentModel.Category("Utility")]
        public static Boolean ToBoolean(this object Boolean)
        {
            try
            {
                string st = Boolean.ToString();

                if (st.ToLower() == "true" | st == "1" | st.ToLower() == "t" | st.ToLower() == "y")
                    return true;
                else if (st.ToLower() == "false" | st == "0" | st.ToLower() == "f" | st.ToLower() == "n")
                    return false;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///<summary>
        ///임의의 값을 Float으로 반환
        ///</summary>
        ///<param name="Float">문자열 Float</param>
        ///<returns>Float</returns>
        ///
        [System.ComponentModel.Description("실수형이 아닌 값을 Float으로 반환하는 함수")]
        [System.ComponentModel.Category("Utility")]
        public static float ToFloat(this object Float)
        {
            if (Float == null || Float == DBNull.Value) return 0;

            float nNum = 0;

            try
            {
                nNum = float.Parse(Regex.Replace(Float.ToString(), "[^-0-9.]", ""));
            }
            catch (Exception) { }

            return nNum;
        }

        ///<summary>
        ///임의의 값을 Double으로 반환
        ///</summary>
        ///<param name="Double">문자열 Double</param>
        ///<returns>Float</returns>
        ///
        [System.ComponentModel.Description("실수형이 아닌 값을 Double형으로 반환하는 함수")]
        [System.ComponentModel.Category("Utility")]
        public static double ToDouble(this object Double)
        {
            if (Double == null || Double == DBNull.Value) return 0;

            double nNum = 0;

            try
            {
                nNum = double.Parse(Regex.Replace(Double.ToString(), "[^-0-9.]", ""));
            }
            catch (Exception) { }

            return nNum;
        }

        ///<summary>
        ///임의의 값을 Decimal으로 반환
        ///</summary>
        ///<param name="Decimal">문자열 Decimal</param>
        ///<returns>Decimal</returns>
        ///
        [System.ComponentModel.Description("실수형이 아닌 값을 Decimal으로 반환하는 함수")]
        [System.ComponentModel.Category("Utility")]
        public static decimal ToDecimal(this object Decimal)
        {
            if (Decimal == null || Decimal == DBNull.Value) return 0;

            decimal nNum = 0;

            try
            {
                nNum = decimal.Parse(Regex.Replace(Decimal.ToString(), "[^-0-9.]", ""));
            }
            catch (Exception) { }

            return nNum;
        }

        ///<summary>
        ///금액관련 문자열값에 소수점을 표시해서 반환
        ///</summary>
        ///<param name = "momey"></param>
        ///<returns>소수점을 표시해서 반환</returns>
        ///
        [System.ComponentModel.Description("숫자값을 소수점을 표시해서 반환해 주는 함수")]
        [System.ComponentModel.Category("Utility")]
        public static string ToDecimalMoney(this string Money)
        {
            string val = null;
            Money = Money.Replace(",", "");

            try
            {
                val = string.Format("{0:N2}", Convert.ToDouble(Money));
            }
            catch
            {
                val = Money;
            }

            return val;
        }

        public static string SubGap(string str)
        {
            int start = 0;
            int num = 0;
            string tmp = str;

            while (tmp.IndexOf(",") > 0)
            {
                num = tmp.IndexOf(",");
                string tmp1 = tmp.Substring(0, num);
                start = num + 1;
                tmp1 += tmp.Substring(num + 1);
                tmp = tmp1;
            }

            return tmp;
        }

        public static bool IsConnectedToInternet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }

        public static void SetIniValue(string section, string key, string value, string iniPath)
        {
            WritePrivateProfileString(section, key, value, iniPath);
        }

        public static string GetiniValue(string section, string key, string iniPath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, iniPath);

            return temp.ToString();
        }

        public static List<string> GetConnectComDevice()
        {
            List<string> lstPorts = new List<string>();
            RegistryKey rkRoot = Registry.LocalMachine.OpenSubKey("HARDWARE");
            RegistryKey rkSubKey = rkRoot.OpenSubKey("DEVICEMAP\\SERIALCOMM");

            if (rkSubKey == null || rkSubKey.ValueCount == 0)
            {
                lstPorts.Add("none");
            }
            else
            {
                string[] tmpCom = rkSubKey.GetValueNames();
                lstPorts.Add("사용안함");
                for (int i = 0; i < rkSubKey.ValueCount; i++)
                {
                    lstPorts.Add((rkSubKey.GetValue(tmpCom[i]).ToString()));
                }
                lstPorts.Sort();
            }
            return lstPorts;
        }
    }
}
