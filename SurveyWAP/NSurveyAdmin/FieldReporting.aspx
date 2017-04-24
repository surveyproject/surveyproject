<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.FieldReporting" Codebehind="FieldReporting.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">


            <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px; overflow: hidden;" title="">
                 <legend class="titleFont titleLegend">
                    <asp:Literal ID="FieldReportTitle" runat="server" EnableViewState="False">Field entries report - Page</asp:Literal>
                    <asp:Label ID="CurrentPageLabel" runat="server">1</asp:Label>&nbsp;/
                                            <asp:Label ID="TotalPagesLabel" runat="server">1</asp:Label>
                </legend>
                <br />
                <ol>
                    <li>

                        <asp:RadioButtonList runat="server" ID="rblReports" RepeatDirection="Vertical"
                            CellPadding="10" OnSelectedIndexChanged="rbListSelectedIndexChanged" AutoPostBack="true"
                            Width="700px">
                            <asp:ListItem Text="GraphicalReports" Value="GR"></asp:ListItem>
                            <asp:ListItem Text="VoterReports" Value="TR" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="CrossTabulationReports" Value="CTR"></asp:ListItem>
                        </asp:RadioButtonList>


                    </li>
                </ol>
                <br />
                </fieldset>

                     <fieldset style="width:750px; margin-left:12px; margin-top:15px;" title="">
                           <br /> <ol>
     <li>
                                   <div class="rounded_corners" style="width:680px; overflow:hidden;">
                                            <asp:DataGrid ID="FieldReportDataGrid" runat="server" AllowCustomPaging="True" GridLines="Vertical"
                                                ForeColor="Black" Width="100%"  AlternatingRowStyle-BackColor="#FFF6BB" ShowFooter="True" AllowPaging="False" PageSize="10" FooterStyle-BackColor="#FFDF12" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="#E2E2E2">
                                                <SelectedItemStyle Font-Bold="True" Wrap="True" ForeColor="White" BackColor="#000099">
                                                </SelectedItemStyle>
                                                <EditItemStyle Wrap="False"></EditItemStyle>
                                                <AlternatingItemStyle Wrap="True" ForeColor="Black" BackColor="#F4F9FA"></AlternatingItemStyle>
                                                <ItemStyle Wrap="True" BackColor="white" ForeColor="Black" Font-Size="xx-small">
                                                </ItemStyle>
                                                <HeaderStyle BackColor="#e2e2e2" ForeColor="#5720C6"  HorizontalAlign="Center" Wrap="true" Width="25%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E2E2E2"></HeaderStyle>
                                                <FooterStyle Wrap="True" BackColor="#CCCCCC"></FooterStyle>
                                                <Columns>                                                    
                                                    <asp:ButtonColumn ItemStyle-Width="95" ButtonType="LinkButton" Text="<img src='../Images/view.png'>" CommandName="Select" />
                                                    <asp:ButtonColumn ItemStyle-Width="95" ButtonType="LinkButton" Text="<img src='../Images/delete.gif'>" CommandName="Delete"></asp:ButtonColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Wrap="False">
                                                </PagerStyle>
                                            </asp:DataGrid>
                                       </div>
                                         </li><li>

                                            <asp:LinkButton CssClass="btn btn-primary btn-xs bw" ID="NextPageLinkButton" runat="server">Next page >></asp:LinkButton>&nbsp;
                                           <asp:LinkButton CssClass="btn btn-primary btn-xs bw" ID="PreviousPageLinkButton" runat="server"><< Previous page</asp:LinkButton>&nbsp;
                                             <br />
                                   </li>
  </ol>
                    <br />
                    </fieldset>
</div>

    </div>

</asp:Content>
