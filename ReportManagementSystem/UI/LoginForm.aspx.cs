using System;
using System.Data;
using ReportManagementSystem.DataBase;

namespace ReportManagementSystem
{
    //ログイン画面
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ユーザーIDエラーラベル、パスワードエラーラベルの非表示
            LblUserIdErr.Visible = false;
            LblPasswordErr.Visible = false;
        }

        //ログインボタン押下時
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            //エラーカウント
            int errCnt = 0;

            //ユーザーIDが未入力の場合
            if (TxtUserId.Text == "")
            {
                LblUserIdErr.Visible = true;
                LblUserIdErr.Text = "ユーザーIDが未入力です";
                errCnt += 1;
            }

            //パスワードが未入力の場合
            if (TxtPassword.Text == "")
            {
                LblPasswordErr.Visible = true;
                LblPasswordErr.Text = "パスワードが未入力です";
                errCnt += 1;
            }

            //エラーチェック
            if (errCnt >= 1)
            {
                return;
            }

            //ユーザーID（メールアドレス）からデータ検索

            DataTable dt = DataBaseSql.LoginSetting(TxtUserId.Text);
           

            //検索結果が0件の場合
            if(dt.Rows.Count == 0)
            {
                LblUserIdErr.Visible = true;
                LblUserIdErr.Text = "ユーザーIDが存在しません";
                return;
            }

            //パスワード判定
            string pass = (string)dt.Rows[0][3];

            if(pass != TxtPassword.Text)
            {
                LblPasswordErr.Visible = true;
                LblPasswordErr.Text = "パスワードが間違っています";
                return;
            }

            //Sessionデータ保存
            Session["UserId"] = dt.Rows[0][0];
            Session["UserName"] = dt.Rows[0][1];
            Session["AdaministratorFlag"] = dt.Rows[0][2];
            Session["GroupId"] = dt.Rows[0][4];
            Session["GroupName"] = dt.Rows[0][5];


            //画面遷移
            //管理者フラグが0の場合
            if(Convert.ToInt32(dt.Rows[0][2]) == 0)
            {
                //学生用画面
                Response.Redirect("TopStudentForm.aspx");
            }
            else
            {
                //管理者用画面
                Response.Redirect("TopAdministratorForm.aspx");
            }
        }

        //パスワード初期化リンク押下時
        protected void LnkBtnPassword_Click(object sender, EventArgs e)
        {
            //画面遷移
            Response.Redirect("InitializationForm.aspx");
        }
    }
}