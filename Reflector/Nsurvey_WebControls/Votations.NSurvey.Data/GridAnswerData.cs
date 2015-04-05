namespace Votations.NSurvey.Data
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Answer that is stored in a section grid 
    /// </summary>
    [Serializable, TypeConverter(typeof(GridAnswerDataConverter))]
    public class GridAnswerData
    {
        private int _answerId;
        private string _fieldText;
        private int _questionId;
        private int _sectionNumber = 0;
        private AnswerTypeMode _typeMode;

        public GridAnswerData(int questionId, int answerId, int sectionNumber, string fieldText, AnswerTypeMode typeMode)
        {
            this.QuestionId = this.QuestionId;
            this.AnswerId = answerId;
            this.FieldText = fieldText;
            this.TypeMode = typeMode;
            this.SectionNumber = sectionNumber;
        }

        public int AnswerId
        {
            get
            {
                return this._answerId;
            }
            set
            {
                this._answerId = value;
            }
        }

        public string FieldText
        {
            get
            {
                return this._fieldText;
            }
            set
            {
                this._fieldText = value;
            }
        }

        public int QuestionId
        {
            get
            {
                return this._questionId;
            }
            set
            {
                this._questionId = value;
            }
        }

        public int SectionNumber
        {
            get
            {
                return this._sectionNumber;
            }
            set
            {
                this._sectionNumber = value;
            }
        }

        public AnswerTypeMode TypeMode
        {
            get
            {
                return this._typeMode;
            }
            set
            {
                this._typeMode = value;
            }
        }
    }
}

