<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Matrix Question Type</h2><hr style="color:#e2e2e2;" />
                
A matrix question is a question that is composed of rows and columns and is created by 
using the <a href="Matrix Question Editor.aspx" title=" Matrix Question Editor " > Matrix Question Editor </a>.<br />
<br />
Each row is basically a single question and each column can be composed
of any of the available <a href="AT_Introduction.aspx" title=" Answer Types Introduction " > Answer Types </a>. Each row will share the
same answer type of the column, it is not possible to have one row that has a different answer type inside the same column.<br />
<br />
Matrix questions are usefull when there is limited space on a page or if there is set of questions that are related.<br />
<br />
<u>Matrix Question Example</u><br />
<br />
A matrix question with a radio buttons layout and a large (text)field in one of its columns. It is also
possible to have matrix questions with several columns of checkboxes to allow multiple
choices.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
<a href="Insert Question.aspx" title=" Insert Question " > Insert Question </a>	<br />
<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " > Answer Types Introduction </a>	<br />
<a href="Matrix Question Editor.aspx" title=" Matrix Question Editor " > Matrix Question Editor </a><br />
<a href="Matrix Rows _ Column Editor.aspx" title=" Matrix Rows / Column Editor " > Matrix Rows / Column Editor </a>	<br />
                                <a href="Single Question.aspx" title=" Single Question " > Single Question </a>	<br />
                <a href="Static Question.aspx" title=" Static Question " > Static Question </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

