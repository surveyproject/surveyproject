namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Base class for all question controls that needs to be 
    /// included inside the .nsurvey control
    /// as a question
    /// </summary>
    [Serializable, ToolboxItem(false)]
    public abstract class QuestionItem : WebControl, INamingContainer
    {
        private Style _answerStyle;
        private string _languageCode;
        private int _questionId = -1;
        private int _questionNumber = -1;
        private Style _questionStyle;

        //private Table _questionTable = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;
        private Panel _questionPanel = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentPanel();//GB

        private ControlRenderMode _renderMode = ControlRenderMode.Standard;
        //private Table _selectionTable = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetSectPercentTable();//JJ;
        //TODO SP25
        private Panel _selectionPanel = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetSectPercentPanel();//GB;

        private string _text = null;
        public string QuestionId_Text { get; set; }
        public string HelpText { get; set; }
        public bool ShowHelpText { get; set; }

        protected QuestionItem() : base(HtmlTextWriterTag.Div)
        {
        }


        /// <summary>
        /// Build a row with the given control and style
        /// </summary>
        /// <param name="child"></param>
        /// <param name="rowStyle"></param>
        /// <returns></returns>
        //protected TableRow BuildRow(Control child, Style rowStyle)
        //{
        //    TableRow row = new TableRow();
        //    TableCell cell = new TableCell();
        //    if (child != null)
        //    {
        //        cell.Controls.Add(child);
        //    }
        //    row.Cells.Add(cell);
        //    row.ControlStyle.CopyFrom(rowStyle);
        //    return row;
        //}

        //TODO SP25
        protected Panel BuildPanelRow(Control child, Style rowStyle)
        {
            Panel PanelRow = new Panel();
            Label cell = new Label();

            if (child != null)
            {
                cell.Controls.Add(child);
            }

            PanelRow.Controls.Add(cell);
            cell.ControlStyle.CopyFrom(rowStyle);
            return PanelRow;
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // rows to add warning messages on required Q/A's
            //this.QuestionTable.Rows.Add(this.BuildRow(null, null));
            //this.QuestionTable.Rows[0].EnableViewState = false;
            //this.QuestionTable.Rows.Add(this.BuildRow(null, null));
            //this.QuestionTable.Rows[1].EnableViewState = false;
            
            this._questionPanel.Controls.Add(this.BuildPanelRow(null, null));
            this._questionPanel.Controls[0].EnableViewState = false;
            this._questionPanel.Controls.Add(this.BuildPanelRow(null, null));
            this._questionPanel.Controls[1].EnableViewState = false;


        }

        /// <summary>
        /// Style used for the question's answers
        /// </summary>
        [Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string) null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style AnswerStyle
        {
            get
            {
                if (this._answerStyle != null)
                {
                    return this._answerStyle;
                }
                return (this._answerStyle = new Style());
            }
            set
            {
                this._answerStyle = value;
            }
        }

        /// <summary>
        /// Language in which 
        /// the messages will be 
        /// rendered 
        /// </summary>
        /// <remarks>
        /// if no language is specified
        /// the default thread culture is taken
        /// </remarks>
        public string LanguageCode
        {
            get
            {
                return this._languageCode;
            }
            set
            {
                this._languageCode = value;
            }
        }

        /// <summary>
        /// Id of the question stored in the DB
        /// </summary>
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

        /// <summary>
        /// Number of the Question 
        /// in the survey
        /// </summary>
        public int QuestionNumber
        {
            get
            {
                return this._questionNumber;
            }
            set
            {
                this._questionNumber = value;
            }
        }

        /// <summary>
        /// Sets the style for the question
        /// </summary>
        [DefaultValue((string) null), Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public Style QuestionStyle
        {
            get
            {
                if (this._questionStyle != null)
                {
                    return this._questionStyle;
                }
                return new Style();
            }
            set
            {
                this._questionStyle = value;
            }
        }

        /// <summary>
        /// Table that holds the question's layout
        /// </summary>
        //public Table QuestionTable
        //{
        //    get
        //    {
        //        return this._questionTable;
        //    }
        //    set
        //    {
        //        this._questionTable = value;
        //    }
        //}

        //TODO SP25
        /// <summary>
        /// Panel that holds the question's layout
        /// </summary>
        public Panel QuestionPanel
        {
            get
            {
                return this._questionPanel;
            }
            set
            {
                this._questionPanel = value;
            }
        }



        /// <summary>
        /// Defines in which mode the control is going 
        /// to be rendered eg : if its for edition or if its 
        /// to let the user answer the question
        /// </summary>
        public ControlRenderMode RenderMode
        {
            get
            {
                return this._renderMode;
            }
            set
            {
                this._renderMode = value;
            }
        }

        /// <summary>
        /// Table that holds the question's selections layout
        /// </summary>
        //public Table SelectionTable
        //{
        //    get
        //    {
        //        return this._selectionTable;
        //    }
        //    set
        //    {
        //        this._selectionTable = value;
        //    }
        //}

        //TODO SP25
        public Panel SelectionPanel
        {
            get
            {
                return this._selectionPanel;
            }
            set
            {
                this._selectionPanel = value;
            }
        }

        /// <summary>
        /// Text of the question
        /// </summary>
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        
    }
}

