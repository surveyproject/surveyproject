<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false"  Inherits="Votations.NSurvey.WebAdmin.HelpFiles" Codebehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <div id="helpPanel">

<h1 style="color:#5720C6;">Helpfiles Index</h1>    
<div style="float:right; margin: -45px 30px 0 0;"><img alt="index" title="Helpfiles Index" src="<%= Page.ResolveUrl("~")%>Images/index-icon.png" /></div> 
<br />

I.	&nbsp;		&nbsp;		&nbsp;		&nbsp;<strong>	Introduction </strong>	<a name="Introduction" ></a><br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
1	&nbsp;	1	&nbsp;		&nbsp;		&nbsp;	<a href="About.aspx" target="_self" title="About" >Survey&trade; Project</a>	<br />
1	&nbsp;	2	&nbsp;		&nbsp;		&nbsp;	<a href="Licence.aspx" title="Survey&trade; License" >GNU GPL v.3 License</a>	<br />
1	&nbsp;	3	&nbsp;		&nbsp;		&nbsp;	<a href="Help on Help.aspx" title="Survey&trade; Helpfiles " >Helpfiles </a>	<br />
1	&nbsp;	4	&nbsp;		&nbsp;		&nbsp;	<a href="Survey Copyright and Disclaimer.aspx" title="Copyright and Disclaimer " >Copyright and Disclaimer </a>	<br />
1	&nbsp;	5	&nbsp;		&nbsp;		&nbsp;	<a href="Requirements.aspx" title="Requirements" >Technical Requirements</a>	<br />

	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
II.	&nbsp;		&nbsp;		&nbsp;		&nbsp;	 <strong>Survey Management </strong> 	<a name="SurveyManagement" ></a><br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
2	&nbsp;	0a	&nbsp;		&nbsp;		&nbsp;	<a href="InstallationSettings.aspx" title=" System Settings "> System Settings </a>	<br />
2	&nbsp;	0b	&nbsp;		&nbsp;		&nbsp;	<a href="ErrorLog.aspx" title=" Error Logging "> Error Logging </a>	<br /><br />



