namespace Votations.NSurvey.Data
{
    using System;
    using Votations.NSurvey.WebControls.UI;

    /// <summary>
    /// Answer posted by a question 
    /// </summary>
    public class PostedAnswerData
    {
        private int _answerId;
        private string _fieldText;
        private AnswerItem _item;
        private int _sectionNumber = 0;
        private AnswerTypeMode _typeMode = AnswerTypeMode.None;

        public PostedAnswerData(AnswerItem item, int answerId, int sectionNumber, string fieldText, AnswerTypeMode typeMode)
        {
            this.Item = item;
            this.AnswerId = answerId;
            this.FieldText = fieldText;
            this.TypeMode = typeMode;
            this._sectionNumber = sectionNumber;
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

        public AnswerItem Item
        {
            get
            {
                return this._item;
            }
            set
            {
                this._item = value;
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

