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
                    Token Protection</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
This security addin will protect our survey using unique token generated
using the TGenerator.html.<br />
<br />
* Available Tokens is the number of token available for use. <br />
<br />
* Used Tokens is the number of token that have been already used. A
  token can only be used once. <br />
<br />
* Allow Access To Valid Tokens Only we can open the survey to
  respondent who don't have any token. If this option is activated
  respondent without token will be able to take the survey several times
  while respondent with token will only be able to submit once their
  answers.<br />
<br />
* Token Source Variable Name is the variable name from which Survey
  will look after in the querystring and session to see if a token has
  been set. If nothing is set or no value is found inside the variable an
  interface will be shown to the user to enter his token.<br />
<br />
  This feature is generally used along with the Emailing Invitation
  Features as you can include a link in your invitation that would have
  following parameters  :<br />
  <a href="http://www.mydomain.com/survey.aspx?surveyid=[--surveyid-" target="_blank">http://www.mydomain.com/survey.aspx?surveyid=[--surveyid-</a>]&amp;token=[--invit
  ationtoken-]<br />
<br />
  In this case the source variable name is &quot;token&quot;.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Security.html<br />
Token_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

