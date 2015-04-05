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
                    Static Question</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
The static question is the most basic "question type" available in Survey.<br />
<br />
It allows to add any free text / pictures to the survey/ form. This question
type cannot handle answers, it can only render static text or HTML to the
respondent.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Insert%20Question.html<br />
Question%20Editor.html<br />
Matrix%20Question%20Editor.html<br />
Single%20Question.html<br />
Matrix%20Question.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

