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
                    Image - Password</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />




This type will render an image that will show a random generated code<br />
that the user will have to type in to confirm that its a humain users who<br />
takes the form and not a bot.<br />
<br />
Type Settings<br />
<br />
* Answer Text is the text that will be shown next to the selection item<br />
  (radio, checkbox) or inside the dropdown list. <br />
<br />
* Image URL we can give an image URL<br />
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show it instead of<br />
  showing the answer text. <br />
<br />
* Type  allows us to change the current type of the answer we want in<br />
  our survey. Respondent answers already collected will not be deleted if<br />
  we change the type.<br />
<br />
* Default Text Value  allows us to set a default validation value.<br />
<br />
* Pipe Alias  allows us to specify an alias that we can use in other<br />
  questions to pipe the textbox field value. To learn about piping and<br />
  pipe alias we suggest reading the Piping_Introduction.html.<br />
<br />
* Reporting Alias is the text that can be shown instead of the answer<br />
  text inside our reports.<br />
<br />
* Extended Report Filter  we will be able to use directly the text<br />
  answers of the respondent as a filter in the<br />
  Report%20General%20Settings.html. To learn more about piping and<br />
  extended report filters we suggest reading the EF_Introduction.html.<br />
<br />
<br />
<br />
<hr /><br />
<br />
Related Topics<br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
EF_Introduction.html<br />
Answers%20Editor.html<br />
  <br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

