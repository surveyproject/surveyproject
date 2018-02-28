<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Email" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Invitation Mailing</h2><hr style="color:#e2e2e2;" />


Use the invitation mailing feature to send invitations to respondents
to participate in answering a survey.<br />
<br />
<i>* Email Invitation List</i> <br /> a comma separated list of emails to which
  the invitations are sent.<br />
<br />
<i>* From Email </i><br /> the email from which the invitation will originate.<br />
<br />
<i>* From Name</i> <br /> the name of the person/ organisation sending the invitation.<br />
<br />
<i>* Invitation Message </i><br /> a [default] invitation message that can be edited/ customized with the Html/text (WYSYWIG) editor. Note:
                the default text of the message can be found in the XmlData/Languages/[language].xml files.                
                <br /><br />A special (optional)
  tag that will be replaced on sending the invitation can be included
  in the invitation:<br />
<br />
 <i>* [--invitationid-] </i><br /> this tag will be replaced by a unique identifier (access code) that
  will be used by the <a href="Email Code Protection.aspx">Email Code Protection security addin</a>  to
  uniquely identify a user to avoid multiple submission. Note that the email code protection - 
   must be activated in order to avoid multiple submissions.<br />
<br />

<i>* Anonymous Entries Survey</i> <br /> if checked SP&trade; will not register a link between the email and the
respondent answers.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
                                <a href="ED_Introduction.aspx" title=" Survey Mailing " > Mailing Introduction </a><br />
                <a href="Mailing Status.aspx" title=" Mailing Status " > Mailing Status </a><br />
                <a href="Mailing Log.aspx" title=" Mailing Log " > Mailing Log </a><br />
                <br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

