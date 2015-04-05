<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="PageBranchingRulesControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.UpdateVoterPageBreakOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="UpdateVoterPageBreakOptionsControl.ascx.cs" %>
<table class="questionBreakOptions" width="100%">
    <tr>
        <td nowrap="nowrap" width="85px">
            &nbsp;<font class="secAddIn">
                <asp:Literal ID="PageTitle" runat="server" EnableViewState="False">Page</asp:Literal>
                <asp:Label ID="PageLabel" runat="server"></asp:Label></font>&nbsp;
        </td>
        <td class="questionBreak" align="right">
            <asp:LinkButton ID="ClearPageAnswersButton" runat="server">Clear page answers</asp:LinkButton>&nbsp;
        </td>
    </tr>
</table>
