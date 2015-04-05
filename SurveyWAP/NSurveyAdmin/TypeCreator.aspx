<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TypeOptionControl" Src="UserControls/TypeOptionControl.ascx" %>

<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.TypeCreator" Codebehind="TypeCreator.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

                <uc1:TypeOptionControl ID="TypeOption" runat="server" Visible="true"></uc1:TypeOptionControl>

</div></div></asp:Content>
