<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Reporting Introduction</h2><hr style="color:#e2e2e2;" />

Survey&trade; Project offers several reporting options to view and present survey related information on different levels (aggregated, detailed etc).<br />
<br />
<dl>
  <dt>Survey Statistics</dt>
  <dd>- Statistics i.c. general information on an individual survey (e.g. number of voters, viewtimes)<br />&nbsp;</dd>
  <dt>Graphical Reports</dt>
  <dd>- Aggregated survey results per survey and/ or survey question(s) including scores presented graphically</dd>
  <dd>- Several Graphical/ charting options (Column, Bubble, Bar, Pie)<br />&nbsp; </dd>
      <dt>Cross Tabulation Reports</dt>
    <dd>
        - Option to present and compare the results of two different questions from a particular survey<br />&nbsp;
    </dd>
          <dt>Individual Results Reports</dt>
    <dd>
        - Report showing the complete set of answers (reponse) of an individual repondent to a survey. Includes editing options.<br />&nbsp;
    </dd>
              <dt>SSRS Reports</dt>
    <dd>
        -  Option to add and present 'custom made' SSRS reports on survey data through the SP webapplication.
    </dd>

</dl>
<u>Additional Options</u><br />
* Data Import/ Export: <br />option to import (xml)/ export (xml/csv) surveys answers<br />
* File Manager: <br />option to manage files uploaded to SP through the fileupload answertype that can be used in surveys<br />
                * Report Filters: <br />option to create and set filters (e.g. periods) to be used on graphical reports<br />

<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
<a href="../Statistics.aspx" title=" Survey Statistics " > Survey Statistics </a>	<br />
<a href="Graphic Reports.aspx" title=" Graphic Reports " > Graphic Reports </a>	<br />
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

