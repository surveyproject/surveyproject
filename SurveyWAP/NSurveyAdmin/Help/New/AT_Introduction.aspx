<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="../default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#AnswerTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
Answer Types Introduction</h2><hr style="color:#e2e2e2;" />

Answer types enables creating questions answer options as needed in order to get
the required feedback. SP&trade; provides out of the box a number of
answer types that can be used to compose a question. Each item has its
specific set of properties and all items can be used in any combination as part of the same question.<br />
<br />
List of Default Answer Items :<br />
<br />
<a href="Selection - Text.aspx" title=" Selection - Text " > Selection - Text </a>	<br />
<a href="Selection - Other.aspx" title=" Selection - Other " > Selection - Other </a>	<br />
                
<a href="../Field - Basic.aspx" title=" Field - Basic " > Field - Basic </a>	<br />
<a href="../Field - Slider.aspx" title=" Field - Slider " > Field - Slider </a>	<br />
<a href="../Field_Address.aspx" title=" Field - Address " > Field - Address </a>	<br />

<a href="Field - Large.aspx" title=" Field - Large " > Field - Large </a>	<br />
<a href="Field - Required.aspx" title=" Field - Required " > Field - Required </a>	<br />
<a href="Field - Email.aspx" title=" Field - Email " > Field - Email </a>	<br />
<a href="Field - Rich.aspx" title=" Field - Rich " > Field - Rich </a>	<br />
<a href="Field - Calendar.aspx" title=" Field - Calendar " > Field - Calendar </a>	<br />
<a href="Field - Hidden.aspx" title=" Field - Hidden " > Field - Hidden </a>	<br />
<a href="Field - Password.aspx" title=" Field - Password " > Field - Password </a>	<br />

<a href="Xml - Country.aspx" title=" Xml - Country " > Xml - Country </a>	<br />
<a href="Xml - US States.aspx" title=" Xml - US States " > Xml - US States </a>	<br />

<a href="Extended - File Upload.aspx" title=" Extended - File Upload " > Extended - File Upload </a>	<br />
<a href="Subscriber - Xml List.aspx" title=" Subscriber - Xml List " > Subscriber - Xml List </a>	<br />
<a href="Boolean.aspx" title=" Boolean " > Boolean </a>	<br />
<br />
It is also possible to create new answer items either using the
<a href="Answer Type Creator.aspx" title=" Answer Type Creator " >Answer Type Creator </a> or through the Survey&trade; Project source code.<br /><hr style="color:#e2e2e2;" />
 
                <h3>
                    More Information</h3>
                <br />
<a href="SCE_Introduction.aspx" title=" Survey Form Builder Introduction " > Survey Form Builder Introduction </a>	<br />
<a href="Answer Type Creator.aspx" title=" Answer Type Creator " >Answer Type Creator </a>	<br />
<a href="Answer Type Editor.aspx" title=" Answer Type Editor " >Answer Type Editor </a>	<br />                <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>

