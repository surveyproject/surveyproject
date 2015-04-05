<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#QuestionLibrary" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Library List & New Library</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
The library pages allows to create new question libraries where questions can be archived and sorted by specific topics.<br />
<br />
<u>
New Library Tab</u> - option to create a new library directory. Only one level directories are supported, it is not possible to
  create sub-directories.<br />
<br />
* Libray Name - the name of the name of library as shown to the users. <br />
<br />
* Description - optional general description of content and use of library

                <br />
<br />
<u>Library List Tab</u>
                                                <br />
<br />
                Library Name - click name to add/ edit questions 
                                <br />
<br />
                * Edit - option to change the library name, description, set multilanguage options and delete the library.<br />
<br />
* Delete Library will delete the directory including all its question  templates.<br />
<br /> <br />

                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
QL_Introduction.html<br />
Library%20Directory.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

