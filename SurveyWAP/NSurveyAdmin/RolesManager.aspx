<%@ Register TagPrefix="uc1" TagName="RolesOptionsControl" Src="UserControls/RolesOptionsControl.ascx" %>
<%@ Page language="c#" MasterPageFile="~/Wap.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.RolesManager" Codebehind="RolesManager.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">
    <table class="TableLayoutContainer">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <table class="Innertext">
                    <tr>
                        <td>
                            <font class="titleFont">
                                <asp:Literal ID="RolesManagerTitle" runat="server" EnableViewState="False" Text="Roles manager"></asp:Literal></font>
                            <br />
                        </td>
                   <td align="right">

                        <div style="position: relative; right: -10px; top: 0px;">
                            <a onmouseover='this.style.cursor="pointer" ' onfocus='this.blur();' onclick="document.getElementById('PopUp').style.display = 'block' ">
                                <img title="Click for more Information" alt="help" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
                            </a>
                            <div id='PopUp' style='display: none; position: absolute; left: -450px; top: 0px;
                                border: solid #CCCCCC 1px; padding: 10px; background-color: rgba(255,255,225,1); text-align: justify;
                                font-size: 11px; width: 425px; line-height:15px; -webkit-border-radius: 5px;  -moz-border-radius: 5px;'>
                                <asp:Literal ID="RolesManagerHelp" runat="server" EnableViewState="False"></asp:Literal> 
                                <br />
                                <div style='text-align: right;'>
                                    <a onmouseover='this.style.cursor="pointer" ' style='font-size: 12px;' onfocus='this.blur();'
                                        onclick="document.getElementById('PopUp').style.display = 'none' "><img alt="Close" title="Close" src="<%= Page.ResolveUrl("~")%>Images/close-icn.png" /> </a></div>
                            </div>
                        </div>

                    </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table class="Innertext">
                                <tr>
                                    <td>
                                        <p>
                                            <asp:Literal ID="RolesToEditLabel" runat="server" EnableViewState="False" Text="Select a role to edit / view :"></asp:Literal>
                                            <br />
                                            <asp:DropDownList ID="RolesDropDownList" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                            <b>
                                                <asp:HyperLink ID="CreateRoleHyperLink" runat="server">Click here to create a new role</asp:HyperLink></b></p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <uc1:RolesOptionsControl ID="RolesOptions" runat="server"></uc1:RolesOptionsControl>
            </td>
        </tr>
    </table>
            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>
