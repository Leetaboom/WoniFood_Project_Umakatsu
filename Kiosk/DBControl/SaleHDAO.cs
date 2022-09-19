using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kiosk.DBControl
{
    class SaleHDAO
    {
        public string BRAND { get; set; }
        public string STORE { get; set; }
        public string DATE { get; set; }
        public string TIME { get; set; }
        public string DESK { get; set; }
        public string RECENO { get; set; }
        public string MEMO { get; set; }
        public string GUBN { get; set; }
        public string APPDATE { get; set; }
        public string SDATE { get; set; }
        public string SELECTDATE { get; set; }
        public string TYPE { get; set; }
        public string REMARK { get; set; }
        public string TURN { get; set; }
        public decimal PAMT { get; set; }
        public decimal CARD { get; set; }
        public decimal TMON { get; set; }
        public decimal SPAY { get; set; }
        public decimal DC { get; set; }

        public bool SaleHSave()
        {
            try
            {
                string spName = "MS_SALEH_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"TIME", @"RECENO", @"PAMT", @"CARD", @"SPAY", @"TMON",
                                  @"DC", @"MEMO", @"GUBN", @"TYPE", @"REMARK", @"TURN" };
                object[] paraValue = { BRAND, STORE, DESK, DATE, TIME, RECENO, PAMT, CARD, SPAY, TMON, DC, MEMO, GUBN, TYPE, REMARK, TURN };

                return DBConnection.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"Log\text\error", "[SalesH Save]:" + e.Message);
                return false;
            }
        }

        public bool SaleHSaveL()
        {
            try
            {
                string spName = "MS_SALEH_SAVE";
                object[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"TIME", @"RECENO", @"PAMT", @"CARD", @"SPAY", @"TMON",
                                  @"DC", @"MEMO", @"GUBN", @"TYPE", @"REMARK", @"TURN" };
                object[] paraValue = { BRAND, STORE, DESK, DATE, TIME, RECENO, PAMT, CARD, SPAY, TMON, DC, MEMO, GUBN, TYPE, REMARK, TURN };

                return DBConnectionL.DoCommand(spName, paraName, paraValue);
            }
            catch (Exception e)
            {
                LogMenager.LogWriter(UtilHelper.Root + @"Log\text\error", "[SalesH SaveL]:" + e.Message);
                return false;
            }
        }
        public DataTable SaleH_CardAppSelectL()
        {
            string spName = "MS_SALEH_CARDAPP_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"SELECTDATE" };
            string[] paraValue = { BRAND, STORE, DESK, SELECTDATE };

            return DBConnectionL.DoSelect(spName, paraName, paraValue);
        }

        public DataTable SaleH_CardCancelSelect()
        {
            string spName = "MS_SALEH_CARDCANCEL_SELECT";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"SELECTDATE" };
            string[] paraValue = { BRAND, STORE, DESK, SELECTDATE };

            return DBConnectionL.DoSelect(spName, paraName, paraValue);
        }

        public DataTable SaleH_SpayCancelSelect()
        {
            string spName = "MS_SALEH_SPAYCANCEL_SELECT";
            string[] paraName = { @"BRAND", @"STORE", "DESK", @"SELECTDATE" };
            string[] paraValue = { BRAND, STORE, DESK, SELECTDATE };

            return DBConnectionL.DoSelect(spName, paraName, paraValue);
        }

        public bool SaleH_Update()
        {
            string spName = "MS_SALEH_UPDATE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"RECENO", @"GUBN" };
            string[] paraValue = { BRAND, STORE, DESK, DATE, RECENO, GUBN };

            return DBConnection.DoCommand(spName, paraName, paraValue);
        }

        public bool SaleH_UpdateL()
        {
            string spName = "MS_SALEH_UPDATE";
            string[] paraName = { @"BRAND", @"STORE", @"DESK", @"DATE", @"RECENO", @"GUBN" };
            string[] paraValue = { BRAND, STORE, DESK, DATE, RECENO, GUBN };

            return DBConnectionL.DoCommand(spName, paraName, paraValue);
        }

        //출고액 쿼리
        public decimal ChulGoaek(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SUM(SH_PAMT) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE  >= (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }
            return 0;
        }

        //반품액
        public decimal SalesReturn(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SUM(SH_PAMT) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_GUBN = '취소' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }
            return 0;
        }

        //신용카드 매출액
        public decimal SalesCard(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SUM(SH_CARD) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_TYPE = 'C' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }
            return 0;
        }

        //삼성페이 매출액
        public decimal SalesSPay(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SUM(SH_SPAY) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_TYPE = 'S' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }
            return 0;
        }

        //총 취소 건수
        public decimal SalesTotalCancel(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT COUNT(*) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_GUBN = '취소' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }

            return 0;
        }

        //신용카드 승인건수
        public decimal SalesCardAppNum(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT COUNT(*) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_TYPE = 'C' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }

            return 0;
        }

        //삼성페이 승인건수
        public decimal SalesSpayAppNum(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT COUNT(*) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_TYPE = 'S' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }

            return 0;
        }

        //신용카드 취소 건수
        public decimal SalesCardCancelNum(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT COUNT(*) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_TYPE = 'C' AND SH_GUBN = '취소' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }

            return 0;
        }

        //삼성페이 취소 건수
        public decimal SalesSpayCancelNum(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT COUNT(*) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_TYPE = 'S' AND SH_GUBN = '취소' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }

            return 0;
        }

        //신용카드 취소액
        public decimal SalesCardCancelAmt(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SUM(SH_CARD) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_TYPE = 'C' AND SH_GUBN = '취소' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }

            return 0;
        }

        //삼성페이 취소액
        public decimal SalesSpayCancelAmt(string openDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SUM(SH_SPAY) FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_TYPE = 'S' AND SH_GUBN = '취소' AND SH_DATE = '");
            sql.Append(openDate);
            sql.Append("' AND SH_FDATE > (SELECT OK_START FROM CF_OPENKIOSK WHERE OK_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND OK_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND OK_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND OK_STOP IS NULL)");

            using (DataTable dt = DBConnectionL.DoSqlSelect(sql.ToString()))
            {
                if (dt.Rows.Count != 0)
                {
                    using (DataTableReader dtr = new DataTableReader(dt))
                    {
                        while (dtr.Read())
                        {
                            if (dtr[0].ToString().Length != 0)
                                return dtr[0].ToDecimal();
                        }
                    }
                }
            }

            return 0;
        }

        /*
         * 
         * 매출조회 함수
         */
         
        //당일 매출 조회
        public DataTable TheDayCardSearch()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT COUNT(*) AS ""건수"", SUM(SH_CARD) + SUM(SH_SPAY) AS ""금액"" FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_DATE = '");
            sql.Append(StoreInfo.StoreOpen);
            sql.Append("' AND SH_GUBN = '정상'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        //일별 매출 조회
        public DataTable OneDayCardSearch(string sDate, string eDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SH_DATE AS ""일자"", COUNT(*) AS ""건수"", SUM(SH_CARD) + SUM(SH_SPAY) AS ""금액"" ");
            sql.Append("FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DATE >= '");
            sql.Append(sDate);
            sql.Append("' AND SH_DATE <= '");
            sql.Append(eDate);
            sql.Append("' AND SH_GUBN != '취소' GROUP BY SH_DATE");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        //월별 매출 조회
        public DataTable MonthCardSearch(string sDate, string eDate)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SUBSTRING(SH_DATE, 5, 2) AS ""일자"", COUNT(*) AS ""건수"", SUM(SH_CARD) + SUM(SH_SPAY) AS ""금액"" ");
            sql.Append("FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DATE >= '");
            sql.Append(sDate);
            sql.Append("' AND SH_DATE <= '");
            sql.Append(eDate);
            sql.Append("' AND SH_GUBN != '취소' GROUP BY SUBSTRING(SH_DATE, 5, 2)");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public DataTable GetStoreFlag()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT SH_MEMO FROM MS_SALEH WHERE SH_BRAND = '");
            sql.Append(BRAND);
            sql.Append("' AND SH_STORE = '");
            sql.Append(STORE);
            sql.Append("' AND SH_DESK = '");
            sql.Append(DESK);
            sql.Append("' AND SH_DATE = '");
            sql.Append(DATE);
            sql.Append("' AND SH_RECENO = '");
            sql.Append(RECENO);
            sql.Append("' AND SH_GUBN = '취소'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }
    }
}
