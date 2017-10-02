<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Introduction" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
        <h2 style="color:#5720C6;">Technical Requirements</h2>
        <hr style="color:#e2e2e2;" />
 
       One of the goals of the project is to make use of the latest version of freely available and/ or open source software to build and run SP&trade;.&nbsp;For both regular use (administration tool and answering surveys) and development SP&trade; tries to keep up to date with the latest developments.<br />
<br />
To make use of the SP&trade; webapplication check the general list of requirements below.<br />
<br />
New or adjusted requirements for a particular version of SP&trade; are described in the Readme.txt files of an installation package and on the downloads page at <a href="https://github.com/surveyproject" target="_blank">Github</a> or Codeplex.<br />
<br />
<br />
<strong>Regular Use</strong><br />
<br />
<em>SP&trade; webapplication</em><br />
<br />
<span style="text-decoration: underline;">Hardware</span><br />
Server, PC or Laptop<br />
<br />
<span style="text-decoration: underline;">Windows Operating Systems</span><br />
- client or server OS<br />
<br />
<span style="text-decoration: underline;">Web Server</span><br />
- Microsoft IIS&nbsp;<br />
- .NET Framework<br />
<br />
<span style="text-decoration: underline;">Database Server</span><br />
- Microsoft SQLserver (Express or Enterprise version)<br />
<br />
<span style="text-decoration: underline;">Mailserver</span><br />
Smtp mailserver or mail account<br />
<br />
<span style="text-decoration: underline;">Webbrowser</span><br />
- tested on: Chrome, Firefox, MSExplorer/Edge<br />
<br />
<br />
<em>SP&trade; Surveys &amp; Webforms</em><br />
<br />
<span style="text-decoration: underline;">Webbrowser</span><br />
<span>- tested on: Chrome, Firefox, MSExplorer/Edge</span><br />
<br />
<span style="text-decoration: underline;">Internetconnection</span><br />
- limited options only for offline answering of surveys<br />
<br />
<span style="text-decoration: underline;">Device</span><br />
- PC/ Laptop/ Tablet/ Mobile<br />
- Older mobile devices not supported, new(er) devices may experience (caching) issues in case of limited resources (memory, diskspace etc.)<br />
<br />
<br />
<strong>Development</strong><br />
<br />
- PC, Laptop or Server<br />
<span>- Windows Operating System</span><br />
- .Net framework<br />
- IIS webserver<br />
- MsSqlserver database<br />
- Smtp mailserver account<br />
<br />
- MS Visual Studio (Community Edition)<br />
<br />
Note: <a href="http://www.asp.net/downloads" target="_blank" title="Developer Downloads">Click here for free MS web development tools downloads</a> suited to work on the SP&trade; webapplication.<br />
<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
