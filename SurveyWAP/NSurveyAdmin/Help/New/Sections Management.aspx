<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RepeatableSections" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Repeatable Section Management</h2><hr style="color:#e2e2e2;" />

The 'repeatable section' options are used to set the repeat options  on a question.<br />
<br />
<u>Full Answers Repeat</u><br />
<br />
This Repeat option will 'copy' the question and answers 'one on one' as many times as allowed or needed. All 'copies' are fully
                shown on the screen (question and answers) one below the other.

                <img src="../Images/RepeatFull.JPG" width="450" title="Full Repeat Section"/>


<br /><br />
<i>* Max. Selections Allowed </i><br /> - the maximum number of times a question can
  be repeated by the respondent.<br />
<br />
<i>* Add Section Link Text </i><br /> -  is the link text shown to the respondent to add a new
  section. If the Multi Language feature is enabled the currently 
  edited text will correspond to the Edition Language selected for the question (top of the page).<br />
<br />
<i>* Delete Section Link Text </i><br /> - text show to the respondent to delete
  an existing section. If the Multi Language feature is enabled the 
  currently edited text will correspond to the Edition language of the question.<br />
<br />
<u>Grid Answers Repeat</u><br />
<br />
This Repeat option will only show the original question that can be repeated as many times as needed or allowed. 
                Answers given and entered will be added and displayed in a grid below the original question. 
                <img src="../Images/RepeatGrid.JPG" width="470" title="Grid Repeat Section"/>
<br /><br />
<i>* Max. Selections Allowed</i><br /> - the maximum number of times a question can
  be repeated by the respondent.<br />
<br />
<i>* Add Section Link Text </i><br /> -  is the link text shown to the respondent to add a new
  section. If the Multi Language feature is enabled the currently 
  edited text will correspond to the Edition Language selected for the question (top of the page).<br />
<br />
<i>* Delete Section Link Text </i><br /> - text show to the respondent to delete
  an existing section. If the Multi Language feature is enabled the 
  currently edited text will correspond to the Edition language of the question.<br />

<br />
<i>* Edit Section Link Text </i><br /> - text show to the respondent to edit
  an existing section. If the Multi Language feature is enabled the 
  currently edited text will correspond to the Edition language of the question.<br />
<br />

<i>* Update Section Link Text </i><br /> - text show to the respondent to update
  an existing section. If the Multi Language feature is enabled the 
  currently edited text will correspond to the Edition language of the question.<br />
<br />

<i>* Answers Shown In Grid </i><br /> -  is the list of answer columns that will be
  shown in the review grid.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />

<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
<a href="Repeatable_Introduction.aspx" title=" Repeatable Section Introduction " > Repeatable Section Introduction </a>	<br />
                <a href="ML_Introduction.aspx" title=" Multi-Languages Introduction " > Multi Language Introduction </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

