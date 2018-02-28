<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Email" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Mailing Status</h2><hr style="color:#e2e2e2;" />
    
The mailing status page presents an overview of all invitations sent by mail and their status.<br />
<br />
<i>* Pending Emails</i> <br />- shows a list of emails that have been invited but
  didn't take the survey yet. It is not possible to send a
  reminder. In order to send a reminder email delete the entry from the
  pending list and re-send an invitation through the  <a href="Survey Mailing.aspx" title=" Survey Mailing " >Mailing </a> menu.<br />
<br />
<i>* Validated Emails </i><br />- shows the list of emails that have responded and answered/ submitted 
  the survey. When clicking on the details button the Individual Reponses report is shown displaying all answers to the survey.<br /><hr style="color:#e2e2e2;" />

                <h3>
                    More Information</h3>
                <br />
                <a href="ED_Introduction.aspx" title=" Survey Mailing " > Mailing Introduction </a><br />
                <a href="Mailing Log.aspx" title=" Mailing Log " > Mailing Log </a><br />
                <a href="Survey Mailing.aspx" title=" Survey Mailing " > Survey Mailing </a><br />
                <br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

