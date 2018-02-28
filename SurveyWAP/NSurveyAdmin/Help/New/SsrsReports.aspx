<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    SSRS Reports</h2><hr style="color:#e2e2e2;" />             

The Sql Server Reporting Services (SSRS) webpage shows a list of reports created through SSRS that can be openened and accessed by clicking the Details button (looking glass icon) 
                in the first column.<br />
<br />
SSRS reports are 'custom made' reports that can be added to the SP webapplication to show/ present any SP or surveys related information or results.<br />
<br />
                SSRS reports are created by making use of the Sql Server Reporting Services as provided by Microsoft. More information and instructions on 
                the installation and use of SSRS can be found at:
                <br /><br />
               <a href="https://docs.microsoft.com/en-us/sql/reporting-services/create-deploy-and-manage-mobile-and-paginated-reports" target="_blank">
                https://docs.microsoft.com/en-us/sql/reporting-services/create-deploy-and-manage-mobile-and-paginated-reports </a> 
                                <br /><br />

 <u>Adding SSRS Reports</u><br /><br />
                Once a SSRS Report is created through SSRS it can be added to the SP webapplication by copying accross the .rdl file from the SSRS solution
                into the NSurveyReports directory of the SP installation. 
                <br /><br />
                Detailed instructions will be published at http://www.surveyproject.org - menu Support/ Helpfiles/ 
                <a href="http://www.surveyproject.org/Support/Helpfiles/GuidesManuals/tabid/300/Default.aspx" target="_blank">Guides & Manuals</a>
                <br />        
               
<hr style="color:#e2e2e2;" /><h3>More Information</h3><br />
                
<a href="Voter%20Report%20Edit.aspx">Edit Individual Responses Report</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

