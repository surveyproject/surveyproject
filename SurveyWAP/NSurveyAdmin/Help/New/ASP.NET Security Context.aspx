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
ASP.NET Security Context</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
This security addin protects the survey based on the ASP.NET security
context. It will check the Context.User.Identity.Name object of ASP.NET
and store the value of it to prevent multiple submissions. <br /><br />
* Allow Multiple Submissions: enables if the same user is allowed to take the
  survey multiple times.<br />
<br />
Technical information on ASP.NET Security:<br /><br />
http://msdn.microsoft.com/en-us/library/ms972109.aspx <br />
http://msdn.microsoft.com/en-us/library/ff647405.aspx <br />
http://support.microsoft.com/kb/301240 <br /><br />

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

