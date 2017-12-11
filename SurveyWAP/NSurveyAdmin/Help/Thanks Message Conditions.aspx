<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Completion" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Thanks Message Conditions</h2>
                <hr style="color:#e2e2e2;" />
                With the "Thanks Message Conditions" logical rules can be created based
                on the respondent's answers to show a specific completion message at the end of a survey. For
                example a specific message could be shown to voters who answered choice X at
                question Y.<br />
                <br />
                Each condition is based on a set of predefined rules. The first condition that is
                met will show its &quot;Thank You&quot; message to the respondent. An unlimited number
                of conditions is possible that can be ordered or re-ordered at any time.<br />
                <br />
                * <strong>Condition</strong><br />
                Select a show condition
                <br /><br />
                * <strong>Question</strong><br />
                Select the Question
                <br /><br />
                * <strong>Conditional Operator</strong><br />
                E.g. Answered/ Not Answered
                <br /><br />
                * <strong>Answer</strong><br />
                Select the Answer
                <br /><br />
                * <strong>Evaluation Condition</strong><br />
                Select a condition: e.g. less than, greater etc.
                <br /><br />
                * <strong>Text</strong><br />
                Enter text to evaluate
                <br /><br />
               * <strong>Message Shown on Submit</strong><br />
                Text/Html editor to write and edit the message that will be shown on submitting the survey and based on the conditions.
                <br /><br />Note: if the Score feature is enabled (survey settings) and set on answer level the score results can be shown in the completion message by adding 
                <code>::score::</code> to the text.
                
                <hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
                <a href="Completion Actions.aspx" title="Completion Actions">Completion Actions</a><br />
                <a href="New\Advanced Completion.aspx" title="Advanced Completion">Advanced Completion</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
