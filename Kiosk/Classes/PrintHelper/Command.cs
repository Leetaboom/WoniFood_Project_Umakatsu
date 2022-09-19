using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk.PrintHelper
{
    public class Command
    {
        public static string NULL = Convert.ToString((char)0);
        public static string SOH = Convert.ToString((char)1);
        public static string STX = Convert.ToString((char)2);
        public static string ETX = Convert.ToString((char)3);
        public static string EOT = Convert.ToString((char)4);
        public static string ENQ = Convert.ToString((char)5);
        public static string ACK = Convert.ToString((char)6);
        public static string BEL = Convert.ToString((char)7);
        public static string BS = Convert.ToString((char)8);
        public static string TAB = Convert.ToString((char)9);
        public static string VT = Convert.ToString((char)11);
        public static string FF = Convert.ToString((char)12);
        public static string CR = Convert.ToString((char)13);
        public static string SO = Convert.ToString((char)14);
        public static string SI = Convert.ToString((char)15);
        public static string DLE = Convert.ToString((char)16);
        public static string DC1 = Convert.ToString((char)17);
        public static string DC2 = Convert.ToString((char)18);
        public static string DC3 = Convert.ToString((char)19);
        public static string DC4 = Convert.ToString((char)20);
        public static string MAK = Convert.ToString((char)21);
        public static string SYN = Convert.ToString((char)22);
        public static string ETB = Convert.ToString((char)23);
        public static string CAN = Convert.ToString((char)24);
        public static string EM = Convert.ToString((char)25);
        public static string SUB = Convert.ToString((char)26);
        public static string ESC = Convert.ToString((char)27);
        public static string FS = Convert.ToString((char)28);
        public static string GS = Convert.ToString((char)29);
        public static string RS = Convert.ToString((char)30);
        public static string US = Convert.ToString((char)31);
        public static string Space = Convert.ToString((char)32);

        #region 기능 커맨드 모음
        //프린터 초기화
        public static string InitializePrinter = ESC + "@";

        //아스크 LF
        public static string NewLine = Convert.ToString((char)10);

        //라인피드
        public static string LineFeed(int val)
        {
            return ESC + "d" + DecimalToCharString(val);
        }

        //볼드 On
        public static string BoldOn = ESC + "E" + DecimalToCharString(1);

        //볼드 Off
        public static string BoldOff = ESC + "E" + DecimalToCharString(0);

        //언더라인 On
        public static string UnderLineOn = ESC + "-" + DecimalToCharString(1);

        //언더라인 Off
        public static string UnderLineOff = ESC + "-" + DecimalToCharString(0);

        //흑백반전 On
        public static string ReverseOn = GS + "B" + DecimalToCharString(1);

        //흑백반전 Off
        public static string ReverseOff = GS + "B" + DecimalToCharString(0);

        //좌측정렬
        public static string AlignLeft = ESC + "a" + DecimalToCharString(0);

        //가운데정렬
        public static string AlignCenter = ESC + "a" + DecimalToCharString(1);

        //오른쪽정렬
        public static string AlignRight = ESC + "a" + DecimalToCharString(2);

        //상하반전 인쇄모드
        public static string TopAndBottom = ESC + "{" + DecimalToCharString(1);

        //상하반전 인쇄모드
        public static string TopAndBottomOff = ESC + "{" + DecimalToCharString(0);

        //커트
        public static string Cut = GS + "V" + DecimalToCharString(2);

        #endregion 기능 커맨드 모음 끝


        //<summary>
        ///Decimal을 캐릭터로 변환 후 스트링을 반환 합니다.
        /// </summary>
        /// <param name="val">커맨드 숫자</param>
        /// <returns>변환된 문자열</returns>
        /// 
        public static string DecimalToCharString(decimal val)
        {
            string result = string.Empty;

            try
            {
                result = Convert.ToString((char)val);
            }
            catch { }

            return result;
        }

        ///<summary>
        ///FONT 명령어의 글자사이즈 속성을 변환 합니다.
        /// </summary>
        /// <param name="width">가로</param>
        /// <param name="height">세로</param>
        /// <returns>가로 X 세로</returns>
        /// 
        public static string ConvertFontSize(int width, int height)
        {
            string result = "0";
            int _w, _h;

            switch (width)
            {
                case 0:
                    _w = 0;
                    break;

                case 1:
                    _w = 16;
                    break;

                case 2:
                    _w = 32;
                    break;

                case 3:
                    _w = 48;
                    break;

                case 4:
                    _w = 64;
                    break;

                case 5:
                    _w = 80;
                    break;

                case 6:
                    _w = 96;
                    break;

                case 7:
                    _w = 112;
                    break;

                default:
                    _w = 0;
                    break;
            }

            switch (height)
            {
                case 0:
                    _h = 0;
                    break;

                case 1:
                    _h = 1;
                    break;

                case 2:
                    _h = 2;
                    break;

                case 3:
                    _h = 3;
                    break;

                case 4:
                    _h = 4;
                    break;

                case 5:
                    _h = 5;
                    break;

                case 6:
                    _h = 6;
                    break;

                case 7:
                    _h = 7;
                    break;

                default:
                    _h = 0;
                    break;
            }

            int sum = _w + _h;

            result = GS + "!" + DecimalToCharString(sum);

            return result;
        }
    }
}
