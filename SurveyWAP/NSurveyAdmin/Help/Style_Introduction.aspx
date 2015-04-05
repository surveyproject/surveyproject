<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyLayout" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Survey Layout &amp; Style</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
                Survey&trade; contains different features to change the look &amp; feel of the
                surveys. The layout and design of reports is fixed and cannot be changed by regular
                endusers. Separate tools can be used for this.
                <br />
                <br />
                The Survey layout and design can be changed through the Layout Menu option. This is where customised headers and footers can 
                be added to the survey webpage as presented to survey takers. Header and footer are created through easy to use WYSIWYG editors.
                <br /><br />
                Throught the CSS upload/ download and edit options customised CSS files can be
                created and edited to determine the graphical design (look & feel and layout) of the survey component and webpage. 
                Customised CSS files are linked to a particular survey.    
                <br /><br />
                The default CSS file linked to the survey component and the survey.aspx webpage can be found 
                in the NsurveyAdmin/CSS directory: nsurveyform.css. Click the next link for instructions
                on using the <a href="Web%20Control%20Style.aspx" target="_self">Web Control
                    CSS Style</a> attributes.
                <br />
                <br />
                To get an idea of how the different CSS tags are used on the survey webcontrol the Campaigns/
                Web menuoption presents an overview of the webcontrol code including the CSS tags. Any changes
                to the code on this page will have no effect on the Survey. The code is for illustration purposes
                only.
                <br />
                <br />
                The default version of the survey webcontrol as used to present surveys to respondents can
                be found in the rootdirectory of the site, filename: survey.aspx.
                <br />
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <a href="Web%20Control%20Style.aspx" target="_self">Web Control CSS Style</a><br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
