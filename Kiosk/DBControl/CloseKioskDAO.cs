using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk.DBControl
{
    class CloseKioskDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }
        public string IDXCD { get; set; }
        public string IDXNM { get; set; }
        public string DATE { get; set; }
        public int CNT { get; set; }
        public decimal AMT { get; set; }

        public bool ColoseKioskSave()
        {
            try
            {
                string spName = "CF_CLOSEKIOSK_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"IDXCD", @"IDXNM", @"CNT", @"AMT", @"DATE" };
                object[] paraValue = { BRAND, STORE, DESK, IDXCD, IDXNM, CNT, AMT, DATE };

                return DBConnection.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + "외부");

                return false;
            }
        }


        public bool ColoseKioskSaveL()
        {
            try
            {
                string spName = "CF_CLOSEKIOSK_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"IDXCD", @"IDXNM", @"CNT", @"AMT", @"DATE" };
                object[] paraValue = { BRAND, STORE, DESK, IDXCD, IDXNM, CNT, AMT, DATE };

                return DBConnectionL.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + "로컬");

                return false;
            }
        }
    }
}
