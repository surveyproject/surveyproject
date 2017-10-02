<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Entry Number Limitation</h2><hr style="color:#e2e2e2;" />
 
This security addin enables the option to set a maximum number of respondent who can
take a survey .<br /><br />
<i>* Entry Count: </i>
               <br /> - the current number of entries counted in the survey.<br />
<br />
<i>   * Max. Entries Allowed: </i>
             <br />- the maximum respondent entries allowed for
  the survey.<br />
<br />
<i>* Quota Reached Message: </i>
                <br />- the text that will be shown instead of the
  survey once the maximum entries have been reached.<hr style="color:#e2e2e2;" />
       
                <h3>
                    More Information</h3>
                <br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

