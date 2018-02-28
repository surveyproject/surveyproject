<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SelectionTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    'Selection - Other' answertype</h2><hr style="color:#e2e2e2;" />
      
                
The way 'Selection' Answer types will render on the survey form depends on the 'selection'-mode that was
chosen in the question editor. The Question level 'selection'-mode determines if  
the answerstype 'Selection - Text' and 'Selection - Other' will be rendered as a radio button, a checkbox or grouped in a dropdown list.<br />
<br />
Besides the default radiobutton, checkbox, dropdownlist the 'Selection - Other' answertype also renders an 'additional' text field box. 'Other' texboxes
                are often used to add a comment in case none of the selection options apply or to add some additional comment to a choice.


<br /><br />
<u>'Selection - Other' - AnwerEditor Settings</u><br />
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
<i>* Image UR</i><br /> -  option to enter an image URL
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show an image instead of
  the answer text. <br />
<br />
<i>* Radio button CSS </i><br /> - A special option is available to determine the CSS class used on 'Selection' Answertypes. 
                Besides making use of the CSS/XML list its possibe to set the CSS on the DIV tag surrounding individual Selection Answertypes through the Answer Editing form. 
                <br /><br />When a Selection Answertype is chosen from the Type DropDownList the 'Radio Button CSS' option is shown to enter a CSS Class/ selector as defined in the CSS files.
                If the Selection Answer CSS textbox is left empty but the 'Alias' textbox is filled with the name of any existing CSS class the particular CSS class/ selector will be applied to the Answer DIV. 

<br /><br />


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

<i>* Selected by Default </i><br /> - option to have the selection item selected / checked by default <br />
<br />

<i>* Rating Part </i><br /> - option to let this selection be part of the
  rating / scale calculation in the reports. To learn more about rating
  please read the <a href="Rating_Introduction.aspx" title=" Rating Introduction " > Rating Introduction </a> .<br />
<br />
  This feature is only available if the rating option is set on question level for the answer type.<br />
<br />
<i>* Score Point </i><br /> - option to specify a score value if the answer is selected.
  This score will be used to calculate the question's score and the overall score of the respondent on the survey.<br />
<br />
  This feature is only available if in the Survey's settings the Score option is enabled. See <a href="../Score_Introduction.aspx" title=" Score Introduction " > Score Introduction </a>	.<br />
<br />

                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
                <br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " > Answer Types Introduction </a><br />
<a href="SelectionT_Introduction.aspx" title=" Selection Answer Types Introduction " > Selection Answer Types Introduction </a>	<br />
<a href="Selection - Text.aspx" title=" Selection - Text " > Selection - Text </a>	<br /> 
                <a href="../Style_Introduction.aspx" title=" Layout & Style Introduction " > Layout & Style Introduction </a>	<br />
                <a href="Rating_Introduction.aspx" title=" Rating Introduction " > Rating Introduction </a>	<br />
                <a href="../Score_Introduction.aspx" title=" Score Introduction " > Score Introduction </a>	<br />
                <a href="AP_Introduction.aspx" title=" Piping Introduction " > Answer Piping Introduction </a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

