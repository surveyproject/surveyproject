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
                    MSI Installation </h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />




The MSI installation is the easiest way to install Survey on a computer<br />
(pc, laptop or server) if you have local access. It will guide you<br />
through the installation proces automatically. You cannot install Survey<br />
from your local machine to a remote server using the MSI. Go to<br />
Remote%20Server%20Deployment.html for further instructions.<br />
<br />
In order to use the MSI installer you must first make sure that you have<br />
an installed and working copy of SQL Server or MSDE and activated the<br />
Internet Information Services through the Add/Remove Windows Components<br />
options of the Windows Add or Remove Programs control panel.<br />
<br />
Once you have a setup of SQL server and a Web server, make sure that<br />
ASP.NET is configured and installed on your machine. If you receive the<br />
sources of the aspx files as clear text in your browser once you've<br />
installed Survey then you will need to install or re-install ASP.NET on<br />
your machine.<br />
<br />
If everything is configured correctly you may now run the MSI installer.<br />
The installer will check for itself and let you know if anything is<br />
missing.<br />
<br />
Step 1 - Starting the Installation<br />
<br />
After you have downloaded the Survey.msi file from the Survey project<br />
Website and saved it to your computer, the first step is to double click<br />
the survey.msi file. This will start the automated installation proces.<br />
It's first check will be to see if you've got the .Net framework version<br />
2.* or higher installed. Next you will see the Welcoming screen. Click<br />
Next to continue.<br />
Step 2 - Checking Requirements<br />
<br />
Several checks are executed. The results are reported to you through the<br />
requirements screen. Different operating systems will trigger different<br />
checks:<br />
<br />
- Administrator privileges ared needed to run the installation<br />
- checkIIS: checks for IIS, at least version 5 and above<br />
- CheckIIS7Modules: check if IIS7 has required modules<br />
- checkIIS7AndFastCgi: checks if IIS7 is installed with the FastCGI<br />
global module; checks if webserver role CGI feature is installed<br />
- checkASPNet: checks for an ASP.NET 2.0 installation<br />
- checkAJAX: checks for the ASP.NET Ajax Extension<br />
- checkIUSR: checks if the IUSR or IUSR_Computername/the internet<br />
guestaccount exists and determines it's name<br />
- checkSQLSERVER: SoftCheck for SQLSERVER and tries to determine<br />
installed instances<br />
<br />
Step 3 - License Agreement<br />
<br />
SURVEY 1.0.0 is a web survey and form engine based on the open source<br />
webapplication formerly known as NSurvey 1.6. NSurvey was written by<br />
Thomas Zumbrunn, copyright (c) 2004 and published under the GNU GENERAL<br />
PUBLIC LICENSE Version 2, June 1991. The new SURVEY project was started<br />
by Fryslan Webservices, copyright (c) 2009. The SURVEY open source<br />
webproject can be found at <a href="http://survey.codeplex.com" target="_blank">http://survey.codeplex.com</a>. You can freely use<br />
the Survey application once the licensing terms are accepted.<br />
Step 4 - Disk Space Requirements<br />
<br />
While the installation wizard is running in the background it determines<br />
the amount of free space available on your computer versus the amount<br />
needed to install the webapplication. In certain situations because of<br />
this proces the Disk Space Requirements screen may show up. Please do not<br />
wait for any results but click the Return button and follow steps 5 and 6<br />
after which you should restart the wizard and try once more.<br />
Step 5 - Cancel the Installation<br />
<br />
Once you've clicked the Return button at step 5 you will return to the<br />
License Agreement Screen. If the Next button is still not available,<br />
click the Cancel button to interrupt the installation and restart the<br />
Survey.msi file.<br />
Step 6 - Interrupt and Exit<br />
<br />
After you have clicked the Cancel button on the License Agreement screen<br />
and have confirmed the cancellation the Exit screen will appear. Click<br />
the Finish button to leave the installation. Restart the installation<br />
proces by double clicking the Survey.msi install file.<br />
Step 7 - Create Website<br />
<br />
This is where you set up your website in IIS. There are two options: a.<br />
create an entirely new website with its own ip and/or maindomainname<br />
(i.e. <a href="www.survey.org" target="_blank">www.survey.org</a>); b. create a new virtual directory in an already<br />
existing website (i.e. <a href="www.survey.org/survey" target="_blank">www.survey.org/survey</a>). Go to step 8. to create a<br />
new virtual directory. If you want to create a new website just follow<br />
the instructions of the wizard.<br />
Step 8 - Create Virtual Directory<br />
<br />
The list of existing (default) websites is generated by the wizard. In<br />
case of multiple websites select the default site to be used. Next enter<br />
the name of the new virtual directory (e.g. mynewsurvey). Then click the<br />
Browse button to select the file directory where the webfiles will be<br />
placed - see step 9.<br />
Step 9 - Create the Webfiles Directory<br />
<br />
By default the wizard points to the rootdirectory of the default website.<br />
Here you can create a new folder to store the survey webfiles or select<br />
an already existing (emtpy!) folder. You can also create a new folder<br />
outside the root of the default website.<br />
Step 10 - Confirm Foldercreation<br />
<br />
Once you've created a new folder to store the survey webfiles please<br />
confirm the creation. The wizard will then return to the website creation<br />
screen - step 11.<br />
Step 11 - Confirm Virtual Directory<br />
<br />
Check the selection of the default website, the virtual directory name<br />
and the folder name for the webfiles. Then click Next to continue.<br />
Step 12 - SqlServer Setup.<br />
<br />
This is where you enter the details of the MsSql server instance running<br />
on your computer. Make sure it is up and running before you fill out this<br />
screen. The wizard will check the connection and existance of the<br />
Sqlserver details. You have two options to connect to sqlserver: a.<br />
integrated windows authentication; b. user based authentication for which<br />
you will have to enter the login details.<br />
Step 13 - Database Setup<br />
<br />
After you have managed to connect to Sqlserver you can create the Survey<br />
database and (main) database user. Fill out the details as asked for by<br />
the wizard (and replace the default entries).<br />
Step 14 - Mailserver Setup<br />
<br />
Throught the Survey webapplication you can send mailinvitations to<br />
participate in a survey. To be able to make use of this option the<br />
webapplication will have to be able to connect to a Smtp mailserver.<br />
Please fill out the details of the Smtp connection the website should<br />
make use of.<br />
Step 15 - Start the Installation<br />
<br />
After all details for the website, directory, database and mailserver<br />
have been filled out the installation proces will start as soon as you<br />
click the Install button. The installation should only take a few minutes.<br />
Step 16 - Installation Progress<br />
<br />
During installation the wizard will show the progress screen to keep you<br />
informed about the running processes.<br />
Step 17 - Final Confirmation<br />
<br />
At the end of the installation process the final screen appears for you<br />
to finish the installation by clicking the Finish button. Before you do<br />
this you can choose to start the survey webapplication right after<br />
finishing the installation automatically or not.<br />
Finished &amp; ready to use...<br />
<br />
o Once the wizard has finished succesfully you should now be able to<br />
start and make use of the Survey webapplication. If you did not choose to<br />
start the website automatically you can start the website manually:<br />
a. by opening your webbrowser and entering the webaddress of the new<br />
Survey application;<br />
b. by clicking the Internet shortcut (LaunchSurvey) that was created in<br />
the files directory of the survey webapplication.<br />
<br />
o During installation a logfile (install.log) was created in the survey<br />
file directory. This file contains the details of your survey website as<br />
entered during the installation.<br />
<br />
o Should you run into problems during the installation there is a first<br />
step in identifying the cause of the problem by (re)running the<br />
installation and starting the Survey.msi installer from the command line<br />
with the following statement:<br />
msiexec /i &quot;[your directorypath]\Survey.msi&quot; /l*v SurveyLogFile.txt<br />
This will create a verbose logfile with all the details of every step<br />
taken during the installation proces. The logfile can be found in<br />
Documents and Settings/ root of [your computeraccount] directory.<br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

