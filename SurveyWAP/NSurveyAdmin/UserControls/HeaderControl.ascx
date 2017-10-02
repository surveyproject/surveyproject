<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.HeaderControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="HeaderControl.ascx.cs" %>


<div class="headerCell">
    <asp:Label Text="" CssClass="loginDiv icon-user" Visible="false" runat="server" ID="MenuUserName" />


<div class="logoutDiv">
    <asp:LinkButton Text="" runat="server" ID="LogoutButton" OnCommand="LogoutButton_Click"
        Visible="false" CssClass="logouthyperlink icon-signout" ToolTip="" />
</div>
</div>
        <div class="mnuDiv">

            <asp:Menu ID="mnuMain"  RenderingMode="List" CssClass="mnuMain" StaticDisplayLevels="1"
                Orientation="Horizontal" 
                runat="server"

                 DynamicSelectedStyle-ForeColor ="YellowGreen"
                 StaticSelectedStyle-ForeColor ="YellowGreen"

                DynamicHorizontalOffset="2" 
                DynamicVerticalOffset="1" 
                StaticPopOutImageUrl="~/Images/arrow-right.gif" 
                DynamicPopOutImageUrl="~/Images/arrow-right.gif" >

                <StaticMenuStyle CssClass="mnuMain" /> 
                <StaticMenuItemStyle CssClass="mnuMain" />
                <StaticSelectedStyle CssClass="selected" />
                <StaticHoverStyle CssClass="mnuMain" />

		<DynamicMenuStyle CssClass="mnuMain" /> 
		<DynamicMenuItemStyle CssClass="mnuMain" /> 
		<DynamicSelectedStyle CssClass="selected" /> 
		<DynamicHoverStyle CssClass="mnuMain" /> 
                
            </asp:Menu>

        </div>
       