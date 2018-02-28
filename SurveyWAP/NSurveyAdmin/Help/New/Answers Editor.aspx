<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Answers Editor<br />
</h2><hr style="color:#e2e2e2;" />
             

The answer editor enables adding new answers to a question.  By
default SP&trade; offers different types of answer items but as can be read at <a href="Form Architecture.aspx" title=" Form Architecture " > Form Architecture </a> it is possible to develop and add
new answer items as well.<br />
<br />
<u>Question's Answer Editor</u><br />
<br />
                <i>* Display Answers of</i><br /> - switch or select question to add or edit it's answer items


                <br /><br />
                <i>* Edition Language option </i><br /> - select in which language to edit the
  answers label and default texts.<br />
<br />   This feature is only available if the multilanguage feature is turned on.<br /><br />


<u>Answer Overview</u><br />

<br />
<i>* Arrow Up - </i><br /> -will change the answer's position upward.
<br /><br />
<i>* Arrow Down -  </i><br /> -will change the answer's position downward.
<br /><br />
<i>* Delete </i><br /> - will delete the answer and all respondent answers related to
  this answer. Its not possible to recover it once it has been deleted.
<br /><br />
<i>* Edit </i><br /> - opens the Edit Answer Settings screen to change any of the answer item settings
<br /><br />

<i>* Add New Answer button </i><br /> - click to add a new answer item to the question.<br />
<br />
<u>Edit Answer Settings</u><br />
As each answer item has its specific set of properties and behavior read the documentation of each type individually to build the survey.<br /><br />
<a href="Selection - Text.aspx" title=" Selection - Text " > Selection - Text </a>	<br />
<a href="Selection - Other.aspx" title=" Selection - Other " > Selection - Other </a>	<br />
                
<a href="../Field - Basic.aspx" title=" Field - Basic " > Field - Basic </a>	<br />
<a href="../Field - Slider.aspx" title=" Field - Slider " > Field - Slider </a>	<br />
<a href="../Field_Address.aspx" title=" Field - Address " > Field - Address </a>	<br />

<a href="Field - Large.aspx" title=" Field - Large " > Field - Large </a>	<br />
<a href="Field - Required.aspx" title=" Field - Required " > Field - Required </a>	<br />
<a href="Field - Email.aspx" title=" Field - Email " > Field - Email </a>	<br />
<a href="Field - Rich.aspx" title=" Field - Rich " > Field - Rich </a>	<br />
<a href="Field - Calendar.aspx" title=" Field - Calendar " > Field - Calendar </a>	<br />
<a href="Field - Hidden.aspx" title=" Field - Hidden " > Field - Hidden </a>	<br />
<a href="Field - Password.aspx" title=" Field - Password " > Field - Password </a>	<br />

<a href="Xml - Country.aspx" title=" Xml - Country " > Xml - Country </a>	<br />
<a href="Xml - US States.aspx" title=" Xml - US States " > Xml - US States </a>	<br />

<a href="Extended - File Upload.aspx" title=" Extended - File Upload " > Extended - File Upload </a>	<br />
<a href="Subscriber - Xml List.aspx" title=" Subscriber - Xml List " > Subscriber - Xml List </a>	<br />
<a href="Boolean.aspx" title=" Boolean " > Boolean </a>	<br />

                <hr style="color:#e2e2e2;"/>
       
                <h3>
                    More Information</h3>
                <br />
<a href="SCE_Introduction.aspx" title=" Survey Form Builder Introduction " > Survey Form Builder Introduction </a>	<br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " >Answer Types Introduction </a>	<br />
<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
<a href="Form Architecture.aspx" title=" Form Architecture " > Form Architecture</a><br />
<a href="ML_Introduction.aspx" title=" Multi-Languages Introduction " > Multi Language Introduction </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

