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
                    Style Builder</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
The style builder is a WYSIWG style editor that will allow us to create 
style templates for our surveys and public reports. Once we have created
a new style we can assign it to our survey using the style properties of
the Survey%20General%20Settings.html.<br />
<br />
Style Settings<br />
<br />
* Style Name is the name of the style.<br />
<br />
* Public Style is the style shared across users in a multi-user
  configuration of Survey. The creator of a style is the only one along
  with the system administrator who can change it once it has been
  created.<br />
<br />
* Clone creates a copy of the style template.<br />
<br />
* Export Xml  will export an Xml file representing the style. This Xml
  file can be imported afterward on any Survey installation when creating
  a new style.<br />
<br />
Items Styles<br />
The items style editor let us create and preview our style on the
different items that composed the survey box control.<br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
ED_Introduction.html<br />
Web%20Control%20Style.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

