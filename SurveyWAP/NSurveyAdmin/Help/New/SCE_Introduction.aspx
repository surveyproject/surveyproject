<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Building Surveys Introduction</h2><hr style="color:#e2e2e2;" />
           

The survey Form Builder is used to create and edit the 'content' (questions/ answers) of surveys and webforms.<br />
<br />
Each survey/ webform can make use of several different question types. Each of which can make use of different answer types. 
There are no limitations to the number of combinations of different answer types that are part of a question.<br />
<br />
E.g. field answer types, selection answer types or SQL bound types can all be combined. All as part of the same question. 
This mix of question and answertypes offers maximum flexibility to create webforms and surveys for different purposes and use.<br />
<br />

                <hr style="color:#e2e2e2;"/>
   
                <h3>
                    More Information</h3>
                <br />
<a href="Survey Form Builder.aspx" title=" Survey Form Builder " > Survey Form Builder </a>	
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

