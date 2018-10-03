<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Campaigns" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                   SurveyBox Custom Webcontrol</h2><hr style="color:#e2e2e2;" />

At the heart of the Survey Project webapplication and SP&trade; surveys is the SurveyBox Custom Webcontrol. It is the (web) control through
                 which all different parts of a 'survey' [questions, answers, messages, design etc.] are presented and exposed on a webpage to respondents.
                <br /> <br />
               
The default webpage in SP to present and publish surveys to the 'outside world' is the <code>SurveyMobile.aspx</code> webpage that can be found in the 
                root directory of the application. The SurveyBox webcontrol is at the center of this page.<br /><br />

                 <u>Multiple Survey Webpages</u><br /><br />
                There may be reasons to create multiple/ different (customized) webpages to present surveys besides the default
                surveymobile.aspx webpage. In order to do so a new (.aspx) webpage could easily be created or added to the SP webapplication.
                     <br /> <br />
                Next the surveybox webcontrol can be added following below instructions.
                An unlimited number of pages can be created that can each hold an unlimited number of surveybox webcontrols if needed (e.g. showing 
                multiple surveys on one page).

<br /><br />
                   <u>Adding the SurveyBox webcontrol</u><br /><br />
In order to add the surveybox webcontrol as shown on
the Weblinks page [Campaigns - Weblinks] to another [.aspx] webpage follow these
steps :
<br />
<br />
1. Open the new [.aspx] webpage that should hold the surveybox webcontrol in a (text) editor e.g. Notepad
<br />
<br />
2. Add the following code on the next line after the <code> &lt;%@Page ... %&gt; </code>page
   directive :<br /><br />
                <code>
   &lt;%@ Register TagPrefix=&quot;vts&quot; Namespace=&quot;Votations.Survey.WebControls&quot;
   Assembly=&quot;SurveyProject.WebControls&quot; %&gt;</code><br />
<br />
3. Once this piece of code is added cut &amp;
   past the relevant pieces of code provided in the textbox on the Weblink menupage and place it
    on the new page.<br />
<br />
4. The SurveyID of the SurveyBox control determines which survey is shown and presented to the respondent on opening the page in a browser.
                The SurveyID's can be found in the surveylist (menu SurveyList).
<br /><br />
5. Add the bootstrap script at the bottom of the page (just before the body closing tag)
                <br />
                    &lt;script src="&lt;%=Page.ResolveUrl("~/Scripts/bootstrap.min.js")%&gt;"&gt;&lt;/script&gt;
<br /><br />
                TIP: In case of doubt check the SurveyMobile.aspx webpage in the SP&trade; rootdirectory as a working example.
                <br /><br />
<u>Adding the SurveyBox webcontrol to another webapplication</u><br /><br />
                It is possible to implement the SurveyBox webcontrol in an entirely different (.NET) webapplication. As an example a Guide
                is written explaining how to use and integrate the control in an MVC webapplication.
                It can be downloaded from surveyproject.org or by clicking: 
                <a href="http://www.surveyproject.org/LinkClick.aspx?fileticket=B3xkzKHTuEA%3d&tabid=300&portalid=1&mid=802&forcedownload=true" target="_blank" title="Survey Webform and MVC Integration">"Survey Webform and MVC Integration"</a>

                <br /><br />
                To get a first impression of what is needed if the webcontrol is to become part of a separate web application
                from the original Survey&trade; Project installation also copy:
                <br /><br />
               o the DLL's from the SP&trade; web
   application Bin directory to the Bin directory of the site that holds the page containing the survey control.<br />
                o the &lt;nsurveysettings&gt; section from the web.config file
                <br />
                o the SP&trade; database connection section from the web.config file
                <br />
                o the XmlData directory and files<br />
                o the Scripts directory and files<br />
                o the CSS directory and files<br />
                <br />Note: more files and directories may have to be copied accross.
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
            
                <a href="Survey Deployement.aspx" title=" Survey Link Deployment " > Survey Link Deployment </a>	<br />
                <a href="Take Survey.aspx" title=" Take Survey " > Take Survey </a>	<br />
                <a href="SD_Introduction.aspx" title=" Survey Publishing & Campaigns " > Survey Publishing & Campaigns </a>	<br />
                <a href="ED_Introduction.aspx" title=" Emailing Introduction " > Emailing Introduction </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

