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
                    Selection Types Introduction</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
           
Selection types will base their rendering on the selection mode that was
choosen in the question editor - Question%20Editor.html. Depending on the selection mode
the type will be rendered as a radio button, checkbox or grouped along
with other selection - text types inside a dropdown list.<br />
<br />
The following selection types are available :<br />
<br />
* Selection - Text - Selection%20-%20Text.html<br />
* Selection - Other - Selection%20-%20Other.html<br />
<br />

<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Question%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

