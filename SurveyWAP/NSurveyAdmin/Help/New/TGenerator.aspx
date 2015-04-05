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
                    Token Generator</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
Survey Project provides out of the box a tool to generate automatically, create
or import authentication tokens for a survey.    <br />
<br />
Token Generator<br />
<br />
* Number Of Tokens To Generate- is the number of token to be
  generated. Note that a token can only be once in the token list unless 
  its deleted after being created.<br />
<br />
* Token Type - is the type of token we want to generate.<br />
<br />
* Token Length is the length of the generated tokens for non GUID type
  token<br />
<br />
Add Single Token<br />
<br />
* Token Value the value of the token to store in the token database.<br />
<br />
* First Name first name of the identity linked to the token <br />
<br />
* Last Name last name of the identity linked to the token<br />
<br />
* Email email linked to the token<br />
<br />
If we are using the ED_Introduction.html Survey will automatically try to
lookup targets distribution email and try to match a token email
indentity to send out the matching token to the email.<br />
<br />
Import Tokens<br />
<br />
Let us import an existing list of tokens in the following format :<br />
TokenValue, FirstName, LastName, Email<br />
TokenValue, FirstName, LastName, Email<br />
TokenValue, FirstName, LastName, Email<br />
...<br />
<br />
If a token value is missing it will be replaced by GUID type token.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Token_Introduction.html<br />
Tk%20Status.html<br />
Token%20Protection.html<br />
Survey%20Security.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

