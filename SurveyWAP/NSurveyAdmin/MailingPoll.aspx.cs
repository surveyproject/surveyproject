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
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.Resources;
using Votations.NSurvey.WebControls;
using Votations.NSurvey.WebControls.UI;
using Votations.NSurvey.WebAdmin.UserControls;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the survey statistics
	/// </summary>
	public partial class MailingPoll : PageBase
	{
		new protected HeaderControl Header;

//        see ...designer.aspx.cs for: 
//        protected System.Web.UI.WebControls.Label FailedSendingLabel;
//        protected ResultsBar ProgressBar;
          protected System.Web.UI.WebControls.Label AllInvitationsSendMessage;


		private void Page_Load(object sender, System.EventArgs e)
		{
			LocalizePage();
			// Header.SurveyId = SurveyId;
            ((Wap)Master).HeaderControl.SurveyId = SurveyId;

            try
            {
                // Check if the task has been finished
                object obj = SyncDataStore.GetRecords(Session.SessionID);
                if (obj != null)
                {
                    AllInvitationsSendMessage.Text = GetPageResource("AllInvitationSendMessage");
                    AllInvitationsSendMessage.CssClass = "SuccessMessage"; 
//                  Response.Write(GetPageResource("AllInvitationSendMessage"));
                    ProgressBar.Visible = false;
                    SyncDataStore.ClearAll();
                }
                else
                {
                    object error = SyncDataStore.GetRecords(Session.SessionID + ":" + "Error");
                    if (error != null)
                    {
                        SyncDataStore.Remove(Session.SessionID + ":" + "Error");
                        throw (Exception)error;
                    }
                    else
                    {
                        // Retrieve the current progress of the task
                        object mailingProgress = SyncDataStore.GetRecords(Session.SessionID + ":" + "Progress");
                        if (mailingProgress != null)
                        {
                            ProgressBar.Progress = int.Parse(mailingProgress.ToString());
                        }
                        else
                        {
                            ProgressBar.Progress = 0;
                        }

                        object failedEmails = SyncDataStore.GetRecords(Session.SessionID + ":" + "FailedEmails");
                        if (failedEmails != null)
                        {
                            FailedSendingLabel.Text = "Number of failed emails : " + failedEmails.ToString();
                        }

                        ProgressBar.TableWidth = Unit.Pixel(950);
                        Response.AddHeader("Refresh", "2");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    Response.Write(@"<script type='text/javascript' language='javascript'>alert('" + ex.InnerException.InnerException.Message.Replace("'", "\\'").Replace(Environment.NewLine, "\\n") + "')</script>");
                }
                else
                {
                    Response.Write(@"<script type='text/javascript' language='javascript'>alert('" + ex.Message.Replace("'", "\\'").Replace(Environment.NewLine, "\\n") + "')</script>");
                }
            }

		}

		private void LocalizePage()
		{
			ProcessInfoLabel.Text = GetPageResource("ProcessInfoLabel");
			PleaseWaitInfo.Text = GetPageResource("PleaseWaitInfo");
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected System.Web.UI.WebControls.Literal PleaseWaitInfo;
		protected System.Web.UI.WebControls.Literal ProcessInfoLabel;


	}

}
