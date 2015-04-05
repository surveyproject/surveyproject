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
                    Report General Settings</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
From here we can create a new report and setup its base settings and
filters.<br />
<br />
Report Options<br />
<br />
* Clone  will make an exact copy of the report and all report items
  that have been assigned to it.<br />
<br />
Report Information<br />
<br />
* Report Name is the name of the report. <br />
<br />
* Public Report if our report is shared across other Survey users. Note
  that a report must also be public in order to make it publicly
  available to respondents of a survey. <br />
<br />
Answer Filters<br />
<br />
* Filter Start Date is the start date interval on which the results of
  the RI_Introduction.html will be calculated.<br />
<br />
* Filter End Date is the end date interval on which the results of the
  RI_Introduction.html will be calculated.<br />
<br />
* Assign A Filter using the Report%20Filter%20Creation.html we can
  create specifc filters for example if we only want to display the
  results of respondent who have choosen answer B to the question Z.<br />
<br />
* Language Filter we can filter the results of the RI_Introduction.html
  by the language choosen by the respondent.<br />
<br />
  This feature is only available if we have turned on Survey's
  ML_Introduction.html features.

<br />
<br />
RI_Introduction.html are able to have their own filter settings and
override the main report filters settings .<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
HowToReport_Introduction.html<br />
RI_Introduction.html<br />
                ED_Introduction.html<br />
Report%20Builder.html<br />
Web%20Control%20Style.html<br />
                Report%20Analysis.html<br />
Web%20Control%20Style.html<br />
                Report%20Filter%20Creation.html<br />
Web%20Control%20Style.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

