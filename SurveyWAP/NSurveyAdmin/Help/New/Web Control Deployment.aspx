<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Campaigns" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Web Control Deployment</h2><hr style="color:#e2e2e2;" />

This information is intended for developers and administrators who want to customize
the survey webcontrol and integrate it .aspx pages of another site.
Note: its possible to integrate as many survey controls on the same page as wanted.<br />
<br />
In order to add the survey webcontrol code that is provided on
the deployment page [Campaigns - Web] to another .aspx page follow these
steps :
<br />
<br />
1. Open the .aspx page that should hold the survey web control
   in a text editor like Notepad
<br />
<br />
2. Add the following code on the next line after the <code> &lt;%@Page ... %&gt; </code>page
   directive :<br /><br />
                <code>
   &lt;%@ Register TagPrefix=&quot;vts&quot; Namespace=&quot;Votations.Survey.WebControls&quot;
   Assembly=&quot;SurveyProject.WebControls&quot; %&gt;</code><br />
<br />
3. Once this piece of code is added all that is needed is to cut &amp;
   past the code provided in the textbox on the Web menupage and place it
   anywhere on the page as long as it is after a <code> &lt;form&gt;</code> tag.<br />
<br />
4. lf the page is part of a separate web application from the original
   Survey&trade; Project installation also copy 
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
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
Survey%20Popup%20Window.html<br />
SD_Introduction.html<br />
Survey%20Deployement.html<br />
ED_Introduction.html<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

