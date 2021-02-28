<%@ Page Language="C#" Trace="false" maxPageStateFieldLength="102400"  EnableViewState="true" EnableViewStateMac="true" ViewStateEncryptionMode="Always" AutoEventWireup="true" CodeBehind="SurveyMobile.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.SurveyMobile" %>
<%@ Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" Assembly="SurveyProject.WebControls" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>SurveyProject&trade; Webform</title>
    <meta charset="UTF-8" />
    <meta name="application-name" content="Survey&trade; Project Webapplication" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <meta name="DESCRIPTION" content="SurveyProject&trade;  is a free and open source survey and (data entry) forms webapplication for processing & gathering data online." />
    <meta name="KEYWORDS" content="surveyproject, survey, webform, questionnaire, nsurvey, w3devpro" />
    <meta name="COPYRIGHT" content=" 2017 www.w3devpro.com - W3DevPro" />
    <meta name="GENERATOR" content="SurveyProject&trade; " />
    <meta name="AUTHOR" content="W3DevPro" />

    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta name="ROBOTS" content="INDEX, FOLLOW" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />

    <!-- IE only -->
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />

    <!-- Bootstrap Package -->
    <!-- <link href="Content/bootstrap.min.css" rel="stylesheet" /> -->

     <!-- Production CSS - minified or full -->
    <link id="defaultCSS" runat="server" href="Content/surveyform/surveymobile.css" rel="stylesheet" type="text/css" />

    <!-- Part of Bootstrap Installation -->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!-- See codebehind -->

    <link rel="SHORTCUT ICON" href="favicon.ico" type="image/x-icon" />
</head>
<body>

<iframe id="KeepAliveFrame" title="Keep Session Alive" src="~/NSurveyForm/KeepSessionAlive.aspx" frameborder="0" width="0" height="0" runat="server"></iframe>

        <section id="MainContainer" class="container"><h2 style="visibility:hidden; line-height:0.0em; margin-top:-5px">Survey&trade; Project Webform</h2>
            <form id="Form1" class="form-inline" runat="server">

                    <header id="ErrorMessageDiv" class="errorMessageDiv">
                        <asp:Label ID="MessageLabel" runat="server" CssClass="errorMessage" Visible="False"></asp:Label>
                        <span id="LoadMessage" style="display:none;">Load Message Text: multiple clicks not allowed......</span>
                    </header>

                <main id="SurveyBoxDiv" class="container panel panel-default" style="border:1px #ddd solid !important; border-radius:7px;">

                        <vts:SurveyBox ID="SurveyControl" CssClass="surveybox" EnableValidation="false" runat="server">
                            <QuestionStyle CssClass="questionStyle"></QuestionStyle>
                            <QuestionValidationMessageStyle CssClass="qvmStyle"></QuestionValidationMessageStyle>
                            <QuestionValidationMarkStyle CssClass="icon-warning-sign"></QuestionValidationMarkStyle>
                            <ConfirmationMessageStyle CssClass="cmStyle"></ConfirmationMessageStyle>
                            <SectionOptionStyle CssClass="soStyle"></SectionOptionStyle>
                            <ButtonStyle CssClass="btn btn-primary btn-sm bw"></ButtonStyle>
                            <AnswerStyle CssClass="answerStyle"></AnswerStyle>
                            <MatrixStyle CssClass="matrixStyle "></MatrixStyle>
                            <MatrixHeaderStyle CssClass="mhStyle"></MatrixHeaderStyle>
                            <MatrixItemStyle CssClass="miStyle"></MatrixItemStyle>
                            <MatrixAlternatingItemStyle CssClass="maiStyle"></MatrixAlternatingItemStyle>
                            <SectionGridAnswersItemStyle CssClass="sgiStyle"></SectionGridAnswersItemStyle>
                            <SectionGridAnswersAlternatingItemStyle CssClass="sgaaisStyle"></SectionGridAnswersAlternatingItemStyle>
                            <SectionGridAnswersStyle CssClass="sgaStyle"></SectionGridAnswersStyle>
                            <SectionGridAnswersHeaderStyle CssClass="sgahStyle"></SectionGridAnswersHeaderStyle>
                            <FootStyle CssClass="footStyle"></FootStyle>
                        </vts:SurveyBox>

                </main>
            </form>
        </section>

    <!-- Bootstrap JS package -->
    <script src="<%=Page.ResolveUrl("~/Scripts/bootstrap.min.js")%>"></script>

</body>
</html>