2	&nbsp;	1a&nbsp;		&nbsp;&nbsp;<a href="Survey Creation.aspx" title=" Survey Creation "> Survey Creation </a>	<br />
2	&nbsp;	1b&nbsp;		&nbsp;&nbsp;<a href="SurveyListDirectory.aspx" title=" Survey List & Directory "> Survey List & Directory </a>	<br />
2	&nbsp;	2	&nbsp;		&nbsp;		&nbsp;	<a href="Survey General Settings.aspx" title=" Survey Settings " > Survey Settings </a>	<br />
2	&nbsp;	3	&nbsp;		&nbsp;		&nbsp;	<a href="Email Notification.aspx" title=" Email Notification " > Email Notification </a>	<br />
2	&nbsp;	4	&nbsp;		&nbsp;		&nbsp;	<a href="Statistics.aspx" title=" Survey Statistics " > Survey Statistics </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
2	&nbsp;	5	&nbsp;		&nbsp;		&nbsp;	 <u>Completion </u>	<a name="Completion" ></a><br />
2	&nbsp;	5	&nbsp;	1	&nbsp;		&nbsp;	<a href="Completion Actions.aspx" title=" Completion Actions " > Completion Actions </a>	<br />
2	&nbsp;	5	&nbsp;	2	&nbsp;		&nbsp;	<a href="Thanks Message Conditions.aspx" title=" Thanks Message Conditions " > Thanks Message Conditions </a>	<br />
2	&nbsp;	5	&nbsp;	3	&nbsp;		&nbsp;	<a href="New\CreateCustomReport.aspx" title="Creating Custom Results Reports">Custom Results Reports</a>	<br />
2	&nbsp;	5	&nbsp;	4	&nbsp;		&nbsp;	<a href="new/Advanced Completion.aspx" title=" Advanced Completion " > Advanced Completion </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
2	&nbsp;	6	&nbsp;		&nbsp;		&nbsp;	 <u>Score</u> 	<a name="Score" ></a><br />
2	&nbsp;	6	&nbsp;	1	&nbsp;		&nbsp;	<a href="Score_Introduction.aspx" title=" Score Introduction " > Introduction </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
2	&nbsp;	7	&nbsp;		&nbsp;		&nbsp;	 <u>Multi-language Surveys </u>	<a name="MLSurveys" ></a><br />
2	&nbsp;	7	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/ML_Introduction.aspx" title=" Multi-Languages Introduction " > Multi Language Introduction </a>	<br />
2	&nbsp;	7	&nbsp;	2	&nbsp;		&nbsp;	<a href="Multi-Languages Settings.aspx" title=" Multi-Languages Settings " > Multi Language Settings </a>	<br />
2	&nbsp;	7	&nbsp;	3	&nbsp;		&nbsp;	<a href="System Messages Manager.aspx" title=" System Messages & Multi language texts" > System Messages & Multi language texts</a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
2	&nbsp;	8	&nbsp;		&nbsp;		&nbsp;	 <u>Survey Layout & Style</u> 	<a name="SurveyLayout" ></a><br />
2	&nbsp;	8	&nbsp;	1	&nbsp;		&nbsp;	<a href="Style_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
2	&nbsp;	8	&nbsp;	2	&nbsp;		&nbsp;	<a href="Web Control Style.aspx" title=" Web Control Style & Layout " > Web Control Style </a>	<br />
2	&nbsp;	8	&nbsp;	3	&nbsp;		&nbsp;	<a href="CssXml.aspx" title=" Css Xml Layoutoptions " > CssXml Layout Options </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
2	&nbsp;	9	&nbsp;		&nbsp;		&nbsp;	 <u>Security </u>	<a name="Security" ></a><br />
2	&nbsp;	9	&nbsp;	1	&nbsp;		&nbsp;	<a href="Sec_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
2	&nbsp;	9	&nbsp;	2	&nbsp;		&nbsp;	<a href="Survey Security.aspx" title=" Survey Security " > Survey Security </a>	<br />
2	&nbsp;	9	&nbsp;	3	&nbsp;		&nbsp;	<a href="Insert Security AddIn.aspx" title=" Insert Security AddIn " > Insert Security AddIn </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />

3	&nbsp;	9	&nbsp;	4	&nbsp;		&nbsp;	 <u>Security AddIns</u> 	<a name="SecurityAddins" ></a><br />
3	&nbsp;	9	&nbsp;	4	&nbsp;	1	&nbsp;	<a href="new/IP Protection.aspx" title=" IP Protection " > IP Protection </a>	<br />
3	&nbsp;	9	&nbsp;	4	&nbsp;	2	&nbsp;	<a href="new/Cookie Protection.aspx" title=" Cookie Protection " > Cookie Protection </a>	<br />
3	&nbsp;	9	&nbsp;	4	&nbsp;	3	&nbsp;	<a href="new/Pass Protection.aspx" title=" Password Protection " > Password Protection </a>	<br />
3	&nbsp;	9	&nbsp;	4	&nbsp;	4	&nbsp;	<a href="new/EMail Code Protection.aspx" title=" EMail Code Protection " > Email Code Protection </a>	<br />
3	&nbsp;	9	&nbsp;	4	&nbsp;	5	&nbsp;	<a href="new/ASP.NET Security Context.aspx" title=" ASP.NET Security Context " > ASP.NET Security Context </a>	<br />
3	&nbsp;	9	&nbsp;	4	&nbsp;	6	&nbsp;	<a href="new/Survey Security Context.aspx" title=" SP&trade; Security Context " > SP&trade; Security Context </a>	<br />
3	&nbsp;	9	&nbsp;	4	&nbsp;	7	&nbsp;	<a href="new/Entry Number Limitation.aspx" title=" Entry Number Limitation " > Entry Number Limitation </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp; <br />
3	&nbsp;	9	&nbsp;	5	&nbsp;		&nbsp;	 <u>Key Provider </u>	<a name="KeyProvider" ></a><br />
3	&nbsp;	9	&nbsp;	5	&nbsp;	1	&nbsp;	<a href="new/KP_Introduction.aspx" title=" Introduction" >  Introduction</a>	<br />
            	&nbsp;		&nbsp;		&nbsp;		&nbsp; <br />
