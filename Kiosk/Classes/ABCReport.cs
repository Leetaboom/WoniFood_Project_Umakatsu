using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk
{
    internal class ABCReport
    {
        public List<string> abc = new List<string>();
        public List<string> itemName = new List<string>();
        public List<string> quan = new List<string>();
        public List<string> amt = new List<string>();
        public List<string> per = new List<string>();
        public DateTime searchTime;

        public void ABCReportClear()
        {
            abc.Clear();
            itemName.Clear();
            quan.Clear();
            amt.Clear();
            per.Clear();
        }
    }
}
