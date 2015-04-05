<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Introduction" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
<br />
<h2 style="color:#5720C6;">About the Survey™ Helpfiles</h2>
<br />
<br />
<hr style="color:#e2e2e2;"/><br /><br />

The Survey™ Project Helpfiles are written to assist users of all levels with using, creating and maintaining SP™ forms or questionnaires. 
From site visitors, to Survey Takers and Administrators through to Technicians and Developers, this manual provides basic instructions on how to set-up, create and maintain one or more surveys using the SP™ Webapplication.
<br /><br />
These helpfiles include step-by-step details on how to perform a task using clear and basic language. 
Some helpfiles may include one or more full color screen captures of the task being completed 
as well as tips for Administrators and Technicians on how to configure SP™ to enable the task. 

<br /><br />
The SP™ Helpfiles are written and updated by the Survey Project Community for the Survey&trade; Project webapplication. These helpfiles are a continuous work in progress. 
The content is freely available as an integral part of the webapplication. <br /><br />
Additional help and information can be found through: <br />
- Survey Project Community site <br />
- Survey Project Codeplex site <br />
- Survey Project YouTube Channel.<br />
<br />
<br />
                <hr style="color:#e2e2e2;" />
                <br /><h3>More Information</h3><br />
<a href="Survey%20Copyright%20and%20Disclaimer.aspx" title="Copyright & Disclaimer" >Copyright & Disclaimer</a><br />

            </td>
        </tr>
    </table>
</div></div></asp:Content>
