using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Kiosk.DBControl
{
    public class DBConnectionL
    {
        private static SqlConnection con;

        public static SqlConnection GetConnection()
        {
            //string strSQL = @"server=ms1203.gabiadb.com,1433;database=wonidb;user id=woni;password=woni@6030p";
            //string strSQL = @"Data Source=(LocalDB)\v12.0;AttachDBFileName=|DataDirectory|Data\KioskRes_Data.mdf;Integrated Security=true";
            string strSQL = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFileName=|DataDirectory|Data\Kiosk_Local.mdf;Integrated Security=true";
            con = new SqlConnection();
            con.ConnectionString = strSQL;

            return con;
        }

        public static DataTable DoSqlSelect(string sql)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand scm = new SqlCommand();
                scm.Connection = GetConnection();
                scm.CommandText = sql;
                scm.CommandType = CommandType.Text;
                sda.SelectCommand = scm;

                try
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "result");

                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public static bool DoSqlCommand(string sql)
        {
            bool result = false;

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand scm = new SqlCommand();
                scm.Connection = GetConnection();
                scm.CommandText = sql;
                scm.CommandType = CommandType.Text;
                sda.SelectCommand = scm;

                try
                {
                    scm.Connection.Open();

                    if (scm.ExecuteNonQuery() > 0)
                        return true;

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    scm.Connection.Close();
                }
            }
        }

        public static bool DoCommand(string spName, object[] paraName, object[] paraValue)
        {
            bool result = false;

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand scm = new SqlCommand();
                scm.Connection = GetConnection();
                scm.CommandText = spName;
                scm.CommandType = CommandType.StoredProcedure;

                if (paraValue != null && paraValue.Length != 0)
                {
                    for (int i = 0; i < paraValue.Length; i++)
                    {
                        SqlParameter spr = new SqlParameter();
                        spr.Value = paraValue[i];
                        spr.ParameterName = paraName[i].ToString();
                        spr.Direction = ParameterDirection.Input;
                        scm.Parameters.Add(spr);
                    }
                }
                sda.SelectCommand = scm;

                try
                {
                    scm.Connection.Open();

                    if (scm.ExecuteNonQuery() > 0)
                        result = true;

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    scm.Connection.Close();
                }
            }
        }

        public static DataTable DoSelect(string spName, object[] paraName, object[] paraValue)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand scm = new SqlCommand();

                scm.Connection = GetConnection();
                scm.CommandText = spName;
                scm.CommandType = CommandType.StoredProcedure;

                if (paraValue != null && paraValue.Length != 0)
                {
                    for (int i = 0; i < paraValue.Length; i++)
                    {
                        SqlParameter spr = new SqlParameter();
                        spr.Value = paraValue[i];
                        spr.ParameterName = paraName[i].ToString();
                        spr.Direction = ParameterDirection.Input;
                        scm.Parameters.Add(spr);
                    }
                }
                sda.SelectCommand = scm;

                try
                {
                    DataSet ds = new DataSet();

                    sda.Fill(ds, "result");

                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
