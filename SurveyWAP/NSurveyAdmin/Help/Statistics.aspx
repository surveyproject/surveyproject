<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyManagement" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Survey Statistics</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;" />
                <br />
                The statistics page provides basic metrics about a survey. It presents a general
                overview of how the survey performs and how many people have participated.<br />
                <br />
                <br />
                <b>* Creation Date</b><br />
                This is the date on which the survey was created.<br />
                <br />
                <br />
                <b>* Last Entry On</b><br />
                This is the date on which the last response to the survey was recorded.<br />
                <br />
                <br />
                <b>* Display Times</b><br />
                This is the number of times the survey web control has been shown. The number is
                not per person / session. It will increase each time the survey control has been
                rendered.<br />
                <br />
                <br />
                <b>* Number Of Voters</b><br />
                Numbers of people that participated in the survey.<br />
                <br />
                <br />
                <b>* Unvalidated Progress Entries</b><br />
                This is the number of participants who have saved their progress to resume it later
                on but have not validated their answers yet. All unvalidated entries can be deleted
                but this will also delete all the answers that were saved in between. Respondents who saved their progress
                will not be able to resume it after a delete.<br />
                <br />
                <br />
                <b>* Monthly Stats</b><br />
                This shows how many respondents per day have participated in the survey. This count
                includes only validated answers.<br />
                <br />
                <br />
                <b>* Reset Votes</b><br />
                This deletes all respondent answers that were posted for the survey (both validated and unvalidated). It is not possible
                to restore the answers once they have been deleted.<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
