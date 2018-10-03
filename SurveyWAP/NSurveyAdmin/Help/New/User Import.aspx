<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RolesUsers" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Import Users</h2><hr style="color:#e2e2e2;" />
                <br />

This page allows to import (a list of) new Survey Project users.<br />
<br />
* Import CSV Data - option to cut &amp; past a list of users to import
  into Survey Project. <br />
<br />
* Administrator - gives administrator rights to the users. <br />
<br />
* Assign All Surveys - users will be able to access all sureys. Note that
  if he is not administrator the roles will apply for what they can do or
  not do on the surveys.<br />
<br />
* Assigned Surveys - what surveys do the users have access to. <br />
<br />
* Roles  - what roles / rights do the users have.<br />
<br />
                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
<a href="../UM_Introduction.aspx" title="User Management Introduction " >User Management Introduction </a><br />
<a href="User Manager.aspx" title=" User Manager " > User Manager </a><br />
<a href="User_Account.aspx" title=" User Account Editing " > User Account Editing </a>	<br />
<a href="Roles Manager.aspx" title=" Roles Manager " > Roles Manager </a><br />
<a href="User Creation Tool.aspx" title=" User Creation Tool " > User Creation Tool </a>	<br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

