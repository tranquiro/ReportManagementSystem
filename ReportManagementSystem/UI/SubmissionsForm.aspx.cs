using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReportManagementSystem.UI
{
    public partial class SubmissionsForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int reportId = Convert.ToInt32(Request.QueryString["ReportId"]);
            Label1.Text = "レポート"+reportId + "の一覧画面です。";
        }
    }
}