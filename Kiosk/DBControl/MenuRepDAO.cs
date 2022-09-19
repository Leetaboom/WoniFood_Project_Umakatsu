using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class MenuRepDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }
        public string GNAM { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string IMGNM { get; set; }
        public string SEQN { get; set; }
        public string MEMO { get; set; }
        public DateTime DDAT { get; set; }
        public DateTime UPDT { get; set; }
        public byte[] IMAGE { get; set; }

        public bool MenuRepSaveL()
        {
            string spName = "CF_MENUREP_SAVE";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"CODE", @"NAME", @"GNAM", @"IMGNM", @"IMAGE", @"MEMO", @"SEQN", @"DDAT"};
            object[] paraValue = { BRAND, STORE, DESK, CODE, NAME, GNAM, IMGNM, IMAGE, MEMO, SEQN, DDAT };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public bool MenuRepAllDeleteL()
        {
            string spName = "CF_MENUREP_ALL_DELETE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public bool MenuRepCodeDelete()
        {
            string spName = "CF_MENUREP_CODE_DELETE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"CODE" };
            string[] paraValue = { BRAND, STORE, DESK, CODE };

            return DBConnection.DoCommand(spName, paraName, paraValue);
        }

        public DataTable MenuRepCodeMake()
        {
            string spName = "CF_MENUREP_AUTONUMBER";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }

        public DataTable MenuRepAllSelect()
        {
            string spName = "CF_MENUREP_ALL_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }

        public DataTable MenuRepCodeSelect()
        {
            string spName = "CF_MENUREP_CODE_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"CODE" };
            string[] paraValue = { BRAND, STORE, DESK, CODE };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }

        public DataTable MenuSeqnAutoNum()
        {
            string spName = "CF_MENUREP_AUTOSEQNUM";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"NAME" };
            string[] paraValue = { BRAND, STORE, DESK, NAME };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }
    }
}
