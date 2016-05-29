<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


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
                <h2 style="color:#5720C6;">Field - Address</h2><br />
<br />
<br />
<hr style="color:#e2e2e2;" /> <br />
This field type renders a Google Maps based entry field for addresses. On entering address information (street, streetnumber, city etc.) location information 
                will be automatically presented (through Google) to select from. The selected address information will be presented on the page and a map will show
                a marker at the selected location.

                
                 Default Type settings can be used also.
<br /><br />

<u>Installation & Activation</u><br /><br />
- to make use of the Google Address Answer Type a Google Subscription is needed in order to get a key;<br />
- the key must be copied to the Address.xml file at the XmlData\Address directory;<br />
- the key will become part of the url as provided by Google and which is programmed into SP;<br />
- the complete Google  url:<br /><br />
 https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=places&sensor=false&key=AIzaSyDwh2agwB_vfybUaiuLrl4Hopr9EDX6rUI&language=en
<br />
<br />
Google Maps Keys Information & Links:<br />
                <a href="https://developers.google.com/maps/documentation/javascript/get-api-key" target="_blank">https://developers.google.com/maps/documentation/javascript/get-api-key</a>
                <br /><br />
                Note: the working of the Google Maps features is determined by the Google Company. Limitations may apply. <br />
               <br /> More information can be found at:<br />
                <a href="https://developers.google.com/maps/documentation/javascript/usage" target="_blank">https://developers.google.com/maps/documentation/javascript/usage</a>

                <br /><br />

<u>Default Type Settings</u><br />
<br />
* <i>Answer Text</i><br />Answertext that will be shown next to the entry field.
<br />
<br />
*<i>Image URL</i><br />Used to add an image URL
  (e.g. <a href="#" target="_blank">http://www.mydomain.com/myimage.gif</a>) to show a picture alongside or instead of
   the answer text.
<br />
<br />
* <i>Type</i><br />Option to change the current field type used in the survey. Respondent answers already collected will not be deleted if
  the answertype is changed.<br />
<br />

* <i>RegEx Server Side Validation</i><br />Option to validate the selected value of the
  address field using a regular expression. Several regular expression
  validations are provided out of the box and it is very easy to create a 
  new one using the Regular Expression Editor. This validation check is
  done server-side.<br />
<br />

* <i>Mandatory</i><br />Option to be set if the address field is required to be
  filled. This check is done server-side.
<br />
<br />
* <i>Default Text Value</i><br />Option to set a default value inside the
  textbox field. When an address/location is added that is recognized by Google the location marker will show automatically 
                on opening the question on the surveyform.
                <br /><br />       
                It is also possible to fill the textbox with a default
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
Option to specify an alias that we can use in other questions to pipe the slider field value. To learn about piping and
  pipe alias we suggest reading the Piping Introduction.
<br />
<br />
* <i>Alias</i><br /> Answer Alias that can used in reports. The Alias is not shown or used in any surveys.<br />
<br />
* <i>ID</i><br /> Answer ID that can used in reports. The ID is not shown or used in any surveys.<br />
<br />
<br />
<br />
<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3><br />
  <br />
<br />            </td>
        </tr>
    </table>
</div></div></asp:Content>
