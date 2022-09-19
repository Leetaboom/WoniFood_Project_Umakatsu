using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class ReceNoDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DATE { get; set; }

        public bool ReceNO_Make()
        {
            string spName = "MS_RECENO_MAKE";
            string[] paraName = { @"BRAND", @"STORE", @"DATE" };
            string[] paraValue = { BRAND, STORE, DATE };

            return DBConnection.DoCommand(spName, paraName, paraValue);
        }

        public DataTable GetReceNo()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SB_SEQN FROM MS_RECENO WHERE SB_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SB_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SB_DATE = '");
            sql.Append(DATE);
            sql.Append("'");

            return DBConnection.DoSqlSelect(sql.ToString());
        }
    }
}
