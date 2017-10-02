<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Misc" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Boolean Answertype</h2><hr style="color:#e2e2e2;" />
                       
This answertype renders a checkbox. If the checkbox is checked the value
&quot;true&quot; is saved as an answer, if its not check the value &quot;false&quot;
is saved as an answer (table vts_tbvoteranswers). These values are also shown on the individual reports and the resultsreport.<br />
<br />
The 'Selection - Text' answertype with the questiontype Checkbox (Multiple) mode will also render checkboxes on the surveyform.
Answers to these questions are not saved as 'true' or 'false'. 

<br /><br />
<u>Boolean Answertype Settings</u><br />
<br />
<i>* Type </i><br /> -  option to change the current answer type of the answer to add to the question. Respondent answers already collected will not be deleted if
  the answer type is changed after submissions have been made.<br />
<br />
<i>* ID </i><br /> - Option to add an ID manually that will be saved to the database and can be used in (reporting) queries. The ID does 
               not show on the survey.
<br />
<br />
<i>* Alias </i><br /> -Option to add an Alias manually that will be saved to the database and can be used in (reporting) queries. The Alias does 
               not show on the survey.
<br />
<br />
<i>* Answer Text </i><br /> - the text that will be shown next to the selection item (radio, checkbox) or inside the dropdown list. 
                <br />
<br />
<i>* Image URL</i><br /> -  option to enter an image URL
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show an image instead of
  the answer text. <br />
<br />


<i>* Default Text Value </i><br /> - option to set a default value 'true' or 'false' depending on whether the checkbox has to be
                checked by default (true) or not (false). It is also possible to fill the checkbox with a default
  value coming from an external source using one of the following tags :<br />
<br />

 <code> ##yourquerystringvariablename## </code>-  will set the default text with a query
  string variable's value.<br />
  <code>@@yoursessionvariablename@@ </code>- will set the default text with a session
  variable's value.<br />
  <code>&amp;&amp;yourcookievariablename&amp;&amp; </code>- will set the default text with a cookie
  variable's value.<br />
  <code>%%servervariablename%% </code>- will set the default text with a server side
  variable's value.<br />
<br />

<i>* Pipe Alias </i><br /> - option to specify an alias that we can use in other
  questions by 'piping' the textbox field value. To learn about piping and pipe alias read hte <a href="AP_Introduction.aspx" title=" Piping Introduction " > Answer Piping Introduction </a>.
                <br />
<br />            
<hr style="color:#e2e2e2;" /> <h3>More Information</h3><br />

                <a href="AT_Introduction.aspx" title="Answer Types Introduction " > Answer Types Introduction </a>	<br />
                <a href="../FieldT_Introduction.aspx" title="Field AnswerTypes Introduction " > Field Answertypes Introduction </a>	<br />
                <a href="SelectionT_Introduction.aspx" title=" Selection AnswerTypes Introduction " > Selection Answertypes Introduction </a>	<br />
<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

