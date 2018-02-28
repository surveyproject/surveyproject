<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Misc" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Subscriber - Xml List</h2><hr style="color:#e2e2e2;" />
                
This answertype will load and bind an XML file from the XmlData directory to a dropdownlist
based on the value posted by the 'answer publisher' it is subscribing to.<br />
<br />
By default the subscriber answertype can be used to connect to the Countries.xml answertype 'publisher' to get a list of 'regions' (states, provinces etc) based on
the country that has been selected. 
              <br /><br />  
                Example: if 'United States' (answervalue: US) is selected in
the Xml - Country List (countries.xml file), the 'Subscriber - Xml List' answertype will fetch a list of US States
and load the 'us.xml' file. If 'The Netherlands' is selected (NL) the subscriber will look for an nl.xml file of 'provinces' to load and present in a dropdownlist.<br />
<br />
This answertype can subscribe to any 'publisher' but its best to subscribe to 'XML Bound' Answer Types. 
                <br />
<br />

<u>Subscriber Connection Settings</u><br />
<br />
<i>* Answer Publishers </i><br /> - List of answer(types) to select from to 'publish' (send) answers to the 'subscriber'.


                <br /><br />
<i>* Subscribed to </i><br /> - Selected Publisher(s) the Subscriber answertype is linked to.

<br /><br />

                <u>Default Subscriber Answertype Settings</u><br />
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

<i>* Mandatory </i><br /> - option to determine if writing an answer in the textbox field is required. When answering the surey this check is done server-side.<br />
<br />


<i>* Pipe Alias </i><br /> - option to specify an alias that we can use in other
  questions by 'piping' the textbox field value. To learn about piping and pipe alias read hte <a href="AP_Introduction.aspx" title=" Piping Introduction " > Answer Piping Introduction </a>.
               <br />
<hr style="color:#e2e2e2;" /><h3>More Information</h3><br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " >Answer Types Introduction </a>	<br />
                <a href="Answer Type Creator.aspx" title=" Answer Type Creator " >Answer Type Creator </a>	<br />
                <a href="XMLT_Introduction.aspx" title=" XML Bound Answer Types Introduction " > XML Bound Answer Types Introduction </a>	<br />
<a href="Xml - US States.aspx" title=" Xml - US States " > Xml - US States </a>	<br />
<a href="Xml - Country.aspx" title=" Xml - Countries " > Xml - Countries </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

