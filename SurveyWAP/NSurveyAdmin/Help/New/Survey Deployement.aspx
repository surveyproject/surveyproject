<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Campaigns" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Survey Link Deployment</h2><hr style="color:#e2e2e2;" />
                <br />

The Campaigns/Web menu option provides a HTML link (Url) to the survey. This link can be distributed through any media from emails, messenger or on website pages.<br />
<br />
You can copy &amp; past the link provided and use it in almost any application that supports HTML links.<br />
<br />
The survey id is unique and it is not possible for the respondent to alter the link in order to find out what other surveys were created unless they know the exact ID of other surveys as well.<br />
<br />Instead of making use of the unique ID as part of the URL it's possible to create a Friedly Name URL. 
                If a friendly name is entered and saved the new URL will be shown on the page. This URL can be used also to publish the survey.<br /><br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Survey%20Popup%20Window.html<br />
SD_Introduction.html<br />
Take%20Survey.html<br />
Web%20Control%20Deployment.html<br />
ED_Introduction.html<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

