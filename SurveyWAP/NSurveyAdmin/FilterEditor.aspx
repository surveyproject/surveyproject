
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.FilterEditor" Codebehind="FilterEditor.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="FilterOptionControl" Src="UserControls/FilterOptionControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">
            <br />
            <fieldset style="width: 730px; margin-left: 12px; margin-top: 15px;" title="Survey Title">
                <legend class="titleFont titleLegend" style="width:96%;">
                    <asp:Literal ID="FilterBuilderTitle" runat="server" EnableViewState="False">Results filter builder</asp:Literal>
                </legend>
                <br />
                <ol>
                    <li>
                        <asp:Label ID="SelectFilteraLabel" runat="server" AssociatedControlID="FilterDropDownList" EnableViewState="False">Select a filter to edit / view :</asp:Label>
                        <asp:DropDownList ID="FilterDropDownList" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        <br />
                    </li>
                    <li>
                        <asp:HyperLink ID="CreateFilterHyperLink" runat="server">Click here to create a new filter ...</asp:HyperLink>
                    </li>
                </ol>
            </fieldset>
                        <uc1:FilterOptionControl ID="FilterOption" Visible="false" runat="server"></uc1:FilterOptionControl>

        </div>
    </div>

</asp:Content>
