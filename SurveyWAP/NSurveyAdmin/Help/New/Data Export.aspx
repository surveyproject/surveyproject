<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="mainBody" class="mainBody contentHolder ps-container"><div id="Panel" class="Panel content">
    <table class="TableLayoutContainer">
        <tr>
            <td>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx" Visible="True" ToolTip="Back to Helpfiles Index" />
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
Using the data export tool we are able to export all respondents answers
to any external third party reporting tool like Excel or SSPS.<br />
<br />
* Export Answers we can choose if we want to export all answers or only
  specific answers of our survey.<br />
<br />
* Export in is the format in which we want to export respondents
  answers. CSV is the most widely accepted format, especially with older
  tools. However we suggest to use Xml with the new versions of Excel or
  Access.<br />
<br />
* Field Delimiter  is the char that will act as a delimiter between the
  exported answers columns inside a CSV.<br />
<br />
* Text Delimiter is the char that will act as a delimiter between the
  text entries inside a CSV.<br />
<br />
* Replace CR is the char that will replace &quot;new line&quot; character. Some
  tools like Excel or Access might encounter problems with &quot;new line&quot;
  characters inside a row. We suggest replacing it with a custom char
  before replacing it again after having imported the data inside the
  target tool.<br />
<br />
* Use Reporting Aliaseswe can export the column names using the
  reporting aliases instead of using the full answers / questions label
  text.<br />
<br />
* Export From Date is the start date interval from which we want to
  export. <br />
<br />
* To Date is the end date interval from which we want to export. 

   <br />
<br />
                <hr style="color:#e2e2e2;"/>
                <br />
                <br />
                <h3>
                    More Information</h3>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</div></div></asp:Content>

