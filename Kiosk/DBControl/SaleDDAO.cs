using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class SaleDDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }
        public string DATE { get; set; }
        public string TIME { get; set; }
        public string RECENO { get; set; }
        public string SEQN { get; set; }
        public string GNAM { get; set; }
        public string PNAM { get; set; }
        public string OPNM { get; set; }
        public string REMARK { get; set; }
        public string GUBN { get; set; }
        public string TYPE { get; set;}
        public string TURN { get; set; }
        public string APPDATE { get; set; }
        public string SELECTDATE { get; set; }
        public int CONT { get; set; }
        public decimal UNIT { get; set; }
        public decimal PAMT { get; set; }
        public decimal DC { get; set; }

        public bool SaleDSave()
        {
            try
            {
                string spName = "MS_SALED_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"TIME", @"RECENO", @"GNAM", @"PNAM", @"OPNM", @"CONT", @"UNIT", @"PAMT",
                                  @"DC", @"GUBN", @"TYPE", @"REMARK", @"TURN" };
                object[] paraValue = { BRAND, STORE, DESK, DATE, TIME, RECENO, GNAM, PNAM, OPNM, CONT, UNIT, PAMT, DC, GUBN, TYPE, REMARK, TURN };

                return DBConnection.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"Log\text\error", "[SalesD Save]:" + e.Message);
                return false;
            }
        }

        public bool SaleDSaveL()
        {
            try
            {
                string spName = "MS_SALED_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"TIME", @"RECENO", @"GNAM", @"PNAM", @"OPNM", @"CONT", @"UNIT", @"PAMT",
                                  @"DC", @"GUBN", @"TYPE", @"REMARK", @"TURN" };
                object[] paraValue = { BRAND, STORE, DESK, DATE, TIME, RECENO, GNAM, PNAM, OPNM, CONT, UNIT, PAMT, DC, GUBN, TYPE, REMARK, TURN };

                return DBConnectionL.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"Log\text\error", "[SalesD SaveL]:" + e.Message);
                return false;
            }
        }
        
        public bool SaleDUpdate()
        {
            string spName = "MS_SALED_UPDATE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"TIME", @"RECENO", @"GUBN" };
            string[] paraValue = { BRAND, STORE, DESK, DATE, TIME, RECENO, GUBN };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public bool SalesNumMaker()
        {
            try
            {
                string spName = "MS_SALESNUM_MAKE";
                string[] paraName = { @"BRAND", @"STORE", @"DATE" };
                string[] paraValue = { BRAND, STORE, DATE };

                return DBConnection.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"Log\text\error", e.Message);
                return false;
            }
        }

        public bool Saled_Update()
        {
            string spName = "MS_SALED_UPDATE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"RECENO", @"GUBN" };
            string[] paraValue = { BRAND, STORE, DESK, DATE, RECENO, GUBN };

            return DBConnection.DoCommand(spName, paraName, paraValue);
        }

        public bool Saled_UpdateL()
        {
            string spName = "MS_SALED_UPDATE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"RECENO", @"GUBN" };
            string[] paraValue = { BRAND, STORE, DESK, DATE, RECENO, GUBN };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        public bool TurnIndexMaker()
        {
            try
            {
                string spName = "MS_TURNINDEX_MAKE";
                string[] paraName = { @"BRAND", @"STORE", @"DATE" };
                string[] paraValue = { BRAND, STORE, DATE };

                return DBConnection.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"\Log\text\error", e.Message);

                return false;
            }
        }

        public string GetSalesNum()
        {
            string receno = string.Empty;

            StringBuilder sql = new StringBuilder();
            sql.Clear();

            sql.Append("SELECT SN_NUMBER FROM MS_SALESNUM WHERE SN_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SN_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SN_DATE = '");
            sql.Append(DATE);
            sql.Append("'");

            using (DataTable dt = DBConnection.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                            receno = dtr[0].ToString();
                    }
                }
            }
            return receno;
        }

        public string GetTurnNum()
        {
            string trunNum = string.Empty;

            StringBuilder sql = new StringBuilder();
            sql.Clear();

            sql.Append("SELECT TN_NUMBER FROM MS_TURNINDEX WHERE TN_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND TN_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND TN_DATE = '");
            sql.Append(DATE);
            sql.Append("'");

            using (DataTable dt = DBConnection.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                            trunNum = dtr[0].ToString();
                    }
                }
            }

            return trunNum;
        }

        public DataTable ItemSearch(string sDate, string eDate)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append(@"SELECT SD_PNAM AS 품목, SUM(CASE WHEN SD_GUBN = '정상' THEN SD_CONT ELSE 0 END) [수량], ");
                sql.Append(@"SUM(CASE WHEN SD_GUBN = '정상' THEN SD_PAMT ELSE 0 END) [금액] ");
                sql.Append(@"FROM MS_SALED WHERE SD_BRAND = '");
                sql.Append(BRAND);
                sql.Append(@"' AND SD_STORE = '");
                sql.Append(STORE);
                sql.Append("' AND SD_DATE >= '");
                sql.Append(sDate);
                sql.Append("' AND SD_DATE <= '");
                sql.Append(eDate);
                sql.Append("' GROUP BY SD_PNAM ORDER BY SD_PNAM DESC");

                LogMenager.LogWriter(UtilHelper.Root + @"\Log\text\error", sql.ToString());
                return DBConnectionL.DoSqlSelect(sql.ToString());
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"\Log\text\error", e.Message);

                return null;
            }
        }

        public DataTable GrpSearch(string sDate, string eDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SD_GNAM AS 그룹, SUM(CASE WHEN SD_GUBN = '정상' THEN SD_CONT ELSE 0 END) [수량], ");
            sql.Append(@"SUM(CASE WHEN SD_GUBN = '정상' THEN SD_PAMT ELSE 0 END) [금액] ");
            sql.Append(@"FROM MS_SALED WHERE SD_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SD_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SD_DATE >= '");
            sql.Append(sDate);
            sql.Append("' AND SD_DATE <= '");
            sql.Append(eDate);
            sql.Append("' GROUP BY SD_GNAM");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable ABCReport(string sDate, string eDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT TOP 3 SD_PNAM, SUM(SD_CONT) CNT, SUM(SD_PAMT) AMT, ");
            sql.Append(@"((SELECT SUM(SD_CONT) FROM MS_SALED WHERE SD_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SD_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SD_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SD_DATE = '");
            sql.Append(sDate);
            sql.Append("' AND SD_DATE <= '");
            sql.Append(eDate);
            sql.Append("')) TOT FROM MS_SALED WHERE SD_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SD_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SD_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SD_DATE = '");
            sql.Append(sDate);
            sql.Append("' AND SD_DATE <= '");
            sql.Append(eDate);
            sql.Append("' GROUP BY SD_PNAM ORDER BY CNT DESC");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }
    }
}
