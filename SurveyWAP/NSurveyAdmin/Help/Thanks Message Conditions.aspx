<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Completion" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Thanks Message Conditions</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;" />
                <br />
                With the "Thanks Message Conditions" logical rules can be created based
                on the respondent's answers to show a specific message at the end of a survey. For
                example a specific message could be shown to voters who answered choice X at
                question Y.<br />
                <br />
                Each condition is based on a set of predefined rules. The first condition that is
                met will show its &quot;Thank You&quot; message to the user. An unlimited number
                of conditions is possible that can be ordered or re-ordered at any time.<br />
                <br />
                <br />
                <hr style="color:#e2e2e2;" />
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <a href="Condition_Introduction.aspx" target="_self">Condition Introduction</a><br />
                <a href="Skip%20Logic%20Conditions.aspx" target="_self">Skip Logic Conditions</a><br />
                <a href="Branching%20conditions.aspx" target="_self">Branching conditions</a><br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
