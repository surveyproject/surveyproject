<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" Assembly="Votations.NSurvey.WebControls" %>

<%@ Page Language="c#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master" ValidateRequest="false"
    AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.ControlCode" CodeBehind="ControlCode.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">
            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>

     <br />
    <fieldset style="width:750px; margin-top:15px; margin-left:12px; text-align: left;">
        <legend class="titleFont" style="margin: 0px 15px 0 15px;">
            <asp:Literal ID="SurveyUrl" runat="server" EnableViewState="False">Survey Hyperlinks</asp:Literal>
        </legend><br />
        <ol>
            <li>
                <div class="scm">
                    <asp:Literal ID="QuickLinkInfo" runat="server" EnableViewState="False">
                Deployment URL:</asp:Literal>
                </div>
                <br />
                Legacy:
                <asp:HyperLink ID="CodeHyperLink" Target="_blank" runat="server">HyperLink</asp:HyperLink>
                <br />
                Default:
                <asp:HyperLink ID="CodeMobileHyperLink" Target="_blank" runat="server">MobileHyperLink</asp:HyperLink>
                <br />
                &nbsp;
            </li>
            <li>
                <div class="scm">
                    <asp:Literal ID="friendlyLabel" runat="server">Create Friendly Name:</asp:Literal></div>

                <div style="float: left; margin-left: 225px; position: relative; top: -19px; width: 440px;">
                    <asp:TextBox ID="txtFriendly" runat="server"></asp:TextBox></div>
                <div style="float: right; position: relative; top: -37px; margin-right: 65px;">
                    <asp:Button ID="btnFriendly" CssClass="btn btn-primary btn-xs bw" runat="server" />
                </div>
            </li>
            <li>Legacy:
                <asp:HyperLink ID="friendlyUrlLink" Target="_blank" runat="server">HyperLink</asp:HyperLink><br />
                Default:
                <asp:HyperLink ID="friendlyMobileUrlLink" Target="_blank" runat="server">MobileHyperLink</asp:HyperLink><br />

                <br />
                <asp:Literal ID="FriendlyUrl" runat="server" EnableViewState="False">Note: Friendly Url format = http:// [YourWebSiteUrl] / surveymobile.aspx / [<b>YourFriendlyName</b>]</asp:Literal>
            </li>
        </ol>
        <br />
    </fieldset>
     <br /> <br />
    <fieldset style="width:750px; margin-left:12px; text-align: left;">
        <legend class="titleFont" style="margin: 0px 15px 0 15px;">
            <asp:Literal ID="ControlCodeTitle" runat="server" EnableViewState="False">Webcontrol asp.net starting code</asp:Literal></legend>
        <br /><ol>
            <li>
                <asp:Literal ID="PageDirectiveInfo" runat="server" EnableViewState="False">You must also include the following directive in the aspx pages that will display the survey :</asp:Literal>
                <br />
                <br />
                <span class="RegisterLiteral">
                    <asp:Literal ID="TagPrefixInfo" runat="server" EnableViewState="False">&lt;%@Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" 
            Assembly="Votations.NSurvey.WebControls" %&gt;</asp:Literal></span>
                <br />
                &nbsp;
            </li>
            <li>

 <textarea id="taCode" runat="server" style="width:685px; border:10px solid white;" rows="12" cols="85">

&lt;vts:SurveyBox ID="SurveyPreview" Surveyid="{0}" CssClass="surveybox" EnableValidation="true" runat="server" &gt;

&lt;QuestionStyle  CssClass="questionStyle"&gt;&lt;/QuestionStyle&gt;

&lt;QuestionValidationMessageStyle CssClass="qvmStyle" &gt;&lt;/QuestionValidationMessageStyle&gt;

&lt;QuestionValidationMarkStyle CssClass="qvmarkStyle" &gt;
&lt;/QuestionValidationMarkStyle&gt;

&lt;ConfirmationMessageStyle CssClass="cmStyle" &gt;
&lt;/ConfirmationMessageStyle&gt;

&lt;SectionOptionStyle CssClass="soStyle"&gt;&lt;/SectionOptionStyle&gt;

&lt;ButtonStyle CssClass="buttonStyle"&gt;&lt;/ButtonStyle&gt;

&lt;AnswerStyle CssClass="answerStyle"&gt;&lt;/AnswerStyle&gt;

&lt;MatrixStyle CssClass="matrixStyle" &gt;&lt;/MatrixStyle&gt;

&lt;MatrixHeaderStyle CssClass="mhStyle"&gt;&lt;/MatrixHeaderStyle&gt;

&lt;MatrixItemStyle CssClass="miStyle" &gt;&lt;/MatrixItemStyle&gt;

&lt;MatrixAlternatingItemStyle CssClass="maiStyle"&gt;
&lt;/MatrixAlternatingItemStyle&gt;

&lt;SectionGridAnswersItemStyle CssClass="sgiStyle" &gt;
&lt;/SectionGridAnswersItemStyle&gt;

&lt;SectionGridAnswersAlternatingItemStyle CssClass="sgaaisStyle" &gt;
&lt;/SectionGridAnswersAlternatingItemStyle&gt;

&lt;SectionGridAnswersStyle CssClass="sgaStyle" &gt;
&lt;/SectionGridAnswersStyle&gt;

&lt;SectionGridAnswersHeaderStyle CssClass="sgahStyle"&gt;
&lt;/SectionGridAnswersHeaderStyle&gt;

&lt;FootStyle CssClass="footStyle" &gt;&lt;/FootStyle&gt;

&lt;/vts:SurveyBox&gt;

</textarea>

            </li>
        </ol>
        <br />
    </fieldset>
</div></div></asp:Content>
