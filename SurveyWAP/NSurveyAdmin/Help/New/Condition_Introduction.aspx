<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#ConditionalLogic" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">Conditional Logic Introduction</h2><hr style="color:#e2e2e2;" />
SP&trade; contains different options to make use of 'Conditional Logic' in a survey. 
                <br /><br />
                Using these conditional logic options survey elements will trigger specific actions based on the conditional rules and a 
                respondent's answers. 
                <br /><br />
<u>Conditional Logic options</u><br />
o Branching Conditions<br />
o Skip Logic Conditions<br />
o Thanks Message Conditions<br />
o Dynamic Content Conditions <br />
 <br />
A condition can be composed of one or more conditional rules that are tied together using conditional operators like AND, OR. 
 <br /><br />

<u>Conditional Rules</u><br />
A conditional rule is the basic element of a condition. Rules can build a condition based on the questions and answers available in a survey form 
                and are evaluated at runtime while the respondent answers the survey's questions.
<br /><br />
<i>Question Answered</i><br />- option to create a rule based on the questions and answers available in the survey form. 
                Once a question is selected it is possible to choose whether to setup a rule for a specific answer or for all answers of
                a question.
                <br /><br />
If the option is chosen to create a rule based on a specific answer there will be several more features available.
<br /><br />
<i>Selection answertype</i><br /> - no extra features.
                <br /><br />
<i>Text entry answers</i><br /> - option to create a rule to see if the text entered matches that rule. 
                If no text is specified SP&trade;  will consider the rule valid if any text has been entered by the respondent.
<br /><br />
<i>Score rules </i> <br />- option to create rules based on the score of the respondent. It can be either the total score at a given
                moment or a score for a specific question. Score features are only available if we have turned on the Scoring features.
<br /><br />
<i>Respondent Quota </i><br />- option to create rules based on the current number of the respondents to
                a question or an answer of a question. 
                Note that the quota is calculated based on the actual number of respondent who finished the survey. 
                It doesn't include the people who are currently taking the survey but who didn't finish it. 
                <br /><br />
<i>QueryString Variable </i><br />- option to create rules based on on a querystring variable value. 
                <br /><br />
<i>Session Variable</i><br /> - option to create rules based on on a server side session variable value.. 
<br /><br />
<i>Language </i><br />- option to create rules based on the selected language in which the survey is currently running. 
This feature is only available if the Multi-Language feature is activated. 
<br /><br />

Once a new rule is created for the condition any number of new rules can be added to the condition as needed.
                If there is more than one rule it is also possible to choose which logical operator will be used to evaluate
                the rules together in the condition.  

<br />            
<hr style="color:#e2e2e2;" /> <h3>More Information</h3><br />

<a href="Branching conditions.aspx" title=" Branching Conditions " > Branching Conditions </a>	<br />
<a href="Skip Logic Conditions.aspx" title=" Skip Logic Conditions " > Skip Logic Conditions </a>	<br />
                <a href="../Score_Introduction.aspx" title=" Score Introduction " > Score Introduction </a>	<br />
                <a href="../Completion Actions.aspx" title=" Completion Actions " > Completion Actions </a>	<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

