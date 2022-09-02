<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="ReportManagementSystem.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        #title {
            display:flex;
            align-items: center; /* 垂直方向 */
        }

        .item{
            display:flex;
        }

        #login{
            margin-left:250px;
        }

        #forget{
            margin-left: 50px;
            margin-top:15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id ="title">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/Title.png" />
            <asp:Label ID="LblTitle" runat="server" Text="レポート管理システム" Font-Size="Larger"></asp:Label>
        </div>

        <p>
            <asp:Label ID="LblLogin" runat="server" Text="ログイン"></asp:Label>
        </p>

        <div class ="item">
            <asp:Image ID="IcnUserId" runat="server" ImageUrl="~/Image/Userid.png" Height="40px" Width="40px" />
            <asp:TextBox ID="TxtUserId" Placeholder="ユーザーID（Email）" runat="server"></asp:TextBox>
        </div>

        <p>
            <asp:Label ID="LblUserIdErr" runat="server" ForeColor="Red" Text="ユーザーIDが未入力です"></asp:Label>
        </p>

        <div class ="item">
            <asp:Image ID="IcnPassword" runat="server" ImageUrl="~/Image/Password.png" Height="40px" Width="40px" />
            <asp:TextBox ID="TxtPassword" TextMode="Password" Placeholder="パスワード" runat="server"></asp:TextBox>
        </div>

        <p>
            <asp:Label ID="LblPasswordErr" runat="server" ForeColor="Red" Text="パスワードが未入力です"></asp:Label>
        </p>

        <div id="login">
            <asp:Button ID="BtnLogin" runat="server" BackColor="Blue" ForeColor="White" Text="ログイン" OnClick="BtnLogin_Click" Style="margin-left: 0px" />
        </div>

        <div id="forget">
            <asp:LinkButton ID="LnkBtnPassword" runat="server" OnClick="LnkBtnPassword_Click">パスワードをお忘れですか？</asp:LinkButton>
        </div>

    </form>
</body>
</html>
