<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.HelpFiles" Codebehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyManagement" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
        <h2 style="color:#5720C6;">Survey List & Directory</h2><hr style="color:#e2e2e2;" />
   
        The survey list and directory structure is used to search and save multiple surveys in a structured way. Survey list and directory 
                are the starting point for most actions and features of SP&trade;
                <br /><br />Before opening any of the other webpages and menuoption a survey has be 'activated' to which the actions will apply. 
                By selecting one of the surveys in the directory structure it is set to 'active'.
                <br /><br />One survey can be set to 'active' by 'default' through the Settings page (option: Default Survey). If no other survey is
                selected or on loging in to SP&trade; any actions or webpages will apply to the 'Default Survey'. 
                 <br /> <br />
                There can be only one Default survey at a time. The 'active' (or default) survey can be recognized in the directory structure
                by its blue highlighted color.

                <br /> <br />
                <i>Note:</i> the term 'active' (for editing inside SP&trade;) should not be confused with the 'active/ inactive' state of a survey
                to make it accessible to respondents (see Settings page - Active option).


                  <br /> <br />
               <u> Survey List </u><br /><br />

                <i>* Search </i><br /> - search box to find existing and saved surveys by name. Only part of the name can be used to find a survey.
                Wildcard characters (e.g. *) do not apply. Search results are shown in the surveylist table.
                 <br /><br />

                <i>* Sort Orer </i><br /> - the sort order of the survey list table can be changed by clicking the (grey) up or down triangles in the
                column headers of the table. Sorting is possible by Survey Title, Status, Number of Votes and Date created.
                 <br /><br />

                <i>* Title </i><br /> - Survey Title as entered on creating the survey. Can be changed in the Settings page. When the Survey Title (link) is clicked 
                it will open the survey's Statistics page.
                 <br /><br />
                <i>* Status </i><br /> - Active or Inactive status of the survey as determined in the Settings page. If set to inactive the survey can not be accessed by respondents.
                 <br /><br />
                <i>* Votes </i><br /> - Number of votes or responses to the survey as submitted.
                 <br /><br />
                <i>* Created </i><br /> - Creation date of the survey. Set when first adding the new survey by adding a surey name.
                 <br /><br />
                <i>* Settings </i><br /> - Setting Icon to go to the survey's (general) Settings page when clicked.
                 <br /><br />
                <i>* Security </i><br /> - Security Icon to go to the survey's Security page when clicked.
                 <br /><br />
                <i>* Form </i><br /> - Form Builder Icon to go to the survey's Form Builder page when clicked.
                 <br /><br />

               <u> Survey Directory </u><br /><br />
                <i>* Active/ Default Survey</i><br /> - The active or default survey can be recognized by the blue hightlighted color. Click a survey to 
                set it to active.
                 <br /><br />

                <i>* Mouse Menu Option</i><br /> - By right clicking inside the directory structure (maps or surveys) new menu options will show that can 
                be applied to the selected object.
                 <br /><br />

                <i>* Drag & Drop Option</i><br /> - Surveys and directory maps can be moved around the directory structure by dragging and dropping 
                the objects with the mouse. E.g. drag a survey from the root map into a subdirectory. Drag a subdirectory into another map. 
                Not all drag actions are 'allowed'. E.g. Dragging a survey with a duplicate name into a directorymap where the similar survey already exists.
                 <br /><br />


                <i>* New Directory Map</i><br /> - Option to add a new directory map.
                 <br /><br />

                 <i>* Add/ Delete/ Rename Directory Map</i><br /> - Option to add a subdirectory, delete the existing map or rename it.
                 <br /><br />

                 <i>* Delete Survey</i><br /> - Option to delete a survey. This will also delete all results/ reponses to the survey.
                 <br /><br />
               <i>Note:</i> The Root directory can never be deleted.

                
                <br /><br />
                <hr style="color:#e2e2e2;" />

        <h3>More Information</h3><br />
        <a href="Survey%20General%20Settings.aspx">Survey General Settings</a><br />
                <a href="Statistics.aspx" title=" Survey Statistics " > Survey Statistics </a>	<br />
                <a href="Survey Security.aspx" title=" Survey Security " > Survey Security </a>	<br />
                <a href="new/Survey Form Builder.aspx" title=" Survey Form Builder " > Survey Form Builder </a>	<br />
        </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
