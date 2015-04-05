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
                    Cookie Protection</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />    

This security addin protects the survey against multiple submissions
by setting a cookie on the client's browser.<br /><br />
* Cookie Expires After is the number of minutes after a cookie will
  expire and the respondent will be allowed to submit answers again.
<br />
<br />
There is no way to prevent the respondent from clearing its cookies once
the survey is taken after which multiple submissions are still possible.<br />
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

