<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
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

This report item offers the option to make a cross tabulation results between
two questions.<br />
<br />
Suppose there are two questions :<br />
- What is your age ?<br />
- How do you find our web site ?<br />
<br />
Using cross tabulation of the two question its possible to see the relation
between their answers.<br />

<br />
<u>Report Question(s)</u><br /><br />
Option to select two questions that will be part of the cross tabulation.<br />
<br />
Cross Tabulation Properties<br />
<br />
* Base Question is the question that has its answers used as rows in
  the cross tabulation.<br />
<br />
* Compare Question is the question that has its answers used as columns
  in the cross tabulation.<br />
<br />

<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3>
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

