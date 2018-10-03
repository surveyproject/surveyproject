<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RolesUsers" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Roles Manager</h2><hr style="color:#e2e2e2;" />

This Roles Manager page is used to create and edit roles that can be assigned to SP Users. A role consists of one or more rolerights that give access to specific
                features or webpages of the application.<br />
<br />
* Select Role to Edit/ View - list of exising roles to edit
* Create New Role - button to start creating a new role                

<br /><br />
* Role Name - is the name of the role. <br />
<br />
* Role's Rights - list of rights that will be added to the role if selected.<br />
<br />
* Create New Role - button to create and save the new role including selected rolerights
<br /><br />
<u>List of Rolerights</u>
                <br /><br />
1	&nbsp;	Create survey	&nbsp; = &nbsp;	Authorisation to access the options to create a new survey	<br />
2	&nbsp;	Delete survey	&nbsp; = &nbsp;	Authorisation to access the options to delete surveys	<br />
3	&nbsp;	View Survey settings	&nbsp; = &nbsp;	Authorisation to access the survey settings options (no edit rights!)	<br />
4	&nbsp;	XML survey export	&nbsp; = &nbsp;	Authorisation to access the option to export a survey as xml	<br />
5	&nbsp;	Change Survey settings	&nbsp; = &nbsp;	Authorisation to access the survey settings options and save changes	<br />
6	&nbsp;	Clone (copy) survey	&nbsp; = &nbsp;	Authorisation to access the option to clone (copy) a survey	<br />
7	&nbsp;	Completion Access	&nbsp; = &nbsp;	Authorisation to access the survey completion options	<br />
8	&nbsp;	Security access	&nbsp; = &nbsp;	Authorisation to access the survey security options	<br />
9	&nbsp;	Survey Statistics access	&nbsp; = &nbsp;	Authorisation to access the survey statistics options	<br />
10	&nbsp;	Reset all survey votes	&nbsp; = &nbsp;	Authorisation to access the survey statistics  - reset votes option	<br />
11	&nbsp;	Delete unvalidated entries	&nbsp; = &nbsp;	Authorisation to access the survey statistics  - delete unvalidated votes option	<br />
12	&nbsp;	Form builder access	&nbsp; = &nbsp;	Authorisation to access the survey Designer - formbuilder	<br />
13	&nbsp;	Answer type editor access	&nbsp; = &nbsp;	Authorisation to access the Answer Types Editor	<br />
14	&nbsp;	Reports access	&nbsp; = &nbsp;	Authorisation to access the Results – Reports options	<br />
15	&nbsp;	Create report results filter	&nbsp; = &nbsp;	Authorisation to access the Results – Reports – Filter options	<br />
16	&nbsp;	Individual Responses access	&nbsp; = &nbsp;	Authorisation to access the Results – Individual Responses options	<br />
17	&nbsp;	Edit respondent entries	&nbsp; = &nbsp;	Authorisation to access the Results – Individual Responses – Edit options	<br />
18	&nbsp;	Delete respondent entries	&nbsp; = &nbsp;	Authorisation to access the Results – Individual Responses – Edit & Delete options	<br />
19	&nbsp;	Cross tabulation report access	&nbsp; = &nbsp;	Authorisation to access the Results Reports Cross Tabulation option	<br />
20	&nbsp;	Export response data access	&nbsp; = &nbsp;	Authorisation to access the Results Data Export options	<br />
21	&nbsp;	Mailing access	&nbsp; = &nbsp;	Authorisation to access the Campaigns – Mailing options	<br />
22	&nbsp;	Preview and Weblink access	&nbsp; = &nbsp;	Authorisation to access the Campaigns – Web Links – SurveyForm Control info	<br />
23	&nbsp;	User manager access	&nbsp; = &nbsp;	Authorisation to access the Accounts – Userlist options	<br />
24	&nbsp;	Question Library access	&nbsp; = &nbsp;	Authorisation to access the Designer – Question Library options	<br />
25	&nbsp;	Insert questions from library	&nbsp; = &nbsp;	??	<br />
26	&nbsp;	Insert questions from survey	&nbsp; = &nbsp;	??	<br />
27	&nbsp;	Manage Question library	&nbsp; = &nbsp;	Authorisation to access the Designer – Question Library – Edit library options	<br />
28	&nbsp;	Create SQL answer types	&nbsp; = &nbsp;	Authorisation to access the Designer – Answer Type Editor – SQL type options	<br />
29	&nbsp;	Xml Reponses import	&nbsp; = &nbsp;	??	<br />
30	&nbsp;	Take Survey access	&nbsp; = &nbsp;	Authorisation to access the Campaigns – Take survey options	<br />
31	&nbsp;	RegEx editor access	&nbsp; = &nbsp;	Authorisation to access the Designer – Regular Expression options	<br />
32	&nbsp;	File manager access	&nbsp; = &nbsp;	Authorisation to access the Results  - File Manager options	<br />
33	&nbsp;	Export files to server	&nbsp; = &nbsp;		<br />
34	&nbsp;	Multi-languages access	&nbsp; = &nbsp;	Authorisation to access the Surveys – Settings – Multilanguages options	<br />
35	&nbsp;	Helpfiles access	&nbsp; = &nbsp;	Authorisation to access the Help menu options	<br />
36	&nbsp;	Question Groups access	&nbsp; = &nbsp;	Authorisation to access the Designer – Question Groups options	<br />
37	&nbsp;	DataImport	&nbsp; = &nbsp;	-	<br />
38	&nbsp;	Token Security	&nbsp; = &nbsp;	Authorisation to access the Surveys – Security – Token security options	<br />
39	&nbsp;	Survey Layout	&nbsp; = &nbsp;	Authorisation to access the Designer – Layout and CssXml options	<br />
40	&nbsp;	Survey List access	&nbsp; = &nbsp;	Authorisation to access the Surveys – SurveyList options	<br />
41	&nbsp;	User Reponses access	&nbsp; = &nbsp;	-	<br />
42	&nbsp;	SSRS Reports access	&nbsp; = &nbsp;	Authorisatino to access the Results – Reports – SSRS report options	<br />
                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
<a href="User Manager.aspx" title=" User Manager " > User Manager </a>	<br />
<a href="User Creation Tool.aspx" title=" User Creation Tool " > User Creation Tool </a>	<br />

<a href="../UM_Introduction.aspx" title=" User Manager Introduction " > User Manager Introduction </a>	<br />
<a href="User Import.aspx" title=" User Import " > User Import </a>	<br />
                <a href="User_Account.aspx" title=" User Account Editing " > User Account Editing </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

