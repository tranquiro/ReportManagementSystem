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
using System.Windows.Forms;

namespace ReportManagementSystem.UI
{
    public partial class TopAdministratorForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ユーザーアイコン、ユーザーラベルの表示
            IcnUser.Visible = true;
            LblUserName.Visible = true;
            LblUserName.Text = (string)Session["ユーザー名"] + "　様";

            //存在エラーラベルの非表示
            LblSearchErr.Visible = false;

            //レポート一覧グリッドビューの非表示
            GrdReportSummary.Visible = false;

            if (!Page.IsPostBack)
            {   
                //大学リストの表示内容設定
                DataTable univ_dt = DataBaseSql.GetUnibersityList((int)Session["ユーザーID"]);

                //大学リストの項目追加
                ListItem univList = new ListItem((string)univ_dt.Rows[0][1], "1");
                DdlUniv.Items.Add(univList);

                //講義リストの表示内容設定
                DataTable lec_dt = DataBaseSql.GetLectureList((int)univ_dt.Rows[0][0]);

                for (int i = 0; i < lec_dt.Rows.Count; i++)
                {
                    //講義リストの項目追加
                    ListItem lecList = new ListItem((string)lec_dt.Rows[i][1], Convert.ToString(lec_dt.Rows[i][0]));
                    DdlLec.Items.Add(lecList);
                }

            }

        }
        //ログアウトボタン押下時
        protected void LnkLogOut_Click(object sender, EventArgs e)
        {
            //Sessionに格納した情報を削除
            Session.RemoveAll();

            //画面遷移
            Response.Redirect("LoginForm.aspx");
        }

        //新規登録ボタン押下時
        protected void BtnNewRegistration_Click(object sender, EventArgs e)
        {
            //登録フラグとして0をSessionに格納する
            Session["登録フラグ"] = 0;

            //画面遷移
            Response.Redirect("NewReportForm.aspx");
        }

        //検索ボタン押下時
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            //レポート一覧グリッドビューの非表示
            GrdReportSummary.Visible = false;

            //データの抽出
            DataTable ReportData = DataBaseSql.GetReportData(Convert.ToInt32(DdlLec.SelectedValue));

            //データの存在チェック
            if(ReportData.Rows.Count == 0)
            {
                //検索エラーラベルの表示
                LblSearchErr.Visible = true;
                return;
            }

            //レポート一覧グリッドビューの表示
            GrdReportSummary.Visible = true;

            //レポート一覧グリッドビュー表示テーブル
            DataTable search_dt = new DataTable();

            search_dt.Columns.Add("レポートID");
            search_dt.Columns.Add("提出期限");
            search_dt.Columns.Add("レポート概要");
            search_dt.Columns.Add("課題種別");
            search_dt.Columns.Add("提出一覧");

            for (int i = 0; i < ReportData.Rows.Count; i++)
            {
                DataRow row;
                row = search_dt.NewRow();

                row["レポートID"] = Convert.ToInt32(ReportData.Rows[i][0]);
                row["提出期限"] = String.Format("{0:yyyy/MM/dd}", ReportData.Rows[i][1]);
                row["レポート概要"] = ReportData.Rows[i][2];

                if(Convert.ToInt32(ReportData.Rows[i][3]) == 0)
                {
                    row["課題種別"] = "個人";
                }
                else
                {
                    row["課題種別"] = "グループ";
                }

                row["提出一覧"] = "提出一覧";
                

                search_dt.Rows.Add(row);
            }

            search_dt.AcceptChanges();

            //データバインド（画面表示）
            this.GrdReportSummary.DataSource = search_dt;
            this.GrdReportSummary.DataBind();

        }

    }
}