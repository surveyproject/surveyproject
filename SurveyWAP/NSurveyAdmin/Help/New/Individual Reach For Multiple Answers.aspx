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
                    Individual Reach For Multiple Answers (IRMA)</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This report item will allow us to see for multiple choices question the
combination of choices answered by a single respondent.<br />
<br />
Suppose we have following multiple choices question :<br />
What do you drink ?<br />
 [] Pepsi<br />
 [] Water<br />
 [] Milk<br />
 [] Fanta<br />
<br />
Using the other report items we would only be able to know either the
total of answers for all choices or the total number of individual
respondent of the question.<br />
<br />
Using IRMA we can know exactly for the reach of individual respondent for
the different answers combinations of the question.<br />
<br />
IRMA offer us two different analysis method.<br />
<br />
1) We can choose specific answers and see the reach for the combination
of these answers.<br />
<br />
2) We can choose a number of combinations of answers and IRMA will
calculate the different combinations along with their individual
respondent reach.<br />
<br />
<br />
Edit Free Text Report Item<br />
<br />
* Report Item Title is an optional text we can specify that will show
  up on top on the report item.<br />
<br />
* Use Aliases do we want to show question and answers reporting aliases
  instead of showing the question / answer label text.<br />
<br />
Filters / Answers Filters<br />
<br />
* Override Report Filters allows us to override the filters that have
  been set at the report level.<br />
<br />
* Filter Start Date is the start date interval on which the results of
  the RI_Introduction.html will be calculated.<br />
<br />
* Filter End Date is the end date interval on which the results of the
  RI_Introduction.html will be calculated.<br />
<br />
* Assign A Filter using the Report%20Filter%20Creation.html we can
  create specific filters for example if we only want to display the
  results of respondent who have chosen answer B to the question Z.<br />
<br />
* Language Filter we can filter the results of the RI_Introduction.html
  by the language chosen by the respondent.<br />
<br />
  This feature is only available if we have turned on Survey's
  ML_Introduction.html features.<br />
<br />
Extended Filters<br />
The extended filters allow us to filter based on respondents answers. To
learn more about extended filters please read EF_Introduction.html .<br />
<br />
<br />
Report Question(s)<br />
We can select one multiple choices question from which will analyze the
answers combinations.<br />
<br />
Individual Calculation Type<br />
<br />
* Answer Reached is the selected answer combination we want to analyze
  against.<br />
<br />
* Answer Number Reached is the number of answers we want to be part in
  our combination calculation.<br />
<br />
<br />

<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
HowToReport_Introduction.html<br />
RI_Introduction.html<br />
Insert%20Report%20Item.html<br />
Report%20Item%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

