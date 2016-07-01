<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Respondent / Voter Report</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The voter report shows the individual and detailed report for each
respondents answers.<br />
<br />
Voter reports can be accessed from following locations :<br />
<br />
* Menu Results/Report/VoterReport/List of Entries  - Voter%20Entries%20List.html<br />
<br />
* Menu Results/ Filemanager/ Posted by columnlink - File%20Manager.html<br />
<br />
* Menu Campaigns/Mailing Status - Mailing%20Status.html<br />
<br />
                Note: the resultsreport that can be shown to the respondent after submitting a survey is identical to the voterreport. See Completion settings - Redirection URL to make use of the resultsreport.
<br /><br />
<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3><br />
                <br />
Voter%20Report%20Edit.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

