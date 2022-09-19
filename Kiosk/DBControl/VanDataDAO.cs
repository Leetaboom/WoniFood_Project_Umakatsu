using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class VanDataDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DESK { get; set; }
        public string VANGB { get; set; }
        public string SEQN { get; set; }
        public string DATE { get; set; }
        public string RECENO { get; set; }
        public string GUBN { get; set; }
        public string TYPE { get; set; }
        public string VALID { get; set; }
        public decimal APPAMT { get; set; }
        public decimal TAX { get; set; }
        public decimal BONG { get; set; }
        public string MOMTH { get; set; }
        public string VANKEY { get; set; }
        public string APPNO { get; set; }
        public string APPDATE { get; set; }
        public string APPTIME { get; set; }
        public string ORDCD { get; set; }
        public string ORDNM { get; set; }
        public string INPCD { get; set; }
        public string INPNM { get; set; }
        public string STOCD { get; set; }
        public string CATID { get; set; }
        public string MSG { get; set; }
        public string CARDNO { get; set; }
        public string CDTYPE { get; set; }
        public string REMARK { get; set; }

        public bool VanDataSave()
        {
            try
            {
                string spName = "MS_VANDATA_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"VANGB", @"DATE", @"RECENO", @"GUBN", @"TYPE", @"VALID",
                                  @"APPAMT", @"TAX", @"BONG", @"MONTH", @"APPNO", @"VANKEY", @"APPDATE", @"APPTIME", @"ORDCD", @"ORDNM",
                                  @"INPCD", @"INPNM", @"STOCD", @"CATID", @"MSG", @"CARDNO", @"CDTYPE", @"REMARK" };
                object[] paraValue = { BRAND, STORE, DESK, VANGB, DATE, RECENO, GUBN, TYPE, VALID, APPAMT, TAX, BONG, MOMTH, APPNO,
                                   VANKEY, APPDATE, APPTIME, ORDCD, ORDNM, INPCD, INPNM, STOCD, CATID, MSG, CARDNO, CDTYPE, REMARK };

                return DBConnection.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"Log\text\error", "[VanData Save]:" + e.Message);
                return false;
            }
        }

        public bool VanDataSaveL()
        {
            try
            {
                string spName = "MS_VANDATA_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"VANGB", @"DATE", @"RECENO", @"GUBN", @"TYPE", @"VALID",
                                  @"APPAMT", @"TAX", @"BONG", @"MONTH", @"APPNO", @"VANKEY", @"APPDATE", @"APPTIME", @"ORDCD", @"ORDNM",
                                  @"INPCD", @"INPNM", @"STOCD", @"CATID", @"MSG", @"CARDNO", @"CDTYPE", @"REMARK" };
                object[] paraValue = { BRAND, STORE, DESK, VANGB, DATE, RECENO, GUBN, TYPE, VALID, APPAMT, TAX, BONG, MOMTH, APPNO,
                                   VANKEY, APPDATE, APPTIME, ORDCD, ORDNM, INPCD, INPNM, STOCD, CATID, MSG, CARDNO, CDTYPE, REMARK };

                return DBConnectionL.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"Log\text\error", "[VanData SaveL]:" + e.Message);
                return false;
            }
        }

        public DataTable InpnmSearch(string sDate, string eDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT VD_INPNM AS ""매입사명"", COUNT(CASE WHEN VD_TYPE = '10' THEN VD_APPAMT END) - ");
            sql.Append("COUNT(CASE WHEN VD_TYPE = '30' THEN VD_APPAMT END) [건수], ");
            sql.Append(@"SUM(CASE WHEN VD_TYPE = '10' THEN VD_APPAMT ELSE 0 END) - ");
            sql.Append(@"SUM(CASE WHEN VD_TYPE = '30' THEN VD_APPAMT ELSE 0 END) [금액] ");
            sql.Append(@"FROM MS_VANDATA WHERE VD_BRAND = '");
            sql.Append(BRAND);
            sql.Append(@"' AND VD_STORE = '");
            sql.Append(STORE);
            sql.Append(@"' AND VD_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND VD_DATE >= '");
            sql.Append(sDate);
            sql.Append("' AND VD_DATE <= '");
            sql.Append(eDate);
            sql.Append("' GROUP BY VD_INPNM");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable SalesCancelSearch(string sDate, string eDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT VD_DATE AS ""일자"", VD_RECENO AS ""영수증번호"", VD_ORDNM AS ""카드사명"", VD_APPAMT AS ""금액"" ");
            sql.Append(@"FROM MS_VANDATA WHERE VD_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND VD_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND VD_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND VD_TYPE = '30' AND VD_DATE >= '");
            sql.Append(sDate);
            sql.Append("' AND VD_DATE <= '");
            sql.Append(eDate);
            sql.Append("' ORDER BY VD_DATE DESC");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }
    }
}
