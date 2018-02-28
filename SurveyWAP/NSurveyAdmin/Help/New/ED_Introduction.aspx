<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Email" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">Email Invitation Mailings</h2><hr style="color:#e2e2e2;" />
SP&trade; provides an email distribution interface that enables sending
email invitations to take part in a survey.<br />
<br />
Once the invitations are sent it is possible to track the status of those who were invited and/ or responded (pending/ validated), 
                 and to use the email code security addin to protect the survey against multiple submissions.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
            
                                <a href="Survey Mailing.aspx" title=" Survey Mailing " > Survey Mailing </a><br />
                <a href="Mailing Status.aspx" title=" Mailing Status " > Mailing Status </a><br />
                <a href="Mailing Log.aspx" title=" Mailing Log " > Mailing Log </a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

