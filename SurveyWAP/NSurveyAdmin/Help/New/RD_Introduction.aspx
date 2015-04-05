<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Report Deployment</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Survey provides several ways to deploy our report to our customers.<br />
<br />
* Report%20Link%20Deployment.html  provides us with a html link to our
  report. We can then use this link anywhere from emails, messenger or to
  our website html pages.<br />
<br />
* Web%20Control%20Deployment.html is for more experienced users who
  like to customize the survey ASP.net control and integrate it inside
  their ASP.net pages.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Report%20Link%20Deployment.html<br />
Web%20Control%20Deployment.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

