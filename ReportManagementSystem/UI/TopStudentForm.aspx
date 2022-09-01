<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopStudentForm.aspx.cs" Inherits="ReportManagementSystem.UI.TopStudentForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
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
                <asp:Image ID="IcnGroup" runat="server" ImageUrl="~/Image/グループ.png" />
                <br />
                <asp:Label ID="LblGroupName" runat="server"></asp:Label>
            </p>
        </div>
        <div>
            <asp:Button ID="BtnPersonalReport" runat="server" Text="個人課題" OnClick="BtnPersonalReport_Click" BackColor="Blue" ForeColor="White" />
            <asp:Button ID="BtnGroupReport" runat="server" Text="グループ課題" OnClick="BtnGroupReport_Click" BackColor="Blue" ForeColor="White" />
            <br />
            <asp:Label ID="LblExistErr" runat="server" ForeColor="Red" Text="現在登録されているレポートがありません"></asp:Label>
            <br />
        </div>
        <div>
            <asp:GridView ID="GrdReport" runat="server" AutoGenerateColumns="False" Height="100px" Width="473px" AllowPaging="true" PageSize="20">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("提出状態") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="提出期限" HeaderText="提出期限" />
                    <asp:HyperLinkField DataNavigateUrlFields="レポートID" DataNavigateUrlFormatString="~/UI/ReportDetailForm.aspx?ReportId={0}" HeaderText="レポート概要" DataTextField="レポート概要" />
                </Columns>
            </asp:GridView>

            <asp:ObjectDataSource ID="personal_dt" runat="server"></asp:ObjectDataSource>

        </div>
    </form>
</body>
</html>
