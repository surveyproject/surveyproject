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
                    Question Editor</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The question editor allows to setup the configuration and constraints of the 
question.<br />
<br />
If answers need to be added to the question the
Answers%20Editor.html is used after having created the question initially.<br />
<br />
<u>Question Options</u><br />
<br />
* Selection Mode is the mode in which the answers -SelectionT_Introduction.html- will
  be rendered by the Survey engine.<br />

<br />
  The radio button mode will show answers- SelectionT_Introduction.html-  inside the
  question as radio buttons. The respondent can select only one answer in
  this mode.<br />


<br />
  The checkbox mode will show answers - SelectionT_Introduction.html- inside the
  question as checkboxes. The respondent can select multiple answers in
  this mode.
<br />
<br />
  The dropdown list mode will group and show answers - SelectionT_Introduction.html -
  inside a dropdown list. The respondent can select only one answer in
  this mode.<br />
<br />
* Display Mode is the layout of the answer items inside the
  question. Options to choose are to order the layout in a vertical manner
  or to order the layout in an horizontal manner.<br />
<br />
* Number Of Columns is the number columns in which the question layout
  will be split into.<br />
<br />
* Randomize Answers Order is an option to show the answer's in a
  random order to each respondent to avoid the &quot;order bias effect&quot;.<br />
<br />
* Rating Part option to activate the answers rating / scaling options as shown on  
 the question's Selection Answer Types settings page. To know more about rating and
  scaling read the Rating_Introduction.html.<br />
<br />
* Min. Selection Required is the minimum number of answers that are mandatory to
  select in the question. Only "Selection" Answer Types are calculated in
  the selection number count, to make Field Answer Types
  mandatory it can be done by checking the mandatory checkbox on the -
  Field%20-%20Basic.html - answers options page.<br />
<br />
* Max. Selection Allowed is the maximum of answers that can be selected
  in the question. Only Selection Answer Types are calculated in the
  selection number count, to make Field Answer Types mandatory
  it can be done by checking the required field in the
  Field%20-%20Basic.html answers options.<br />
<br />
* Pipe Alias to specify an alias that can be used to pipe respondent
  question's answers in subsequent questions labels or answers labels. To
  learn more about piping and pipe alias read the
  Piping_Introduction.html.<br />
<br />

<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
Piping_Introduction.html<br />
Rating_Introduction.html<br />
Repeatable_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

