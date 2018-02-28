<%@ Page language="c#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"  AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.MailingStatus" Codebehind="MailingStatus.aspx.cs" %>
<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="SurveyProject.WebApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

          <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Mailing Status.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
                <fieldset>
                    <legend class="titleFont titleLegend">
<asp:Literal id="PendingEmailsTitle" runat="server" EnableViewState="False">Pending emails - Page</asp:Literal>

          <asp:label id="CurrentPendingPageLabel" runat="server"></asp:label>&nbsp;/
          <asp:label id="TotalPendingPagesLabel" runat="server"></asp:label>
                        </legend><br />
<ol>
           
          <asp:placeholder id="pendinglist" runat="server">
       <li>    <div class="rounded_corners">
          <asp:datagrid id="PendingEmailsDataGrid" runat="server" PageSize="10" AllowPaging="True" AutoGenerateColumns="False" Width="100%"  >
          <AlternatingItemStyle BackColor="#FFF6BB">
          </AlternatingItemStyle>
          
          <ItemStyle Font-Size="Small" BackColor="White">
          </ItemStyle>
          
          <HeaderStyle BackColor="#e2e2e2" BorderColor="#e2e2e2" ForeColor="#5720C6"   Font-Size="Small" Font-Bold="True">
          </HeaderStyle>
          
          <Columns>
          <mbrsc:RowSelectorColumn AllowSelectAll="True"></mbrsc:RowSelectorColumn>
          <asp:BoundColumn DataField="Email" HeaderText="Email"></asp:BoundColumn>
          <asp:BoundColumn DataField="RequestDate" HeaderText="Request date"></asp:BoundColumn>
          <asp:BoundColumn Visible="False" DataField="SurveyId"></asp:BoundColumn>
          <asp:BoundColumn Visible="False" DataField="EmailId"></asp:BoundColumn>
          </Columns>
          </asp:datagrid>
           </div>

</li><li>
          <asp:linkbutton id="PreviousPendingPageLinkButton" runat="server">&lt;&lt; Previous page</asp:linkbutton> &nbsp;|&nbsp;
          <asp:linkbutton id="NextPendingPageLinkButton" runat="server">Next page >></asp:linkbutton>
</li><li>
          <asp:button id="DeleteRequestButton" CssClass="btn btn-primary btn-xs bw"  runat="server" Text="Delete selected requests"></asp:button>
    <br />
    </li>
          </asp:placeholder>
          
          <asp:placeholder id="NoPending" runat="server">
<li>
              <asp:Literal id="NoEmailInvitationInfo" runat="server" EnableViewState="False">No
          email invitation pending but you can start sending invitations right
          now using the mailing tool</asp:Literal>
 </li>
          </asp:placeholder>


                         
  </ol>
                    <br />
                    </fieldset>
         <br />
                <fieldset>
                    <legend class="titleFont titleLegend">
            
        <asp:Literal id="ValidatedEmailTitle" runat="server" EnableViewState="False">Validated emails answers - Page</asp:Literal>
                   

            <asp:label id="CurrentAnsweredPageLabel" runat="server"></asp:label>&nbsp;/
            <asp:label id="TotalAnsweredPagesLabel" runat="server"></asp:label>
 </legend><br />
<ol>
     
            <asp:placeholder id="NoAnswersPlaceHolder" runat="server">
 <li> 
            <asp:Literal id="NoEmailAnswered" runat="server" EnableViewState="False">No emails have answered your
            invitations yet.</asp:Literal>
</li>
            </asp:placeholder>
            
            <asp:placeholder id="AnswersPlaceHolder" runat="server">
<li>
                <div class="rounded_corners">
            <asp:datagrid id="AnsweredEmailsDatagrid" runat="server" border="0" AllowPaging="False" Pagesize="10" AutoGenerateColumns="False" Width="100%">
            <AlternatingItemStyle BackColor="#FFF6BB">
            </AlternatingItemStyle>
            
            <ItemStyle Font-Size="Small" BackColor="White">
            </ItemStyle>
            
            <HeaderStyle Font-Size="Small" Font-Bold="True" BackColor="#e2e2e2" BorderColor="#e2e2e2" ForeColor="#5720C6" >
            </HeaderStyle>
            
            <Columns>
            <mbrsc:RowSelectorColumn AllowSelectAll="True"></mbrsc:RowSelectorColumn>
            <asp:TemplateColumn>
            <ItemTemplate>
            </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="Email" HeaderText="Email"></asp:BoundColumn>
            <asp:BoundColumn DataField="VoteDate" HeaderText="Answer date"></asp:BoundColumn>
            </Columns>
            </asp:datagrid>
            </div>
    </li><li>
            <asp:linkbutton id="PreviousAnsweredPageLinkButton" runat="server">&lt;&lt; Previous page</asp:linkbutton> &nbsp;|&nbsp; 
             <asp:linkbutton id="NextAnsweredPageLinkButton" runat="server">Next page >></asp:linkbutton>
        </li>
                <li>
            <asp:Button id="DeleteVoterButton" CssClass="btn btn-primary btn-xs bw"  runat="server" Text="Delete selected voters"></asp:Button>
 <br />
   </li>

            </asp:placeholder>

                  
  </ol>
                    <br />
                    </fieldset>
           <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>
