<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.SurveyMain" %>
<%@ Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" Assembly="Votations.NSurvey.WebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>The Survey Project surveyform</title>
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
     <link href="nsurveyadmin/css/nsurveyform.css" type="text/css" rel="stylesheet" />

    <link rel="SHORTCUT ICON" href="favicon.ico" type="image/x-icon" />
</head>
<body>
    <form id="Form1" class="surveyform" runat="server">
            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
    <table width="100%">
        <tr>
            <td>
               
            </td>
        </tr>
    </table>
<!--   <table width="700px" class="innerText">
        <tr>
            <td> -->
                <vts:SurveyBox ID="SurveyControl" CssClass="surveybox" EnableValidation="false" runat="server">
                    <QuestionStyle CssClass="questionStyle"></QuestionStyle>
                    <QuestionValidationMessageStyle CssClass="qvmStyle"></QuestionValidationMessageStyle>
                    <QuestionValidationMarkStyle CssClass="qvmarkStyle"></QuestionValidationMarkStyle>
                    <ConfirmationMessageStyle CssClass="cmStyle"></ConfirmationMessageStyle>
                    <SectionOptionStyle CssClass="soStyle"></SectionOptionStyle>
                    <ButtonStyle CssClass="buttonStyle"></ButtonStyle>
                    <AnswerStyle CssClass="answerStyle"></AnswerStyle>
                    <MatrixStyle CssClass="matrixStyle"></MatrixStyle>
                    <MatrixHeaderStyle CssClass="mhStyle"></MatrixHeaderStyle>
                    <MatrixItemStyle CssClass="miStyle"></MatrixItemStyle>
                    <MatrixAlternatingItemStyle CssClass="maiStyle"></MatrixAlternatingItemStyle>
                    <SectionGridAnswersItemStyle CssClass="sgiStyle"></SectionGridAnswersItemStyle>
                    <SectionGridAnswersAlternatingItemStyle CssClass="sgaaisStyle"></SectionGridAnswersAlternatingItemStyle>
                    <SectionGridAnswersStyle CssClass="sgaStyle"></SectionGridAnswersStyle>
                    <SectionGridAnswersHeaderStyle CssClass="sgahStyle"></SectionGridAnswersHeaderStyle>
                    <FootStyle CssClass="footStyle"></FootStyle>
                </vts:SurveyBox>
<!--            </td>
        </tr>
    </table> -->
    <table width="100%">
        <tr>
            <td>

            </td>
        </tr>
    </table>
    </form>
</body>
</html>

