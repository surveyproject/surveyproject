<%@ Page Title="" Language="C#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"
    AutoEventWireup="true" CodeBehind="SurveyTokenSecurity.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.SurveyTokenSecurity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

    <script type="text/javascript">
        function ConfirmSelectedTokenDelete() {
            return confirm('<%= GetPageResource("msgDeleteSelectedTokens") %>');
        }

        function ConfirmAllTokenDelete() {
            return confirm('<%= GetPageResource("msgDeleteAllTokens") %>');
        }
    </script>

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
            <br />
                    <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;">
                         <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                    <asp:Literal ID="literalTokenSecurityTitle" runat="server" Text="Data Import" EnableViewState="False"></asp:Literal>
                        </legend> 
                         <br />
<ol>
     <li>
                <asp:Label ID="lblGenerateCountPrompt" AssociatedControlID="txtNumTokens" runat="server" Text="Number of tokens:" />&nbsp;&nbsp;
                <asp:TextBox runat="server" ID="txtNumTokens"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnGenerate" Text="Generate" CssClass="btn btn-primary btn-xs bw"
                    OnClick="btnGenerate_Click" /><br />
</li><li>
                <asp:Label ID="lblTokensCreatedPrompt" AssociatedControlID="lblTokensCreated" runat="server" Text="Number of Tokens created:" />&nbsp;&nbsp;
                <asp:Label ID="lblTokensCreated" runat="server" Text="" />
</li><li>
                <asp:Label ID="lblTokensAvailPrompt" AssociatedControlID="lblTokensAvailable" runat="server" Text="Number of Tokens available:" />&nbsp;&nbsp;
                <asp:Label ID="lblTokensAvailable" runat="server" Text="" />
</li><li>
                <asp:Label ID="lblTokensUsedPrompt" AssociatedControlID="lblTokensUsed" runat="server" Text="Number of Tokens used:" />&nbsp;&nbsp;
                <asp:Label ID="lblTokensUsed" runat="server" Text="" />
</li><li>
    <br /><br />
                            <asp:Label ID="ltFilterToken" AssociatedControlID="txtToken" runat="server" Text="By Token"></asp:Label>

                            <asp:TextBox runat="server" ID="txtToken"></asp:TextBox>
</li><li>
                            <asp:Label ID="ltFilterType" AssociatedControlID="ddlTokenOptions" runat="server" Text="By Token Type"></asp:Label>

                            <asp:DropDownList runat="server" ID="ddlTokenOptions">
                                <asp:ListItem Text="UnUsed Only" Value="Unused"></asp:ListItem>
                                <asp:ListItem Text="Used Only" Value="Used"></asp:ListItem>
                                <asp:ListItem Text="All Tokens" Value="All"></asp:ListItem>
                            </asp:DropDownList>
</li><li>
                            <asp:Label ID="ltIssuedOn" AssociatedControlID="txtIssuedOn" runat="server" Text="By Issued Date"></asp:Label>

                            <asp:TextBox runat="server" ID="txtIssuedOn"></asp:TextBox>
              
</li><li>
                            <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" ID="btnApplyFilter" Text="Apply Filter"
                                OnClick="btnApplyFilter_Click" /><br />


    </li><li>

                <asp:ListView ID="lvTokens" runat="server" GroupItemCount="2" OnPagePropertiesChanged="lvTokens_PagePropertiesChanged">
                    <LayoutTemplate>
                        <table  class="BorderedTable" width="100%" cellspacing="0">

                            <asp:PlaceHolder ID="GroupPlaceHolder" runat="server"></asp:PlaceHolder>

                            <asp:DataPager PageSize="30" PagedControlID="lvTokens" ID="DataPager2" runat="server">
                                <Fields>
                                    <asp:NextPreviousPagerField PreviousPageText="<" ShowNextPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField NextPageText=">" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <td  class="<%# (Container.DisplayIndex/2) % 2 == 0 ? "" : "AlternateStyle" %>" width="50%"
                            style="padding-right: 6px;">
                            <table>
                                <td>
                                    <asp:Literal ID="ltTokenId" runat="server" Visible="false" Text='<%# Eval("TokenId") %>'></asp:Literal>
                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox Width="63px" ID="txtCreationDate" ReadOnly="true" Text='<%#Eval("CreationDate","{0:d}")%>'
                                        runat="server"> </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox Width="190px" ID="txtToken" ReadOnly="true" Text='<%#Eval("Token")%>'
                                        runat="server"> </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox Width="12px" ID="chkUsed" ReadOnly="true"  BackColor='<%#((bool)Eval("Used"))? System.Drawing.Color.Red : System.Drawing.Color.Green%>'
                                        runat="server"></asp:TextBox>
                                </td>
                            </table>
                        </td>
                    </ItemTemplate>
             
                </asp:ListView>

        </li><li>
            
                <asp:Button runat="server" CssClass="btn btn-primary btn-xs bw" ID="btnDeleteSelected" OnClientClick="return ConfirmSelectedTokenDelete();"
                    Text="Delete selected" OnClick="btnDeleteSelected_Click" />
                <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" OnClientClick="return ConfirmAllTokenDelete();"
                    ID="btnDeleteAll" Text="Delete all" OnClick="btnDeleteAll_Click" />
                <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" ID="btnExport" 
                    Text="Export Tokens" onclick="btnExport_Click" />
            <br />
       </li>
  </ol>
                    <br />
                    </fieldset>

</div></div></asp:Content>
