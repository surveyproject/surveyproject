<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Token" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Token Security</h2><hr style="color:#e2e2e2;" />
              
The Token Security webpage enables the creation of unique tokens and thereby protect access to a 
survey. Token security is the most secure
method to prevent multiple submissions as it is only possible to take the
survey if one has a valid token. Tokens can be used only once.<br />
<br />
Another advantage is that it can link a user identity (voterid) to each token
created, this way its possible by way of querying the database (vts_tbsurveytoken table) to know exactly who did take or didn't take the survey. 
                As a token is just a text it can be distributed using any electronic
or non electronic medium from standard mail, phone to emails.<br />
<br />
Token security has already been used on major projects to conduct large
ballots with great success.<br />
<br />Go to <a href="Token Protection.aspx">Token Protection</a> for details on the generation of Tokens.


                <hr style="color:#e2e2e2;"/>
   
                <h3>
                    More Information</h3>
                <br />
                <a href="Token Protection.aspx">Token Protection</a><br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
