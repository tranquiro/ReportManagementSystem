using System;
using System.Data;

namespace ReportManagementSystem.CommonLogic
{
    public class ReportViewCreate
    {
        //レポート提出状況表示テーブル作成メソッド
        public static DataTable GetDataTable(DataTable dt)
        {
            //レポート提出状況表示テーブル
            DataTable returndt = new DataTable();

            //列追加
            returndt.Columns.Add("ReportId");
            returndt.Columns.Add("SubmissionStatus");
            returndt.Columns.Add("SubmissionDeadline");
            returndt.Columns.Add("ReportSummary");

            //行回数文ループ
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //行作成
                DataRow row;
                row = returndt.NewRow();

                //レポートID列
                row["ReportId"] = dt.Rows[i][0];

                //提出レポートがない場合
                if (dt.Rows[i][1].Equals(DBNull.Value))
                {
                    //提出状態列
                    row["SubmissionStatus"] = "~/Image/Unsubmit.png";
                }
                else
                {
                    //提出状態列
                    row["SubmissionStatus"] = "~/Image/Submit.png";
                }

                //提出期限列
                row["SubmissionDeadline"] = String.Format("{0:yyyy/MM/dd}", dt.Rows[i][2]);

                //レポート概要列
                row["ReportSummary"] = dt.Rows[i][3];

                //行追加
                returndt.Rows.Add(row);
            }

            returndt.AcceptChanges();
            return returndt;
        }

        //検索講義レポート表示メソッド
        public static DataTable GetSearchtable(DataTable ReportData)
        {
            //レポート一覧グリッドビュー表示テーブル
            DataTable search_dt = new DataTable();

            //列追加
            search_dt.Columns.Add("ReportId");
            search_dt.Columns.Add("SubmissionDeadline");
            search_dt.Columns.Add("ReportSummary");
            search_dt.Columns.Add("LectureType");
            search_dt.Columns.Add("SubmissionList");

            //行回数文ループ
            for (int i = 0; i < ReportData.Rows.Count; i++)
            {
                //行作成
                DataRow row;
                row = search_dt.NewRow();

                //レポートID列
                row["ReportId"] = Convert.ToInt32(ReportData.Rows[i][0]);

                //提出期限列
                row["SubmissionDeadline"] = String.Format("{0:yyyy/MM/dd}", ReportData.Rows[i][1]);

                //レポート概要列
                row["ReportSummary"] = ReportData.Rows[i][2];

                //課題種別が0の場合
                if (Convert.ToInt32(ReportData.Rows[i][3]) == 0)
                {
                    row["LectureType"] = "個人";
                }
                else
                {
                    row["LectureType"] = "グループ";
                }

                //提出一覧列
                row["SubmissionList"] = "提出一覧";

                //行追加
                search_dt.Rows.Add(row);
            }

            search_dt.AcceptChanges();
            return search_dt;
        }
    }
}