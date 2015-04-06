________________________________________________________________________________________________


Survey™ Project v. 2.4 - The open source webapplication for online surveys and webforms

________________________________________________________________________________________________


Available release packages for the Survey™ Project v. 2.4 solution:

- Install		: http://survey.codeplex.com/Release/ProjectReleases.aspx
- Upgrade		: see advise below
- Source		: v. 2.3 available at http://survey.codeplex.com

- MS WebApp gallery	: Survey™ Project 2.3 available soon

________________________________________________________________________________________________

New Features & Fixes:

SP 2.4. - status: 2014/.....


I. New Features:



II. Technical upgrades:

1 - 


III. Bugfixes

1- 

- Database script changes





For information and questions visit the Survey™ Project community site at http://www.surveyproject.org.

________________________________________________________________________________________________

Instructions "Survey™ Project 2.4 Install package" - NEW INSTALLATION


1. SYSTEM REQUIREMENTS

Clientside:
- Tested Browsertypes IE11 ; Firefox 33.* >; Chrome 38.* 

Serverside:
- .NET Framework 4.5 or higher must be installed
- IIS webserver (7.5 or higer)
- SMTP mailserver account
- MsSqlserver 2012 database
- supported Operating Systems are Windows7 or >, Windows Server 2008/2012
- you must have administrator privileges on your computer 

Free downloads available at http://www.asp.net/downloads/


2. FILES
- download zipped install package to your computer
- right click file: check Properties --> Unblock
- unzip files to (new) file directory (e.g. C:/Survey/..)


3. IIS

Option 1.
- create a new Virtual Directory in IIS [e.g. called 'SurveyProject'] which points to the fysical directory where the Survey files are unzipped
- make sure you have default.aspx specified as a Default Document for your Virtual Directory
- set Asp.NET to version 4.* (or higher) and "Pipeline mode: Integrated"

Option 2. 
- Create a WebApplication in IIS and follow the steps of option 1.
- Add the Webapplication to the proper Applicationpool using .NET 4.5 and Integrated Pipeline Mode


4. DATABASE

"Existing DB installation"
- start MS SQL Server (using Enterprise Manager or your tool of choice )**
- create a new empty database
- open sql file 'SurveyProject_2.4_BETA_ExistingDBInstall_Mssql2012.sql' from the '_DatabaseSql' SP website directory in a query window
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




---------- NEW ------------

CREATE SETTINGS THROUGH SP SETTINGS PAGE

- browse to http://[nameofyourwebserver]/[NameOfSurveySite] or your hosting URL in your web browser
- the application will start running
- next open the settings.aspx page: http://[nameofyourwebserver]/[NameOfSurveySite]/setttings.aspx

This will open the General Settings administration page for the SP installation with the following options:


a. DATABASE CONNECTION STRINGS
	- edit the connectionstring(s) to match your SP database;
	- on submit the connectionstring section of the web.config file will be encrypted
	- to unencrypt the connection strings section of the web.config file click button Decrypt


b. DEFAULT DB CONNECTION
	- select / edit the default db connectionstring to use;
	- click button submit to save the changes


c. SMTP MAILSETTINGS
	- edit the SMTP Server settings
	- on submit the NsurveySettings section (including Smtp) of the web.config file will be encrypted
	- to unencrypt the NsurveySettings section of the web.config file click button Decrypt

d. MISCELLANEOUS SETTINGS
	- edit miscellaneous settings:
		* Set SP installation to Single User mode
		* Allow the use of Sqlbased Answertypes	
		* Set timeout on Uploaded Files
		* Set session timeout on uploaded Files
 
	- on submit the NsurveySettings section (including Smtp) of the web.config file will be encrypted
	- to unencrypt the NsurveySettings section of the web.config file click button Decrypt
	


---------- OLD ------------

CREATE SETTINGS MANUALLY


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


9. START SURVEY
- browse to http://[nameofyourwebserver]/[NameOfSurveySite] or your hosting URL in your web browser
- the application will start running


10. DEFAULT LOGIN
- on first login use:
	Username: admin
	Password: SP_admin01
- next create a survey and do not forget to change the default admin password

________________________________________________________________________________________________


Instructions on "Survey™ 2.4 Install package" - UPGRADE EXISTING INSTALLATION


WARNING: SP 2.4 BETA does not formally support upgrading from previous versions.If you decide to upgrade a previous version, make sure to create backups first.


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



