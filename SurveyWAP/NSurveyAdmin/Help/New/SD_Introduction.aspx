<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Campaigns" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Survey Campaigns</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
Survey Project provides several ways to publish a survey to respondents.<br />
<br />
* The Campaigns/Web menu - Survey%20Deployement.html -  provides a HTML link (URL) to the
  survey. This link can be distibuted through all sort of media from emails, messenger or on website pages.<br />
<br />

* The Campaigns/ Take Survey menuoption - Take%20Survey.html - presents a special page inside Survey Project's administration
  that is meant to allow a registered user -  User%20Manager.html - to take a
  survey once logged in to the SP tool. This page requires the user to
  have the &quot;take survey&quot; right set for a given Role -  Roles%20Manager.html .<br />
<br />
* The Survey Webcontrol - Web%20Control%20Deployment.html - is for more experienced users who
  want to customize the survey ASP.net control and integrate it into ASP.net pages of a separate website.<br />
<br />
* ED_Introduction.html - Survey Project provides a complete mailing tool to email invitations including survey links to respondents.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Popup%20Window.html<br />
Survey%20Deployement.html<br />
Take%20Survey.html<br />
Web%20Control%20Deployment.html<br />
ED_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