3	&nbsp;	9	&nbsp;	6	&nbsp;		&nbsp;	 <u>Token Security </u>	<a name="Token" ></a><br />
3	&nbsp;	9	&nbsp;	6	&nbsp;	1	&nbsp;	<a href="new/Token_Introduction.aspx" title=" Token Introduction" > Token Introduction</a>	<br />
3	&nbsp;	9	&nbsp;	6	&nbsp;	2	&nbsp;	<a href="new/Token Protection.aspx" title=" Token Protection" > Token Protection</a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />


III.&nbsp;		&nbsp;		&nbsp;		&nbsp;	 <strong>Survey Form Builder <a name="SurveyDesigner" ></a></strong>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />

 
4	&nbsp;	1	&nbsp;		&nbsp;		&nbsp;	<a href="new/SCE_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
4	&nbsp;	2	&nbsp;		&nbsp;		&nbsp;	<a href="new/Survey Form Builder.aspx" title=" Survey Form Builder " > Survey Form Builder </a>	<br />
4	&nbsp;	3	&nbsp;		&nbsp;		&nbsp;	<a href="new/Insert Question.aspx" title=" Insert Question " > Insert Question </a>	<br />
4	&nbsp;	4	&nbsp;		&nbsp;		&nbsp;	<a href="new/Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
4	&nbsp;5a&nbsp;		&nbsp;		&nbsp;	<a href="new/Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>	<br />
4	&nbsp;5b&nbsp;		&nbsp;		&nbsp;	<a href="new/RequiredMarker.aspx" title=" Required Markers " >Required Markers </a>	<br />
 <!-- limited revision: -->
4	&nbsp;	6a&nbsp;		&nbsp;		&nbsp;<a href="new/Answer Type Creator.aspx" title=" Answer Type Creator " >Answer Type Creator </a>	<br />
4	&nbsp;	6b&nbsp;		&nbsp;		&nbsp;<a href="new/Answer Type Editor.aspx" title=" Answer Type Editor " >Answer Type Editor </a>	<br />
4	&nbsp;	7	&nbsp;		&nbsp;		&nbsp;	<a href="new/QuestionGroups.aspx" title=" Question Groups " > Question Groups </a>	<br />


4	&nbsp;	8	&nbsp;		&nbsp;		&nbsp;	<a href="new/Matrix Question Editor.aspx" title=" Matrix Question Editor " > Matrix Question Editor </a>	<br />
4	&nbsp;	9	&nbsp;		&nbsp;		&nbsp;	<a href="new/Matrix Rows _ Column Editor.aspx" title=" Matrix Rows / Column Editor " > Matrix Rows / Column Editor </a>	<br />
4	&nbsp;	10&nbsp;      &nbsp;		&nbsp;<a href="new/Regular Expression Editor.aspx" title=" Regular Expression Editor " >Regular Expression Editor </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />

4	&nbsp;	11	&nbsp;		&nbsp;		&nbsp;	<u>Question Library</u> <a name="QuestionLibrary"></a><br />
4	&nbsp;	11	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/QL_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
4	&nbsp;	11	&nbsp;	2	&nbsp;		&nbsp;	<a href="new/Library Directory.aspx" title=" Library Directory and Edit " > Library Directory and Edit </a>	<br />
4	&nbsp;	11	&nbsp;	3	&nbsp;		&nbsp;	<a href="new/Library Templates.aspx" title=" Library Templates " > Library Templates </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
4	&nbsp;	12	&nbsp;		&nbsp;		&nbsp;	 <u>Repeatable Sections</u> <a name="RepeatableSections"></a> 	<br />
4	&nbsp;	12	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/Repeatable_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
4	&nbsp;	12	&nbsp;	2	&nbsp;		&nbsp;	<a href="new/Sections Management.aspx" title=" Sections Management " > Sections Management </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
           
