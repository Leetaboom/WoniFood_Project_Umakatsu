using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk
{
    internal class SalesCancel
    {
        public List<string> date = new List<string>();
        public List<string> receNo = new List<string>();
        public List<string> cardName = new List<string>();
        public List<string> amt = new List<string>();
        public int totCnt = 0;
        public decimal totAmt = 0;
        public DateTime searchTime;

        public void SaleCancelClear()
        {
            receNo.Clear();
            cardName.Clear();
            amt.Clear();
            totCnt = 0;
            totAmt = 0;

        }
    }
}
