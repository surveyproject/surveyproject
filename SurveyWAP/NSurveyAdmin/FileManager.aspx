<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.FileManager" Codebehind="FileManager.aspx.cs" %>
<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="SurveyProject.WebApplication" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

<div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/File Manager.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
                        <fieldset>
                <legend class="titleFont titleLegend">

                                            <asp:Literal ID="UploadedFilesTitle" runat="server" Text="Uploaded files - Page"
                                                EnableViewState="False"></asp:Literal><asp:Label ID="CurrentPendingPageLabel" runat="server">1</asp:Label>&nbsp;/
                                            <asp:Label ID="TotalPendingPagesLabel" runat="server">1</asp:Label>
                                    </legend>
                <br />
                <ol>
                    <li>
                         <div class="rounded_corners">
                            <asp:DataGrid ID="ValidatedFilesDataGrid" runat="server" Width="100%"  AutoGenerateColumns="False"
                                AllowCustomPaging="True">
                                <AlternatingItemStyle BackColor="#F4F9FA"></AlternatingItemStyle>
                                <ItemStyle Font-Size="XX-Small" BackColor="White"></ItemStyle>
                                <HeaderStyle VerticalAlign="Middle" Font-Size="XX-Small" Font-Bold="True" BackColor="#e2e2e2" BorderColor="#e2e2e2" ForeColor="#5720C6" ></HeaderStyle>
                                <Columns>
                                    <mbrsc:RowSelectorColumn AllowSelectAll="True">
                                    </mbrsc:RowSelectorColumn>
                                    <asp:ButtonColumn Text="Download" HeaderText="Download" CommandName="download"></asp:ButtonColumn>
                                    <asp:ButtonColumn Text="Details" HeaderText="Posted by" CommandName="Select"></asp:ButtonColumn>
                                    <asp:BoundColumn DataField="FileName" HeaderText="File name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FileType" HeaderText="Type"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <%#(Math.Round(double.Parse(DataBinder.Eval(Container.DataItem, "FileSize").ToString())/1048576*100000)/100000).ToString("0.##")  + " " + GetPageResource("UploadFileSizeFormat")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="SaveDate" HeaderText="Upload date" DataFormatString="{0:dd/MM/yyyy}">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn Visible="True" HeaderText="GroupGuid" DataField="GroupGuid"></asp:BoundColumn>
                                    <asp:BoundColumn Visible="True" HeaderText="VoterId" DataField="VoterId"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                             </div>
                                            </li>
                    <li>
                                <div style="left: 30px; position:absolute; ">
                            <asp:Button ID="PreviousValidatedPageLinkButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Previous page" /> &nbsp;&nbsp;
                          <asp:Button ID="NextValidatedPageLinkButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Next Page"></asp:Button>
                                </div>
                             <div style="right: 10px;">
                                <asp:Button ID="DeleteFilesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete selected files"></asp:Button>
                            </div>
                        <br />
                                            </li>
                                                        </ol>
            </fieldset>
                    
                <asp:PlaceHolder ID="FileExportPlaceHolder" runat="server">
                                            <fieldset>
                <legend class="titleFont titleLegend">
                        <asp:Literal ID="ExportFilesTitle" runat="server" EnableViewState="False" Text="Export all files to server's directory"></asp:Literal>
                                    </legend>
                <br />
                <ol>
                    <li>
                                                <asp:Label ID="CreateGroupsLabel" AssociatedControlID="FileGroupsDropDownList" runat="server" EnableViewState="False" Text="Create group directories"></asp:Label>

                                            <asp:DropDownList ID="FileGroupsDropDownList" runat="server">
                                            </asp:DropDownList>
                                        </li>
                    <li>
                                                <asp:Label ID="ServerPathLabel" AssociatedControlID="ServerPathTextBox" runat="server" EnableViewState="False" Text="Path on server :"></asp:Label>

                                            <asp:TextBox ID="ServerPathTextBox" runat="server" Columns="40"></asp:TextBox>
                        <br /><br />
                                            <asp:Literal ID="PathExampleLabel" runat="server" EnableViewState="False" Text="e.g : c:\myfiledirectory"></asp:Literal>
                                            </li>
                    <li>
                                <asp:Button ID="ExportFilesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Export to server"></asp:Button>
                        <br />
                                            </li>
                                    </ol>
            </fieldset>

                </asp:PlaceHolder>
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
    
</div></asp:Content>