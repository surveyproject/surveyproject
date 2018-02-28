<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                	Email Code Protection</h2><hr style="color:#e2e2e2;" />
    

This security addin protects the survey using the 'email invitation
code' provided to the respondent when receiving the Email Invitation message through the use of the Campaigns<a href="ED_Introduction.aspx" title=" Mailings Introduction " > Mailings </a> menu.<br />
<br />
<i>* Only Invited Emails Can Participate</i> - (checked by default)
                <br />option to restrict access to respondents invited by the Campaigns/Mailing feature (by survey URL and invitationcode) only or to                 
                open the survey to respondents who did not receive any invitation code as well. <br /><br />
                
                If this option is activated (checked by default) respondents with the invitation code will only be able to submit their answers once.
                <br /><br />
                Respondents who are invited by way of the SP&trade; Mailing feature without(!) adding and activating the Email Security addin will have (direct)
                access to the survey through the survey URL without the need for the invitation code. <br /><br />
                However there will be no status registration on the Mailing Status webpage (pending and validated emails). To activate this feature 
                the email security addin must be added to the survey.
                
                <hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
                <a href="ED_Introduction.aspx" title=" Mailings Introduction " > Mailings Introduction </a>
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

