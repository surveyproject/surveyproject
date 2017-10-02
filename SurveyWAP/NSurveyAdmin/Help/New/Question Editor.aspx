<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Question Editor</h2><hr style="color:#e2e2e2;" />
           
With the question editor the configuration and constraints of a survey 
question are determined.<br />
<br />
If answers need to be added to the question the
answer editor (click the add/ edit answers button) is used after creating the question initially.<br />
<br />
A special 'paging menu' of text links is available at the top of the Edit Question page to add/ clone questions and move back and forth on the survey form
                more efficiently without having to switch back to the main Form builder.

<br /><br />
<u>Question Options</u>

<br />
<br />
<i>* Question Editor </i><br /> - WYSYWIG HTML/ Text editor to write and edit the question text and layout.

<br />
<br />
<i>* Edition Language </i><br /> - available if the Multilanguage feature is enabled. Option to choose and switch the languages to write/ edit
                questions for different survey language versions.                
<br />
<br />
<i>* ID </i><br /> - Option to add an ID manually that will be saved to the database and can be used in (reporting) queries. The ID does 
               not show on the survey.
<br />
<br />
<i>* Alias </i><br /> -Option to add an Alias manually that will be saved to the database and can be used in (reporting) queries. The Alias does 
               not show on the survey.
<br />
<br />
<i>* Help Text </i><br /> - Option to write and add a helptext to the question. If activated (Show Help Text checkbox) the helptext will show on the 
                survey as part of the question but with a separate (editable) layout.

<br />
<br />
<i>* Show Help Text </i><br /> - Option to show or hide the Help Text on the survey form.
                
                <br />
<br />
<i>* Selection Mode </i><br /> - is the mode in which the answers will
  be rendered on the surveyform.<br />

<br />
  o The radio button mode - <br />will show answer options to the
  question as radio buttons. The respondent can select only one answer in
  this mode.<br />


<br />
 o  The checkbox mode <br />will show answers options to the
  question as checkboxes. The respondent can select multiple answers in
  this mode.
<br />
<br />
 o The dropdown list mode <br /> will group and show answers options inside a dropdown list. The respondent can select only one answer in
  this mode.<br />
<br />
<i>* Display Mode </i><br /> -  is the layout of the answer items of the
  question. Options to choose are to order the layout in a vertical manner
  or to order the layout in a horizontal manner.<br />
<br />
<i>* Number Of Columns </i><br /> - the number columns into which the question layout and answeritems 
  will be split<br />
<br />
<i>* Randomize Answers Order </i><br /> -  an option to present the answer's on the survey in a
  random order to each respondent to avoid the &quot;order bias effect&quot;.<br />
<br />
<i>* Rating </i><br /> - option to activate the answers rating / scaling options as shown on  
 the question's Selection / Answer Types settings page. To know more about rating and
  scaling read the <a href="Rating_Introduction.aspx" title=" Rating Introduction " > Rating Introduction </a>.<br /><br />
                Note: On selecting the Rating opion Group and Sub Group drop downlists will appear. Go to 
                <a href="QuestionGroups.aspx" title=" Question Groups" >Question Groups </a> for more information.	

                <br />
<br />
<i>* Min. Selection Required </i><br /> -  the minimum number of answers that are mandatory to
  select in the question. Only "Selection" Answer Types are calculated in
  the selection number count, to make Field Answer Types
  mandatory it can be done by checking the mandatory checkbox on the answers options page.<br />
<br />
<i>* Max. Selection Allowed </i><br /> -  the maximum of answers that can be selected
  in the question. Only Selection Answer Types are calculated in the
  selection number count, to make Field Answer Types mandatory
  it can be done by checking the required field in the answers options.<br />
<br />
<i>* Pipe Alias </i><br /> - field to specify an alias that can be used to 'pipe' a respondent's
  answers in subsequent questions labels or answers labels. To
  learn more about piping and pipe alias read the
  <a href="AP_Introduction.aspx" title=" Piping Introduction " > Answer Piping Introduction </a>.

                <br />
<br />
<i>* Export XML Button </i><br /> - option to create an XML file of the current Question that can be exported and saved to a directory on the computer for later use in another survey.

                <br />
<br />
<i>* Repeatable Sections </i><br /> - option to allow repeating/ adding multiple answers/entries to the same question. Read the <a href="Repeatable_Introduction.aspx" title=" Introduction " >Repeatable Answers Introduction </a> for more information.
  <br /><hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
                <a href="Insert Question.aspx" title=" Insert Question " > Insert Question </a>	<br />
<a href="AT_Introduction.aspx" title="Answer Types Introduction " >Answer Types Introduction </a>	<br />
<a href="AP_Introduction.aspx" title=" Piping Introduction " >Answer Piping Introduction</a><br />
<a href="Rating_Introduction.aspx" title=" Rating Introduction " > Rating Introduction </a><br />
                	<a href="QuestionGroups.aspx" title=" Question Groups" >Question Groups </a>	<br />
	<a href="Repeatable_Introduction.aspx" title=" Repeatable Answer Introduction " >Repeatable Answers Introduction </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

