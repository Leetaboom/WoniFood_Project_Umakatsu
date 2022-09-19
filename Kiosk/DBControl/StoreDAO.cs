using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class StoreDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }

        public DataTable StoreCodeSelect()
        {
            string spName = "MS_STORE_CODE_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }
        
        public bool IsStoreLoginCheck(string pass)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT * FROM MS_STORE WHERE MS_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND MS_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND MS_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND MS_PASS = '");
            sql.Append(pass);
            sql.Append("'");

            if (DBConnection.DoSqlSelect(sql.ToString()).Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
