<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#FieldTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">Field Types Introduction</h2><br />
<br />
<br />
<hr style="color:#e2e2e2;" /><br />
Field types are the different type of objects to choose from that can be used to have the respondent submit [text based] information or answers. 
Field types range from basic textboxes to complex field types like HTML editors, calendars or slider
fields. It is possible to create new field types using the integrated Answer Type Creator.
<br />
<br />
The following field types are available by default:<br />
<br />
* <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/NSurveyAdmin/Help/Field - Basic.aspx" ToolTip="Field - Basic" Text="Field - Basic">Field - Basic</asp:HyperLink><br />
* Field - Large<br />
* Field - Required<br />
* Field - Email<br />
* Field - Calendar<br />
* Field - Rich<br />
* Field - Hidden<br />
* Field - Password<br />
* <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/NSurveyAdmin/Help/Field - Slider.aspx" ToolTip="Field - Slider" Text="Field - Slider">Field - Slider</asp:HyperLink><br />
<br />
<br />
<br />
<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3><br />
<br />            </td>
        </tr>
    </table>
</div></div></asp:Content>


