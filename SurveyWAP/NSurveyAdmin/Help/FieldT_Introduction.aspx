<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#FieldTypes" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">Field Answertypes Introduction</h2>
<hr style="color:#e2e2e2;" />
Field answertypes are used to have the respondent submit free text as an answer.
Field answertypes range from basic textboxes to complex field types like Text/HTML editors, calendars or slider
fields. It is possible to create new field types using SP&trade;'s Answer Type Creator.
<br />
<br />
The following field types are available by default:<br />
<br />
* <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/NSurveyAdmin/Help/Field - Basic.aspx" ToolTip="Field - Basic" Text="Field - Basic">Field - Basic</asp:HyperLink><br />
* <a href="Field_Address.aspx" title=" Field - Address " > Field - Address </a>	<br />
* <a href="New/Field - Large.aspx" title=" Field - Large " > Field - Large </a>	<br />
* <a href="New/Field - Required.aspx" title=" Field - Required " > Field - Required </a>	<br />
* <a href="New/Field - Email.aspx" title=" Field - Email " > Field - Email </a>	<br />
* <a href="New/Field - Rich.aspx" title=" Field - Rich " > Field - Rich </a>	<br />
* <a href="New/Field - Calendar.aspx" title=" Field - Calendar " > Field - Calendar </a>	<br />
* <a href="New/Field - Hidden.aspx" title=" Field - Hidden " > Field - Hidden </a>	<br />
* <a href="New/Field - Password.aspx" title=" Field - Password " > Field - Password </a>	<br />

* <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/NSurveyAdmin/Help/Field - Slider.aspx" ToolTip="Field - Slider" Text="Field - Slider">Field - Slider</asp:HyperLink><br />
<br />
<hr style="color:#e2e2e2;" /> <h3>More Information</h3><br />

<a href="new/AT_Introduction.aspx" title=" Introduction " > Answer Types Introduction </a>		<br />
                <a href="new/Answers Editor.aspx" title=" Answers Editor " >Answers Editor </a>	<br />


                  </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>


