using System;
using System.Data;
using ReportManagementSystem.DataBase;
using ReportManagementSystem.CommonLogic;

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

            //表示データ作成
            DataTable returndt = ReportViewCreate.GetDataTable(dt);

            //データバインド（画面表示）
            this.GrdReport.DataSource = returndt;
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

            //表示データ作成
            DataTable returndt = ReportViewCreate.GetDataTable(dt);

            //データバインド（画面表示）
            this.GrdReport.DataSource = returndt;
            this.GrdReport.DataBind();

        }
    }
}