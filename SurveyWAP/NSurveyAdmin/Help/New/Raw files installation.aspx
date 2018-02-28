<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Raw Files Installation </h2><hr style="color:#e2e2e2;" />
                <br />

Raw files are designed for people who whish to have the maximum of
flexibility during their installation process. The installation files are
delivered inside a single compressed ZIP file and require you to
configure Internet Information Services and SQL Server manually.<br />
<br />
Step 1 - Web Server Installation<br />
<br />
Unzip the compressed ZIP file inside a directory of your choice, for
example Survey. Once you have unzipped the directory you will need to
configure it as a virtual directory for Internet Information Services.<br />
<br />
To do this part right click on the directory and select its properties
and under Web sharing choose share this folder, keep the default
settings, click ok and then apply the changes.<br />
<br />
Once you have finished these steps Internet Information Services will be
configured with this new virtual directory and should be able to access
it using your browser and URL : <a href="http://yourserver/yourdirectory" target="_blank">http://yourserver/yourdirectory</a><br />
<br />
Step 2 - SQL Server Installation<br />
<br />
The last step is to configure your SQL server database. To run execute
the SQL scripts that will configure your remote SQL Server database you
will need to do following steps.<br />
<br />
* Open a command prompt from the Start / Run menu and then type cmd
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
the web.config so that Survey can connect to the SQL server.<br />
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

