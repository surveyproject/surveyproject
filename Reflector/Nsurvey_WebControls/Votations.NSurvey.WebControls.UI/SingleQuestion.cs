namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.WebControlsFactories;

    /// <summary>
    /// Abstract class that questions which 
    /// offer all services required to generate
    /// single question's answer sections
    /// </summary>
    public abstract class SingleQuestion : SectionQuestion
    {
        private AnswerConnectionsCollection _answerConnections;
        private AnswerItemCollection _answers = new AnswerItemCollection();
        private int _columnsNumber = 1;
        private object _dataSource = null;
        private QuestionLayoutMode _questionLayoutMode;
        private SectionCollection _sections = new SectionCollection();
        private Table _sectionTable = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;

        protected SingleQuestion()
        {
        }

        /// <summary>
        /// Returns a hashtable containing using the section 
        /// number as hash key which each value being the number 
        /// of answers
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        private Hashtable CountSectionSelections(PostedAnswerDataCollection answers)
        {
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < answers.Count; i++)
            {
                if ((answers[i].TypeMode & AnswerTypeMode.Selection) > AnswerTypeMode.None)
                {
                    int sectionNumber = answers[i].SectionNumber;
                    hashtable[sectionNumber] = (hashtable[sectionNumber] == null) ? 1 : (int.Parse(hashtable[sectionNumber].ToString()) + 1);
                }
            }
            return hashtable;
        }

        /// <summary>
        /// Creates a new section of answers
        /// </summary>
        protected override Section CreateSection(int sectionNumber, int sectionUid)
        {
            if (this._answerConnections == null)
            {
                this._answerConnections = SubscriberItemFactory.CreateSubscriberItemCollection(base.QuestionId);
            }
            AnswerSection section = this.GetAnswerSection((AnswerData) this.DataSource, sectionNumber, sectionUid);
            section.QuestionId = base.QuestionId;
            section.ColumnsNumber = this.ColumnsNumber;
            section.LayoutMode = this.LayoutMode;
            section.AnswerConnections = this._answerConnections;
            section.AnswerStyle = base.AnswerStyle;
            section.EnableClientSideValidation = base.EnableClientSideValidation;
            section.ClientScriptGenerated += new ClientScriptGeneratedEventHandler(this.Section_ClientScriptGenerated);
            section.ID = GlobalConfig.AnswerSectionName + section.SectionUid;
            return section;
        }

        /// <summary>
        /// Parse an AnswerDataCollection, converts the data 
        /// to webcontrols and returns a filled answer section
        /// </summary>
        protected abstract AnswerSection GetAnswerSection(AnswerData answers, int sectionNumber, int sectionUid);
        protected override SectionAnswersGridItem GetSectionAnswersGrid()
        {
            SectionAnswersGridItem item = new SectionAnswersGridItem();
            item.Answers = (AnswerData) this.DataSource;
            item.LanguageCode = base.LanguageCode;
            return item;
        }

        /// <summary>
        /// Returns the highest section available in the 
        /// current voter answer state
        /// </summary>
        /// <returns></returns>
        protected override int GetSectionCountFromState()
        {
            int sectionNumber = 0;
            if (base.VoterAnswersState != null)
            {
                VoterAnswersData.VotersAnswersRow[] rowArray = (VoterAnswersData.VotersAnswersRow[]) base.VoterAnswersState.Select("QuestionId = " + base.QuestionId, "SectionNumber DESC");
                if ((rowArray != null) && (rowArray.Length > 0))
                {
                    sectionNumber = rowArray[0].SectionNumber;
                }
            }
            return sectionNumber;
        }

        /// <summary>
        /// Check if too much selections were made
        /// </summary>
        /// <param name="questionEventArgs"></param>
        /// <returns></returns>
        protected override bool MaxSelectionsReached(PostedAnswerDataCollection answers)
        {
            if (base.MaxSelectionAllowed > 0)
            {
                foreach (DictionaryEntry entry in this.CountSectionSelections(answers))
                {
                    if ((base.MaxSelectionAllowed != 0) && (int.Parse(entry.Value.ToString()) > base.MaxSelectionAllowed))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if any selection was left
        /// </summary>
        /// <param name="questionEventArgs"></param>
        /// <returns></returns>
        protected override bool MinSelectionsRequired(PostedAnswerDataCollection answers)
        {
            if (base.MinSelectionRequired > 0)
            {
                Hashtable hashtable = this.CountSectionSelections(answers);
                int sectionCount = base.SectionCount;
                if (base.RepeatMode == RepeatableSectionMode.GridAnswers)
                {
                    sectionCount = base.GetSectionCountFromAnswers(answers);
                    if (sectionCount == -1)
                    {
                        sectionCount = 0;
                    }
                    else if (base.GridMode != SectionGridMode.None)
                    {
                        sectionCount++;
                    }
                }
                if ((base.MinSelectionRequired > 0) && (hashtable.Keys.Count <= sectionCount))
                {
                    return true;
                }
                foreach (DictionaryEntry entry in hashtable)
                {
                    if (int.Parse(entry.Value.ToString()) < base.MinSelectionRequired)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// List of answer publisher that have to be linked
        /// to their subscribers
        /// </summary>
        public AnswerConnectionsCollection AnswerConnections
        {
            get
            {
                return this._answerConnections;
            }
        }

        /// <summary>
        /// Answer items collection that will be used
        /// as child controls
        /// </summary>
        public AnswerItemCollection Answers
        {
            get
            {
                return this._answers;
            }
        }

        /// <summary>
        /// How many columns of answers will be rendered 
        /// </summary>
        public int ColumnsNumber
        {
            get
            {
                return this._columnsNumber;
            }
            set
            {
                this._columnsNumber = value;
            }
        }

        /// <summary>
        /// Datasource that contains the answers, at this
        /// time it must be of an AnswerDataCollection type 
        /// </summary>
        public object DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                if (!(value is AnswerData))
                {
                    throw new ArgumentException("DataSource must be an AnswerData instance." + value.GetType());
                }
                this._dataSource = value;
            }
        }

        /// <summary>
        /// How will the answers of the control be rendered 
        /// </summary>
        public QuestionLayoutMode LayoutMode
        {
            get
            {
                return this._questionLayoutMode;
            }
            set
            {
                this._questionLayoutMode = value;
            }
        }
    }
}

