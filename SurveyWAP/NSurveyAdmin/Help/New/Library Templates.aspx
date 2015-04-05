<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionLibrary" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Library Templates</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The library templates allows to add new questions to a library that
will be available as templates to be re-used from the Question editor - 
Question%20Editor.html.<br />
<br />

* Select Preview Language - set library language to add questions to
<br />
<br />
* Insert New / Existing Question - option to add a new question to the
  library either by creating a new one or by copying an existing one
  from a survey, form another library or import it from an XML file.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Question%20Editor.html <br />
QL_Introduction.html<br />
Library%20Templates.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

