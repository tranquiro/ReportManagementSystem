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
            sbSql.AppendLine("   UserInfo.UserId ");
            sbSql.AppendLine("  ,UserInfo.UserName ");
            sbSql.AppendLine("  ,UserInfo.AdministratorFlag ");
            sbSql.AppendLine("  ,UserInfo.Password ");
            sbSql.AppendLine("  ,GroupInfo.GroupId");
            sbSql.AppendLine("  ,GroupInfo.GroupName");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  UserInfo");
            sbSql.AppendLine("LEFT OUTER JOIN GroupInfo");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("UserInfo.GroupId= GroupInfo.GroupId");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  UserInfo.MailAddress　= @USERID ");
            SqlParameter paramId = new SqlParameter("USERID", txtuserid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  UserInfo.DeleteFlag　= 0 ");

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
            sbSql.AppendLine("   ReportInfo.ReportId ");
            sbSql.AppendLine("  ,SubmissionInfo.CreateDatetime ");
            sbSql.AppendLine("  ,ReportInfo.SubmissionDeadline ");
            sbSql.AppendLine("  ,ReportInfo.ReportSummary ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  ReportInfo");
            sbSql.AppendLine("INNER JOIN LectureInfo");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("ReportInfo.LectureId= LectureInfo.LectureId");
            sbSql.AppendLine("INNER JOIN AttendInfo");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("LectureInfo.LectureId= AttendInfo.LectureId");
            sbSql.AppendLine("INNER JOIN UserInfo");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("AttendInfo.UserId= UserInfo.UserId");
            sbSql.AppendLine("LEFT OUTER JOIN SubmissionInfo");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("ReportInfo.ReportId= SubmissionInfo.ReportId");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("UserInfo.UserId= SubmissionInfo.UserId");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  ReportInfo.SubmissionDeadline　>= GetDate() ");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  ReportInfo.LectureType = 0 ");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  AttendInfo.UserId = @USERID  ");
            SqlParameter paramId = new SqlParameter("USERID", userid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  ReportInfo.DeleteFlag　= 0 ");

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

            sbSql.AppendLine("WITH SubmitReport as(");
            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   ReportInfo.ReportId ");
            sbSql.AppendLine("  ,SubmissionInfo.CreateDatetime ");
            sbSql.AppendLine("  ,ReportInfo.SubmissionDeadline ");
            sbSql.AppendLine("  ,ReportInfo.ReportSummary ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  ReportInfo");
            sbSql.AppendLine("LEFT OUTER JOIN SubmissionInfo");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("  ReportInfo.ReportId = SubmissionInfo.ReportId");
            sbSql.AppendLine("WHERE ReportInfo.LectureId");
            sbSql.AppendLine("    in ( ");
            sbSql.AppendLine("       SELECT");
            sbSql.AppendLine("           AttendInfo.LectureId");
            sbSql.AppendLine("       FROM ");
            sbSql.AppendLine("           UserInfo");
            sbSql.AppendLine("       INNER JOIN AttendInfo");
            sbSql.AppendLine("       ON ");
            sbSql.AppendLine("           UserInfo.UserId= AttendInfo.UserId");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  UserInfo.AdministratorFlag = 0 ");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  UserInfo.GroupId =  @GROUPID ");
            SqlParameter paramId = new SqlParameter("GROUPID", groupid);
            sbSql.AppendLine("GROUP BY ");
            sbSql.AppendLine("  AttendInfo.LectureId) ");
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  ReportInfo.LectureType = 1 ");
            sbSql.AppendLine("GROUP BY ");
            sbSql.AppendLine("   ReportInfo.ReportId ");
            sbSql.AppendLine("  ,SubmissionInfo.CreateDatetime ");
            sbSql.AppendLine("  ,ReportInfo.SubmissionDeadline ");
            sbSql.AppendLine("  ,ReportInfo.ReportSummary) ");

            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   a1.ReportId ");
            sbSql.AppendLine("  ,a2.CreateNewDatetime ");
            sbSql.AppendLine("  ,a1.SubmissionDeadline ");
            sbSql.AppendLine("  ,a1.ReportSummary ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  SubmitReport as a1");
            sbSql.AppendLine("INNER JOIN (");
            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   ReportId ");
            sbSql.AppendLine("  ,MAX(CreateDatetime) as CreateNewDatetime");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  SubmitReport");
            sbSql.AppendLine("GROUP BY");
            sbSql.AppendLine("  ReportId)as a2 ");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("  a1.ReportId = a2.ReportId ");
            sbSql.AppendLine("GROUP BY");
            sbSql.AppendLine("   a1.ReportId ");
            sbSql.AppendLine("  ,a2.CreateNewDatetime ");
            sbSql.AppendLine("  ,a1.SubmissionDeadline ");
            sbSql.AppendLine("  ,a1.ReportSummary ");

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
            sbSql.AppendLine("   UniversityInfo.UniversityId ");
            sbSql.AppendLine("  ,UniversityInfo.UniversityName ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  UniversityInfo");
            sbSql.AppendLine("LEFT OUTER JOIN UserInfo");
            sbSql.AppendLine("ON ");
            sbSql.AppendLine("UniversityInfo.UniversityId= UserInfo.UniversityId");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  UserInfo.UserId = @USERID  ");
            SqlParameter paramId = new SqlParameter("USERID", userid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine("  UniversityInfo.DeleteFlag　= 0 ");

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
            sbSql.AppendLine("   LectureInfo.LectureId ");
            sbSql.AppendLine("  ,LectureInfo.LectureName ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  LectureInfo");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  LectureInfo.UniversityId = @UNIVID  ");
            SqlParameter paramId = new SqlParameter("UNIVID", userid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine(" LectureInfo.DeleteFlag　= 0 ");

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
            sbSql.AppendLine("   ReportInfo.ReportId ");
            sbSql.AppendLine("  ,ReportInfo.SubmissionDeadline ");
            sbSql.AppendLine("  ,ReportInfo.ReportSummary ");
            sbSql.AppendLine("  ,ReportInfo.LectureType ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  ReportInfo");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  ReportInfo.LectureId = @LECID  ");
            SqlParameter paramId = new SqlParameter("LECID", lecid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine(" ReportInfo.DeleteFlag　= 0 ");

            //データ取得
            DataTable dt = DataBaseLogic.DataBaseUnit(sbSql, paramId);
            return dt;
        }

        /// <summary>
        /// レポート詳細データ取得
        /// </summary>
        public static DataTable GetReportDetailData(int reportid)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("SELECT    ");
            sbSql.AppendLine("   ReportInfo.LectureType ");
            sbSql.AppendLine("  ,ReportInfo.SubmissionDeadline ");
            sbSql.AppendLine("  ,ReportInfo.ReportSummary ");
            sbSql.AppendLine("  ,ReportInfo.ReportContents ");
            sbSql.AppendLine("FROM ");
            sbSql.AppendLine("  ReportInfo");
            sbSql.AppendLine("WHERE ");
            sbSql.AppendLine("  ReportInfo.ReportId = @REPORTID  ");
            SqlParameter paramId = new SqlParameter("REPORTID", reportid);
            sbSql.AppendLine("AND ");
            sbSql.AppendLine(" ReportInfo.DeleteFlag　= 0 ");

            //データ取得
            DataTable dt = DataBaseLogic.DataBaseUnit(sbSql, paramId);
            return dt;
        }
    }
}