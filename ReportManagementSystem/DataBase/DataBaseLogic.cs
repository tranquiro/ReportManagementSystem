using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace ReportManagementSystem.DataBase
{
    public class DataBaseLogic
    {
        /// <summary>
        /// データベース接続
        /// </summary>
        public static DataTable DataBaseUnit(StringBuilder sbSql, SqlParameter paramId)
        {
            //Web.configよりDB接続文字列を取得する
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ReportManagementDB"].ToString();

            //DataBase接続用オブジェクトの作成
            SqlConnection conn = new SqlConnection(connectionString);

            //Select用コマンドオブジェクトの作成
            SqlCommand selectCmd = new SqlCommand();
            selectCmd.Connection = conn;

            selectCmd.Parameters.Add(paramId);

            selectCmd.CommandText = sbSql.ToString();

            //DataAdapterの作成
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = selectCmd;

            // DataTableへ読み込み
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}

 