<%@ Import Namespace="Votations.NSurvey.Data" %>

<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.LibraryTemplates" Codebehind="LibraryTemplates.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="LibraryQuestionOptionsControl" Src="UserControls/LibraryQuestionOptionsControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

            
                                        <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Library%20Templates.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
         
                  <fieldset>
        <legend class="titleFont titleLegend">
            <asp:Literal ID="LibraryQuestionsTemplatesTitle" runat="server" Text="Library templates"
                                    EnableViewState="False"></asp:Literal></legend><br />
                <ol>
                    <li>

                <asp:Label ID="PreviewSurveyLanguageLabel" AssociatedControlID="LanguagesDropdownlist" runat="server" Text="Language preview :"></asp:Label>

                            <asp:DropDownList ID="LanguagesDropdownlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LanguagesDropdownlist_SelectedIndexChanged">
                            </asp:DropDownList>
</li><li>
               <asp:PlaceHolder ID="TemplatesPlaceHolder" runat="server"></asp:PlaceHolder>
    </li><li>
                <asp:Button CssClass="btn btn-primary btn-xs bw" ID="InsertQuestionButton" runat="server" Text="Insert new / existing question">
                </asp:Button>
        <br />

    </li></ol>
   </fieldset>
            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>

</div>


</asp:Content>
