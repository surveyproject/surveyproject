<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiLanguagesEdit.ascx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.MultiLanguagesEdit" %>
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="SurveyListControl.ascx" %>

    <table class="TableLayoutContainer">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
          <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                <table class="innerText">
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td width="99%">
                                        <font class="titleFont">
                                            <asp:Literal ID="MultiLanguagesTitle" runat="server" EnableViewState="False" Text="Multi-Languages settings"></asp:Literal></font>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <uc1:SurveyListControl ID="SurveyList" runat="server"></uc1:SurveyListControl>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <table class="innerText">
                                <tr>
                                    <td nowrap="nowrap" width="180">
                                        <strong>
                                            <asp:Literal ID="EnableMultiLanguagesLabel" runat="server" EnableViewState="False"
                                                Text="Enable multi-languages :"></asp:Literal></strong>
                                    </td>
                                    <td width="480">
                                        <asp:CheckBox ID="MultiLanguagesCheckBox" runat="server" AutoPostBack="True"></asp:CheckBox>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="MultiLanguagesPlaceHolder" Visible="False" runat="server">
                                    <tr>
                                        <td width="180">
                                            <strong>
                                                <asp:Literal ID="MultiLanguagesModeLabel" runat="server" EnableViewState="False"
                                                    Text="Multi languages mode :"></asp:Literal></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="MultiLanguagesModeDropDownList" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="180" valign="top">
                                            <strong>
                                                <asp:Literal ID="VariableNameLabel" runat="server" Visible="False" Text="Variable name :"></asp:Literal></strong>
                                        </td>
                                        <td>
                                            
                                                <asp:TextBox ID="VariableNameTextBox" runat="server" Visible="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="VariableNameUpdateButton" runat="server" Visible="False" Text="Update">
                                                </asp:Button>
                                                <br />
                                                <asp:Label ID="VariableNameInfoLabel" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="180"><br />
                                            <strong>
                                                <asp:Literal ID="EnabledLanguagesLabel" runat="server" EnableViewState="False" Text="Enabled languages :"></asp:Literal></strong>
                                        </td>
                                        <td><br />
                                            <table width="375">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:ListBox ID="DisabledLanguagesListBox" runat="server" AutoPostBack="True"></asp:ListBox>
                                                    </td>
                                                    <td align="center">
                                                        &gt;&gt;<br />
                                                        &lt;&lt;
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <asp:ListBox ID="EnabledLanguagesListBox" runat="server" AutoPostBack="True"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="180"><br />
                                            <strong>
                                                <asp:Literal ID="DefaultLanguageLabel" runat="server" EnableViewState="False" Text="Set default language to :"></asp:Literal></strong>
                                        </td>
                                        <td><br />
                                            <asp:DropDownList ID="DefaultLanguageDropdownlist" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>