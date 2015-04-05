<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Advanced Completion</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

<hr /><br />
<br />
Survey also provides several programmatic ways to hook your own .NET
methods to integrate it further in your environment or with third party
tools.<br />
<br />
Here are a few code samples taken from the CSharp_Samples directory that
is available in your Program files/Survey directory when we installed
Survey using MSI installation.<br />
<br />
Form Submitting Event<br />
Using this event we can manage the data posted by the respondent before
they got stored in the database at this point the voter id is still a
temporary id and its the last time we can modify voter information or
answers before they get stored in the database.<br />

<br />
protected void Page_Load(Object sender, EventArgs e)<br />
{<br />
MySurvey.SurveyId = int.Parse(Request[&quot;SurveyId&quot;]);<br />
MySurvey.FormSubmitting += new FormEventHandler(FormSubmitting);<br />
}<br />
<br />
protected void FormSubmitting(Object sender, FormItemEventArgs e)<br />
{<br />
Response.Write(String.Format(&quot;&lt;b&gt;form submitting for survey number<br />
{0}!&lt;/b&gt;&lt;br&gt;&quot;,<br />
((SurveyBox)sender).SurveyId));<br />
PrintAnswers(e.VoterAnswers.VotersAnswers);<br />
PrintVoterInfo(e.VoterAnswers.Voters[0]);<br />
}<br />
<br />
private void PrintAnswers(VoterAnswersData.VotersAnswersDataTable answers)<br />
{<br />
foreach (VoterAnswersData.VotersAnswersRow answer in answers)<br />
{<br />
Response.Write(String.Format(&quot;Answer Id = {0}&lt;br&gt;&quot;, answer.AnswerId));<br />
Response.Write(String.Format(&quot;Question Id = {0}&lt;br&gt;&quot;, answer.QuestionId));<br />
Response.Write(String.Format(&quot;Answer text entered if its a field =<br />
{0}&lt;br&gt;&quot;, answer.AnswerText));<br />
}<br />
}<br />
<br />
private void PrintVoterInfo(VoterAnswersData.VotersRow voter)<br />
{<br />
Response.Write(String.Format(&quot;Voter Id = {0}&lt;br&gt;&quot;, voter.VoterId));<br />
Response.Write(String.Format(&quot;Vote date = {0}&lt;br&gt;&quot;, voter.VoteDate));<br />
Response.Write(String.Format(&quot;Validated = {0}&lt;br&gt;&quot;, voter.Validated));<br />
}<br />
<br />

<br />
Form Submitted Event<br />
Using this event we can manage the data posted by the respondent after
they got stored in the database at this point the voter id is the one
stored and returned by the database.<br />
<br />
protected void Page_Load(Object sender, EventArgs e)<br />
{<br />
MySurvey.SurveyId = int.Parse(Request[&quot;SurveyId&quot;]);<br />
MySurvey.FormSubmitted += new FormEventHandler(FormSubmitted);<br />
}<br />
<br />
protected void FormSubmitted(Object sender, FormItemEventArgs e)<br />
{<br />
Response.Write(String.Format(&quot;&lt;b&gt;form submitted for survey number<br />
{0}!&lt;/b&gt;&lt;br&gt;&quot;,<br />
((SurveyBox)sender).SurveyId));<br />
PrintAnswers(e.VoterAnswers.VotersAnswers);<br />
PrintVoterInfo(e.VoterAnswers.Voters[0]);<br />
}<br />
<br />
private void PrintAnswers(VoterAnswersData.VotersAnswersDataTable answers)<br />
{<br />
foreach (VoterAnswersData.VotersAnswersRow answer in answers)<br />
{<br />
Response.Write(String.Format(&quot;Answer Id = {0}&lt;br&gt;&quot;, answer.AnswerId));<br />
Response.Write(String.Format(&quot;Question Id = {0}&lt;br&gt;&quot;, answer.QuestionId));<br />
Response.Write(String.Format(&quot;Answer text entered if its a field =<br />
{0}&lt;br&gt;&quot;, answer.AnswerText));<br />
}<br />
}<br />
<br />
private void PrintVoterInfo(VoterAnswersData.VotersRow voter)<br />
{<br />
Response.Write(String.Format(&quot;Voter Id = {0}&lt;br&gt;&quot;, voter.VoterId));<br />
Response.Write(String.Format(&quot;Vote date = {0}&lt;br&gt;&quot;, voter.VoteDate));<br />
Response.Write(String.Format(&quot;Validated = {0}&lt;br&gt;&quot;, voter.Validated));<br />
}<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Thanks%20Message%20Conditions.html<br />
Survey%20Copyright%20and%20Disclaimer.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
