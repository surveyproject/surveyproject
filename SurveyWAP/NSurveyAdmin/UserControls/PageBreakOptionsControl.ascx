<%@ Register TagPrefix="uc1" TagName="PageBranchingRulesControl" Src="PageBranchingRulesControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.PageBreakOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="PageBreakOptionsControl.ascx.cs" %>
<div class="questionBreakOptions">
    
        <span>
            <asp:ImageButton ID="UpImageButton" runat="server" ImageUrl="~/Images/questionupbutton.gif">
            </asp:ImageButton>
        </span>
        <span>
            <asp:ImageButton ID="DownImageButton" runat="server" ImageUrl="~/Images/questiondownbutton.gif">
            </asp:ImageButton>
        </span>
        <span class="questionBreakPageNr">
                <asp:Literal ID="PageTitle" runat="server" EnableViewState="False">Page</asp:Literal>
                <asp:Label ID="PageLabel" runat="server"></asp:Label>
        </span>
        <div class="questionBreak">
            <asp:LinkButton ID="DeleteButton" runat="server">Delete</asp:LinkButton>|
            <asp:HyperLink ID="InsertHyperLink" runat="server">Insert question</asp:HyperLink>|
            <asp:LinkButton ID="InsertLineBreakButton" runat="server">Insert line break</asp:LinkButton>|
            <asp:LinkButton ID="EnableRandomHyperlink" runat="server">Enable random</asp:LinkButton>|
            <asp:HyperLink ID="BranchingHyperLink" runat="server">Edit branching</asp:HyperLink>|
            <asp:LinkButton ID="EnableSubmitHyperlink" runat="server">Enable submit</asp:LinkButton>
        </div>

</div>

            <uc1:PageBranchingRulesControl ID="PageBranchingRules" runat="server"></uc1:PageBranchingRulesControl>

