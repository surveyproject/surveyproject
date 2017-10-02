<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    IP Protection</h2><hr style="color:#e2e2e2;" />
             
This security addin protects the survey against multiple submissions from the same IP address (computer or server)
by recording the IP of the respondent and disallow taking the
survey a second time after submitting the answers.<br /><br />
* IP Expires After: the number of minutes after a respondent IP that
  was recorded will be allowed to submit answers again . <br />
<br />

If multiple respondents are behind the same firewalls or proxies respondents may
have an identical IP address. SP&trade; tries to get the real IP but
depending on the mode activated on the firewalls or proxies it is
sometime not possible to get a unique IP per respondent.<br />
<br />

                <b>Note:</b><br /> Besides IP Protection there is a second IP based security addin to choose from: <a href="IP Filter.aspx">IP Filter</a>. This addin allows to create and set specific IP addresses (or IP ranges) that
                are allowed to have access to the survey. To create IP ranges for filtering go to menu Security/ IP Filter.

<br /><br />
                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

