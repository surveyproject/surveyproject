<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.TypeEditor" CodeBehind="TypeEditor.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="TypeOptionControl" Src="UserControls/TypeOptionControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Panel" class="Panel">

        <div class="helpDiv">
            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/new/AT_Introduction.aspx"
                title="Click for more Information">
                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
            </a>
        </div>

        <fieldset>
            <legend class="titleFont titleLegend">
                <asp:Literal ID="AnswerTypeBuilderTitle" runat="server" EnableViewState="False">Answer type builder</asp:Literal>
            </legend>
            <br />

            <ol>
                <li>
                    <asp:Label ID="TypeToEditLabel" AssociatedControlID="TypesDropDownList" runat="server">Select a type to edit / view : </asp:Label>
                    &nbsp;&nbsp;
                                        <asp:DropDownList ID="TypesDropDownList" Width="200" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                </li>
                <li>
                    <asp:Literal ID="BuiltInTypeNotEditedLabel" runat="server" EnableViewState="False">(built in types cannot be edited)</asp:Literal>
                </li>
                <li>
                    <asp:Button ID="CreateTypeHyperLink" CssClass="btn btn-primary btn-xs bw" Text="Create New type" runat="server" />
                    <br />
                </li>
            </ol>
            <br />
        </fieldset>

        <uc1:TypeOptionControl ID="TypeOption" runat="server" Visible="False"></uc1:TypeOptionControl>

    </div>

</asp:Content>
