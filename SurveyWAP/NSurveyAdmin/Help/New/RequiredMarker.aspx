<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Required Markers</h2><hr style="color:#e2e2e2;" />
          
                <b>General working</b><br /><br />

"Required Markers" are shown on the surveyform to inform and "warn" the respondent that a particular question or answer has to be answered or selected
 before the survey can be submitted or before continuing to the next page.
                <br /><br />

<b>Detailed working & improvements</b>
                <br /><br />
- Required Markers can be set on Question level and/or on Answer level:<br /><br />

a. Question level: by making use of the "Min. and Max. Selections Required/Allowed" dropdownlist options;<br />
b. Answer level: by making use of the "Mandatory" check box option;
<br /><br />
<u>A - Questionlevel:</u><br /><br />
* by setting "Min.selection required" to one (or higher) a [orange] required marker [triangle warning icon] will show on the survey form at the end of the question;<br />
* in previous SP&trade; versions this marker would only show if on Survey Settings level "Question Numbering" was NOT disabled; it is now independent of the use of Question numbering;
<br /><br />
The use of the "Min/ max. selection" options dropdownlist to set the required marker is confusing:
                <br /><br />
-  On questions (single type) with a selection mode like radiobutton ordropdownlist that will never allow selecting more than one answeroption 
it does not make sense to set Min./Max selection anyway. <br />
- Another "risk" is the fact that if on selection types like radio button or dropdownlist Min. Selection is set to more than one [> 1] the question 
will block the survey since only one option can be selected.<br /><br />


However the Min/ max. options cannot be blocked for these selection types as long as they are
also used to set the Required Marker on Question level. A future SP version may introduce a separate "Set Required Marker" checkbox.
                <br /><br />

<u>B - Answerlevel:</u><br /><br />
* most answertypes once selected to add to a question will show the Mandatory checkbox;<br />
* exceptions are: selection text, boolean, field hidden;<br />
* if the mandatory checkbox is checked a [red] required marker [*] will show on the survey form at the end of the answer text
<br /><br />
Examples:<br />
- answertype "Selection Other": mandatory set, marker shown --> if radiobutton is selected and no textanswer given: warning "other required"<br />
- answertype "Field basic": mandatory set, marker shown --> if now answer given, warning message: ".... required"
<br /><br />


<u>Additional remarks:</u>
<br /><br />
- Other ways to check, block or raise warnings on questions/ answers is to use the Answertype javascript functions and/ or Reg Expressions options.
E.g. the build in answertype Field Required makes use of javascript to raise a (popup) warning message is no answer is given. These options do not set a marker beforehand but will raise a warning/ message if the question/ answer is not answered/selected properly
<br /><br />
- On using Fileupload questions: do not use question level Min.required Setting to add a required marker, instead use the Mandatory checkbox on answerlevel; 
if done otherwise even if one file is uploaded uploaded going to the next page is blocked
<br /><br />
- On Multilanguage Questions: to show the required marker set the Min. Required select. on all language versions otherwise the marker will not show;
<br /><br />
- Required markers on both question and answer level are now also shown on the survey formbuiler once set.<hr style="color:#e2e2e2;" />
  
                <h3>
                    More Information</h3>
                <br />
                <a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a><br />
                <a href="Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>	<br />

                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

