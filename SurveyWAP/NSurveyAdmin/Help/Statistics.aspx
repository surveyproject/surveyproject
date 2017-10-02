<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyManagement" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Survey Statistics</h2>
                <hr style="color:#e2e2e2;" />
     
                The statistics page provides basic metrics about a survey. It presents a general
                overview of how the survey "performs" and how many people have participated.<br />
                <br />
            
                <b>* Creation Date</b><br />
                This is the date on which the survey was created.<br />
                <br />
         
                <b>* Last Entry On</b><br />
                This is the date on which the last response to the survey was recorded.<br />
                <br />
         
                <b>* Display Times</b><br />
                This is the number of times the survey (<a href="New/Web Control Deployment.aspx">web control</a>) has been shown. The number is
                not per person / session. It will increase each time the survey control has been
                rendered (i.e. on loading the survey page).<br />
                <br />
           
                <b>* Number Of Voters</b><br />
                Number of people that participated in the survey.<br />
                <br />
              
                <b>* Unvalidated Progress Entries</b><br />
                This is the number of participants who have saved their progress to resume it later
                on but have not validated their answers yet. All unvalidated entries can be deleted
                but this will also delete all the answers that were saved in between. Respondents who saved their progress
                will not be able to resume it after a delete.<br />
                <br />
                
                <b>* Monthly Stats</b><br />
                This shows how many respondents per day have participated in the survey. This count
                includes only validated answers.<br />
                <br />
             
                <b>* Reset Votes</b><br />
                This deletes all respondent answers that were posted for the survey (both validated and unvalidated). It is not possible
                to restore the answers once they have been deleted.<br />
                <br />
                                        <hr style="color:#e2e2e2;" />
        <h3>More Information</h3><br />
                <a href="New/Web Control Deployment.aspx">Web control Deployment</a>
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
