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
<h2 style="color:#5720C6;">Insert Security AddIn</h2><br />
<br />
<hr style="color:#e2e2e2;"/><br />
Following security addins can be added to the security pipe :
<br />
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
A security addin can only be added once in the security pipe.<br />
<br />
<br />
<hr style="color:#e2e2e2;"/><br />
<br />
<h3>More Information</h3><br />
<a href="Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />

<br />
           </td>
        </tr>
    </table>
</div></div></asp:Content>


