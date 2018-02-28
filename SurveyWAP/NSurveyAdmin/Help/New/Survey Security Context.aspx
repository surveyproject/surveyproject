<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    SP&trade; Security Context</h2><hr style="color:#e2e2e2;" />
             
This security addin protects access to the survey based on the SP&trade; Security
context. It requires a respondent to have an SP&trade; useraccount and to be logged in to the SP&trade; webapplication to take surveys.
                 <br /><br />
                
                It will check the current SP&trade; User that is
logged in to the SP&trade; webapplication and store its username to prevent multiple submissions. <br /><br />
<i>* Allow Multiple Submissions:</i> 
                <br />- enables if the same user is allowed to take the
  survey multiple times.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
                <a href="../UM_Introduction.aspx" title="User Manager Introduction">User Manager Introduction</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

