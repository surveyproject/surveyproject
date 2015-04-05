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
                    Report Builder</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
The report builder is the heart of Survey's reporting features as it
allows us create and edit the report we are going to analyze. A report
can be build around report items, we can combine different
RI_Introduction.html together as we need it.<br />
<br />
By default Survey offers already many types of RI_Introduction.html to
build our reports but it is very easy to develop and add our own specific
RI_Introduction.html inside Survey's reporting engine.<br />
<br />
Page Options<br />
<br />
*  will move the page up until it reaches the next page break above.<br />
<br />
*  will move the page down until it reaches the next page break below.<br />
<br />
* Delete will delete the page. Report items on the deleted page will be
  moved to the remaining page in the report.<br />
<br />
* Insert%20Report%20Item.html inserts a new report item at the end of
  the page. If we want to insert a report item before another one we can
  use the insert report item link at the report item options level.<br />
<br />
Report Item Options<br />
<br />
* will move a report item's position up.<br />
<br />
* will move a report item's position down.<br />
<br />
* Edit the report item using the Report Item Editor.<br />
<br />
* Delete the report item.<br />
<br />
* Clone makes an exact copy of the question.<br />
<br />
* Insert%20Report%20Item.html inserts a new report item before the
  current report item. If we want to insert a report item at the end of
  the page we can use the insert report item link at the page options
  level.<br />
<br />
* Insert Report Page Break inserts a page break before the current
  report item.<br />
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
Report%20General%20Settings.html<br />
Web%20Control%20Style.html<br />
                Report%20Analysis.html<br />
Web%20Control%20Style.html<br />
                Report%20Filter%20Creation.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

