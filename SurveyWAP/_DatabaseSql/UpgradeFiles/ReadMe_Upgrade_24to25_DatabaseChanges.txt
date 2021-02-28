_______________________________________________________________________


Databasechanges & Update for SP v. 2.4 to SP v. 2.5

_______________________________________________________________________

Notes: 
- DB upgrades are not formally supported.
- Always make sure to backup your databases first before making any changes or running scripts.
- In case of upgrading an SP v. 2.4 database to v.2.5 execute all queries listed below in the order as described
- The queryfiles can be found in the _DatabaseSql\UpgradeFiles directory

WARNING
- Due to the long list of changes the correct working after running all scripts cannot be guaranteed (untested)
- Only the new full DB script [SurveyProject_2.5_DBInstall_Mssql2016.sql] has been properly tested

***********************************************************************************

0. Collation adjustments (full DB scripts only)
- several changes (consistancy of use of capitals and i/ I etc) throughout the entire DB scripts
- no upgrade scripts available

1. Azure compatibility adjustment
*remove 'Use [dbname]' from Sql script (manually) before running on MS AZURE platform

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
a* vts_spReportGetscores_CustomReportData NPS example
b* default calculation format

10. Set UserId (0) to 1 in SingleUserMode to show all Folders in treelist
* vts_spTreeNodesGetAll_SingleUserMode.sql

11. Update Multilanguages table: delete old/ insert new entries
* Update_vts_tbMultilanguages_Edits.sql

12. Add new security Roleright to Access SSRS report overview

13. Add new stored procedure for SSRS Report test to get all Voter data

14. Add new Regular Expressions

15 (abc) New Message option on Unauthentied (Security addin)

16. New Security Rights

- NTextReplacements: Replace NText by NVARCHAR(max)

17. New Table Menu and MenuSecurityRight: used to match Menu items and security rights

18. Stored procedure to fetch Menu items and matching Security rights.

19. Adjusted Stored procedure: SpVoterDeleteAll - batch delete: adjusted to avoid timeouts and exploding db log files; used when deleting all voters from the statistics page.

20. Delete of two answertypes making use of (deprecated and redundante) javascript

21. Adjusted Stored Procedure to make the menu Filemanager webpage work correctly (paging/ list of files)

22. Regular Expression in Use (Answer)

--- 2020/03

23. - 31. 
Modified Stored Procedures to correctly apply displayorder value to Matrix child questions (including export/import etc)









