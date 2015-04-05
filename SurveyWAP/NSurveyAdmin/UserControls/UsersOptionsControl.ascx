<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.UsersOptionsControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="UsersOptionsControl.ascx.cs" %>

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
<br />
            <fieldset style="width:750px; margin-left:-5px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                <asp:Label ID="UserOptionTitleLabel" runat="server"></asp:Label>
                    </legend>
                                <br />
                <ol>
     
                <asp:PlaceHolder ID="NSurveyUserPlaceHolder" runat="server">
<li>
                     <asp:Label ID="UserNameLabel" AssociatedControlID="UserNameTextBox" runat="server" EnableViewState="False" Text="User name:"></asp:Label>
                            <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>

                      </li><li>
                                <asp:Label ID="UserPasswordLabel" AssociatedControlID="PasswordTextBox" runat="server" EnableViewState="False" Text="Password :"></asp:Label>
                            <asp:TextBox ID="PasswordTextBox" runat="server" MaxLength="12" TextMode="Password"></asp:TextBox>
</li><li>
                                <asp:Label ID="UserFirstNameLabel" AssociatedControlID="FirstNameTextBox" runat="server" EnableViewState="False" Text="First name :"></asp:Label>
                            <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
  </li><li>
                                <asp:Label ID="UserLastNameLabel" AssociatedControlID="LastNameTextBox" runat="server" EnableViewState="False" Text="Last name :"></asp:Label>
                            <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
  </li><li>
                                <asp:Label ID="UserEmailLabel" AssociatedControlID="EmailTextBox" runat="server" EnableViewState="False" Text="Email :"></asp:Label>
                            <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
 </li>
                </asp:PlaceHolder>

  <li>
                             <asp:Label ID="UserIsAdministratorLabel" AssociatedControlID="IsAdminCheckBox" runat="server" EnableViewState="False" Text="Administrator :"></asp:Label>
                        <asp:CheckBox ID="IsAdminCheckBox" runat="server"></asp:CheckBox>

     </li><li>                         <asp:Label ID="AssignAllSurveysLabel" AssociatedControlID="HasSurveyAccessCheckBox" runat="server" EnableViewState="False" Text="Assign all survey :"></asp:Label>

                        <asp:CheckBox ID="HasSurveyAccessCheckBox" runat="server" AutoPostBack="True"></asp:CheckBox>
                
     </li>
  </ol>          <asp:Button ID="CreateNewUserButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create user"></asp:Button>

                        <asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply changes"></asp:Button>
                        <asp:Button ID="DeleteUserButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete user" Visible="False" OnClick="DeleteUserButton_Click1"></asp:Button>

          
                    <br /><br /> 
                    </fieldset>

<br /><br />

                <asp:PlaceHolder ID="ExtendedSettingsPlaceHolder" runat="server">

 <fieldset style="width:750px; margin-left:-5px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                                <asp:Literal ID="UserSurveyAssignedLabel" runat="server" Text="Assigned surveys :" EnableViewState="False"></asp:Literal>
                    </legend>
     <br />
<ol>
     <li>
                            <asp:PlaceHolder ID="phSurveySelect" runat="server">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Literal ID="UnAssignedSurveysLabel" runat="server" Text="Available surveys"
                                                EnableViewState="False"></asp:Literal>
                                        </td>
                                        <td style="width:10%">
                                        </td>
                                        <td>
                                            <asp:Literal ID="AssignedSurveysLabel" runat="server" Text="Assigned to user" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 45%">
                                            <asp:ListBox ID="SurveysListBox" runat="server" AutoPostBack="True" Rows="6"
                                                Width="100%"></asp:ListBox>
                                        </td>
                                        <td style="width:10%">
                                            <div style="margin: 10px;">
                                                &gt;&gt;<br />
                                                &lt;&lt;</div>
                                        </td>
                                        <td style="width: 45%">
                                            <asp:ListBox ID="UserSurveysListBox" runat="server" AutoPostBack="True" Rows="6"
                                                Width="100%"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:PlaceHolder>

</li><li> 
                                <asp:Literal ID="RolesLabel" runat="server" Text="Roles :" EnableViewState="False"></asp:Literal>

                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Literal ID="AvailableRolesLabel" runat="server" Text="Available roles" EnableViewState="False"></asp:Literal>
                                    </td>
                                    <td style="width: 10%">
                                    </td>
                                    <td>
                                        <asp:Literal ID="UserRolesLabel" runat="server" Text="User's roles" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:ListBox ID="RolesListBox" runat="server" AutoPostBack="True" Rows="6"
                                            Width="100%"></asp:ListBox>
                                    </td>
                                    <td style="width:10%">
                                        <div style="margin: 10px;">
                                            &gt;&gt;<br />
                                            &lt;&lt;</div>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:ListBox ID="UserRolesListBox" runat="server" AutoPostBack="True" Rows="6"
                                            Width="100%"></asp:ListBox>
                                    </td>
                                </tr>
                            </table>

         </li>
  </ol>
                    <br />
                    </fieldset>

                </asp:PlaceHolder>

