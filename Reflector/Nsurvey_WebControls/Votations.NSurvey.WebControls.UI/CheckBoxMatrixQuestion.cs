namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.WebControlsFactories;

    /// <summary>
    /// Matrix layout question control which holds
    /// child questions
    /// </summary>
    public class CheckBoxMatrixQuestion : MatrixQuestion
    {
        /// <summary>
        /// Parse an MatrixChildCollection, converts the data 
        /// to webcontrols and returns a filled matrix section
        /// </summary>
        protected override MatrixSection GetMatrixSection(MatrixChildQuestionData childQuestions, int sectionNumber, int sectionUid)
        {
            MatrixSection sectionContainer = new MatrixSection();
            sectionContainer.SectionUid = sectionUid;
            sectionContainer.SectionNumber = sectionNumber;
            sectionContainer.ChildQuestions = QuestionItemFactory.CreateQuestionChildCollection(childQuestions, sectionContainer, base.LanguageCode, this.UniqueID + GlobalConfig.AnswerSectionName + sectionUid, AnswerSelectionMode.CheckBox, base.AnswerStyle, base.RenderMode, base.VoterAnswersState, base.EnableAnswersDefault);
            return sectionContainer;
        }
    }
}

