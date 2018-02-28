<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Security" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <br />
<h2 style="color:#5720C6;">Insert Security AddIn</h2>
<hr style="color:#e2e2e2;"/><br />
Following security addins can be added to the security line :
<br />
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
A security addin can only be added once in the security line.<br /><hr style="color:#e2e2e2;" /><br />
<br />
<h3>More Information</h3><br />
<a href="Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />

<br />
           </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>


