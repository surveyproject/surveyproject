<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Misc" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Extended - File Upload Answertype</h2><hr style="color:#e2e2e2;" />

This answertype allows the respondent to upload any number of files. There
is no limitation to the number of allowed filetypes per question or per
page in a survey. Uploaded files can be managed using the File Manager .<br />
<br />
This field has no additional client side validation check but if it is required for
the respondent to upload at least one file the mandatory
checkbox can be selected from the anwertype settings.<br />
<br />

<br />
<u>Extended Type Settings</u><br /><br />
Extended answertype settings are additional properties of an answertype that are
only available and become visible once an answertype has been selected and added to the question.<br />
<br />
To open and edit the 'extended settings' of an answer click the edit
button on the answer overview once the answer is added to the question or after changing an existing answertype to a new one by clicking the update button.
<br /><br />
* <i>Max. File Upload Number</i> <br />- the maximum number of files a respondent can
  upload for this answer.
<br /><br />
* <i>Max. File Size</i> <br />-  the maximum size in bytes per file allowed to upload. 
Note: there may also be a maximum set on file sizes set by the IIS webserver or .NET (the operating system). 
<br />
<br />
* <i>Accepted Content Type</i> <br />- option to specify the file types that will be accepted for upload. E.g. pdf, gif, txt.
                Enter the Mime/File type description to allow uploading a specific filetype. E.g. application/pdf or txt/plain. 
For an overview to mime and filetypes see <a href="https://www.freeformatter.com/mime-types-list.html" target="_blank">freeformatter.com</a>.
<br /><br />Note: If no File type is set all extentions are allowed.

<br />
<br />
<u>File Upload Answertype Settings</u><br />
<br />
<i>* Type </i><br /> -  option to change the current answer type of the answer to add to the question. Respondent answers already collected will not be deleted if
  the answer type is changed after submissions have been made.<br />
<br />
<i>* ID </i><br /> - Option to add an ID manually that will be saved to the database and can be used in (reporting) queries. The ID does 
               not show on the survey.
<br />
<br />
<i>* Alias </i><br /> -Option to add an Alias manually that will be saved to the database and can be used in (reporting) queries. The Alias does 
               not show on the survey.
<br />
<br />
<i>* Answer Text </i><br /> - the text that will be shown next to the selection item (radio, checkbox) or inside the dropdown list. 
                <br />
<br />
<i>* Image URL</i><br /> -  option to enter an image URL
  (<a href="http://www.mydomain.com/myimage.gif" target="_blank">http://www.mydomain.com/myimage.gif</a>) in order to show an image instead of
  the answer text. <br />
<br />


<i>* Mandatory </i><br /> - option to determine if writing an answer in the textbox field is required. When answering the surey this check is done server-side.<br />
<br />

<hr style="color:#e2e2e2;" /><h3>More Information</h3><br />

                <a href="AT_Introduction.aspx" title=" Introduction " > Answer Types Introduction </a>	<br />
<a href="File Manager.aspx" title=" File Manager " > File Manager </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

