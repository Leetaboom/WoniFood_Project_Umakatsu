using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Kiosk.PrintHelper
{
    class ImagePrint
    {
        public byte[] GetLogo(string logoPath, int printWidth)
        {
            List<byte> byteList = new List<byte>();

            if (!File.Exists(logoPath))
                return null;

            BitmapData data = GetBitmapData(logoPath, printWidth);
            BitArray dots = data.Dots;

            byte[] width = BitConverter.GetBytes(data.Width);

            int offset = 0;

            byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            byteList.Add(Convert.ToByte('3'));
            byteList.Add((byte)24);

            while (offset < data.Height)
            {
                byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
                byteList.Add(Convert.ToByte('*'));
                byteList.Add((byte)33);
                byteList.Add(width[0]);
                byteList.Add(width[1]);

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;

                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;
                            int i = (y * data.Width) + x;

                            bool v = false;

                            if (i < dots.Length)
                                v = dots[i];

                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }
                        byteList.Add(slice);
                    }
                }
                offset += 24;
                byteList.Add(Convert.ToByte(0x0A));
            }

            byteList.Add(Convert.ToByte(0x1B));
            byteList.Add(Convert.ToByte('3'));
            byteList.Add((byte)30);

            return byteList.ToArray();
        }


        private BitmapData GetBitmapData(string bmpFileName, int width)
        {
            using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
            {
                var threshold = 127;
                var index = 0;
                double multiplier = width;
                double scale = (double)(multiplier / (double)bitmap.Width);
                int xheight = (int)(bitmap.Height * scale);
                int xwidth = (int)(bitmap.Width * scale);

                var dimensions = xwidth * xheight;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < xheight; y++)
                {
                    for (var x = 0; x < xwidth; x++)
                    {
                        var _x = (int)(x / scale);
                        var _y = (int)(y / scale);
                        var color = bitmap.GetPixel(_x, _y);
                        var luminace = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminace < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                {
                    Dots = dots,
                    Height = (int)(bitmap.Height * scale),
                    Width = (int)(bitmap.Width * scale)
                };
            }
        }

        private class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }
    }
}
