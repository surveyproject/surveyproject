
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="Votations.NSurvey.WebAdmin" %>
<%@ Page language="c#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"  AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.MailingLog" Codebehind="MailingLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">


            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

             <br />
                <fieldset style="width:750px; margin-top:15px; margin-left:12px;">
                    <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">

<asp:literal id="MailingLogTitle"
          runat="server"
          Text="Mailing log"
          EnableViewState="False"></asp:literal>
    
    <asp:label id="CurrentPendingPageLabel" runat="server">1</asp:label>&nbsp;/
          <asp:label id="TotalPendingPagesLabel" runat="server">1</asp:label>
                    </legend><br />
<ol><li>
     <div class="rounded_corners">
    <asp:datagrid id="MailingLogDataGrid" runat="server" Width="100%" 
                        AutoGenerateColumns="False" AllowCustomPaging="True" PageSize="1">
                <AlternatingItemStyle BackColor="#FFF6BB">
                </AlternatingItemStyle>
                
                <ItemStyle Font-Size="Small" BackColor="White">
                </ItemStyle>
                
                <HeaderStyle Font-Size="Small" Font-Bold="True" BackColor="#e2e2e2" BorderColor="#e2e2e2" ForeColor="#5720C6" >
                </HeaderStyle>
                
                <Columns>
                <mbrsc:RowSelectorColumn AllowSelectAll="True"></mbrsc:RowSelectorColumn>
                <asp:BoundColumn DataField="Email" ItemStyle-Width="18%" HeaderText="Email"></asp:BoundColumn>
                <asp:BoundColumn DataField="ExceptionType" ItemStyle-Width="15%" ItemStyle-Wrap="true" HeaderText="Type"></asp:BoundColumn>
                <asp:BoundColumn DataField="ExceptionMessage" ItemStyle-Width="55%" HeaderText="Message"></asp:BoundColumn>
                <asp:BoundColumn DataField="ErrorDate" ItemStyle-Width="12%" HeaderText="Error Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                </Columns>
                </asp:datagrid>
         </div>

</li><li>
             <asp:linkbutton id="PreviousLogPageLinkButton" runat="server">&lt;&lt; Previous page</asp:linkbutton> &nbsp;|&nbsp; 
                <asp:linkbutton id="NextLogPageLinkButton" runat="server">Next page >></asp:linkbutton>
</li><li>
                <asp:button id="DeleteLogsButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete selected entries"></asp:button> 
    <br />
       </li>

</ol>         
                    <br />
                    </fieldset>
</div></div></asp:Content>
