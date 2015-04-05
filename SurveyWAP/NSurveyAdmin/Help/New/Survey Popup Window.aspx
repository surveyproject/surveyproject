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
                    Survey Popup Window</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Survey allows us to deploy our survey through invitation popup windows.
We can set a specifc frequency to the window and based on it show the
invitation only after a given number of people have visited the page on
which the popup code is hosted.<br />
<br />
The popup window is simulated using a DHTML generated window and is fully
compatible with browsers having popup blocker activated.<br />
<br />
Popup Settings<br />
<br />
* Show Popup Every is the frequency of visitors the popup should be
  shown to. A cookie is set on each visitors machine to prevent having a
  visitor to be counted twice in the frequency count.<br />
   <br />
  It is recommended to put a 0 value during testing as this will always
  show the window .<br />
<br />
* Open In New Window alllows us to open the survey inside a new window. <br />
<br />
* Edition Language  is the language in which we are currently editing
  the title, link text and popup message.<br />
<br />
  This feature is only available if we have turned on Survey's
  ML_Introduction.html features.<br />
<br />
* Get Language From allows us to choose from which source Survey will
  try to get the language code in order to get the correct translation
  for the popup window.<br />
<br />
  This feature is only available if we have turned on Survey's
  ML_Introduction.html features.<br />
<br />
* Popup Title is the title shown at the top of the popup window.<br />
<br />
* Popup Survey Link Text is the text of the link that will take the
  visitor to our survey.<br />
<br />
* Popup message  is the message that will be shown inside the popup
  window.<br />

<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Deployement.html<br />
Take%20Survey.html<br />
Web%20Control%20Deployment.html<br />
ED_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