4	&nbsp;	13	&nbsp;		&nbsp;		&nbsp;	 <u>Question Types</u> <a name="QuestionTypes"></a> 	<br />
4	&nbsp;	13	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/Single Question.aspx" title=" Single Question " > Single Question </a>	<br />
4	&nbsp;	13	&nbsp;	2	&nbsp;		&nbsp;	<a href="new/Matrix Question.aspx" title=" Matrix Question " > Matrix Question </a>	<br />
4	&nbsp;	13	&nbsp;	3	&nbsp;		&nbsp;	<a href="new/Static Question.aspx" title=" Static Question " > Static Question </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
4	&nbsp;	14	&nbsp;		&nbsp;		&nbsp;	 <u>Answer Types</u> <a name="AnswerTypes"></a> 	<br />
4	&nbsp;	14	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/AT_Introduction.aspx" title=" Introduction " > Introduction </a>	
    <br />
    <br />
           
4	&nbsp;	14	&nbsp;	2	&nbsp;		&nbsp;	 <u>Selection Answer Types</u>	<a name="SelectionTypes" ></a><br />
	
4	&nbsp;	14	&nbsp;	2	&nbsp;	1	&nbsp;	<a href="new/SelectionT_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
4	&nbsp;	14	&nbsp;	2	&nbsp;	2	&nbsp;	<a href="new/Selection - Text.aspx" title=" Selection - Text " > Selection - Text </a>	<br />
4	&nbsp;	14	&nbsp;	2	&nbsp;	3	&nbsp;	<a href="new/Selection - Other.aspx" title=" Selection - Other " > Selection - Other </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />


4	&nbsp;	14	&nbsp;	3	&nbsp;		&nbsp;	 <u>Field Answer Types </u>	<a name="FieldTypes" ></a><br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	1	&nbsp;	<a href="FieldT_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	2	&nbsp;	<a href="Field - Basic.aspx" title=" Field - Basic " > Field - Basic </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	3	&nbsp;	<a href="Field - Slider.aspx" title=" Field - Slider " > Field - Slider </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	4	&nbsp;	<a href="Field_Address.aspx" title=" Field - Address " > Field - Address </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	5	&nbsp;	<a href="new/Field - Large.aspx" title=" Field - Large " > Field - Large </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	6	&nbsp;	<a href="new/Field - Required.aspx" title=" Field - Required " > Field - Required </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	7	&nbsp;	<a href="new/Field - Email.aspx" title=" Field - Email " > Field - Email </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	8	&nbsp;	<a href="new/Field - Rich.aspx" title=" Field - Rich " > Field - Rich </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	9	&nbsp;	<a href="new/Field - Calendar.aspx" title=" Field - Calendar " > Field - Calendar </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	10	&nbsp;	<a href="new/Field - Hidden.aspx" title=" Field - Hidden " > Field - Hidden </a>	<br />
4	&nbsp;	14	&nbsp;	3	&nbsp;	11	&nbsp;	<a href="new/Field - Password.aspx" title=" Field - Password " > Field - Password </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
4	&nbsp;	14	&nbsp;	4	&nbsp;		&nbsp;	 <u>Xml Bound Answer Types</u> 	<a name="XmlBoundTypes" ></a><br />
	
4	&nbsp;	14	&nbsp;	4	&nbsp;	1	&nbsp;	<a href="new/XMLT_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
4	&nbsp;	14	&nbsp;	4	&nbsp;	2	&nbsp;	<a href="new/Xml - Country.aspx" title=" Xml - Country " > Xml - Country </a>	<br />
4	&nbsp;	14	&nbsp;	4	&nbsp;	3	&nbsp;	<a href="new/Xml - US States.aspx" title=" Xml - US States " > Xml - US States </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
4	&nbsp;	14	&nbsp;	5	&nbsp;		&nbsp;	 <u>Sql Bound Answer Types</u> 	<a name="SqlTypes" ></a><br />
	
4	&nbsp;	14	&nbsp;	5	&nbsp;	1	&nbsp;	<a href="new/SQLType_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
4	&nbsp;	14	&nbsp;	6	&nbsp;		&nbsp;	 <u>Misc. Answer Types</u> 	<a name="Misc" ></a><br />
	
