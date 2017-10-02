<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserImport.ascx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.UserImport" %>
<%@ Register TagPrefix="uc1" TagName="UsersOptionsControl" Src="UsersOptionsControl.ascx" %>


            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/User Import.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
            <fieldset>
                <legend class="titleFont titleLegend">
                        <asp:Label ID="ImportUsersTitle" runat="server">Import users</asp:Label>
                    </legend>
                <br />
<ol>
     <li>                 
                                    
                                        <asp:Label ID="ImportUserLabel" AssociatedControlID="ImportUsersTextBox" runat="server" EnableViewState="False"></asp:Label>
         <br /> <br />
                                        <asp:TextBox ID="ImportUsersTextBox" runat="server" TextMode="MultiLine" Rows="5"
                                            Columns="85" Width="100%" Wrap="False"></asp:TextBox>
</li><li>
                                        <asp:Label ID="ImportInfo1Label" runat="server" EnableViewState="False"></asp:Label>
                                        
                                        <asp:Label ID="ImportInfo2Label" runat="server" EnableViewState="False"></asp:Label>
                                        
                                        <asp:Label ID="ImportInfo3Label" runat="server" EnableViewState="False"></asp:Label>
      
 
    </li><li>
                               <asp:Label ID="UserIsAdministratorLabel" AssociatedControlID="IsAdminCheckBox" runat="server" EnableViewState="False" ></asp:Label>
                                    <asp:CheckBox ID="IsAdminCheckBox" runat="server"></asp:CheckBox>
        </li><li>
                                        <asp:Label ID="AssignAllSurveysLabel" AssociatedControlID="HasSurveyAccessCheckBox" runat="server" EnableViewState="False" Text="Assign all survey :"></asp:Label>
                                    <asp:CheckBox ID="HasSurveyAccessCheckBox" runat="server" AutoPostBack="True"></asp:CheckBox>
       </li><li>
                            <asp:Label ID="UserSurveyAssignedLabel" runat="server" AssociatedControlID="SurveysListBox" Text="Assigned surveys :" EnableViewState="False"></asp:Label>
<br />
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:Literal ID="UnAssignedSurveysLabel" runat="server" Text="Available surveys" EnableViewState="False"></asp:Literal>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Literal ID="AssignedSurveysLabel" runat="server" Text="Assigned to user" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  style="width:45%;">
                                                <asp:ListBox ID="SurveysListBox" runat="server" AutoPostBack="True" Width="100%" Rows="6">
                                                </asp:ListBox>
                                            </td>
                                            <td class="smallText" style="width:10%;">
                                                &nbsp;<strong>&gt;&gt;<br />
                                                    &lt;&lt;</strong>&nbsp;
                                            </td>
                                            <td style="width:45%;">
                                                <asp:ListBox ID="UserSurveysListBox" runat="server" AutoPostBack="True" Width="100%" Rows="6">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>

              </li><li>
                                        <asp:Label ID="RolesLabel" AssociatedControlID="RolesListBox" runat="server" Text="Roles :" EnableViewState="False"></asp:Label>

                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:Literal ID="AvailableRolesLabel" runat="server" Text="Available roles" EnableViewState="False"></asp:Literal>
                                            </td>
                                            <td >
                                            </td>
                                            <td>
                                                <asp:Literal ID="UserRolesLabel" runat="server" Text="User's roles" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:45%;">
                                                <asp:ListBox ID="RolesListBox" runat="server" AutoPostBack="True" Width="100%" Rows="6">
                                                </asp:ListBox>
                                            </td>
                                            <td class="smallText" style="width:10%;">
                                                &nbsp;<strong>&gt;&gt;<br />
                                                    &lt;&lt;</strong>&nbsp;
                                            </td>
                                            <td style="width:45%;">
                                                <asp:ListBox ID="UserRolesListBox" runat="server" AutoPostBack="True" Width="100%"  Rows="6">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
           </li><li>

            <asp:Button ID="ImportUsersButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Import users" OnClick="ImportUsersButton_Click1">
            </asp:Button>
               <br /><br />
    </li>
  </ol>

                </fieldset>