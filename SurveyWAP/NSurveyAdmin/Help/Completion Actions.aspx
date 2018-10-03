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
                <u>Redirection or Report URL</u>
                 <br /> <br />
                <b>A. Redirection URL</b><br />
                <br />
                This action will redirect the respondent to a specific (web)page or website URL once the survey is completed and submitted. If this option is used no "thank you" message
                will be shown.<br />
                <br />
                * Redirection URL<br />
                This is the URL to which the respondent will be redirected. It must be in following format: http://www.yourdomain.com or 
                http://www.youdomain.com/yourpage.aspx <br />
                                <br />
                <br />
                <b>B. Report URL</b><br />
                <br />
                This action will redirect the respondent to one of [2] the SP reports once the survey is completed and submitted. 
                <br /><br />
                <i>1. Results Report</i>
                <br />
                Through the resultsreport the respondent will get an overview of all survey questions, the answers submitted and scores (if set). 
                
                An option to print the report is shown on the page. The content and layout of the resultsreport 
                is based on and similar to the <a href="New/Voter report.aspx" title="Voter Report">Individual Results Report</a>.
                                <br /><br />

                <i>2. Custom Report</i>
                <br />
                 The customreport is used to present any data and/ or information based on a customized stored procedure [vts_spReportGetScores] 
                in the SP database. Through the stored procedure (calculated) values or query results can be send to the customreport webpage that is shown to the repondent after submitting the survey.
                <br /><br />
                                See <a href="New\CreateCustomReport.aspx" title="Creating Custom Reports">Creating Custom Reports</a> on how to create/ edit the custom report.
                                <br /><br />


                <u>Results Report - Redirection URL</u><br />
                <br />

                * To set the correct Resultsreport 'redirection URL' go to menuoption 'Campaigns/ Web' - 'Deployment URL' <br />
                * copy the surveyid part of the url (e.g. surveyid=bb058c21-861d-450e-a2a5-4601467a59ca )<br />
                * go back to the Completion menu<br />
                * type the following url including tilde sign [~] and questionmark [?]: ~\NSurveyReports\resultsreport.aspx?<br />
                * paste the surveyid at the end of the typed part<br /><br />
                URL Endresult: ~\NSurveyReports\resultsreport.aspx?surveyid=bb058c21-861d-450e-a2a5-4601467a59ca 
                <br />
                <br />Note: the report option does also work with the Friendly URL. Instead of the surveyid the FriendlyUrl namepart must be added.
                <br /><br />
                URL Endresult: ~\NSurveyReports\resultsreport.aspx\MyFriendlyName 
                <br /><br />

                <hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
                <a href="Thanks Message Conditions.aspx" title="">Thanks Message Conditions</a><br />
                                <a href="New\Advanced Completion.aspx" title="Advanced Completion">Advanced Completion</a><br />
                <a href="New\CreateCustomReport.aspx" title="Creating Custom Reports">Creating Custom Reports</a><br />
                <a href="Multi-Languages Settings.aspx" title="Multi Languages">Multi Languages Settings</a><br />
                <a href="Score_Introduction.aspx" title="Score Introduction">Score Introduction</a><br />
                <a href="New/Voter report.aspx" title="Voter Report">Individual Responses Reports</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
