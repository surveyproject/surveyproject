<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RepeatableSections" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Repeatable Sections Introduction</h2><hr style="color:#e2e2e2;" />
               
SP&trade;'s repeatable sections enable the respondent to copy and repeat and submit a
question as many times as needed.<br />
<br />
E.g. Suppose there is a question that asks the respondent to
enter information about books he/she has read during the past year. There would be two option to either add a huge number of answer
fields to the question to make sure the respondent will have enough
fields or to add the required answer fields for a book one time and
enable the repeatable sections. This will let the respondent have the
option to add as many new books answers as needed.<br />
<br />
When exporting the results of a repeatable question, data corresponding to each section are grouped by a similar section number.<br />
<br />
               Read more at <a href="Sections Management.aspx" title=" Sections Management " > Repeatable Sections Management </a><br />

                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
<a href="Question Editor.aspx" title=" Question Editor " > Question Editor </a>	<br />
<a href="Sections Management.aspx" title=" Sections Management " > Repeatable Sections Management </a><br />
<a href="Data Export.aspx" title=" Data Export " > Data Export </a>	<br />
                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

