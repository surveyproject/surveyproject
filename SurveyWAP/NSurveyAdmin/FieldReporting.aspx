<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.FieldReporting" Codebehind="FieldReporting.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">
        
                                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Voter report.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
            <fieldset>
                 <legend class="titleFont titleLegend">
                    <asp:Literal ID="FieldReportTitle" runat="server" EnableViewState="False">Field entries report - Page</asp:Literal>
                    <asp:Label ID="CurrentPageLabel" runat="server">1</asp:Label>&nbsp;/
                                            <asp:Label ID="TotalPagesLabel" runat="server">1</asp:Label>
                </legend>
            

                </fieldset>


                     <fieldset>
                         <br />
                            <ol>
     <li>
                                   <div class="rounded_corners" style="width:680px; overflow:hidden;">
                                            <asp:DataGrid ID="FieldReportDataGrid" runat="server" AllowCustomPaging="True" GridLines="Vertical"
                                                ForeColor="Black" Width="100%"  AlternatingRowStyle-BackColor="#FFF6BB" ShowFooter="True" AllowPaging="False" PageSize="15" FooterStyle-BackColor="#FFDF12" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="#E2E2E2">
                                                <SelectedItemStyle Font-Bold="True" Wrap="True" ForeColor="White" BackColor="#000099">
                                                </SelectedItemStyle>
                                                <EditItemStyle Wrap="False"></EditItemStyle>
                                                <AlternatingItemStyle Wrap="True" ForeColor="Black" BackColor="#F4F9FA"></AlternatingItemStyle>
                                                <ItemStyle Wrap="True" BackColor="white" ForeColor="Black" Font-Size="xx-small">
                                                </ItemStyle>
                                                <HeaderStyle BackColor="#e2e2e2" ForeColor="#5720C6"  HorizontalAlign="Center" Wrap="true" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E2E2E2"></HeaderStyle>
                                                <FooterStyle Wrap="True" BackColor="#CCCCCC"></FooterStyle>
                                                <Columns>                                                    
                                                    <asp:ButtonColumn ItemStyle-Width="55" ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" Text="<img src='../Images/view.png'>" CommandName="Select" />
                                                    <asp:ButtonColumn ItemStyle-Width="55" ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" Text="<img src='../Images/delete.gif'>" CommandName="Delete"></asp:ButtonColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Wrap="False">
                                                </PagerStyle>
                                            </asp:DataGrid>
                                       </div>
                                         </li><li>
                                           
                                             <asp:Button CssClass="btn btn-primary btn-xs" ID="PreviousPageButton" runat="server" />&nbsp; &nbsp;
                                             <asp:Button CssClass="btn btn-primary btn-xs" ID="NextPageButton" runat="server" />


                                             <br />
                                   </li>
  </ol>
                    <br />
                    </fieldset>
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div>


</asp:Content>
