<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    File Manager</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The file manager is created to manage all the files that have been uploaded by
the respondent using the Extended%20-%20File%20Upload.html answer type.<br />
<br />
<u>Uploaded Files</u>
                <br /><br />
The list of uploaded files is presented. They can be downloaded locally.
Note: there is an option to see the respondent answers details by clicking on
the &quot;Details&quot; link (Posted By column) on the right of the &quot;Download&quot; link.<br />
<br />
<u>Export All Files To Server's Directory</u><br />
If there is a large number of files, downloading each file one by one can
quickly become a very cumbersome task.<br />
<br />
That's why there is an option to export all files that were uploaded to a directory
on the (web)server. Note &quot;write&quot; rights are necessary on the server
directory to which we will export the files.<br />
<br />
* <i>Create File Group Directories</i><br />
<br />
  o No sub-directory groups will copy all the files inside the same
    directory.<br />
  o Voter Id group will group the files uploaded by the same
    respondent under the same directory.<br />
  o File-GUID sub-directory will group the files uploaded on the same
    answer item inside the same directory.<br />
<br />
* Full Path On Server is the complete path  of the directory on the
  server to which the files are going to be exported.<br />
<br />

<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3><br />
                <br />
Extended%20-%20File%20Upload.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

