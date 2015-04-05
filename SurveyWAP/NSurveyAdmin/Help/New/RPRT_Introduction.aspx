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
                    Reporting Introduction </h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Survey's reporting builder allows us to create reports and results
analysis in an easy way.<br />
<br />
Each report is composed of RI_Introduction.html.  We can composed these
items together to build a report that will suit our anlaysis need. We can
have any number of report items we want per report, however it is
recommended to use different paging depending on the load of data to
analysis.<br />
<br />
To learn more about report please check the HowToReport_Introduction.html<br />
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
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

