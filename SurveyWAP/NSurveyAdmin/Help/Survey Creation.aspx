<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.HelpFiles" Codebehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
            <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyManagement" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
      <tr>
        <td class="contentCell" valign="top">
        <br />
        <h2 style="color:#5720C6;">Survey Creation</h2><br />
        <br />
        <hr style="color:#e2e2e2;" />
         <br />
        The overall proces to create and use surveys is as follows:<br /><br />
                <ul style="position: relative; left: 10px;">
            <li> Create a new survey</li>
            <li> Adjust the survey settings</li>
            <li> Add questions through the form builder</li>
            <li> Adjust the question settings</li>
            <li> Add new answer items to the question</li>
            <li> Repeat step 3 to 5 as often as needed</li>
            <li> Setup security, multi-language and completion</li>
            <li> Publish the survey through a web link or by email</li>
            <li> Collect the results</li>
            <li> Create reports</li>
            <li> Analyse the results</li>
        </ul>
        <br />
        The first step after installing the Survey™ webapplication is to login and change the default username/ password combination (admin/ admin or admin/ SP_admin01) for the admin account. <br /><br />
        Once logged in as the administrator a new survey can be created. Select New Survey and add a title for the survey or import a survey XML file (of a previously generated and exported survey). The new survey will now open in the Form Builder. 
        This is were questions and answer items can be created. To set specific Survey settings like starting/ opening date and activating the survey go to <a href="Survey General Settings.aspx">Surveys - Settings</a>.  <br />
        <br />    
        Importing XML survey files exported using versions prior to version 2.* will need to be updated manually using notepad.<br />   <br />
        <br />
        <hr style="color:#e2e2e2;" />
        <br />
        <h3>More Information</h3><br />
        <a href="Survey%20General%20Settings.aspx">Survey General Settings</a><br />
        <!--
        <a href="FBT_Introduction.html">Form Builder Tutorial</a><br />
        <a href="SCE_Introduction.html">Survey Content Edition Introduction</a><br />
        <a href="SEC_Introduction-2.html">Survey Security</a><br />
        <a href="SD_Introduction.html">Survey Deployment</a><br />
        <a href="Introduction.html">Reporting Introduction</a>
        -->
        </td>
        </tr>
    </table>
</div></div></asp:Content>
