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
                    Boolean</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />            
This Answer Type renders a checkbox. If the checkbox is checked the value
&quot;true&quot; is saved as an answer, if its not check the value &quot;false&quot;
is saved as an answer.<br />
<br />
Answer Type Settings<br />
<br />
* Answer Text is the text that will be shown next to the selection item
  (radio, checkbox) or inside the dropdown list. <br />
<br />
* Image URL an image URL like <a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a> can be added to show it instead of
  showing the answer text. <br />
<br />
* Type allows us to change the current type of the answer we want in
  our survey. Respondent answers already collected will not be deleted if
  we change the type.<br />
<br />
* Default Text Value  allows us to set &quot;true&quot; if the checkbox must be
  checked by default. It is also possible to use a default value coming
  from an external source using following tags :<br />
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
* Pipe Alias allows us to specify an alias that we can use in other<br />
  questions to pipe the textbox field value. To learn about piping and
  pipe alias we suggest reading the Piping_Introduction.html.
<br />
* Reporting Alias is the text that can be shown instead of the answer
  text inside our reports.<br />
<br />
* Extended Report Filter  we will be able to use directly the text
  answers of the respondent as a filter in the
  Report%20General%20Settings.html.To learn more about piping and
  extended report filters we suggest reading the EF_Introduction.html.<br />
<br /><br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
EF_Introduction.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

