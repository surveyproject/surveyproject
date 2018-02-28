<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Remote Server Deployment </h2><hr style="color:#e2e2e2;" />
                <br />
Once you have installed Survey using the MSI or RAW file installation
methods, it is pretty straightforward to install Survey on a remote
server or hosting provider.<br />
<br />
Make sure that your remote server or hosting provider has ASP.NET enabled
and that you can have access remotely to a SQL or MSDE database and a FTP
server. If you have any doubts we strongly suggest to contact your
service provider or local administrator prior to deploying Survey on your
remote server.<br />
<br />
Step 1 - Files Copy<br />

 <br />
Connect to your remote FTP server using any FTP client tool like for
example Windows Explorer or CuteFTP and browse to your local Survey
directory and drag and drop the files to the remote FTP server directory.<br />
<br />
Step 2 - SQL installation<br />
<br />
To run execute the SQL scripts that will configure your remote SQL Server
database you will need to do following steps.<br />
<br />
* Open a command prompt from the Start / Run menu and then type cmd<br />
* Once you are in the command prompt navigate to your local Survey
  directory and located the SQL directory<br />
* This directory contains the different scripts from Install to
  upgrades. If this is the first time you install Survey make sure to run
  the Install script first and then run all subsequent version upgrade
  scripts in order to reach the lastest version.<br />
* Type following command in the prompt to execute the required script
  (the osql.exe is provided as part of the SQL server or MSDE packages) :
  osql.exe -S SQLServerName -U YourUserName -P YourPassword -d
  YourDataBaseName -i SurveyScriptToExecute.sql<br />
Do not forget to change also the Survey Connection String settings inside
the web.config so that Survey can connect to the SQL server. If you have
troubles connecting to your remote SQL server we suggest to contact your
hosting provider to get their latest connection procedures and settings.<br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

