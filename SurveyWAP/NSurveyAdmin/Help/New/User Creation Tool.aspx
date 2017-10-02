<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RolesUsers" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Accounts</h2><hr style="color:#e2e2e2;" />
                <br />
Through the Accounts pages useraccounts that can access the SP tool are created and administered. Depending on the
the type of user provider used on SP (i.e. AD integration of .NET users) it may not be possible edit the user but only assign roles and surveys.<br />
<br />
* User Name - the user name to log into Survey Project. <br />
<br />
* User Password is the password to log into Survey Project. This password is
  encrypted in the database and cannot be recovered.<br />
<br />
* First Name - is the first name of the account's user.<br />
<br />
* Last Name - is the last name of the account's user.<br />
<br />
* Email - is the email of the account's user.<br />
<br />
* Administrator - gives administrator rights to the user.<br />
<br />
* Assign All Surveys - user will be able to access all sureys. Note that
  if he is not administrator the roles will apply for what he can do or
  not do on the surveys.<br />
<br />
* Assigned Surveys - what surveys does the user have access to.<br />
<br />
* Roles - what roles / rights has the user.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
UM_Introduction.html<br />
User%20Manager.html<br />
Group%20Manager.html<br />
Roles%20Manager.html<br />
User%20Import.html<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

