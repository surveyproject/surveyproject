<%@ Page Language="c#" MasterPageFile="MsterPageTabs.master" AutoEventWireup="false" ValidateRequest="false" Inherits="Votations.NSurvey.WebAdmin.EditRegEx" CodeBehind="EditRegEx.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="Panel" class="Panel">

        <div class="errorDiv">
            <asp:Label ID="MessageLabel" runat="server" CssClass="errorMessage" Visible="False"></asp:Label>
        </div>
        <div class="helpDiv">
            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/New/Regular Expression Editor.aspx"
                title="Click for more Information">
                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
            </a>
        </div>

        <fieldset>
            <legend class="titleFont titleLegend">
                <asp:Literal ID="RegExLibraryTitle" runat="server" Text="RegEx library" EnableViewState="False"></asp:Literal>
            </legend>
            <br />

            <ol>
                <li>
                    <asp:Label ID="RegExToEditLabel" AssociatedControlID="RegExDropDownList" runat="server"></asp:Label>
                    <asp:DropDownList ID="RegExDropDownList" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Button ID="CreateRegExBtn" runat="server" CssClass="btn btn-primary btn-xs bw" Text="Create Regex" />
                    [
                    <asp:HyperLink ID="RegExLibComHyperLink" runat="server" Target="_blank" NavigateUrl="http://www.regexlib.com">Go to http://www.regexlib.com for more regular expressions</asp:HyperLink>
                    ]
                </li>
                <asp:PlaceHolder ID="RegExOptionsPlaceHolder" Visible="False" runat="server">
                    <li>
                        <asp:Label ID="RegExOptionTitleLabel" runat="server">Label</asp:Label>
                    </li>
                    <li>
                        <asp:Label ID="RegExNameLabel" AssociatedControlID="RegExDescriptionTextbox" runat="server"></asp:Label>

                        <asp:TextBox ID="RegExDescriptionTextbox" Width="300" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="RegularExpressionLabel" AssociatedControlID="RegularExpressionTextbox" runat="server"></asp:Label>

                        <asp:TextBox ID="RegularExpressionTextbox" runat="server" Width="400" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="RegExErrorMessageLabel" AssociatedControlID="ErrorMessageTextbox" runat="server"></asp:Label>

                        <asp:TextBox ID="ErrorMessageTextbox" runat="server" Width="400" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Button ID="CreateNewRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create regex"></asp:Button>
                        <asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply changes"></asp:Button>
                        <asp:Button ID="DeleteRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete regex"></asp:Button>
                        <asp:Button ID="MakeBuiltInRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Make built in"></asp:Button>
                        <asp:Button ID="CancelRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Cancel"></asp:Button>
                        <br />

                </li>
                </asp:PlaceHolder>
                            </ol>
                </fieldset>
        <div class="errorDiv">
            <asp:Label ID="TestMessageLabel" runat="server" CssClass="errorMessage" Visible="False"></asp:Label>
        </div>

                <fieldset>
                     <legend class="titleFont titleLegend">
                        <asp:Label ID="TestRegExTitle" runat="server">Test a regular expression</asp:Label>
                         </legend>
                    <br />
            <ol>
                <li>
                    <asp:Label ID="RegularExpressionTestLabel" AssociatedControlID="TestExpressionTextbox" runat="server"></asp:Label>

                    <asp:TextBox ID="TestExpressionTextbox" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </li>
                <li>

                    <asp:Label ID="TestExpressionValueLabel" AssociatedControlID="TestValueTextBox" runat="server"></asp:Label>

                    <asp:TextBox ID="TestValueTextBox" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </li>
                <li>
                    <asp:Button ID="TextRegExButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Test expression"></asp:Button><br />
                </li>
            </ol>
        </fieldset>
        <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
    </div>

</asp:Content>
