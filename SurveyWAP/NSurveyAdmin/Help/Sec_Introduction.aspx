<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Security" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
<br />
<h2 style="color:#5720C6;">Survey Security Introduction</h2><br />
<br />
<hr style="color:#e2e2e2;"/><br />
Survey offers an extensive addin security system with different options to protect access to surveys.<br />
<br />
Survey security is build around a so called &quot;security pipe&quot;. As many
security addins as needed can be added to the "pipe". The respondent will only have access to 
the survey once they have been authorised by all the security addins that
have been put in the &quot;security pipe&quot; using the Surveys/ Security menuoption.<br />
<br />

The following security addins are provided to build a security pipe :<br />
<br />
* IP Protection<br />
* IP Filters<br />
* Cookie Protection<br />
* Password Protection<br />
* EMail Code Protection<br />
* Token Protection   <br />
* ASP.NET Security Context<br />
* Survey Security Context<br />
* Entry Number Limitation<br />
<br />
<br />
<hr style="color:#e2e2e2;"/><br />
<br />
<h3>More Information</h3><br />

<a href="Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="Insert Security AddIn.aspx" title="Insert Security Addin">Insert Security Addin</a><br />
<br />
<br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

