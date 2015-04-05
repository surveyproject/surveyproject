<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RolesManager.ascx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.RolesManager" %>
<%@ Register TagPrefix="uc1" TagName="RolesOptionsControl" Src="RolesOptionsControl.ascx" %>

<br />
                                        <div style="position: absolute; width:40px; right: 22px; top: 52px;">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/UM_Introduction.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
            <fieldset style="width:745px; margin-left:0px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                            <asp:Literal ID="RolesManagerTitle" runat="server" EnableViewState="False" Text="Roles manager"></asp:Literal>
                    </legend>

<br />


                      <ol>
     <li>                            
                                        <asp:Label ID="RolesToEditLabel" runat="server" AssociatedControlID="RolesDropDownList" EnableViewState="False" Text="Select a role to edit / view :"></asp:Label>
                                        
                                        <asp:DropDownList ID="RolesDropDownList" Width="180" runat="server" AutoPostBack="True" OnSelectedIndexChanged="User_IndexChanged">
                                        </asp:DropDownList>

                                            <asp:Button ID="CreateRoleHyperLink" CssClass="btn btn-primary btn-xs bw" runat="server" OnClick="OnNewRole"></asp:Button>

                                   </li>
  </ol>
</fieldset>

            <uc1:RolesOptionsControl ID="RolesOptions" runat="server"></uc1:RolesOptionsControl>
