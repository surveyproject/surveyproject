<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SecurityAddins" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Password Protection</h2><hr style="color:#e2e2e2;" />
          

This security addin protects the survey using a general password. Only
respondent knowing the right password will be able to access the survey.<br /><br />



<i>* Password:</i> 
                <br /> general password that protects the survey.
                <br /><br />
                                Rules for setting the password: <br /><br />
                o length minium 8 - maximum 12 characters; <br />
                o minimum character types: 1 small, 1 capital, 1 special, 1 number required<br /><br />
                
                <hr style="color:#e2e2e2;" />
                <h3>
                    More Information</h3>
                <br />
<a href="../Survey%20Security.aspx" title="Survey Security">Security</a><br />
<a href="../Sec_Introduction.aspx" title="Security Introduction">Security Introduction</a><br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

