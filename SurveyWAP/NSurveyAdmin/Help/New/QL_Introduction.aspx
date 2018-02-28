<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionLibrary" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Question Library Introduction</h2><hr style="color:#e2e2e2;" />
   

SP&trade; enables the creation of a 'Question Library' where 
frequently used questions can be stored and archived to avoid having to create them multiple times and to 
re-use/ add them through the Question Editor to a new or existing survey form.<br />
<br />
As the Question library can be shared across all users in a multi-user
configuration, it is also a good way to share questions with other survey creators.<br />
<br />E.g. Suppose two survey creators have each created a survey. Unless theses surveys are mutually assigned to each other one creator 
                cannot access the others survey. To still be able to access/ use 'each others questions' without having to assign the complete survey a
                question library can be used to share specific questions.

                <hr style="color:#e2e2e2;"/>
   
                <h3>
                    More Information</h3>
                <br />
<a href="Library Directory.aspx" title=" Question Library Directory " >Question Library Directory </a>	<br />
<a href="Library Templates.aspx" title=" Library Templates " > Library Templates </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

