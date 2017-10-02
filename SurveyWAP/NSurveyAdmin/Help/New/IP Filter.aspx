<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    IP Filters</h2><hr style="color:#e2e2e2;" />
               

With this security addin specific IP addresses (or IP ranges) can be created and set that are allowed to have access to the survey.
                To create IP ranges for filtering go to menu Security/ IP Filter.

<br /><br />
                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
                <br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
                <a href="../Insert Security AddIn.aspx" title="Insert Security Addin">Insert Security Addin</a><br />

            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

