_______________________________________________________________________


Databasechanges based on file comparison SP v. 2.3 and SP v. 2.4

_______________________________________________________________________


Notes: 
- DB upgrades are not formally supported.
- Always make sure to backup your databases first before making any changes or running scripts.
- In case of upgrading an SP v. 2.3 database to v.2.4 the following changes have to be made:

***********************************************************************************


1 01_vts_tbWebSecurityAddIn_IPFilterToken.sql

* issue Github 26: missing entries from vts_tbSecurityAddIn
* add token and filter add in's to vts_tbsecurityaddin


2 02_vts_tbanswertype_AddFieldAddressType.sql

* new answertype (google) FieldAddressType


3 vts_spVoterInvitationAnsweredGetAll.sql
  vts_spVoterInvitationQueueGetAll.sql

* changes/edited stored procedures:
* Both to add paging on mailingstatus.aspx page.


4 04a_vts_tbAnswer.sql
  04b_vts_spAnswerAddNew.sql
  04c_vts_spAnswersCloneByQuestionId.sql
  04d_vts_spAnswerGetDetails.sql
  04e_vts_spAnswerUpdate.sql
  04f_vts_spQuestionGetAnswers.sql
  04g_vts_spSurveyGetForExport.sql
  04h_vts_spQuestionGetForExport.sql


* add CSS field option on answer level (+ include in export)
* change vts_answertable (1x)
* change/ add stored procedures  (7x)

