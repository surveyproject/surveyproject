<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.SecurityAddInVoterReportControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="SecurityAddInVoterReportControl.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="PageBranchingRulesControl.ascx" %>
<table class="innerText" id="Table1">
    <tr>
        <td colspan="2">
            <font class="titleFont">
                <asp:Literal ID="AddInDescriptionTitle" runat="server" EnableViewState="False"></asp:Literal></font>
            <br />
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;&nbsp;
        </td>
        <td>
            <asp:PlaceHolder ID="VoterDetailsPlaceHolder" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
</table>
