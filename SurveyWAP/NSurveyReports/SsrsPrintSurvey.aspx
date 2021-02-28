<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SsrsPrintSurvey.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyReports.SsrsPrintSurvey" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SurveyProject SSRS Report</title>
</head>
<body>
    <form id="form1" runat="server">
 

<div style="display:block; font-family: arial; font-size:0.8em; margin:20px 0 20px 175px;">
    <span style="margin:2px 10px 0 0;">
     <asp:ImageButton ID="btnBack" Width="16px" BorderWidth="0" ImageUrl="~/Images/back_button.gif" runat="server" CssClass="buttonBack" OnCommand="OnBackButton" Visible="true" ToolTip="Go back to SSRS Reports" />
</span>

            <asp:Label ID="ChooseSurveyLabel" AssociatedControlID="ddlSurveys" runat="server" Visible="false"></asp:Label>
    &nbsp;&nbsp;
            <asp:DropDownList ID="ddlSurveys" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSurveys_SelectedIndexChanged1"
                Visible="false">
            </asp:DropDownList>
   
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <rsweb:ReportViewer runat="server" ID="reportViewer" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1105px" Height="100%" SizeToReportContent="True">
                <localreport reportpath="NSurveyReports\SsrsSurveyQAPrint.rdlc">
                </localreport>
            </rsweb:ReportViewer>
    </form>
</body>
</html>
