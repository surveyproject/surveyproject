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
                    Matrix Question Editor</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
                The matrix question editor allows to setup the configuration and constraints
of the "parent" (main) question of our Matrix question - Matrix%20Question.html.<br />
<br />
If we want to add rows and columns to the matrix we will need to use the rows and Columns editor at the bottom of the matrix editor page - 
Matrix%20Rows%20_%20Column%20Editor.html - after having created our parent
matrix question.<br />
<br />
<u>Matrix Question Options</u><br />
<br />
* Multiple Choices Matrix - is the mode in which the matrix 
  SelectionT_Introduction.html columns will be rendered by the Survey
  engine. By default these types are rendered as radio buttons but we can
  also render checkboxes to allow multiple choices.<br />
<br />
* Rating Part - activates the answers  rating / scaling options in
  our question's Selection Answer Types. To know more about rating and
  scaling read the Rating_Introduction.html.<br />
<br />
* Repeatable Matrix Section - allows to specify if the respondent will
  be able to duplicate the matrix in order to provide more answers. To
  learn more about repeatable sections read the
  Repeatable_Introduction.html<br />
<br />
* Min. Selection Required per row - is the minimum answers that are
  mandatory to select in each row. Only Selection Answer Types are
  calculated in the selection number count, if we want to make Field
  Answer Types mandatory we can do it by checking the Mandatory checkbox
  in the Field%20-%20Basic.html answers options.<br />
<br />
* Max. Selection Allowed per row - is the maximum of answers that can be
  selected in each row. Only Selection Answer Types are calculated in the
  selection number count, if we want to make Field Answer Types mandatory
  we can do it by checking the Required field in the
  Field%20-%20Basic.html answers options.<br />
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
Matrix Rows / Columns EditorPiping_Introduction.html<br />
Rating_Introduction.html<br />
Repeatable_Introduction.html<br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

