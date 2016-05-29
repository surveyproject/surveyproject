/**************************************************************************************************
	Survey changes: copyright (c) 2010, Fryslan Webservices TM (http://survey.codeplex.com)	

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
using Microsoft.VisualBasic;
using MetaBuilders.WebControls;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Enums;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;


namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Edit form for a single question
    /// </summary>
    public partial class EditSingleQuestionAnswers : PageBase
    {
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.Literal AnswerTypeLabel;
        protected System.Web.UI.WebControls.DropDownList AnswerTypeDropDownList;
        protected System.Web.UI.WebControls.Literal Literal1;
        protected System.Web.UI.WebControls.Literal AnswerURLLabel;
        protected System.Web.UI.WebControls.Literal SelectedAnswersLabel;
        protected System.Web.UI.WebControls.Literal DefaultTextLabel;
        protected System.Web.UI.WebControls.CheckBox SelectionCheckBox;
        protected System.Web.UI.WebControls.TextBox AnswerImageURLTextBox;
        protected System.Web.UI.WebControls.TextBox DefaultTextTextBox;
        protected System.Web.UI.WebControls.CheckBox RatingPartCheckbox;
        protected System.Web.UI.WebControls.TextBox AnswerTextTextBox;
        protected System.Web.UI.WebControls.Button BackToQuestionButton;
        protected System.Web.UI.WebControls.Literal Literal3;
        protected System.Web.UI.WebControls.TextBox ScoreTextBox;
        protected System.Web.UI.WebControls.Label PipeHelpLabel;
        protected System.Web.UI.WebControls.Literal SingleQuestionAnswerEditorTitle;
        protected System.Web.UI.WebControls.Label DisplayAnswersOfLabel;
        protected System.Web.UI.WebControls.DropDownList QuestionsDropDownList;
        protected System.Web.UI.WebControls.Button EditQuestionButton;
        protected System.Web.UI.WebControls.Literal AnswersOverviewTitle;
        protected System.Web.UI.WebControls.DataGrid AnswersDataGrid;
        protected System.Web.UI.WebControls.Button AddNewAnswerButton;
        protected System.Web.UI.WebControls.PlaceHolder AnswerOverviewPlaceHolder;
        new protected HeaderControl Header;
        protected AnswerOptionControl AnswerOption;
        protected System.Web.UI.WebControls.DropDownList LanguagesDropdownlist;
        protected System.Web.UI.WebControls.Label EditionLanguageLabel;


        protected bool MultipleSelection
        {
            get { return (bool)ViewState["MultipleSelection"]; }
            set { ViewState["MultipleSelection"] = value; }
        }

        int getLibraryId()
        {
            if (ViewState["LibraryId"] == null) return -1;
            return (int)ViewState["LibraryId"];
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.FormBuilder);

            SetupSecurity();
            MessageLabel.Visible = false;
            LocalizePage();
            ValidateRequestQuestionId();
            AnswerOption.SurveyId = getSurveyId();

            if (_questionId != -1)
            {
                SetQuestionOptions();
            }
            else
            {
                AnswerOverviewPlaceHolder.Visible = false;
            }
            if (!Page.IsPostBack)
            {
                // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = getSurveyId();
                BindData();
                BindLanguages();


                if (_questionId != -1)
                {
                    BindAnswerOptions();
                }
            }

            AnswerOption.OptionChanged += new EventHandler(SurveyOption_OptionChanged);
        }

        protected void OnBackButton(object sender, System.EventArgs e)
        {
            int _questionId = -1;
            if (!(Request.QueryString["QuestionId"] != null && int.TryParse(Request.QueryString["QuestionId"], out _questionId)))
                _questionId = -1;
            Response.Redirect(UINavigator.LibraryTemplateReturnUrl() == null ?
               UINavigator.SurveyContentBuilderLink + (_questionId == -1 ? string.Empty : "?" + Constants.Constants.ScrollQuestionQstr + "=" + _questionId.ToString()) : UINavigator.LibraryTemplateReturnUrl());
        }

        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.AccessFormBuilder, true);
        }

        /// <summary>
        ///	Triggered by the answer option user control
        ///	in case anything was changed we will need to
        ///	rebind to display the updates to the user 
        /// </summary>
        void SurveyOption_OptionChanged(object sender, EventArgs e)
        {
            BindAnswerOptions();
        }

        void BindData()
        {
            if (_question.Questions.Rows.Count > 0 &&
                !_question.Questions[0].IsLibraryIdNull())
            {
                ViewState["LibraryId"] = _question.Questions[0].LibraryId;
                QuestionsDropDownList.DataSource =
                    new Questions().GetLibraryAnswerableSingleQuestionListWithoutChilds(int.Parse(Request["libraryid"]));

                if (!NSurveyUser.Identity.IsAdmin)
                {
                    CheckRight(NSurveyRights.ManageLibrary, true);
                }
            }
            else if (Information.IsNumeric(Request["libraryid"]) && Request["libraryid"] != "-1")
            {
                ViewState["LibraryId"] = int.Parse(Request["libraryid"]);
                QuestionsDropDownList.DataSource =
                    new Questions().GetLibraryAnswerableSingleQuestionListWithoutChilds(int.Parse(Request["libraryid"]));

                if (!NSurveyUser.Identity.IsAdmin)
                {
                    CheckRight(NSurveyRights.ManageLibrary, true);
                }
            }
            else
            {
                ViewState["LibraryId"] = -1;
                QuestionsDropDownList.DataSource =
                    new Questions().GetAnswerableSingleQuestionListWithoutChilds(SurveyId);
            }

            QuestionsDropDownList.DataTextField = "QuestionText";
            QuestionsDropDownList.DataValueField = "QuestionId";
            QuestionsDropDownList.DataBind();

            if (int.Parse(ViewState["LibraryId"].ToString()) == -1)
            {
                QuestionsDropDownList.Items.Insert(0,
                    new ListItem(GetPageResource("SelectQuestionMessage"), "-1"));
            }
            else
            {
                QuestionsDropDownList.Items.Insert(0,
                    new ListItem(GetPageResource("SelectQuestionFromLibraryMessage"), "-1"));
            }

            QuestionsDropDownList.SelectedValue = _questionId.ToString();
            if (QuestionsDropDownList.Items.Count == 1)
            {
                EditQuestionButton.Enabled = false;
            }
        }

        private void BindLanguages()
        {
            MultiLanguageMode languageMode = MultiLanguageMode.UserSelection;
            if (getLibraryId() == -1)
                languageMode = new MultiLanguages().GetMultiLanguageMode(SurveyId);
            if (languageMode != MultiLanguageMode.None || getLibraryId() != -1)
            {
                LanguagesDropdownlist.Items.Clear();
                MultiLanguageData surveyLanguages;
                if (getLibraryId() == -1)
                    surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);
                else
                    surveyLanguages = new MultiLanguages().GetSurveyLanguages(getLibraryId(), Constants.Constants.EntityLibrary);

                foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
                {
                    ListItem defaultItem = new ListItem(GetPageResource(language.LanguageDescription), language.LanguageCode);
                    if (language.DefaultLanguage)
                    {
                        defaultItem.Value = "";
                        defaultItem.Text += " " + GetPageResource("LanguageDefaultText");
                    }

                    LanguagesDropdownlist.Items.Add(defaultItem);
                }

                LanguagesDropdownlist.Visible = true;
                EditionLanguageLabel.Visible = true;
            }
            else
            {
                LanguagesDropdownlist.Visible = false;
                EditionLanguageLabel.Visible = false;
            }
        }


        private void LocalizePage()
        {
            AddNewAnswerButton.Text = GetPageResource("AddNewAnswerButton");
            SingleQuestionAnswerEditorTitle.Text = GetPageResource("SingleQuestionAnswerEditorTitle");
            AnswersOverviewTitle.Text = GetPageResource("AnswersOverviewTitle");
            DisplayAnswersOfLabel.Text = GetPageResource("DisplayAnswersOfLabel");
            ((TemplateColumn)AnswersDataGrid.Columns[0]).HeaderText = GetPageResource("OrderHeader");
            ((TemplateColumn)AnswersDataGrid.Columns[1]).HeaderText = GetPageResource("SelectedHeader");
            ((TemplateColumn)AnswersDataGrid.Columns[2]).HeaderText = GetPageResource("AnswerTextHeader");
            ((TemplateColumn)AnswersDataGrid.Columns[3]).HeaderText = GetPageResource("TypeHeader");
            ((TemplateColumn)AnswersDataGrid.Columns[4]).HeaderText = GetPageResource("RatingHeader");
            ((TemplateColumn)AnswersDataGrid.Columns[5]).HeaderText = GetPageResource("ScoreHeader");

            ((TemplateColumn)AnswersDataGrid.Columns[6]).HeaderText = GetPageResource("ButtonDeleteColumn");

            //((EditCommandColumn)AnswersDataGrid.Columns[7]).EditText = GetPageResource("EditText");
            //((ButtonColumn)AnswersDataGrid.Columns[8]).Text = GetPageResource("ButtonDeleteColumn");

            EditionLanguageLabel.Text = GetPageResource("EditionLanguageLabel");
            EditQuestionButton.Text = GetPageResource("EditQuestionButton");
        }


        private void ValidateRequestQuestionId()
        {
            if (QuestionsDropDownList.SelectedItem != null && QuestionsDropDownList.SelectedValue != "-1")
            {
                _questionId = int.Parse(QuestionsDropDownList.SelectedValue);
            }
            else
            {
                _questionId =
                    Information.IsNumeric(Request["QuestionId"]) ? int.Parse(Request["QuestionId"]) : -1;
            }

            if (_questionId != -1 && !NSurveyUser.Identity.IsAdmin &&
                !NSurveyUser.Identity.HasAllSurveyAccess)
            {
                if (!new Question().CheckQuestionUser(_questionId, NSurveyUser.Identity.UserId))
                {
                    _questionId = -1;
                }
            }

        }

        /// <summary>
        /// Set the forms to match DB question options
        /// </summary>
        private void SetQuestionOptions()
        {
            // Retrieve the original question values
            _question = new Questions().GetQuestionById(_questionId, null);
            QuestionData.QuestionsRow questionRow = _question.Questions[0];

            // Can the question have answers
            if (!(((QuestionTypeMode)questionRow.TypeMode & QuestionTypeMode.Answerable) > 0) ||
                (((QuestionTypeMode)questionRow.TypeMode & QuestionTypeMode.ChildQuestion) > 0)
                )
            {
                AnswersDataGrid.Visible = false;
                AddNewAnswerButton.Visible = false;
                AnswerOption.Visible = false;
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("SingleQuestionCannotBeEditedMessage"));
            }
            else
            {
                MultipleSelection = ((QuestionTypeMode)questionRow.TypeMode & QuestionTypeMode.MultipleAnswers) > 0;
                _ratingEnabled = questionRow.RatingEnabled;
                _scoreEnabled = _question.Questions[0].IsSurveyIdNull() ? true :
                        new Surveys().IsSurveyScored(SurveyId);

                AnswersDataGrid.Columns[4].Visible = _ratingEnabled;
                AnswersDataGrid.Columns[5].Visible = _scoreEnabled;
            }
        }

        private void BindAnswerOptions()
        {
            _answerTypes = new AnswerTypes().GetAnswerTypes();
            _answers = new Answers().GetAnswers(_questionId, LanguagesDropdownlist.SelectedValue);
            if (_answers.Answers.Rows.Count > 0)
            {
                AnswersDataGrid.Visible = true;
                AnswersDataGrid.DataSource = _answers;
                AnswersDataGrid.DataMember = "Answers";
                AnswersDataGrid.DataKeyField = "AnswerId";
                AnswersDataGrid.DataBind();
            }
            else
            {
                AnswersDataGrid.Visible = false;
            }

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
            this.QuestionsDropDownList.SelectedIndexChanged += new System.EventHandler(this.QuestionsDropDownList_SelectedIndexChanged);
            this.EditQuestionButton.Click += new System.EventHandler(this.EditQuestionButton_Click);
            this.LanguagesDropdownlist.SelectedIndexChanged += new System.EventHandler(this.LanguagesDropdownlist_SelectedIndexChanged);
            this.AnswersDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.AnswersDataGrid_ItemCommand);
            this.AnswersDataGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.AnswersDataGrid_EditCommand);
            this.AnswersDataGrid.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.AnswersDataGrid_DeleteCommand);
            this.AnswersDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.AnswersDataGrid_ItemDataBound);
            this.AddNewAnswerButton.Click += new System.EventHandler(this.AddNewButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        /// <summary>
        /// Generates the templated column with the
        /// selection mode
        /// </summary>
        private void AnswersDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            DataGridItem gridItem = (DataGridItem)e.Item;
            if (gridItem.ItemType != ListItemType.Footer && gridItem.ItemType != ListItemType.Header)
            {
                AnswerTypeData typeData = new AnswerTypes().GetAnswerTypeById(int.Parse(_answers.Answers[gridItem.DataSetIndex].AnswerTypeId.ToString()));
                gridItem.Cells[3].Text = GetPageResource(typeData.AnswerTypes[0].Description) != null ?
                    GetPageResource(typeData.AnswerTypes[0].Description) : typeData.AnswerTypes[0].Description;

                int typeMode = int.Parse(DataBinder.Eval(e.Item.DataItem, "TypeMode").ToString());
                bool ratePart = (bool)DataBinder.Eval(gridItem.DataItem, "RatePart");

                //				// Can this answer be selected ?
                if ((((AnswerTypeMode)typeMode & AnswerTypeMode.Selection) > 0))
                {
                    if (MultipleSelection)
                    {
                        CheckBox check = (CheckBox)gridItem.Cells[1].FindControl("DefaultCheckBox");
                        check.Checked = (bool)DataBinder.Eval(e.Item.DataItem, "Selected");
                        check.Visible = true;
                    }
                    else
                    {
                        GlobalRadioButton radio = (GlobalRadioButton)gridItem.Cells[1].FindControl("DefaultRadio");
                        radio.Checked = (bool)DataBinder.Eval(e.Item.DataItem, "Selected");
                        radio.Visible = true;
                    }

                    if (ratePart)
                    {
                        ((Label)gridItem.FindControl("RatingLabel")).Text = _currentRating.ToString();
                        _currentRating++;
                    }
                    else
                    {
                        ((Label)gridItem.FindControl("RatingLabel")).Text = "0";
                    }

                    if (_scoreEnabled)
                    {
                        if (DataBinder.Eval(e.Item.DataItem, "ScorePoint") != null &&
                            DataBinder.Eval(e.Item.DataItem, "ScorePoint").ToString().Length > 0)
                        {
                            ((Label)gridItem.FindControl("ScorePoint")).Text = DataBinder.Eval(e.Item.DataItem, "ScorePoint").ToString();
                        }
                        else
                        {
                            ((Label)gridItem.FindControl("ScorePoint")).Text = "0";
                        }
                    }
                }
                else
                {
                    gridItem.Cells[1].FindControl("DefaultRadio").Visible = false;
                    gridItem.Cells[1].FindControl("DefaultCheckBox").Visible = false;
                    ((Label)gridItem.FindControl("RatingLabel")).Text = "n/a";
                    ((Label)gridItem.FindControl("ScorePoint")).Text = "n/a";
                }
            }

        }


        /// <summary>
        /// Deletes the answer row
        /// </summary>
        private void AnswersDataGrid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            new Answer().DeleteAnswer(
                int.Parse(AnswersDataGrid.DataKeys[e.Item.ItemIndex].ToString()));

            MessageLabel.Visible = true;
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SelectionDeletedMessage"));

            if (AnswerOption.AnswerId == int.Parse(AnswersDataGrid.DataKeys[e.Item.ItemIndex].ToString()))
            {
                AnswerOption.AnswerId = -1;
                AnswerOption.Visible = false;
            }

            BindAnswerOptions();
        }


        private void AddNewButton_Click(object sender, System.EventArgs e)
        {
            AnswerOption.Visible = true;
            AnswerOption.AnswerId = -1;
            AnswerOption.QuestionId = _questionId;
            AnswerOption.RatingEnabled = _ratingEnabled;
            AnswerOption.ScoreEnabled = _scoreEnabled;
            AnswerOption.BindData();
           // BindAnswerOptions();
        }

        private void AnswersDataGrid_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            AnswerOption.Visible = true;
            AnswerOption.AnswerId = int.Parse(AnswersDataGrid.DataKeys[e.Item.ItemIndex].ToString());
            AnswerOption.QuestionId = _questionId;
            AnswerOption.RatingEnabled = _ratingEnabled;
            AnswerOption.ScoreEnabled = _scoreEnabled;
            AnswerOption.BindData();
            BindAnswerOptions();
        }

        private void AnswersDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "up")
            {
                new Answer().MoveAnswerUp(int.Parse(AnswersDataGrid.DataKeys[e.Item.ItemIndex].ToString()));
                // Rebind the answers
                BindAnswerOptions();
            }
            else if (e.CommandName == "down")
            {
                new Answer().MoveAnswerDown(int.Parse(AnswersDataGrid.DataKeys[e.Item.ItemIndex].ToString()));
                // Rebind the answers
                BindAnswerOptions();
            }
        }

        private void QuestionsDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (QuestionsDropDownList.SelectedValue == "-1")
            {
                AnswerOption.Visible = false;
                AnswerOverviewPlaceHolder.Visible = false;
            }
            else
            {
                _questionId = int.Parse(QuestionsDropDownList.SelectedValue);
                AnswerOverviewPlaceHolder.Visible = true;
                AnswerOption.Visible = false;
                SetQuestionOptions();
                BindAnswerOptions();
            }

        }

        private void EditQuestionButton_Click(object sender, System.EventArgs e)
        {
            if (_questionId == -1)
            {
                return;
            }
            else
            {
                UINavigator.NavigateToSingleQuestionEdit(SurveyId, _questionId, (int)ViewState["LibraryId"], MenuIndex);
            }
        }

        private void LanguagesDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindAnswerOptions();
            AnswerOption.LanguageCode = LanguagesDropdownlist.SelectedValue;
            AnswerOption.BindData();
        }


        int _questionId,
            _currentRating = 1;
        bool
            _ratingEnabled = false,
            _scoreEnabled = false;
        AnswerData _answers;
        AnswerTypeData _answerTypes;
        QuestionData _question = new QuestionData();


    }

}
