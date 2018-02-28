<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Matrix Question Editor</h2><hr style="color:#e2e2e2;" />
           
                The 'matrix question editor' is used to determine the configuration and constraints
of the "parent" (main) question of a Matrix of questions.<br />
<br />
                To start the Matrix Question Editor first insert a new question into the survey form. Next select the Type - Matrix Question from the dropdown list. Click the Add new Question button to open the Matrix Question Editor.
                <br /><br />
To add rows and columns to the matrix the  <a href="Matrix Rows _ Column Editor.aspx" title=" Matrix Rows / Column Editor " > Matrix Rows & Column Editor </a> at the bottom of the matrix editor page will be needed 
                after having created a parent matrix question.<br />
<br />
<u>Matrix Question Options</u><br />
<br />
<i>* Multiple Choices Matrix </i><br /> - the multiple choice mode in which the matrix 
  selection columns will be rendered by the survey. By default these types are rendered as radio buttons but it can
  also render checkboxes to allow multiple choices.<br />
<i><br />
* Rating </i><br /> -  activates the answers  rating  options in
 the Matrix Rows & Columns editor. To know more about rating and
  scaling read the <a href="Rating_Introduction.aspx" title=" Rating Introduction " > Rating Introduction </a>. Also the <a href="QuestionGroups.aspx" title=" Question Groups " > Question Groups </a> options become available
                when the Rating is activated.<br />
<br />
<i>* Repeatable Matrix Section </i><br /> - determines if the respondent will
  be able to duplicate (repeat) the matrix in order to provide more answers or submit it multiple times. To
  learn more about repeatable sections read the <a href="Repeatable_Introduction.aspx" title=" Repeatable Section Introduction " > Introduction </a><br />
<br />
<i>* Min. Selection Required per row </i><br /> -  the minimum number of answers that is
  mandatory to select in each row. Only Selection Answer Types are
  calculated in the selection number count. If Field
  Answer Types have to be mandatory it can be set by checking the Mandatory checkbox
  in the Matrix Rows & Columns editor's answers options.<br />
<br />
<i>* Max. Selection Allowed per row </i><br /> -  the maximum number of answers that can be
  selected in each row. Only Selection Answer Types are calculated in the
  selection number count. If Field Answer Types have to be mandatory
  it can be set by checking the Mandatory checkbox
  in the Matrix Rows & Columns editor's answers options.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
                <a href="AT_Introduction.aspx" title=" Answer Types Introduction " >Answer Types Introduction </a>	<br />
<a href="Matrix Question.aspx" title=" Matrix Question " > Matrix Question </a><br />
<a href="Matrix Rows _ Column Editor.aspx" title=" Matrix Rows / Column Editor " > Matrix Rows / Column Editor </a>	<br />
<a href="Rating_Introduction.aspx" title=" Rating Introduction " >Rating Introduction</a><br />
<a href="Repeatable_Introduction.aspx" title=" Introduction " >Repeatable Sections Introduction</a><br />
    <a href="QuestionGroups.aspx" title=" Question Groups " > Question Groups </a>       <br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

