<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeepSessionAlive.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.KeepSessionAlive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SP KeepSession Alive</title>

<meta id="MetaRefresh" http-equiv="refresh" content="21600;url=KeepSessionAlive.aspx" runat="server" />

<script>
   window.status = "<%=WindowStatusText%>";
</script>

</head>
<body>
Test = <%=WindowStatusText%> <br /><br />
    List of Variables = 
    <br />
    <% 
            foreach (var crntsession in Session)
            {
                Response.Write(string.Concat(crntsession, "=", Session[crntsession.ToString()]) + "<br />");
            }                    
     %>

</body>
</html>
