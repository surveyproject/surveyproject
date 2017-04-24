<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.InsertSecurityAddIn" Codebehind="InsertSecurityAddIn.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

    <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;">
        <legend class="titleFont titleLegend">
            <asp:Literal ID="SurveyAddAdinTitle" runat="server" EnableViewState="False" Text="Add new security addin"></asp:Literal>
        </legend>
        <br />
        <ol>
            <li>
                <asp:Label ID="AvailableAddInsLabel" AssociatedControlID="SecurityAddInDropDownList" runat="server" EnableViewState="False"></asp:Label>
                <asp:DropDownList ID="SecurityAddInDropDownList" runat="server"></asp:DropDownList>
            </li>
            <li>
                <br />
                <div style="float: right; position: relative; top: -18px; margin-right: 70px;">
                    <asp:Button ID="AddAddinButton" runat="server" CssClass="btn btn-primary btn-xs bw" Text="Add security addin to survey"></asp:Button>
                </div>
            </li>
        </ol>
        <br />
    </fieldset>
</div></div></asp:Content>
