using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class MenuListCfgDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }
        public string GNAM { get; set; }
        public string TYPE { get; set; }
        public string TNAM { get; set; }
        public string TCOD { get; set; }
        public string NAME { get; set; }
        public string PRICE { get; set; }
        public string SOLDYN { get; set; }
        public int PAGE { get; set; }
        public int COLROW { get; set; }
        public DateTime DDAT { get; set; }

        public DataTable MenuListCfgAllSelect()
        {
            string spName = "CF_MENULISTCFG_ALLSELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }

        public DataTable MenuListCfgSelect()
        {
            string spName = "CF_MENULISTCFG_SELECT";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"GNAM", @"PAGE", @"COLROW" };
            object[] paraValue = { BRAND, STORE, DESK, GNAM, PAGE, COLROW };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }

        public DataTable MenuListCfgSelectL()
        {
            string spName = "CF_MENULISTCFG_SELECT";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"GNAM", @"PAGE", @"COLROW" };
            object[] paraValue = { BRAND, STORE, DESK, GNAM, PAGE, COLROW };

            return DBConnectionL.DoSelect(spName, paraName, paraValue);
        }

        public DataTable SetMenuListCfgSelectL()
        {
            string spName = "CF_SETMENULISTCFG_SELECT";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"PAGE", @"COLROW" };
            object[] paraValue = { BRAND, STORE, DESK, PAGE, COLROW };

            return DBConnectionL.DoSelect(spName, paraName, paraValue);
        }

        public DataTable MenuListCfgImageSelectL(string name, string tcode)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT MR_IMAGE FROM CF_MENUREP WHERE MR_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND MR_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND MR_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND MR_NAME = '");
            sql.Append(name);
            sql.Append("' AND MR_CODE = '");
            sql.Append(tcode);
            sql.Append("'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuListCfgMemoSelectL(string name, string tcode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT MR_MEMO FROM CF_MENUREP WHERE MR_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND MR_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND MR_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND MR_NAME = '");
            sql.Append(name);
            sql.Append("' AND MR_CODE = '");
            sql.Append(tcode);
            sql.Append("'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuListCfgAmtSelectL(string tcode)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT PT_PRICE FROM CF_PRODUCT WHERE PT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND PT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND PT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND PT_TCOD = '");
            sql.Append(tcode);
            sql.Append("'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuListCfgAmtLisSelectL(string grpName, string tcode)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT PT_OPNM, PT_PRICE FROM CF_PRODUCT WHERE PT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND PT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND PT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND PT_NAME = '");
            sql.Append(grpName);
            sql.Append("' AND PT_TCOD = '");
            sql.Append(tcode);
            sql.Append("' AND PT_RCOD = 'Y'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuListCfgTotalPageSelectL(string grpName)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT * FROM CF_MENULISTCFG WHERE MT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND MT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND MT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND MT_TNAM != '' AND MT_GNAM = '");
            sql.Append(grpName);
            sql.Append("'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuListCfgType1_SelectL(string name, string tcod)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT PT_OPNM, PT_PRICE FROM CF_PRODUCT WHERE PT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND PT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND PT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND PT_NAME = '");
            sql.Append(name);
            sql.Append("' AND PT_TCOD = '");
            sql.Append(tcod);
            sql.Append("' AND PT_RCOD = 'Y'");
            sql.Append(" ORDER BY PT_PRICE ASC");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuListCfgType2_SelectL(string name, string tcod)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT PT_OPNM, PT_PRICE FROM CF_PRODUCT WHERE PT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND PT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND PT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND PT_NAME = '");
            sql.Append(name);
            sql.Append("' AND PT_TCOD = '");
            sql.Append(tcod);
            sql.Append("' AND PT_RCOD = 'Y'");
            sql.Append(" ORDER BY PT_PRICE DESC");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuListCfgType3_SelectL(string name, string tcod)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT PT_OPNM, PT_PRICE FROM CF_PRODUCT WHERE PT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND PT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND PT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND PT_NAME = '");
            sql.Append(name);
            sql.Append("' AND PT_TCOD = '");
            sql.Append(tcod);
            sql.Append("' AND PT_RCOD = 'Y'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuSalesListSelectL()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT * MT_PAGE, MT_COLROW, MT_GNAM, MT_TNAM FROM CF_MENULISTCFG WHERE MT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND MT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND MT_TNAM != ''");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable OptionItemTotalPageSelectL()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT * FROM CF_SETMENULISTCFG WHERE ST_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND ST_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND ST_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND ST_NAME != ''");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable MenuListSelectL()
        {
            StringBuilder sql = new StringBuilder();

            sql.Clear();

            sql.Append("SELECT MT_PAGE, MT_COLROW, MT_GNAM, MT_TNAM, MT_SOLDYN FROM CF_MENULISTCFG WHERE MT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND MT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND MT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND MT_TNAM != ''");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public bool MenuListCfgSoldOutUpdateL(string soldYn, string grpName, string tNam)
        {
            StringBuilder sql = new StringBuilder();
            sql.Clear();

            sql.Append("UPDATE CF_MENULISTCFG SET MT_SOLDYN = '");
            sql.Append(soldYn);
            sql.Append("' WHERE MT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND MT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND MT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND MT_GNAM = '");
            sql.Append(grpName);
            sql.Append("' AND MT_TNAM = '");
            sql.Append(tNam);
            sql.Append("'");

            return DBConnectionL.DoSqlCommand(sql.ToString());
        }

        public bool MenuListCfgSoldOutUpdate(string soldYn, string grpName, string tNam)
        {
            StringBuilder sql = new StringBuilder();
            sql.Clear();

            sql.Append("UPDATE CF_MENULISTCFG SET MT_SOLDYN = '");
            sql.Append(soldYn);
            sql.Append("' WHERE MT_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND MT_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND MT_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND MT_GNAM = '");
            sql.Append(grpName);
            sql.Append("' AND MT_TNAM = '");
            sql.Append(tNam);
            sql.Append("'");

            return DBConnection.DoSqlCommand(sql.ToString());
        }

        public bool MenuListCfgDeleteL()
        {
            string spName = "CF_MENULISTCFG_DELETE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public bool MenuListCfgSaveL()
        {
            string spName = "CF_MENULISTCFG_SAVE";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"GNAM", @"TYPE", @"PAGE", @"COLROW", @"TNAM", @"TCOD", @"DDAT", @"SOLDYN" };
            object[] paraValue = { BRAND, STORE, DESK, GNAM, TYPE, PAGE, COLROW, TNAM, TCOD, DDAT, SOLDYN };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public bool SetMenuListCfgSaveL()
        {
            string spName = "CF_SETMENULISTCFG_SAVE";
            object[] paraName = { @"BRAND", @"STORE", @"DESK", @"PAGE", @"COLROW", @"NAME", @"PRICE", @"TCOD", @"DDAT" };
            object[] paraValue = { BRAND, STORE, DESK, PAGE, COLROW, NAME, PRICE, TCOD, DDAT };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public bool SetMenuListCfgAllDeleteL()
        {
            string spName = "CF_SETMENULISTCFG_DELETE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public DataTable SetMenuListCfgAllSelect()
        {
            string spName = "CF_SETMENULISTCFG_ALL_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK" };
            string[] paraValue = { BRAND, STORE, DESK };

            return DBConnection.DoSelect(spName, paraName, paraValue);
        }
    }
}
