<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#FieldTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">Field - Slider</h2>
<hr style="color:#e2e2e2;" /> 
This field answertype renders a slider on the surveyform for the respondent to select a value from a range of values. 
                Instead of entering values the slider can be moved up and down the slider bar to 
set the selected value. Left and right cursors can be used to move the slider as well. Special Slider Field settings are shown when selecting the 
Field - Slider Type. Default Answertype settings can be used also.
<br /><br />
<u>Slider Answertype Settings</u><br />
<br />

* <i>Min/Max Range</i><br />- Options to set the starting point of sliderbar greyscale: a. minimum value = grey sliderbar start from mimimum side; b. maximum value = grey sliderbar
starts from maximum side; c. no value = no grey bar on either side (min. or max.).
<br />
<br />
* <i>Default Value</i><br />
- Option to set the default position of the slider on the sliderbar by entering a value (signed integers only i.e. both positive and negative numbers, no decimal numbers).
<br />
<br />
* <i>Minimum Value</i><br />
- Option to set the minimum value to select. Signed integers only i.e. both positive and negative numbers, no decimal numbers.
<br />
<br />
* <i>Maximum Value</i><br />
- Option to set the maximum value to select. Signed integers only i.e. both positive and negative numbers, no decimal numbers.
<br />
<br />
* <i>Animate</i><br />
- Option to set the animate feature of the sliderbar. If set the sliderbar if clicked will glide towards the clicked point on the bar.
<br />
<br />
* <i>Steps</i><br />
- Option to set the steps value (signed integers only). The steps value determines the size of the increments with which the slider moves along the bar if clicked.
<br />
<br />
<u>Default Answertype Settings</u><br />
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

<i>* Default Text Value </i><br /> - option to set a default value inside the
  textbox field. WARNING: does not apply in case of a slider and should not be set<br />
<br />

<i>* Mandatory </i><br /> - option to determine if writing an answer in the textbox field is required. When answering the surey this check is done server-side.<br />
<br />


<i>* Pipe Alias </i><br /> - option to specify an alias that we can use in other
  questions by 'piping' the textbox field value. To learn about piping and pipe alias read hte <a href="new/AP_Introduction.aspx" title=" Piping Introduction " > Answer Piping Introduction </a>.
                <br />
<br /> 

<hr style="color:#e2e2e2;" /> <h3>More Information</h3><br />
                    <a href="new/AT_Introduction.aspx" title=" Introduction " > Answer Types Introduction </a>	<br />
                <a href="new/Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>	<br />
                <a href="FieldT_Introduction.aspx" title=" Introduction " > Field Answertypes Introduction </a>	<br />
<br />            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>