<%@ Page Language="c#" MasterPageFile="~/Wap.master" EnableEventValidation="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UsersManager"
    CodeBehind="UsersManager.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="UsersOptionsControl" Src="UserControls/UsersOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RolesManager" Src="UserControls/RolesManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserImport" Src="UserControls/UserImport.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--    <script type="text/javascript">
        var obj = { 
            beforeActivate: <%= selectedTabIndex %> , active: function(event, ui) {
                $("#tabindex").val(ui.newTab); 
                __doPostBack(); 
            } 
        };
        $(function () { $("#tabs").tabs(obj); });
    </script>--%>

        
        <script type="text/javascript">
            $(function () {
                var obj = {
                    beforeActivate: function(event, ui) { 
                        $("#tabindex").val(ui.newTab.index()); 
                        __doPostBack();
                    },

                    active: <%= selectedTabIndex %>
                    };

                $("#tabs").tabs(obj);
            });
    </script>


    <input type="hidden" id="tabindex" name="tabindex" value="<%= selectedTabIndex %>" />
    <div id="usersTabEvents" style="display: none" runat="server" onclick="foo" />

    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">
                <%=GetPageResource("UsersTab")%></a></li>
            <li><a href="#tabs-2">
                <%=GetPageResource("RolesTab")%></a></li>
            <li><a href="#tabs-3">
                <%=GetPageResource("ImportUsersTab")%></a></li>
        </ul>


        <div id="Panel" class="Panel">
             <fieldset id="liML" runat="server">    
                <asp:ImageButton ID="btnBack" Width="16px" ImageUrl="~/Images/back_button.gif" runat="server" CssClass="buttonBack" OnCommand="EditBackButton" Visible="false" ToolTip="Go back to previous" />
            </fieldset>

        <div id="tabs-1">
            <uc1:UsersOptionsControl ID="UsersOptionsControl1" runat="server"></uc1:UsersOptionsControl>

            <asp:PlaceHolder runat="server" ID="phUsersList" Visible="true">

            <fieldset>
                <legend class="titleFont titleLegend">
                            <asp:Label ID="UserListTitleLabel" runat="server">UserListLabel</asp:Label>
                    </legend>

                <table style="width:100%; margin: 5px;">
                    <tr>
                        <td colspan="6" style="text-align:left;">
                            <br /><asp:Literal ID="UserlistFilterOptionLiteral" runat="server" EnableViewState="false">Userlist Filter Options</asp:Literal>    <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            <asp:TextBox runat="server"   ID="txtUserName" Width="110px" ToolTip="UserName" />
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox runat="server" ID="txtFirstName" Width="110px" ToolTip="FirstName" />
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox runat="server" ID="txtLastName" Width="110px" ToolTip="LastName" />
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox runat="server" ID="txtEmail" Width="160px" ToolTip="Email" />
                        </td>
                        <td style="text-align:center;">
                            <asp:CheckBox runat="server" ID="chkAdmin" Width="45px" ToolTip="CheckAdmin" />
                        </td>
                        <td style="text-align:center;">
                            <asp:Button runat="server" CssClass="btn btn-primary btn-xs bw" ID="btnApplyfilter" Width="85px" OnCommand="OnApplyFilter" />
                        </td>
                    </tr>
                </table><br />

                   <div class="rounded_corners">
                <asp:GridView runat="server" Width="100%" ID="gvUsers" AutoGenerateColumns="False" AllowPaging="true"
                    OnPageIndexChanged="gvUsers_PageIndexChanged" OnPageIndexChanging="gvUsers_PageIndexChanging"
                    PageSize="20" 
                    AlternatingRowStyle-BackColor="#FFF6BB" 
                    ShowFooter="True" 
                    FooterStyle-BackColor="#FFDF12" 
                    FooterStyle-BorderStyle="None" 
                    FooterStyle-BorderColor="#E2E2E2"
                     HeaderStyle-HorizontalAlign="Center"
                     RowStyle-HorizontalAlign="Center"
                     >
                    
                    <PagerSettings Visible="true" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10"
                        NextPageText=">" PreviousPageText="<" />
                    <PagerStyle ForeColor="Black" Font-Size="X-Small"  HorizontalAlign="Center" BackColor="#C6C3C6" VerticalAlign="Bottom"
                        Width="200px" Height="5px"></PagerStyle>

                    <Columns>
                        <asp:TemplateField ItemStyle-Width="17%" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="110px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lbl1" Text='<%#GetPageResource("UsersTabUserName") %>' /></HeaderTemplate>
                            <ItemTemplate>
                         <!--       <asp:Label runat="server" Text='<%#(Eval("UserName")) %>' /> -->
                             <asp:LinkButton Text='<%#(Eval("UserName")) %>' runat="server" ToolTip="Edit User" OnCommand="OnUserEdit"
                                    CommandName="UserEdit" CommandArgument='<%#(Eval("UserId")) %>' CssClass="hyperlink" />
                                
                                </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="17%" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="110px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='<%#GetPageResource("UsersTabFirstName") %>' /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#(Eval("FirstName")) %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="17%"  ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="110px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='<%#GetPageResource("UsersTabLastName") %>' /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#(Eval("LastName")) %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="25%"  ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="110px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  >
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='<%#GetPageResource("UsersTabEmail") %>' /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#(Eval("Email")) %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="7%"  ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="110px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  >
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='<%#GetPageResource("UsersTabAdministrator") %>' /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%#IsAdmin((int)Eval("UserId")) %>' Enabled="false" /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="7%"  ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="110px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  >
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='<%#GetPageResource("UsersTabEdit") %>' /></HeaderTemplate>
                            <ItemTemplate>
                            <asp:ImageButton runat="server" ToolTip="Click to Edit" ImageUrl="~/Images/edit_pen.gif" Width="16" Height="16" OnCommand="OnUserEdit"
                                    CommandName="UserEdit" CommandArgument='<%#(Eval("UserId")) %>' />
                           </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="7%" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="110px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  >
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='<%#GetPageResource("UsersTabDelete") %>' /></HeaderTemplate>
                            <ItemTemplate>
                            <asp:ImageButton runat="server" ToolTip="Click to Delete" ImageUrl="~/Images/delete.gif" Width="16" Height="16" OnCommand="OnUserDelete"
                                    CommandName="UserDelete" CommandArgument='<%#(Eval("UserId")) %>' />
                           </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                       </div>
</fieldset>

            </asp:PlaceHolder>
        </div>

        <div id="tabs-2">
            <uc1:RolesManager ID="rolesManager" runat="server"></uc1:RolesManager>
        </div>

        <div id="tabs-3">
            <uc1:UserImport ID="userImport" runat="server"></uc1:UserImport>
        </div>

            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
            </div>
    </div>
          
</asp:Content>
