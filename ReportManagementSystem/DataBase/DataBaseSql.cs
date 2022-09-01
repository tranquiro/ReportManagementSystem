using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Text;
using ReportManagementSystem.DataBase;

namespace ReportManagementSystem.DataBase
{
    public class DataBaseSql
    {
        /// <summary>
        /// ログイン情報取得
        /// </summary>
        public static DataTable LoginSetting(string txtuserid)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   ユーザー情報.ユーザーID ");
            sbSql.AppendLine("  ,ユーザー情報.ユーザー名 ");
            sbSql.AppendLine("  ,ユーザー情報.管理者フラグ ");
            sbSql.AppendLine("  ,ユーザー情報.パスワード ");
            sbSql.AppendLine("  ,グループ情報.グループID");
            sbSql.AppendLine("  ,グループ情報.グループ名");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  ユーザー情報");
            sbSql.AppendLine("LEFT OUTER JOIN グループ情報");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("ユーザー情報.グループID= グループ情報.グループID");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  ユーザー情報.メールアドレス　= @USERID ");
            SqlParameter paramId = new SqlParameter("USERID", txtuserid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  ユーザー情報.削除フラグ　= 0 ");

            //データ取得
            DataTable dt = DataBaseLogic.DataBaseUnit(sbSql, paramId);
            return dt;
        }

        /// <summary>
        /// 個人課題取得
        /// </summary>
        public static DataTable GetPersonalReport(int userid)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   レポート情報.レポートID ");
            sbSql.AppendLine("  ,提出情報.登録日時 ");
            sbSql.AppendLine("  ,レポート情報.提出期限日 ");
            sbSql.AppendLine("  ,レポート情報.レポート概要 ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  レポート情報");
            sbSql.AppendLine("INNER JOIN 講義情報");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("レポート情報.講義ID= 講義情報.講義ID");
            sbSql.AppendLine("INNER JOIN 受講情報");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("講義情報.講義ID= 受講情報.講義ID");
            sbSql.AppendLine("INNER JOIN ユーザー情報");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("受講情報.ユーザーID= ユーザー情報.ユーザーID");
            sbSql.AppendLine("LEFT OUTER JOIN 提出情報");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("レポート情報.レポートID= 提出情報.レポートID");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("ユーザー情報.ユーザーID= 提出情報.ユーザーID");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  レポート情報.提出期限日　>= GetDate() ");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  レポート情報.課題種別 = 0 ");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  受講情報.ユーザーID = @USERID  ");
            SqlParameter paramId = new SqlParameter("USERID", userid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  レポート情報.削除フラグ　= 0 ");

            //データ取得
            DataTable dt = DataBaseLogic.DataBaseUnit(sbSql, paramId);
            return dt;
        }

        /// <summary>
        /// グループ課題取得       
        /// </summary>
        public static DataTable GetGroupReport(int groupid)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("WITH 提出レポート as(");
            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   レポート情報.レポートID ");
            sbSql.AppendLine("  ,提出情報.登録日時 ");
            sbSql.AppendLine("  ,レポート情報.提出期限日 ");
            sbSql.AppendLine("  ,レポート情報.レポート概要 ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  レポート情報");
            sbSql.AppendLine("LEFT OUTER JOIN 提出情報");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("  レポート情報.レポートID = 提出情報.レポートID");
            sbSql.AppendLine("WHERE レポート情報.講義ID");
            sbSql.AppendLine("    in ( ");
            sbSql.AppendLine("       SELECT");
            sbSql.AppendLine("           受講情報.講義ID");
            sbSql.AppendLine("       FROM ");
            sbSql.AppendLine("           ユーザー情報");
            sbSql.AppendLine("       INNER JOIN 受講情報");
            sbSql.AppendLine("       ON ");
            sbSql.AppendLine("           ユーザー情報.ユーザーID= 受講情報.ユーザーID");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  ユーザー情報.管理者フラグ = 0 ");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  ユーザー情報.グループID =  @GROUPID ");
            SqlParameter paramId = new SqlParameter("GROUPID", groupid);
            sbSql.AppendLine("GROUP BY ");
            sbSql.AppendLine("  受講情報.講義ID) ");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  レポート情報.課題種別 = 1 ");
            sbSql.AppendLine("GROUP BY ");
            sbSql.AppendLine("   レポート情報.レポートID ");
            sbSql.AppendLine("  ,提出情報.登録日時 ");
            sbSql.AppendLine("  ,レポート情報.提出期限日 ");
            sbSql.AppendLine("  ,レポート情報.レポート概要) ");

            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   a1.レポートID ");
            sbSql.AppendLine("  ,a2.登録最新日時 ");
            sbSql.AppendLine("  ,a1.提出期限日 ");
            sbSql.AppendLine("  ,a1.レポート概要 ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  提出レポート as a1");
            sbSql.AppendLine("INNER JOIN (");
            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   レポートID ");
            sbSql.AppendLine("  ,MAX(登録日時) as 登録最新日時 ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  提出レポート");
            sbSql.AppendLine("GROUP BY");
            sbSql.AppendLine("  レポートID)as a2 ");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("  a1.レポートID = a2.レポートID ");
            sbSql.AppendLine("GROUP BY");
            sbSql.AppendLine("   a1.レポートID ");
            sbSql.AppendLine("  ,a2.登録最新日時 ");
            sbSql.AppendLine("  ,a1.提出期限日 ");
            sbSql.AppendLine("  ,a1.レポート概要 ");

            //データ取得
            DataTable dt = DataBaseLogic.DataBaseUnit(sbSql, paramId);
            return dt;
        }

        /// <summary>
        /// 大学リスト取得
        /// </summary>
        public static DataTable GetUnibersityList(int userid)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   大学情報.大学ID ");
            sbSql.AppendLine("  ,大学情報.大学名 ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  大学情報");
            sbSql.AppendLine("LEFT OUTER JOIN ユーザー情報");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("大学情報.大学ID= ユーザー情報.大学ID");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  ユーザー情報.ユーザーID = @USERID  ");
            SqlParameter paramId = new SqlParameter("USERID", userid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  大学情報.削除フラグ　= 0 ");

            //データ取得
            DataTable dt = DataBaseLogic.DataBaseUnit(sbSql, paramId);
            return dt;
        }

        /// <summary>
        /// 講義リスト取得
        /// </summary>
        public static DataTable GetLectureList(int userid)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   講義情報.講義ID ");
            sbSql.AppendLine("  ,講義情報.講義名 ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  講義情報");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  講義情報.大学ID = @UNIVID  ");
            SqlParameter paramId = new SqlParameter("UNIVID", userid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine(" 講義情報.削除フラグ　= 0 ");

            //データ取得
            DataTable dt = DataBaseLogic.DataBaseUnit(sbSql, paramId);
            return dt;
        }

        /// <summary>
        /// レポートデータ取得
        /// </summary>
        public static DataTable GetReportData(int lecid)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   レポート情報.レポートID ");
            sbSql.AppendLine("  ,レポート情報.提出期限日 ");
            sbSql.AppendLine("  ,レポート情報.レポート概要 ");
            sbSql.AppendLine("  ,レポート情報.課題種別 ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  レポート情報");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  レポート情報.講義ID = @LECID  ");
            SqlParameter paramId = new SqlParameter("LECID", lecid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine(" レポート情報.削除フラグ　= 0 ");

            //データ取得
            DataTable dt = DataBaseLogic.DataBaseUnit(sbSql, paramId);
            return dt;
        }
    }
}