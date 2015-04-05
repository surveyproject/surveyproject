<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Group Manager</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This page will let use create or edit our groups in the system. Depending
on the user provider we might not be able to edit the group but only
assign roles and surveys to it.<br />
<br />
* Group Name is the name of the group. <br />
<br />
* Members Are Administrator makes all members administrators.<br />
<br />
* Assign All Surveys To Group  members will be able to access all
  surveys. Note that if he is not administrator the roles will apply for
  what he can do or not do on the surveys.<br />
<br />
* Assigned Users allows us to assign users to the group.<br />
<br />
* Assigned To Group  allows us assign surveys to the group.<br />
<br />
* Roles  allows us to assign roles / rights to the group.<br />
<br />

<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
UM_Introduction.html<br />
User%20Manager.html<br />
User%20Creation%20Tool.html<br />
Roles%20Manager.html<br />
User%20Import.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

