<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.SecurityAddInOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="SecurityAddInOptionsControl.ascx.cs" %>

        <li style="text-align:justify;">
            <asp:ImageButton ID="UpImageButton" runat="server" Height="12" Width="12" ImageUrl="~/Images/questionupbutton.gif">
            </asp:ImageButton>

            <asp:ImageButton ID="DownImageButton" runat="server" Height="12" Width="12" ImageUrl="~/Images/questiondownbutton.gif">
            </asp:ImageButton>

             <asp:Label ID="AddInDescriptionLabel" runat="server"></asp:Label>

            <asp:LinkButton ID="DisableAddInLinkButton" runat="server">Disable</asp:LinkButton>&nbsp;|&nbsp;
            <asp:LinkButton ID="DeleteButton" runat="server">Delete</asp:LinkButton>

            <div style="float:right;">
                <asp:HyperLink ID="InsertAddInHyperLink" runat="server">Insert security add in</asp:HyperLink>&nbsp;&nbsp;

            </div>
            <br />
                <asp:Label ID="AddInDisabledLabel" runat="server" ForeColor="Red" Font-Size="Small"></asp:Label>
        </li>