4	&nbsp;	14	&nbsp;	6	&nbsp;	1	&nbsp;	<a href="new/Extended - File Upload.aspx" title=" Extended - File Upload " > Extended - File Upload </a>	<br />
4	&nbsp;	14	&nbsp;	6	&nbsp;	2	&nbsp;	<a href="new/Subscriber - Xml List.aspx" title=" Subscriber - Xml List " > Subscriber - Xml List </a>	<br />
4	&nbsp;	14	&nbsp;	6	&nbsp;	4	&nbsp;	<a href="new/Boolean.aspx" title=" Boolean " > Boolean </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />

4	&nbsp;	15	&nbsp;		&nbsp;		&nbsp;	 <u>Survey Conditional Logic</u> 	<a name="ConditionalLogic" ></a><br />

4	&nbsp;	15	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/Condition_Introduction.aspx" title=" Conditional Logic Introduction " > Introduction </a>	<br />
4	&nbsp;	15	&nbsp;	2	&nbsp;		&nbsp;	<a href="new/Branching conditions.aspx" title=" Branching Conditions " > Branching Conditions </a>	<br />
4	&nbsp;	15	&nbsp;	3	&nbsp;		&nbsp;	<a href="new/Skip Logic Conditions.aspx" title=" Skip Logic Conditions " > Skip Logic Conditions </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
4	&nbsp;	16	&nbsp;		&nbsp;		&nbsp;	 <u>Survey Rating</u> 	<a name="Rating" ></a><br />
4	&nbsp;	16	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/Rating_Introduction.aspx" title=" Rating Introduction " > Rating Introduction </a>	<br />
            <br />
4	&nbsp;	17	&nbsp;		&nbsp;		&nbsp;	 <u>Answer Piping</u> 	<a name="Piping" ></a><br />
4	&nbsp;	17	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/AP_Introduction.aspx" title=" Piping Introduction " > Answer Piping Introduction </a>	<br />
            <br /><br />
IV.	&nbsp;		&nbsp;		&nbsp;		&nbsp;	 <strong>Survey Publishing & Campaigns <a name="Campaigns" ></a></strong>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
5	&nbsp;	1	&nbsp;		&nbsp;		&nbsp;	<a href="new/SD_Introduction.aspx" title=" Survey Campaigns & Publishing Introduction " > Introduction </a>	<br />
5	&nbsp;	2	&nbsp;		&nbsp;		&nbsp;	<a href="new/Survey Deployement.aspx" title=" Survey Link Deployment " > Survey Link Deployment </a>	<br />
5	&nbsp;	3	&nbsp;		&nbsp;		&nbsp;	<a href="new/Take Survey.aspx" title=" Take Survey " > Take Survey </a>	<br />
5	&nbsp;	4	&nbsp;		&nbsp;		&nbsp;	<a href="new/Web Control Deployment.aspx" title="SurveyBox Web Control Deployment " >SurveyBox Web Control Deployment </a>	
    <br />
    <br /><br />


V.	&nbsp;		&nbsp;		&nbsp;		&nbsp;	 <strong>Email Invitation Mailings </strong> <a name="Email" ></a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
5	&nbsp;	5	&nbsp;	1	&nbsp;		&nbsp;	<a href="new/ED_Introduction.aspx" title=" Emailing Introduction " > Introduction </a>	<br />
5	&nbsp;	5	&nbsp;	2	&nbsp;		&nbsp;	<a href="new/Survey Mailing.aspx" title=" Invitation Mailing " > Invitation Mailing </a>	<br />
5	&nbsp;	5	&nbsp;	3	&nbsp;		&nbsp;	<a href="new/Mailing Status.aspx" title=" Mailing Status " > Mailing Status </a>	<br />
5	&nbsp;	5	&nbsp;	4	&nbsp;		&nbsp;	<a href="new/Mailing Log.aspx" title=" Mailing ErrorLog " > Mailing ErrorLog </a>	<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />

VI.	&nbsp;		&nbsp;		&nbsp;		&nbsp;	 <strong>Results & Reporting </strong> 	<a name="Reporting" ></a><br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />

