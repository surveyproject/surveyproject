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
                    Report Filter Creation</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

Report filters will allow us to setup a set of rules based on our
survey's questions and answers. Once we have created a filter and added
rules to it, we can assign it to a Report%20General%20Settings.html or a
RI_Introduction.html . Once a filter as been applied all results
calculation will be done accordingly to the filter that has been set up.<br />
<br />
* Filter Name is the name of the filter that will contain the rules.<br />
<br />
* Conditional Rule Operator is the operator that Survey will use to
  evaluate the rules inside the filter. At this time only a single
  conditional operator (AND, OR) can be applied to a group of rules
  inside a filter.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
ED_Introduction.html<br />
Survey%20Mailing.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

