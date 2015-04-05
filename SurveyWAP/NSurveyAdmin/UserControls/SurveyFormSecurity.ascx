<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyFormSecurity.ascx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.SurveyFormSecurity" %>
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
                                            <asp:Literal ID="SurveySecurityTitle" runat="server" Text="Survey security" EnableViewState="False"></asp:Literal></font>
                                    </td>
                                    <td align="right">
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
                            <asp:PlaceHolder ID="SecurityOptionsPlaceHolder" runat="server" Visible="False"><strong>
                                <asp:Label ID="UnAuthentifiedUserActionLabel" runat="server">If user has not been automatically authentified by all security addins :</asp:Label>&nbsp;</strong><asp:DropDownList
                                    ID="ActionsDropDownList" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <br />
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="AddInListPlaceHolder" runat="server"></asp:PlaceHolder>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>