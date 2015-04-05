<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Campaigns" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Web Control Deployment</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This information is intended for technical users and administrators who like to customize
the survey ASP.net control and integrate it inside the ASP.net pages of another site.
Note that its possible to integrate as many survey controls on the same page as wanted.<br />
<br />
In order to integrate the ASP.net web control code that is provided on
the deployment page [Campaigns - Web] inside an ASP.net page follow these
steps :
<br />
<br />
1. Open the ASP.net page that should hold the survey web control
   in a text editor like notepad
<br />
<br />
2. Add the following code on the next line after the &lt;%@Page ... %&gt; page
   directive :<br />
   &lt;%@ Register TagPrefix=&quot;vts&quot; Namespace=&quot;Votations.Survey.WebControls&quot;
   Assembly=&quot;Votations.Survey.WebControls&quot; %&gt;<br />
<br />
3. Once this piece of code is added all that is needed is to cut &amp;
   past the code provided in the Survey asp.net code section and place it
   anywhere on the page as long as it is after a &lt;form&gt; tag.<br />
<br />
4. lf the page is in a separate web application than the original
   Survey Project installation also copy the DLL's from the Survey Project web
   application Bin directory to the Bin directory of the site that holds the page that
   contains the ASP.net control.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Popup%20Window.html<br />
SD_Introduction.html<br />
Survey%20Deployement.html<br />
ED_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

