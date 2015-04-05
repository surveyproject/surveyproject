________________________________________________________________________________________________


Survey™ Project v. 2.3 - BETA - The open source webapplication for online surveys and webforms

________________________________________________________________________________________________


Available release packages for the Survey™ Project v. 2.3 solution:

- Install		: http://survey.codeplex.com/Release/ProjectReleases.aspx
- Upgrade		: see advise below
- Source		: v. 2.3 available soon at http://survey.codeplex.com
- MS WebApp gallery	: Survey™ Project 2.0 available

________________________________________________________________________________________________

New Features & Fixes:

SP 2.3. BETA - status: 2014/09/03


I. New Features:

1- complete overhaul of Administration tool userinterface:

* Layout & graphics adjustments and changes;
* Use of fieldset, ol/li, div, labels instead of tables & literals;
* Introduction of new logo & header replacement;
* Menu adjustments;
* Login panel adjustments;
* Introduction of Perfect Scrollbar;
* Results menu: graphical reports - Columnchart & Piechart adjustments


2- Survey userinterface adjustments & introduction of survey version for mobile devices as default for 

* Mailings
* Preview
* Take Survey
* Web - deployment url

through the use of:

* bootstrap 3.2.0
* fontawesome 4.1.0
* Modernizer 2.7.2
* Respond 1.4.2
* Html5shiv

* new survey CSS file: surveymobile.css

Original survey layout & css still available through use of menuoption Layout (nsurveyform.css) and as Legacy weblink through menuoption Campaigns/ Web 


3- CSS cleanout & new files added


4-Several new additions & corrections to XML language files (English, Dutch, German)


5-New (sub)filter options on menuoption Results-Filters and Results/Reports-Graphical Reports (see PDF in Instructions Directory). 


6- Additional new helpfiles (including instructions on the use of "Required Markers") and help tooltips.


7- Required Markers made visible on Survey Formbuilder. Adjusted working of Required Markers on Question and Answer level.



II. Technical upgrades:

1- CKeditor 4.4.3 + skins;
2- Jquery cleanout and upgrades to latest versions;
3- ASTreeview 1.6.0.4;

4- Introduction of Nuget Packages directory: 11 packages added;
5- Introduction of Libary directory for third party DLL's;
6- Permission to use ItextSharp 5.5.2 for next SP releases; testbutton added to menuoption Data Export - Export PDF

7- Removal of Enterprise Library - Data customization + default EL (data/ custom) added as Nuget packages;


III. Bugfixes

1- Error on adding/ copying question to/ from library;
2- Error on adding/ copying question to/ from survey;
3- Error library: move question up/ down
4- Sorting option on surveylist not working + responses column based on voters instead of nr. of times shown
5- Error on deleting ip range (surveyiprange.cs)
6- Error delete regexpression

- Database script changes
* removed redundant & legacy insert lines 
* fieldorder adjustments to stored procedures
* addition of filter/subfilter tablechange & stored procedures


For information and questions visit the Survey™ Project community site at http://www.surveyproject.org.

________________________________________________________________________________________________

Instructions "Survey™ Project 2.3 Install package" - NEW INSTALLATION


1. SYSTEM REQUIREMENTS

Clientside:
- Tested Browsertypes IE11 ; Firefox 31.0 >; Chrome 36.0.1985.143 

Serverside:
- .NET Framework 4.* or higher must be installed
- IIS webserver (7.5 or higer)
- SMTP mailserver account
- MsSqlserver 2008 R2 or 2012 database
- supported Operating Systems are Windows7 or >, Windows Server 2008/2012
- you must have administrator privileges on your computer 

Free downloads available at http://www.asp.net/downloads/


2. FILES
- download zipped install package to your computer
- right click file: check Properties/ Unblock
- unzip files to (new) file directory (e.g. C:/Survey/..)


3. IIS

Option 1.
- create a new Virtual Directory in IIS [e.g. called 'SurveyProject'] which points to the fysical directory where the Survey files are unzipped
- make sure you have default.aspx specified as a Default Document for your Virtual Directory
- set Asp.NET to version 4.* (or higher) and "Pipeline mode: Integrated"

Option 2. 
- Create a WebApplication in IIS and follow the steps of option 1.
- Add the Webapplication to the proper Applicationpool using .NET 4.* and Integrated Pipeline Mode


4. DATABASE

"Existing DB installation"
- start MS SQL Server (using Enterprise Manager or your tool of choice )**
- create a new empty database
- open sql file 'SurveyProject_2.3_BETA_ExistingDBInstall_Mssql2012.sql' from the '_DatabaseSql' SP website directory in a query window
- change USE [yourdatabasename] command to the name of your database
- run the SQL query
- check to see if the database was created correctly

*Notes: 
	o you cannot upgrade any existing SP databases with the 2.3 scripts
	o for Mssqlserver 2008 R2 try the (untested) file: SurveyProject_2.3_BETA_ExistingDBInstall_Mssql2008R2_Untested.sql


