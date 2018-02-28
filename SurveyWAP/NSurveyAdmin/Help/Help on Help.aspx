<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="helpPanel">

            <div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Introduction" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div>

            <div>
<h2 style="color:#5720C6;">About the Helpfiles</h2><hr style="color:#e2e2e2;" /><br />

These helpfiles are written to assist users of all levels with using, creating and maintaining SP™ forms or questionnaires. <br /><br />
From site visitors, to Survey Takers and Administrators to Technicians and Developers, this manual provides basic instructions on how to set-up, create and maintain one or more surveys using the SP™ Webapplication.
<br /><br />
Some of these helpfiles include step-by-step details on how to perform a task using clear and basic language. 
Others explain the working of the different features on a particular webpage of SP&trade; or clarify different menu options.
Some helpfiles may include one or more screen captures of the task at hand 
as well as tips for Administrators and Technicians on how to configure SP™ to enable the task. 

<br /><br />
The Helpfiles are written and updated by the Survey&trade; Project Community for the Survey&trade; Project webapplication. They are a (continuous) work in progress. 
The content is freely available as an integral part of the webapplication. <br /><br />
Additional help and information is available at: <br />
- <a href="http://www.surveyproject.org" target="_blank">Survey&trade; Project Community site</a> <br />
- <a href="https://github.com/surveyproject" target="_blank">Survey&trade; Project Github site </a><br />
- <a href="https://www.youtube.com/user/TheSurveyProject" target="_blank">Survey&trade; Project YouTube Channel</a><br />
<hr style="color:#e2e2e2;" />
                <h3>More Information</h3><br />
<a href="Survey%20Copyright%20and%20Disclaimer.aspx" title="Copyright & Disclaimer" >Copyright & Disclaimer</a><br />

            </div>

<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
