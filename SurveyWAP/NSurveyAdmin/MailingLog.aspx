<%@ Page language="c#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"  AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.MailingLog" Codebehind="MailingLog.aspx.cs" %>
<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="SurveyProject.WebApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">


             <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Mailing Log.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
                <fieldset>
                    <legend class="titleFont titleLegend">

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
                        AutoGenerateColumns="False" AllowCustomPaging="True">
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
                                   <div style="left: 30px; position:absolute; ">
                          <asp:Button ID="PreviousLogPageLinkButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Previous page" /> &nbsp;&nbsp;
                          <asp:Button ID="NextLogPageLinkButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Next Page"></asp:Button>
                                </div>
                             <div style="right: 10px;">
                <asp:button id="DeleteLogsButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete selected entries"></asp:button> 
                                 </div>
    <br />
       </li>

</ol>         
                    <br />
                    </fieldset>
            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>
