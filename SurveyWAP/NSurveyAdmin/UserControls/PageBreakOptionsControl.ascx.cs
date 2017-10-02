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
	using System.Web.UI.WebControls;
	using Votations.NSurvey.Data;
	using Votations.NSurvey.BusinessRules;
	using Votations.NSurvey.DataAccess;
	using Votations.NSurvey.Enums;

	/// <summary>
	///	Handles the survey's page breaks options	
	/// </summary>
	public partial class PageBreakOptionsControl : System.Web.UI.UserControl
	{

		// Current page 
		public int PageNumber
		{
			get { return _pageNumber; }
			set { _pageNumber = value; }
		}

		// Total pages in the survey
		public int TotalPagesNumber
		{
			get { return _totalPagesNumber; }
			set { _totalPagesNumber = value; }
		}

		public int SurveyId
		{
			get { return _surveyId; }
			set { _surveyId = value; }
		}

		// Order of the question before this new page
		public int PreviousQuestionDisplayOrder
		{
			get { return _previousQuestionDisplayOrder; }
			set { _previousQuestionDisplayOrder = value; }
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			InitPageOptions();
			LocalizePage();

			PageLabel.Text = PageNumber.ToString();
			PageBranchingRules.PageNumber = PageNumber;
			PageBranchingRules.SurveyId = SurveyId;
			PageBranchingRules.BindData();

			if (PreviousQuestionDisplayOrder == 0 )
			{
				// No more question before page break
				UpImageButton.Visible = false;
				InsertHyperLink.NavigateUrl = 
					String.Format(UINavigator.InsertQuestionLink+"?SurveyID={0}&DisplayOrder={1}&Page={2}&MenuIndex={3}", 
					SurveyId, PageNumber,PreviousQuestionDisplayOrder, ((PageBase)Page).MenuIndex);
				EnableSubmitHyperlink.Enabled = false;
			}
			else
			{
				InsertHyperLink.NavigateUrl = 
					String.Format("{0}?SurveyID={1}&DisplayOrder={2}&Page={3}&MenuIndex={4}", 
					UINavigator.InsertQuestionLink, SurveyId, _previousQuestionDisplayOrder, PageNumber, ((PageBase)Page).MenuIndex);
			}
		
			if (PageNumber < TotalPagesNumber)
			{
				BranchingHyperLink.NavigateUrl = string.Format("{0}?SurveyID={1}&Page={2}&MenuIndex={3}",
					UINavigator.EditPageBranching, SurveyId, PageNumber, ((PageBase)Page).MenuIndex);
			}
			else
			{
				EnableSubmitHyperlink.Enabled = false;
				DownImageButton.Visible = false;
				BranchingHyperLink.Enabled = false;
			}
			DeleteButton.Attributes.Add("onClick",
					"javascript:if(confirm('" + ((PageBase)Page).GetPageResource("DeleteQuestionPageConfirmationMessage") + "')== false) return false;");
		}

		private void LocalizePage()
		{
			BranchingHyperLink.Text = ((PageBase)Page).GetPageResource("BranchingHyperLink");
			InsertHyperLink.Text = ((PageBase)Page).GetPageResource("InsertHyperLink");
			InsertLineBreakButton.Text = ((PageBase)Page).GetPageResource("InsertLineBreakButton");
			DeleteButton.Text = ((PageBase)Page).GetPageResource("DeleteButton");
			PageTitle.Text = ((PageBase)Page).GetPageResource("PageTitle");
			EnableRandomHyperlink.Text = (_enableRandomize) ?
				((PageBase)Page).GetPageResource("DisableRandomHyperlink") : ((PageBase)Page).GetPageResource("EnableRandomHyperlink");
            EnableRandomHyperlink.ForeColor = (_enableRandomize) ?
                System.Drawing.ColorTranslator.FromHtml("Red") : System.Drawing.ColorTranslator.FromHtml("black");
			EnableSubmitHyperlink.Text = (_enableSubmit) ?
				((PageBase)Page).GetPageResource("DisableSubmitHyperlink") : ((PageBase)Page).GetPageResource("EnableSubmitHyperlink");
            EnableSubmitHyperlink.ForeColor = (_enableSubmit) ?
                System.Drawing.ColorTranslator.FromHtml("Red") : System.Drawing.ColorTranslator.FromHtml("black");
		}

		private void InitPageOptions()
		{
			PageOptionData pageOptions = new Surveys().GetSurveyPageOptions(SurveyId, PageNumber);
			if (pageOptions.PageOptions.Rows.Count > 0)
			{
				_enableRandomize = pageOptions.PageOptions[0].IsRandomizeQuestionsNull() ? false : pageOptions.PageOptions[0].RandomizeQuestions;
				_enableSubmit = pageOptions.PageOptions[0].IsEnableSubmitButtonNull() ? false : pageOptions.PageOptions[0].EnableSubmitButton;
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.UpImageButton.Click += new System.Web.UI.ImageClickEventHandler(this.UpImageButton_Click);
			this.DownImageButton.Click += new System.Web.UI.ImageClickEventHandler(this.DownImageButton_Click);
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.InsertLineBreakButton.Click += new System.EventHandler(this.InsertLineBreak_Click);
			this.EnableRandomHyperlink.Click += new System.EventHandler(this.EnableRandomHyperlink_Click);
			this.EnableSubmitHyperlink.Click += new System.EventHandler(this.EnableSubmitHyperlink_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UpImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			new Survey().MovePageBreakUp(SurveyId, PageNumber);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(SurveyId, ((PageBase)Page).MenuIndex);
		}

		private void DownImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			new Survey().MovePageBreakDown(SurveyId, PageNumber);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(SurveyId, ((PageBase)Page).MenuIndex);

		}
		private void InsertLineBreak_Click(object sender, System.EventArgs e)
		{
			new Survey().InsertSurveyLineBreak(SurveyId, PreviousQuestionDisplayOrder + 1, PageNumber);

		/*
			if (PreviousQuestionDisplayOrder == 0)
			{
				PreviousQuestionDisplayOrder = 1;
			}
		
			if (PreviousQuestionDisplayOrder == 1 && PageNumber == 1)
			{
				new Survey().InsertSurveyLineBreak(SurveyId, PreviousQuestionDisplayOrder, PageNumber);
			}
			else
			{
				new Survey().InsertSurveyLineBreak(SurveyId, PreviousQuestionDisplayOrder+1, PageNumber);
			}

			*/
			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(SurveyId, ((PageBase)Page).MenuIndex);
		}

		private void DeleteButton_Click(object sender, System.EventArgs e)
		{
			new Survey().DeletePageBreak(SurveyId, PageNumber);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(SurveyId, ((PageBase)Page).MenuIndex);
		}
		private void EnableRandomHyperlink_Click(object sender, System.EventArgs e)
		{
			PageOptionData updatedPageOptions = new PageOptionData();
			PageOptionData.PageOptionsRow pageOption = updatedPageOptions.PageOptions.NewPageOptionsRow();
			pageOption.SurveyId = SurveyId;
			pageOption.PageNumber = PageNumber;
			pageOption.RandomizeQuestions = !_enableRandomize;
			pageOption.EnableSubmitButton = _enableSubmit;
			updatedPageOptions.PageOptions.Rows.Add(pageOption);
			new Survey().UpdateSurveyPageOptions(updatedPageOptions);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(SurveyId, ((PageBase)Page).MenuIndex);
		}

		
		private void EnableSubmitHyperlink_Click(object sender, System.EventArgs e)
		{
			PageOptionData updatedPageOptions = new PageOptionData();
			PageOptionData.PageOptionsRow pageOption = updatedPageOptions.PageOptions.NewPageOptionsRow();
			pageOption.SurveyId = SurveyId;
			pageOption.PageNumber = PageNumber;
			pageOption.RandomizeQuestions = _enableRandomize;
			pageOption.EnableSubmitButton = !_enableSubmit;

			updatedPageOptions.PageOptions.Rows.Add(pageOption);
			new Survey().UpdateSurveyPageOptions(updatedPageOptions);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(SurveyId, ((PageBase)Page).MenuIndex);
		
		}

		int _totalPagesNumber = 1,
			_pageNumber = 1,
			_surveyId,
			_previousQuestionDisplayOrder = 1;
		bool 
			_enableRandomize = false,
			_enableSubmit = false;

		protected System.Web.UI.WebControls.ImageButton UpImageButton;
		protected System.Web.UI.WebControls.ImageButton DownImageButton;
		protected System.Web.UI.WebControls.LinkButton DeleteButton;
		protected System.Web.UI.WebControls.HyperLink InsertHyperLink;
		protected System.Web.UI.WebControls.LinkButton InsertLineBreakButton;
		protected System.Web.UI.WebControls.HyperLink BranchingHyperLink;
		protected System.Web.UI.WebControls.Label PageLabel;
		protected System.Web.UI.WebControls.Literal PageTitle;
		protected System.Web.UI.WebControls.LinkButton EnableRandomHyperlink;
		protected System.Web.UI.WebControls.LinkButton EnableSubmitHyperlink;
		protected PageBranchingRulesControl PageBranchingRules;



	}
}
