<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Survey Designer & Form Builder</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The form builder or survey designer is at the heart of the Survey Project tool as it is used to create and edit
the surveys and forms that are presented to survey takers (respondents) and endusers.<br />
<br />
By default Survey Project offers many different questions types and answer items
to build forms and surveys. The modular architecture of the application also makes it possible to develop (asp.net/ C#) and add customised 
anwertypes.<br /><br />
<u>Form Options</u><br />
<br />
* Preview Form in: a dropdown list to select the Language in which to edit or preview the form .<br />
<br />
  The dropdownlist will only show if the survey is set to use the multilanguage features (surveys/settings/multi languages).<br />
<br />
<u>Page Options</u><br />
<br />
*  Arrow Page Up: will move the page up until it reaches the next page break above.<br />
<br />
*  Arrow Page Down: will move the page down until it reaches the next page break below.<br />
<br />
* Delete: will delete the page. <br />
<br />
* Insert Question:  inserts a new question at the end of the page.
  If wanting to insert a question before another one the
  insert question link at the question options level can be used.<br />
<br />
* Insert Line Break: insert a new line break at the end of the page.
  Behind the scene a line break is just a Static Question that
  shows an HTML [hr] line break tag.<br />
<br />
* Enable Random: enables the page to show the questions inside it
  in a random order each time the survey is shown.<br />
<br />
* Edit Branching: allows setting up Branching Conditions. In case of multiple pages the respondent can be redirected to a defined page
  based on the answers. This option is only available in case of 
  multiple pages in the form.<br />
<br />
* Enable Submit: allows to show the submit button on the page instead
  of showing a next page button. This option is only available on
  multiple pages forms. Its generally used to finish the survey on a
  given page where a respondent was redirected using branching.<br />
<br />
<u>Question Options</u><br />
<br />
* Arrow Page Up: will move a question's position up.<br />
<br />
* Arrow Page Down: will move a question's position down.<br />
<br />
* Edit Question depending on the type of question it will be possible to use
  either the Question%20Editor.html or to the
  Matrix%20Question%20Editor.html.<br />
<br />
* Edit Answers / Edit Matrix Layout depending on the type of question
  it will be possible to add or edit new Answers using the
  Answers%20Editor.html or to add / edit the matrix rows &amp; columns
  through the Matrix Question Editor.<br />
<br />
* Delete the question, its answers and all related respondent answers.
  Note that it's not possible to recover the question or its answers
  afterward.<br />
<br />
* Clone makes an exact copy of the question.<br />
<br />
* Insert%20Question.html inserts a new question before the current
  question. If its needed to insert a question at the end of the page the insert question link at the page options level can be used.<br />
<br />
* Insert Page Break inserts a page break before the current question.<br />
<br />
* Insert Line Break inserts a new line break before the current
  question. Behind the scene a line break is just a
  Static%20Question.html that shows an HTML [hr] line break tag.<br />
<br />
* Skip logic let us creates Skip%20Logic%20Conditions.html to hide the
  question based respondent's answers on the previous pages. This option
  is only available if there are multiple pages in the form. <br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Insert%20Question.html<br />
Skip%20Logic%20Conditions.html<br />
Question%20Editor.html<br />
Matrix Question Editor<br />
Answers%20Editor.html<br />
Form Architecture<br />
ML_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

