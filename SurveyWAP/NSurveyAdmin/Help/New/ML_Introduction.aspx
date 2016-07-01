<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#MLSurveys" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Multi-Language Surveys</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

There are often times when there may be a worldwide audience of respondent who
dont speak the same language and are not always speaking English. It would be possible to 
create for each language a separate survey but this would results in big
complications while compiling and aggregating the final results.<br />
<br />
Survey Project has the flexibility to translate a single survey into multiple
languages and let the respondent choose in which language he wants to
take the survey.<br />
<br />
At the end of the survey Survey's  reporting options Report%20General%20Settings.html will
provide a way to either have a global look at the results or a more
granular look per respondent's languages.<br />
<br />
Also check the MultiLanguage_Introduction.html in order to learn
more about the multi-languages features of Survey.<br />
<br />
Survey engine handles also languages written from right to left, like
Arabic.<br />
 <br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
MultiLanguage_Introduction.html<br />
Multil-Languages%20Settings.html<br />
System%20Messages%20Manager.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

