<%@ Page language="c#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Security.Principal" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SP AD Start Testpage</title>
</head>
  <body>
    <form id="Form1" method="post" runat="server">
      <asp:Label ID="lblName" Runat="server" /><br />
      <asp:Label ID="lblAuthType" Runat="server" />
    </form>
  </body>
</html>
<script runat="server">
void Page_Load(Object sender, EventArgs e)
{
  lblName.Text = "Hello " + Context.User.Identity.Name + ".";
  lblAuthType.Text = "You were authenticated using " +   Context.User.Identity.AuthenticationType + ".";
}
</script>