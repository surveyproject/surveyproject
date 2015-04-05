<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#FieldTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">Field - Slider</h2><br />
<br />
<br />
<hr style="color:#e2e2e2;" /> <br />
This field type renders a slider to the respondent to select a value from a range of values. Instead of entering values the slider can be moved up and down the slider bar to 
set the selected value. Left and right cursors can be used to move the slider as well. Special Slider Field settings are shown when selecting the 
Field - Slider Type. Default Type settings can be used also.
<br /><br />
<u>Slider Type Settings</u><br />
<br />

* <i>Min/Max Range</i><br />Options to set the starting point of sliderbar greyscale: a. minimum value = grey sliderbar start from mimimum side; b. maximum value = grey sliderbar
starts from maximum side; c. no value = no grey bar on either side (min. or max.).
<br />
<br />
* <i>Default Value</i><br />
Option to set the default position of the slider on the sliderbar by entering a value (signed integers only i.e. both positive and negative numbers, no decimal numbers).
<br />
<br />
* <i>Minimum Value</i><br />
Option to set the minimum value to select. Signed integers only i.e. both positive and negative numbers, no decimal numbers.
<br />
<br />
* <i>Maximum Value</i><br />
Option to set the maximum value to select. Signed integers only i.e. both positive and negative numbers, no decimal numbers.
<br />
<br />
* <i>Animate</i><br />
Option to set the animate feature of the sliderbar. If set the sliderbar if clicked will glide towards the clicked point on the bar.
<br />
<br />
* <i>Steps</i><br />
Option to set the steps value (signed integers only). The steps value determines the size of the increments with which the slider moves along the bar if clicked.
<br />
<br />
<u>Default Type Settings</u><br />
<br />
* <i>Answer Text</i><br />Answertext that will be shown next to the entry field.
<br />
<br />
*<i>Image URL</i><br />Used to add an image URL
  (e.g. <a href="#" target="_blank">http://www.mydomain.com/myimage.gif</a>) to show a picture alongside or instead of
   the answer text.
<br />
<br />
* <i>Type</i><br />Option to change the current field type used in the survey. Respondent answers already collected will not be deleted if
  the answertype is changed.<br />
<br />

* <i>RegEx Server Side Validation</i><br />Option to validate the selected value of the
  slider field using a regular expression. Several regular expression
  validations are provided out of the box and it is very easy to create a 
  new one using the Regular Expression Editor. This validation check is
  done server-side.<br />
<br />

* <i>Mandatory</i><br />Option to be set if the slider field is required to be
  filled. This check is done server-side.
<br />
<br />
* <i>Default Text Value</i><br />Option does not apply for Slider Field answers.
<br />
<br />
* <i>Pipe Alias</i><br />
Option to specify an alias that we can use in other questions to pipe the slider field value. To learn about piping and
  pipe alias we suggest reading the Piping Introduction.
<br />
<br />
* <i>Alias</i><br /> Answer Alias that can used in reports. The Alias is not shown or used in any surveys.<br />
<br />
* <i>ID</i><br /> Answer ID that can used in reports. The ID is not shown or used in any surveys.<br />
<br />
<br />
<br />
<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3><br />
  <br />
<br />            </td>
        </tr>
    </table>
</div></div></asp:Content>
