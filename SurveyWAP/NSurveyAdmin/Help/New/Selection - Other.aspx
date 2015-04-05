<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SelectionTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Selection - Other</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
                
A selection type will base its rendering on the selection mode that was
choosen in the question editor -  Question%20Editor.html. Depending on the selection mode
the type will be rendered as a radio button, checkbox or grouped along
with other selection - text types inside a dropdown list.<br />
<br />
<u>Type Settings</u><br />
<br />
* Answer Text-  the text that will be shown next to the selection item
  (radio, checkbox) or inside the dropdown list. <br />
<br />
* Image URL we can give an image URL
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show it instead of<br />
  showing the answer text. <br />
<br />
* Type - option to change the current type of the answer we want in
  our survey. Respondent answers already collected will not be deleted if
  we change the type.<br />
<br />
* RegEx Server Side Validation - option to validate the content of the
  textbox field using a regular expression. Several regular expression
  validation are provided out of the box and it is very easy to create
  new one using the regular expression editor -  Regular%20Expression%20Editor.html. This check is
  done server-side.<br />
<br />
* Mandatory - option to set if the textbox field requires to be
  filled. This check is done server-side.<br />
<br />
* Default Text Value - option to set a default value inside the
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
* Pipe Alias - option to specify an alias that we can use in other
  questions to pipe the textbox field value. To learn about piping and
  pipe alias we suggest reading the Piping_Introduction.html.<br />
<br />

* Selected Answers we can choose to have the selection item selected /
  checked by default.<br />
<br />
* Score Point 0  option to specify a score value if the answer is selected.
  This score will be used to calculate the question's score and survey
  overall score of the respondent.<br />
<br />
  This feature is only available if we have turned on Survey's Score feature - 
  Score_Introduction.html features.<br />
    <br />
<br />
* Rating Part - option to choose if we want this selection to be part of the
  rating / scale calculation in the reports. To learn more about rating
  please read the Rating_Introduction.html.<br />
<br />
  This feature is only available if we have turned on the rating feature - 
  Rating_Introduction.html on the question of the answer type.<br />
<br />

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

