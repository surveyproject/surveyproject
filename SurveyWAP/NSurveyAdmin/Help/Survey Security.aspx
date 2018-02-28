<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Security" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <br />
<h2 style="color:#5720C6;">Survey Security</h2><hr style="color:#e2e2e2;" />
The survey security feature provides the option to build a &quot;security line&quot; with the individual
security addins. The respondent will be authenticated and identified against each addin that applies.<br />
<br />
<i>Survey Security Options</i>
<br />
<br />
* "Action on Invalid Authorisation": <br />option to choose the action to execute (show message or hide survey) when authentication with the security addins has
  failed. Note that if one of the security addins in the "line" shows an
  interface (eg: password addin), this interface will still be shown.
<br />
<br />

<i>Security AddIn Options</i><br />
<br />

* Up arrow:<br /> option to move the security addin up in the security line.
<br /><br />
* Down arrow: <br />option to move the security addin down in the security line.
<br />
<br />
* Disable : <br />option to disable the security addin from the line. By disabling
  the security addin it will not try to authenticate the respondent, the
  advantage is that if the security addin has stored specific information
  related to respondents (tokens, email codes etc...) this information
  will not be lost.
<br />
<br />
* Delete:<br /> option to delete the security addin from the line including all
  information it might have saved about or from respondents.
<br />
<br />
* Insert Security Addin:<br /> option to insert a new security addin.<br />
<hr style="color:#e2e2e2;" />
<h3>More information</h3><br />

<a href="Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
<a href="Insert Security AddIn.aspx" title="Insert Security Addin">Insert Security Addin</a><br />

           </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>


