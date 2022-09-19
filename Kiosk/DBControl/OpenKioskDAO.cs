using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class OpenKioskDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }
        public string DATE { get; set; }
        public decimal ENDPAY { get; set; }

        public bool KioskOpenSave()
        {
            string spName = "CF_OPENKIOSK_OPENSAVE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE" };
            string[] paraValue = { BRAND, STORE, DESK, DATE };

            return DBConnection.DoCommand(spName, paraName, paraValue);
        }

        public bool KioskOpenSaveL()
        {
            string spName = "CF_OPENKIOSK_OPENSAVE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE" };
            string[] paraValue = { BRAND, STORE, DESK, DATE };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public bool KioskCloseSave()
        {
            string spName = "CF_OPENKIOSK_CLOSESAVE";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"ENDPAY" };
            object[] paraValue = { BRAND, STORE, DESK, DATE, ENDPAY };

            return DBConnection.DoCommand(spName, paraName, paraValue);
        }

        public bool KioskCloseSaveL()
        {
            string spName = "CF_OPENKIOSK_CLOSESAVE";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"ENDPAY" };
            object[] paraValue = { BRAND, STORE, DESK, DATE, ENDPAY };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public DataTable KioskOpenState()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT * FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_START != '' AND OK_STOP IS NULL");

            return DBConnection.DoSqlSelect(sql.ToString());
        }

        public DataTable KioskOpenStateL()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT * FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_START != '' AND OK_STOP IS NULL");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }
    }
}
