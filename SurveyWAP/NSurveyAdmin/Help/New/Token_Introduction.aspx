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
                    Token Security</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
Token security will allow us to create unique tokens and protect our
survey using these unique tokens. Token security is the most secure
method to prevent multiple submissions as it is only possible to take the
survey if we have a valid token.<br />
<br />
Another advantage is that we can link a user identity for each token
created, this way we know exactly who did take or didn't take the survey
and as token is just a text we can distribute them using any electronic
or non electronic media we want from standard mail, phone to emails.<br />
<br />
Token security has already been used on major projects to conduct large
ballots with great success.<br />
<br />


                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
TGenerator.html<br />
Tk%20Status.html<br />
Token%20Protection.html<br />
Survey%20Security.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
