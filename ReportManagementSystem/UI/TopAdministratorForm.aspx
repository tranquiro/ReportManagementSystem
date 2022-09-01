<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopAdministratorForm.aspx.cs" Inherits="ReportManagementSystem.UI.TopAdministratorForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body style="height: 195px; width: 1239px">
    <form id="form1" runat="server">
        <div style="text-align: right;">
            <!-- ログアウト確認表示 -->
            <asp:LinkButton ID="LnkLogOut" runat="server" OnClick="LnkLogOut_Click" OnClientClick="return confirm('ログアウトしますか？');">ログアウト</asp:LinkButton>
        </div>
        <div>
            <asp:Label ID="LblTitle" runat="server" Text="レポート提出状況"></asp:Label>
        </div>
        <div>
            <p style="text-align: right;">
                <asp:Image ID="IcnUser" runat="server" ImageUrl="~/Image/ユーザーID.png" />
                <br />
                <asp:Label ID="LblUserName" runat="server"></asp:Label>
            </p>
            <p style="text-align: right;">
                <asp:Button ID="BtnNewRegistration" runat="server" Text="新規登録" OnClick="BtnNewRegistration_Click" BackColor="Blue" ForeColor="White" />
            </p>
        </div>

        <div>
            <p>
                <asp:Label ID="LblUnivLst" runat="server" Text="大学："></asp:Label>
                <asp:DropDownList ID="DdlUniv" runat="server"></asp:DropDownList>
                <asp:Label ID="LblLecLst" runat="server" EnableTheming="True" Text="講義："></asp:Label>
                <asp:DropDownList ID="DdlLec" runat="server"></asp:DropDownList>
                <asp:Button ID="BtnSearch" runat="server" Text="検索" OnClick="BtnSearch_Click" BackColor="Blue" ForeColor="White" />
            </p>
        </div>

        <div>
            <p>
                <asp:Label ID="LblSearchErr" runat="server" ForeColor="Red" Text="検索結果は0件です。条件を変えて再検索をしてください"></asp:Label>
            </p>
        </div>


        <asp:GridView ID="GrdReportSummary" runat="server" AutoGenerateColumns="False" AllowPaging ="True" PageSize ="20" CellPadding="4">
            <Columns>
                <asp:BoundField DataField="レポートID" HeaderText="レポートID" Visible="False" />
                <asp:BoundField DataField="提出期限" HeaderText="提出期限" />
                <asp:HyperLinkField DataNavigateUrlFields="レポートID" DataNavigateUrlFormatString="~/UI/ReportDetailForm.aspx?ReportId={0}" DataTextField="レポート概要" HeaderText="レポート概要" />
                <asp:BoundField DataField="課題種別" HeaderText="課題種別" />
                <asp:HyperLinkField DataNavigateUrlFields="レポートID" DataNavigateUrlFormatString="~/UI/SubmissionsForm.aspx?ReportId={0}" DataTextField="提出一覧" HeaderText="提出一覧" />
            </Columns>
            <RowStyle HorizontalAlign ="Center" />
        </asp:GridView>

    </form>
</body>
</html>
