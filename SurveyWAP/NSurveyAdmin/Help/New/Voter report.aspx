<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Individual Responses</h2><hr style="color:#e2e2e2;" />
             

The Individual Responses webpage shows a list of individual reponses reports for each
respondents answers.<br />
<br />
Individual Reponses reports can be accessed from the following locations :<br />
<br />
* Menu Results/Individual Reponses - <a href="Voter%20Entries%20List.aspx">List of Entries</a><br />
<br />
* Menu Results/ Filemanager/ <a href="File%20Manager.aspx">Posted by</a>  columnlink<br />
<br />
* Menu Campaigns/<a href="Mailing Status.aspx">Mailing Status</a><br />
<br />
                Note: the resultsreport that can be shown to the respondent after submitting a survey is identical to the voterreport. See Completion settings - Redirection URL to make use of the resultsreport.
<br /><br />
<hr style="color:#e2e2e2;" /><h3>More Information</h3><br />
                <br />
<a href="Voter%20Report%20Edit.aspx">Edit Individual Responses Report</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

