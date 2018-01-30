<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeepSessionAlive.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.KeepSessionAlive" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>SP&#8482; KeepSession Alive</title>

    <meta charset="UTF-8" />

    <meta name="application-name" content="Survey&trade; Project KeepAlive Webpage" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <meta name="DESCRIPTION" content="SurveyProject&trade;  is a free and open source survey and (data entry) forms webapplication for processing & gathering data online." />
    <meta name="KEYWORDS" content="surveyproject, survey, webform, questionnaire, nsurvey, w3devpro" />
    <meta name="COPYRIGHT" content=" 2017 &lt;href='http://www.w3devpro.com'>W3DevPro&lt;/a>" />
    <meta name="GENERATOR" content="SurveyProject&trade; " />
    <meta name="AUTHOR" content="W3DevPro" />

    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta name="ROBOTS" content="INDEX, FOLLOW" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />

<!-- refresh rate: content = seconds -->
<meta id="MetaRefresh" http-equiv="refresh" content="600;url=KeepSessionAlive.aspx" runat="server" />

            <link rel="SHORTCUT ICON" href="favicon.ico" type="image/x-icon" />

<script>
   window.status = "<%=WindowStatusText%>";
</script>

</head>
<body>
Test = <%=WindowStatusText%> 
    <br />

        List of Variables = 
    <br />
    <% 
        foreach (var crntsession in Session)
        {
            Response.Write(string.Concat(crntsession, "=", Session[crntsession.ToString()]) + "<br />");
        }
     %>
    <br /><br />
    <% foreach (var x in Request.ServerVariables)
        {
            Response.Write(string.Concat(x, " = ") + Request.ServerVariables[string.Concat(x)] + "<br />");
        }
%>

</body>
</html>
