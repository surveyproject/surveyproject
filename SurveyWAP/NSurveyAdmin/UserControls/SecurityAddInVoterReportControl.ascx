<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.SecurityAddInVoterReportControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="SecurityAddInVoterReportControl.ascx.cs" %>

<table class="innerText" id="Table1">
    <tr>
        <td colspan="2">
            <div class="titleFont">
                <asp:Literal ID="AddInDescriptionTitle" runat="server" EnableViewState="False"></asp:Literal></div>
            <br />
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;&nbsp;
        </td>
        <td>
            <asp:PlaceHolder ID="VoterDetailsPlaceHolder" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
</table>
