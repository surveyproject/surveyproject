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
                    Insert Report Item</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

We can insert as many report items per report as we want. Survey provides
out of the box following report items :<br />
<br />
* Free%20Text%20Report.html<br />
* Graphic%20Reports.html<br />
* Cross%20Tabulation.html<br />
* Voter%20Entries%20List.html<br />
* Individual%20Reach%20For%20Multiple%20Answers.html (IRMA)<br />
* Matrix%20Grid%20Report.html<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
HowToReport_Introduction.html<br />
RI_Introduction.htmlED_Introduction.html<br />
Report%20Builder.html<br />
Web%20Control%20Style.html<br />
                Report%20Analysis.html<br />
Web%20Control%20Style.html<br />
                Report%20Filter%20Creation.html<br />
                ML_Introduction.ht<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

