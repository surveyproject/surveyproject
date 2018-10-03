<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Graphics Report</h2><hr style="color:#e2e2e2;" />
                <br />

This report item will show graphical charts and statistics for
questions and answers.<br />
<br />

<u>Filters / Answers Filters</u><br /><br />

* Filter Start Date - is the start date interval on which the results of
  the chart report RI_Introduction.html will be calculated.<br />
<br />
* Filter End Date - is the end date interval on which the results of the
  chart report RI_Introduction.html will be calculated.<br />
<br />
* Assign A Filter - using the Report%20Filter%20Creation.html its possible to
  create specific filters. For example if one only wants to display the
  results of respondents who have chosen answer B to the question Z.<br />
<br />
* Language Filter - option to filter the results of the chart report
  RI_Introduction.html by the language chosen by the respondent.<br />
<br />
  This feature is only available if Multi language is turned on the survey's settings
  ML_Introduction.html features.<br />
<br />
<u>Extended Filters</u><br /><br />
The extended filters allows to filter reportresults based on respondents answers. To
learn more about extended filters please read EF_Introduction.html .
<br />
<br />
<u>Report Question(s)</u><br /><br />
Option to select for which questions to show the graphic charts.<br />
<br />

<u>Graphics Properties</u><br />
<br />
* Column Chart will render a group of column charts to display the results.<br />
<br />
* Bar Chart will render a group of bar charts to display the results.<br />
<br />
* Pie Chart will render a pie chart to display the results.<br />
<br />
* Bubble Chart will render bubble charts to display the results.<br />
<br />
* HTML Chart will render lines to display the results.<br />
<br />
<u>Charts Properties</u><br /><br />
The charts provide the following information during runtime analysis of  :<br />
<br />
* Voters - the number of individual respondents who did answer to
  this question, if ther are multiple choices answers and a respondent has
  selected more than one answers he/she will be still counted as one
  respondent .<br />
<br />
* Rating - is the average rating value for the question based on the
  answer selection types that are marked as rating part.<br />
<br />
  This feature is only available if rating is turned on
  Rating_Introduction.html from the Question%20Editor.html  and if there
  is at least one selection type answer that is marked as a rate part .<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
<a href="Reports_Introduction.aspx" title=" Reports Introduction " > Reports Introduction </a>	<br />
<a href="../Statistics.aspx" title=" Survey Statistics " > Survey Statistics </a>	<br />
<a href="Cross Tabulation.aspx" title=" Cross Tabulation " > Cross Tabulation </a>	<br />

<a href="Report Filter Creation.aspx" title=" Report Filter Creation " > Report Filter Creation </a>	<br />
<a href="Report Filter Editor.aspx" title=" Report Filter Editor " > Report Filter Editor </a>	<br />

<a href="File Manager.aspx" title=" File Manager " > File Manager </a>	<br />

<a href="Voter report.aspx" title=" Respondent / Voter Report " > Respondent Report </a>	<br />
<a href="Voter Entries List.aspx" title=" Voter Entries List " > Respondents Entries List </a>	<br />
<a href="Voter Report Edit.aspx" title=" Voter Report Edit " > Respondent Report Edit </a>	<br />

<a href="Data Export.aspx" title=" Data Export " > Data Export </a>	<br />
<a href="Data Import.aspx" title=" Data Import " > Data Import </a>	<br />

<a href="SsrsReports.aspx" title=" Ssrs Reports " > SSRS Reports </a>	<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

