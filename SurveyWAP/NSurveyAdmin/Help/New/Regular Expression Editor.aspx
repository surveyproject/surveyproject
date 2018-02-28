<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Regular Expression Editor</h2><hr style="color:#e2e2e2;" /> 

A "regular expression" is a piece of code that is used to check if a
given (answer) text matches the "expression" or not. A text (string) can be checked against
any regular expression combination like emails, numbers, zip codes etc
<br /><br /> Almost every Field Answertype can be validated against a regular expression created using the regular expression editor.<br />
<br />
RegularExpression can be added to a Field Answertype through the <a href="Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>. Option 'Regex server
                side validation'.
                <br /><br />
To learn more about regular expressions visit : <a href="http://www.regexlib.com" target="_blank">www.regexlib.com</a><br />
<br />

<u>Edit Regular Expression</u><br />
<br />
<i>* Select a RegEx to Edit/ View</i><br /> -
                to edit/ view an existing Regular Expression select an option from the Drop Down List to open the editor

<br /><br />
<i>* Name </i><br /> - the name that the regular expression will have in the Regular Expression library.<br />
<br />
<i>* Regular Expression </i><br /> - the regular expression code that will validate the answer text entry.<br />
<br />
<i>* Error Message </i><br /> - the error message that will be shown to the
  respondent if the answer entry does not match the regular expression.<br />
<br />

<u>Create a Regular Expression</u><br />
<br />
<i>* Click here to create a new RegEx </i><br /> - link to open the RegEx editor fields to add a new Regular Expression
<br /><br />
<i>* Name </i><br /> - the name that the regular expression will have in the Regular Expression library.<br />
<br />
<i>* Regular Expression </i><br /> - the regular expression code that will validate the answer text entry.<br />
<br />
<i>* Error Message </i><br /> - the error message that will be shown to the
  respondent if the answer entry does not match the regular expression.<br />
<br />


<u>Test Regular Expression</u><br />
<br />
<i>* Regular Expression </i><br /> - the regular expression code to be tested.<br />
<br />
<i>* Value To Test </i><br /> - the (answer text) value to be tested against the regular
  expression.<br />
<br />

                <hr style="color:#e2e2e2;"/>
      
                <h3>
                    More Information</h3>
                <br />
<a href="Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>	<br />
<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

