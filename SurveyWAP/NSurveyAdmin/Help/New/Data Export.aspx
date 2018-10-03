<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">Data Export</h2><hr style="color:#e2e2e2;" />
                <br />
Using the data export tool it is possible to export all respondents answers
to any external third party reporting tool like Excel or SSPS.<br />
<br />
* <i>Data Selection</i> -  Option to export all answers or only
  specific answers of a survey.<br />
<br />
* <i>Export in</i> - is the format in which to export respondents
  answers. CSV is the most widely accepted format, especially with older
  tools. However Xml can be used with the new versions of Excel or
  Access.<br />
<br />
*<i> Field Delimiter</i> - is the character (char) that will act as a delimiter between the
  exported answers columns inside a CSV.<br />
<br />
* <i>Text Delimiter</i> - is the char that will act as a delimiter between the
  text entries inside a CSV.<br />
<br />
* <i>Replace CR</i> - is the char that will replace &quot;new line&quot; character. Some
  tools like Excel or Access might encounter problems with &quot;new line&quot;
  characters inside a row. Its advised to replace it with a custom char
  before replacing it again after having imported the data inside the
  target tool.<br />
<br />
* <i>Export Layout</i> option to determine the layout of the export file based on the order of columns and/or rows and question and answers<br />
<br />
* <i>Export From Date</i> -  is the start date interval from which to
  export. <br />
<br />
* <i>To Date</i> - is the end date interval from which to export. 

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
                <a href="Voter Entries List.aspx" title=" Voter Entries List " > Respondents Entries List </a>	<br />
<a href="Voter Report Edit.aspx" title=" Voter Report Edit " > Respondent Report Edit </a>	<br />

<a href="Data Import.aspx" title=" Data Import " > Data Import </a>	<br />

<a href="SsrsReports.aspx" title=" Ssrs Reports " > SSRS Reports </a>	<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

