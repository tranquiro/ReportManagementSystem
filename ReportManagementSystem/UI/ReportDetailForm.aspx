<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportDetailForm.aspx.cs" Inherits="ReportManagementSystem.UI.ReportDetailForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right;">
            <!-- ログアウト確認表示 -->
            <asp:LinkButton ID="LnkLogOut" runat="server" OnClick="LnkLogOut_Click" OnClientClick="return confirm('ログアウトしますか？');">ログアウト</asp:LinkButton>
        </div>
        <div>
            <asp:Label ID="LblTitle" runat="server" Text="レポート詳細"></asp:Label>
        </div>
        <div>
            <p style="text-align: right;">
                <asp:Image ID="IcnUser" runat="server" ImageUrl="~/Image/ユーザーID.png" />
                <br />
                <asp:Label ID="LblUserName" runat="server"></asp:Label>
            </p>
            <p style="text-align: right;">
                <asp:Image ID="IcnGroup" runat="server" ImageUrl="~/Image/グループ.png" />
                <br />
                <asp:Label ID="LblGroupName" runat="server"></asp:Label>
            </p>
            <p style="text-align: right;">
                <asp:Label ID="LblDeadLine" runat="server" Text ="提出期限"></asp:Label>
                <br />
                <asp:Label ID="DeadLine" runat="server"></asp:Label>
            </p>
        </div>
        <asp:Label ID="LblReportSummary" runat="server" Text="＜レポート概要＞"></asp:Label>
        <p>
            <asp:Label ID="ReportSummary" runat="server"></asp:Label>
        </p>
        <p style =" text-align:right">
            <asp:Button ID="BtnAcquisition" runat="server" Text="取得" />
        </p>
        <p>
            <asp:Label ID="LblReportContent" runat="server" Text="＜レポート内容＞"></asp:Label>
        </p>
        <p>
            <asp:Label ID="ReportContent" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="LblSubmitReport" runat="server" Text="＜提出レポート＞"></asp:Label>
        </p>
        <p>
            <asp:Label ID="SubmitReport" runat="server"></asp:Label>
        </p>
        <p style =" text-align:right">
            <asp:Button ID="BtnSubmit" runat="server" Text="提出" />
        </p>
        <p style =" text-align:right">
            <asp:Button ID="BtnUpdate" runat="server" Text="更新" />
        </p>
        <p style =" text-align:center">
            <asp:Button ID="BtnReturnTop" runat="server" Text="戻る" />
        </p>
    </form>
    
</body>
</html>
