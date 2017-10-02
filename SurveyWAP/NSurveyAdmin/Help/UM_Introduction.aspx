<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RolesUsers" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">Roles & User Management</h2>
<hr />
Access to the SP™ application features and surveys is determined by ROLES and SURVEYS that are assigned to a USER. A ROLE is a collection of one or more ROLERIGHTS. Every ROLERIGHT is linked to specific application features. A SP™ USER can have one or more ROLES and/ or SURVEYS assigned.
<br /><br />
There are 40 different rolerights to choose and create roles from in any combination. The minimum number of rolerights to select is one, the maximum 40. In certain cases there is a hierarchy of rolerights to take into account when creating roles. 
<br /><br />
Once a ROLE is created and assigned to a SP™ user it will give access to the application features as set by the rolerights. Certain features a roleright gives access to will only become available if one or more surveys are assigned to the user as well. 
<br /><br />
<i>Example:</i> Create a Role named "Example Role" and add (4) rolerights:<br /><br />

1. Helpfile Access 		--> menu option Helpfiles available and features accessible<br />
2. Library Access 		--> menu option Question Libraries available and features accessible<br />
3. Usermanager Access 	--> menu option User administration available and features accessible<br /><br />

4. Form Builder Access 		--> menu option Form Builder available but features not accessible<br /><br />

The first three rolerights will give access to SP™ features that are not directly linked to surveys. The Form builder will only work if one or more surveys are assigned to the user as well. If no survey is assigned a warning message is shown: "No survey selected. Please create or Choose a survey first". 
<br /><br />
Once a survey is assigned to the user and selected(!) when clicking the Form Builder menu option the Formbuilder screen will open and show the survey to edit.
<br /><br />

<b>Hierarchy of rolerighs</b>
<br /><br />
The main roleright that is needed to be able to select a survey and access features that will only work if a survey is selected is the AccessSurveyList roleright. This roleright will give access to the SurveyList menu option and webpage and open the Survey TreeView in the left panel of the application. Any surveys assigned to a user will be shown in the Treeview and the SurveyList to select. Only administrators will see all surveys created through the SP™ application.
<br /><br />
The logic of other combinations of rolerights partly depends on the usertype or usergroups and the intended use of the application by that group.  
<br /><br />
An overview of the different rolerights, related features and menuoptions can be downloaded at .....
<br /><br />

<b>Survey Takers</b><br /><br />

The only exception to the AccessSurveyList roleright hierarchy and the need to select a survey from the surveylist or treeview first before accessing features directly related to a survey is the Survey Taker roleright.
<br /><br />
Survey takers (or voters) are a special group of users that are not supposed to have access to any of the other features of the SP™ application except to take a specific survey (one or more) and vote. 
<br /><br />
After login Survey takers that have multiple surveys assigned will be shown a dropdownlist to select the survey(s) to take. If only one survey is assigned is will be presented by default.
<br /><br />

<b>Roles & Usertypes</b>
<br /><br />
Three main usertypes can be distinguished:
<br /><br />
1- administrators: <br />
* default admin user and all new registered users assigned the administrator rights<br />
* full access to the entire application<br />
* all surveys and rolerights are assigned automatically<br /><br />

2- survey takers/ voters<br />
* registered SP™ users allowed to take one or more surveys based on the SurveyTaker role. <br />
* a SurveyTaker role consists of just the surveytaker roleright.<br />
 * one or more surveys are assigned to the user to take and vote on
 <br /><br />
3- all others:<br />
* all registered SP™ users (username/ password) without full administrator rights<br />
* assigned different roles, rolerights and survey combinations based on the accesslevel needed
<br /><br />
Customised roles can be created to be assigned so a specific group of users or a usertype. E.g.
<br /><br />
<i>Designers</i><br />
- role with rolerights to build and design the survey or webform<br />
<br />
<i>Campaigners</i><br />
- role with rolerights to preview and test surveys and invite surveytakes through mailings
<br /><br />
<i>Report Viewers</i><br />
- role with rolerights to view surveysresults, reports and import/ export data
<br /><br />
<img alt="roles and users" src="Images/AccesAuthorisation.gif" title="Acces & Authorisation" />
<br /><br />

 <hr style="color:#e2e2e2;" />

<h3>More Information</h3><br />

<a href="new/User Manager.aspx" title=" User Manager " > User Manager </a>	<br />
<a href="new/User Creation Tool.aspx" title=" User Creation Tool " > User Creation Tool </a>	<br />

<a href="new/Roles Manager.aspx" title=" Roles Manager " > Roles Manager </a>	<br />
<a href="new/User Import.aspx" title=" User Import " > User Import </a>	<br />


<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
