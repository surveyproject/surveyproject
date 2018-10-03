<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Campaigns" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Survey Link Deployment</h2><hr style="color:#e2e2e2;" />
                <br />

The Campaigns/Web Links menu option provides a (direct) (HTML) link (Url) to the survey. This link can be distributed through any media from emails, 
                messenger or on website pages.<br />
<br />
You can copy &amp; paste the link provided and use it in almost any application that supports HTML links.<br />
<br />
The survey ID  that is part of the URL is unique and it is not possible for the respondent to alter the link in order to find out what other surveys were 
                created unless they know the exact ID of other surveys as well.<br />
<br />Instead of making use of the unique ID as part of the URL it's possible to create a "Friendly Name" URL. 
                If a friendly name is entered and saved the new URL will be shown on the page. This URL can also be used to publish the survey.
                
                <br /><br />

                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
                <br />
                <a href="SD_Introduction.aspx" title=" Survey Deployment Introduction " > Survey Deployment Introduction </a>	<br />
                <a href="Take Survey.aspx" title=" Take Survey " > Take Survey </a>	<br />
                <a href="Web Control Deployment.aspx" title=" Web Control Deployment " > SurveyBox Web Control Deployment </a>	<br />
                <a href="ED_Introduction.aspx" title=" Emailing Introduction " > Emailing Introduction </a>	<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

