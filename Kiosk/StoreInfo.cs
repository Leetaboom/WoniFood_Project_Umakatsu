using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk
{
    public static class StoreInfo
    {
        //가맹점 정보
        public static string BrnadCode { get; set; }
        public static string StoreCode { get; set; }
        public static string StoreDesk { get; set; }
        public static string StoreName { get; set; }
        public static string StoreArea { get; set; }
        public static string StoreSano { get; set; }
        public static string StoreSang { get; set; }
        public static string StoreCeo { get; set; }
        public static string StoreAdd1 { get; set; }
        public static string StoreAdd2 { get; set; }
        public static string StorePhon { get; set; }
        public static string StoreOpen { get; set; }
        public static bool IsTakeOut { get; set; }

        //가맹점 환경 프로퍼티
        public static bool IsCur { get; set; }
        public static string Kitchen1Prn { get; set; }
        public static string Kitchen2Prn { get; set; }
        public static string ReceiptPrn { get; set; }
        public static string CounterPrn { get; set; }
        public static int Kitchen1Rate { get; set; }
        public static int Kitchen2Rate { get; set; }
        public static int ReceiptRate { get; set; }
        public static int CounterRate { get; set; }
        public static string Kitchen1Type { get; set; }
        public static string Kitchen2Type { get; set; }
        public static string CounterType { get; set; }
        public static string KitchenPrnIP1 { get; set; }
        public static string KitchenPrnIP2 { get; set; }
        public static string CounterPrnIP1 { get; set; }
        public static string CounterPrnIP2 { get; set; } 
        public static int KitchenPrnPort1 { get; set; }
        public static int KitchenPrnPort2 { get; set; }
        public static int CounterPrnPort1 { get; set; }
        public static int CounterPrnPort2 { get; set; }
        public static int MediaVolume { get; set; }
        public static int BWaitTime { get; set; }
        public static int PWaitTiem { get; set; }
        public static bool IsHoldMode { get; set; }
        public static string VanSelect { get; set; }
        public static string Tid { get; set; }
        public static string Port { get; set; }
        public static int MaxTap { get; set; }
        public static string[] ExceptMenuGrp { get; set; }
        public static string[] ExceptMenu { get; set; }
        public static string[] CuttingMenu { get; set; }
    }
}
