<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Campaigns" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Survey Publishing & Campaigns</h2><hr style="color:#e2e2e2;" />
             
SP&trade; provides several ways to start a campaign and publish a survey to respondents.<br />
<br />
<i>* The Campaigns/ Weblinks menu - Deployment URL </i><br /> -   provides a HTML weblink (URL) to open and access the
  survey in a webbrowser. This link can be distibuted (copy/ paste) through all sort of media from emails, messengers or on website pages.<br />
<br />

<i>* The Campaigns/ Take Survey menu</i><br /> -  presents a special page after loging in to the SP&trade; webapplication 
  that is meant to allow a registered user to take a
  survey. This page requires the user to
  have the &quot;take survey&quot; right set for a given Role .<br />
<br />
<i>* The SurveyBox Webcontrol</i><br /> -  meant for more experienced users and developers who
  want to customize the default SurveyBox ASP.net control and integrate it into ASP.net pages of a separate website.<br />
<br />
<i>* Mailings </i><br /> - a complete set of mailing features are part of SP&trade; to email invitations including survey links and invitation codes to respondents.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
                <a href="Survey Deployement.aspx" title=" Survey Link Deployment " > Survey Link Deployment </a>	<br />
                <a href="Take Survey.aspx" title=" Take Survey " > Take Survey </a>	<br />
                <a href="Web Control Deployment.aspx" title=" Web Control Deployment " > SurveyBox Web Control Deployment </a>	<br />
                <a href="ED_Introduction.aspx" title=" Emailing Introduction " > Emailing Introduction </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

