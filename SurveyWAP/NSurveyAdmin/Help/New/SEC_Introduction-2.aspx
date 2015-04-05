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
                    Survey Security</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Survey provides with us with a very rich addin system in order to protect
our surveys.<br />
<br />
Security in Survey is based on a &quot;security pipe&quot;. You can put as many
security addin as you want in the pipe. The respondent will only reach
the survey once he has been authentified by all the security addins that
have been put in the &quot;security pipe&quot; using the Survey%20Security.html
page.<br />
<br />
As you can see in the security pipe architecture, we could easily add our
own security addin using Survey's SDK to match our enterprise's security
system.<br />
<br />
Following security addins are provided out of the box to build our
security pipe :<br />
<br />
* IP%20Protection.html<br />
* Cookie%20Protection.html<br />
* Pass%20Protection.html<br />
* EMail%20Code%20Protection.html<br />
* Token%20Protection.html   <br />
* ASP.NET%20Security%20Context.html<br />
* Survey%20Security%20Context.html<br />
* Active%20Directory%20Security%20Addin.html<br />
* Entry%20Number%20Limitation.html<br />
* Image%20Password.html<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Security.html<br />
Insert%20Security%20AddIn.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

