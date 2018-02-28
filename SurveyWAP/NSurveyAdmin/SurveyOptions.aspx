<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveyOptions" Codebehind="SurveyOptions.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="SurveyOptionControl" Src="UserControls/SurveyOptionControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">
            <uc1:SurveyOptionControl ID="SurveyOption" runat="server"></uc1:SurveyOptionControl>
                            <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
        </div>

</asp:Content>

