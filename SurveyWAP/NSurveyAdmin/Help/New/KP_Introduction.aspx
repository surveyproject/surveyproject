<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#KeyProvider" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Key Providers in SP&trade;</h2><hr style="color:#e2e2e2;" />
             
The key provider model determines the way unique (access)
codes are generated in SP&trade;. <br /><br />
                
Functions like 'Save / resume progress' or options to change/ edit 
answers require generating unique IDs in order to allow the
respondent to restore the session in a secure way.<br />
<br />
By default SP&trade; is generating a unique code that will be delivered to
the respondent if for example progress is saved during a
survey. But what happens if the respondent is invited using an email
invitation that already has a unique code for each email or the
respondent has already received a token?<br />
<br />
 <i>Currently SP has no option to choose between keyproviders.</i> <br /><br />Both unique code and emailcode
                are needed to resume and continue answering the survey.
          <br /><br />      
                That's were the key provider model comes in. In future editions it should be possible to specify
which security addin will take priority to provide and manage
the unique keys required to restore the respondent sessions and answers.<br />
<br />
The following features act as a key provider for a survey :<br />
<br />
* <a href="EMail Code Protection.aspx" title="Email Code Protection">Email Code Protection</a><br />
* <a href="Token Protection.aspx" title="Token Protection">Token Protection</a><br />
                * <a href="../Survey%20General%20Settings.aspx" title="Resume of Progress">Resume of Progress</a><br />
                
<br /><br />
               
In case of the email invitation example, if the
respondent should be allowed to resume a session later on using the same code as the one
that is used by email code protection the email code
protection security addin should be set to act as a 'key provider' and the respondent
will have only one code to use for all action related to his session.<br />
<br />
Some authentication providers like Active Directory would even go further
as they automatically detect if an Active Directory user has saved a
session and will resume it without having the user to enter any code.<br /><hr style="color:#e2e2e2;" />
        
                <h3>
                    More Information</h3>
                <br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

