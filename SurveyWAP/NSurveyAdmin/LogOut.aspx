<%@ Page language="c#" MasterPageFile="~/Wap.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.LogOut" Codebehind="LogOut.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">

<table class="TableLayoutContainer">
  <tr>
    <td></td></tr>
  <tr>
    <td class="contentCell" valign="top"> <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
    
      <table class="innerText">
        <tr>
          <td colspan="2"><font class="titleFont"><asp:Literal id="NSurveyAuthenticationTitle" runat="server" EnableViewState="False">NSurvey authentication</asp:Literal></font> 
          <br /><br /><br /> </td>
        </tr>
        <tr>
          <td>&nbsp;&nbsp; </td>
          <td>
          
          <table class="innerText">
          <tr>
          <td nowrap="nowrap" width="140"><strong><asp:Literal id="LoginLabel" runat="server" EnableViewState="False">Login :</asp:Literal></strong> </td>
          <td width="480"><asp:textbox id="LoginTextBox" runat="server"></asp:textbox></td>
          </tr>
          <tr>
          <td width="140"><strong><asp:Literal id="PasswordLabel" runat="server" EnableViewState="False">Password :</asp:Literal></strong> </td>
          <td><asp:textbox id="PasswordTextBox" runat="server" Width="154px" TextMode="Password"></asp:textbox></td>
          </tr>
          </table>
          <br />
          
                <asp:button id="ValidateCredentialsButton" runat="server" Text="Validate credentials"></asp:button></td>
                </tr>
                </table>
                
                </td>
                </tr>
                
</table>
</div></div></asp:Content>
