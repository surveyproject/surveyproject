<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Introduction" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
        <br />
        <h2 style="color:#5720C6;">Technical Requirements</h2><br /><br />
        <hr style="color:#e2e2e2;" />
        <br />
       Survey&trade; Project v. 2.*  requires the following components:  <br />  <br />

<b>For End Users</b>  <br />  <br />

<i>Windows Operating Systems</i>  <br />
Windows XP (Pro)  <br />
Windows 2000  <br />
Windows Server 2003  <br />
Windows Vista  <br />
Windows 2008  <br />
Windows 7 or 8  <br />
Windows 2008 R2  <br />  <br />

<i>Web Server</i>  <br />
Microsoft IIS 5.0, 5.1, 6.0, 7.0, 7.5 or >  <br />  <br />

<i>Database Server</i>  <br />
Microsoft SQL Server 2008/ 2012  <br />
Microsoft SQL Server 2008/ 2012 Express  <br />  <br />

<i>.NET Framework</i> <br />
4.0/ 4.5  <br />  <br />

<i>Mailserver</i>  <br />
Smtp mailserver or mailserver account  <br />  <br />

<i>Webbrowsers</i>  <br />
MS Explorer 9 or ><br />
Google Chrome 17 or ><br />
Firefox 10 or ><br /><br />


<i>Recommend Components</i>  <br />
Windows Server 2008 R2  <br />
IIS 7.5  <br />
SQL Server 2012  <br />
.NET Framework 4.5  <br />  <br />


<b>For Developers </b> <br />  <br />

- .Net framework 4.* or higher  <br />
- IIS 6.* or higher  <br />
- MsSqlserver 2012 database  <br />
- Smtp mailserver account  <br />
- Windows Operating System  <br />
- MS Visual Web Developer 2012 Express  <br />
- SandCastle Helpfiles  <br />
- FxCop  <br />
- StyleCop  <br /><br />

                <b>Note:</b> requirements may change on every major or minor release without prior notice. In general the latest version of the open source or freely available tools for developers and endusers are used.

            </td>
        </tr>
    </table>
</div></div></asp:Content>
