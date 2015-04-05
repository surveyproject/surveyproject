<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Appendix" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Form Architecture</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Survey Project has a flexible and modular architecture based on answer types and
question types and a survey container.<br />
<br />
The main survey control is a container that can contains any question
type either one of the provided question types or new and customised question types can be created using the Survey Project source code. 
These questions items contain answer items. As with the question item,  answer items can be either one
of the provided answer item or one that can be created additionally throught the sourcecode.<br />
<br />
Each answer item provides a set of events that the question item can use
in order to get notified when new answers have been posted and in return
the question item will aggregate those results and send them back to the
Survey container that will act accordigly to what it receives.<br />
<br />
More technical information, documentation and the Survey Project source code can be found at the Survey Project Codeplex site:
                <br />
                http://survey.codeplex.com 
                <br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

