<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Email" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">Email Distribution</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
<br />
Survey Project provides a full mail distribution interface that allows sending
email invitations to take a survey.<br />
<br />
Once the invitations are sent it is possible to track who did
answer to it or not, including who answered which answers and using the email code security - 
EMail%20Code%20Protection.html - addin to protect the survey against multiple
submissions.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Mailing.html<br />
Mailing%20Templates.html<br />
Mailing%20Status.html<br />
Mailing Log<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

