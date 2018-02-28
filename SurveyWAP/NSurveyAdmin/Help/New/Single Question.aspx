<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Single Question Type</h2><hr style="color:#e2e2e2;" />

A 'single question' can be composed out of any number of answer types.<br />
<br />
SP&trade; does not have the concept of a fixed radio-button, checkbox or textfield "question".
Instead is much more modular and allows to compose questions
based on the different Answer Types that are need for a particular question.<br />
<br />
<u>Single Question Examples</u><br />
<br />
A single question composed of Field text answers  and an XML (Country) list<br />
<br />
A single question composed of a Selection answertype with a radio button selection.<br />
<br />
A single question composed of Selection answertype with a drop down list.<br />
<br />
By combining different Answertypes almost any kind of questions can be created.<br />
<br />

                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
                <br />
<a href="Insert Question.aspx" title=" Insert Question " > Insert Question </a>	<br />
<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " > Answer Types Introduction </a>	<br />
                                <a href="Single Question.aspx" title=" Single Question " > Single Question </a>	<br />
                <a href="Static Question.aspx" title=" Static Question " > Static Question </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

