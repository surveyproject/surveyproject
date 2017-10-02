<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FilterOptionControl" Src="UserControls/FilterOptionControl.ascx" %>

<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.FilterCreator"
    CodeBehind="FilterCreator.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="Panel" class="Panel content">
                                                                      <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Report%20Filter%20Creation.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>

<fieldset id="liML" runat="server">    
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" runat="server"
                    CssClass="buttonBack" ToolTip="Click to go Back" OnCommand="OnBackButton" />
                                </fieldset>
            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                <uc1:FilterOptionControl ID="FilterOption" Visible="true" runat="server"></uc1:FilterOptionControl>

                                     <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div>
            </asp:Content>
