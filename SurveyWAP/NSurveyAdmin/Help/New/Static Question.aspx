<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Static Question Type</h2><hr style="color:#e2e2e2;" />
            
The static question is the most basic "question type" available in SP&trade;.<br />
<br />
It is used to add any free text or pictures to the survey form. This question
type does not allow adding any answers. It will only render static text or HTML to the
respondent.<br />
<br />

                <hr style="color:#e2e2e2;"/>

                <h3>
                    More Information</h3>
                <br />
<a href="Insert Question.aspx" title=" Insert Question " > Insert Question </a>	<br />
<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
                <a href="Single Question.aspx" title=" Single Question " > Single Question </a>	<br />
<a href="Matrix Question.aspx" title=" Matrix Question " > Matrix Question </a>	<br />                <br />
         


                  </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

