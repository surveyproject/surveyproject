<%@ Page Title="" Language="C#" MasterPageFile="~/Wap.Master" AutoEventWireup="true"
    CodeBehind="SurveyList.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.SurveyList" %>
<%@ Register TagPrefix="uc1" TagName="SurveyOptionControl" Src="UserControls/SurveyOptionControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<script type="text/javascript">
    function ConfirmSurveyDelete() {
        return confirm('<%= GetPageResource("SurveyListDeleteConfirm") %>');
    }
</script>

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
   <div id="surveysTabEvents" style="display: none" runat="server" onclick="foo" />

    <div id="tabs" style="min-height:750px;">
        <ul>
            <li><a runat="server" id="Tab1" href="#tabs-1">
                <%=GetPageResource("SurveyListTabList")%></a></li>
            <li><a runat="server" id="Tab2" href="#tabs-2">
                <%=GetPageResource("SurveyListTabNew")%></a></li>
        </ul>

        <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

        <div id="tabs-1" style="position: relative; left:-12px;">
           
            <fieldset style="width: 750px; margin-left: 7px; margin-top: 15px;">
                 <legend class="titleFont titleLegend">
                    <asp:Literal ID="SearchTitle" runat="server" EnableViewState="False">Search Surveylist</asp:Literal>
                    </legend>
                <br />
                <ol>
                    <li>
                        <asp:TextBox runat="server" Columns="50" ID="txtSearchField"></asp:TextBox>
                        <asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" ID="btnSearch" Text="" OnClick="OnSurveyFilter" />
                    </li>
                </ol><br />
            </fieldset>
            <br />

            <div class="rounded_corners" style="width: 750px; margin-left: 7px; margin-top: 10px;">
                        <asp:GridView ID="gridSurveys" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            ShowHeaderWhenEmpty="true" OnSorting="gvSurveys_Sorting" AlternatingRowStyle-BackColor="#FFF6BB" ShowFooter="True" AllowPaging="True"  OnPageIndexChanging="gridSurveys_PageIndexChanging" PageSize="10" FooterStyle-BackColor="#FFDF12" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="#E2E2E2">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Survey" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" ItemStyle-Width="25%" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2">
                                    <HeaderTemplate>
                                        <table align="center">
                                            <tr>
                                                <td style="width:90%; text-align:center;">
                                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyTitle") %>' /> &nbsp;
                                                </td>
                                                <td style="width:15%;">
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_up.gif" CommandName="sort"
                                                        CommandArgument="Title ASC" /><br />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_down.gif" CommandName="sort"
                                                        CommandArgument="Title DESC" />
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Survey Statistics" Text='<%#GetSurveyName(Container.DataItemIndex + 1, (string)Eval("Title")) %>'
                                            OnCommand="OnSurveyStats" CommandName="SurveyStats" CommandArgument='<%#Eval("SurveyID")%>' CssClass="hyperlink" />
                                    </ItemTemplate>
                                </asp:TemplateField >
                                <asp:TemplateField  ItemStyle-Wrap="true" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#E2E2E2" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                    <HeaderTemplate>
                                        <table align="center">
                                            <tr>
                                                <td style="width:90%; text-align:center;">
                                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyStatus") %>' />  &nbsp;
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_up.gif" CommandName="sort"
                                                        CommandArgument="Activated ASC" /><br />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_down.gif" CommandName="sort"
                                                        CommandArgument="Activated DESC" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#GetStatus((bool)Eval("Activated")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-Wrap="true" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#E2E2E2" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                    <HeaderTemplate>
                                        <table align="center">
                                            <tr>
                                                <td style="width:90%; text-align:center;">
                                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyResponses") %>' /> &nbsp;
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_up.gif" CommandName="sort"
                                                        CommandArgument="VoterNumber ASC" /><br />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_down.gif" CommandName="sort"
                                                        CommandArgument="VoterNumber DESC" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("VoterNumber")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-Wrap="true" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#E2E2E2">
                                    <HeaderTemplate>
                                        <table align="center">
                                            <tr>
                                                <td style="width:90%; text-align:center;">
                                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyCreated") %>' /> &nbsp;
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_up.gif" CommandName="sort"
                                                        CommandArgument="CreationDate ASC" /><br />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_down.gif" CommandName="sort"
                                                        CommandArgument="CreationDate DESC" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("CreationDate")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField  ItemStyle-Wrap="true" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#E2E2E2">
                                    <HeaderTemplate>
                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyEdit") %>' />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <!--     <asp:LinkButton runat="server" Text='<%#GetPageResource("SurveyEdit")%>' OnCommand="OnSurveyEdit"
                                            CommandName="SurveyEdit" CommandArgument='<%#Eval("SurveyID")%>' CssClass="hyperlink" /> -->
                                                    <asp:ImageButton runat="server" ToolTip="Edit Survey Information" ImageUrl="~/Images/edit.gif" OnCommand="OnSurveyEdit"
                                            CommandName="SurveyEdit" CommandArgument='<%#Eval("SurveyID")%>' />                                               
                                            
                                            </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#E2E2E2" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                    <HeaderTemplate>
                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveySecurity") %>' />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                               <!--         <asp:LinkButton runat="server" Text='<%#GetPageResource("SurveySecurity")%>' OnCommand="OnSurveySecurity"
                                            CommandName="SurveySecurity" CommandArgument='<%#Eval("SurveyID")%>' CssClass="hyperlink" /> -->
                                                    <asp:ImageButton runat="server" ToolTip="Survey Security Settings" ImageUrl="~/Images/lock.gif" OnCommand="OnSurveySecurity"
                                            CommandName="SurveySecurity" CommandArgument='<%#Eval("SurveyID")%>'  />                                            
                                            
                                     </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="true"  ItemStyle-Width="9%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#E2E2E2" ItemStyle-BorderStyle="Solid">
                                    <HeaderTemplate>
                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyDesign") %>' />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <!-- <asp:LinkButton runat="server" Text='<%#GetPageResource("SurveyDesign")%>' OnCommand="OnSurveyDesign"
                                            CommandName="SurveyDesign" CommandArgument='<%#Eval("SurveyID")%>' CssClass="hyperlink" /> -->
                                                    <asp:ImageButton runat="server" ToolTip="Survey Form Builder" ImageUrl="~/Images/edit_pen.gif" OnCommand="OnSurveyDesign"
                                            CommandName="SurveyDesign" CommandArgument='<%#Eval("SurveyID")%>' />


                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                </div>

            <div class="rounded_corners" style="width: 750px; text-align: center; margin-left: 7px; margin-top: 10px;">
                <i><asp:Literal ID="SurveyListPageNrLiteral" runat="server"  EnableViewState="false">You are viewing page</asp:Literal> <%=gridSurveys.PageIndex + 1%> / <%=gridSurveys.PageCount%> </i>
            </div>
            <!-- code to be added later: 
            <div class="rounded_corners" style="width: 750px; text-align: center; margin-left: 7px; margin-top: 10px;">
                <asp:LinkButton ID="PreviousSurveysPageLinkButton" runat="server">&lt;&lt; Previous page</asp:LinkButton>
                &nbsp;|&nbsp; 
             <asp:LinkButton ID="NextSurveysPageLinkButton" runat="server">Next page >></asp:LinkButton>
            </div>
            -->

            </div>


        <div id="tabs-2" style="position: relative; left:-17px;">

            <uc1:SurveyOptionControl ID="SurveyOption" runat="server" Visible="false"></uc1:SurveyOptionControl>

        </div>
        </div>
        </div>
      </div>

  
</asp:Content>
