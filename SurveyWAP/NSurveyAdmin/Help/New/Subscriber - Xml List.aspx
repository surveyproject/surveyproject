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
                    Subscriber - Xml List</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
This type will load and bind dynamically an Xml file to a dropdownlist
based on the value posted by the answer publisher its subscribing to.<br />
<br />
Out of the box a subscriber type can be used to be be connected to an
Xml%20-%20Country.html type publisher to get a list of regions based on
the country that has been selected. eg: if United States are selected in
the Xml - Country List, the Subscriber - Xml List type will receive US
and try to load the us.xml file.<br />
<br />
This type can subscribe to any publisher but its better to subscribe to
XMLT_Introduction.html. To learn more about the publisher / subscriber
mode we suggest watching the Publisher%20_%20Subscriber%20Tutorial.html.<br />
<br />
Type Settings<br />
<br />
* Answer Text is the text that will be shown next to the dropdown list. <br />
<br />
* Image URL we can give an image URL<br />
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show it instead of<br />
  showing the answer text. <br />
<br />
* Type  allows us to change the current type of the answer we want in
  our survey. Respondent answers already collected will not be deleted if
  we change the type.<br />
<br />
* Mandatory  allows us to set if at least one selection is required.
  This check is done server-side.<br />
<br />
* Default Text Value allows us to set a default item in the dropdown
  list. It is also possible to set a default list item with a default
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
* Pipe Alias allows us to specify an alias that we can use in other
  questions to pipe the textbox field value. To learn about piping and
  pipe alias we suggest reading the Piping_Introduction.html.<br />
<br />
* Reporting Alias is the text that can be shown instead of the answer
  text inside our reports.<br />
<br />
* Extended Report Filter  we will be able to use directly the text
  answers of the respondent as a filter in the
  Report%20General%20Settings.html. To learn more about piping and
  extended report filters we suggest reading the EF_Introduction.html.<br />
<br />
* Connections which publisher will be the one that sends it selected
  value to this type. To learn more about the publisher / subscriber mode
  we suggest watching the Publisher%20_%20Subscriber%20Tutorial.html.<br />
<br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Publisher%20_%20Subscriber%20Tutorial.html<br />
AT_Introduction.html<br />
Answer%20Type%20Creator.html<br />
EF_Introduction.html<br />
XMLT_Introduction.html<br />
Xml - Country List<br />
Answers%20Editor.htmlQuestion%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

