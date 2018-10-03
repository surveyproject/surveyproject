<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Individual Responses List</h2><hr style="color:#e2e2e2;" />
                <br />

This webpage list the respondents (voters by voterid and date) who participated in
the survey including some of the data and answers to the survey.  <br /><br />
                
                Through this webpage the individual resultsreport is accessed where all questions and answsers are presented including the option 
                to edit already submitted results.
                
                <br /><br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <a href="Reports_Introduction.aspx" title=" Reports Introduction " > Reports Introduction </a>	<br />
<a href="../Statistics.aspx" title=" Survey Statistics " > Survey Statistics </a>	<br />
<a href="Graphic Reports.aspx" title=" Graphic Reports " > Graphic Reports </a>	<br />
<a href="Cross Tabulation.aspx" title=" Cross Tabulation " > Cross Tabulation </a>	<br />

<a href="Report Filter Creation.aspx" title=" Report Filter Creation " > Report Filter Creation </a>	<br />
<a href="Report Filter Editor.aspx" title=" Report Filter Editor " > Report Filter Editor </a>	<br />

<a href="File Manager.aspx" title=" File Manager " > File Manager </a>	<br />

<a href="Voter report.aspx" title=" Respondent / Voter Report " > Respondent Report </a>	<br />
<a href="Voter Report Edit.aspx" title=" Voter Report Edit " > Respondent Report Edit </a>	<br />

<a href="Data Export.aspx" title=" Data Export " > Data Export </a>	<br />
<a href="Data Import.aspx" title=" Data Import " > Data Import </a>	<br />

<a href="SsrsReports.aspx" title=" Ssrs Reports " > SSRS Reports </a>	<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

