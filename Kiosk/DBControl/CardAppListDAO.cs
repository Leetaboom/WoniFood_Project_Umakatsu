using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Kiosk.DBControl
{
    class CardAppListDAO
    {
        const int APPLISTCOUNT_MAX = 15;

        static int nCardAppListCount;
        static int nCardAppListPage = 1;
        static int nCardAppListEndPage;

        public static string sSearchDate { get; set; }

        public static int CardAppAllListCount
        {
            get { return nCardAppListCount; }
            set
            {
                nCardAppListCount = value;
                CardAppAllListEndPage = value;
            }
        }

        public static int CardAppAllListPage
        {
            get { return nCardAppListPage; }
            set
            {
                if (value < 1)
                    nCardAppListPage = 1;
                else
                    nCardAppListPage = value;
            }
        }

        public static int CardAppAllListEndPage
        {
            get { return nCardAppListEndPage; }
            set
            {
                nCardAppListEndPage = value / APPLISTCOUNT_MAX;

                if ((value % APPLISTCOUNT_MAX) > 0)
                    nCardAppListEndPage++;

                if (nCardAppListEndPage < 1)
                    nCardAppListEndPage = 1;
            }
        }

        public static DataTable CardAppList()
        {
            string search = sSearchDate;

            StringBuilder where = new StringBuilder();
            where.Append(" WHERE VD_BRAND = '");
            where.Append(StoreInfo.BrnadCode);
            where.Append("' AND VD_STORE = '");
            where.Append(StoreInfo.StoreCode);
            where.Append("' AND VD_DESK = '");
            where.Append(StoreInfo.StoreDesk);
            where.Append("' AND VD_DATE = '");
            where.Append((search.CompareTo("") == 0 ? StoreInfo.StoreOpen : search));
            where.Append("'");

            StringBuilder where2 = new StringBuilder();
            where2.Append(" VD_BRAND = '");
            where2.Append(StoreInfo.BrnadCode);
            where2.Append("' AND VD_STORE = '");
            where2.Append(StoreInfo.StoreCode);
            where2.Append("' AND VD_DESK = '");
            where2.Append(StoreInfo.StoreDesk);
            where2.Append("' AND VD_DATE = '");
            where2.Append((search.CompareTo("") == 0 ? StoreInfo.StoreOpen : search));
            where2.Append("'");

            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT VD_APPDATE AS ""승인일자"", VD_APPTIME AS ""승인시간"", VD_RECENO AS ""영수증번호"", VD_GUBN, ");
            sql.Append(@"VD_APPNO AS ""승인번호"", VD_APPAMT AS ""승인금액"", VD_INPNM AS ""카드사명"", VD_MONTH AS ""할부개월"", ");
            sql.Append(@"VD_REMARK FROM MS_VANDATA ");
            sql.Append(where);

            
            CardAppAllListCount = DBConnectionL.DoSqlSelect(sql.ToString()).Rows.Count;

            if (nCardAppListPage <= 1)
            {
                sql.Clear();

                sql.Append(@"SELECT TOP ");
                sql.Append(APPLISTCOUNT_MAX.ToString());
                sql.Append(@" VD_APPDATE AS ""승인일자"", VD_APPTIME AS ""승인시간"", VD_RECENO AS ""영수증번호"", ");
                sql.Append(@"VD_APPNO AS ""승인번호"", VD_GUBN, VD_APPAMT AS ""승인금액"", VD_INPNM AS ""카드사명"", ");
                sql.Append(@"VD_MONTH AS ""할부개월"", VD_REMARK FROM MS_VANDATA ");
                sql.Append(where);
                sql.Append(" ORDER BY VD_RECENO DESC ");
            }
            else
            {
                sql.Clear();

                sql.Append(@"SELECT TOP ");
                sql.Append(APPLISTCOUNT_MAX.ToString());
                sql.Append(@" VD_APPDATE AS ""승인일자"", VD_APPTIME AS ""승인시간"", VD_RECENO AS ""영수증번호"", ");
                sql.Append(@"VD_APPNO AS ""승인번호"", VD_GUBN, VD_APPAMT AS ""승인금액"", VD_INPNM AS ""카드사명"", ");
                sql.Append(@"VD_MONTH AS ""할부개월"", VD_REMARK FROM MS_VANDATA ");
                sql.Append(@"WHERE VD_RECENO NOT IN (SELECT TOP ");
                sql.Append(((nCardAppListPage - 1) * APPLISTCOUNT_MAX).ToString());
                sql.Append(@" VD_RECENO FROM MS_VANDATA ");
                sql.Append(where);
                sql.Append(@" ORDER BY VD_RECENO DESC) ");

                sql.Append(" AND ");
                sql.Append(where2);
                sql.Append(" ORDER BY VD_RECENO DESC ");
            }

            LogMenager.LogWriter(UtilHelper.Root + @"\Log\err\sql", "[Error]:" + sql.ToString());

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public static DataTable GetCardCancel(string appDate, string appRecNo, string appNo, string appAmt)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT * FROM MS_VANDATA WHERE VD_BRAND = '");
            sql.Append(StoreInfo.BrnadCode);
            sql.Append("' AND VD_STORE = '");
            sql.Append(StoreInfo.StoreCode);
            sql.Append("' AND VD_DESK = '");
            sql.Append(StoreInfo.StoreDesk);
            sql.Append("' AND VD_APPDATE = '");
            sql.Append(appDate);
            sql.Append("' AND VD_RECENO = '");
            sql.Append(appRecNo);
            sql.Append("' AND VD_APPNO = '");
            sql.Append(appNo);
            sql.Append("' AND VD_APPAMT = '");
            sql.Append(appAmt);
            sql.Append("' AND VD_TYPE = '30'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
        }

        public static DataTable GetSamsungPay(string appDate, string appRecNo, string appNo, string appAmt)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT * FROM MS_VANDATA WHERE VD_BRAND = '");
            sql.Append(StoreInfo.BrnadCode);
            sql.Append("' AND VD_STORE = '");
            sql.Append(StoreInfo.StoreCode);
            sql.Append("' AND VD_DESK = '");
            sql.Append(StoreInfo.StoreDesk);
            sql.Append("' AND VD_APPDATE = '");
            sql.Append(appDate);
            sql.Append("' AND VD_RECENO = '");
            sql.Append(appRecNo);
            sql.Append("' AND VD_APPNO = '");
            sql.Append(appNo);
            sql.Append("' AND VD_APPAMT = '");
            sql.Append(appAmt);
            sql.Append("' AND VD_REMARK = 'S'");

            return DBConnectionL.DoSqlSelect(sql.ToString());
            
        }
    }
}
