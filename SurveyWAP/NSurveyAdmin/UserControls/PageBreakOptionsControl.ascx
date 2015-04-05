<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="PageBranchingRulesControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.PageBreakOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="PageBreakOptionsControl.ascx.cs" %>
<table class="questionBreakOptions" style="width:100%; height:18px;">
    <tr>
        <td style="width:7px;">
            <asp:ImageButton ID="UpImageButton" runat="server" ImageUrl="~/NSurveyAdmin/images/questionupbutton.gif">
            </asp:ImageButton>
        </td>
        <td style="width:7px;">
            <asp:ImageButton ID="DownImageButton" runat="server" ImageUrl="~/NSurveyAdmin/images/questiondownbutton.gif">
            </asp:ImageButton>
        </td>
        <td nowrap="nowrap" style="width:60px; height:18px;">
           <b>
                <asp:Literal ID="PageTitle" runat="server" EnableViewState="False">Page</asp:Literal>
                <asp:Label ID="PageLabel" runat="server"></asp:Label></b>
        </td>
        <td class="questionBreak">
            <asp:LinkButton ID="DeleteButton" runat="server">Delete</asp:LinkButton>|
            <asp:HyperLink ID="InsertHyperLink" runat="server">Insert question</asp:HyperLink>|
            <asp:LinkButton ID="InsertLineBreakButton" runat="server">Insert line break</asp:LinkButton>|
            <asp:LinkButton ID="EnableRandomHyperlink" runat="server">Enable random</asp:LinkButton>|
            <asp:HyperLink ID="BranchingHyperLink" runat="server">Edit branching</asp:HyperLink>|
            <asp:LinkButton ID="EnableSubmitHyperlink" runat="server">Enable submit</asp:LinkButton>
        </td>
    </tr>
</table>

            <uc1:PageBranchingRulesControl ID="PageBranchingRules" runat="server"></uc1:PageBranchingRulesControl>

