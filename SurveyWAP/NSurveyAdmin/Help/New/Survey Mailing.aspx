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
                    Invitation Mailing</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Usethe invitation mailing to send invitations to the respondents
to take a survey.<br />
<br />
* Email Invitation List - is a comma separated list of emails to which
  you want to send the invitation.<br />
<br />
* From Email - is the email from which the invitation will originate.<br />
<br />
* From Name - is the name of the person sending the invitation.<br />
<br />
* Invitation Message - this is the invitation message, a special optional
  tag that will be replaced while sending the invitation can be included
  in the invitation:<br />
<br />
  [--invitationid-] this tag will be replaced by a unique identifier that
  will be used by - EMail%20Code%20Protection.html- the email security addin to
  uniquely identify a user to avoid multiple submission. Note that the email code protection - 
  EMail%20Code%20Protection.html - must be activated in order to avoid
  multiple submissions.<br />
<br />

Anonymous Entries Survey - if checked SP will not track a link between the email and the
respondent answers.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
ED_Introduction.html<br />
Mailing%20Templates.html<br />
Mailing%20Status.html<br />
Mailing%20Log.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

