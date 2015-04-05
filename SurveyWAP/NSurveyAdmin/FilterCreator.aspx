<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FilterOptionControl" Src="UserControls/FilterOptionControl.ascx" %>

<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.FilterCreator"
    CodeBehind="FilterCreator.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>

                            <div style="position: absolute; width: 50px; text-align: center; margin-left: 700px; top: 15px;">
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/back_button.gif" runat="server"
                    CssClass="buttonBack" ToolTip="Click to go Back" OnCommand="OnBackButton" />
                                </div>

                <uc1:FilterOptionControl ID="FilterOption" Visible="true" runat="server"></uc1:FilterOptionControl>

</div></div></asp:Content>
