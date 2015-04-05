<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.RolesOptionsControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="RolesOptionsControl.ascx.cs" %>


            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
<br />
            <fieldset style="width:745px; margin-left:0px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                <asp:Label ID="RolesOptionTitleLabel" runat="server">Label</asp:Label>
                    </legend>
                <br />
 <ol>
     <li>               


                            <asp:Label ID="RoleNameLabel" runat="server" AssociatedControlID="RoleNameTextBox" Text="Role name:" EnableViewState="False"></asp:Label>

                        <asp:TextBox ID="RoleNameTextBox" runat="server"></asp:TextBox>
</li><li>

    <asp:Label ID="RoleRightsLabel" AssociatedControlID="RightsCheckBoxList" runat="server" Text="Role's rights :" EnableViewState="False"></asp:Label>
    <br /><br />
     <table class="normal" style="width:100%;">
                <tr>
                    <td>
                        <asp:CheckBoxList ID="RightsCheckBoxList" runat="server" RepeatColumns="3"  CssClass="smallText chkPrivs"
                            Height="22px" Width="100%">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>


</li><li>

<asp:Button ID="CreateNewRoleButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create role" OnClick="CreateNewRoleButton_Click1"></asp:Button>
<asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply changes"></asp:Button>
<asp:Button ID="DeleteRoleButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete role"></asp:Button>
    <br />

                                                   </li>
  </ol>
</fieldset>