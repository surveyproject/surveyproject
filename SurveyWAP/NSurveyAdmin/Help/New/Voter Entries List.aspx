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
                    Voter Entries List</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This report item will list the respondents (voters) who participated in
the survey.<br />
<br />
Edit Free Text Report Item<br />
<br />
* Report Item Title is an optional text we can specifiy that will show
  up on top on the report item.<br />
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
  create specifc filters. For example if we only want to display the
  results of respondents who have chosen answer B to the question Z.<br />
<br />
* Language Filter we can fillter the results of the
  RI_Introduction.html by the language choosen by the respondent.<br />
<br />
  This feature is only available if we have turned on Survey's
  ML_Introduction.html features.<br />
<br />
Extended Filters<br />
The extended filters allow us to filter based on respondents answers. To
learn more about extended filters please read EF_Introduction.html .<br />
<br />
<br />
Voter Entries List Properties<br />
<br />
* Paging Size we can turn on paging if we have a long list of
  respondents.<br />
<br />
* Text Sort Order we can sort the respondent list by date on which they
  took the survey.  <br />
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

