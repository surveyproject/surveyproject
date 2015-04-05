<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#AnswerTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
Answer Types Introduction</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Answer types alows creating questions as needed in order to get
the required feedback. Survey Project provides out of the box a number of
answer types that can be used to compose a question. Each item has its
specific set of properties and all items can be used together in the
same question.<br />
<br />
Here are the items provided out of the box :<br />
<br />
Selection%20-%20Text.html <br />
Selection%20-%20Other.html<br />
Field%20-%20Basic.html <br />
Field%20-%20Large.html  <br />               
Field%20-%20Required.html<br />
Field%20-%20Email.html       <br />          
Field%20-%20Calendar.html <br />
Field%20-%20Rich.html<br />
Field%20-%20Ranking.html <br />
Field%20-%20Constant%20Sum.html<br />
Field%20-%20Hidden.html <br />
Field%20-%20Password.html<br />
Extended%20-%20File%20Upload.html <br />
Boolean.html <br />
Xml%20-%20Country.html <br />
Xml%20-%20US%20States.html<br />
Subscriber%20-%20Xml%20List.html<br />
<br />
It is also possible to create new items either using the
Answer%20Type%20Creator.html or through the Survey Project source code.<br />
<br />

<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Answer%20Type%20Creator.html<br />
Answers%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

