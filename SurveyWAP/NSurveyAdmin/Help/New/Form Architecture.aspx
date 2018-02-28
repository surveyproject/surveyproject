<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Appendix" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Survey Form Architecture</h2><hr style="color:#e2e2e2;" />
          

Survey&trade; Project surveys have a flexible and modular architecture based on answer types and
question types and a survey webcontrol or 'container'.<br />
<br />
The main survey webcontrol is a 'container' that can holds any question
type either one of the default question types or newly created and/or customised question types (e.g. by adding new types to the source code). 
<br /><br />
These questions items contain answer items. As with the question item an answer item can be either one
of the default answer items or one that can be created additionally throught the sourcecode.<br />
<br />
Technically each answer item provides a set of 'events' that the question item can use in order to get 'notified' when new answers have been posted and in return
the question item will aggregate those results and send them back to the survey container that will act accordingly to what it receives (e.g. show/ hide results etc.)<br />
<br />
More technical information, documentation and the Survey&trade; Project source code can be found at the Survey&trade; Project Github site:
   
                <a href="https://github.com/surveyproject" target="_blank" title="Github SurveyProject">https://github.com/surveyproject</a>
                
                
                <hr style="color:#e2e2e2;" />
     
                <h3>
                    More Information</h3>
                <br />
<a href="AT_Introduction.aspx" title=" Answer Types Introduction " > Answer Types Introduction </a>	<br />
<a href="Single Question.aspx" title=" Single Question " > Single Question </a>	<br />
<a href="Matrix Question.aspx" title=" Matrix Question " > Matrix Question </a>	<br />       
<a href="Static Question.aspx" title=" Static Question " > Static Question </a>	<br />
                
                <div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

