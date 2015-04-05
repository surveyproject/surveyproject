<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.LibraryQuestionOptionsControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="LibraryQuestionOptionsControl.ascx.cs" %>

<table width="100%" class="questionOptions">
    <tr>
        <td style="text-align:left;">
            <asp:ImageButton runat="server" ID="btnQuestionUP" Width="16" ImageUrl="~/Images/arrow-green-up.gif" />&nbsp;|&nbsp;
            <asp:ImageButton runat="server" ID="btnQuestionDown" Width="16" ImageUrl="~/Images/arrow-red-down.gif" />&nbsp;|&nbsp;
            <asp:HyperLink ID="EditHyperLink" runat="server">Edit</asp:HyperLink>&nbsp;|&nbsp;
            <asp:HyperLink ID="EditAnswersHyperLink" runat="server">Edit answers</asp:HyperLink>&nbsp;|&nbsp;
            <asp:LinkButton ID="DeleteButton" runat="server">Delete</asp:LinkButton>&nbsp;|&nbsp;
            <asp:LinkButton ID="ExportButton" runat="server">Export XML</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td style="text-align:left;">
            QuestionID:<asp:Label runat="server" ID="lblQuestionId" /> &nbsp;|&nbsp; 
            HelpText:<asp:Label runat="server" ID="lblQuestionHelpText" />
        </td>
    </tr>
</table>
