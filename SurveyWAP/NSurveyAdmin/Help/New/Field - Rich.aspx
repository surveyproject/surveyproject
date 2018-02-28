<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#FieldTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Field - Rich</h2><hr style="color:#e2e2e2;" />
          

This type renders a 'rich' textbox that supports both Text and HTML editing by making use of the
                <a href="http://www.ckeditor.com" target="_blank">CKEditor</a>
component. There is no maximum length limit by default. If changes are required of the size of the field or
its properties it is possible to create a custom field answertype using the Answer Type Creator.<br />
<br />
This field has no additional client side validation checks.<br />
<br />

<u>Extended Answertype Settings</u><br /><br />
Extended answertype settings are additional properties of an answertype that are
only available and become visible once an answertype has been selected and added to the question.<br />
<br />
To open and edit the 'extended settings' of an answer click the edit
button on the answer overview once the answer is added to the question or after changing an existing answertype to a new one by clicking the update button.
<br /><br />
* <i>TextBox Style</i> <br /> -the graphical template that will be used to show the
  HTML options of the textbox.<br />
<br />
* <i>Break Mode</i> <br /> - the way line breaks are rendered in HTML inside the textbox.<br />
<br />
* <i>Language</i> <br /> -the language used to display the HTML options.<br />
<br />
* <i>ToolBar Layout</i> <br /> - option to customize the toobar elements that the
  respondent can use to edit text/HTML.<br />
<br />

<u>Field Rich Answertype Settings</u><br />
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


<i>* Default Text Value </i><br /> - option to set a default value inside the
  textbox field. It is also possible to fill the textbox with a default
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

<i>* RegEx Server Side Validation </i><br /> - option to validate (check) the content of the
  textbox field using a 'regular expression'. Several regular expression
  validations are provided out of the box and it is possible to create a 
  new one using the regular expression editor. When answering the survey the validation is done server-side.<br />
<br />
<i>* Mandatory </i><br /> - option to determine if writing an answer in the textbox field is required. When answering the surey this check is done server-side.<br />
<br />


<i>* Pipe Alias </i><br /> - option to specify an alias that we can use in other
  questions by 'piping' the textbox field value. To learn about piping and pipe alias read hte <a href="AP_Introduction.aspx" title=" Piping Introduction " > Answer Piping Introduction </a>.
                <br />
<br />            
<hr style="color:#e2e2e2;" /> <h3>More Information</h3><br />

                <a href="AT_Introduction.aspx" title=" Introduction " > Answer Types Introduction </a>	<br />
                <a href="Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>	<br />
                <a href="Answer Type Creator.aspx" title=" Answer Type Creator " >Answer Type Creator </a>	<br />
                <a href="../FieldT_Introduction.aspx" title=" Introduction " > Field Answertypes Introduction </a>	<br />
<br />            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

