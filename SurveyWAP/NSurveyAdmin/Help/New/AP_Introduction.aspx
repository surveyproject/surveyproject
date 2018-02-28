<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Piping" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
Question & Answer Piping Introduction</h2><hr style="color:#e2e2e2;" />
            

Question & Answer Piping is the option to insert or "pipe" answertext from a previous question into a question and/ or answer on a later page in the survey.
                 <br /> <br />
<i>An example of how to set up a survey using Question & Answer Piping:</i>
                <br /><br />
Page 1 <br />
                Question1: In what country do you currently live?
                <br />
Answertype: Field - Basic <br />
                Answer Text: My Country <br />
                Pipe Alias: <b>mycountry</b>
                <br /><br />
Page 2<<br />
                Question2: How satisfied are you with the following aspects of <b>[[mycountry]]</b> ?
                <br />
Matrix question type (optional Rating scale columns 1 - 4)
                <br /><br />
Matrix Rows:<br />
o Environmental protection<br />
o Safety<br />
o Education<br />
o Costs of living<br />
<br />
If the respondent answers "The Netherlands" for Question1, "The Netherlands" will replace <b>[[mycountry]]</b> in the follow-up question text. 
For that specific respondent, Question2 would then read "How satisfied are you with the following aspects of <b>The Netherlands</b>?"
<br /><br />
<u>Adding Piping</u>
<br /><br />
Adding piping requires a sender question-answer with a "pipealias" and one or more receiver questions and/or answers on a later page. 
                A sender question is a question whose answer is inserted by making use of a pipingalias into a later receiver question 
                or answeroption in the survey.
                <br /><br />
Piping is activated when the respondent clicks the Next button to go to the next page. Therefore a receiver question can not be on the same page as the associated sender question.
                <br /><br />
Only add piping after the survey design is complete. Piping will not dynamically update if questions 
                are added or if the order of the questions is changed after adding the piping features.
<br /><br />
<u>PipeAlias</u>
                <br /><br />
                To be able to pipe and send an answer to a receiver question/ answer a pipealias must be created through the answereditor of the sending question. 
                The pipealias must be unique for a particular survey. Otherwise any text without spaces (sic!) (letters/ numbers/ special characters) can be used to create a pipealias.

                <br /><br />
<u>'Sender Question' Answer Types</u>
                <br /><br />
You can pipe answer text from the following Answer Types:<br /><br />

- all Field Answer types<br /><br />
Remarks:<br />
* When using the Hidden field: add a default answer to pipe<br />
* When using the Password field: the (secret?!) password will be visible on the receiver question<br />
                <br />
- Selection Other: the Other text is piped<br />
- XML (country/ US States) list: the XML value is piped<br />

                <br />
Answer text can be piped into the questiontext of all question types. Answertext can be piped into the "answertext" and "default text value" fields if available on the answereditor screen. 
                <br /><br />
Answers can be piped into the Row and Column names of Matrix questions. It's not possible to pipe into the Matrix answeroptions.                                <br />
                <br />

<u>To pipe answer text into a following question and/or answer</u>
                <br /><br />
In the Survey Formbuilder click Edit Question for the question to be customized with piped text. This must be a question on a page that comes after the sender question page.
Click the question text into which a previous answer should be piped and leave the cursor in the position where the piped text should be inserted.
<br /><br />
Next type the "pipealias" of the sender questions answer and put it between Square Brackets e.g. [[Question1]]
<br /><br />
When respondents take the survey, the "bracketed template"  will be replaced by the respondent's answer to the sender question.
                <br /><br />
                To pipe answertext into the answer of a receiving question open the questions answereditor and add the pipealias including square brackets in the 'answertext' or 'default answer value' field. 
                <br /><br />
<u>Piping and Page Randomization</u>
<br /><br />
Question and Answer Piping is also supported when 'Page Randomization' is active and applied to either the page that contains the sender question, or the page that contains the receiver question.
                <br /><br />
<u>Survey Testing</u>
<br /><br />
Before a survey is sent out, preview and test the survey to see what it will look like to respondents.
If a respondent skips the sender question, the 'bracketed' template won't show in the receiver questions. It is replaced by a blank space. 
               The sender question can be made 'mandatory' to avoid incomplete sentences or blank spaces.

<br /><br />
                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
                <a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
                <a href="Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>	<br />
                <a href="AT_Introduction.aspx" title=" Answer Types Introduction " > Answer Types Introduction </a>	
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