6	&nbsp;	0	&nbsp;		&nbsp;		&nbsp;	<a href="new/Reports_Introduction.aspx" title=" Reporting Introduction " > Reporting Introduction </a>	<br />

6	&nbsp;	1	&nbsp;		&nbsp;		&nbsp;	<a href="new/Graphic Reports.aspx" title=" Graphic Reports " > Graphic Reports </a>	<br />
6	&nbsp;	2	&nbsp;		&nbsp;		&nbsp;	<a href="new/Cross Tabulation.aspx" title=" Cross Tabulation " > Cross Tabulation </a>	<br />

6	&nbsp;	3	&nbsp;		&nbsp;		&nbsp;	<a href="new/Report Filter Creation.aspx" title=" Report Filter Creation " > Report Filter Creation </a>	<br />
6	&nbsp;	4	&nbsp;		&nbsp;		&nbsp;	<a href="new/Report Filter Editor.aspx" title=" Report Filter Editor " > Report Filter Editor </a>	<br />

6	&nbsp;	5	&nbsp;		&nbsp;		&nbsp;	<a href="new/File Manager.aspx" title=" File Manager " > File Manager </a>	<br />

6	&nbsp;	6	&nbsp;		&nbsp;		&nbsp;	<a href="new/Voter report.aspx" title=" Respondent / Voter Report " > Respondent Report </a>	<br />
6	&nbsp;	7	&nbsp;		&nbsp;		&nbsp;	<a href="new/Voter Entries List.aspx" title=" Voter Entries List " > Respondents Entries List </a>	<br />
6	&nbsp;	8	&nbsp;		&nbsp;		&nbsp;	<a href="new/Voter Report Edit.aspx" title=" Voter Report Edit " > Respondent Report Edit </a>	<br />

6	&nbsp;	9 &nbsp;		&nbsp;		&nbsp;	<a href="new/Data Export.aspx" title=" Data Export " > Data Export </a>	<br />
6	&nbsp;	10&nbsp;&nbsp;		&nbsp;	<a href="new/Data Import.aspx" title=" Data Import " > Data Import </a>	<br />

6	&nbsp;	11&nbsp;&nbsp;		&nbsp;	<a href="new/SsrsReports.aspx" title=" Ssrs Reports " > SSRS Reports </a>	<br />


<br /><br />
VII.&nbsp;		&nbsp;		&nbsp;		&nbsp;	<strong>Roles & User Management</strong>	<a name="RolesUsers" ></a><br /><br />
	
7	&nbsp;	1	&nbsp;		&nbsp;		&nbsp; <a name="introduction" /><a href="UM_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />

7	&nbsp;	2	&nbsp;		&nbsp;		&nbsp;	<a href="new/User Manager.aspx" title=" User Manager " > User Manager </a>	<br />
7	&nbsp;	3	&nbsp;		&nbsp;		&nbsp;	<a href="new/User Creation Tool.aspx" title=" User Creation Tool " > User Creation Tool </a>	<br />

7	&nbsp;	4	&nbsp;		&nbsp;		&nbsp;	<a href="new/Roles Manager.aspx" title=" Roles Manager " > Roles Manager </a>	<br />
7	&nbsp;	5	&nbsp;		&nbsp;		&nbsp;	<a href="new/User Import.aspx" title=" User Import " > User Import </a>	<br />

7	&nbsp;	6	&nbsp;		&nbsp;		&nbsp;	<a href="new/User_Account.aspx" title=" User Acount Editing" > User Account </a>	<br />


	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />

VIII.&nbsp;		&nbsp;		&nbsp;		&nbsp;	 <strong>Appendix</strong> 	<a name="Appendix" ></a><br />
	&nbsp;		&nbsp;		&nbsp;		&nbsp;		<br />
	&nbsp;	1	&nbsp;		&nbsp;		&nbsp;	<a href="new/Form Architecture.aspx" title=" Survey Form Architecture " > Survey Form Architecture </a>	<br />
							<br />
									<br />
<b>Note </b> - these helpfiles are a work in progress. Updates and additions will follow gradually.
<br /><div id="fillerDiv" class="fillerDiv">&nbsp;</div>
        </div>

    </asp:Content>
