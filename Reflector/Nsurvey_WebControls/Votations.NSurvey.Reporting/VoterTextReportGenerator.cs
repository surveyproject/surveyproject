/**************************************************************************************************

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
using System.Data;
using System.Text;
using Votations.NSurvey.Enums;
using Votations.NSurvey.Resources;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Security;
using Votations.NSurvey.Helpers;
using System.Collections.Specialized;


namespace Votations.NSurvey.Reporting
{
	/// <summary>
	/// Generates a text report of a voter's answers
	/// </summary>
	public class VoterTextReportGenerator : VoterReportGenerator
	{
		public VoterTextReportGenerator(VoterAnswersData voterAnswers, int surveyId) : base(voterAnswers, surveyId)
		{
		}

		protected override string GenerateHeader()
		{
			return 	ResourceManager.GetString("VoterInformationTitle") + Environment.NewLine + "<br />" + "-----------------------------------------" + Environment.NewLine + "<br />";
		}

		protected override string GenerateFooter()
		{
			if (isScored)
			{
				return ResourceManager.GetString("VoterScoreTotalLabel") + _totalScore.ToString();
			}

			return "";
		}

		protected override string GenerateVoterInfo()
		{
			StringBuilder voterReport = new StringBuilder();
			TimeSpan timeTaken = new TimeSpan(0);
			
			if (!_voterAnswers.Voters[0].IsVoteDateNull() && !_voterAnswers.Voters[0].IsStartDateNull())
			{
				timeTaken = _voterAnswers.Voters[0].VoteDate - _voterAnswers.Voters[0].StartDate;
			}

			voterReport.Append(ResourceManager.GetString("VoterDBIDLabel") +  " " + _voterAnswers.Voters[0].VoterId + Environment.NewLine + "<br />");
			if (!_voterAnswers.Voters[0].IsContextUserNameNull())
			{
				voterReport.Append(ResourceManager.GetString("VoterUserNameLabel") +  " " + _voterAnswers.Voters[0].ContextUserName + Environment.NewLine + "<br />");
			}

			if (_voterAnswers.Voters[0].IsLanguageCodeNull() || _voterAnswers.Voters[0].LanguageCode.Length == 0)
			{
				voterReport.Append(ResourceManager.GetString("VoterLanguageLabel") + " " +ResourceManager.GetString("LanguageUndefined") + Environment.NewLine + "<br />");
			}
			else
			{
				voterReport.Append(ResourceManager.GetString("VoterLanguageLabel") + " " +_voterAnswers.Voters[0].LanguageCode + Environment.NewLine + "<br />");
			}

			voterReport.Append(ResourceManager.GetString("VoterIPAddressLabel") +  " " + _voterAnswers.Voters[0].IPSource + Environment.NewLine + "<br />");
			voterReport.Append(ResourceManager.GetString("TimeToTakeLabel") +  string.Format(" {0} min. , {1} sec.", timeTaken.Minutes.ToString(), timeTaken.Seconds.ToString()) + Environment.NewLine + "<br />");
			voterReport.Append(ResourceManager.GetString("VoteRecordedLabel") +  " " + _voterAnswers.Voters[0].VoteDate + Environment.NewLine + "<br />");
			voterReport.Append(GenerateAddInsReport());
			return voterReport.ToString();
		}

		protected virtual string GenerateAddInsReport()
		{
			StringBuilder addInsReport = new StringBuilder();

			WebSecurityAddInCollection securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetWebSecurityAddIns(_surveyId), null, null);
			NameValueCollection addInVoterData;
			for (int i=0;i<securityAddIns.Count;i++)
			{
				addInVoterData = securityAddIns[i].GetAddInVoterData(_voterAnswers.Voters[0].VoterId);
				if (addInVoterData != null)
				{
					addInsReport.Append(Environment.NewLine + securityAddIns[i].Description + Environment.NewLine + "<br />" + "-----------------------------------------" + Environment.NewLine + "<br />");
					for (int j=0; j<addInVoterData.Count;j++)
					{
						addInsReport.Append(addInVoterData.GetKey(i) + " : " + addInVoterData[i] + Environment.NewLine + "<br />");
					}
				}
			}

			return addInsReport.ToString();
		}

		protected override string GenerateQuestionsReport(bool onlyAnswered)
		{
			StringBuilder questionsReport = new StringBuilder();
			_questionData = new Questions().GetQuestionHierarchy(_surveyId);

			questionsReport.Append(Environment.NewLine + "<br />" + ResourceManager.GetString("SurveyAnswersTitle") + Environment.NewLine + "<br />");
			questionsReport.Append("-----------------------------------------" + Environment.NewLine + "<br />");

			QuestionData.QuestionsRow[] parentQuestions = GetParentQuestions();

			for (int i=0;i<parentQuestions.Length;i++)
			{
				string questionText = ((QuestionData.QuestionsRow)parentQuestions[i]).QuestionText;
				questionText = questionText.Replace("<br />", Environment.NewLine + "<br />");
				questionText = 	new PipeManager().PipeValuesInText(((QuestionData.QuestionsRow)parentQuestions[i]).QuestionId, questionText, _voterAnswers.VotersAnswers, null);

				if (System.Web.HttpContext.Current != null)
				{
					questionText = System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ");
				}

				// Check if the question has childs
				string filter = string.Format("ParentQuestionID = {0}", ((QuestionData.QuestionsRow)parentQuestions[i]).QuestionId); 
				QuestionData.QuestionsRow[] childQuestions = (QuestionData.QuestionsRow[])_questionData.Questions.Select(filter,"DisplayOrder", DataViewRowState.CurrentRows);
				if (childQuestions.Length == 0)
				{
					StringBuilder answerReport = new StringBuilder();

					for (int sectionNumber=0;sectionNumber<=GetQuestionSectionCount(((QuestionData.QuestionsRow)parentQuestions[i]).QuestionId);sectionNumber++)
					{
						answerReport.Append(GenerateAnswersReport(((QuestionData.QuestionsRow)parentQuestions[i]).QuestionId, onlyAnswered, sectionNumber));
						answerReport.Append(Environment.NewLine + "<br />");
					}
					
					if (answerReport.Length > 0 || !onlyAnswered)
					{

						questionsReport.Append( questionText + Environment.NewLine + "<br />");
						questionsReport.Append(answerReport);
						if (isScored)
						{
							questionsReport.Append(Environment.NewLine + "<br />");
							questionsReport.Append(ResourceManager.GetString("QuestionScoreLabel")+ _questionScore);
						}
						_totalScore += _questionScore;
						questionsReport.Append(Environment.NewLine + "<br />" + Environment.NewLine + "<br />");
					}
					_questionScore = 0;
				}
				else
				{
					questionsReport.Append( "-----" + Environment.NewLine + "<br />");
					questionsReport.Append( questionText + Environment.NewLine + "<br />" + Environment.NewLine + "<br />" );
					for (int sectionNumber=0;sectionNumber<=GetMatrixSectionCount(new DataView(_questionData.Questions,filter,"DisplayOrder", DataViewRowState.CurrentRows));sectionNumber++)
					{
						questionsReport.Append(GenerateMatrixReport(childQuestions, onlyAnswered, sectionNumber));
						questionsReport.Append(Environment.NewLine + "<br />");
					}
					questionsReport.Append( "-----"+ Environment.NewLine + "<br />");
					questionsReport.Append(Environment.NewLine + "<br />" + Environment.NewLine + "<br />");
				}
			}

			return questionsReport.ToString();
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


		string GenerateMatrixReport(QuestionData.QuestionsRow[] childQuestions, bool onlyAnswered, int sectionNumber)
		{
			StringBuilder matrixReport = new StringBuilder();
			
			for (int i=0;i<childQuestions.Length;i++)
			{
				string questionText = childQuestions[i].QuestionText;
				questionText = questionText.Replace("<br />", Environment.NewLine + "<br />");
				new PipeManager().PipeValuesInText(childQuestions[i].QuestionId, questionText, _voterAnswers.VotersAnswers, null);
				if (System.Web.HttpContext.Current != null)
				{
					questionText = System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ");
				}
				string answerReport = GenerateAnswersReport(childQuestions[i].QuestionId, onlyAnswered, sectionNumber);
				if (answerReport.Length > 0 || !onlyAnswered)
				{
					matrixReport.Append(questionText + "<br />"+ Environment.NewLine + "<br />");
					matrixReport.Append(answerReport);
					matrixReport.Append(Environment.NewLine + "<br />");
				}
			}

			return matrixReport.ToString();
		}

		/// <summary>
		/// Generates the answers list for the question
		/// </summary>
		/// <param name="questionId"></param>
		/// <param name="onlyAnswered">show only answers that have been answered</param>
		string GenerateAnswersReport(int questionId, bool onlyAnswered, int sectionNumber)
		{
			StringBuilder answersReport = new StringBuilder();

			AnswerData questionAnswers = new Answers().GetAnswers(questionId, null);
			int longestTextLength = GetLongestAnswerLength(questionAnswers);

			foreach (AnswerData.AnswersRow answer in questionAnswers.Answers.Rows)
			{
				AnswerTypeMode typeMode = (AnswerTypeMode) answer.TypeMode;

				VoterAnswersData.VotersAnswersRow voterAnswer = _voterAnswers.VotersAnswers.FindByVoterIdAnswerIdSectionNumber(_voterAnswers.Voters[0].VoterId , answer.AnswerId, sectionNumber);
				string answerText = new PipeManager().PipeValuesInText(questionId, answer.AnswerText, _voterAnswers.VotersAnswers, null);

				if (voterAnswer != null)
				{

					// Check answer type
					if ((((typeMode & AnswerTypeMode.Field) > 0) || 
						((typeMode & AnswerTypeMode.Custom) > 0) ||
						((typeMode & AnswerTypeMode.DataSource) > 0)) &&
						((typeMode & AnswerTypeMode.Selection) == 0))
					{
						if (voterAnswer.AnswerText.Length > 0)
						{
							answersReport.Append(" " + answerText + GenerateMissingSpaces(answerText.Length, longestTextLength) + " " + voterAnswer.AnswerText + Environment.NewLine + "<br />");
						}
						else if (!onlyAnswered)
						{
							answersReport.Append(" " + answerText + GenerateMissingSpaces(answerText.Length, longestTextLength) + " " + ResourceManager.GetString("AnswerNotAnsweredMessage") + Environment.NewLine + "<br />");
						}
					}
					else if (((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Field) > 0 &&
							((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) > 0)
					{
						answersReport.Append( " " + answerText + GenerateMissingSpaces(answerText.Length, longestTextLength) + " [x] " +voterAnswer.AnswerText+ Environment.NewLine + "<br />");
						_questionScore += answer.ScorePoint;
					}
					else if(((AnswerTypeMode)typeMode & AnswerTypeMode.Upload) > 0)
					{
						answersReport.Append( " " + answerText + GenerateMissingSpaces(answerText.Length, longestTextLength) + " " +GenerateFileList(voterAnswer.AnswerText)+ Environment.NewLine + "<br />");
					}

					else
					{
						answersReport.Append( " " + answerText + GenerateMissingSpaces(answerText.Length, longestTextLength) + " [x]" + Environment.NewLine + "<br />");
						_questionScore += answer.ScorePoint;
					}
				}
				else if (!onlyAnswered)
				{
					// Check answer type
					if ((((typeMode & AnswerTypeMode.Field) > 0) || 
						((typeMode & AnswerTypeMode.Custom) > 0) ||
						((typeMode & AnswerTypeMode.DataSource) > 0)) &&
						((typeMode & AnswerTypeMode.Selection) == 0))
					{
						answersReport.Append(" " + answerText + GenerateMissingSpaces(answerText.Length, longestTextLength) + " " + ResourceManager.GetString("AnswerNotAnsweredMessage")+ Environment.NewLine + "<br />");
					}
					else if ( ((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Field) > 0 &&
							((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) > 0)
					{
						answersReport.Append( " " + answerText + GenerateMissingSpaces(answerText.Length, longestTextLength) + " [] " +ResourceManager.GetString("AnswerNotAnsweredMessage")+ Environment.NewLine + "<br />");
					}
					else
					{
						answersReport.Append( " " + answerText + GenerateMissingSpaces(answerText.Length, longestTextLength) + " []" + Environment.NewLine + "<br />");
					}

				}
			}

			return answersReport.ToString();
		}

		private int GetLongestAnswerLength(AnswerData questionAnswers)
		{
			int longestLength = 0;
			foreach (AnswerData.AnswersRow answer in questionAnswers.Answers.Rows)
			{
				if (answer.AnswerText.Length > longestLength)
				{
					longestLength = answer.AnswerText.Length;
				}
			}

			return longestLength;
		}

		private string GenerateFileList(string groupGuid)
		{
			StringBuilder fileList = new StringBuilder();
			
			// Retrieve all files that are available for this group
			FileData answerFiles = new Answers().GetGuidFiles(groupGuid);

			if (answerFiles.Files.Rows.Count > 0)
			{
				for (int i=0; i<answerFiles.Files.Count;i++)
				{
					fileList.Append(answerFiles.Files[i].FileName + " (" + (Math.Round((double)answerFiles.Files[i].FileSize/1048576*100000)/100000).ToString("0.##") + ResourceManager.GetString("UploadFileSizeFormat") + ")");

					if (i+1<answerFiles.Files.Count)
					{
						fileList.Append(", ");
					}
				}
			}

			return fileList.ToString();
		}

		private string GenerateMissingSpaces(int currentLength, int longestTextLength)
		{
			StringBuilder missingSpaces = new StringBuilder();
			
			for (int i=0; i<longestTextLength-currentLength;i++)
			{
				missingSpaces.Append(" ");
			}

			return missingSpaces.ToString();
		}

		private QuestionData.QuestionsRow[] GetParentQuestions()
		{
			string filter = string.Format("Isnull(ParentQuestionID,0) = 0",	
				(int)QuestionTypeMode.Answerable);
			return (QuestionData.QuestionsRow[])
				_questionData.Questions.Select(filter,"DisplayOrder", DataViewRowState.CurrentRows);
		}

		int _questionScore = 0,
		_totalScore = 0;

	}
}
