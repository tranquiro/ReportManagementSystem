using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ReportManagementSystem.DataBase;

namespace ReportManagementSystem.UI
{
    //レポート詳細画面
    public partial class ReportDetailForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int reportId = Convert.ToInt32(Request.QueryString["ReportId"]);

            //レポート詳細情報取得
            DataTable data = DataBaseSql.GetReportDetailData(reportId);

            //個人課題の時
            if(Convert.ToInt32(data.Rows[0][0]) == 0)
            {
                IcnGroup.Visible = false;
                LblGroupName.Visible = false;
                LblUserName.Text = (string)Session["UserName"] + "　様";
            }

            //グループ課題の時
            if (Convert.ToInt32(data.Rows[0][0]) == 1)
            {
                IcnUser.Visible = false;
                LblUserName.Visible = false;
                LblGroupName.Text = (string)Session["GroupName"] +"グループ";
            }

            //提出期限日の表示
            DeadLine.Text = Convert.ToString(String.Format("{0:yyyy/MM/dd}", data.Rows[0][1]));

            //レポート概要、レポート内容の表示
            ReportSummary.Text = Convert.ToString(data.Rows[0][2]);
            ReportContent.Text = Convert.ToString(data.Rows[0][3]);

            //表示項目設定
            //学生の場合
            if(Convert.ToInt32(Session["AdaministratorFlag"]) == 0)
            {
                BtnAcquisition.Visible = true;
                LblSubmitReport.Visible = true;
                SubmitReport.Visible = true;
                BtnSubmit.Visible = true;

                //更新ボタン非表示
                BtnUpdate.Visible = false;
            }

            //管理者の場合
            if (Convert.ToInt32(Session["AdaministratorFlag"]) == 1)
            {
                BtnAcquisition.Visible = false;
                LblSubmitReport.Visible = false;
                SubmitReport.Visible = false;
                BtnSubmit.Visible = false;

                //更新ボタン表示
                BtnUpdate.Visible = true;
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
    }
}