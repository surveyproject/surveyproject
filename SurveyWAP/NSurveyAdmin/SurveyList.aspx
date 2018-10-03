<%@ Page Title="" Language="C#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="SurveyList.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.SurveyList" %>
<%@ Register TagPrefix="uc1" TagName="SurveyOptionControl" Src="UserControls/SurveyOptionControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    function ConfirmSurveyDelete() {
        return confirm('<%= GetPageResource("SurveyListDeleteConfirm") %>');
    }
</script>

        <div id="Panel" class="Panel">
            <asp:PlaceHolder ID="surveyList" runat="server">
        <div id="tabs-1">
                                                   <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/surveylistdirectory.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
            <fieldset >
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

            <div class="rounded_corners">
                        <asp:GridView ID="gridSurveys" Width="100%" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            ShowHeaderWhenEmpty="true" OnSorting="gvSurveys_Sorting" AlternatingRowStyle-BackColor="#FFF6BB" ShowFooter="True" AllowPaging="True" PagerSettings-Mode="NumericFirstLast"  OnPageIndexChanging="gridSurveys_PageIndexChanging" PageSize="10" FooterStyle-BackColor="#FFDF12" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="#E2E2E2">
                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                            <Columns>
                                <asp:TemplateField HeaderText="Survey" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" ItemStyle-Width="30%" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2">
                                    <HeaderTemplate>
                                                <div style="width:80%; float:left; margin:10px 0 0 0;text-align:center;">
                                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyTitle") %>' ToolTip='<%#GetPageResource("SurveyTitleTooltip") %>' />
                                                </div>
                                                <div style="width:10%; float:right; margin:2px 0px 0 0;">
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_up.gif" CommandName="sort"
                                                        CommandArgument="Title ASC" /><br />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_down.gif" CommandName="sort"
                                                        CommandArgument="Title DESC" />
                                                </div>                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="float:left; margin:0px 0 0 5px; text-align:left;">
                                        <asp:LinkButton runat="server"  ToolTip='<%# GetSurveyFriendlyName(Container.DataItemIndex + 1, (string)Eval("FriendlyName")) %>' Text='<%#GetSurveyName(Container.DataItemIndex + 1, (string)Eval("Title")) %>'
                                            OnCommand="OnSurveyStats" CommandName="SurveyStats" CommandArgument='<%#Eval("SurveyID")%>' CssClass="hyperlink" />
                                        [<asp:Label runat="server" Text='<%#Eval("SurveyID")%>' />]
                                            </div>
                                    </ItemTemplate>
                                </asp:TemplateField >


                                <asp:TemplateField  ItemStyle-Wrap="true" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#E2E2E2" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                    <HeaderTemplate>
                                            <div style="width:80%; float:left; margin:10px 0 0 0;text-align:center;">
                                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyStatus") %>' ToolTip='<%#GetPageResource("SurveyStatusTooltip") %>' />  &nbsp;
                                                </div>
                                                <div style="width:15%; float:right; margin:2px 5px 0 0">
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_up.gif" CommandName="sort"
                                                        CommandArgument="Activated ASC" /><br />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_down.gif" CommandName="sort"
                                                        CommandArgument="Activated DESC" />
                                                </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#GetStatus((bool)Eval("Activated")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-Wrap="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#E2E2E2" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                    <HeaderTemplate>
                                            <div style="width:75%; float:left; margin:10px 0 0 0;text-align:center;">
                                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyResponses") %>' ToolTip='<%#GetPageResource("SurveyReponsesTooltip") %>' /> &nbsp;
                                                </div>
                                                <div style="width:15%; float:right; margin:2px 5px 0 0">
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_up.gif" CommandName="sort"
                                                        CommandArgument="VoterNumber ASC" /><br />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_down.gif" CommandName="sort"
                                                        CommandArgument="VoterNumber DESC" />
                                                </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("VoterNumber")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-Wrap="true" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#E2E2E2">
                                    <HeaderTemplate>
                                            <div style="width:80%; float:left; margin:10px 0 0 0;text-align:center;">
                                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyCreated") %>' ToolTip='<%#GetPageResource("SurveyCreatedTooltip") %>' /> &nbsp;
                                                </div>
                                            <div style="width:15%; float:right; margin:2px 5px 0 0">
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_up.gif" CommandName="sort"
                                                        CommandArgument="CreationDate ASC" /><br />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/arrow_down.gif" CommandName="sort"
                                                        CommandArgument="CreationDate DESC" />
                                                </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("CreationDate")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField  ItemStyle-Wrap="true" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#E2E2E2">
                                    <HeaderTemplate>
                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyEdit") %>' />
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                                    <asp:ImageButton runat="server" ToolTip="Edit Survey Settings" ImageUrl="~/Images/edit.gif" OnCommand="OnSurveyEdit"
                                            CommandName="SurveyEdit" CommandArgument='<%#Eval("SurveyID")%>' />                                               
                                            
                                            </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#E2E2E2" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                    <HeaderTemplate>
                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveySecurity") %>' />
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                                    <asp:ImageButton runat="server" ToolTip="Edit Security Settings" ImageUrl="~/Images/lock.gif" OnCommand="OnSurveySecurity"
                                            CommandName="SurveySecurity" CommandArgument='<%#Eval("SurveyID")%>'  />                                            
                                            
                                     </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Wrap="true"  ItemStyle-Width="9%" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" HeaderStyle-HorizontalAlign="Center" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#E2E2E2" ItemStyle-BorderStyle="Solid">
                                    <HeaderTemplate>
                                    <asp:Label runat="server" Text='<%#GetPageResource("SurveyDesign") %>' />
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                                    <asp:ImageButton runat="server" ToolTip="Edit Survey Form" ImageUrl="~/Images/edit_pen.gif" OnCommand="OnSurveyDesign"
                                            CommandName="SurveyDesign" CommandArgument='<%#Eval("SurveyID")%>' />


                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                </div>
            <br />

            
            </div>
                </asp:PlaceHolder>
          

        <div id="tabs-2">

            <uc1:SurveyOptionControl ID="SurveyOption" runat="server" Visible="false"></uc1:SurveyOptionControl>
            
        </div>   
            
            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
    </div>
</asp:Content>
