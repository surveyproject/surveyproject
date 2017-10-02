<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionLibrary" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Library Templates</h2><hr style="color:#e2e2e2;" />
         

The library templates enables the addition of new questions to a Question Library that
will be available as 'question templates' to be re-used and added through the Question editor to the survey form.<br />
<br />

<i>* Select Preview Language </i><br /> - set the library language to add questions to
<br />
<br />
<i>* Insert New / Existing Question</i><br /> -  option to add a new question to the
  library either by creating a new questino or by copying a question
  from an existing survey, form another library or import it from an XML file.<br />
<br />

                <hr style="color:#e2e2e2;"/>
    
                <h3>
                    More Information</h3>
                <br />
<a href="QL_Introduction.aspx" title=" Question Library Introduction " >Question Library Introduction </a>	<br />
<a href="Library Directory.aspx" title=" Library Directory " > Library Directory </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

