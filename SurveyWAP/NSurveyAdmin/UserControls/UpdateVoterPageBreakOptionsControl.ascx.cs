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
    public partial class UpdateVoterPageBreakOptionsControl : System.Web.UI.UserControl
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

		public int VoterId
		{
			get { return _voterId; }
			set { _voterId = value; }
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			LocalizePage();

			ClearPageAnswersButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +((PageBase)Page).GetPageResource("ClearPageAnswersConfirmationMessage")+ "')== false) return false;");

			PageLabel.Text = PageNumber.ToString();
		}

		private void LocalizePage()
		{
			ClearPageAnswersButton.Text = ((PageBase)Page).GetPageResource("ClearPageAnswersButton");
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
			this.ClearPageAnswersButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DeleteButton_Click(object sender, System.EventArgs e)
		{
			new Voter().DeleteVoterPageAnswers(SurveyId, VoterId, PageNumber);

			// Reloads the builder
			UINavigator.NavigateToEditVoterReport(SurveyId, VoterId, ((PageBase)Page).MenuIndex);
		}

		int _totalPagesNumber = 1,
			_pageNumber = 1,
			_surveyId,
			_voterId = -1;

		protected System.Web.UI.WebControls.Label PageLabel;
		protected System.Web.UI.WebControls.Literal PageTitle;
		protected System.Web.UI.WebControls.LinkButton ClearPageAnswersButton;

	}
}
