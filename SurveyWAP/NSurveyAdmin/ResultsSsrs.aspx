<%@ Page Title="SSRS Reports" Language="C#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master" AutoEventWireup="true" CodeBehind="ResultsSsrs.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.ResultsSsrs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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

    <asp:DataGrid 
        ID="SsrsDataGrid" 
        runat="server" 
        AllowCustomPaging="True" 
        GridLines="Vertical"
        ForeColor="Black" 
        Width="100%" 
        AlternatingRowStyle-BackColor="#FFF6BB" 
        ShowFooter="True" 
        AllowPaging="False" 
        PageSize="25" 
        FooterStyle-BackColor="#FFDF12" 
        FooterStyle-BorderStyle="None" 
        FooterStyle-BorderColor="#E2E2E2">
        <SelectedItemStyle Font-Bold="True" Wrap="True" ForeColor="White" BackColor="#000099"></SelectedItemStyle>
        <EditItemStyle Wrap="False"></EditItemStyle>
        <AlternatingItemStyle Wrap="True" ForeColor="Black" BackColor="#F4F9FA"></AlternatingItemStyle>
        <ItemStyle Wrap="True" BackColor="white" ForeColor="Black"></ItemStyle>
        <HeaderStyle BackColor="#e2e2e2" ForeColor="#5720C6" HorizontalAlign="Center" Wrap="true" Width="25%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E2E2E2"></HeaderStyle>
        <FooterStyle Wrap="True" BackColor="#CCCCCC"></FooterStyle>
        <Columns>
            <asp:ButtonColumn ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center" ButtonType="LinkButton" Text="<img src='../Images/view.png'>" CommandName="Select" />	
                    <asp:TemplateColumn ItemStyle-Font-Size="0.9em" HeaderText="Description">
                        <ItemTemplate>
                            <asp:Literal ID="litReportDescription" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
        </Columns>

    </asp:DataGrid>

                                                                              </div>
                                         </li>
  </ol>
                    <br />
                    </fieldset>
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div>

</asp:Content>
