<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Survey Form Builder</h2><hr style="color:#e2e2e2;" />
               
The form builder is at the heart of the Survey&trade; Project webapplication as it is used to create and edit
the surveys and forms that are presented to survey takers (respondents) and endusers.<br />
<br />
By default SP&trade; has a wide range of questions types and answer items
to build forms and surveys. The modular architecture of the application also makes it possible to develop (in ASP.NET/ C#) and add new or customised 
anwertypes.<br /><br />
<u>Form Options</u><br />
<br />
<i>* Preview Form in:</i> <br />- a dropdown list to select the Language in which to edit or preview the form .<br />
<br />
  The dropdownlist will only show if the survey is set to use the multilanguage features (surveys/settings/multi languages).<br />
<br />
<u>Page Options</u><br />
<br />
<i>*  Arrow Page Up: </i><br />- will move the page up until it reaches the next page break above.<br />
<i><br />
*  Arrow Page Down: </i><br />- will move the page down until it reaches the next page break below.<br />
<br />
<i>* Delete: </i><br />- will delete the page. <br />
<br />
<i>* Insert Question:  </i><br />- inserts a new question at the end of the page.
  If wanting to insert a question before another one the
  insert question link at the question options level can be used.<br />
<br />
<i>* Insert Line Break:</i> <br />- insert a new line break at the end of the page.
  Behind the scene a line break is just a Static Question that
  shows an HTML [hr] line break tag.<br />
<br />
<i>* Enable Random: </i><br />- enables the page to show the questions inside it
  in a random order each time the survey is shown.<br />
<br />
<i>* Edit Branching: </i><br />- allows setting up Branching Conditions. In case of multiple pages the respondent can be redirected to a defined page
  based on the answers. This option is only available in case of 
  multiple pages in the form.<br />
<br />
<i>* Enable Submit: </i><br />- allows to show the submit button on the page instead
  of showing a next page button. This option is only available on
  multiple pages forms. Its generally used to finish the survey on a
  given page where a respondent was redirected using branching.<br />
<br />
<u>Question Options</u><br />
<br />
<i>* Arrow Page Up: </i><br />- will move a question's position up.<br />
<br />
<i>* Arrow Page Down: </i><br />- will move a question's position down.<br />
<br />
<i>* Edit Question</i> <br />- depending on the type of question it will be possible to use
  either the Question%20Editor.html or to the
  Matrix%20Question%20Editor.html.<br />
<br />
<i>* Edit Answers / Edit Matrix Layout</i> <br />- depending on the type of question
  it will be possible to add or edit new Answers using the
  Answers%20Editor.html or to add / edit the matrix rows &amp; columns
  through the Matrix Question Editor.<br />
<br />
<i>* Delete </i><br />-the question, its answers and all related respondent answers.
  Note that it's not possible to recover the question or its answers
  afterward.<br />
<br />
<i>* Clone</i><br />- makes an exact copy of the question.<br />
<br />
<i>* Insert Question </i><br />-inserts a new question before the current
  question. If its needed to insert a question at the end of the page the insert question link at the page options level can be used.<br />
<br />
<i>* Insert Page Break</i> <br />-inserts a page break before the current question.<br />
<br />
<i>* Insert Line Break</i><br />- inserts a new line break before the current
  question. Behind the scene (html code) a line break is just a
  <a href="Static Question.aspx" title=" Introduction " > Static Question </a> that shows an HTML [hr] line break tag.<br />
<br />
<i>* Skip logic</i><br />- Option to create Skip Logic conditions to hide the
  question based on a respondent's answers on the previous pages. This option
  is only available if there are multiple pages in the form.<hr style="color:#e2e2e2;" />
           
                <h3>
                    More Information</h3>
                <br />
<a href="SCE_Introduction.aspx" title=" Introduction " > Introduction </a>	<br />
<a href="Insert Question.aspx" title=" Insert Question " > Insert Question </a>	<br />
<a href="Condition_Introduction.aspx" title=" Introduction " >Conditions Introduction </a>	<br />
<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
<a href="Matrix Question Editor.aspx" title=" Matrix Question Editor " > Matrix Question Editor </a>	<br />
<a href="Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>	<br />
<a href="Form Architecture.aspx" title=" Form Architecture " > Form Architecture </a>	<br />
<a href="ML_Introduction.aspx" title=" Multi-Languages Introduction " > Multi Language Introduction </a>	<br />
             
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

