/**************************************************************************************************
	Survey™ Project changes: copyright (c) 2009-2017, W3DevPro™ (https://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)


	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

************************************************************************************************/
using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey;
using Votations.NSurvey.WebControlsFactories;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebControls.UI;
using Votations.NSurvey.Enums;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Creates the content of a survey (questions, breaks etc ...)
    /// </summary>
    public partial class SurveyContentBuilder : PageBase
    {
        //protected SurveyListControl SurveyList;
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.PlaceHolder QuestionListPlaceHolder;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Literal SurveyBuilderTitle;
        protected System.Web.UI.WebControls.DropDownList LanguagesDropdownlist;
        protected System.Web.UI.WebControls.Label PreviewSurveyLanguageLabel;
        new protected HeaderControl Header;


        public string GetScrollQuestionId()
        {
            return Request.QueryString[Constants.Constants.ScrollQuestionQstr];
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.FormBuilder);

            SetupSecurity();
            LocalizePage();

            if (!Page.IsPostBack)
            {
                BindLanguages();
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
            }

            // Build the survey's questions
            BuildQuestionList();
        }

        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.AccessFormBuilder, true);
        }


        private void LocalizePage()
        {
            PreviewSurveyLanguageLabel.Text = GetPageResource("PreviewSurveyLanguageLabel");
            // SurveyBuilderTitle.Text = GetPageResource("SurveyBuilderTitle");
        }


        /// <summary>
        /// Builds the Question list table with
        /// the edit options
        /// </summary>
        private void BuildQuestionList()
        {
            _questionsData = new Questions().GetQuestions(SurveyId, LanguagesDropdownlist.SelectedValue);
            int currentPage = 1,
                previousDisplayOrder = 0,
                totalPages = new Surveys().GetPagesNumber(SurveyId);

            Table questionsContainer = new Table();
            questionsContainer.CssClass = "questionsContainer";
            questionsContainer.ID = "qcT";

            Table questionTable = new Table();
            questionTable.CssClass = "questionBuilder";
            questionTable.ID = "qT";

            TableRow pageBreakRow = BuildRow(null);

            // always add first page 
            questionTable.Rows.Add(pageBreakRow);

            foreach (QuestionData.QuestionsRow question in _questionsData.Questions.Rows)
            {
                while (question.PageNumber > currentPage)
                {
                    var lastq = _questionsData.Questions
                        .AsEnumerable().Where(x => x.PageNumber < currentPage)
                        .OrderByDescending(x => x.DisplayOrder).FirstOrDefault();
                    int displayOrder = lastq == null ? 0 : lastq.DisplayOrder + 1;
                    pageBreakRow.Cells[0].Controls.Add(BuildPageBreakOptionsRow(currentPage, totalPages, displayOrder));

                    currentPage++;
                    questionsContainer.Rows.Add(BuildRow(questionTable));
                    QuestionListPlaceHolder.Controls.Add(questionsContainer);

                    // Creates a new page
                    questionsContainer = new Table();
                    questionsContainer.CssClass = "questionsContainer";
                    questionsContainer.ID = "qcT"+question.QuestionId;

                    questionTable = new Table();
                    questionTable.CssClass = "questionBuilder";
                    questionTable.ID = "qT"+question.QuestionId;

                    pageBreakRow = BuildRow(null);
                    questionTable.Rows.Add(pageBreakRow);
                }

                if (question.PageNumber == currentPage)
                {
                    AddQuestionWebControl(questionTable, question);
                    previousDisplayOrder = question.DisplayOrder;
                }
            }

            pageBreakRow.Cells[0].Controls.Add(BuildPageBreakOptionsRow(currentPage, totalPages, previousDisplayOrder));

            questionsContainer.Rows.Add(BuildRow(questionTable));
            QuestionListPlaceHolder.Controls.Add(questionsContainer);
        }

        /// <summary>
        /// Get a web control instance of the question row 
        /// and adds it with its options to the table 
        /// </summary>
        private void AddQuestionWebControl(Table questionTable, QuestionData.QuestionsRow question)
        {
            QuestionItem questionWebControl = QuestionItemFactory.Create(question, LanguagesDropdownlist.SelectedValue,
                this.UniqueID, 0, null, true, true);

            // Set question's style
            // and bind the data
            Style questionStyle = new Style();
            questionStyle.CssClass = "surveyQuestion";

            Style answerStyle = new Style();
            answerStyle.CssClass = "surveyAnswer";


            if (questionWebControl is ActiveQuestion)
            {
                ((ActiveQuestion)questionWebControl).EnableClientSideValidation = false;
                ((ActiveQuestion)questionWebControl).EnableServerSideValidation = false;
                ((ActiveQuestion)questionWebControl).ValidationMarkStyle.CssClass = "icon-warning-sign"; //GB
            }
            if (questionWebControl is SectionQuestion)
            {
                ((SectionQuestion)questionWebControl).SectionOptionStyle.CssClass = "questionOptions";
                ((SectionQuestion)questionWebControl).ValidationMarkStyle.CssClass = "icon-warning-sign"; //GB
            }
            
            if (questionWebControl is MatrixQuestion)
            {
                ((MatrixQuestion)questionWebControl).MatrixHeaderStyle = answerStyle;
                ((MatrixQuestion)questionWebControl).MatrixItemStyle = answerStyle;
                ((MatrixQuestion)questionWebControl).MatrixAlternatingItemStyle = answerStyle;
                ((MatrixQuestion)questionWebControl).ValidationMarkStyle.CssClass = "icon-warning-sign"; //GB
            }

            questionWebControl.RenderMode = ControlRenderMode.ReadOnly;
            questionWebControl.AnswerStyle = answerStyle;
            questionWebControl.QuestionStyle = questionStyle;
   

            questions.Add(questionWebControl);

            // Add the question and its options to the table
            questionTable.Rows.Add(BuildQuestionOptionsRow(question));

            if (questionWebControl is MatrixQuestion)
            {
                questionTable.Rows.Add(BuildRow(questionWebControl));
            }
            else
                questionTable.Rows.Add(BuildRow(questionWebControl));
        }


        /// <summary>
        /// Builds a row with the options available to the question
        /// </summary>
        /// <returns>a tablerow instance with the options</returns>
        private TableRow BuildQuestionOptionsRow(QuestionData.QuestionsRow question)
        {
            // Creates a new edit options control
            QuestionOptionsControl questionOptions =
                (QuestionOptionsControl)LoadControl("UserControls/QuestionOptionsControl.ascx");
            questionOptions.ID = "QuestionOptions_" + question.QuestionId;
            questionOptions.Question = question;
            questionOptions.QuestionCount = _questionsData.Questions.Rows.Count;
            return BuildRow(questionOptions);
        }

        /// <summary>
        /// Builds a row with the options available for page breaks
        /// </summary>
        /// <returns>a tablerow instance with the options</returns>
        private PageBreakOptionsControl BuildPageBreakOptionsRow(int pageNumber, int totalPages, int previousDisplayOrder)
        {
            // Creates a new edit page break options control
            PageBreakOptionsControl pageBreakOptionsControl =
                (PageBreakOptionsControl)LoadControl("UserControls/PageBreakOptionsControl.ascx");
            pageBreakOptionsControl.ID = "PageBreakOptions_" + pageNumber;
            pageBreakOptionsControl.SurveyId = SurveyId;
            pageBreakOptionsControl.PageNumber = pageNumber;
            pageBreakOptionsControl.TotalPagesNumber = totalPages;
            pageBreakOptionsControl.PreviousQuestionDisplayOrder = previousDisplayOrder;

            return pageBreakOptionsControl;
        }



        private TableRow BuildRow(Control child)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Wrap = true;
            if (child != null)
            {
                cell.Controls.Add(child);
            }
            row.Cells.Add(cell);
            return row;
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LanguagesDropdownlist.SelectedIndexChanged += new System.EventHandler(this.LanguagesDropdownlist_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private QuestionItemCollection questions = new QuestionItemCollection();

        private QuestionData _questionsData = new QuestionData();

        private void BindLanguages()
        {
            MultiLanguageMode languageMode = new MultiLanguages().GetMultiLanguageMode(SurveyId);
            if (languageMode != MultiLanguageMode.None)
            {
                LanguagesDropdownlist.Items.Clear();
                MultiLanguageData surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);
                foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
                {
                    ListItem defaultItem = new ListItem(GetPageLanguageCodes(language.LanguageDescription), language.LanguageCode);
                    if (language.DefaultLanguage)
                    {
                        defaultItem.Value = "";
                        defaultItem.Text += " " + GetPageLanguageCodes("LanguageDefaultText");
                    }

                    LanguagesDropdownlist.Items.Add(defaultItem);
                }

                LanguagesDropdownlist.Visible = true;
                PreviewSurveyLanguageLabel.Visible = true;
                liML.Visible = true;
            }
            else
            {
                LanguagesDropdownlist.Visible = false;
                PreviewSurveyLanguageLabel.Visible = false;
                liML.Visible = false;
            }
        }

        private void LanguagesDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            QuestionListPlaceHolder.Controls.Clear();
            BuildQuestionList();
        }
    }

}
