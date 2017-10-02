<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RolesUsers" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Roles Manager</h2><hr style="color:#e2e2e2;" />
                <br />
This page to create roles that have specific rights.<br />
<br />
* Role Name - is the name of our role. <br />
<br />
* Role's Rights - is the rights that will be assigned to the role.<br />
<br />
 For an overview of Roles, Rolerights and the exact working of authorisations on page and menu level check the documentation section at http://survey.codeplex.com 
<br /><br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
UM_Introduction.html<br />
User%20Manager.html<br />
User%20Creation%20Tool.html<br />
Group%20Manager.html<br />
User%20Import.html<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

