<%@ Page Language="c#" MasterPageFile="~/nsurveyadmin/MsterPageTabs.master" AutoEventWireup="false"
    Inherits="Votations.NSurvey.WebAdmin.HelpFiles" CodeBehind="default.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><div id="helpPanel"><div>
                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/index-icon.png" runat="server" CssClass="buttonIndex"
                    PostBackUrl="~/NSurveyAdmin/Help/default.aspx#SurveyLayout" Visible="True" ToolTip="Back to Helpfiles Index" />
            </div><div>
                <h2 style="color:#5720C6;">
                    Web Control Style</h2><hr style="color:#e2e2e2;" />
            
                When deploying the survey webcontrol by publishing a survey the style of the survey can
                be customized by changing the CSS itemstyle attributes of the (ASP.NET) survey webcontrol
                code (see the surveymobile.aspx file for the survey webcontrol and the surveymobiles.css file for the css) .<br />
                <br />
                SP&trade; offers an option to change almost any part of the
                control's design as shown throught this list of control attributes:<br />
                <br />
                * &lt;QuestionStyle&gt;<br />
                * &lt;QuestionValidationMessageStyle&gt;<br />
                * &lt;QuestionValidationMarkStyle&gt;<br />
                * &lt;ConfirmationMessageStyle&gt;<br />
                * &lt;SectionOptionStyle&gt;<br />
                * &lt;SectionGridAnswersItemStyle&gt;<br />
                * &lt;SectionGridAnswersAlternatingItemStyle&gt;<br />
                * &lt;SectionGridAnswersStyle&gt;<br />
                * &lt;SectionGridAnswersHeaderStyle&gt;<br />
                * &lt;ButtonStyle&gt;<br />
                * &lt;AnswerStyle&gt;<br />
                * &lt;MatrixStyle&gt;<br />
                * &lt;MatrixHeaderStyle&gt;<br />
                * &lt;MatrixItemStyle&gt;<br />
                * &lt;MatrixAlternatingItemStyle&gt;<br />
                * &lt;FootStyle&gt;<br />
                <br />
                <br />Default list of Control attributes and CSS styles used:                <br />                <br />
                 <code>
                    QuestionStyle CssClass="questionStyle"                <br />
                    QuestionValidationMessageStyle CssClass="qvmStyle"                <br />
                    QuestionValidationMarkStyle CssClass="qvmarkStyle"                <br />
                    ConfirmationMessageStyle CssClass="cmStyle"                <br />
                    SectionOptionStyle CssClass="soStyle"                <br />
                    ButtonStyle CssClass="buttonStyle"                <br />
                    AnswerStyle CssClass="answerStyle"                <br />
                    MatrixStyle CssClass="matrixStyle"                <br />
                    MatrixHeaderStyle CssClass="mhStyle"                <br />
                    MatrixItemStyle CssClass="miStyle"                <br />
                    MatrixAlternatingItemStyle CssClass="maiStyle"                <br />
                    SectionGridAnswersItemStyle CssClass="sgiStyle"                <br />
                    SectionGridAnswersAlternatingItemStyle CssClass="sgaaisStyle"                <br />
                    SectionGridAnswersStyle CssClass="sgaStyle"                <br />
                    SectionGridAnswersHeaderStyle CssClass="sgahStyle"                <br />
                    FootStyle CssClass="footStyle"
                </code>
                <br /><br />
                 In addition to these default CSS tags used on the survey webcontrol, the look and feel of multiple other individual
                 elements on the surveyform webpage can be determined through the Designer/ Layout/ CSSXML list. This is were CSS class names and selectors
                can be linked to different elements of the surveyform webpage (besides the ones mentioned at the surveycontrol).
                <br /><br />
                <h3>
                    How To Apply CSS</h3><br />
                    In case of customised CSS it can be applied to the style items as follows. As
                    an example take this CSS style that must first be added to the surveymobile.css file:
                    <br />
                    <br />
                    <code>.myQuestionCSS<br />
                        {<br />
                        background-color: #003F51;<br />
                        color : #C0FAFF;<br />
                        font-family: Arial, Verdana, sans-serif;<br />
                        font-size:x-small;<br />
                        }<br />
                    </code>
                    <br />
                    To apply it to any of the style items listed above do this:<br />
                    <br />
                    <code>&lt;QuestionStyle CssClass=&quot;myQuestionCSS&quot;&gt;<br />
                        &lt;/QuestionStyle&gt;<br />
                    </code><hr style="color:#e2e2e2;" />
                    <h3>
                        More Information</h3>
                    <br />
                    <a href="Style_Introduction.aspx" title="Style Introduction">Style Introduction</a><br />
                    <a href="CssXml.aspx" target="_self">Css XML List</a><br />

                    <br />
            </div>
<div id="fillerDiv" class="fillerDiv">&nbsp;</div></div></asp:Content>
