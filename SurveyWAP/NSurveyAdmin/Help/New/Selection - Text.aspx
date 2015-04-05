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
                    Selection - Text</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
A selection type will base its rendering on the selection mode that was
choosen in the Question Editor -  Question%20Editor.html. Depending on the selection mode
the type will be rendered as a radio button, checkbox or grouped along
with other selection - text types inside a dropdown list.<br />
<br />
<u>Type Settings</u><br />
<br />
* Answer Text - the text that will be shown next to the selection item
  (radio, checkbox) or inside the dropdown list. <br />
<br />
* Image UR:  option to enter image URL
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show it instead of<br />
  showing the answer text. <br />
<br />
* Type -  option to change the current type of the answer we want in
  our survey. Respondent answers already collected will not be deleted if
  we change the type.<br />
<br />
* Selected by Default - we can choose to have the selection item selected /
  checked by default <br />
<br />
* Score Point - lets us specify a score value if the answer is selected.
  This score will be used to calculate the question's score and survey
  overall score of the respondent.<br />
<br />
  This feature is only available if we have turned on Survey's Score - 
  Score_Introduction.html features.<br />
<br />
* Rating Part - let us choose if we want this selection to be part of the
  rating / scale calculation in the reports. To learn more about rating
  please read the Rating_Introduction.html.<br />
<br />
  This feature is only available if we have turned on rating - 
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
Score_Introduction.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

