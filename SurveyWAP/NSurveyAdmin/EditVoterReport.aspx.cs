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
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Enums;
using Votations.NSurvey.Security;
using Votations.NSurvey.WebControls.UI;
using Votations.NSurvey.WebControlsFactories;

using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using Votations.NSurvey.WebAdmin.Code;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display a report for a voter
	/// </summary>
    public partial class EditVoterReport : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label VoterUIDLabel;
		protected System.Web.UI.WebControls.Label IPAddressLabel;
		protected System.Web.UI.WebControls.Label VoteDateLabel;
		protected System.Web.UI.WebControls.Label TimeToTakeSurveyLabel;
		protected System.Web.UI.WebControls.Label VoterEmail;
		protected System.Web.UI.WebControls.Literal VoterInformationTitle;
		protected System.Web.UI.WebControls.Literal VoterDBIDLabel;
		protected System.Web.UI.WebControls.Literal VoterEmailLabel;
		protected System.Web.UI.WebControls.Literal VoterIPAddressLabel;
		protected System.Web.UI.WebControls.Literal VoteRecordedLabel;
		protected System.Web.UI.WebControls.Literal TimeToTakeLabel;
		protected System.Web.UI.WebControls.PlaceHolder AddInVoterDataPlaceHolder;
		protected System.Web.UI.WebControls.Button UpdateVoterAnswersButton;
		protected System.Web.UI.WebControls.PlaceHolder EditAnswersPlaceHolder;
		protected System.Web.UI.WebControls.Literal EditSurveyAnswersTitle;
		protected System.Web.UI.WebControls.Button ReadOnlyAnswersLinkButton;
		protected System.Web.UI.WebControls.Literal VoterUserNameLabel;
		protected System.Web.UI.WebControls.Label VoterUserName;
		new protected HeaderControl Header;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetupSecurity();
			LocalizePage();
            UITabList.SetEditVoterReportTabs((MsterPageTabs)Page.Master, UITabList.EditVoterReportTabs.EditVoterReport);

            // decrypt voterid
            string test = Request["voterid"];
            string urltest = HttpUtility.UrlEncode(test);

            //int voterIdDec = int.Parse(QueryStringModule.DecryptString(Request["voterid"]));
            int voterIdDec = int.Parse(QueryStringModule.DecryptString(urltest));


            //         if (Information.IsNumeric(Request["voterid"]))
            //{
            //	_voterId =  int.Parse(Request["voterid"]);
            //}
            if (Information.IsNumeric(voterIdDec))
            {
                _voterId = voterIdDec;
            }
            else
			{
				throw new FormatException("Invalid voter id!");
			}

			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
			}

			BindData();

			// Build the survey's questions
			BuildQuestionList();

			// Bind answers after the question have 
			// been added to the control tree
			// to get correct viewstate
			foreach (QuestionItem question in questions)
			{
				question.DataBind();
			}
		}

		private void LocalizePage()
		{
			VoterInformationTitle.Text = GetPageResource("VoterInformationTitle");
			VoterDBIDLabel.Text = GetPageResource("VoterDBIDLabel");
			VoterEmailLabel.Text = GetPageResource("VoterEmailLabel");
			VoterIPAddressLabel.Text = GetPageResource("VoterIPAddressLabel");
			VoteRecordedLabel.Text = GetPageResource("VoteRecordedLabel");
			TimeToTakeLabel.Text = GetPageResource("TimeToTakeLabel");
			EditSurveyAnswersTitle.Text = GetPageResource("EditVoterAnswersTitle");
			ReadOnlyAnswersLinkButton.Text = GetPageResource("ReadOnlyAnswersLinkButton");
			VoterUserNameLabel.Text = GetPageResource("VoterUserNameLabel");
            UpdateVoterAnswersButton.Text = GetPageResource("UpdateVoterAnswersButton");

		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.EditVoterEntries, true);
		}

		/// <summary>
		/// Get the current DB stats and fill 
		/// the label with them
		/// </summary>
		private void BindData()
		{
			TimeSpan timeTaken;
			_voterAnswers = new Voters().GetVoterAnswers(_voterId);

			if (!_voterAnswers.Voters[0].IsVoteDateNull() && !_voterAnswers.Voters[0].IsStartDateNull())
			{
				timeTaken = _voterAnswers.Voters[0].VoteDate - _voterAnswers.Voters[0].StartDate;
			}
			else
			{
				timeTaken = new TimeSpan(0);
			}

			VoterUIDLabel.Text = _voterId.ToString();
			IPAddressLabel.Text = _voterAnswers.Voters[0].IPSource;
			VoteDateLabel.Text = _voterAnswers.Voters[0].VoteDate.ToString();
			VoterEmail.Text = _voterAnswers.Voters[0].IsEmailNull() ? GetPageResource("AnonymousVoteInfo") : _voterAnswers.Voters[0].Email;
			VoterUserName.Text = _voterAnswers.Voters[0].IsContextUserNameNull() ?  GetPageResource("ContextUserNameDisabled") : _voterAnswers.Voters[0].ContextUserName;
			TimeToTakeSurveyLabel.Text = string.Format("{0} {1}, {2} secs.", 
				timeTaken.Minutes.ToString(), GetPageResource("MinutesInfo"), timeTaken.Seconds.ToString());

			WebSecurityAddInCollection securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetWebSecurityAddIns(SurveyId), ViewState, null);
			NameValueCollection addInVoterData;
			for (int i=0;i<securityAddIns.Count;i++)
			{
				addInVoterData = securityAddIns[i].GetAddInVoterData(_voterId);
				if (addInVoterData != null)
				{
					// Creates a new addin voter details page
					SecurityAddInVoterReportControl addInVoterControl = 
						(SecurityAddInVoterReportControl)LoadControl("UserControls/SecurityAddInVoterReportControl.ascx");

					addInVoterControl.AddInDescription = securityAddIns[i].Description;
					addInVoterControl.AddInVoterData = addInVoterData;
					AddInVoterDataPlaceHolder.Controls.Add(addInVoterControl);
				}
			}
		}

		void BuildQuestionList()
		{
			QuestionData _questionsData = new Questions().GetAnswerableQuestionWithoutChilds(SurveyId);
			int currentPage = 1,
				previousDisplayOrder = 0,
				totalPages = new Surveys().GetPagesNumber(SurveyId);
			
			Table questionsContainer = new Table();
			questionsContainer.Width = Unit.Percentage(100);
			Table questionTable = new Table();
			questionTable.CellSpacing = 2;
			questionTable.CellPadding = 4;
			questionTable.CssClass = "questionBuilder";
				
			questionTable.Rows.Add(BuildPageBreakOptionsRow(currentPage, totalPages,previousDisplayOrder));
			foreach (QuestionData.QuestionsRow question in _questionsData.Questions.Rows)
			{
				while (question.PageNumber > currentPage)
				{
					currentPage++;

					questionsContainer.Rows.Add(BuildRow(questionTable));
					EditAnswersPlaceHolder.Controls.Add(questionsContainer);

					// Creates a new page
					questionsContainer = new Table();
					questionsContainer.Width = Unit.Percentage(100);
					questionTable = new Table();
					questionTable.CellSpacing = 2;
					questionTable.CellPadding = 4;
					questionTable.CssClass = "questionBuilder";
					questionTable.Rows.Add(BuildPageBreakOptionsRow(currentPage, totalPages,previousDisplayOrder));
				}

				if (question.PageNumber == currentPage)
				{
					AddQuestionWebControl(questionTable, question);
					previousDisplayOrder = question.DisplayOrder;
				}
			}

			questionsContainer.Rows.Add(BuildRow(questionTable));
			EditAnswersPlaceHolder.Controls.Add(questionsContainer);
		}

		private TableRow BuildRow(Control child)
		{
			TableRow row = new TableRow();
			TableCell cell  = new TableCell();
			cell.Controls.Add(child);
			row.Cells.Add(cell);
			return row;
		}

		/// <summary>
		/// Get a web control instance of the question row 
		/// and adds it with its options to the table 
		/// </summary>
		private void AddQuestionWebControl(Table questionTable, QuestionData.QuestionsRow question)
		{
			QuestionItem questionWebControl;
			
			// Set voter answers only on first load to init
			// question control's answer items
			if (!Page.IsPostBack)
			{
				questionWebControl = QuestionItemFactory.Create(question, null, this.UniqueID, 0, _voterAnswers.VotersAnswers, false);
			}
			else
			{
				questionWebControl = QuestionItemFactory.Create(question, null, this.UniqueID, 0, null, false);
			}

			// Set question's style
			// and bind the data
			Style questionStyle = new Style();
			questionStyle.CssClass = "surveyQuestion";
			Style answerStyle = new Style();
			answerStyle.CssClass = "surveyAnswer";	
			Style markStyle = new Style();
			Style validationStyle = new Style();
			validationStyle.CssClass = "questionValidationMessageStyleCSS";
			Style confirmationStyle = new Style();
			confirmationStyle.CssClass = "confirmationMessageStyleCSS";

			if (questionWebControl is ActiveQuestion)
			{
				((ActiveQuestion)questionWebControl).AnswerPosted += 
					new AnswerPostedEventHandler(OnAnswerPost);
				((ActiveQuestion)questionWebControl).EnableClientSideValidation = false;
				((ActiveQuestion)questionWebControl).EnableServerSideValidation = false;
				((ActiveQuestion)questionWebControl).ValidationMessageStyle = validationStyle;
				((ActiveQuestion)questionWebControl).ConfirmationMessageStyle = confirmationStyle;
			}
			if (questionWebControl is SectionQuestion)
			{
				((SectionQuestion)questionWebControl).SectionOptionStyle.CssClass = "questionOptions";
			}
			if (questionWebControl is MatrixQuestion)
			{
				((MatrixQuestion)questionWebControl).MatrixHeaderStyle = answerStyle;
				((MatrixQuestion)questionWebControl).MatrixStyle = answerStyle;
				((MatrixQuestion)questionWebControl).MatrixItemStyle = answerStyle;
				((MatrixQuestion)questionWebControl).MatrixAlternatingItemStyle = answerStyle;
			}
			if (questionWebControl is SectionQuestion)
			{
				((SectionQuestion)questionWebControl).SectionGridAnswersItemStyle = answerStyle;
				((SectionQuestion)questionWebControl).SectionGridAnswersHeaderStyle = answerStyle;
				((SectionQuestion)questionWebControl).SectionGridAnswersAlternatingItemStyle = answerStyle;
				((SectionQuestion)questionWebControl).SectionGridAnswersStyle = answerStyle;
				((SectionQuestion)questionWebControl).EnableGridSectionClientSideValidation = false;
				((SectionQuestion)questionWebControl).EnableGridSectionServerSideValidation = false;
			}

			questionWebControl.RenderMode = ControlRenderMode.Edit;
			questionWebControl.AnswerStyle = answerStyle;
			questionWebControl.QuestionStyle = questionStyle;
			
			questions.Add(questionWebControl);
						
			// Add the question and its options to the table
			questionTable.Rows.Add(BuildQuestionOptionsRow(question));
			questionTable.Rows.Add(BuildRow(questionWebControl));
		}

		/// <summary>
		/// Builds a row with the options available to the question
		/// </summary>
		/// <returns>a tablerow instance with the options</returns>
		private TableRow BuildQuestionOptionsRow(QuestionData.QuestionsRow question)
		{
			// Creates a new edit options control
			UpdateVoterQuestionOptionsControl questionOptions = 
				(UpdateVoterQuestionOptionsControl)LoadControl("UserControls/UpdateVoterQuestionOptionsControl.ascx");
			questionOptions.Question = question;
			questionOptions.VoterId = _voterId;
			return BuildRow(questionOptions);
		}

		/// <summary>
		/// Builds a row with the options available for page breaks
		/// </summary>
		/// <returns>a tablerow instance with the options</returns>
		private TableRow BuildPageBreakOptionsRow(int pageNumber, int totalPages, int previousDisplayOrder)
		{
			// Creates a new edit page break options control
			UpdateVoterPageBreakOptionsControl pageBreakOptionsControl = 
				(UpdateVoterPageBreakOptionsControl)LoadControl("UserControls/UpdateVoterPageBreakOptionsControl.ascx");
			pageBreakOptionsControl.SurveyId = SurveyId;
			pageBreakOptionsControl.VoterId = _voterId;
			pageBreakOptionsControl.PageNumber = pageNumber;
			
			return BuildRow(pageBreakOptionsControl);
		}

		/// <summary>
		/// Store the answers received in a 
		/// temporary storage
		/// </summary>
		/// <param name="sender">The question that raised the event</param>
		/// <param name="e">Answers posted with the question</param>
		protected virtual void OnAnswerPost(Object sender, QuestionItemAnswersEventArgs e)
		{
			int voterId = _voterAnswers.Voters[0].VoterId;
			foreach (PostedAnswerData postedAnswer in e.Answers)
			{
				VoterAnswersData.VotersAnswersRow voterAnswer = updatedAnswerSet.VotersAnswers.NewVotersAnswersRow(); 
				voterAnswer.VoterId = voterId;
				voterAnswer.AnswerId = postedAnswer.AnswerId;
				voterAnswer.QuestionId = postedAnswer.Item.QuestionId;
				voterAnswer.AnswerText = postedAnswer.FieldText;
				voterAnswer.SectionNumber = postedAnswer.SectionNumber;
				updatedAnswerSet.EnforceConstraints = false;
				updatedAnswerSet.VotersAnswers.AddVotersAnswersRow(voterAnswer);
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
			this.ReadOnlyAnswersLinkButton.Click += new System.EventHandler(this.ReadOnlyAnswersLinkButton_Click);
			this.UpdateVoterAnswersButton.Click += new System.EventHandler(this.UpdateVoterAnswersButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UpdateVoterAnswersButton_Click(object sender, System.EventArgs e)
		{
			if (updatedAnswerSet!= null)
			{
				_voterAnswers.VotersAnswers.Clear();

				// If answer were posted in the temp. dataset merge them in 
				// the "total" vistor's answer set
				_voterAnswers.Merge(updatedAnswerSet,false);
			
				new Voter().UpdateVoter(_voterAnswers);
			}

		}

	
		int _voterId;

		VoterAnswersData _voterAnswers, updatedAnswerSet = new VoterAnswersData();

		private QuestionItemCollection questions = new QuestionItemCollection();

		protected void ReadOnlyAnswersLinkButton_Click(object sender, EventArgs e)
		{
			UINavigator.NavigateToVoterReport(SurveyId, _voterId, MenuIndex);
		}
	}

}
