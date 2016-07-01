<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#Reporting" Visible="True" ToolTip="Back to Helpfiles Index" />
            </td>
        </tr>
        <tr>
            <td class="contentCell" valign="top">
                <br />
                <h2 style="color:#5720C6;">Data Export</h2>
                <br />
                <br />
                <hr style="color:#e2e2e2;"/>
                <br />
Using the data export tool it is possible to export all respondents answers
to any external third party reporting tool like Excel or SSPS.<br />
<br />
* <i>Data Selection</i> -  Option to export all answers or only
  specific answers of a survey.<br />
<br />
* <i>Export in</i> - is the format in which to export respondents
  answers. CSV is the most widely accepted format, especially with older
  tools. However Xml can be used with the new versions of Excel or
  Access.<br />
<br />
*<i> Field Delimiter</i> - is the character (char) that will act as a delimiter between the
  exported answers columns inside a CSV.<br />
<br />
* <i>Text Delimiter</i> - is the char that will act as a delimiter between the
  text entries inside a CSV.<br />
<br />
* <i>Replace CR</i> - is the char that will replace &quot;new line&quot; character. Some
  tools like Excel or Access might encounter problems with &quot;new line&quot;
  characters inside a row. Its advised to replace it with a custom char
  before replacing it again after having imported the data inside the
  target tool.<br />
<br />
* <i>Export Layout</i> option to determine the layout of the export file based on the order of columns and/or rows and question and answers<br />
<br />
* <i>Export From Date</i> -  is the start date interval from which to
  export. <br />
<br />
* <i>To Date</i> - is the end date interval from which to export. 

   <br />
<br />
<hr style="color:#e2e2e2;" /> <br /><h3>More Information</h3><br />
                <br />

                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

