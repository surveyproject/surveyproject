<%@ Page Language="C#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master" AutoEventWireup="true" CodeBehind="EditCssXml.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.EditCssXml" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">


        <div id="Panel" class="Panel">

                        <div class="helpDiv">
                <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/CssXml.aspx"
                    title="Click for more Information">
                    <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                </a>
            </div>

                   <fieldset>
        <legend class="titleFont titleLegend">
          <asp:Literal ID="CssXmlLegend" runat="server" EnableViewState="false">Surveylayout CSS XML</asp:Literal>  
        </legend><br />

 <ol>
     <li>
            <asp:GridView ID="CssXmlGridView" Width="100%" PagerSettings-Mode="Numeric"
                ShowFooter="true"
                AutoGenerateColumns="false"  
                runat="server" 
                AllowPaging="true" 
                PageSize="15"             
                 CellSpacing="25"   
                
                OnRowCancelingEdit ="CssXmlGridView_RowCancelingEdit"
                OnRowDeleting ="CssXmlGridView_RowDeleting"
                OnRowEditing="CssXmlGridView_RowEditing"
                OnRowUpdating="CssXmlGridView_RowUpdating"

                OnPageIndexChanging="OnPageIndexChanging"  
                CellPadding="4" 
                ForeColor="#333333" 
                GridLines="None">

                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="CSS Selectors" HeaderStyle-Width="50%" HeaderStyle-Height="20">

                        <EditItemTemplate>
                            <asp:TextBox ID="txtValue"  Width="100%" runat="server" Text='<%#DataBinder.Eval(
                           Container. DataItem,"value") %>'></asp:TextBox>
                        </EditItemTemplate>

                        <ItemTemplate>
                            <asp:Label ID="lblValue" CssClass="cssXmlAlign" runat="server" Text='<%#DataBinder.Eval(
                                 Container. DataItem,"value") %>'></asp:Label>
                        </ItemTemplate>
 
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ObjectID" HeaderStyle-Width="10%">
                        
                        <ItemTemplate>
                            <asp:Label ID="lblId" CssClass="cssXmlAlign"  runat="server" Text='<%#DataBinder.Eval(
                                 Container. DataItem,"id") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name" HeaderStyle-Width="30%">

                        <ItemTemplate>
                            <asp:Label ID="lblName" CssClass="cssXmlAlign" runat="server" Text='<%#DataBinder.Eval(
                                 Container. DataItem,"name") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%">
                        <EditItemTemplate>
                            <asp:ImageButton ID="lbtnUpdate" ImageUrl="~/Images/save.gif" ToolTip="Save" Text="Update" CommandName="Update" runat="server">
                                              </asp:ImageButton>
                            <asp:ImageButton ID="lbtnCancel" ImageUrl="~/Images/cancel2.gif" ToolTip="Cancel" Text="Cancel" CommandName="Cancel" runat="server">
                                              </asp:ImageButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="lbtnEdit" ImageUrl="~/Images/edit_pen.gif" ToolTip="Edit" Text="Edit" runat="server" CommandName="Edit">
                                               </asp:ImageButton>
                       <!--     <asp:ImageButton ID="lbtnDelete" ImageUrl="~/Images/delete.gif" ToolTip="Delete" Text="Delete" runat="server" CommandName="Delete">
                                                </asp:ImageButton> -->
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

                <EditRowStyle BackColor="yellow"></EditRowStyle>

                <FooterStyle BackColor="#EFF3FB" Font-Bold="True" ForeColor="White"></FooterStyle>

                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

                <PagerStyle HorizontalAlign="Center" CssClass="cssxmlpaging" BackColor="#2461BF" Font-Size="1.3em" ForeColor="White"></PagerStyle>

                <RowStyle BackColor="#EFF3FB"></RowStyle>

                <SelectedRowStyle BackColor="#D1DDF1"  Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
            </asp:GridView>
</li></ol>
            <br /><br />
                                   </fieldset>
            </div>
    </asp:content>