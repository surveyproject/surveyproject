<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
ASP.NET Security Context</h2><hr style="color:#e2e2e2;" />
              
This security addin protects the survey based on the ASP.NET security
context. It will check the HttpContext.Current.User.Identity object of ASP.NET
and store the value of it to prevent multiple submissions. <br /><br />
<i>* Allow Multiple Submissions: </i><br />
                - determines if the same user is allowed to take the
  survey multiple times.<br />
<br />
                Without making any changes to SP&trade; (e.g. web.config) the ASP.NET security addin will disable answering a survey when there is no active browser session 
                with an SP&trade; useraccount logged in to the SP webapplication.<br /><br /> On submitting the survey the data from the HttpContext.Current.User.Name object will be saved to 
                the vts_tbvoter table in the ContextUserName field (e.g. test,1,SurveyProject,Administrator,mail@surveyproject.org,True,True|).<br /><br /> This information is presented on the 
                voter report as part of the Repondent Details.
                <br /><br />
<u>Technical information on ASP.NET Security:</u><br />

<a href="http://msdn.microsoft.com/en-us/library/ms972109.aspx" target="_blank">http://msdn.microsoft.com/en-us/library/ms972109.aspx </a><br />
<a href="http://msdn.microsoft.com/en-us/library/ff647405.aspx" target="_blank">http://msdn.microsoft.com/en-us/library/ff647405.aspx </a><br />
<a href="http://support.microsoft.com/kb/301240" target="_blank">http://support.microsoft.com/kb/301240 </a><br /><br />

                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
                <br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

