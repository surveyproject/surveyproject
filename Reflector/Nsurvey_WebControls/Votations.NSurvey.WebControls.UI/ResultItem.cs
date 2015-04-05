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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using Votations.NSurvey.Data;
using Votations.NSurvey.Resources;

namespace Votations.NSurvey.WebControls.UI
{
	/// <summary>
	/// Graphical result for a question
	/// </summary>
	public class ResultItem : WebControl, INamingContainer
	{

		/// <summary>
		/// Show only percents and not vote details
		/// </summary>
		[Bindable(true), 
		Category("General"), 
		DefaultValue("false")]
		public bool ShowOnlyPercent
		{
			get	{ return _showOnlyPercent;}
			set	{ _showOnlyPercent = value;}
		}


		/// <summary>
		/// CellSpacing of the poll box table
		/// </summary>
		[Bindable(true), 
		Category("Layout"), 
		DefaultValue("0")] 	
		public int CellSpacing
		{
			get	{ return _cellSpacing;}
			set	{ _cellSpacing = value;}
		}

		/// <summary>
		/// CellPadding of the poll box table
		/// </summary>
		[Bindable(true), 
		Category("Layout"), 
		DefaultValue("0")] 	
		public int CellPadding
		{
			get	{ return _cellPadding;}
			set	{ _cellPadding = value;}
		}

		/// <summary>
		/// Color of the results bars
		/// </summary>
		[Bindable(true), 
		Category("Layout"), 
		DefaultValue("red")] 	
		public string BarColor
		{
			get { return _barColor; }
			set { _barColor = value;}
		}

		/// <summary>
		/// Sets the style for the title
		/// </summary>
		[
		Category("Styles"),
		DefaultValue(null),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		PersistenceMode(PersistenceMode.InnerProperty)

		]
		public Style HeadStyle 
		{
			get 
			{
				if (_headStyle == null)
					_headStyle = new Style();

				return _headStyle;
			}
			set
			{
				_headStyle = value;
			}

		}

		/// <summary>
		/// Sets the style of the results footer
		/// </summary>
		[
		Category("Styles"),
		DefaultValue(null),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		PersistenceMode(PersistenceMode.InnerProperty)

		]
		public Style FootStyle 
		{
			get 
			{
				if (_footStyle == null)
					_footStyle = new Style();

				return _footStyle;
			}
			set
			{
				_footStyle = value;
			}

		}

