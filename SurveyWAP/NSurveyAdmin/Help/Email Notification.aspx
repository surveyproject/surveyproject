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
                    Email Notification Settings</h2>
                <br /><br />
                <hr style="color:#e2e2e2;" />
                <br />
                The email notification feature is part of the Settings screen. It's purpose is to send an email message each
                time a survey has been answered.  <br /> <br />There are three types of notifications:<br />
                <br />
                * <span style="text-decoration: underline">Short Message</span> This will send an email
                with a brief text stating that someone has answered the survey .<br />
                <br />
                * <span style="text-decoration: underline">Full Entry</span> This will send a complete
                report where all the questions are shown including those that were not
                answered.<br />
                <br />
                * <span style="text-decoration: underline">Answers Only</span>This will send a partial
                report where only answered questions will be shown.<br />
                <br />
                <br />
                Once the preferred notification type is chosen further (email)settings are shown:
                <br />
                <br />
                * <span style="text-decoration: underline">Email From</span>: the email address
                from which the notification will be sent.<br />
                <br />
                * <span style="text-decoration: underline">Email Notification To</span>: the email
                address that will receive the notification.<br />
                <br />
                * <span style="text-decoration: underline">Email Notification Subject</span>: the
                subject of the notification email.<br />
                <br />
                <br />
                Before using the email Notification options make sure that the (SMTP) mail server
                is setup correctly either during the installation or afterwards: check the web.config
                file's "mail server settings" in the SurveySetting section.
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
