using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk
{
    public static class OrderListInfo
    {
        public static List<string> ItemType = new List<string>();
        public static List<string> ItemName = new List<string>();
        public static List<string> ItemOpNm = new List<string>();
        public static List<string> ItemGNam = new List<string>();
        public static List<string> Memo1 = new List<string>();
        public static List<string> Memo2 = new List<string>();
        public static List<int> ItemQuan = new List<int>();
        public static List<decimal> ItemAmt = new List<decimal>();
        public static List<decimal> ItemOptAmt1 = new List<decimal>();
        public static List<decimal> ItemOptAmt2 = new List<decimal>();
        public static List<string> ItemTCode = new List<string>();

        public static void OrderListClear()
        {
            ItemType.Clear();
            ItemName.Clear();
            ItemOpNm.Clear();
            ItemGNam.Clear();
            Memo1.Clear();
            Memo2.Clear();
            ItemQuan.Clear();
            ItemAmt.Clear();
            ItemOptAmt1.Clear();
            ItemOptAmt2.Clear();
            ItemTCode.Clear();
        }
    }
}
