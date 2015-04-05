using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using System.Data;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class QuestionGroups : PageBase
    {

        protected System.Web.UI.WebControls.Label MessageLabel;

        public int SelectedGroupID
        {
            get { return (ViewState["selectedGroup"] != null) ? (int)ViewState["selectedGroup"] : -1; }
            set { ViewState["selectedGroup"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Visible = false;
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.QuestionGroups);
            if (!Page.IsPostBack)
            {
                BindLanguages();
                LocalizePage();
                BindFields();
                SetupSecurity();
            }

        }


        private void LocalizePage()
        {
            QGroupsExlpainLabel.Text = GetPageResource("QGroupsExplain");
            lbAddNew.Text = GetPageResource("AddNewGroup");
            lbAddNewSubGroup.Text = GetPageResource("AddNewSubgroup");
            QuestionGroupLegend.Text = GetPageResource("QuestionGroupLegend");
            
        }
        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.AccessQuestionGroupRight, true);
        }
        void BindFields()
        {
            DataView dv;
            QuestionGroupsData groups = new Votations.NSurvey.DataAccess.QuestionGroups().GetAll(ddlLanguage.SelectedValue);
            dv = groups.DefaultViewManager.CreateDataView(groups.Tables[0]);
            dv.RowFilter = "ParentGroupId =-1 or parentGroupId is null ";
            dv.Sort = "DisplayOrder asc";


            // when no results found we add new empty row and clear it cells
            if (dv.Count == 0)
            {
                if (gvGroups.ShowFooter)
                {
                    dv.AddNew();
                    gvGroups.DataSource = dv;

                    gvGroups.DataBind();
                    foreach (TableCell cell in gvGroups.Rows[0].Cells)
                    {
                        cell.Controls.Clear();
                        cell.Controls.Add(new Literal());
                    }
                    gvGroups.Rows[0].Visible = false;
                    gvSubGroup.Visible = false;
                    gvGroups.Visible = true;
                }
                else
                {
                    gvSubGroup.Visible = false;
                    gvGroups.Visible = false;
                }
                return;
            }
            gvGroups.Visible = true;
            gvSubGroup.Visible = true;
            gvGroups.DataSource = dv;
            gvGroups.DataBind();

            if ((SelectedGroupID < 0) && (dv.Table.Rows.Count > 0))
            {
                SelectedGroupID = ((QuestionGroupsData.QuestionGroupsRow)dv[0].Row).ID;
            }

            foreach (GridViewRow row in gvGroups.Rows)
            {
                if (((QuestionGroupsData.QuestionGroupsRow)dv[row.DataItemIndex].Row).ID == SelectedGroupID)
                {
                    RadioButton rb = (RadioButton)row.FindControl("rbButton");
                    rb.Checked = true;
                    if (lblGroupName.Text == string.Empty)
                        lblGroupName.Text = ((QuestionGroupsData.QuestionGroupsRow)dv[0].Row).GroupName;
                    break;
                }
            }

            DataView dv1 = groups.DefaultViewManager.CreateDataView(groups.Tables[0]);
            dv1.RowFilter = "ParentGroupId = " + SelectedGroupID.ToString();
            dv1.Sort = "DisplayOrder asc";

            // when no results found we add new empty row and clear it cells
            if (dv1.Count == 0)
            {
                dv1.AddNew();
                gvSubGroup.DataSource = dv1;
                gvSubGroup.DataBind();
                foreach (TableCell cell in gvSubGroup.Rows[0].Cells)
                {
                    cell.Controls.Clear();
                    cell.Controls.Add(new Literal());
                }
            }
            else
            {
                gvSubGroup.DataSource = dv1;
                gvSubGroup.DataBind();
            }
        }

        private void BindLanguages()
        {
            MultiLanguageMode languageMode = new MultiLanguages().GetMultiLanguageMode(SurveyId);
            if (languageMode != MultiLanguageMode.None)
            {
                ddlLanguage.Items.Clear();
                MultiLanguageData surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);
                foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
                {
                    ListItem defaultItem = new ListItem(GetPageResource(language.LanguageDescription), language.LanguageCode);
                    if (language.DefaultLanguage)
                    {
                        defaultItem.Value = "";
                        defaultItem.Text += " " + GetPageResource("LanguageDefaultText");
                    }

                    ddlLanguage.Items.Add(defaultItem);
                }

                ddlLanguage.Visible = true;
                Labellanguage.Visible = true;
            }
            else
            {
                ddlLanguage.Visible = false;
                Labellanguage.Visible = false;
            }
        }

        protected void OnReorderGroup(object sender, CommandEventArgs e)
        {
            int gid = int.Parse(e.CommandArgument.ToString().Split(',')[1]);
            IDAL.QuestionGroupDisplayOrder way = (e.CommandArgument.ToString().Split(',')[0].ToLower() == "up") ? IDAL.QuestionGroupDisplayOrder.Up : IDAL.QuestionGroupDisplayOrder.Down;
            new Votations.NSurvey.DataAccess.QuestionGroups().UpdateDisplayOrder(gid, way);
            BindFields();
        }

        protected void OnReorderSubGroup(object sender, CommandEventArgs e)
        {
            int gid = int.Parse(e.CommandArgument.ToString().Split(',')[1]);
            IDAL.QuestionGroupDisplayOrder way = (e.CommandArgument.ToString().Split(',')[0].ToLower() == "up") ? IDAL.QuestionGroupDisplayOrder.Up : IDAL.QuestionGroupDisplayOrder.Down;
            new Votations.NSurvey.DataAccess.QuestionGroups().UpdateDisplayOrder(gid, way);
            BindFields();
        }

        protected void OnGroupEditCancel(object sender, System.EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).Parent.Parent);
            int rowIndex = row.RowIndex;
            gvGroups.SelectedIndex = -1;
            gvGroups.EditIndex = -1;
            if (rowIndex >= 0)
                gvGroups.Rows[rowIndex].RowState = DataControlRowState.Normal;
            gvGroups.ShowFooter = false;

            BindFields();
        }

        protected void OnSubGroupEditCancel(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).Parent.Parent);
            int rowIndex = row.RowIndex;
            gvSubGroup.SelectedIndex = -1;
            gvSubGroup.EditIndex = -1;
            if (rowIndex >= 0)
                gvSubGroup.Rows[rowIndex].RowState = DataControlRowState.Normal;
            gvSubGroup.ShowFooter = false;

            BindFields();
        }

        protected void OnGroupEditOK(object sender, CommandEventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).Parent.Parent);
            int rowIndex = row.RowIndex;

            gvGroups.SelectedIndex = -1;
            gvGroups.EditIndex = -1;
            if (rowIndex >= 0)
            {
                gvGroups.Rows[rowIndex].RowState = DataControlRowState.Normal;
            }

            gvGroups.ShowFooter = false;

            if (e.CommandName == "GroupEdit")
            {
                TextBox tb = (TextBox)row.FindControl("txtGroupName");
                new Votations.NSurvey.DataAccess.QuestionGroups().UpdateGroup(int.Parse(e.CommandArgument.ToString()), null, tb.Text, ddlLanguage.SelectedValue);
            }
            else if (e.CommandName == "GroupInsert")
            {
                TextBox tb = (TextBox)row.FindControl("txtGroupName");
                if (tb.Text.Length == 0)
                {
                    ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingGroupNameMessage"));
                    MessageLabel.Visible = true;
                    return;
                }

                new Votations.NSurvey.DataAccess.QuestionGroups().AddNewGroup(tb.Text, null, ddlLanguage.SelectedValue);
            }

            BindFields();
        }

        protected void OnSubGroupEditOK(object sender, CommandEventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).Parent.Parent);
            int rowIndex = row.RowIndex;

            gvSubGroup.SelectedIndex = -1;
            gvSubGroup.EditIndex = -1;
            if (rowIndex >= 0)
            {
                gvSubGroup.Rows[rowIndex].RowState = DataControlRowState.Normal;
            }

            gvSubGroup.ShowFooter = false;

            if (e.CommandName == "GroupEdit")
            {
                TextBox tb = (TextBox)row.FindControl("txtSubGroupName");
                int gid = int.Parse(e.CommandArgument.ToString());
                new Votations.NSurvey.DataAccess.QuestionGroups().UpdateGroup(gid, SelectedGroupID, tb.Text, ddlLanguage.SelectedValue);
            }
            else if (e.CommandName == "GroupInsert")
            {

                TextBox tb = (TextBox)row.FindControl("txtSubGroupName");
                if (tb.Text.Length == 0)
                {
                    ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingSubGroupNameMessage"));
                    MessageLabel.Visible = true;
                    return;
                }
                new Votations.NSurvey.DataAccess.QuestionGroups().AddNewGroup(tb.Text, SelectedGroupID, ddlLanguage.SelectedValue);
            }

            BindFields();
        }

        protected void OnGroupEdit(object sender, CommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;

            // Select the row
            gvGroups.SelectedIndex = rowIndex;

            // Set the edit row
            gvGroups.EditIndex = rowIndex;

            // Change the row state
            gvGroups.Rows[rowIndex].RowState = DataControlRowState.Edit;
            BindFields();
        }

        protected void OnSubGroupEdit(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;

            // Select the row
            gvSubGroup.SelectedIndex = rowIndex;

            // Set the edit row
            gvSubGroup.EditIndex = rowIndex;

            // Change the row state
            gvSubGroup.Rows[rowIndex].RowState = DataControlRowState.Edit;

            BindFields();
        }

        protected void OnGroupDelete(object sender, CommandEventArgs e)
        {
            int gid = int.Parse(e.CommandArgument.ToString());
            new Votations.NSurvey.DataAccess.QuestionGroups().DeleteGroup(gid);
            SelectedGroupID = -1;
            lblGroupName.Text = string.Empty;
            BindFields();
        }

        protected void OnSubGroupDelete(object sender, CommandEventArgs e)
        {
            int gid = int.Parse(e.CommandArgument.ToString());
            new Votations.NSurvey.DataAccess.QuestionGroups().DeleteGroup(gid);
            BindFields();
        }

        protected void OnAddNewGroup(object sender, System.EventArgs e)
        {
            gvGroups.ShowFooter = true;
            BindFields();
        }
        protected void OnAddNewSubGroup(object sender, EventArgs e)
        {
            gvSubGroup.ShowFooter = true;
            BindFields();
        }
        protected void OnGroupSelectionChanged(object sender, System.EventArgs e)
        {
            RadioButton rb;

            foreach (GridViewRow row in gvGroups.Rows)
            {
                rb = (RadioButton)row.FindControl("rbButton");
                rb.Checked = false;
            }

            rb = (RadioButton)sender;
            rb.Checked = true;
            string[] vals = ((string)rb.Attributes["value"]).Split(',');
            SelectedGroupID = int.Parse(vals[0]);
            lblGroupName.Text = vals.Length > 1 ? vals[1] : string.Empty;
            BindFields();
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFields();
        }

    }
}