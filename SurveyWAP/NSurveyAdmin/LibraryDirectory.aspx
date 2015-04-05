<%@ Import Namespace="Votations.NSurvey.WebAdmin" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LibraryNew" Src="UserControls/LibraryNew.ascx" %>

<%@ Page Language="c#" MasterPageFile="~/Wap.master" AutoEventWireup="true" Inherits="Votations.NSurvey.WebAdmin.LibraryDirectory"
    CodeBehind="LibraryDirectory.aspx.cs" %>

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

    <div id="tabs" style=" min-height:750px;">
        <ul>
            <li><a runat="server" id="Tab1" href="#tab-1"><%# GetPageResource("LibraryList") %>  </a></li>
            <li><a runat="server" id="Tab2" href="#tab-2"><%# GetPageResource("NewTab") %></a></li>
        </ul>
       
            <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

        <div   id="tab-1"><br />
            <asp:Panel runat="server" ID="LibraryList">

                     <fieldset style="width:730px; margin-left:12px; margin-top:10px;" title="">
        <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
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
                                            <div style="position: absolute; width: 50px; text-align: center; margin-left: 700px; top: 15px;">
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" ToolTip="Click to go Back" runat="server"
                    CssClass="buttonBack" OnCommand="EditBackButton" />
                </div>

                <uc1:LibraryNew runat="server" ID="lbnEdit" />

            </asp:Panel>
        </div>

        </div>


        <div  id="tab-2">
            <uc1:LibraryNew runat="server" ID="lbnNew" />
        </div>
            </div></div>
    </div>
</asp:Content>
