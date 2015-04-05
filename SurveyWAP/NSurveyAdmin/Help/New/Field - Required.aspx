<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Field - Required</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This type will render a standard textbox. There is no maximum length
limit by default. If we need a to change the size of the field or its
properties it is possible to create our own field type using the
Answer%20Type%20Creator.html.<br />
<br />
This field has a client side validation script to make sure it has been
filled. It is recommended to also activate the server-side checking using
the Mandatory option of the type settings.<br />
<br />
Type Settings<br />
<br />
* Answer Text is the text that will be shown next to the field. <br />
<br />
* Image URL we can give an image URL
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show it instead of
  showing the answer text. <br />
<br />
* Type allows us to change the current type of the answer we want in
  our survey. Respondent answers already collected will not be deleted if
  we change the type.<br />
<br />
* RegEx Server Side Validation allows us to validate the content of the
  textbox field using a regular expression. Several regular expression
  validation are provided out of the box and it is very easy to create
  new one using the Regular%20Expression%20Editor.html. This check is
  done server-side.<br />
<br />
* Mandatory allows us to set if the textbox field requires to be
  filled. This check is done server-side.<br />
<br />
* Default Text Value allows us to set a default value inside the
  textbox field. It is also possible to fill the textbox with a default
  value coming from an external source using following tags :<br />
<br />
  ##yourquerystringvariablename## will set the default text with a query
  string variable's value.<br />
  @@yoursessionvariablename@@ will set the default text with a session
  variable's value.<br />
  &amp;&amp;yourcookievariablename&amp;&amp; will set the default text with a cookie
  variable's value.<br />
  %%servervariablename%% will set the default text with a server side
  variable's value.<br />
<br />
* Pipe Alias  allows us to specify an alias that we can use in other
  questions to pipe the textbox field value. To learn about piping and
  pipe alias we suggest reading the Piping_Introduction.html.<br />
<br />
* Reporting Alias is the text that can be shown instead of the answer
  text inside our reports.<br />
<br />
* Extended Report Filter  we will be able to use directly the text
  answers of the respondent as a filter in the
  Report%20General%20Settings.html.To learn more about piping and
  extended report filters we suggest reading the EF_Introduction.html.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
Answer%20Type%20Creator.html<br />
EF_Introduction.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

