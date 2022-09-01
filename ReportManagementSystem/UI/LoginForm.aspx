<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="ReportManagementSystem.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/タイトル.png" />
            <asp:Label ID="LblTitle" runat="server" Text="レポート管理システム"></asp:Label>
        </div>
        <div>

            <br />
            <asp:Label ID="LblLogin" runat="server" Text="ログイン"></asp:Label>

        </div>
        <div>

            <asp:Image ID="IcnUserId" runat="server" ImageUrl="~/Image/ユーザーID.png" />
            <asp:TextBox ID="TxtUserId" Placeholder ="ユーザーID（Email）" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LblUserIdErr" runat="server" ForeColor="Red" Text="ユーザーIDが未入力です"></asp:Label>
        
        </div>
        <div>

            <asp:Image ID="IcnPassword" runat="server" ImageUrl="~/Image/パスワード.png" />
            <asp:TextBox ID="TxtPassword" Placeholder ="パスワード" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LblPasswordErr" runat="server" ForeColor="Red" Text="パスワードが未入力です"></asp:Label>
        
        </div>

        <div style ="margin-left:300px">
            <br />
            <asp:Button ID="BtnLogin" runat="server" BackColor="Blue" ForeColor="White" Text="ログイン" OnClick="BtnLogin_Click" style="margin-left: 0px" />

        </div>
        <div style ="margin-left:50px">
            <br />
            <asp:LinkButton ID="LnkBtnPassword" runat="server">パスワードをお忘れですか？</asp:LinkButton>

        </div>

    </form>
</body>
</html>
