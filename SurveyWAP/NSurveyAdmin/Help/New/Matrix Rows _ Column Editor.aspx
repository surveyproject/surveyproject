<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyDesigner" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">
                    Matrix Layout Editor</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />

The matrix layout editor alows to add, edit rows / columns to the matrix question- Matrix%20Question.html.<br />
<br />
<u>Matrix Layout Editor</u><br />
<br />
* add either a new column or a new row to the matrix.<br />
<br />
* edit either a column or a row inside the matrix.<br />
<br />
* deletes a column or a row. Respondent answers related to the row
  columns will also be deleted and cannot be recovered afterward.<br />
<br />
<u>Row Editor</u><br />
<br />
* Row Question - is the text of the row.<br />
<br />

<u>Column Editor</u><br />
<br />
* Column Header Text -  the text that will appear in the matrix's column's
  header<br />
<br />
* Type - allows us to set the current answertype - AT_Introduction.html - we want in
  our column. Respondent answers already collected will not be deleted if we change the type.<br />
<br />

* Rating Part -option to choose if this selection is to be part of the
  rating / scale calculation in the reports. To learn more about rating
  read the Rating_Introduction.html.<br />
<br />
  This feature is only available if rating is turned on - 
  Rating_Introduction.html - on the matrix question of the row / columns
  set.<br />
<br />

<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
FBT_Introduction.html<br />
Matrix%20Question.html<br />
AT_Introduction.html<br />
Matrix Question EditorMatrix%20Question%20Editor.html<br />
Rating_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

