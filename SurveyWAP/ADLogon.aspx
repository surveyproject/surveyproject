<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADLogon.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.ADLogon" %>
<%@ Import Namespace="Votations.NSurvey.UserProvider" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SP AD Logon Testpage</title>
</head>
<body>
<form id="Login" method="post" runat="server">
      <asp:Label ID="Label1" Runat="server" >Domain:</asp:Label>
      <asp:TextBox ID="txtDomain" Runat="server" ></asp:TextBox><br />    
      <asp:Label ID="Label2" Runat="server" >Username:</asp:Label>
      <asp:TextBox ID="txtUsername" Runat="server" ></asp:TextBox><br />
      <asp:Label ID="Label3" Runat="server" >Password:</asp:Label>
      <asp:TextBox ID="txtPassword" Runat="server" TextMode="Password"></asp:TextBox><br />
      <asp:Button ID="btnLogin" Runat="server" Text="Login"></asp:Button><br />
      <asp:Label ID="errorLabel" Runat="server" ForeColor="#ff3300"></asp:Label><br />
      <asp:CheckBox ID="chkPersist" Runat="server" Text="Persist Cookie" />
    </form>
</body>
</html>
