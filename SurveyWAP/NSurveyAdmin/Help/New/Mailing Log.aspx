<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Email" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Mailing Error Log</h2><hr style="color:#e2e2e2;" />
              

This page logs and displays any internal technical or connections errors that
might have occured while sending out the emails. It can be used to check and correct any failed mailattempts.<hr style="color:#e2e2e2;" />

                <h3>
                    More Information</h3>
                <br />
                                <a href="ED_Introduction.aspx" title=" Survey Mailing " > Mailing Introduction </a><br />
                <a href="Mailing Status.aspx" title=" Mailing Status " > Mailing Status </a><br />
                <a href="Survey Mailing.aspx" title=" Survey Mailing " > Survey Mailing </a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

