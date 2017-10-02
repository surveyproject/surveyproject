<%@ Page language="c#" MasterPageFile="~/Wap.master"   AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.MailingPoll" Codebehind="MailingPoll.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="UserControls/SurveyListControl.ascx" %>

<%@ Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls.UI" Assembly="SurveyProject.WebControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 775px; 
                background-color: #ffffff; 
                height:750px; 
                vertical-align:top;
                border: 1px #aaaaaa solid ;              
               -webkit-border-radius: 7px;
               -moz-border-radius: 7px;
                border-radius: 7px;
                padding:10px;
                ">
        <br />
                        <fieldset>
                            <legend class="titleFont titleLegend">
                        <asp:Literal ID="PleaseWaitInfo" runat="server" EnableViewState="False">.... Please wait system is sending survey invitations ....</asp:Literal>
                    </legend>
                    <br />
<ol>
     <li>
                                    <vts:ResultsBar ID="ProgressBar" runat="server" />
                                    <asp:Label ID="FailedSendingLabel" runat="server" />
                                    <asp:Label ID="AllInvitationsSendMessage" runat="server" />

                    <!--
                    <iframe src="MailingPollStatus.aspx" frameborder="0" width="400" scrolling="no" height="50">
                        ; [Your user agent does not support frames or is currently configured not to display
                        frames.</iframe>
-->
          </li><li>
                        <asp:Literal ID="ProcessInfoLabel" runat="server" EnableViewState="False">This process can take time depending on the load and network conditions</asp:Literal>
                          </li>
  </ol>
                    <br />
                    </fieldset>
</div>
        </asp:Content>
