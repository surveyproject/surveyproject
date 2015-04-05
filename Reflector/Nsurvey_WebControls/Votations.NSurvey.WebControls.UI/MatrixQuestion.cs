namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;
    using Votations.NSurvey.WebControlsFactories;

    /// <summary>
    /// Matrix layout question control which holds
    /// child questions
    /// </summary>
    public class MatrixQuestion : SectionQuestion
    {
        private MatrixChildCollection _childQuestions = new MatrixChildCollection();
        private object _dataSource = null;
        private Style _matrixAlternatingItemStyle;
        private Style _matrixHeaderStyle;
        private Style _matrixItemStyle;
        private Style _matrixStyle;

        private Hashtable CountSectionChildQuestionsSelections(PostedAnswerDataCollection answers)
        {
            Hashtable hashtable = new Hashtable();
            new Hashtable();
            for (int i = 0; i < answers.Count; i++)
            {
                if ((answers[i].TypeMode & AnswerTypeMode.Selection) > AnswerTypeMode.None)
                {
                    int questionId = answers[i].Item.QuestionId;
                    int sectionNumber = answers[i].SectionNumber;
                    if (hashtable[questionId] == null)
                    {
                        hashtable[questionId] = new Hashtable();
                    }
                    ((Hashtable) hashtable[questionId])[sectionNumber] = (((Hashtable) hashtable[questionId])[sectionNumber] == null) ? 1 : (int.Parse(((Hashtable) hashtable[questionId])[sectionNumber].ToString()) + 1);
                }
            }
            return hashtable;
        }

        /// <summary>
        /// Creates a new matrix section 
        /// </summary>
        protected override Section CreateSection(int sectionNumber, int sectionUid)
        {
            MatrixSection section = this.GetMatrixSection((MatrixChildQuestionData) this.DataSource, sectionNumber, sectionUid);
            section.QuestionId = base.QuestionId;
            section.MatrixStyle = this.MatrixStyle;
            section.MatrixItemStyle = this.MatrixItemStyle;
            section.MatrixAlternatingItemStyle = this.MatrixAlternatingItemStyle;
            section.MatrixHeaderStyle = this.MatrixHeaderStyle;
            section.EnableClientSideValidation = base.EnableClientSideValidation;
            section.ClientScriptGenerated += new ClientScriptGeneratedEventHandler(this.Section_ClientScriptGenerated);
            section.ID = GlobalConfig.AnswerSectionName + section.SectionUid;
            return section;
        }

        /// <summary>
        /// Parse an MatrixChildCollection, converts the data 
        /// to webcontrols and returns a filled matrix section
        /// </summary>
        protected virtual MatrixSection GetMatrixSection(MatrixChildQuestionData childQuestions, int sectionNumber, int sectionUid)
        {
            MatrixSection sectionContainer = new MatrixSection();
            sectionContainer.SectionUid = sectionUid;
            sectionContainer.SectionNumber = sectionNumber;
            sectionContainer.ChildQuestions = QuestionItemFactory.CreateQuestionChildCollection(childQuestions, sectionContainer, base.LanguageCode, this.UniqueID + GlobalConfig.AnswerSectionName + sectionUid, AnswerSelectionMode.Radio, base.AnswerStyle, base.RenderMode, base.VoterAnswersState, base.EnableAnswersDefault);
            return sectionContainer;
        }

        protected override SectionAnswersGridItem GetSectionAnswersGrid()
        {
            return null;
        }

        /// <summary>
        /// Returns the number of sections that 
        /// must be restored for this matrix question based
        /// on the current answer state
        /// </summary>
        protected override int GetSectionCountFromState()
        {
            int sectionNumber = 0;
            MatrixChildQuestionData.ChildQuestionsDataTable childQuestions = ((MatrixChildQuestionData) this.DataSource).ChildQuestions;
            if ((base.VoterAnswersState != null) && (childQuestions != null))
            {
                for (int i = 0; i < childQuestions.Rows.Count; i++)
                {
                    VoterAnswersData.VotersAnswersRow[] rowArray = (VoterAnswersData.VotersAnswersRow[]) base.VoterAnswersState.Select("QuestionId = " + ((MatrixChildQuestionData.ChildQuestionsRow) childQuestions.Rows[i]).QuestionId, "SectionNumber DESC");
                    if (((rowArray != null) && (rowArray.Length > 0)) && (rowArray[0].SectionNumber > sectionNumber))
                    {
                        sectionNumber = rowArray[0].SectionNumber;
                    }
                }
            }
            return sectionNumber;
        }

        /// <summary>
        /// Check if any selection was left
        /// </summary>
        /// <param name="questionEventArgs"></param>
        /// <returns></returns>
        protected override bool MaxSelectionsReached(PostedAnswerDataCollection answers)
        {
            if (base.MaxSelectionAllowed > 0)
            {
                base.MaxAnswerSelectionMessage = ResourceManager.GetString("MaxMatrixAnswerSelection", base.LanguageCode);
                foreach (DictionaryEntry entry in this.CountSectionChildQuestionsSelections(answers))
                {
                    Hashtable hashtable2 = (Hashtable) entry.Value;
                    foreach (DictionaryEntry entry2 in hashtable2)
                    {
                        if ((base.MaxSelectionAllowed != 0) && (int.Parse(entry2.Value.ToString()) > base.MaxSelectionAllowed))
                        {
                            return true;
                        }
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
                base.MinAnswerSelectionMessage = ResourceManager.GetString("MinMatrixAnswerSelection", base.LanguageCode);
                Hashtable hashtable = this.CountSectionChildQuestionsSelections(answers);
                if ((base.MinSelectionRequired > 0) && (hashtable.Keys.Count < ((MatrixChildQuestionData) this.DataSource).ChildQuestions.Rows.Count))
                {
                    return true;
                }
                foreach (DictionaryEntry entry in hashtable)
                {
                    Hashtable hashtable2 = (Hashtable) entry.Value;
                    if ((base.MinSelectionRequired > 0) && (hashtable2.Keys.Count <= base.SectionCount))
                    {
                        return true;
                    }
                    foreach (DictionaryEntry entry2 in hashtable2)
                    {
                        if (int.Parse(entry2.Value.ToString()) < base.MinSelectionRequired)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public MatrixChildCollection ChildQuestions
        {
            get
            {
                return this._childQuestions;
            }
            set
            {
                this._childQuestions = value;
            }
        }

        /// <summary>
        /// Datasource that contains the answers, at this
        /// time it must be of an MatrixChildQuestionData type 
        /// </summary>
        public object DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                if (!(value is MatrixChildQuestionData))
                {
                    throw new ArgumentException("DataSource must be an MatrixChildQuestionData instance." + value.GetType());
                }
                this._dataSource = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix items 
        /// </summary>
        [DefaultValue((string) null), PersistenceMode(PersistenceMode.InnerProperty), Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style MatrixAlternatingItemStyle
        {
            get
            {
                if (this._matrixAlternatingItemStyle != null)
                {
                    return this._matrixAlternatingItemStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixAlternatingItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix header
        /// </summary>
        [DefaultValue((string) null), Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style MatrixHeaderStyle
        {
            get
            {
                if (this._matrixHeaderStyle != null)
                {
                    return this._matrixHeaderStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixHeaderStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix items 
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Styles"), DefaultValue((string) null)]
        public Style MatrixItemStyle
        {
            get
            {
                if (this._matrixItemStyle != null)
                {
                    return this._matrixItemStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix table 
        /// </summary>
        [Category("Styles"), DefaultValue((string) null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public Style MatrixStyle
        {
            get
            {
                if (this._matrixStyle != null)
                {
                    return this._matrixStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixStyle = value;
            }
        }
    }
}

