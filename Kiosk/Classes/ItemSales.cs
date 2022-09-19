using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk
{
    internal class ItemSales
    {
        public List<string> itemName = new List<string>();
        public List<string> quan = new List<string>();
        public List<string> amt = new List<string>();
        public int totCnt = 0;
        public decimal totAmt = 0;
        public DateTime searchTime;

        public void ItemSalesClear()
        {
            itemName.Clear();
            quan.Clear();
            amt.Clear();
            totCnt = 0;
            totAmt = 0;
        }
    }
}
