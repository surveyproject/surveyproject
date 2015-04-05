<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Score" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Score Introduction</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;" />
                <br />
                Surveys can be set up to be scored. With the scoring feature any number of points can be assigned to any of the answers
                 that can be selected in a question.<br />
                <br />
                The score will be calculated dynamically as the respondent is taking the survey.
                Scoring systems are used in for example in training institutions to create multiple
                choices tests. The Score feature has to be activated on survey level first by checking the score
                checkbox from the <a href="Survey%20General%20Settings.aspx">Settings</a> tab.<br />
                <br />
                The following example will demonstrate the basic principle of the scoring option.
                Suppose there is a question similar to the one below with a list of answers and
                each answer has a specific score value.<br />
                <br />
                What is the the best open source survey tool on the web?
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
                total score eg: "you've failed" or "you've passed". The total score can be shown also by adding ::score:: to the text.<br />
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <a href="Survey%20General%20Settings.aspx">Settings</a><br />
                <a href="Thanks%20Message%20Conditions.aspx">Completion - Conditional Thank You Message</a><br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
