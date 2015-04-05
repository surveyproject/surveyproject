<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyOptionControl" Src="UserControls/SurveyOptionControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveyOptions" Codebehind="SurveyOptions.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <uc1:SurveyOptionControl ID="SurveyOption" runat="server"></uc1:SurveyOptionControl>
        </div>

    </div>
</asp:Content>

