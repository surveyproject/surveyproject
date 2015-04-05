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
                <h2 style="color:#5720C6;">
                    Mailing Status</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
The mailing status page presents an overview of all invitations sent by mail and their status.<br />
<br />
* Pending Emails - shows a list of emails that have been invited but
  didn't take the survey yet. It is not possible to send a
  reminder, In order to send a remind email delete the entry from the
  pending list and re-send an invitation through the Mailing menu - Survey%20Mailing.html.<br />
<br />
* Validated Emails - shows the list of emails that have responded to
  the survey. When clicking on the details button the voter report is shown containing the answers to the survey.<br />
<br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
ED_Introduction.html<br />
Survey%20Mailing.html<br />
Mailing%20Templates.html<br />
Mailing%20Log.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

