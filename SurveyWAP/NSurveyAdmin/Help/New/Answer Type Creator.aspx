<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Answer Type Creator</h2><hr style="color:#e2e2e2;" />

SP&trade; provides a way to create custom Answer Types without the need to have programming knowledge.
                By using the Answertype creator four different answer types can be created and that can be added to questions :<br />
<br />
<a href="SelectionT_Introduction.aspx" title=" Selection AT Introduction " > Selection Type </a>	<br />
<a href="../FieldT_Introduction.aspx" title=" Field AT Introduction " > Field Type </a>	<br />
<a href="XMLT_Introduction.aspx" title=" XML AT Introduction " > XML Type </a>	<br />
<a href="SQLType_Introduction.aspx" title=" SQL AT Introduction " > SQL Type </a>	<br />
<br />
<u>Create New Selection Answer Type</u><br />
<br />
<i>* Field Name </i><br /> -  is the name of the new Answertype that will be shown drop down list of answer types to select in the Answer Editor.<br />
<br />
<i>* Use a Datasource</i><br /> - switch to XML or SQL to show relevant options on the form to create an XML or SQL answertype; not needed on Selection Type. 

                <br /> <br />
<i>* Allow Selection </i><br /> -  in case of creating a selection answertype this options needs to be checked.<br />
<br />
<i>* Field Entry </i><br /> -  check if the selection type offers an alternate entry option like the 'Selection - Other' answertype (or in case of creating a new Field Answer Type)<br />
<br />
<u>Create New Field Answer Types</u><br />
<br />
<i>* Field Name </i><br /> -   is the name of the new Answertype that will be shown drop down list of answer types to select in the Answer Editor.<br /><br />
<i>* Allow Selection </i><br /> -  if creating a field type there is no need to
  check this option.<br />
<br />
<i>* Field Entry </i><br /> -  check this option to display the extra
  settings related to field types.<br />
<br />
<i>* Rich Field </i><br /> -  check if the new field allows extended HTML edition features (through the CK Editor).
<br /><br />
<i>* Field Width </i><br /> - Visible width of the field textbox
<br /><br />
<i>* Field Height </i><br /> - Visible height of the field textbox. If the height is more than [1] the field
  will be automatically rendered as a multi-line textbox.
<br /><br />
<i>* Field Max. Length </i><br /> - the maximum text length a respondent can enter.
  Note that fields with a height of more than [1] do not validate the
  max. length.<br />
<br />
<!--
<i>* Field Shown In Results</i><br /> - [OBSOLETE]
<br /><br /> -->
<i>* Javascript Function Name </i><br /> -  the name of the javascript function that
  will be called to validate the field content. The javascript function
  should be defined inside the Javascript code option.
<br /><br />
<i>* Error Message </i><br /> -   the error message that will show up if the
  javascript function returns false.
<br /><br />
<i>* Javascript Code </i><br /> -  the javascript code that will validate the field
  content. The function must return true if the condition is matched or
  false if the method could not validate the content of the field.<br />
<br />
  <u>Code Example to validate a mandatory field :</u><br /><br />
                <code>
  function isFilled(sender)<br />
  {<br />
  if (sender.value.length == 0)<br />
  {<br />
  sender.focus();<br />
  return false;<br />
  }<br />
  else<br />
  {<br />
  return true;<br />
  }<br />
  }<br />
<br /></code>
<u>Create new Xml Bound Answer Types</u><br />
<br />
<i>* Field Name </i><br /> - the name of the type that will be shown in the type
  selection inside the Answer Editor.
<br /><br />
<i>* Xml File Name</i><br /> - name of the Xml file that the type will be bound to.
<br /><br />
  Note that the Xml file has to be in the directory specified by the 
  NsurveyXmlDataPath element of the web.config. It is not
  possible to edit or create Xml files through the administration
  interface.<br />
<br />
  The format of the Xml file must be as following :<br />
<br /><code>
  &lt;?xml version=&quot;1.0&quot; standalone=&quot;yes&quot;?&gt;<br />
  &lt;SurveyDataSource<br />
  xmlns=&quot;<a href="http://survey.codeplex.com/SurveyDataSource.xsd" target="_blank">http://survey.codeplex.com/SurveyDataSource.xsd</a>&quot;&gt;<br />
  &lt;XmlDataSource&gt;<br />
  &lt;RunTimeAnswerLabel&gt;Label To Show : &lt;/RunTimeAnswerLabel&gt;<br />
  &lt;XmlAnswers&gt;<br />
  &lt;XmlAnswer&gt;<br />
  &lt;AnswerValue&gt;&lt;/AnswerValue&gt;&lt;AnswerDescription&gt;[Select an<br />
  answer]&lt;/AnswerDescription&gt;<br />
  &lt;/XmlAnswer&gt;<br />
  &lt;XmlAnswer&gt;<br />
  &lt;AnswerValue&gt;yourvalue1&lt;/AnswerValue&gt;&lt;AnswerDescription&gt;your<br />
  answer&lt;/AnswerDescription&gt;&lt;/XmlAnswer&gt;<br />
  &lt;/XmlAnswers&gt;<br />
  &lt;XmlAnswer&gt;<br />
  &lt;AnswerValue&gt;yourvalue2&lt;/AnswerValue&gt;&lt;AnswerDescription&gt;your second<br />
  answer&lt;/AnswerDescription&gt;&lt;/XmlAnswer&gt;<br />
  &lt;/XmlAnswers&gt;<br />
  &lt;/XmlDataSource&gt;<br />
  &lt;/SurveyDataSource&gt;<br />
<br /></code>
<u>Create New Sql Bound AnswerTypes</u><br />
<br />
<i>* Field Name </i><br /> - name of the type that will be shown in the type
  selection inside the Answer Editor.
<br /><br />
<i>* Sql Query</i><br /> - the Sql query that will populate the dropdown list. You
  can learn more about Sql bound types by reading the
  SQLType_Introduction.html
<br /><br />

                <hr style="color:#e2e2e2;"/>
      
                <h3>
                    More Information</h3>
                <br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " >Answer Types Introduction </a>	<br />
<a href="Answer Type Editor.aspx" title=" Answer Type Editor " >Answer Type Editor </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

