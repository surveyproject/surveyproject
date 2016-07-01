<%@ Import Namespace="Votations.NSurvey.Data" %>

<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.LibraryTemplates" Codebehind="LibraryTemplates.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="LibraryQuestionOptionsControl" Src="UserControls/LibraryQuestionOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>
            <br />
                  <fieldset style="width:730px; margin-left:12px; margin-top:15px;" title="">
        <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;"><asp:Literal ID="LibraryQuestionsTemplatesTitle" runat="server" Text="Library templates"
                                    EnableViewState="False"></asp:Literal></legend><br />
                <ol>
                    <li>

                <asp:Literal ID="PreviewSurveyLanguageLabel" runat="server" Text="Language preview :"></asp:Literal>

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


</div></div>


</asp:Content>
