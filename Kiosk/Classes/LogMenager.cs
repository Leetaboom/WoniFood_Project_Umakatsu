using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Kiosk
{
    static class LogMenager
    {
        public static void LogWriter(string strLogPrefix, string strMsg)
        {
            string m_strLogPrefix = strLogPrefix;//@"\Log\RecvData";
            string m_strLogExt = @".LOG";
            DateTime dtNow = DateTime.Now;

            string strDate = dtNow.ToString("yyyyMMdd");
            string strPath = string.Format("{0}{1}{2}", m_strLogPrefix, strDate, m_strLogExt);
            string strDir = Path.GetDirectoryName(strPath);
            DirectoryInfo diDir = new DirectoryInfo(strDir);

            if (!diDir.Exists)
            {
                diDir.Create();
                diDir = new DirectoryInfo(strDir);
            }

            if (diDir.Exists)
            {
                StreamWriter swStream = File.AppendText(strPath);

                try
                {
                    string strLog = string.Format("{0}: {1}", dtNow.ToString("yyyyMMddhhmmss"), strMsg);
                    swStream.WriteLine(strLog);
                    swStream.Close();
                }
                catch
                {
                    swStream.Close();
                }
            }
        }
    }
}
