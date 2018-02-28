<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#MLSurveys" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Multi-Language Surveys</h2><hr style="color:#e2e2e2;" />

There are often times when there may be a international audience of respondent who speak 
different languages and are not all fluent in English. It would be possible to 
create a separate survey for each language but this would results in a lot of work and great 
                difficulty in compiling and aggregating the final results.<br />
<br />
SP&trade; has the flexibility to translate a single survey into multiple
languages and let the respondent choose in which language he or she wants to
take the survey.<br />
<br />
After closing the survey the <a href="Graphic Reports.aspx">reporting options</a> will
provide a way to either have a global look at the results or a more
granular look per respondent's languages.<br />
<br />
Also check the <a href="..\Multi-Languages Settings.aspx">Multi Language Settings</a> helppage in order to learn
more about the multi-languages features of Survey.<br />
<br />
The survey web control also handles languages written from right to left, like
Arabic or Hebrew.<br />
 <br />
                <hr style="color:#e2e2e2;"/>
                <h3>
                    More Information</h3>
                <br />
<a href="..\Multi-Languages Settings.aspx">Multi Language Settings</a><br />
                                <a href="..\System%20Messages%20Manager.aspx" target="_self">System Messages & Multiple Languages</a><br />
                <a href="Graphic Reports.aspx">Reporting options</a>

                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

