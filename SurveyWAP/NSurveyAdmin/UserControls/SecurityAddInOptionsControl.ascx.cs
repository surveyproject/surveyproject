/**************************************************************************************************
	Survey changes: copyright (c) 2010, W3DevPro TM (http://survey.codeplex.com)	

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
	using Votations.NSurvey.Enums;
	using Votations.NSurvey.Security;

	/// <summary>
	///	Handles the survey's page breaks options	
	/// </summary>
    public partial class SecurityAddInOptionsControl : System.Web.UI.UserControl
	{

		public IWebSecurityAddIn SecurityAddIn
		{
			get { return _securityAddIn; }
			set { _securityAddIn = value; }
		}

		// Number of addins available
		public int TotalAddIns
		{
			get { return _totalAddIns; }
			set { _totalAddIns = value; }
		}

		public int SurveyId
		{
			get { return _surveyId; }
			set { _surveyId = value; }
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			LocalizePage();

			if (SecurityAddIn!=null)
			{
                AddInDescriptionLabel.Text = '"' + SecurityAddIn.Description + '"' + " - ";
			}
			else
			{
				DeleteButton.Enabled = false;
				DisableAddInLinkButton.Enabled = false;
			}


			if (SecurityAddIn==null || SecurityAddIn.Order <= 1)
			{
				UpImageButton.Visible = false;
				InsertAddInHyperLink.NavigateUrl = 
					String.Format(UINavigator.InsertSecurityAddInLink+"?SurveyID={0}&AddInOrder=1&MenuIndex={1}", 
					SurveyId, ((PageBase)Page).MenuIndex);
			}
			else
			{
				InsertAddInHyperLink.NavigateUrl = 
					String.Format("{0}?SurveyID={1}&AddInOrder={2}&MenuIndex={3}", 
					UINavigator.InsertSecurityAddInLink, SurveyId, SecurityAddIn.Order, ((PageBase)Page).MenuIndex);
			}
		
			if (SecurityAddIn==null || SecurityAddIn.Order >= TotalAddIns)
			{
				DownImageButton.Visible = false;
			}
		}

		private void LocalizePage()
		{
			DeleteButton.Text = ((PageBase)Page).GetPageResource("DeleteButton");
			InsertAddInHyperLink.Text = ((PageBase)Page).GetPageResource("InsertAddInHyperLink");			
//Correction 2010-06-28 by Erran3:			
			DisableAddInLinkButton.Text = ((PageBase)Page).GetPageResource("DisableAddInLinkButton");
			
			if (SecurityAddIn != null)
			{
				if (SecurityAddIn.Disabled)
				{
					AddInDisabledLabel.Text = ((PageBase)Page).GetPageResource("AddInDisabledLabel");
					DisableAddInLinkButton.Text = ((PageBase)Page).GetPageResource("EnableAddInLinkButton");
				}
//				else
//				{
//					DisableAddInLinkButton.Text = ((PageBase)Page).GetPageResource("DisableAddInLinkButton");
//				}
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
			this.DisableAddInLinkButton.Click += new System.EventHandler(this.DisableAddInLinkButton_Click);
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UpImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			new SecurityAddIn().MoveWebSecurityAddInUp(SurveyId, SecurityAddIn.AddInDbId);

			// Reloads the addin list
			UINavigator.NavigateToSurveySecurity(SecurityAddIn.SurveyId, ((PageBase)Page).MenuIndex);
		}

		private void DownImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			new SecurityAddIn().MoveWebSecurityAddInDown(SurveyId, SecurityAddIn.AddInDbId);

			// Reloads the addin list
			UINavigator.NavigateToSurveySecurity(SurveyId, ((PageBase)Page).MenuIndex);

		}

		private void DeleteButton_Click(object sender, System.EventArgs e)
		{

			_securityAddIn.UnInitOnSurveyRemoval();

			new SecurityAddIn().DeleteWebSecurityAddIn(SurveyId, SecurityAddIn.AddInDbId);

			// Reloads the addin list
			UINavigator.NavigateToSurveySecurity(SurveyId, ((PageBase)Page).MenuIndex);
		}

		int _totalAddIns = 1,
			_surveyId = -1;		
		IWebSecurityAddIn _securityAddIn;
		protected System.Web.UI.WebControls.ImageButton UpImageButton;
		protected System.Web.UI.WebControls.ImageButton DownImageButton;
		protected System.Web.UI.WebControls.LinkButton DeleteButton;
		protected System.Web.UI.WebControls.HyperLink InsertAddInHyperLink;
		protected System.Web.UI.WebControls.LinkButton DisableAddInLinkButton;
		protected System.Web.UI.WebControls.Label AddInDisabledLabel;
		protected System.Web.UI.WebControls.Label AddInDescriptionLabel;

		private void DisableAddInLinkButton_Click(object sender, System.EventArgs e)
		{
			if (SecurityAddIn.Disabled)
			{
				new SecurityAddIn().EnableWebSecurityAddIn(SurveyId, SecurityAddIn.AddInDbId);
			}
			else
			{
				new SecurityAddIn().DisableWebSecurityAddIn(SurveyId, SecurityAddIn.AddInDbId);
			}

			// Reloads the addin list
			UINavigator.NavigateToSurveySecurity(SurveyId, ((PageBase)Page).MenuIndex);
		}

	}
}
