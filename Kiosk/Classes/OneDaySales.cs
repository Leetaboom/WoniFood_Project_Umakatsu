using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk
{
    internal class OneDaySales
    {
        public List<string> date = new List<string>();
        public List<string> cardCnt = new List<string>();
        public List<string> cardAmt = new List<string>();
        public List<string> totCnt = new List<string>();
        public List<string> totAmt = new List<string>();
        public DateTime searchTime;

        public void OneDaySalesClear()
        {
            date.Clear();
            cardCnt.Clear();
            cardAmt.Clear();
            totCnt.Clear();
            totAmt.Clear();
        }
    }
}
