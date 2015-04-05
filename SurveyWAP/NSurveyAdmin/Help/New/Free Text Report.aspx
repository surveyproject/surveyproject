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
                    Free Text Report</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This report item will show us all free text entries that have been
entered by the respondents. We can choose what questions answers we want
to show the free text for and also choose if we want to group them by
respondents or by question's answers.<br />
<br />
Edit Free Text Report Item<br />
<br />
* Report Item Title is an optional text we can specify that will show
  up as information on top on the report item in the analysis mode.<br />
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
  create specific filters. For example if we only want to display the
  results of respondents who have chosen answer B to the question Z.<br />
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
Report Questions Answer(s)<br />
We can select from which answers we want to show the respondent's free
text entries. <br />
<br />
Free Text Report Properties<br />
<br />
* Display  we can either group the display by answers or by
  respondents. Grouping by respondent will show us all selected answers
  by respondent.<br />
<br />
* Paging Size we can turn on paging if we have a long list of free text
  answers entries.<br />
<br />
* Text Sort Order we can sort the text entries by alphabetical order. <br />
<br />
* Show Voter Details we can show more information about respondents
  like IP's, context username. <br />
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

