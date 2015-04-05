<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Page language="c#" MasterPageFile="~/Wap.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserImport" Codebehind="UserImport.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UsersOptionsControl" Src="UserControls/UsersOptionsControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
<table class="TableLayoutContainer">
<tr>
	<td></td>
</tr>
<tr>
<td class="contentCell"  valign="top">

<table class="uiTable" width="600" border="0"><tr><td>
            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
</td>
</tr>
</table>

<table class="smallText" width="600">
  <tr>
    <td colspan="2"><font class="titleFont"><asp:label id="ImportUsersTitle" runat="server">Import users</asp:label></font>
    <br />&nbsp; </td></tr>
  <tr>
    <td>&nbsp;&nbsp; </td>
    <td>

      <table class="smallText">
        
		 <tr>
          <td width="160" valign="top"><strong><asp:literal id="ImportUserLabel" 
             runat="server" EnableViewState="False" 
            Text="Import users :"></asp:literal></strong></td>
          <td width="440">
                  <p><asp:textbox id="ImportUsersTextBox" runat="server" TextMode="MultiLine" Rows="10" Columns="65" Wrap="False"></asp:textbox><br />
                  <asp:Label id="ImportInfo1Label" runat="server" EnableViewState="False"> Import format are comma separated values, each new line must contain a user to import eg :</asp:Label>
                  <br /><asp:Label id="ImportInfo2Label" runat="server" EnableViewState="False"> UserName,Password,First name,Last name,Email </asp:Label>
                  <br />
                  <asp:Label id="ImportInfo3Label" runat="server" EnableViewState="False">or UserName,Password,,, </asp:Label></p>
           </td>
              </tr>
              <tr>
          <td width="160"><strong><asp:literal id="UserIsAdministratorLabel" 
             runat="server" EnableViewState="False" 
            Text="Administrator :"></asp:literal></strong></td>
          <td><asp:checkbox id="IsAdminCheckBox" runat="server"></asp:checkbox></td>
          </tr>
        <tr>
          <td width="160"><strong><asp:literal id="AssignAllSurveysLabel" 
             runat="server" EnableViewState="False" 
            Text="Assign all survey :"></asp:literal></strong></td>
          <td><asp:checkbox id="HasSurveyAccessCheckBox" runat="server" AutoPostBack="True"></asp:checkbox></td>
          </tr>
              <tr>
                <td valign="top" nowrap="nowrap" width="160"><strong><asp:literal id="UserSurveyAssignedLabel" runat="server" Text="Assigned surveys :" EnableViewState="False"></asp:literal></strong></td>
                <td>
                
                  <table class="smallText">
                    <tr>
                      <td><asp:literal id="UnAssignedSurveysLabel" runat="server" Text="Available surveys" EnableViewState="False"></asp:literal></td>
                      <td></td>
                      <td><asp:literal id="AssignedSurveysLabel" runat="server" Text="Assigned to user" EnableViewState="False"></asp:literal></td>
                    </tr>
                    <tr>
                      <td><asp:listbox id="SurveysListBox" runat="server" AutoPostBack="True" Height="120px"></asp:listbox></td>
                      <td>&nbsp;<strong>&gt;&gt;<br />&lt;&lt;</strong>&nbsp;</td>
                      <td valign="top"><asp:listbox id="UserSurveysListBox" runat="server" AutoPostBack="True" Height="120px"></asp:listbox></td>
                      </tr>
                    </table>
                    
                </td>
              </tr>
              <tr>
                <td valign="top" width="160"><strong><asp:literal id="RolesLabel" runat="server" Text="Roles :" EnableViewState="False"></asp:literal></strong></td>
                <td>
                
                  <table class="smallText">
                    <tr>
                      <td><asp:literal id="AvailableRolesLabel" runat="server" Text="Available roles" EnableViewState="False"></asp:literal></td>
                      <td width="23"></td>
                      <td><asp:literal id="UserRolesLabel" runat="server" Text="User's roles" EnableViewState="False"></asp:literal></td></tr>
                    <tr>
                      <td><asp:listbox id="RolesListBox" runat="server" AutoPostBack="True" Height="120px"></asp:listbox></td>
                      <td width="23">&nbsp;<strong>&gt;&gt;<br />&lt;&lt;</strong>&nbsp;</td>
                      <td valign="top">
                      <asp:listbox id="UserRolesListBox" runat="server" AutoPostBack="True" Height="120px"></asp:listbox>
                   </td></tr></table>
                   
               </td></tr></table>
               
            </td></tr></table>
                      
                      <asp:button id="ImportUsersButton" runat="server" Text="Import users"></asp:button>
                      </td></tr>
</table>
</div></div></asp:Content>
