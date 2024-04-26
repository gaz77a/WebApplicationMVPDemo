<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginView.aspx.cs" Inherits="WebApplicationMVP.Login.View.LoginView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Login</h2>
            <div>
                <asp:Label ID="Message" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div>
                <label for="Username">Username:</label>
                <asp:TextBox ID="Username" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="Password">Password:</label>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
