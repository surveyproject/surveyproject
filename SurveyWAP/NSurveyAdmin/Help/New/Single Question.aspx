<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Single Question</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
A single question can be composed of any number of answer types.<br />
<br />
Survey Project does not have the concept of radio, checkbox or field questions.
Survey Project is much more modular and allows to compose questions
based on the Answer Type - AT_Introduction.html - that is need for a particular question.<br />
<br />
<u>Single Question Examples</u><br />
<br />
A single question composed of Field text answers - FieldT_Introduction.html  and - 
XMLT_Introduction.html - an XML (Country) list<br />
<br />
A single question composed of a Selection answertype - SelectionT_Introduction.html  with a radio
button selection.<br />
<br />
A single question composed of Selection answertype - SelectionT_Introduction.html -  with a drop
down selection.<br />
<br />
By combining the Answertypes -  AT_Introduction.html -  one can
almost create any kind of questions needed.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
SelectionT_Introduction.html<br />
AT_Introduction.html<br />
FieldT_Introduction.html<br />
XMLT_Introduction.html<br />
AT_Introduction.html<br />
Insert%20Question.html<br />
Question%20Editor.html<br />
Matrix%20Question%20Editor.html<br />
Static%20Question.html<br />
Matrix%20Question.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

