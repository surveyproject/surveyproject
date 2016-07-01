<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Misc" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Extended - File Upload</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This type will allow the respondent to upload any number of files. There
are no limitation on the number of file upload types per question or per
page in a survey. Uploaded files can be managed using the
File%20Manager.html .<br />
<br />
This field has no extra client side validation check but if it is required for
the respondent to upload at least one file the mandatory
checkbox can be selected inside the type settings.<br />
<br />

<br />
<u>Extended Type Settings</u><br /><br />
Extended type settings are additional properties of certain answer types that are
only available once a type has been created and added to the question.<br />
<br />
To edit the extended settings of a type click the edit
button of the answer overview once it's added to the list of answers added to the question
or after changing an existing type to a new one by clicking the update button.
<br /><br />
* <i>Max. File Upload Number</i> is the maximum number of files a respondent can
  upload for this type instance.
<br /><br />
* <i>Max. File Size</i> is the maximum size in bytes allowed for upload. 
Note: there may also be a maximum set on file sizes set by IIS (webserver) or .NET (the operating system). 
<br />
<br />
* <i>Accepted Content Type</i> option to specify the files types that will be accepted for upload eg: pdf, gif etc ..<br />
<br />
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

<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3><br />
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
EF_Introduction.html<br />
Question%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

