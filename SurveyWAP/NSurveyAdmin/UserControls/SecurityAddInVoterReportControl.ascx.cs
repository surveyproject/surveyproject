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
	using System.Collections.Specialized;

	/// <summary>
	///	Handles the survey's page breaks options	
	/// </summary>
	public partial class SecurityAddInVoterReportControl : System.Web.UI.UserControl
	{

		public string AddInDescription
		{
			get { return _addInDescription; }
			set { _addInDescription = value; }
		}

		public NameValueCollection AddInVoterData
		{
			get { return _addInVoterData; }
			set { _addInVoterData = value; }
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			AddInDescriptionTitle.Text = AddInDescription;
			Table voterDataTable = new Table();
			voterDataTable.CssClass = "Innertext";
			TableCell voterDataCell;
			TableRow voterDataRow;

			for (int i=0; i<AddInVoterData.Count;i++)
			{
				voterDataRow = new TableRow();
				voterDataCell = new TableCell();
				voterDataCell.Attributes.Add("width","160");
				Label keyName = new Label();
				keyName.ControlStyle.Font.Bold = true;
				keyName.Text  = AddInVoterData.GetKey(i) + " :";
				voterDataCell.Controls.Add(keyName);
				voterDataRow.Cells.Add(voterDataCell);
	
				voterDataCell = new TableCell();
				Label keyValue = new Label();
				keyValue.ControlStyle.Font.Bold = false;
				keyValue.Text  = AddInVoterData[i];
				voterDataCell.Controls.Add(keyValue);
				voterDataRow.Cells.Add(voterDataCell);
				voterDataTable.Rows.Add(voterDataRow);
			}

			VoterDetailsPlaceHolder.Controls.Add(voterDataTable);
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		string _addInDescription;
		protected System.Web.UI.WebControls.Literal AddInDescriptionTitle;
		protected System.Web.UI.WebControls.PlaceHolder VoterDetailsPlaceHolder;
		NameValueCollection _addInVoterData;
	}
}
