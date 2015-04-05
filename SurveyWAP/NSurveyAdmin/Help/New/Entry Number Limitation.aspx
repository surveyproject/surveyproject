<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Entry Number Limitation</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This security addin makes it possible to set a maximum of respondent who can
take a survey .<br /><br />
* Entry Count: the current number of entries counted in the survey.<br />
<br />
* Max. Entries Allowed: the maximum respondent entries allowed for
  the survey.<br />
<br /><br />
* Quota Reached Message: the text that will be shown instead of the
  survey once the maximum entries have been reached. <br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Security.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

