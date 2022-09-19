using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KioskUpdater.Controler
{
    public class TransferingInfo
    {
        /// <summary>
        /// 전송할 데이터의 전체 크기
        /// </summary>
        public long TotalLength { get; set; }

        /// <summary>
        /// 파일 단위로 전송된 데이터의 크기 
        /// </summary>
        public long TransferedLength { get; set; }

        /// <summary>
        /// 파일 단위로 전송 중인 데이터의 크기
        /// </summary>
        public long TransferingLength { get; set; }

        /// <summary>
        /// 전송된 데이터의 전체 크기
        /// </summary>
        public long TotalTransferedLength { get { return TransferedLength + TransferingLength; } }

        /// <summary>
        /// 전송할 파일의 전체 갯수
        /// </summary>
        public int TotalFileCount { get; set; }

        /// <summary>
        /// 전송된 파일의 갯수
        /// </summary>
        public int TransferedFileCount { get; set; }

        /// <summary>
        /// 전송된 데이터의 퍼센트
        /// </summary>
        public int LengthPercent
        {
            get { return (int)(TotalTransferedLength * 100.0 / TotalLength); }
        }

        /// <summary>
        /// 전송된 파일 갯수의 퍼센트
        /// </summary>
        public int FileCountPercent
        {
            get { return (int)(TransferedFileCount * 100.0 / TotalFileCount); }
        }

        /// <summary>
        /// 현재 전송 중인 파일의 로컬 경로
        /// </summary>
        public string CurrentFilePath { get; set; }

        /// <summary>
        /// 남은 전송 시간 (초)
        /// </summary>
        public double RemainingSeconds { get; set; }
    }
}
