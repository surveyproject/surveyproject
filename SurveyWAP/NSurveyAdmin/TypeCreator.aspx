
<%@ Register TagPrefix="uc1" TagName="TypeOptionControl" Src="UserControls/TypeOptionControl.ascx" %>

<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.TypeCreator" Codebehind="TypeCreator.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

                <uc1:TypeOptionControl ID="TypeOption" runat="server" Visible="true"></uc1:TypeOptionControl>

</div></asp:Content>
