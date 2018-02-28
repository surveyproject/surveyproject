<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyManagement" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Email Notification Settings</h2>
                <hr style="color:#e2e2e2;" />
             
                The email notification feature is part of the <a href="Survey General Settings.aspx">Survey Settings</a> screen. 
                It's purpose is to send an email message each
                time a survey has been answered and submitted.  <br /> <br />There are three different types of notification:<br />
                <br />
                * <span style="text-decoration: underline">Short Message</span> This will send an email
                with a brief text stating that someone has answered the survey .<br />
                <br />
                * <span style="text-decoration: underline">Full Entry</span> This will send a complete
                report where all questions and answers are shown including those that were not
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
                is setup correctly either during the installation (web.config - nSurveySettings section) or afterwards through the System Settings menu.
                <br />
                        <hr style="color:#e2e2e2;" />
        <h3>More Information</h3><br />
                <a href="InstallationSettings.aspx">System Settings</a><br />
                <a href="Survey General Settings.aspx">Survey Settings</a>
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
