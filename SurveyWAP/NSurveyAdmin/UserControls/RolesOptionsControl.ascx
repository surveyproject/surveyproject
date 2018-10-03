<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.RolesOptionsControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="RolesOptionsControl.ascx.cs" %>


            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
<br />
            <fieldset>
                <legend class="titleFont titleLegend">
                    <asp:Label ID="RolesOptionTitleLabel" runat="server">Label</asp:Label>
                </legend>
                <br />
                <ol>
                    <li>
                        <asp:Label ID="RoleNameLabel" runat="server" AssociatedControlID="RoleNameTextBox" Text="Role name:" EnableViewState="False"></asp:Label>

                        <asp:TextBox ID="RoleNameTextBox" runat="server"></asp:TextBox>
                    </li>
                    <li>

                        <asp:Label ID="RoleRightsLabel" AssociatedControlID="RightsCheckBoxList" runat="server" Text="Role's rights :" EnableViewState="False"></asp:Label>
                        <br />
                        <br />
                        <table class="normal" style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="RightsCheckBoxList" runat="server" RepeatColumns="1" CssClass="smallText chkPrivs"
                                        Height="25px" Width="100%">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </li>
                    <li>
                        <asp:Button ID="CreateNewRoleButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create role" OnClick="CreateNewRoleButton_Click1"></asp:Button>
                        <asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply changes"></asp:Button>
                        <asp:Button ID="DeleteRoleButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete role"></asp:Button>
                        <br />

                    </li>
                </ol>
</fieldset>