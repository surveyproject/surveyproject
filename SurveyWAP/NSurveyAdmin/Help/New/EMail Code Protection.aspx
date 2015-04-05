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
                	Email Code Protection</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This security addin protects the survey using the email invitation
code provided to the respondent when receiving the Email Distribution message.<br />
<br />
* Only Invited Emails Can Participate: option to open the survey to
  respondents who did not receive any invitation code. <br /><br />
                
                If this option is activated respondents without code will be able to take the survey
  several times (if they receive the proper link) while respondent with the invitation code will only be
  able to submit their answers once. <br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Security.html<br />
ED_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

