<%@ Page Title="" Language="C#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"
    AutoEventWireup="false" CodeBehind="CampaignStart.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.CampaignStart" %>

<%@ Register TagPrefix="vts" Namespace="Votations.NSurvey.WebControls" Assembly="SurveyProject.WebControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="Panel" class="Panel">

            <fieldset>
                 <legend class="titleFont titleLegend">
                                            <asp:Literal ID="SurveyCodePreviewTitle" runat="server" EnableViewState="False" Text="Preview Survey"></asp:Literal>
             </legend> 
            <br />

                            <table class="innerText"  style="table-layout:fixed;  margin-left:15px; text-align:left; width:95%;">
                                <tr>
                                    <td>
                                        <asp:Literal Mode="PassThrough" ID="SurveyHeaderCustom" runat="server"></asp:Literal>
                                        <asp:Literal runat="server" ID="ltImg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <vts:SurveyBox ID="SurveyPreview" CssClass="surveybox" EnableValidation="true" runat="server">
                                            <QuestionStyle CssClass="questionStyle"></QuestionStyle>
                                            <QuestionValidationMessageStyle CssClass="qvmStyle"></QuestionValidationMessageStyle>
                                            <QuestionValidationMarkStyle CssClass="icon-warning-sign"></QuestionValidationMarkStyle>
                                            <ConfirmationMessageStyle CssClass="cmStyle"></ConfirmationMessageStyle>
                                            <SectionOptionStyle CssClass="soStyle"></SectionOptionStyle>
                                            <ButtonStyle CssClass="btn btn-primary btn-xs bw"></ButtonStyle>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal Mode="PassThrough" ID="SurveyFooterCustom" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
            </fieldset>
                                                 <div id="fillerDiv" class="fillerDiv">&nbsp;</div>
</div></asp:Content>
