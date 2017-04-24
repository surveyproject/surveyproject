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
            <td class="contentCell help" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Survey Layout &amp; Style</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
                Survey&trade; Project offers different features to change the look &amp; feel of the
                surveys. (The layout and design of results reports is fixed and cannot be changed by regular
                endusers. Separate tools can be used for this.)
                <br />
                <br />
                The survey layout and design can be changed primarily through the Designer/Layout menu option. This is where customised headers and footers can 
                be added to the surveywebpage as presented to survey takers. Header and footer are created through easy to use WYSIWYG editors.
                <br /><br />
                Throught the CSS upload/ download and edit options customised CSS files can be
                created and edited to determine the graphical design (look & feel and layout) of the different survey components and webpage. 
                Once uploaded the customised CSS files are linked to the survey selected in the treelist.    
                <br /><br />
                The default (minified) CSS file linked to the survey component and the surveymobile.aspx webpage can be found 
                in the NsurveyAdmin/CSS directory: surveymobile.min.css. Click the next link for instructions
                on using the <a class="help" href="Web%20Control%20Style.aspx" target="_self">Web Control
                    CSS Style</a> attributes.
                <br />
                <br />
                To get an idea of how the different CSS tags are used on the survey webcontrol the Campaigns/
                Web menuoption presents an overview of the webcontrol code including the CSS tags. Any changes
                to the code on this page will have no effect on the Survey. The code is for illustration purposes
                only.
                <br />
                <br />
                In addition to the default CSS tags used on the surveycontrol, the look and feel of multiple other individual
                 elements on the surveyform webpage can be determined through the Designer/ Layout/ CSSXML list. This is were CSS class names and selectors
                can be linked to different elements of the surveyform webpage (besides the ones mentioned at the surveycontrol).
                <br />
                <br />
                A special option is available to determine the CSS class used on Selection Answertypes. Besides making use of the CSS/XML list its possibe to set the CSS on the DIV tag surrounding 
                individual Selection Answertypes through the Answer Editing form. When a Selection Answertype is chosen from the Type DropDownList the Selection
                Answer CSS option is shown to enter a CSS Class/ selector as defined in the CSS files. If the Selection Answer CSS textbox is left empty but the Alias textbox is filled with the name of any existing CSS class the particular CSS class/ selector will be applied to the Answer DIV.
                <br />
                <br />
                The default version of the survey webcontrol as used to present surveys to respondents can
                be found in the rootdirectory of the site, filename: SurveyMobile.aspx. A legacy version is called: survey.aspx.
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
                <a href="CssXml.aspx" target="_self">Css XML List</a><br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>
