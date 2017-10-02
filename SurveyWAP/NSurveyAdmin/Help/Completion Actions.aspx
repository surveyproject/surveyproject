<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Completion" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Completion Actions</h2>
                <hr style="color:#e2e2e2;" />
               
                Once respondents have finished answering the survey on submitting their answers
                the survey can be set to trigger a "completion action" at the end of the survey. <br /><br />
                Available actions:<br />
                <br />
                <b>&quot;Thank you&quot; Message</b><br />
                <br />
                This will show a message (created with the text/html editor) to the respondent once the survey
                is finished and submitted. It could be used to thank the user or possibly provide some information
                about the survey taken. <br />
                <br /><i>Settings</i><br /><br />
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
                Text/html editor to create (write and style) the message. This is the message that will be shown to the end user once the survey is completed.
                <br />
                <br /> <br />
                <b>URL Redirection</b><br />
                <br />
                This action will redirect the user to a specific (web)page or URL once the survey is completed.<br />
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
                This is the URL to which the respondent will be redirected. It must be in following format: http://www.yourdomain.com or 
                http://www.youdomain.com/yourpage.aspx<br />
                                <br />
                <br />
                <b>Results Report</b><br />
                <br />
                This action will redirect the user to the Resultsreport webpage once the survey is completed and submitted. 
                Through the results report the respondent
                will get an overview of all survey questions, the answers submitted and scores (if set). An option to print the report is shown on the page.
                                <br />
           
                <br />
                * Redirection URL<br />
                This is used to set the URL to the Resultsreport of the survey where the respondent will be redirected on completing the survey. 
                The content and layout of the resultsreport is based on and similar to the <a href="New/Voter report.aspx" title="Voter Report">Voter Report</a>.
                                                <br />
                <br />

                * To set the correct Resultsreport 'redirection URL' go to menuoption 'Campaigns/ Web' go to the 'Deployment URL' <br />
                * copy the surveyid part of the url (e.g. surveyid=bb058c21-861d-450e-a2a5-4601467a59ca )<br />
                * go back to the Completion menu<br />
                * type the following url including questionmark: NSurveyReports\resultsreport.aspx?<br />
                * paste the surveyid at the end of the typed part<br /><br />
                URL Endresult: NSurveyReports\resultsreport.aspx?surveyid=bb058c21-861d-450e-a2a5-4601467a59ca 
                <br />
                <br />Note: the report option does not work with the Friendly URL
                <br />
                <hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
                <a href="Thanks Message Conditions.aspx" title="">Thanks Message Conditions</a><br />
                                <a href="New\Advanced Completion.aspx" title="Advanced Completion">Advanced Completion</a><br />
                <a href="Multi-Languages Settings.aspx" title="Multi Languages">Multi Languages Settings</a><br />
                <a href="Score_Introduction.aspx" title="Score Introduction">Score Introduction</a><br />
                <a href="New/Voter report.aspx" title="Voter Report">Voter Report</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
