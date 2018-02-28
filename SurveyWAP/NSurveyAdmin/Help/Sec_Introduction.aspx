<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Security" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <br />
<h2 style="color:#5720C6;">Survey Security Introduction</h2>
<hr style="color:#e2e2e2;"/>
Part of SP&trade; is an extensive addin security system including several options to protect access to surveys.<br />
<br />
Survey security is build around a so called &quot;security line&quot;. As many
security addins as needed can be added to the "line". The respondent will only have access to 
the survey once they have been authorised by all the security addins that
have been added to the &quot;security line&quot; using the Surveys/ Security menuoption.<br />
<br />

The following security addins are provided to build a "security line" :<br />
<br />
* <a href="New/IP Protection.aspx">IP Protection</a> <br />
* <a href="New/IP Filter.aspx">IP Filters</a><br />
* <a href="New/Cookie Protection.aspx">Cookie Protection</a><br />
* <a href="New/Pass Protection.aspx">Password Protection</a><br />
* <a href="New/EMail Code Protection.aspx">Email Code Protection</a><br />
* <a href="New/Token Protection.aspx">Token Protection</a>   <br />
* <a href="New/ASP.NET Security Context.aspx">ASP.NET Security Context</a><br />
* <a href="New/Survey Security Context.aspx">SP&trade; Security Context</a><br />
* <a href="New/Entry Number Limitation.aspx">Entry Number Limitation</a><br />
<br />
                <br />
                Click the individual security addin link for instructions.


<hr style="color:#e2e2e2;" />
<h3>More Information</h3><br />

<a href="Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="Insert Security AddIn.aspx" title="Insert Security Addin">Insert Security Addin</a><br />
<br />
<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

