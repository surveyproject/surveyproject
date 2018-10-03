<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RolesUsers" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    User Account Editing</h2><hr style="color:#e2e2e2;" />
                <br />
Registered SP users can be authorised (role based) to access their personal user account details. They will have the option to edit the account details as described below.
<br /><br />
To authorise an individual user to edit the account details the Usermanager Access [5100] and User Account Access [5101] rolerights will have to be added to the role to which the user is assigned.
                <br /><br />
* User Name - the user name to log into Survey Project. <br />
<br />
* User Password is the password to log into Survey Project. This password is
  encrypted in the database and cannot be recovered.<br />
<br />
* First Name - is the first name of the account's user.<br />
<br />
* Last Name - is the last name of the account's user.<br />
<br />
* Email - is the email address of the account's user.<br />
<br />

                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
                <br />
<a href="../UM_Introduction.aspx" title="User Management Introduction " >User Management Introduction </a><br />
<a href="User Manager.aspx" title=" User Manager " > User Manager </a><br />
<a href="User Creation Tool.aspx" title=" User Creation Tool " > User Creation Tool </a>	<br />
<a href="Roles Manager.aspx" title=" Roles Manager " > Roles Manager </a><br />
<a href="User Import.aspx" title=" User Import " > User Import </a>	<br />                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

