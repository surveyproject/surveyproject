<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Insert Question</h2><hr style="color:#e2e2e2;" />
    
The insert question link enables the insertion of a new question into the survey/
form. It will open the New Question editor. Three types of questions are available :<br />
<br />
* Single Question (single/ multiple selection and all other)<br />
* Matrix Question (multiple columns and rows)<br />
* Static Question (static text only)<br />
<br />
                <u>ID</u><br />Option to add an ID manually that will be saved to the database and can be used in (reporting) queries. The ID does 
               not show on the survey.
                <br /><br />
                <u>Question </u><br />WYSYWIG HTML/text editor to write and edit the question text and layout.
                <br /><br />
<u>Copy Existing Question</u><br />
SP&trade; has the option to copy an existing question into the current survey
either from another survey already created or from a Question Library<br />
<br />
<u>Import XML Question</u><br />
It is also possible to import an existing question from an Xml file that
was exported earlier using the export feature of the question editor.<br />

<br /><hr style="color:#e2e2e2;" />
        
                <h3>
                    More Information</h3>
                <br />                <a href="SCE_Introduction.aspx" title=" Introduction " > Form Builder Introduction </a>	<br />
<a href="Survey Form Builder.aspx" title=" Survey Form Builder " > Survey Form Builder </a><br />

<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
<a href="Matrix Question Editor.aspx" title=" Matrix Question Editor " > Matrix Question Editor </a>	<br />
<a href="Single Question.aspx" title=" Answers Editor " >Single Question </a>	<br />
                <a href="Matrix Question.aspx" title=" Answers Editor " >Matrix Question </a>	<br />
                <a href="Static Question.aspx" title=" Answers Editor " >Static Question </a>	<br />

                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

