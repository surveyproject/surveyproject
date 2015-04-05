<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#KeyProvider" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Working of the Key Providers</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The key provider model determines the way unique
codes are generated in Survey. <br /><br />
                
Functions like save / resume progress or
answer changes require generating unique ids in order to allow the
respondent to restore the session in a secure way.<br />
<br />
By default Survey is generating a unique code that will be delivered to
the respondent if for example progress is saved during a
survey. But what happens if the respondent is invited using an email
invitation that already has a unique code for each email or the
respondent has already received a token?<br />
<br />
 <B>Currently SP has no option to choose between keyproviders.</B> Both unique code and emailcode
                are needed to resume and continue answering the survey.
          <br /><br />      
                That's were the key provider model comes in. In future editions it should be possible to specify
which security addin will take the responsability to provide and manage
the unique keys required to restore the respondent sessions and answers.<br />
<br />
The following security addin should act as a key provider for a survey :<br />
<br />
* EMail%20Code%20Protection.html<br />
* Active%20Directory%20Security%20Addin.html<br />
* Token%20Protection.html<br /><br />
               
Coming back to the email invitation example, if the
respondent should be allowed to resume a session later on using the same code than the one
that is used by email code protection the email code
protection security addin should be set to act as a key provider and the respondent
will have only one code to use for all action related to his session.<br />
<br />
Some providers like the Active Directory Security would go even a bit further
as they automatically detect if an Active Directory user has saved a
session and will resume it without having the user to enter any code.<br />
<br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Security.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

