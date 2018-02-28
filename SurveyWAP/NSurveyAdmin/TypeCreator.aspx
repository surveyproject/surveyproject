<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.TypeCreator" Codebehind="TypeCreator.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="TypeOptionControl" Src="UserControls/TypeOptionControl.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="Panel" class="Panel">

        <uc1:TypeOptionControl ID="TypeOption" runat="server" Visible="true"></uc1:TypeOptionControl>

    </div>

</asp:Content>
