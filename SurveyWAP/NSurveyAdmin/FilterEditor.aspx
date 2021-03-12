
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.FilterEditor" Codebehind="FilterEditor.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="FilterOptionControl" Src="UserControls/FilterOptionControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel content">
                                                                <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/Report%20Filter%20Editor.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>
            <fieldset>
                <legend class="titleFont titleLegend">
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
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
        </div>

</asp:Content>