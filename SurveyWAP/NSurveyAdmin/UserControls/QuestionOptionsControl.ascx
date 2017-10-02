<%@ Register TagPrefix="uc1" TagName="SkipLogicRulesControl" Src="SkipLogicRulesControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.QuestionOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="QuestionOptionsControl.ascx.cs" %>
<table style="position: relative;" width="100%" class="questionOptions">
    <tr>
        <td>
            <asp:ImageButton ID="UpImageButton" ImageUrl="~/Images/questionupbutton.gif"
                runat="server"></asp:ImageButton>
            <asp:ImageButton ID="DownImageButton" ImageUrl="~/Images/questiondownbutton.gif"
                runat="server"></asp:ImageButton>
            <asp:HyperLink ID="EditHyperLink" runat="server">Edit</asp:HyperLink>|&nbsp;
            <asp:HyperLink ID="EditAnswersHyperLink" runat="server">Edit answers</asp:HyperLink>|&nbsp;
            <asp:LinkButton ID="DeleteButton" runat="server">Delete</asp:LinkButton>|&nbsp;
            <asp:LinkButton ID="CloneButton" runat="server">Clone</asp:LinkButton>|&nbsp;
            <asp:HyperLink ID="InsertHyperLink" runat="server">Insert question</asp:HyperLink>|&nbsp;
            <asp:LinkButton ID="InsertPageBreakButton" runat="server">Insert page break</asp:LinkButton>|&nbsp;
            <asp:LinkButton ID="InsertLineBreakButton" runat="server">Insert line break</asp:LinkButton>|&nbsp;
            <asp:HyperLink ID="EditSkipLogicHyperLink" runat="server">Skip logic</asp:HyperLink>
        </td>
    </tr>
    <tr class="IdLabel">
        <td>
            ID :
            <asp:Label ID="lblId" ForeColor="Black" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblHelpLabel"  runat="server" Text="Help Text :"> </asp:Label> 
            <asp:Label ID="lblHelpText" CssClass="DesignHelpText" runat="server"></asp:Label>
        </td>
    </tr>

</table>

            <uc1:SkipLogicRulesControl ID="SkipLogicRules" runat="server"></uc1:SkipLogicRulesControl>
