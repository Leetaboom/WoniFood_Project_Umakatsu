using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KioskUpdater.Controler
{
    public class RemoteFile
    {
        /// <summary>
        /// URI
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 다운로드될 로컬 경로
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// 마지막으로 수정된 날짜
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// 파일 길이
        /// </summary>
        public long ContentLength { get; set; }
    }
}
