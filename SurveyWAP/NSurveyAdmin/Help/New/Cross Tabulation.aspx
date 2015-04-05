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
                    Cross Tabulation</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This report item will allow us to make a cross tabulation results between
two questions.<br />
<br />
Suppose we have two questions :<br />
What is your age ?<br />
How do you find our web site ?<br />
<br />
Using cross tabulation of the two question we can see the relation
between their answers.<br />
<br />
Edit Free Text Report Item<br />
<br />
* Report Item Title is an optional text we can specify that will show
  up on top on the report item.<br />
<br />
* Use Aliases do we want to show question and answers reporting aliases
  instead of showing the question / answer label text.<br />
<br />
* Multiple Items Layout  if our report item supports multiple items
  selections like multiple answers or questions we can display the
  results either vertically or horizontally.<br />
<br />
* Columns Number is the number of columns in which we want to display
  the items inside our report item. <br />
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
learn more about extended filters please read EF_Introduction.html .
<br />
<br />
Report Question(s)<br />
We can select two questions that will be part of the cross tabulation.<br />
<br />
Cross Tabulation Properties<br />
<br />
* Base Question is the question that has its answers used as rows in
  the cross tabulation.<br />
<br />
* Compare Questionis the question that has its answers used as columns
  in the cross tabulation.<br />
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

