<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Regular Expression Editor</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

A "regular expression" is a string based piece of code that allows to check if a
given text matches the expression or not. We can check a string (text) against
any regular expression combination like emails, numbers, zip codes etc
... Almost any Field Answertype - FieldT_Introduction.html - in Survey Project can be validated
against a regular expression created using the regular expression editor.<br />
<br />
To learn more about regular expressions visit the following site :<br />
<a href="http://www.regexlib.com" target="_blank">www.regexlib.com</a><br />
<br />

<u>Edit Regular Expression</u><br />
<br />
* Name - the name that the regular expression will have in Survey Project
  regular expression library.<br />
<br />
* Regular Expression - the regular expression that will validate the
  text entry.<br />
<br />
* Error Message - the error message that will be shown to the
  respondent if his entry didn't match the regular expression.<br />
<br />
<u>Test Regular Expression</u><br />
<br />
* Regular Expression - the regular expression to be tested.<br />
<br />
* Value To Test -the value to be tested against the regular
  expression.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Question%20Editor.html   <br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

