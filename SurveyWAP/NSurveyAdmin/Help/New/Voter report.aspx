<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Individual Responses</h2><hr style="color:#e2e2e2;" />             

The Individual Responses webpage shows a list of entries to access a report of an individual
respondents' answers. The report can also be used to edit/ correct any answers. 
                Both an regular respondent or any other users with a SP Useraccount if properly authorised (see below) can access and edit the answers  .<br />
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
<u>Respondent Edit Options</u>         
                <br /><br />In case of surveys that make use of the "SP User' Security Addin 
                Respondents can be given access to their individual respondent reports to review and/ or edit 
                their responses (answers submitted and saved to the database).
<br /><br />                To be able to access/ edit the individual report an SP User account (username/ password) is required (first to answer the survey, next to access the report).
                Proper authorisation (role and accessrights) is needed to give access only to the repondents voter report(s). 
<br /><br />Besides assigning the correct survey to the SP User account a role including the "Access User Responses" role right must be 
                assigned.
                <br />
<hr style="color:#e2e2e2;" /><h3>More Information</h3><br />
                
<a href="Voter%20Report%20Edit.aspx">Edit Individual Responses Report</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

