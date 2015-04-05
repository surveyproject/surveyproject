namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.WebControlsFactories;

    /// <summary>
    /// Radiobutton (single choices) layout question control
    /// </summary>
    public class RadioButtonQuestion : SingleQuestion
    {
        /// <summary>
        /// Parse an AnswerDataCollection, 
        /// converts the data to webcontrols and assign them to a new section
        /// </summary>
        protected override AnswerSection GetAnswerSection(AnswerData answers, int sectionNumber, int sectionUid)
        {
            if (answers == null)
            {
                throw new ArgumentException("No instance set for the data of the question control");
            }
            RadioButtonAnswerSection section = new RadioButtonAnswerSection();
            section.SectionUid = sectionUid;
            section.SectionNumber = sectionNumber;
            section.LanguageCode = base.LanguageCode;
            section.Answers = AnswerItemFactory.CreateAnswerItemCollection(answers, this, section, AnswerSelectionMode.Radio, base.AnswerStyle, base.RenderMode, base.LanguageCode, this.UniqueID + GlobalConfig.AnswerSectionName + sectionUid, true, base.VoterAnswersState, base.EnableAnswersDefault);
            return section;
        }
    }
}

