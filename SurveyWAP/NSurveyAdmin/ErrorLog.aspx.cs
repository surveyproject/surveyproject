using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.Data;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class ErrorLog : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UITabList.SetSurveyInfoTabs((MsterPageTabs)Page.Master, UITabList.SurveyInfoTabs.ErrorLog);

            SetupSecurity();

            if (!Page.IsPostBack)
            {
                LocalizePage();
            

            EditLogTextBox.Visible = true;
            EditLogPlaceHolder.Visible = true;
            EditLogTextBox.Focus();
            EditLogTextBox.Text = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/"+ "ErrorLog.txt"));
            }
        }

        private void SetupSecurity()
        {
            if (NSurveyUser.Identity.IsAdmin == false)
            {
                UINavigator.NavigateToAccessDenied(getSurveyId(), MenuIndex);
            }
        }

        private void LocalizePage()
        {
            EditLogTitle.Text = GetPageResource("EditLogTitle");
            EditLogCancelButton.Text = GetPageResource("EditLogCancel");
            EditLogSaveButton.Text = GetPageResource("EditLogSave");
        }
        

        protected void EditLogCancelButton_Click(object sender, EventArgs e)
        {
            EditLogTextBox.Text = string.Empty;
            EditLogTextBox.Visible = false;
            EditLogPlaceHolder.Visible = false;

            MessageLabel.Visible = false;
        }

        protected void EditLogSaveButton_Click(object sender, EventArgs e)
        {

            File.WriteAllText( System.Web.HttpContext.Current.Server.MapPath("~/App_Data/" + "ErrorLog.txt"), EditLogTextBox.Text);
            //EditLogTextBox.Text = string.Empty;
            //EditLogPlaceHolder.Visible = false;

            ShowNormalMessage(MessageLabel, GetPageResource("LogfileUpdatedMsg"));
        }

    }
}