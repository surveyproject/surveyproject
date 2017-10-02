
<%@ Page Language="c#" MasterPageFile="~/Wap.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserCreator"
    CodeBehind="UserCreator.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="UsersOptionsControl" Src="UserControls/UsersOptionsControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td class="contentCell" valign="top">
                <uc1:UsersOptionsControl ID="UsersOptions" runat="server"></uc1:UsersOptionsControl>
            </td>
        </tr>
    </table>
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>
