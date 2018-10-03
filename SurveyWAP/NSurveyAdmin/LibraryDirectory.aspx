<%@ Page Language="c#" MasterPageFile="~/Wap.master" AutoEventWireup="true" Inherits="Votations.NSurvey.WebAdmin.LibraryDirectory"
    CodeBehind="LibraryDirectory.aspx.cs" %>
<%@ Import Namespace="Votations.NSurvey.WebAdmin" %>
<%@ Register TagPrefix="uc1" TagName="LibraryNew" Src="UserControls/LibraryNew.ascx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
    <input type="hidden" id="editmode" name="editmode" value="<%= editMode %>" />

    <div id="tabs">
        <ul>
            <li><a runat="server" id="Tab1" href="#tab-1"><%# GetPageResource("LibraryList") %>  </a></li>
            <li><a runat="server" id="Tab2" href="#tab-2"><%# GetPageResource("NewTab") %></a></li>
        </ul>
       

        <div id="Panel" class="Panel">

        <div   id="tab-1">
            <asp:Panel runat="server" ID="LibraryList">

                                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/QL_Introduction.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>

            <fieldset>
                 <legend class="titleFont titleLegend">
            <asp:Literal ID="LibraryDirectoryLegend" runat="server"  EnableViewState="false">Library Directory</asp:Literal>

        </legend>
                         <br />

<div class="rounded_corners">
                <asp:GridView ID="gridLibraries" runat="server" AllowSorting="false" AutoGenerateColumns="false"
                    Width="100%" OnSelectedIndexChanged="gridLibraries_SelectedIndexChanged" 
                    AlternatingRowStyle-BackColor="#FFF6BB" ShowFooter="True"  FooterStyle-BackColor="#FFDF12" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="#E2E2E2">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" ToolTip="Library Templates" Text='<%# Eval("LibraryName") %>' runat="server"
                                    OnCommand="EditLibrary" CommandArgument='<%# Eval("LibraryId") %>' CssClass="hyperlink" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Description" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" />
                        <asp:BoundField  DataField="QuestionCnt" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2"/>
                        <asp:TemplateField  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2">
                            <ItemTemplate>
                             <!--      <asp:LinkButton ID="LinkButton2" Text="Edit" runat="server" OnCommand="EditLibraryProperties"
                                    CommandArgument='<%# Eval("LibraryId") %>' CssClass="hyperlink" /> -->
                                <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Edit Library" ImageUrl="~/Images/edit.gif" Height="16" Width="16" OnCommand="EditLibraryProperties"
                                    CommandArgument='<%# Eval("LibraryId") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
    
 </div><br />
         </fieldset>

            </asp:Panel>

        <div id="LibaryEditDiv">
            <asp:Panel runat="server" ID="LibraryEdit">
                       <fieldset id="liML" runat="server">                          
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" ToolTip="Click to go Back" runat="server"
                    CssClass="buttonBack" OnCommand="EditBackButton" />

                                                                                   <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Library Directory.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
                </fieldset>
                  
                <uc1:LibraryNew runat="server" ID="lbnEdit" />
                <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
            </asp:Panel>
        </div>

        </div>


        <div  id="tab-2">
            <uc1:LibraryNew runat="server" ID="lbnNew" />
        </div>
            </div></div>
    
</asp:Content>
