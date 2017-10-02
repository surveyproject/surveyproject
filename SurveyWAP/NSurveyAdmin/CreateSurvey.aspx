<%@ Page language="c#" MasterPageFile="~/Wap.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.CreateSurvey" Codebehind="CreateSurvey.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="SurveyOptionControl" Src="UserControls/SurveyOptionControl.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!-- note: page never used!? -->

        <div id="Panel" class="Panel">
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
</div></asp:Content>
