using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using ReportManagementSystem.DataBase;

namespace ReportManagementSystem.UI
{
    //学生用Top画面
    public partial class TopStudentForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ユーザーアイコン・ユーザーラベルの表示
            IcnUser.Visible = true;
            LblUserName.Visible = true;
            LblUserName.Text = (string)Session["USERNAME"] + "　様";

            //グループアイコン・グループラベルの非表示
            IcnGroup.Visible = false;
            LblGroupName.Visible = false;

            //存在エラーラベルの非表示
            LblExistErr.Visible = false;

            //レポート一覧グリッドビューの非表示
            GrdReport.Visible = false;
        }

        //ログアウトボタン押下時
        protected void LnkLogOut_Click(object sender, EventArgs e)
        {
            //Sessionに格納した情報を削除
            Session.RemoveAll();

            //画面遷移
            Response.Redirect("LoginForm.aspx");
        }

        //個人課題ボタン押下時
        protected void BtnPersonalReport_Click(object sender, EventArgs e)
        {
            //ユーザーアイコン・ユーザーラベルの表示
            IcnUser.Visible = true;
            LblUserName.Visible = true;
            LblUserName.Text = (string)Session["UserName"] + "　様";

            //グループアイコン・グループラベルの非表示
            IcnGroup.Visible = false;
            LblGroupName.Visible = false;

            //存在エラーラベルの非表示
            LblExistErr.Visible = false;

            //レポート一覧グリッドビューの表示
            GrdReport.Visible = true;

            //レポート提出状況のチェック
            DataTable dt = DataBaseSql.GetPersonalReport((int)Session["UserId"]);
            
            //検索結果が0件の場合
            if (dt.Rows.Count == 0)
            {
                //存在エラーラベル表示
                LblExistErr.Visible = true;
                return;
            }

            //レポート提出状況表示テーブル
            DataTable personal_dt = new DataTable();

            //列追加
            personal_dt.Columns.Add("ReportId");
            personal_dt.Columns.Add("SubmissionStatus");
            personal_dt.Columns.Add("SubmissionDeadline");
            personal_dt.Columns.Add("ReportSummary");

            //行回数文ループ
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //行作成
                DataRow row;
                row = personal_dt.NewRow();

                //レポートID列
                row["ReportId"] = dt.Rows[i][0];

                //提出レポートがない場合
                if (dt.Rows[i][1].Equals(DBNull.Value))
                {
                    //提出状態列
                    row["SubmissionStatus"] = "~/Image/Unsubmit.png";
                }
                else
                {　//提出状態列
                    row["SubmissionStatus"] = "~/Image/Submit.png";
                }

                //提出期限列
                row["SubmissionDeadline"] = String.Format("{0:yyyy/MM/dd}", dt.Rows[i][2]);

                //レポート概要列
                row["ReportSummary"] = dt.Rows[i][3];

                //行追加
                personal_dt.Rows.Add(row);
            }

            personal_dt.AcceptChanges();


            //データバインド（画面表示）
            this.GrdReport.DataSource = personal_dt;
            this.GrdReport.DataBind();
        }

        //グループ課題押下時
        protected void BtnGroupReport_Click(object sender, EventArgs e)
        {
            //ユーザーアイコン・ユーザーラベルの非表示
            IcnUser.Visible = false;
            LblUserName.Visible = false;

            //グループアイコン・グループラベルの表示
            IcnGroup.Visible = true;
            LblGroupName.Visible = true;
            LblGroupName.Text = (string)Session["GroupName"] + "グループ";

            //存在エラーラベルの非表示
            LblExistErr.Visible = false;

            //レポート一覧グリッドビューの表示
            GrdReport.Visible = true;

            //レポート提出状況のチェック
            DataTable dt = DataBaseSql.GetGroupReport((int)Session["GroupId"]);

            //検索結果が0件の場合
            if (dt.Rows.Count == 0)
            {
                //存在エラーラベル表示
                LblExistErr.Visible = true;
                return;
            }

            //レポート提出状況表示テーブル
            DataTable group_dt = new DataTable();

            //列追加
            group_dt.Columns.Add("ReportId");
            group_dt.Columns.Add("SubmissionStatus");
            group_dt.Columns.Add("SubmissionDeadline");
            group_dt.Columns.Add("ReportSummary");

            //行回数文ループ
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //行作成
                DataRow row;
                row = group_dt.NewRow();

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
                group_dt.Rows.Add(row);
            }

            group_dt.AcceptChanges();

            //データバインド（画面表示）
            this.GrdReport.DataSource = group_dt;
            this.GrdReport.DataBind();

        }
    }
}