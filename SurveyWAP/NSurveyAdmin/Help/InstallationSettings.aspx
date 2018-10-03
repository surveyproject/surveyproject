<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.HelpFiles" Codebehind="default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyManagement" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
            <h2 style="color:#5720C6;">System Settings</h2><hr style="color:#e2e2e2;" />

            Installation or "system settings" are at the core of the webapplication. In part they are set during the initial installation on the webserver and can be found
            in the web.config file in the root directory of the webapplication.
            <br /><br />
            Access to the System Settings webpage is restricted to SP administrators (admin account) only.

            <br /><br /><u>Encryption</u><br /><br />
            Through the System Settings page the nsurvey web.config sections can be encrypted and decrypted. Encryption will make the 
            sections more secure and unreadable to people who have access to the files on the webserver (e.g. third party hosting administrators).
            <br /><br />
            On installation and by default the Web.config file sections are not encypted. When section settings are changed through the System Settings page these will be encrypted automatically. 
                To decrypt the section press the Decrypt button.
            <br /><br /><br />
            <b>Database Connections</b><br /><br />
            This section is used to setup the connection to the SP database(s) where all surveys, questions/ answers, settings and surveysresults are stored.
            <br /><br />
            <i>Connection Strings</i><br /><br />
            Option to set up three different default database connections for different environments/ platforms:<br /><br />
            - Development: connection name = SurveyProjectDevConnectionString<br />
            - Test: connection name = SurveyProjectTestConnectionString<br />
            - Production: connection name = SurveyProjectProdConnectionString<br /><br />

            <i>Default Connection</i><br /><br />
            There can be only one active database connection at a time. The active connection name must be selected from the list of [3] connectionstrings. 

            <br /><br /><br />
            <b>Globalisation Culture</b><br /><br />
            Option to set the User Interface culture and language: if set to "auto" the application will pick up the preffered culture/ language of the browser settings.
            To explicitely pick a culture/ language, replace "auto" with "en-US" or "nl-NL" for example. Through the <a href="Multi-Languages Settings.aspx">Multi-Language settings</a> culture and language will also be influenced.
            <br /><br /><br />
            <b>NSurvey Settings</b><br /><br />
            These concern several of the original NSurveySettings sections to make use of particular features of the application or determine settings that may influence the general working of SP.
            <br /><br />
            <i>Mailserver Settings</i><br /><br />
            Option to set the (SMTP) mailserver settings:<br />
            <br />Mailserver - ip or webaddress of the SMTP mailserver
            <br />Username - username of SMTP account to access the mailserver
            <br />Password - password of SMTP account to access the mailserver
            <br />Port - portnumber to access the mailserver (e.g. set to 25 which is the default used by hosting providers)
            <br /><br />
            The mailserver is used to send mailmessage from the SP&trade; application. Several features make use of the mailserver. E.g. invitation messages or notification messages. 
            <br /><br />
            <i>Miscellaneous Settings</i><br /><br />
            o <u>FormUserProviderSingleMode:</u>
           <br />
            You can run nsurvey in a single user mode 
			and let the default FormUserProvider create 
			a "dummy" admin user on the fly. If enabled user management 
			and access right checking features will be disabled.
            <br /><br />
            o <u>SQL Based Answer Types Allowed:</u>
            <br />
            Option to allow/ disallow the use of database queries using Sql based answer types.
            <br /><br />
            o <u>UploadedFileDeleteTimeOut:</u>
            <br />
            Setting to determine how many hours unvalidated uploaded files will stay saved on the database
            <br /><br />
            o <u>SessionUploadedFileDeleteTimeOut:</u>
            <br />
            Setting to determine how many hours uploaded files for sessions that have not 
			yet been resumed stay saved on the database. Leave some time
			because user wont get notified if its session have been deleted when he resumes.
            <br /><br />
                <hr style="color:#e2e2e2;" />
                <h3>More Information</h3><br />
                                <a href="ErrorLog.aspx">Error Logging</a><br />
                <a href="Multi-Languages Settings.aspx">Multi-Language settings</a><br />

                    </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