		/// <summary>
		/// Sets the style for the question
		/// </summary>
		[
		Category("Styles"),
		DefaultValue(null),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public Style QuestionStyle 
		{
			get 
			{
				if (_questionStyle == null)
					_questionStyle = new Style();

				return _questionStyle;
			}
			set
			{
				_questionStyle = value;
			}

		}

		/// <summary>
		/// Sets the style for the answers
		/// </summary>
		[
		Category("Styles"),
		DefaultValue(null),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public Style AnswerStyle 
		{
			get 
			{
				if (_answerStyle == null)
					_answerStyle = new Style();

				return _answerStyle;
			}
			set
			{
				_answerStyle = value;
			}
		}


		/// <summary>
		/// Sets the style for the submit button
		/// </summary>
		[
		Category("Styles"),
		DefaultValue(null),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public Style ButtonStyle 
		{
			get 
			{
				if (_buttonStyle == null)
					_buttonStyle = new Style();

				return _buttonStyle;
			}
		}


		/// <summary>
		/// Datasource that contains the answers, at this
		/// time it must be of an AnswerDataCollection type 
		/// </summary>
		public object DataSource
		{
			get { return _dataSource; }
			set
			{
				if (value is QuestionResultsData)
					_dataSource = (QuestionResultsData)value;
				else
					throw new ArgumentException("DataSource must be an QuestionResultsData instance." + value.GetType());
			}		
		}

		public override void DataBind()
		{
			base.DataBind();
		}

		/// <summary>
		/// Check if any datasource has been set and 
		/// builds the results table
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDataBinding(EventArgs e) 
		{
			// Clears all childs controls 
			Controls.Clear();

			// Reset child controls viewstate
			if (HasChildViewState)
				ClearChildViewState();

			if (DataSource is QuestionResultsData)
			{
				BuildResults();

				// No need to call CreateChildControls
				ChildControlsCreated = true;
			}

			if (!IsTrackingViewState)
				TrackViewState();
		}



		/// <summary>
		/// Override control collection and return base collection
		/// </summary>
		public override ControlCollection Controls 
		{
			get 
			{
				EnsureChildControls();
				return base.Controls;
			}
		}

		
		/// <summary>
		/// Web control constructor
		/// </summary>
		public ResultItem()
		{
			// If Context is available we're not in design time.
			if (Context != null)
				_isDesignTime = false;
		}
		

		/// <summary>
		/// Renders the control in the designer
		/// </summary>
		public void RenderAtDesignTime() 
		{
			// In design time?
			if (!_isDesignTime)
				return;
		}


		protected override void CreateChildControls() 
		{
			if (_isDesignTime)
				return;
		}

		protected void BuildResults()
		{
			Table resultTable = new Table();
			resultTable.Width = Unit.Percentage(100);
			resultTable.CellPadding = CellPadding;
			resultTable.CellSpacing = CellSpacing;
			resultTable.Height = this.Height;
			resultTable.BackColor = base.BackColor;
			Label literal = new Label();
			// Do we need to show the parent question text for matrix based child questions
			if (!_dataSource.Questions[0].IsParentQuestionIdNull() && _dataSource.Questions[0].ParentQuestionText != null) 
			{
				String questionText = String.Format("{0} - {1}", 
					_dataSource.Questions[0].ParentQuestionText, 
					_dataSource.Questions[0].QuestionText);

				// Show parent and child question text
				literal.Text = questionText;
			} 
			else 
			{
				// Show question text
				literal.Text = _dataSource.Questions[0].QuestionText;
			}
			literal.ControlStyle.Font.Bold = true;
			QuestionStyle.Font.Bold = false;
			resultTable.Rows.Add(BuildRow(literal, QuestionStyle));				

			QuestionResultsData.AnswersRow[] answers = _dataSource.Questions[0].GetAnswersRows();

			double totalOfVotes = GetVotersTotal(answers);

			// Add the answers and results bar
			resultTable.Rows.Add(BuildRow(BuildResultsBarTable(answers, totalOfVotes), AnswerStyle));

			if (!ShowOnlyPercent)
			{
				// Add Total of votes row
				resultTable.Rows.Add(BuildRow(String.Format(ResourceManager.GetString("TotalOfVotes"), totalOfVotes), FootStyle));
			}

			Controls.Add(resultTable);
		}


		private Table BuildResultsBarTable(QuestionResultsData.AnswersRow[] answers, double totalOfVotes)
		{
			double currentRate = 0; 
			double totalRate = 0;
			double totalOfVotesRate = 0;

			Table barTable = new Table();
			barTable.Width = Unit.Percentage(95);
			barTable.BorderWidth = 0;
			barTable.ControlStyle.CopyFrom(AnswerStyle);

			foreach (QuestionResultsData.AnswersRow answer in answers)
			{
				if ((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) > 0))
				{
					double VotePercent = 0;

					if (totalOfVotes != 0)
					{
						if (answer.VoterCount==0)
							VotePercent = 0;		
						else
							VotePercent = ((double)answer.VoterCount / (double)totalOfVotes) * 100;
					}

					// Add answer text & vote count
					answer.AnswerText = Context.Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answer.AnswerText, "<[^>]*>", " "));
					string answerCellText;
					if (!ShowOnlyPercent)
					{
						answerCellText = string.Format("{0}&nbsp;({1})",
							answer.AnswerText, answer.VoterCount.ToString());
					}
					else
					{
						answerCellText = answer.AnswerText;
					}
					barTable.Rows.Add(BuildRow(answerCellText, AnswerStyle));				

					// Add results bar
					ResultsBar resultsBar = new ResultsBar();
					resultsBar.Progress = (int)Math.Round(VotePercent);
					resultsBar.ItemStyle = this.AnswerStyle;
					resultsBar.BarColor = BarColor;
					if (this.Width.IsEmpty)
					{
						resultsBar.TableWidth = 110;
					}
					else
					{
						resultsBar.TableWidth = this.Width;
					}
					barTable.Rows.Add(BuildRow(resultsBar, AnswerStyle));				
					
					// Do we include this answer in the
					// rating total
					if (answer.RatePart)
					{
						currentRate++;
						totalRate += currentRate * answer.VoterCount;
						totalOfVotesRate += answer.VoterCount;
					}
				}
			}
			
			// Add Rating Bar
			if (_dataSource.Questions[0].RatingEnabled && currentRate>0)
			{
				double meanRate = 0;

				if (totalOfVotes == 0)
					meanRate = 0;
				else
					meanRate = totalRate / totalOfVotesRate;
			
				RatingBar ratingBar = new RatingBar();
				ratingBar.MaxRating = currentRate;
				ratingBar.Rating = meanRate;
				ratingBar.ItemStyle = this.AnswerStyle;

				if (this.Width.IsEmpty)
				{
					ratingBar.TableWidth = 110;
				}
				else
				{
					ratingBar.TableWidth = this.Width;
				}

				barTable.Rows.Add(BuildRow(ratingBar, AnswerStyle));
			}

			return barTable;
		}

		private double GetVotersTotal(QuestionResultsData.AnswersRow[] answers)
		{
			double total = 0;
			foreach (QuestionResultsData.AnswersRow answer in answers)
			{
				// Increment only selection answers that can be selected
				if ((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) > 0))
				{
					total += answer.VoterCount;
				}
			}
			return total;
		}

		private TableRow BuildRow(Control child, Style rowStyle)
		{
			TableRow row = new TableRow();
			TableCell cell  = new TableCell();
			cell.Controls.Add(child);
			row.Cells.Add(cell);
			row.ControlStyle.CopyFrom(rowStyle);
			return row;
		}
		
		private TableRow BuildRow(string Text, Style rowStyle)
		{
			TableRow row = new TableRow();
			TableCell cell  = new TableCell();
			cell.Text = Text;
			row.Cells.Add(cell);
			row.ControlStyle.CopyFrom(rowStyle);
			return row;
		}

		int	_cellSpacing,
			_cellPadding;
		QuestionResultsData _dataSource = new QuestionResultsData();
		bool	_isDesignTime = true,
				_showOnlyPercent = false;
		string	_barColor = "red";
		Style	_headStyle,
				_footStyle,
				_questionStyle,
				_answerStyle,
				_buttonStyle;
	}
}
