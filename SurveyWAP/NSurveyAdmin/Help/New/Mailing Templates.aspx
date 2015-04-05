<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Email" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Mailing Templates</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
It's very easy to create new mailing templates that we will be able to
reuse across all the surveys.<br />
<br />
* Add New Template this will change the send mode in Add mode, once
  we've create our invitiation as we want it to be we can click on the
  Add button to add the new template.<br />
<br />
* Edit Template to edit / delete a template we need to select the
  template we want to edit from the dropdown list and click on the Edit
  Template link.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
ED_Introduction.html<br />
Survey%20Mailing.html<br />
Mailing%20Status.html<br />
Mailing%20Log.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

