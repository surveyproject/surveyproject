<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.InsertSecurityAddIn" Codebehind="InsertSecurityAddIn.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

            <div class="errorDiv">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
                </div>

                                                    <div class="helpDiv">
                                            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Insert Security AddIn.aspx"
                                                title="Click for more Information">
                                                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                            </a>
                                        </div>

    <fieldset>
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
                            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>
