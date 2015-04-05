<%@ Page language="c#" MasterPageFile="~/Wap.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.CreateSurvey" Codebehind="CreateSurvey.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="SurveyOptionControl" Src="UserControls/SurveyOptionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <uc1:SurveyOptionControl ID="SurveyOption" runat="server"></uc1:SurveyOptionControl>
            </td>
        </tr>
    </table>
</div></div></asp:Content>
