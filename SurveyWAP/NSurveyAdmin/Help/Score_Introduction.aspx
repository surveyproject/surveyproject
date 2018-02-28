<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Score" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Score Introduction</h2>
                <hr style="color:#e2e2e2;" />
                Surveys can be set up to activate scoring options. With the scoring feature points can be assigned to the answers
                 that can be selected in a question.<br />
                <br />
                The (total) score will be calculated dynamically as the respondent is taking the survey.
                Scoring systems are used for example in training institutions to create multiple
                choices tests. The Score feature has to be activated on survey level first by checking the Score
                checkbox on the <a href="Survey%20General%20Settings.aspx">Survey Settings</a> page.<br />
                <br />
                The following example will demonstrate the basic working of the scoring option.
                Suppose there is a question similar to the one below with a list of answers and
                each answer has a specific value (score) assigned to it.<br />
                <br /> What is the the best open source survey tool on the web?
                <ul style="position: relative; left: 10px;">
                    <li>Nsurvey (scores 1)</li>
                    <li>Surveymonkey (scores 2)</li>
                    <li>Limesurvey (scores 3)</li>
                    <li>Survey&trade; Project (scores 4)</li>
                </ul>
                <br />
                Based on the selected answer it is possible to determine if the respondent has correctly
                answered this question. If his score on the question is 4 the answer to the question
                would be correct.<br />
                <br />
                If there are multiple questions at the end of the survey a <a href="Thanks%20Message%20Conditions.aspx">
                   conditional Thank You Message</a> could be used to show a specific text based on the respondent
                total score eg: "you've failed" or "you've passed". The total score can be shown also by adding ::score:: to the message text.<br />
                <hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
                <a href="Survey%20General%20Settings.aspx">Survey Settings</a><br />
                <a href="Thanks%20Message%20Conditions.aspx">Completion - Conditional Thank You Message</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
