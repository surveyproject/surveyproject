<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Security" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
<br />
<h2 style="color:#5720C6;">Survey Security</h2><br />
<br />
<hr style="color:#e2e2e2;" /><br />
The survey security features provide the option to build a &quot;security pipe&quot; with the
security addins the respondent will be authenticated and identified against.<br />
<br />
<i>Survey Security Options</i>
<br />
<br />
* "User Has Not Been Authenticated By All Addins": option to choose if the survey is shown when authentication with the security addins has
  failed. Note that if one of the security addins in the pipe shows an
  interface (eg: password addin), this interface will still be shown.
<br />
<br />

<i>Security AddIn Options</i><br />
<br />

* Up arrow: option to move the security addin up in the security pipe.
<br />
* Down arrow: option to move the security addin down in the security pipe.
<br />
<br />
* Disable : option to disable the security addin from the pipe. By disabling
  the security addin it wont try to authenticate the respondent, the
  advantage is that if the security addin has stored specific information
  related to respondents (tokens, email codes etc...) this information
  wont be lost.
<br />
<br />
* Delete: option to delete the security addin from the pipe including all
  information it might have saved about respondents.
<br />
<br />
* Insert Security Addin: option to insert a new security addin in the pipe.<br />
<br />
<br />
<hr style="color:#e2e2e2;" /><br />
<br />
<h3>More information</h3><br />

<a href="Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
<a href="Insert Security AddIn.aspx" title="Insert Security Addin">Insert Security Addin</a><br />

           </td>
        </tr>
    </table>
</div></div></asp:Content>


