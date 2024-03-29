﻿<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Campaigns" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Take Survey</h2><hr style="color:#e2e2e2;" />
                <br />
The Campaigns/ Take Survey webpage is a special page that is used to allow a registered SP&trade;
User to take a survey once logged into the SP tool. This
page requires the user to have the &quot;Take survey&quot; roleright set.<br />
<br />
Once the user has logged in to SurveyProject and reaches this page it's possible to select the survey that has been assigned through the user manager. 
Respondents can be restricted to take a survey only one time by activating the proper security options on the survey.<br />
                
                <hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <a href="SD_Introduction.aspx" title=" Survey Deployment Introduction " > Survey Deployment Introduction </a>	<br />
                                <a href="Survey Deployement.aspx" title=" Survey Link Deployment " > Survey Link Deployment </a>	<br />
                <a href="Web Control Deployment.aspx" title=" Web Control Deployment " > SurveyBox Web Control Deployment </a>	<br />
                <a href="ED_Introduction.aspx" title=" Emailing Introduction " > Emailing Introduction </a>	<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

