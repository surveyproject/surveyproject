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
                    Extended - File Upload</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

This type will allow the respondent to upload any number of files. There
are no limitation on the number of file upload types per question or per
page in a survey. You can manage the uploaded file using the
File%20Manager.html .<br />
<br />
This field has no extra client side validation check but if we require
the respondent to upload at least one file we can check the mandatory
checkbox inside the type settings.<br />
<br />
To learn more about file uploads we may check the
FileUpload_Introduction.html .<br />
<br />
Type Settings<br />
<br />
* Answer Text is the text that will be shown next to the upload field.<br />
<br />
* Image URL we can give an image URL
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show it instead of
  showing the answer text.
   <br />
<br />
* Type allows us to change the current type of the answer we want in
  our survey. Respondent answers already collected will not be deleted if
  we change the type.<br />
<br />
* Mandatory allows us to set if at least one file must be uploaded.
  This check is done server-side.<br />
<br />
* Reporting Alias is the text that can be shown instead of the answer
  text inside our reports.<br />
<br />
Extended Type Settings<br />
Extended type settings are extra specific properties of a type that are
only available once a type has been created.<br />
<br />
To edit the extended settings of a type we will have to click on the edit
button of the answer overview once we've added our type to our question
or changed an existing type to a new one by clicking the update button.<br />
<br />
* Max. File Upload Number is the number of files a respondent can
  upload for this type instance.
<br />
* Max. File Size is the maximum size in bytes allowed for upload. Note
  that by default ASP.net maximum size is 4mb, if we want to allow bigger
  upload. You may checking following
  ../../../support.microsoft.com/defaultb3c7.html?scid=kb;en-us;295626 on
  how to allow bigger uploads on your website.<br />
<br />
* Accepted Content Type let us specify if we want to accept only
  specific files eg: pdf, gif etc ..<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
AT_Introduction.html<br />
EF_Introduction.html<br />
Question%20Editor.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

