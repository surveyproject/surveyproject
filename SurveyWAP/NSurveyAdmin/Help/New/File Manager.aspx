<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
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

The file manager lets us manage all the files that have been uploaded by
the respondent using the Extended%20-%20File%20Upload.html answer type.<br />
<br />
Uploaded Files<br />
We can see the list of uploaded files and download locally the files.
Note that we can also see the respondent answers details by clicking on
the &quot;Details&quot; link on the right of the &quot;Download&quot; link.<br />
<br />
Export All Files To Server's Directory<br />
If we have a huge number of files, downloading each file one by one can
quicly become a very cumbersome task.<br />
<br />
That's why we can also export all files that were uploaded to a directory
on the server. Note that we need to have &quot;write&quot; rights on the server
directory to which we will export the files.<br />
<br />
* Create File Group Directories<br />
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

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
Extended%20-%20File%20Upload.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

