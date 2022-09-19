using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class MenuGrpCfgDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }
        public string SEQN { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public DateTime DDAT { get; set; }
    
        public bool MenuGrpCfgSave()
        {
            string spName = "CF_MENUGRPCFG_SAVE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"SEQN", @"NAME", @"TYPE"};
            string[] paraValue = { BRAND, STORE, DESK, SEQN, NAME, TYPE };

            return DBConnection.DoCommand(spName, paraName, paraValue);
        }

        public bool MenuGrpCfgSaveL()
        {
            try
            {
                string spName = "CF_MENUGRPCFG_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"SEQN", @"NAME", @"TYPE", @"DDAT" };
                object[] paraValue = { BRAND, STORE, DESK, SEQN, NAME, TYPE, DDAT };

                return DBConnectionL.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);

                return false;
            }
        }

        public bool MenuGrpCfgDelete()
        {
            string spName = "CF_MENUGRPCFG_DELETE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnection.DoCommand(spName, paraName, paraValue);
        }

        public bool MenuGrpCfgDeleteL()
        {
            string spName = "CF_MENUGRPCFG_DELETE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public DataTable MenuGrpCfgSelect()
        {
            string spName = "CF_MENUGRPCFG_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }

        public DataTable MenuGrpCfgSelectL()
        {
            string spName = "CF_MENUGRPCFG_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnectionL.DoSelect(spName, paraName, paraValue);
        }
    }
}
