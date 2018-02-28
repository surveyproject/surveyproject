<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#XmlBoundTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Xml - US States</h2><hr style="color:#e2e2e2;" />
    

This answertype is bound to and reads the 'usstates.xml' file located in the
XmlData directory on the webserver to present a drop down list of countries on the surveyform. It is also possible to
create a new Xml bound type using the Answer Type Creator.<br />
<br />
If a default element has to be selected in the dropdown list this can be done
by setting the &quot;Default Text Value&quot; property to a value that is available
inside the bound dropdownlist.<br />
<br />
<u>XML US States Answertype Settings</u><br />
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
                <br /><br />
<hr style="color:#e2e2e2;" /><h3>More Information</h3><br />
                
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " >Answer Types Introduction </a>	<br />
                <a href="Answer Type Creator.aspx" title=" Answer Type Creator " >Answer Type Creator </a>	<br />
                <a href="XMLT_Introduction.aspx" title=" XML Bound Answer Types Introduction " > XML Bound Answer Types Introduction </a>	<br />
<a href="Xml - Country.aspx" title=" Xml - Countries " > Xml - Countries </a>	<br />
                <a href="Subscriber - Xml List.aspx" title=" Subscriber - Xml List " > Subscriber - Xml List </a>	
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

