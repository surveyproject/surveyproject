_______________________________________________________________________


Databasechanges SP v. 2.4 to SP v. 2.5

_______________________________________________________________________


Notes: 
- DB upgrades are not formally supported.
- Always make sure to backup your databases first before making any changes or running scripts.
- In case of upgrading an SP v. 2.3 database to v.2.4 execute all queries listed below in the order as described
- The queryfiles can be found in the _DatabaseSql\UpgradeFiles_23to24 directory

***********************************************************************************


1. Azure compatibility

2. update Assembly names
* vts_tbanswertypes
* vts_tbquestionselectionoptions


3. add count rows of Usertable
* vts_spUserGetUseridFromUserName
* used to create initial admin account on first login after installation of SP


4. insert new security right
* AccessUserReponses right




