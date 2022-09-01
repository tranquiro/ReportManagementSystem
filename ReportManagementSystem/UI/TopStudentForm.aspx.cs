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
    public partial class TopStudentForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ユーザーアイコン・ユーザーラベルの表示
            IcnUser.Visible = true;
            LblUserName.Visible = true;
            LblUserName.Text = (string)Session["ユーザー名"] + "　様";

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
            LblUserName.Text = (string)Session["ユーザー名"] + "　様";

            //グループアイコン・グループラベルの非表示
            IcnGroup.Visible = false;
            LblGroupName.Visible = false;

            //存在エラーラベルの非表示
            LblExistErr.Visible = false;

            //レポート一覧グリッドビューの表示
            GrdReport.Visible = true;

            //レポート提出状況のチェック
            DataTable dt = DataBaseSql.GetPersonalReport((int)Session["ユーザーID"]);
            
            //検索結果が0件の場合
            if (dt.Rows.Count == 0)
            {
                //存在エラーラベル表示
                LblExistErr.Visible = true;
                return;
            }

            //レポート提出状況表示テーブル
            DataTable personal_dt = new DataTable();

            personal_dt.Columns.Add("レポートID");
            personal_dt.Columns.Add("提出状態");
            personal_dt.Columns.Add("提出期限");
            personal_dt.Columns.Add("レポート概要");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row;
                row = personal_dt.NewRow();

                row["レポートID"] = dt.Rows[i][0];

                if (dt.Rows[i][1].Equals(DBNull.Value))
                {
                    row["提出状態"] = "~/Image/未提出.png";
                }
                else
                {
                    row["提出状態"] = "~/Image/提出.png";
                }

                row["提出期限"] = String.Format("{0:yyyy/MM/dd}", dt.Rows[i][2]);
                row["レポート概要"] = dt.Rows[i][3];
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
            LblGroupName.Text = (string)Session["グループ名"];

            //存在エラーラベルの非表示
            LblExistErr.Visible = false;

            //レポート一覧グリッドビューの表示
            GrdReport.Visible = true;

            //レポート提出状況のチェック
            DataTable dt = DataBaseSql.GetGroupReport((int)Session["グループID"]);

            //検索結果が0件の場合
            if (dt.Rows.Count == 0)
            {
                //存在エラーラベル表示
                LblExistErr.Visible = true;
                return;
            }

            //レポート提出状況表示テーブル
            DataTable group_dt = new DataTable();

            group_dt.Columns.Add("レポートID");
            group_dt.Columns.Add("提出状態");
            group_dt.Columns.Add("提出期限");
            group_dt.Columns.Add("レポート概要");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row;
                row = group_dt.NewRow();

                row["レポートID"] = dt.Rows[i][0];


                if (dt.Rows[i][1].Equals(DBNull.Value))
                {
                    row["提出状態"] = "~/Image/未提出.png";
                }
                else
                {
                    row["提出状態"] = "~/Image/提出.png";
                }

                row["提出期限"] = String.Format("{0:yyyy/MM/dd}", dt.Rows[i][2]);
                row["レポート概要"] = dt.Rows[i][3];
                group_dt.Rows.Add(row);
            }

            group_dt.AcceptChanges();

            //データバインド（画面表示）
            this.GrdReport.DataSource = group_dt;
            this.GrdReport.DataBind();

        }
    }
}