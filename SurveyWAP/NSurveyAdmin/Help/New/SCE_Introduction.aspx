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
                    Survey Design & Form Builder Introduction</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The Form Builder or Survey designer is used to create and edit surveys and webforms easily.<br />
<br />
Each survey/ form can make use of several different question types. Each of which can make use of different answertypes. 
There are no limitations to the use of combinations of different answer types inside a question.<br />
<br />
E.g. field answer types, selection answer types or SQL bound types can all be combined. All as part of the same question. 
This mix of question and answertypes offers maximum flexibility to create webforms and surveys.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Survey%20Form%20Builder.html<br />
Form Architecture<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

