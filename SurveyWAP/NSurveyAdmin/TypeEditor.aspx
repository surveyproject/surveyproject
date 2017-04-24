<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.TypeEditor" Codebehind="TypeEditor.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="TypeOptionControl" Src="UserControls/TypeOptionControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

                            <div style="position: relative; left: 720px; width: 10px;  top: 13px; clear:none;">
                                <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/FieldT_Introduction.aspx"
                                    title="Click for more Information">
                                    <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                                </a>
                            </div>

                        <fieldset style="width:750px; margin-left:12px; margin-top:15px;">
                <legend class="titleFont titleLegend">
                    <asp:Literal ID="AnswerTypeBuilderTitle" runat="server" EnableViewState="False">Answer type builder</asp:Literal>
                </legend> <br />
                        


<ol>
     <li>
                                        <asp:Label ID="TypeToEditLabel" AssociatedControlID="TypesDropDownList" runat="server">Select a type to edit / view : </asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="TypesDropDownList" width="200" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </li><li>
                                        <asp:Literal ID="BuiltInTypeNotEditedLabel" runat="server" EnableViewState="False">(built in types cannot be edited)</asp:Literal>
  </li><li>
                                        <asp:HyperLink ID="CreateTypeHyperLink" runat="server">Click here to create a new type ...</asp:HyperLink>
            </li>
  </ol>
                    <br />
                    </fieldset>

<uc1:TypeOptionControl ID="TypeOption" runat="server" Visible="False"></uc1:TypeOptionControl>


</div></div>

</asp:Content>
