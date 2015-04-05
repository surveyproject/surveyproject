<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.HelpFiles" Codebehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
            <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyManagement" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
      <tr>
        <td class="contentCell" valign="top">
        <br />
        <h2 style="color:#5720C6;">Survey Settings</h2><br />
        <br />
        <hr style="color:#e2e2e2;" />
        <br />
        The Settings options determine the working of the survey&#39;s general behaviour:<br />
        <br />

        * <strong>Title</strong><br />The title is used as logical information to distinguish one 
        survey from the other in the administration tool. It is not used or shown on the
        survey control as seen by the survey respondents. The default mailmessage to invite survey takers does show the title initially. It can be edited at will.<br />
        <br />

        * <strong>Opening Date</strong><br />This is the date as of which the survey will accept new
        entries. No entries will be accepted before that date and a message is shown when someone tries to acces the survey before the opening date. Leave blank if the survey
        has no particular opening date.<br />
        <br />

        *<strong> Closing Date</strong><br />This is the date at which the survey will close and not
        accept any more entries. No entries will be accepted after that date and a message is shown when someone tries to acces the survey after the closing date. 
        Leave blank if the survey has no deadline or a particular closing date.<br />
        <br />

        * <strong>Footer Progress Display</strong><br />This option determines what will be shown in the
        survey control footer to indicate progress while answering the survey. The choices are to show nothing or to show
        specific information on the current progress by selecting page numbers or percentage
        progress.<br />
        <br />

        * <strong>Disable Questions Numbering</strong> <br />Check this option if the current question number on the left 
        of each question in the survey control should not be shown.<br />
        <br />

        * <strong>Active</strong><br />With this option the survey will be activated so respondents will be able to access and take the survey. 
        A message is shown to survey takers when the survey is accessed while it is not activated. The Active option always has to be set before 
        publishing a survey. Otherwise access will not be possible.        
        <br />
        <br />

        * <strong>Default Survey</strong><br />This option can only be set by administrators. It allows to set a default survey that will be automatically be selected
        if no other survey is selected from the survey treelist directory. If a default survey is set and no survey is selected from the treelist directory on selecting a menuoption (e.g. formbuilder)
        that would otherwise require a selected survey from the treelist directory now the default survey will be opened and no warning message is shown.<br />
        <br />

        * <strong>Scored</strong> <br />This option has to be enabled in order to be able to specify a score for each
        answer on answerlevel and then see the scores for each respondent in the results. More information at: <a href="Score_Introduction.aspx"
            target="_self" title="Score Introduction">Score Introduction</a>.<br />
        <br />

        *<strong> Previous / Next Page Navigation </strong><br />This option will allow the user to navigate
        back and forth in the survey's pages while answering questions. By default
        the user can only move forward in the survey and can not go back to
        change answers.<br />
        <br />

        * <b>Resume Of Progress</b> <br />This option will allow a user to save current answers and resume the
        survey at a later moment by providing a unique code. <br /><br />
        
        There are two options to save the unique code provided by the Key Provider
        <!-- <a href="KP_Introduction.html" title="Key Provider">Key Provider</a>-->. The first option is a Cookie. 
        When the respondent returns to the survey it will not be necessary to enter any code manually. The drawback of this option is that 
        the survey must be resumed on the same computer as where the cookie is saved.
        <br />
        <br />
        Resume can also be set to Manually, in which case a unique code
        will be provided to the survey taker as a text message on screen. The code has to be written down or saved. The survey taker will have the option to
        resume at any time and from anywhere by clicking the resume progress button and entering the code.<br />
        <br />

        * <b>Delete Button</b> <br />This option will delete an entire survey and all questions, answers and
        respondent answers that are part of it. It is not possible to recover a survey once it has
        been deleted.<br />
        <br />

        * <b>Clone Button</b> <br />This option will create a complete copy of the survey, it's settings and content (questions, answertypes etc.).
        The respondent answers are not copied to the cloned survey.<br />
        <br />

        * <b>Xml Export Button</b> <br />This option will export the survey in XML in order to (re-)import it at a later time or on another Survey installation. For example a series of surveys could be created on a test server, exported and re-imported on a production
        server through the XML Export and Import option (see New Survey).<br />
        <br /><br />
        <hr style="color:#e2e2e2;" />
        <br />
        <br />
        <h3>More Information</h3><br />
 <!--       <a href="Style%20Editor.html">Style Editor</a><br /> -->
        <a href="Score_Introduction.aspx">Score Introduction</a><br />
        <a href="Completion%20Actions.aspx">Completion Actions</a><br />
 <!--       <a href="KP_Introduction.html">Key Provider</a><br /> -->
        <br />
        </td>
        </tr>
    </table>
</div></div></asp:Content>

