using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class ProductDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }
        public string CODE { get; set; }
        public string RCOD { get; set; }
        public string TCOD { get; set; }
        public string NAME { get; set; }
        public string OPNM { get; set; }
        public string GCOD { get; set; }
        public string GNAM { get; set; }
        public string MEMO { get; set; }
        public string SEQN { get; set; }
        public double PRICE { get; set; }
        public DateTime DDAT { get; set; }

        public bool ProductSaveL()
        {
            string spName = "CF_PRODUCT_SAVE";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"CODE", @"RCOD", @"TCOD", @"NAME", @"OPNM", @"GCOD", @"GNAM",
                                   @"PRICE", @"MEMO", @"SEQN", @"DDAT" };
            object[] paravalue = { BRAND, STORE, DESK, CODE, RCOD, TCOD, NAME, OPNM, GCOD, GNAM, PRICE, MEMO, SEQN, DDAT };

            return DBConnectionL.DoCommand(spName, paraName, paravalue);
        }

        public bool ProductAllDeleteL()
        {
            string spName = "CF_PRODUCT_ALL_DELETE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }
        public DataTable ProductAllSelect()
        {
            string spName = "CF_PRODUCT_ALL_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }
    }
}
