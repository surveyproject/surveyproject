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

namespace Votations.NSurvey.WebAdmin.UserControls
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Enums;
    using Microsoft.VisualBasic;
    using System.Linq;
    /// <summary>
    /// Survey data CU methods
    /// </summary>
    public partial class FilterOptionControl : UserControl
    {
        public event EventHandler OptionChanged;

        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.TextBox TextBox1;
        protected System.Web.UI.WebControls.DropDownList DropDownList1;
        protected System.Web.UI.WebControls.Button UpdateFilterButton;
        protected System.Web.UI.WebControls.Button DeleteFilterButton;
        protected System.Web.UI.WebControls.Button CreatefilterButton;
        protected System.Web.UI.WebControls.PlaceHolder EditplaceHolder;
        protected System.Web.UI.WebControls.TextBox FilterNameTextBox;
        protected System.Web.UI.WebControls.DropDownList LogicalOperatorDropDownList;
        protected System.Web.UI.WebControls.Repeater RulesRepeater;
        protected System.Web.UI.WebControls.DropDownList AnswerFilterDropdownlist;
        protected System.Web.UI.WebControls.Label AnswerLabel;
        protected System.Web.UI.WebControls.Label FilterText;
        protected System.Web.UI.WebControls.TextBox TextFilterTextbox;
        protected System.Web.UI.WebControls.DropDownList QuestionFilterDropdownlist;
        protected System.Web.UI.WebControls.Button AddRuleButton;
        protected System.Web.UI.WebControls.Label FilterNameLabel;
        protected System.Web.UI.WebControls.Label RuleOperatorLabel;
        protected System.Web.UI.WebControls.Literal NewRuleTitle;
        protected System.Web.UI.WebControls.Label QuestionLabel;
        protected System.Web.UI.WebControls.Label FilterRulesTitle;
        protected System.Web.UI.WebControls.Label filtertitle;

        /// <summary>
        /// Id of the answer type to edit
        /// if no id is given put the 
        /// usercontrol in creation mode
        /// </summary>
        public int FilterId
        {
            get { return (ViewState["FilterId"] == null) ? -1 : int.Parse(ViewState["FilterId"].ToString()); }
            set { ViewState["FilterId"] = value; }
        }

        /// <summary>
        /// Id of the survey to which the type belongs
        /// </summary>
        public int SurveyId
        {
            get { return (ViewState["SurveyID"] == null) ? -1 : int.Parse(ViewState["SurveyID"].ToString()); }
            set { ViewState["SurveyID"] = value; }
        }


        private void Page_Load(object sender, System.EventArgs e)
        {
            LocalizePage();

            MessageLabel.Visible = false;

            // Check if any answer type id has been assigned
            if (FilterId == -1)
            {
                SwitchToCreationMode();
            }
            else
            {
                SwitchToEditionMode();
            }
        }


        private void LocalizePage()
        {
            FilterNameLabel.Text = ((PageBase)Page).GetPageResource("FilterNameLabel");
            RuleOperatorLabel.Text = ((PageBase)Page).GetPageResource("RuleOperatorLabel");
            if (!Page.IsPostBack)
            {
                LogicalOperatorDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("AndOperator"), "0"));
                LogicalOperatorDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("OrOperator"), "1"));
            }
            NewRuleTitle.Text = ((PageBase)Page).GetPageResource("NewRuleTitle");
            QuestionLabel.Text = ((PageBase)Page).GetPageResource("QuestionLabel");
            AnswerLabel.Text = ((PageBase)Page).GetPageResource("AnswerLabel");
            FilterText.Text = ((PageBase)Page).GetPageResource("FilterText");
            FilterRulesTitle.Text = ((PageBase)Page).GetPageResource("FilterRulesTitle");
            AddRuleButton.Text = ((PageBase)Page).GetPageResource("AddRuleButton");
            UpdateFilterButton.Text = ((PageBase)Page).GetPageResource("UpdateFilterButton");
            DeleteFilterButton.Text = ((PageBase)Page).GetPageResource("DeleteFilterButton");
            CreatefilterButton.Text = ((PageBase)Page).GetPageResource("CreatefilterButton");

            autoFilterLabel.Text = ((PageBase)Page).GetPageResource("AutoFilter");
            autoFilterMessageLabel.Text = ((PageBase)Page).GetPageResource("AutoFilterMessage");
            autoFilterQuestionLabel.Text = ((PageBase)Page).GetPageResource("AutoFilterQuestion");

            autoQuestionNamingLabel.Text = ((PageBase)Page).GetPageResource("AutoFilterQuestionNaming");
            autoanswerNaminglabel.Text = ((PageBase)Page).GetPageResource("AutoFilterAnswerLabel");

            autoFilterMsg2title.Text = ((PageBase)Page).GetPageResource("autoFilterMsg2");
            btnAutoFilter.Text = ((PageBase)Page).GetPageResource("btnAutoFilter");
            autofilterInfo2Label.Text = ((PageBase)Page).GetPageResource("autofilterInfo2Label");
            ((PageBase)Page).TranslateListControl(ddlAutoQuestionNaming);
            ((PageBase)Page).TranslateListControl(ddlAutoAnswerNaming);

        }


        /// <summary>
        /// Setup the control in creation mode
        /// </summary>
        private void SwitchToCreationMode()
        {
            // Creation mode
            filtertitle.Text = ((PageBase)Page).GetPageResource("AddNewFilterTitle");
            CreatefilterButton.Visible = true;
            EditplaceHolder.Visible = false;
            UpdateFilterButton.Visible = false;
            DeleteFilterButton.Visible = false;
            AutoPlaceHolder.Visible = true;
            if (!IsPostBack) BindFieldsAutoGenerate();
        }

        /// <summary>
        /// Setup the control in edition mode
        /// </summary>
        private void SwitchToEditionMode()
        {
            filtertitle.Text = ((PageBase)Page).GetPageResource("EditFilterTitle");
            CreatefilterButton.Visible = false;
            EditplaceHolder.Visible = true;
            UpdateFilterButton.Visible = true;
            DeleteFilterButton.Visible = true;
            AutoPlaceHolder.Visible = false;
        }

        /// <summary>
        /// Get the current DB data and fill 
        /// the fields with them
        /// </summary>
        public void BindFields()
        {
            FilterData filterData = new Filters().GetFilterById(FilterId);
            FilterNameTextBox.Text = filterData.Filters[0].Description;
            LogicalOperatorDropDownList.SelectedValue = filterData.Filters[0].LogicalOperatorTypeID.ToString();

            RulesRepeater.DataSource = new Filters().GetRulesForFilter(FilterId);
            RulesRepeater.DataMember = "FilterRules";
            RulesRepeater.DataBind();

            QuestionFilterDropdownlist.DataSource = new Questions().GetAnswerableQuestionList(SurveyId);
            QuestionFilterDropdownlist.DataTextField = "QuestionText";
            QuestionFilterDropdownlist.DataValueField = "QuestionID";
            QuestionFilterDropdownlist.DataBind();
            QuestionFilterDropdownlist.Items.Insert(0, new ListItem(((PageBase)Page).GetPageResource("SelectQuestionMessage"), "-1"));
            BindFieldsAutoGenerate();

            BindFieldsParentFilter(ParentFilterNameDropDownList);
            SelectParentFilter(ParentFilterNameDropDownList, filterData.Filters[0].ParentFilterId);
            //ParentFilterNameDropDownList.SelectedValue = filterData.Filters[0].ParentFilterId.ToString();

            AddRuleButton.Enabled = false;
            AnswerLabel.Visible = false;
            AnswerFilterDropdownlist.Visible = false;
            TextFilterTextbox.Visible = false;
            FilterText.Visible = false;
        }

        private void SelectParentFilter(ListControl ddl, int value)
        {
            try
            {
                ddl.SelectedValue = value.ToString();
            }
            catch (Exception)
            {
                ddl.SelectedValue = "0";
            }
        }

        private void BindFieldsAutoGenerate()
        {
            ddlAutoQuestions.DataSource = new Questions().GetAnswerableQuestionList(SurveyId);

            ddlAutoQuestions.DataTextField = "QuestionText";
            ddlAutoQuestions.DataValueField = "QuestionID";
            ddlAutoQuestions.DataBind();

            BindFieldsParentFilter(ddlAutoFilterParent);
            BindFieldsParentFilter(ParentFilterNameDropDownList);
        }

        private void BindFieldsParentFilter(DropDownList ddl)
        {
            if (ddl == null) return;
            ddl.DataSource = new Filters().GetFilters(SurveyId);
            ddl.DataMember = "Filters";
            ddl.DataTextField = "Description";
            ddl.DataValueField = "FilterID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(((PageBase)Page).GetPageResource("SelectFilterMessage"), "0"));
        }

        protected string FormatRule(string questionText, string answerText, string textFilter)
        {
            if (answerText.Length == 0)
            {
                return string.Format(((PageBase)Page).GetPageResource("FilterRuleNoAnswer"),
                    Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")));
            }
            else if (textFilter.Length == 0)
            {
                return string.Format(((PageBase)Page).GetPageResource("FilterRuleAnswer"),
                    Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
                    Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")));
            }
            else
            {
                return string.Format(((PageBase)Page).GetPageResource("FilterRuleAnswerWithText"),
                    Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
                    Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
                    Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(textFilter, "<[^>]*>", " ")));
            }
        }

        private void QuestionFilterDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (QuestionFilterDropdownlist.SelectedValue != "-1")
            {
                AddRuleButton.Enabled = true;
                AnswerLabel.Visible = true;
                AnswerFilterDropdownlist.Visible = true;
                BindAnswerDropDownList();
            }
            else
            {
                AddRuleButton.Enabled = true;
                AnswerLabel.Visible = false;
                AnswerFilterDropdownlist.Items.Clear();
                AnswerFilterDropdownlist.Visible = false;
                TextFilterTextbox.Visible = false;
                TextFilterTextbox.Text = string.Empty;
                FilterText.Visible = false;
            }
        }

        /// <summary>
        /// Bind the list and mark field answer 
        /// with a negative answer id value
        /// </summary>
        private void BindAnswerDropDownList()
        {
            AnswerData answers = new Answers().GetAnswersList(int.Parse(QuestionFilterDropdownlist.SelectedValue));
            AnswerFilterDropdownlist.Items.Clear();
            AnswerFilterDropdownlist.Items.Add(new ListItem(((PageBase)Page).GetPageResource("AnyAnswerMessage"), "0"));

            foreach (AnswerData.AnswersRow answerRow in answers.Answers)
            {
                if ((((AnswerTypeMode)answerRow.TypeMode & AnswerTypeMode.Field) > 0) ||
                    (((AnswerTypeMode)answerRow.TypeMode & AnswerTypeMode.Custom) > 0) ||
                    (((AnswerTypeMode)answerRow.TypeMode & AnswerTypeMode.DataSource) > 0))
                {
                    // Mark field answer with a negative answerid
                    AnswerFilterDropdownlist.Items.Add(new ListItem(answerRow.AnswerText + " " +
                        ((PageBase)Page).GetPageResource("TextEntryInfo"), (-answerRow.AnswerId).ToString()));
                }
                else
                {
                    AnswerFilterDropdownlist.Items.Add(new ListItem(answerRow.AnswerText + " " +
                        ((PageBase)Page).GetPageResource("SelectionInfo"), answerRow.AnswerId.ToString()));
                }
            }
        }

        private void AnswerFilterDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (int.Parse(AnswerFilterDropdownlist.SelectedValue) < 0)
            {
                TextFilterTextbox.Visible = true;
                FilterText.Visible = true;
            }
            else
            {
                TextFilterTextbox.Text = string.Empty;
                TextFilterTextbox.Visible = false;
                FilterText.Visible = false;
            }
        }
        #region Web Form Designer generated code

        private void InitializeComponent()
        {
            this.UpdateFilterButton.Click += new System.EventHandler(this.UpdateFilterButton_Click);
            this.DeleteFilterButton.Click += new System.EventHandler(this.DeleteFilterButton_Click);
            this.CreatefilterButton.Click += new System.EventHandler(this.CreatefilterButton_Click);
            this.QuestionFilterDropdownlist.SelectedIndexChanged += new System.EventHandler(this.QuestionFilterDropdownlist_SelectedIndexChanged);
            this.AnswerFilterDropdownlist.SelectedIndexChanged += new System.EventHandler(this.AnswerFilterDropdownlist_SelectedIndexChanged);
            this.AddRuleButton.Click += new System.EventHandler(this.AddRuleButton_Click);
            this.RulesRepeater.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.RulesRepeater_ItemCommand);
            this.Load += new System.EventHandler(this.Page_Load);
            btnAutoFilter.Click += new EventHandler(btnAutoFilter_Click);

        }

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        #endregion


        string TrimSplit(string s)
        {
            return s;
        }

        bool IsMatrixQuestion(QuestionData.QuestionsRow q)
        {
            return false;
        }
        public string GetQuestionPart(QuestionData.QuestionsRow q, AnswerData.AnswersRow a)
        {

            switch (ddlAutoQuestionNaming.SelectedValue)
            {
                case "Question":/*
                    
                    if (IsMatrixQuestion(q))
                        return TrimSplit((q.ParentQuestionText + splitChar + q.QuestionText + splitChar + q.AnswerText));
                    else */
                    return TrimSplit(q.QuestionText);

                case "QuestionDisplayOrderNumber":/*
                    if (IsMatrixQuestion(q))
                        return TrimSplit(q.DisplayOrder.ToString() + splitChar + q.RowOrder.ToString() + splitChar + q.AnswerText);
                    else */
                    return TrimSplit(q.DisplayOrder.ToString());

                case "QuestionID":/*
                    if (IsMatrixQuestion(q))
                        return TrimSplit((q.IsParentQuestionIdTextNull() ? string.Empty : q.ParentQuestionIdText) +
                            splitChar + q.RowOrder.ToString() +
                            splitChar + (q.IsAnswerTextNull() ? string.Empty : q.AnswerText));
                    else */
                    return TrimSplit(q.IsQuestionIdTextNull() ? q.QuestionText : q.QuestionIdText);

                case "QuestionAlias":/*
                    if (IsMatrixQuestion(q))
                        return TrimSplit(q.ParentQuestionAliasText.ToString()
                            + splitChar + q.RowOrder.ToString() + splitChar + q.AnswerText);
                    else */

                    return TrimSplit(q.IsAliasNull() ? q.QuestionText : q.Alias);

                default: return TrimSplit("Invalid DDl Value");
            }

        }

        public string GetAnswerPart(AnswerData.AnswersRow a)
        {
            switch (ddlAutoAnswerNaming.SelectedValue)
            {

                case "Answer": return a.AnswerText;
                case "AnswerDisplayOrderNumber":/*
                    if (!a.IsRowOrderNull())
                        return TrimSplit(a.AnswerDisplayOrder.ToString());
                    else*/
                    return a.DisplayOrder.ToString();
                case "AnswerID": return a.IsAnswerIDTextNull() ? a.AnswerText : a.AnswerIDText;
                case "AnswerAlias": return a.IsAnswerAliasNull() ? a.AnswerText : a.AnswerAlias;

                case "SliderRange": return a.IsSliderRangeNull() ? a.AnswerText : a.SliderRange;
                case "SliderValue": return a.IsSliderValueNull() ? a.AnswerText : a.SliderValue.ToString();
                case "SliderMin": return a.IsSliderMinNull() ? a.AnswerText : a.SliderMin.ToString();
                case "SliderMax": return a.IsSliderMaxNull() ? a.AnswerText : a.SliderMax.ToString();
                case "SliderAnimate": return a.IsSliderAnimateNull() ? a.AnswerText : a.SliderAnimate.ToString();
                case "SliderStep": return a.IsSliderAnimateNull() ? a.AnswerText : a.SliderStep.ToString();

                default: return "Invalid DDl Value";
            }
        }

        private string GetSubstr(string inp)
        {
            int maxLen = 32;
            return inp.Length <= maxLen ? inp : inp.Substring(0, maxLen);
        }


        public string FilterName(QuestionData.QuestionsRow q, AnswerData.AnswersRow a, string splitChar)
        {
            return GetSubstr(GetQuestionPart(q, a)) + splitChar + GetSubstr(GetAnswerPart(a));
        }

        public string FilterName(QuestionData.QuestionsRow q, AnswerData.AnswersRow a, FilterData.FiltersRow f, string splitChar)
        {
            return GetSubstr(GetFilterPart(f, splitChar)) + splitChar + FilterName(q, a, splitChar);
        }

        public string FilterName(QuestionData.QuestionsRow q, AnswerData.AnswersRow a, FilterData.FiltersRow f)
        {
            const string splitChar = "|";

            return f.FilterId == 0
                ? FilterName(q, a, splitChar)
                : FilterName(q, a, f, splitChar);
        }

        private string GetFilterPart(FilterData.FiltersRow f, string splitChar)
        {
            var split = f.Description.Split(new[] { splitChar[0] });

            return split.Any()
                ? split[0]
                : f.Description;
        }

        void AddFilter(QuestionData.QuestionsRow q, AnswerData.AnswersRow a, FilterData.FiltersRow p)
        {
            FilterData filterData = new FilterData();
            FilterData.FiltersRow filterRow = filterData.Filters.NewFiltersRow();
            filterRow.LogicalOperatorTypeID = short.Parse(LogicalOperatorDropDownList.SelectedValue);
            filterRow.Description = FilterName(q, a, p);
            filterRow.SurveyId = SurveyId;
            filterRow.ParentFilterId = p.FilterId;
            filterData.Filters.AddFiltersRow(filterRow);
            new Filter().AddFilter(filterData);

            FilterRuleData filterRuleData = new FilterRuleData();
            FilterRuleData.FilterRulesRow filterRuleRow = filterRuleData.FilterRules.NewFilterRulesRow();
            filterRuleRow.QuestionId = q.QuestionId;
            filterRuleRow.AnswerId = a.AnswerId;
            filterRuleRow.FilterId = filterRow.FilterId;
            filterRuleData.FilterRules.AddFilterRulesRow(filterRuleRow);
            new Filter().AddRule(filterRuleData);
        }

        int GenerateFilter()
        {
            int questionId = Convert.ToInt32(ddlAutoQuestions.SelectedValue);
            if (questionId == -1) return 0;
            var q = new Questions().GetAnswerableQuestionList(SurveyId).Questions.Single(x => x.QuestionId == questionId);
            var parentFilterId = Convert.ToInt32(ddlAutoFilterParent.SelectedValue);
            FilterData.FiltersRow p = parentFilterId == 0
                ? new Filters().GetEmptyFilter().Filters.FindByFilterId(parentFilterId)
                : new Filters().GetFilterById(parentFilterId).Filters.FindByFilterId(parentFilterId);
            int filterCount = 0;

            foreach (AnswerData.AnswersRow a in new Answers().GetAnswersList(questionId).Answers)
            {

                if (((AnswerTypeEnum)a.AnswerTypeId) == AnswerTypeEnum.SelectionTextType)
                {
                    AddFilter(q, a, p);
                    filterCount++;
                }
            }
            return filterCount;
        }

        void btnAutoFilter_Click(object sender, EventArgs e)
        {
            if (GenerateFilter() > 0)
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("FilterGeneratedMsg"));
            else
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("FilterNotGeneratedMsg"));
        }
        private void AddRuleButton_Click(object sender, System.EventArgs e)
        {
            FilterRuleData filterRuleData = new FilterRuleData();
            FilterRuleData.FilterRulesRow filterRule = filterRuleData.FilterRules.NewFilterRulesRow();

            int questionId = int.Parse(QuestionFilterDropdownlist.SelectedValue),
                answerId = int.Parse(AnswerFilterDropdownlist.SelectedValue);
            if (answerId == 0)
            {
                filterRule.SetAnswerIdNull();
            }
            else if (answerId < 0)
            {
                filterRule.AnswerId = -answerId;
            }
            else
            {
                filterRule.AnswerId = answerId;
            }

            filterRule.QuestionId = questionId;
            if (TextFilterTextbox.Visible)
            {
                filterRule.TextFilter = TextFilterTextbox.Text;
            }
            filterRule.FilterId = FilterId;
            filterRuleData.FilterRules.AddFilterRulesRow(filterRule);
            new Filter().AddRule(filterRuleData);
            MessageLabel.Visible = true;
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("FilterRuleAddedMessage"));
            BindFields();
        }


        private void RulesRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            new Filter().DeleteRule(int.Parse(e.CommandArgument.ToString()));
            MessageLabel.Visible = true;
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("FilterRuleDeletedMessage"));
            BindFields();
        }

        private void DeleteFilterButton_Click(object sender, System.EventArgs e)
        {
            new Filter().DeleteFilter(FilterId);
            MessageLabel.Visible = true;
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("FilterDeletedMessage"));
            this.Visible = false;
            OnOptionChanged();
        }


        private void DeleteFilter(int filterId)
        {
            var childFilters = new Filters().GetFiltersByParent(SurveyId, filterId);
            foreach (var filter in childFilters.Filters.OfType<FilterData.FiltersRow>())
            {
                DeleteFilter(filter.FilterId);
            }

            new Filter().DeleteFilter(filterId);
        }

        protected void OnOptionChanged()
        {
            if (OptionChanged != null)
            {
                OptionChanged(this, EventArgs.Empty);
            }
        }

        private void UpdateFilterButton_Click(object sender, System.EventArgs e)
        {
            if (FilterNameTextBox.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidFilterDescriptionMessage"));
            }
            else
            {
                FilterData filterData = new FilterData();
                FilterData.FiltersRow filterRow = filterData.Filters.NewFiltersRow();
                filterRow.LogicalOperatorTypeID = short.Parse(LogicalOperatorDropDownList.SelectedValue);
                filterRow.Description = FilterNameTextBox.Text;
                filterRow.FilterId = FilterId;
                filterRow.ParentFilterId = int.Parse(ParentFilterNameDropDownList.SelectedValue);
                filterData.Filters.AddFiltersRow(filterRow);
                new Filter().UpdateFilter(filterData);
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("FilterUpdatedMessage"));
            }
        }

        private void CreatefilterButton_Click(object sender, System.EventArgs e)
        {
            if (FilterNameTextBox.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidFilterDescriptionMessage"));
            }
            else
            {
                FilterData filterData = new FilterData();
                FilterData.FiltersRow filterRow = filterData.Filters.NewFiltersRow();
                filterRow.LogicalOperatorTypeID = short.Parse(LogicalOperatorDropDownList.SelectedValue);
                filterRow.Description = FilterNameTextBox.Text;
                filterRow.SurveyId = SurveyId;
                filterRow.ParentFilterId = int.Parse(ParentFilterNameDropDownList.SelectedValue);
                filterData.Filters.AddFiltersRow(filterRow);
                new Filter().AddFilter(filterData);
                UINavigator.NavigateToFilterEditor(SurveyId, ((PageBase)Page).MenuIndex);
            }
        }
    }
}
