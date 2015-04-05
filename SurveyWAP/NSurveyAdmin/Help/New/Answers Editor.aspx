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
                    Answers Editor<br />
</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The answer editor allows to add new answers to a question.  By
default Survey Project offers different types of answer items but as can be read at Form%20Architecture.html it is possible to develop and add
new answer items as well.<br />
<br />
<u>Answer Overview</u><br />
<br />
* Edition Language option to select in which language to edit the
  answers label and default texts.<br />
<br />
  This feature is only available if the multilanguage feature is turned on - Survey's
  ML_Introduction.html.<br />
<br />
* Arrow Up - will move the answer's position up.
<br />
* Arrow Down -  will move the answer's position down.
<br />
* Delete - will delete the answer and all respondent answers related to
  this answers. Its not possible to recover it once it has been deleted.
<br />
* Add New Answer option to add a new answer type to the question.<br />
<br />
<u>Edit Answer Settings</u><br />
As each answer item has its specific set of properties and behavior read the documentation of each type individually to build the survey.<br /><br />
* Selection%20-%20Text.html<br />
* Selection%20-%20Other.html<br />
* Field%20-%20Basic.html<br />
* Field%20-%20Large.html<br />
* Field%20-%20Required.html<br />
* Field%20-%20Email.html<br />
* Field%20-%20Calendar.html<br />
* Field%20-%20Rich.html<br />
* Field%20-%20Ranking.html<br />
* Field%20-%20Constant%20Sum.html<br />
* Field%20-%20Hidden.html<br />
* Field%20-%20Password.html<br />
* Extended%20-%20File%20Upload.html<br />
* Boolean.html<br />
* Image%20-%20Password.html<br />
* Xml%20-%20Country.html<br />
* Xml%20-%20US%20States.html<br />
* Subscriber%20-%20Xml%20List.html<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
Question%20Editor.html<br />
Form Architecture<br />
ML_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

