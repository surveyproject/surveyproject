﻿<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Report Filter Editor</h2><hr style="color:#e2e2e2;" />
                <br />

Report filters are used to setup a set of rules based on the
survey's questions and answers. Once a filter is created and
rules are added to it, it can be assigned to a report or a chart. Once a filter as been applied all results
calculation will be done accordingly to the filter that has been set up.<br />
<br />
* <i>Filter Name</i> - is the name of the filter that will contain the rules.<br />
<br />
* <i>Conditional Rule Operator</i> - is the operator that Survey will use to
  evaluate the rules inside the filter. At this time only a single
  conditional operator (AND, OR) can be applied to a group of rules
  inside a filter.<br />
<br />
<u>Add New Rule To Filter</u><br /><br />
Option to choose a question from which to select an answer for a 
rule. Based on the answer type there areseveral more options :<br />
<br />
* Selection answer types have no extra features.<br />
<br />
* Text entry answers allow to setup an extra rule to see if the text
  entered match that rule. If no text is specified, Survey Project will
  consider the rule valid if any text has been entered by the respondent.<br />
<br />
Once a new rule is created for the condition any number
of new rules can be added to the filter as needed. If there is more than one rule the
logical operator (AND, OR) of the filter will be used to evaluate the
rules together in the filter.
                <br /><br />

                                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
<a href="Reports_Introduction.aspx" title=" Reports Introduction " > Reports Introduction </a>	<br />
<a href="../Statistics.aspx" title=" Survey Statistics " > Survey Statistics </a>	<br />
<a href="Graphic Reports.aspx" title=" Graphic Reports " > Graphic Reports </a>	<br />

<a href="Cross Tabulation.aspx" title=" Cross Tabulation " > Cross Tabulation </a>	<br />
<a href="Report Filter Creation.aspx" title=" Report Filter Creation " > Report Filter Creation </a>	<br />

<a href="File Manager.aspx" title=" File Manager " > File Manager </a>	<br />

<a href="Voter report.aspx" title=" Respondent / Voter Report " > Respondent Report </a>	<br />
<a href="Voter Entries List.aspx" title=" Voter Entries List " > Respondents Entries List </a>	<br />
<a href="Voter Report Edit.aspx" title=" Voter Report Edit " > Respondent Report Edit </a>	<br />

<a href="Data Export.aspx" title=" Data Export " > Data Export </a>	<br />
<a href="Data Import.aspx" title=" Data Import " > Data Import </a>	<br />

<a href="SsrsReports.aspx" title=" Ssrs Reports " > SSRS Reports </a>	<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

