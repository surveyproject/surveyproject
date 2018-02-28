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

using System.Linq;
using System.Drawing.Printing;

using Microsoft.VisualBasic;

using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Enums;
using Votations.NSurvey.Security;
using Votations.NSurvey.Helpers;

using Votations.NSurvey.WebAdmin.NSurveyAdmin;


namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display a report for a voter
	/// </summary>
    public partial class VoterReport : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label VoterUIDLabel;
		protected System.Web.UI.WebControls.Label IPAddressLabel;
		protected System.Web.UI.WebControls.Label VoteDateLabel;
		protected System.Web.UI.WebControls.Literal VoterUserNameLabel;
		protected System.Web.UI.WebControls.Label VoterUserName;
		protected System.Web.UI.WebControls.Label TimeToTakeSurveyLabel;
		protected System.Web.UI.WebControls.DataGrid QuestionsDataGrid;
		protected System.Web.UI.WebControls.Label VoterEmail;
		protected System.Web.UI.WebControls.Literal VoterInformationTitle;
		protected System.Web.UI.WebControls.Literal VoterDBIDLabel;
		protected System.Web.UI.WebControls.Literal VoterEmailLabel;
		protected System.Web.UI.WebControls.Literal VoterIPAddressLabel;
		protected System.Web.UI.WebControls.Literal VoteRecordedLabel;
		protected System.Web.UI.WebControls.Literal TimeToTakeLabel;
		protected System.Web.UI.WebControls.Literal SurveyAnswersTitle;
		protected System.Web.UI.WebControls.PlaceHolder AddInVoterDataPlaceHolder;
		protected System.Web.UI.WebControls.Button EditAnswersLinkButton;
		protected System.Web.UI.WebControls.Label VoterScoreTotalLabel;
		protected System.Web.UI.WebControls.Literal VoterLanguageLabel;
		protected System.Web.UI.WebControls.Label VoterLanguageValueLabel;

		new protected HeaderControl Header;


		private void Page_Load(object sender, System.EventArgs e)
		{
			SetupSecurity();
			LocalizePage();
            UITabList.SetVoterReportTabs((MsterPageTabs)Page.Master, UITabList.VoterReportTabs.VoterReport);


			if (Information.IsNumeric(Request["voterid"]))
			{
				_voterId =  int.Parse(Request["voterid"]);
			}
			else
			{
				//throw new FormatException("Invalid voter id!");
                throw new FormatException(GetPageResource("InvalidVoterID"));
			}

			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
			}

			BindData();
           
		}


		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessFieldEntries, true);
			EditAnswersLinkButton.Visible = CheckRight(NSurveyRights.EditVoterEntries, false);
		}

		private void LocalizePage()
		{
			VoterInformationTitle.Text = GetPageResource("VoterInformationTitle");
			VoterDBIDLabel.Text = GetPageResource("VoterDBIDLabel");
			VoterEmailLabel.Text = GetPageResource("VoterEmailLabel");
			VoterUserNameLabel.Text = GetPageResource("VoterUserNameLabel");
			VoterIPAddressLabel.Text = GetPageResource("VoterIPAddressLabel");
			VoteRecordedLabel.Text = GetPageResource("VoteRecordedLabel");
			TimeToTakeLabel.Text = GetPageResource("TimeToTakeLabel");
			SurveyAnswersTitle.Text = GetPageResource("SurveyAnswersTitle");
			EditAnswersLinkButton.Text = GetPageResource("EditAnswersLinkButton");
			VoterLanguageLabel.Text = GetPageResource("VoterLanguageLabel");
		}

        public void OnBackButton(object sender, CommandEventArgs e)
        {
            //SwitchToListMode();
            Response.Redirect(UINavigator.FieldsReportHyperlink);
        }

        /// <summary>
        /// Get the current DB stats and fill 
        /// the label with them
        /// </summary>
        private void BindData()
		{
			isScored = new Surveys().IsSurveyScored(SurveyId);

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
			VoterLanguageValueLabel.Text = _voterAnswers.Voters[0].IsLanguageCodeNull() || _voterAnswers.Voters[0].LanguageCode.Length == 0 ?  
				GetPageResource("LanguageUndefined") : _voterAnswers.Voters[0].LanguageCode;
			TimeToTakeSurveyLabel.Text = string.Format("{0} {1}, {2} secs.", timeTaken.Minutes.ToString(), GetPageResource("MinutesInfo"), timeTaken.Seconds.ToString());

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

			_questionData = new Questions().GetQuestionHierarchy(SurveyId);
			QuestionsDataGrid.DataSource = GetParentQuestions();
			QuestionsDataGrid.DataKeyField = "QuestionId";
			QuestionsDataGrid.DataBind();
			if (isScored)
			{
				VoterScoreTotalLabel.Text = GetPageResource("VoterScoreTotalLabel") + _totalScore.ToString();
			}
		}

		private DataView GetParentQuestions()
		{
			string filter = string.Format("Isnull(ParentQuestionID,0) = 0",	
				(int)QuestionTypeMode.Answerable);
			return new DataView(_questionData.Questions,filter,"DisplayOrder", DataViewRowState.CurrentRows);
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
			this.EditAnswersLinkButton.Click += new System.EventHandler(this.EditAnswersLinkButton_Click);
			this.QuestionsDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.QuestionsDataGrid_ItemCommand);
			this.QuestionsDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.BindQuestions);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void BindQuestions(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				int questionId = int.Parse(QuestionsDataGrid.DataKeys[e.Item.ItemIndex].ToString());
				
				// Check if the question has childs
				string filter = string.Format("ParentQuestionID = {0}", questionId); 
				DataView childQuestions = 
					new DataView(_questionData.Questions,filter,"DisplayOrder", DataViewRowState.CurrentRows);
				if (childQuestions.Count == 0)
				{
					DataGrid answerDataGrid = (DataGrid)e.Item.Cells[0].FindControl("QuestionAnswersDataGrid");
					AnswerData questionAnswers = new Answers().GetAnswers(questionId, null);
					for (int sectionNumber=0;sectionNumber<=GetQuestionSectionCount(questionId);sectionNumber++)
					{
						BindAnswerTable(questionAnswers,(PlaceHolder)e.Item.Cells[0].FindControl("QuestionAnswerPlaceHolder") 
							, sectionNumber);
				
						Label scoreTotalLabel = (Label)e.Item.Cells[0].FindControl("QuestionScoreLabel");
						if (isScored)
						{
							scoreTotalLabel.Text = string.Format("<br />" +GetPageResource("QuestionScoreLabel")+ _questionScore);
						}
						_totalScore += _questionScore;
						_questionScore = 0;
					}
				}
				else
				{
					for (int sectionNumber=0;sectionNumber<=GetMatrixSectionCount(childQuestions);sectionNumber++)
					{
						BindMatrix(childQuestions,
							(PlaceHolder)e.Item.Cells[0].FindControl("MatrixPlaceHolder"), sectionNumber);
					}
				}
			}

		}

		/// <summary>
		/// Get highest section count answered by the user for this question
		/// </summary>
		private int GetQuestionSectionCount(int questionId)
		{
			int sections = 0;

			VoterAnswersData.VotersAnswersRow[] answerState = 
				(VoterAnswersData.VotersAnswersRow[])_voterAnswers.VotersAnswers.Select("QuestionId = " +questionId, "SectionNumber DESC");
		
			if (answerState != null && answerState.Length > 0)
			{
				sections = answerState[0].SectionNumber;
			}

			return sections;
		}

		/// <summary>
		/// Get highest section count answered by the user
		/// </summary>
		private int GetMatrixSectionCount(DataView childQuestions)
		{
			int sections = 0;

			// Find highest section count based on child questions
			foreach (DataRowView questionRow in childQuestions)
			{
				VoterAnswersData.VotersAnswersRow[] matrixSectionNumber = 
					(VoterAnswersData.VotersAnswersRow[])_voterAnswers.VotersAnswers.Select("QuestionId = " +questionRow["QuestionId"], "SectionNumber DESC");
		
				if (matrixSectionNumber != null && matrixSectionNumber.Length > 0)
				{
					if (matrixSectionNumber[0].SectionNumber > sections)
					{
						sections = matrixSectionNumber[0].SectionNumber;
					}
				}
			}

			return sections;
		}

		private void BindAnswerTable(AnswerData questionAnswers, PlaceHolder container, int sectionNumber)
		{
			Table selectionTable = new Table();
			TableRow selectionRow;
			TableCell	answerTypeCell,
						answerTextCell;

			selectionTable.CssClass = (sectionNumber%2) == 0 ? 
				"innerText" : "alternatingSection" ;

			// Get all answer items available for this
			// question
			foreach (AnswerData.AnswersRow answer in questionAnswers.Answers)
			{
			
				selectionRow = new TableRow();
				answerTypeCell = new TableCell();
				answerTypeCell.Width = Unit.Pixel(10);
				answerTypeCell.VerticalAlign = VerticalAlign.Top;
				answerTextCell = new TableCell();

				string answerText = Server.HtmlEncode(
					System.Text.RegularExpressions.Regex.Replace(new PipeManager().PipeValuesInText(
					answer.QuestionId, answer.AnswerText, _voterAnswers.VotersAnswers, null), "<[^>]*>", " "));
				System.Web.UI.WebControls.Image spotImage = new System.Web.UI.WebControls.Image();
				spotImage.ImageUrl = GlobalConfig.ImagesPath + "spot_on.gif";
				
				// Check if the voter has answered this answer
				VoterAnswersData.VotersAnswersRow voterAnswer = _voterAnswers.VotersAnswers.FindByVoterIdAnswerIdSectionNumber(_voterId, answer.AnswerId, sectionNumber);
				if (voterAnswer != null)
				{
					// Check answer type
					if (((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Field) > 0) || 
						(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Custom) > 0) ||
						(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.DataSource) > 0)) &&
						(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) == 0))
					{
						spotImage.ImageUrl = GlobalConfig.ImagesPath + "spot_text.gif";
						answerTextCell.Text = FormatVoterAnswer(answerText, voterAnswer.AnswerText, true);
					}
					else if ( ((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Field) > 0 &&
							((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) > 0)
					{
						answerTextCell.Text = FormatVoterAnswer(answerText, voterAnswer.AnswerText,true);
						_questionScore += answer.ScorePoint;
					}
					else if(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Upload) > 0)
					{
						spotImage.ImageUrl = GlobalConfig.ImagesPath + "spot_file.gif";
						answerTextCell.Controls.Add(new LiteralControl(answerText + "<br />"));
						answerTextCell.Controls.Add(GenerateFileList(voterAnswer.AnswerText));
					}
					else	
					{
						answerTextCell.Text = string.Format("<b>{0}</b>", answerText);
						_questionScore += answer.ScorePoint;
					}
				}
				else
				{
					// Check answer type
					if (((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Field) > 0) ||
						(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Custom) > 0) ||
						(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.DataSource) > 0)) &&
						(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) == 0))
					{
						spotImage.ImageUrl = GlobalConfig.ImagesPath + "spot_text.gif";
						answerTextCell.Text = FormatVoterAnswer(answerText, null, false);
					}
					else if(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Upload) > 0)
					{
						spotImage.ImageUrl = GlobalConfig.ImagesPath + "spot_file.gif";
						answerTextCell.Text = answerText + "<br />" + GetPageResource("NoFileUploadedMessage");
					}
					else
					{
						answerTextCell.Text = answerText;
						spotImage.ImageUrl = GlobalConfig.ImagesPath + "spot_off.gif";
					}
				}

				answerTypeCell.Controls.Add(spotImage);

				
				selectionRow.Cells.Add(answerTypeCell);
				selectionRow.Cells.Add(answerTextCell);
				selectionTable.Rows.Add(selectionRow);
			}

			selectionTable.Width = Unit.Percentage(100);
			container.Controls.Add(selectionTable);
		}


		private void BindMatrix(DataView childQuestions, PlaceHolder container, int sectionNumber)
		{
			Table selectionTable = new Table();
			TableRow	selectionRow = new TableRow(),
						matrixHeaderRow = new TableRow();							
			TableCell	selectionCell;	// Selection cell for the answer item
			bool generateHeader = true;	// Header generation in progress
			System.Web.UI.WebControls.Image spotImage = new System.Web.UI.WebControls.Image();

			selectionTable.CssClass = (sectionNumber%2) == 0 ? 
				"innerText" : "alternatingSection" ;
		
			// Creates header's first empty cell 
			TableCell answerHeaderCell = new TableCell();
			matrixHeaderRow.Cells.Add(answerHeaderCell);

			// Get all answer items available for this
			// question
			foreach (DataRowView questionRow in childQuestions)
			{
				selectionRow = new TableRow();
				TableCell questionCell = new TableCell();
				questionCell.VerticalAlign = VerticalAlign.Top;
				questionCell.Text = new PipeManager().PipeValuesInText(
					int.Parse(questionRow["QuestionId"].ToString()),questionRow["QuestionText"].ToString(), _voterAnswers.VotersAnswers, null);

				selectionRow.Cells.Add(questionCell);
				
				// Parse the child question's answers
				AnswerData questionAnswers = new Answers().GetAnswers(int.Parse(questionRow["QuestionId"].ToString()), null);
				foreach (AnswerData.AnswersRow answer in questionAnswers.Answers)
				{
					// New spot image
					spotImage = new System.Web.UI.WebControls.Image();

					// Creates a new answer text in the matrix header
					if (generateHeader)
					{
						answerHeaderCell = new TableCell();
						answerHeaderCell.VerticalAlign = VerticalAlign.Top;
						answerHeaderCell.HorizontalAlign = HorizontalAlign.Center;
						answerHeaderCell.Wrap = false;
						answerHeaderCell.Text = 
							new PipeManager().PipeValuesInText(answer.QuestionId, answer.AnswerText, _voterAnswers.VotersAnswers, null);
						matrixHeaderRow.Cells.Add(answerHeaderCell);
					}

					// Add a new selection to the matrix's child question
					selectionCell = new TableCell();
					selectionCell.VerticalAlign = VerticalAlign.Top;
					selectionCell.HorizontalAlign = HorizontalAlign.Center;
					selectionCell.Wrap = false;

					// Check if the voter has answered this answer
					VoterAnswersData.VotersAnswersRow voterAnswer = _voterAnswers.VotersAnswers.FindByVoterIdAnswerIdSectionNumber(_voterId, answer.AnswerId, sectionNumber);
					if (voterAnswer != null)
					{
						// Check answer type
						if ((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Field) > 0) || 
							(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Custom) > 0) ||
							(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.DataSource) > 0))
						{
							selectionCell.Text = FormatVoterAnswer(null, voterAnswer.AnswerText,true);
						}
						else if(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Upload) > 0)
						{
							selectionCell.Controls.Add(GenerateFileList(voterAnswer.AnswerText));
						}
						else
						{
							spotImage.ImageUrl = GlobalConfig.ImagesPath + "spot_on.gif";
							selectionCell.Controls.Add(spotImage);
						}
					}
					else
					{
						// Check answer type
						if ((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Field) > 0)  || 
							(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Custom) > 0) ||
							(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.DataSource) > 0))
						{
							selectionCell.Text = FormatVoterAnswer(null,null,false);
						}
						else if(((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Upload) > 0)
						{
							selectionCell.Text = GetPageResource("NoFileUploadedMessage");
						}
						else
						{
							spotImage.ImageUrl = GlobalConfig.ImagesPath + "spot_off.gif";
							selectionCell.Controls.Add(spotImage);
						}
					}

					selectionRow.Cells.Add(selectionCell);
				}
				
				// Generates the answer text header
				if (generateHeader)
				{
					selectionTable.Rows.Add(matrixHeaderRow);
					generateHeader = false;					
				}
				
				selectionTable.Rows.Add(selectionRow);
			}


			selectionTable.Width = Unit.Percentage(100);

			container.Controls.Add(selectionTable);
		}

		string FormatVoterAnswer(string answer, string voterAnswer, bool selected)
		{

			if (voterAnswer == null)
			{
				if (answer == null ||  answer.Length == 0)
				{
					return "<span class='notanswered'>" + GetPageResource("AnswerNotAnsweredMessage") + "</span>";
				}
				else
				{
					if (selected)
					{
						return string.Format("<b>{0}</b> :<br />{1}",answer, GetPageResource("AnswerNotAnsweredMessage"));
					}
					else
					{
						return string.Format("{0} :<br />{1}",answer, GetPageResource("AnswerNotAnsweredMessage"));
					}
				}
			}
			else
			{
				if (answer == null)
				{
					if (selected)
					{
						return string.Format("<b>{0}</b>", voterAnswer);
					}
					else
					{
						return  voterAnswer;
					}
				}
				else
				{
					if (selected)
					{
						return string.Format("<b>{0}</b> :<br />{1}",answer, voterAnswer);
					}
					else
					{
						return string.Format("{0} :<br />{1}",answer, voterAnswer);
					}
				}
			}
		}

		private Table GenerateFileList(string groupGuid)
		{
			Table fileTable = new Table();
			fileTable.CssClass = "smallText";
			fileTable.Rows.Clear();
	
			// Retrieve all files that are available for this group
			FileData answerFiles = new Answers().GetGuidFiles(groupGuid);

			if (answerFiles.Files.Rows.Count > 0)
			{
				foreach (FileData.FilesRow file in answerFiles.Files)
				{
					fileTable.Rows.Add(GetFileRow(file.FileId, file.GroupGuid, file.FileSize, file.FileName, file.FileType));
				}
			}
			else
			{
				TableRow fileRow = new TableRow();
				TableCell fileCell = new TableCell();
				fileCell.Text = GetPageResource("NoFileUploadedMessage");
				fileRow.Cells.Add(fileCell);
				fileTable.Rows.Add(fileRow);
			}

			return fileTable;
		}

		protected virtual TableRow GetFileRow(int fileId, string groupGuid, int fileSize, string fileName, string fileType)
		{
			TableRow fileRow = new TableRow();
			TableCell fileCell = new TableCell();
			fileCell.Text = "&nbsp;";
			fileRow.Cells.Add(fileCell);
			
			// Setup download link 
			// Setup read only access
			fileCell = new TableCell();
			fileCell.Text = fileName;
			fileRow.Cells.Add(fileCell);

			fileCell = new TableCell();
			fileCell.Text = (Math.Round((double)fileSize/1048576*100000)/100000).ToString("0.##")  + GetPageResource("UploadFileSizeFormat");
			fileRow.Cells.Add(fileCell);
					
			fileCell = new TableCell();
			LinkButton downloadButton = new LinkButton();
			downloadButton.ID = "Download" + fileId;

			downloadButton.Text = GetPageResource("UploadFileDownloadButton");
			downloadButton.CommandArgument = string.Format("{0},{1},{2},{3}", fileId.ToString(), groupGuid, fileName, fileType);
			downloadButton.CommandName = "download";
			downloadButton.CssClass = "nsurveyUploadCommandClass";

			fileCell.Controls.Add(downloadButton);
			fileRow.Cells.Add(fileCell);

			// Creates only delete button if user can edit answers of voters
			if (CheckRight(NSurveyRights.EditVoterEntries, false))
			{
				fileCell = new TableCell();
				LinkButton deleteButton = new LinkButton();
				deleteButton.ID = GlobalConfig.DeleteFileButtonName + fileId;
				deleteButton.CssClass = "nsurveyUploadCommandClass";
				deleteButton.Text = GetPageResource("UploadFileDeleteButton");
				deleteButton.CommandArgument = string.Format("{0},{1},{2},{3}", fileId.ToString(), groupGuid, fileName, fileType);
				deleteButton.CommandName = "delete";
				fileCell.Controls.Add(new LiteralControl("|&nbsp;"));
				fileCell.Controls.Add(deleteButton);
				fileRow.Cells.Add(fileCell);
			}

			return fileRow;
		}
		
		private void DeleteFile(int fileId, string groupGuid) 
		{
			// Deletes the file
			new Answer().DeleteAnswerFile(fileId , groupGuid);
			
			UINavigator.NavigateToVoterReport(SurveyId, _voterId, MenuIndex);
		}

		/// <summary>
		/// Sends the file to the client
		/// </summary>
		private void SendFile(int fileId, string groupGuid, string fileName, string fileType) 
		{
			Byte[] fileData = new Answers().GetAnswerFileData(fileId, groupGuid);

			// Clear buffer and send the file
			Context.Response.Clear();
			Context.Response.ContentType = fileType != null && fileType.Length > 0 ?
				fileType : "application/octet-stream";
			Context.Response.AddHeader("Content-Disposition", "attachment; filename=\""+fileName+"\"");
			Context.Response.BinaryWrite(fileData);
			Context.Response.End();	
		}

		protected void EditAnswersLinkButton_Click(object sender, EventArgs e)
		{
			UINavigator.NavigateToEditVoterReport(SurveyId, _voterId, MenuIndex);
		}


		int _voterId,
			_questionScore = 0,
			_totalScore = 0;
		bool isScored = false;
		protected VoterAnswersData _voterAnswers;
		QuestionData _questionData;

		private void QuestionsDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string[] fileInfo = e.CommandArgument.ToString().Split(',');
			if (e.CommandName == "download")
			{
				SendFile(int.Parse(fileInfo[0]),fileInfo[1], fileInfo[2], fileInfo[3]);

			}
			else if (e.CommandName == "delete")
			{
				DeleteFile(int.Parse(fileInfo[0]),fileInfo[1]);
			}
		}

	}

}