5. SECURITY
If using Windows 2000 or XP - IIS5
- the {Server}/ASPNET user account must have Read, Write and Change Control 
of the root application directory (this allows the application to create files/folders ) 

If using Windows 2003 - IIS6
- the {Server}/NetworkService user account must have Read, Write and Change Control 
of the root application directory (this allows the application to create files/folders ) 


6. DATABASE CONNECTION
- open the web.config file in notepad or a texteditor
- Check the database connection string in the web.config file. You can create connectionstrings for different databases (e.g. Development, Test, Production):

  <!-- selection of database connection options & settings-->
  <connectionStrings>
    <add name="SurveyProjectDevConnectionString" connectionString="Data Source=.\yoursqlserver;Initial Catalog=YourSpDevDatabase;Persist Security Info=True;User ID=yourusername;Password=yourpassword" providerName="System.Data.SqlClient" />
    <add name="SurveyProjectTestConnectionString" connectionString="Data Source=.\yoursqlserver;Initial Catalog=YourSpTestDatabase;Persist Security Info=True;User ID=yourusername;Password=yourpassword" providerName="System.Data.SqlClient" />
    <add name="SurveyProjectProdConnectionString" connectionString="Data Source=.\yoursqlserver;Initial Catalog=YourSpProdDatabase;Persist Security Info=True;User ID=yourusername;Password=yourpassword" providerName="System.Data.SqlClient" />
  </connectionStrings>

- Next set the default dbconnection to use on the SP site:

  <!-- Survey Project database connection: to select connectionsetting options see connectionStrings-->
  <dataConfiguration defaultDatabase="SurveyProjectProdConnectionString" />


7. SMTP MAILSETTINGS
- open the web.config file in notepad or a texteditor
- check smpt settings. Default is:
		<add key="NSurveySMTPServer" value="127.0.0.1" />
		<add key="NSurveySMTPServerPort" value="25" />
		<add key="NSurveySMTPServerAuthUserName" value="" />
		<add key="NSurveySMTPServerAuthPassword" value="" />

                <add key="NSurveySMTPServerEnableSsl" value="false"/>  

8. CULTURE/UICULTURE
- The language setting of your webbrowser will determine the XML languagefiles used to translate webpage texts. 
- This effect is caused by one of the settings in the web.config file: culture/ uiculture. 
- If set to "auto" (default) Survey™ Project will pick up culture/ language of preferred browser settings. 
- To explicitely pick a culture/ language, replace auto with "en-US" or "nl-NL" for example. 


9. CKeditor & Filemanager
a- Set server path in Filemanager\Scripts\filemanager.config.js -->

        "serverRoot": "[your webserver]",
        "fileRoot": "",

Check instructions at: https://github.com/simogeo/Filemanager/wiki/Filemanager-configuration-file

b- Set server path in CKeditor\config.js

    config.filebrowserBrowseUrl = '/[your webserver]/Filemanager/index.html';


10. START SURVEY
- browse to http://[nameofyourwebserver]/[NameOfSurveySite] or your hosting URL in your web browser
- the application will start running


11. DEFAULT LOGIN
- on first login use:
	Username: admin
	Password: SP_admin01
- next create a survey and do not forget to change the default admin password

________________________________________________________________________________________________


Instructions on "Survey™ 2.3 Install package" - UPGRADE EXISTING INSTALLATION


WARNING: SP 2.3 BETA does not formally support upgrading from previous versions.If you decide to upgrade a previous version, make sure to create backups first.


Webfiles:
- because of several changes to the directorystructure it is advised to delete all previous files and copy the files and directories to the webserver
- CSS and image user directories can be copied back from the backupfiles


Database:
- please check the DatabaseSql/UpgradeFiles directory for a description of changes and upgrade files


Surveys:
- Surveys, questions and answers created in 2.* can be exported to XML and imported into the 2.3 version. 
- Manual corrections to the XML files may be necessary;
- Instruction on how to import 1.* surveys can be found in the file: SP20_ImportSurveys.txt


________________________________________________________________________________________________

Survey's sources are released free of charge. However, you must READ and FULLY understand
 the license agreement before you download and use the software.

You can support the project (hosting, dev. tools) and/or donate. 
Go to http://survey.codeplex.com or visit the Community site at http://www.surveyproject.info

________________________________________________________________________________________________

Survey™ Source Code

Sources are published separately at the Survey Project Codeplex site: Source Code.
 
If you fix bugs or add new features that can be useful to the Survey Project
 community do not hesitate to contact us to integrate them in the public release.

Also if you use Survey Projects engine or tool in a project we would be happy to hear from 
your testimonials on our forums at http://www.surveyproject.org or in private. 

For the latest news check >> http://www.surveyproject.org
For the latest downloads check >> http://survey.codeplex.com

________________________________________________________________________________________________

Survey™ Form Samples

- Sample Survey forms can be found in the _SurveySamples directory
- Import the xml files through the New Survey menu options

________________________________________________________________________________________________



