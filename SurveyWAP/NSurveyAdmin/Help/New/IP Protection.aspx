<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    IP Protection</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This security addin protects the survey against multiple submissions
by recording the IP of the respondent and disallow taking the
survey a second time after submitting the answers.<br /><br />
* IP Expires After: the number of minutes after a respondent IP that
  was recorded will be allowed to submit answers again . <br />
<br />

If the respondents are behind firewalls or proxies respondents may
have the same IP address. SP tries to get the real IP but
depending on the mode activated on the firewalls or proxies it is
sometime not possible to get a unique IP per respondent.<br />
<br />

<br />

<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                Survey%20Security.html<br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

