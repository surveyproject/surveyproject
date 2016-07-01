<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#FieldTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Field - Hidden</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
This type will not be shown to the respondent. The main purpose of this
type is to gather additional information without showing it on the surveyform.<br />
<br />
For example in order to capture the browser used by the respondent a Field - Hidden type could be added to the question and by setting the
default text to : %%HTTP_USER_AGENT%%  it would automatically capture the
browser of the respondent and add it to the answerresults. <br />
<br />
<u>Type Settings</u><br />
<br />
* <i>Answer Text</i><br />Answertext that will be shown next to the entry field.
<br />
<br />
*<i>Image URL</i><br />Used to add an image URL
  (e.g. <a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) to show a picture alongside or instead of
   the answer text.
<br />
<br />
* <i>Type</i><br />Option to change the current field type used in the survey. Respondent answers already collected will not be deleted if
  the answertype is changed.<br />
<br />

* <i>RegEx Server Side Validation</i><br />Option to validate the content of the
  textbox field using a regular expression. Several regular expression
  validations are provided out of the box and it is very easy to create a 
  new one using the Regular Expression Editor. This validation check is
  done server-side.<br />
<br />

* <i>Mandatory</i><br />Option to be set if the textbox field is required to be
  filled. This check is done server-side.
<br />
<br />
* <i>Default Text Value</i><br />Option to set a default value inside the
  textbox field. It is also possible to fill the textbox with a default
  value coming from an external source using on of the following tags :
<br /><br />
  ##yourquerystringvariablename## 
  <br />this will set the default text with a query string variable's value.<br /><br />
  @@yoursessionvariablename@@ 
  <br />this will set the default text with a session variable's value.<br /><br />
  &amp;&amp;yourcookievariablename&amp;&amp;<br />
  This will set the default text with a cookie variable's value.<br /><br />
  %%servervariablename%% 
  <br />This will set the default text with a server side variable's value.
<br />
<br />
* <i>Pipe Alias</i><br />
Option to specify an alias that can be used in other questions to pipe the textbox field value. To learn about piping and
  pipe alias read the Piping Introduction.
<br />
<br />
* <i>Alias</i><br /> Answer Alias that can used in reports. The Alias is not shown or used in any surveys.<br />
<br />
* <i>ID</i><br /> Answer ID that can used in reports. The ID is not shown or used in any surveys.<br />
<br />
<br />

<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
Answer%20Type%20Creator.html<br />
Answers%20Editor.html<br />
EF_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

