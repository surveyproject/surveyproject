_______________________________________________________________________


Databasechanges & Update for SP v. 2.4 to SP v. 2.5

_______________________________________________________________________


Notes: 
- DB upgrades are not formally supported.
- Always make sure to backup your databases first before making any changes or running scripts.
- In case of upgrading an SP v. 2.4 database to v.2.5 execute all queries listed below in the order as described
- The queryfiles can be found in the _DatabaseSql\UpgradeFiles directory

***********************************************************************************


1. Azure compatibility adjustment
*remove 'Use [dbname]' from Sql script (manually)

2. update Assembly names  (new DLL name: SurveyProject. )
* vts_tbanswertypes
* vts_tbquestionselectionmode
* vts_tbwebsecurityaddin

3. Add count # of rows of Usertable
* vts_spUserGetUseridFromUserName
* used to create initial admin account on first login after installation of SP
* if no table rows: create admin account
* default admin account (user+ usersettings) removed from install script

4a. Insert new security right for access to individual voterreport
* AccessUserReponses right added

4b. Voter report entries for individual user Access
*vts_spVoterGetPivotTextIndivEntries

4c. Get list of voterreports for individual user
* vts_spVoterGetPagedIndiv

5. Add Friendly (url) name to surveytitle tooltip in surveylist
*vts_spSurveyGetListbyTitle

6 Nvarchar: varchar & max replacement: remove text lenght 4000 limit
a* vts_spQuestionAddNew_nvarcharmax
b* vts_spQuestionUpdate_nvarcharmax
c* vts_tbQuestion_Nvarcharmax
d* vts_spMultilanguageTextAdd_Nvarcharmax
e* vts_spMultilanguageTextUpdate_Nvarcharmax
f* vts_tbMultilanguageText_Nvarcharmax

7. Friendly name URL check/ control: existing or not
* vts_spsurveyUpdateFriendlyName_Checkexisting name

8. XML USstates added to answertypes
* InsertAnserType_XmlUsStates

9. Custom (results) Report Stored Procedure
* vts_spReportGetscores_CustomReportData

10. Set UserId (0) to 1 in SingleUserMode to show all Folders in treelist
* vts_spTreeNodesGetAll_SingleUserMode.sql

