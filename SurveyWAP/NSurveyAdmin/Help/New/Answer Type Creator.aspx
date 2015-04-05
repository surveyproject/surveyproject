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
                    Answer Type Creator</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/><br />

Survey Project provides a way to create customised Answer Types without the need to have 
any programming knowledge. Using the type creator four answer types can be created and that can be re-used inside the questions :<br />
<br />
* Selection - SelectionT_Introduction.html<br />
* Field - FieldT_Introduction.html<br />
* XML - XMLT_Introduction.html<br />
* SQL -  SQLType_Introduction.html<br />
<br />
<u>Selection Types</u><br />
<br />
* Field Name - is the name of the type that will be shown in the type
  selection inside the Answer Editor.<br />
<br />
* Allow Selection - if we are creating a selection type we need to check
  this option.<br />
<br />
* Field Entry - is our selection type offering an alternate entry like
  the Selection%20-%20Other.html type.<br />
<br />
<u>Field Types</u><br />
<br />
* Field Name - is the name of the type that will be shown in the type
  selection inside the Answer Editor.<br />
<br />
* Allow Selection - if we are creating a field type we don't need to
  check this option.<br />
<br />
* Field Entry - we need to check this option to display the extra
  settings related to field types.<br />
<br />
* Rich Field - does our field allow extended HTML edition features (through the CK Editor).
<br /><br />
* Field Width of our field.
<br /><br />
* Field Height of the field. If the height is more than one the field
  will be automatically rendered as a multi-line textbox.
<br /><br />
* Field Max. Length the maximum text length a respondent can enter.
  Note that fields with a height of more than one do not validate the
  max. length.<br />
<br />
* Field Shown In Results ..................
<br /><br />
* Javascript Function Name - is the name of the javascript function that
  will be called to validate the field content. The javascript function
  should be defined inside the Javascript code option.
<br /><br />
* Error Message -  the error message that will show up if the
  javascript function returns false.
<br /><br />
* Javascript Code - the javascript code that will validate the field
  content. The function must return true if the condition is matched or
  false if the method could not validate the content of the field.<br />
<br />
  <u>Code Example to validate a mandatory field :</u><br /><br />
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
<br />
<u>Xml Bound Types</u><br />
<br />
* Field Name - the name of the type that will be shown in the type
  selection inside the Answer Editor.
<br /><br />
* Xml File Name - name of the Xml file that the type will be bound to.
<br /><br />
  Note that the Xml file has to be in the directory specified by the 
  NsurveyXmlDataPath ellement of the web.config. It is not
  possible to edit or create Xml files through the administration
  interface.<br />
<br />
  The format of the Xml file must be as following :<br />
<br />
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
<br />
<u>Sql Bound Types</u><br />
<br />
* Field Name - name of the type that will be shown in the type
  selection inside the Answer Editor.
<br /><br />
* Sql Query - the Sql query that will populate the dropdown list. You
  can learn more about Sql bound types by reading the
  SQLType_Introduction.html
<br /><br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
Score_Introduction.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

