<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Individual Responses Report Edit</h2><hr style="color:#e2e2e2;" />
             
                The Individual Responses report shows the report for each individual respondents answers in detail.
The reports <u>edit mode</u> shows an individual respondent answers with the option to change / edit the answers that were posted.

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

                <hr style="color:#e2e2e2;" /><h3>More Information</h3>
                <br />
                <a href="Voter%20Report.aspx">Individual Responses Report</a><br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

