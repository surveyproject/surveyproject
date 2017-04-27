<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Completion" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Completion Actions</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;" />
                <br />
                Once the respondents have finished answering the survey and submitted their answers
                the survey can be set to trigger a "completion action" to run at the end of the survey. 
                Available completion actions are:<br />
                <br />
                <b>&quot;Thank you&quot; Message Action</b><br />
                <br />
                This option will show a text/html message to the respondent once the survey
                is finished. It could be used to thank the user or possibly provide some information
                about the survey taken. <br />
                <br />
                * Edition Language<br />
                This is the language in which the &quot;Thank You&quot; message text is edited and
                in which language it will be shown. A language can be selected, the text changed
                and when the update button is clicked the text will be assigned to the selected
                language.<br />
                <br />
                This feature is only available if the survey's <a href="Multi-Languages Settings.aspx" title="Multi Languages">Multi Languages Settings</a>
                are activated.<br />
                <br />
                * Thanks Message<br />
                This is the message that will be shown to the end user once the survey is completed.
                <br />
                <br /> <br />
                <b>URL Redirection Action</b><br />
                <br />
                This action will redirect the user to a specific page or URL once the survey is completed.<br />
                <br />
                * Edition Language<br />
                The language for which the redirection will occur. This is also the language in
                which the &quot;Thank You&quot; message text is edited and in which language it
                will be shown.<br />
                <br />
                This feature is only available if survey's <a href="Multi-Languages Settings.aspx" title="Multi Languages">Multi Languages Settings</a>
                are activated.<br />
                <br />
                * Thanks Message<br />
                This is the message that will be shown to the end user once the survey is completed.
                This message will only show up if no redirection URL is specified. It is mostly
                used in case of multiple languages and a redirection is used for one language but
                not for the others.<br />
                <br />
                * Redirection URL<br />
                This is the URL to which the user will be redirected. It must be in following format: http://www.yourdomain.com or 
                http://www.youdomain.com/yourpage.aspx<br />
                                <br />
                <br />
                <b>Results Report Redirection URL</b><br />
                <br />
                This action will redirect the user to the Resultsreport page once the survey is completed and submitted. Through the resultsreport the respondent
                will get an overview of all survey questions, the answers submitted by the respondent and scores (if set). An option to print the report is shown on the page.
                                <br />
                <br />
                * To set the correct Resultsreport redirection URL go to menuoption: Campaigns/ Web/ .. Deployment URL <br />
                * copy the surveyid part of the url (e.g. surveyid=bb058c21-861d-450e-a2a5-4601467a59ca )<br />
                * go back to the Completion menu<br />
                * type the following url including questionmark: resultsreport.aspx?<br />
                * paste the surveyid at the end of the typed part<br /><br />
                URL Endresult: resultsreport.aspx?surveyid=bb058c21-861d-450e-a2a5-4601467a59ca 
                <br />
                <br />Note: the report option does not work with the Friendly URL
                <br />
                <hr style="color:#e2e2e2;" />
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <a href="Thanks Message Conditions.aspx" title="">Thanks Message Conditions</a><br />
    <!--              Report%20General%20Settings.html<br />
                KP_Introduction.html<br /> --> 
                <a href="Multi-Languages Settings.aspx" title="Multi Languages">Multi Languages Settings</a><br />
                <a href="Score_Introduction.aspx" title="Score Introduction">Score Introduction</a><br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
