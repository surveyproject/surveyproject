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
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.UserControls;
using System.Text.RegularExpressions;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;


namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the user editor
	/// </summary>
    public partial class EditRegEx : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Literal RegExLibraryTitle;
		protected System.Web.UI.WebControls.LinkButton CreateRegExHyperLink;
		protected System.Web.UI.WebControls.Button ApplyChangesButton;
		protected System.Web.UI.WebControls.Label RegExOptionTitleLabel;
		protected System.Web.UI.WebControls.Label TestRegExTitle;
		protected System.Web.UI.WebControls.Button TextRegExButton;
		protected System.Web.UI.WebControls.Label TestExpressionValueLabel;
		protected System.Web.UI.WebControls.TextBox TestValueTextBox;
		protected System.Web.UI.WebControls.Label RegularExpressionTestLabel;
		protected System.Web.UI.WebControls.Button CreateNewRegExButton;
		protected System.Web.UI.WebControls.Label RegExErrorMessageLabel;
		protected System.Web.UI.WebControls.Button DeleteRegExButton;
		protected System.Web.UI.WebControls.Label RegularExpressionLabel;
		protected System.Web.UI.WebControls.DropDownList RegExDropDownList;
		protected System.Web.UI.WebControls.TextBox RegularExpressionTextbox;
		protected System.Web.UI.WebControls.TextBox ErrorMessageTextbox;
		protected System.Web.UI.WebControls.TextBox TestExpressionTextbox;
		protected System.Web.UI.WebControls.Label RegExNameLabel;
		protected System.Web.UI.WebControls.TextBox RegExDescriptionTextbox;
		protected System.Web.UI.WebControls.PlaceHolder RegExOptionsPlaceHolder;
		protected System.Web.UI.WebControls.Button MakeBuiltInRegExButton;
		protected System.Web.UI.WebControls.Button CancelRegExButton;
		protected System.Web.UI.WebControls.HyperLink RegExLibComHyperLink;
		protected System.Web.UI.WebControls.Label RegExToEditLabel;

		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.RegexLib);

			MessageLabel.Visible = false;
			SetupSecurity();
			LocalizePage();

			if (!Page.IsPostBack)
			{
				BindFields();
			}
		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessRegExEditor, true);
		}

		private void LocalizePage()
		{
			RegExLibraryTitle.Text = GetPageResource("RegExLibraryTitle");
			CreateRegExHyperLink.Text = GetPageResource("CreateRegExHyperLink");
			RegExToEditLabel.Text = GetPageResource("RegExToEditLabel");
			TestRegExTitle.Text = GetPageResource("TestRegExTitle");
			RegularExpressionTestLabel.Text = GetPageResource("RegularExpressionLabel");
			TestExpressionValueLabel.Text = GetPageResource("TestExpressionValueLabel");
			TextRegExButton.Text = GetPageResource("TextRegExButton");
			MakeBuiltInRegExButton.Text = GetPageResource("MakeBuiltInRegExButton");
			RegExToEditLabel.Text = GetPageResource("RegExToEditLabel");
			CreateNewRegExButton.Text = GetPageResource("CreateNewRegExButton");
			RegularExpressionLabel.Text = GetPageResource("RegularExpressionLabel");
			RegExNameLabel.Text = GetPageResource("RegExNameLabel");
			RegExLibComHyperLink.Text = GetPageResource("RegExLibComHyperLink");
			ApplyChangesButton.Text = GetPageResource("ApplyChangesButton");
			DeleteRegExButton.Text = GetPageResource("DeleteRegExButton");
			CancelRegExButton.Text = GetPageResource("CancelRegExButton");
            RegExErrorMessageLabel.Text = GetPageResource("RegExErrorMessageLabel");
		}

		/// <summary>
		/// Get the current DB data and fill 
		/// the fields with them
		/// </summary>
		private void BindFields()
		{
			// Header.SurveyId = SurveyId;
            ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;

			MakeBuiltInRegExButton.Visible = NSurveyUser.Identity.IsAdmin;

			if (NSurveyUser.Identity.IsAdmin)
			{
				// Get all available regex including built in one
				RegExDropDownList.DataSource = new RegularExpressions().GetAllRegularExpressionsList();
			}
			else
			{
				// Get only regex owned by the user
				RegExDropDownList.DataSource = new RegularExpressions().GetEditableRegularExpressionsListOfUser(NSurveyUser.Identity.UserId);
			}
			
			RegExDropDownList.DataMember = "RegularExpressions";
			RegExDropDownList.DataTextField = "Description";
			RegExDropDownList.DataValueField = "RegularExpressionId";
			RegExDropDownList.DataBind();

			TranslateListControl(RegExDropDownList);

			if (RegExDropDownList.Items.Count > 0)
			{
				RegExDropDownList.Items.Insert(0, 
					new ListItem(GetPageResource("SelectRegExMessage"),"0"));
			}
			else
			{
				RegExDropDownList.Items.Insert(0, 
					new ListItem(GetPageResource("CreateARegExMessage"),"0"));
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
			this.RegExDropDownList.SelectedIndexChanged += new System.EventHandler(this.RegExDropDownList_SelectedIndexChanged);
			this.CreateRegExHyperLink.Click += new System.EventHandler(this.CreateRegExHyperLink_Click);
			this.CreateNewRegExButton.Click += new System.EventHandler(this.CreateNewRegExButton_Click);
			this.ApplyChangesButton.Click += new System.EventHandler(this.ApplyChangesButton_Click);
			this.DeleteRegExButton.Click += new System.EventHandler(this.DeleteRegExButton_Click);
			this.MakeBuiltInRegExButton.Click += new System.EventHandler(this.MakeBuiltInRegExButton_Click);
			this.CancelRegExButton.Click += new System.EventHandler(this.CancelRegExButton_Click);
			this.TextRegExButton.Click += new System.EventHandler(this.TextRegExButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CreateNewRegExButton_Click(object sender, System.EventArgs e)
		{
			if (!ValidateFields())
			{
				return;
			}

			// Create a new regular expression entry in the database
			RegularExpressionData regularExpressionData = new RegularExpressionData();
			RegularExpressionData.RegularExpressionsRow regularExpression = regularExpressionData.RegularExpressions.NewRegularExpressionsRow();
			regularExpression.RegExpression = RegularExpressionTextbox.Text;
			regularExpression.RegExMessage = ErrorMessageTextbox.Text.Length > 0 ? 
				ErrorMessageTextbox.Text : null;
			regularExpression.Description = RegExDescriptionTextbox.Text;
			regularExpressionData.RegularExpressions.AddRegularExpressionsRow(regularExpression);
			new RegularExpression().AddRegularExpression(regularExpressionData, NSurveyUser.Identity.UserId);

			MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExAddedMessage"));
			ResetUIState();
		}

		private bool ValidateFields()
		{
			if (RegularExpressionTextbox.Text.Length == 0)
			{
				MessageLabel.Visible = true;
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExRequiredMessage"));
				return false;
			}

			if (RegExDescriptionTextbox.Text.Length == 0)
			{
				MessageLabel.Visible = true;
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExNameRequiredMessage"));
				return false;
			}

			return true;
		}

		private void TextRegExButton_Click(object sender, System.EventArgs e)
		{
			if (TestExpressionValueLabel.Text.Length == 0 || TestExpressionTextbox.Text.Length == 0 )
			{
				MessageLabel.Visible = true;
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExTestValueRequiredMessage"));
				return;
			}
			else
			{
				if (Regex.IsMatch(TestValueTextBox.Text, TestExpressionTextbox.Text))
				{
					MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExPassedMessage"));
				}
				else
				{

					MessageLabel.Visible = true;
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExFailedMessage"));
				}
			}

		}

		private void CreateRegExHyperLink_Click(object sender, System.EventArgs e)
		{
			RegExOptionTitleLabel.Text = GetPageResource("CreateRegExOptionTitleLabel");
			RegExDropDownList.ClearSelection();
			ErrorMessageTextbox.Text = string.Empty;
			RegularExpressionTextbox.Text = string.Empty;
			RegExDescriptionTextbox.Text = string.Empty;

			ApplyChangesButton.Visible = false;
			DeleteRegExButton.Visible = false;
			MakeBuiltInRegExButton.Visible = false;
			CreateNewRegExButton.Visible = true;
			RegExOptionsPlaceHolder.Visible = true;

		}

		private void RegExDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (RegExDropDownList.SelectedValue == "0")
			{
				ResetUIState();
			}
			else
			{
				
				RegExOptionTitleLabel.Text = GetPageResource("EditRegExOptionTitleLabel");
				DeleteRegExButton.Attributes.Add("onClick",
					"javascript:if(confirm('" +((PageBase)Page).GetPageResource("DeleteRegExConfirmationMessage")+ "')== false) return false;");

				RegularExpressionData regularExpressionData = 
					new RegularExpressions().GetRegularExpressionById(int.Parse(RegExDropDownList.SelectedValue));

				ErrorMessageTextbox.Text = regularExpressionData.RegularExpressions[0].RegExMessage;
				RegularExpressionTextbox.Text = regularExpressionData.RegularExpressions[0].RegExpression;
				RegExDescriptionTextbox.Text = regularExpressionData.RegularExpressions[0].Description;

				ApplyChangesButton.Visible = true;
				DeleteRegExButton.Visible = true;
				MakeBuiltInRegExButton.Visible = 
					NSurveyUser.Identity.IsAdmin && !regularExpressionData.RegularExpressions[0].BuiltIn;
				CreateNewRegExButton.Visible = false;
				RegExOptionsPlaceHolder.Visible = true;


				if (regularExpressionData.RegularExpressions[0].BuiltIn)
				{
					DeleteRegExButton.Attributes.Add("onClick",
						"javascript:if(confirm('" +((PageBase)Page).GetPageResource("DeleteBuiltInRegExConfirmationMessage")+ "')== false) return false;");
					ApplyChangesButton.Attributes.Add("onClick",
						"javascript:if(confirm('" +((PageBase)Page).GetPageResource("ApplyBuiltInRegExChangesConfirmationMessage")+ "')== false) return false;");
				}
				else
				{
					DeleteRegExButton.Attributes.Remove("onClick");
					ApplyChangesButton.Attributes.Remove("onClick");
					MakeBuiltInRegExButton.Attributes.Add("onClick",
						"javascript:if(confirm('" +((PageBase)Page).GetPageResource("MakeBuiltInRegExConfirmationMessage")+ "')== false) return false;");
				}
			}
		}

		private void CancelRegExButton_Click(object sender, System.EventArgs e)
		{
			ResetUIState();
		}

		private void ResetUIState()
		{
			BindFields();
			ErrorMessageTextbox.Text = string.Empty;
			RegularExpressionTextbox.Text = string.Empty;
			RegExDescriptionTextbox.Text = string.Empty;
			RegExOptionsPlaceHolder.Visible = false;
		}

		private void ApplyChangesButton_Click(object sender, System.EventArgs e)
		{
			// Create a new regular expression entry in the database
			RegularExpressionData regularExpressionData = new RegularExpressionData();
			RegularExpressionData.RegularExpressionsRow regularExpression = regularExpressionData.RegularExpressions.NewRegularExpressionsRow();
			regularExpression.RegularExpressionId = int.Parse(RegExDropDownList.SelectedValue);
			regularExpression.RegExpression = RegularExpressionTextbox.Text;
			regularExpression.RegExMessage = ErrorMessageTextbox.Text.Length > 0 ? 
				ErrorMessageTextbox.Text : null;
			regularExpression.Description = RegExDescriptionTextbox.Text;
			regularExpressionData.RegularExpressions.AddRegularExpressionsRow(regularExpression);
			new RegularExpression().UpdateRegularExpression(regularExpressionData);

			MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExUpdatedMessage"));
			ResetUIState();
		}

		private void MakeBuiltInRegExButton_Click(object sender, System.EventArgs e)
		{
			new RegularExpression().SetBuiltInRegularExpression(int.Parse(RegExDropDownList.SelectedValue));
			DeleteRegExButton.Attributes.Remove("onClick");
			ApplyChangesButton.Attributes.Remove("onClick");
			MakeBuiltInRegExButton.Visible = false;
			MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExBuiltInConvertedMessage"));
		}

		private void DeleteRegExButton_Click(object sender, System.EventArgs e)
		{
			new RegularExpression().DeleteRegularExpressionById(int.Parse(RegExDropDownList.SelectedValue));
			ResetUIState();
			MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("RegExDeleteedMessage"));
		}
	}

}
