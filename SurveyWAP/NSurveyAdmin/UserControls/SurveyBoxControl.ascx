<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyBoxControl.ascx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.SurveyBoxControl" %>
<%@ Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" Assembly="Votations.NSurvey.WebControls" %>


 <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="errorMessage" Visible="False"></asp:Label>
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
