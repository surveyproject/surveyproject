<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#ConditionalLogic" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Branching Conditions</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
Branching conditions allow to setup logical rules based on
respondent's answers to branch the respondent to a specific page in a
survey or even terminate the survey.<br />
<br />
As branching conditions is a feature related to pages at
least 2 pages have to be created in a survey to be able to use the branching features. Its
not possible to use branching on the last page of the survey.<br />
<br />
Each condition is based on a set of rules that can be defined. The first
condition that will be met will branch to the page that is defined in the
branching editor. Unlimited conditions are available which can be ordered
or re-ordered at any time.<br />
<br />
To learn more about conditions and how they work and how to use them read - Condition_Introduction.html.
<br /><br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Skip%20Logic%20Conditions.html<br />
Thanks%20Message%20Conditions.html<br />
HowToBranch_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

