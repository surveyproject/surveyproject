<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyMobile.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.SurveyMobile" %>
<%@ Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" Assembly="Votations.NSurvey.WebControls" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>The Survey Project Mobile Surveyform</title>
    <meta id="MetaDescription" name="DESCRIPTION" content="Survey Project is a free and open source web based survey and (data entry) forms toolkit for processing & gathering data online." />
    <meta id="MetaKeywords" name="KEYWORDS" content="Survey, Project, Nsurvey, c#, open source, websurvey, surveyform, formbuilder, FWS, Fryslan Webservices, codeplex " />
    <meta id="MetaCopyright" name="COPYRIGHT" content=" 2011 &lt;href='http://www.fryslanwebservices.com'>Fryslan Webservices&lt;/a>" />
    <meta id="MetaGenerator" name="GENERATOR" content="Survey Project" />
    <meta id="MetaAuthor" name="AUTHOR" content="Fryslan Webservices" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta name="ROBOTS" content="INDEX, FOLLOW" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Bootstrap -->
    <link href="content/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="content/bootstrap-theme.min.css" />
    <link href="nsurveyadmin/css/surveymobile.css" type="text/css" rel="stylesheet" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="Scripts/Javascript/HTML5shiv/html5shiv.js"></script>
      <script src="Scripts/respond.min.js"></script>
    <![endif]-->    

    <link rel="SHORTCUT ICON" href="favicon.ico" type="image/x-icon" />
</head>
<body>

    <div class="container">
    <form id="Form1" class="form-inline" runat="server">

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
                    <div class="container panel panel-default" style="margin-top:15px;">

                <vts:SurveyBox ID="SurveyControl" CssClass="surveybox" EnableValidation="false" runat="server">
                    <QuestionStyle CssClass="questionStyle"></QuestionStyle>
                    <QuestionValidationMessageStyle CssClass="qvmStyle"></QuestionValidationMessageStyle>
                    <QuestionValidationMarkStyle CssClass="icon-warning-sign"></QuestionValidationMarkStyle>
                    <ConfirmationMessageStyle CssClass="cmStyle"></ConfirmationMessageStyle>
                    <SectionOptionStyle CssClass="soStyle"></SectionOptionStyle>
                    <ButtonStyle CssClass="btn btn-primary btn-xs bw"></ButtonStyle>
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

      </div>

    </form>
        </div>

        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>

</body>
</html>

