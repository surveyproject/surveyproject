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
                    Xml - US States</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This type is bound to and will read the United States xml file located in
the XmlData directory to show a list of the United States. It is also
possible to create new Xml bound types using the
Answer%20Type%20Creator.html.<br />
<br />
If a default element needs to be selected in the dropdown list it can be done
by setting the &quot;Default Text Value&quot; property to a value that is available
inside the bound dropdownlist.<br />
<br />
Type Settings<br />
<br />
* Answer Text - the text that will be shown next to the the dropdown
  list. <br />
<br />
* Image URL - used to add an image URL<br />
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show an image instead of
  showing the answer text. <br />
<br />
* Type - option to change the current type of the answer used in
  the survey. Respondent answers already collected will not be deleted if
  the type is changed.<br />
<br />
* Mandatory - option to set if the dropdown list requires at least
  one selection. This check is done server-side.<br />
<br />
* Default Text Value - option to set a default list item inside the
  dropdown list. It is also possible to set a default list item with a
  default value coming from an external source using following tags :<br />
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
* Pipe Alias - option to specify an alias that can be used in other
  questions to pipe the textbox field value. To learn about piping and
  pipe alias read the Piping_Introduction.html.<br />
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
Answers%20Editor.htmlQuestion%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

