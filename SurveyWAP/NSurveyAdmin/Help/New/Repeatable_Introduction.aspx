<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#RepeatableSections" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Repeatable Sections</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
Survey Project's repeatable sections allows to enable the respondent to copy and repeat a
question as many times as needed.<br />
<br />
For example if there is a question that ask sthe respondent to
enter information about books he/she has read during the past year. We would
have two choices here, either we choose to add a huge number of answer
fields to our question to make sure the respondent will have enough
fields or we only add the required answer fileds for a book one time and
enable the repeatable sections. This will let the respondent have the
opportunty to add as many new books answers as needed.<br />
<br />
When exporting the results, data corresponding to each section are grouped by a same section number.<br />
<br />
A repeatable section in &quot;full repeat mode&quot;.<br />
<br />
A repeatable section in &quot;grid repeat mode&quot;.<br />
<br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
RSI_Introduction-2.html<br />
Question%20Editor.html<br />
Sections%20Management.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

