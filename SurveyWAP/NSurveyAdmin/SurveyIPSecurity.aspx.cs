using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using System.Data;
namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class SurveyIPSecurity : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sId = SurveyId;//This causes a redirection if survey id is not set
            UITabList.SetSecurityTabs((MsterPageTabs)Page.Master, UITabList.SecurityTabs.IPRange);
            SetupSecurity();
            LocalizePage();
            if (!IsPostBack)
            {
              
                BindFields();
            }

        }

        private void LocalizePage()
        {
            literalIPRangeStart.Text=GetPageResource("IPRangeStartTitle");
            literalIPRangeEnd.Text = GetPageResource("IPRangeEndTitle");
            literalIPRangesTitle.Text = GetPageResource("IPRangeFormTitle");
            lbAddNew.Text = GetPageResource("AddNew");
        }
        protected void BindFields()
        {
            SurveyIPRangeData data = new SurveyIPRange().GetAllSurveyIPranges(((PageBase)Page).SurveyId);
            DataView dv1 = data.DefaultViewManager.CreateDataView(data.Tables[0]);
          

            // when no results found we add new empty row and clear it cells
            if (dv1.Count == 0)
            {
                dv1.AddNew();
                gvIPRanges.DataSource = dv1;

                gvIPRanges.DataBind();
                foreach (TableCell cell in gvIPRanges.Rows[0].Cells)
                {
                    cell.Controls.Clear();
                    cell.Controls.Add(new Literal());
                }
                gvIPRanges.Rows[0].Visible = false;
            }
            else
            {
                gvIPRanges.DataSource = data.SurveyIPRange.Rows;

                gvIPRanges.DataBind();
            }

        }
        protected string GetIPSection(object IPAddressObj, int sectionNumber)
        {
            if (IPAddressObj == null) return "";
            string IPAddress = IPAddressObj as string;
          return  IPAddress==null?"":IPAddress.Split('.')[sectionNumber];
        }
        private void SetupSecurity()
        {
            CheckRight(Data.NSurveyRights.TokenSecurityRight, true);
        }


        protected void lbAddNew_Click(object sender, EventArgs e)
        {
            gvIPRanges.ShowFooter = true;
            BindFields();

        }

        private void InsertUpdateIPRange(bool isInsert,int? surveyIPRangeId,int surveyId, string IPStart, string IPEnd)
        {

            SurveyIPRangeData data = new SurveyIPRangeData();

            SurveyIPRangeData.SurveyIPRangeRow row = data.SurveyIPRange.NewSurveyIPRangeRow();
            row.SurveyId = surveyId;
            row.IPStart = IPStart;
            row.IPEnd = IPEnd;

            row.SurveyIPRangeId = surveyIPRangeId ?? 0;
               
            data.SurveyIPRange.Rows.Add(row);
            if (isInsert)
                new SurveyIPRange().AddNewSurveyIPRange(data);
            new SurveyIPRange().UpdateIPRange(data);

        }
        private string assembleIpString(string controlNamePrefix, GridViewRow row)
        {
            string retval = string.Empty;
            for (int i = 1; i <= 4; i++)
                retval += ((TextBox)row.FindControl(controlNamePrefix + i.ToString())).Text + ".";
            return retval.TrimEnd(new char[] { '.' });
        }
        private void InsertIPRange(GridViewRow row)
        {
            string startIp = assembleIpString("txtIpStartNew", row);
            string endIp = assembleIpString("txtIpEndNew", row);
            InsertUpdateIPRange(true,null,((PageBase)Page).SurveyId, startIp, endIp);
        }
        private void UpdateIPRange(GridViewRow row)
        {
            string startIp = assembleIpString("txtIpStart", row);
            string endIp = assembleIpString("txtIpEnd", row);
            int surveyIPRangeID = Convert.ToInt32(gvIPRanges.DataKeys[row.RowIndex].Value.ToString());
            InsertUpdateIPRange(false,surveyIPRangeID,((PageBase)Page).SurveyId, startIp, endIp);
        }
        private void DeleteIPRange(GridViewRow row)
        {
            int surveyIPRangeID = Convert.ToInt32(gvIPRanges.DataKeys[row.RowIndex].Value.ToString());
            new SurveyIPRange().DeleteSurveyIPRangeById(surveyIPRangeID);
        }
        protected void OnCommand(object sender, CommandEventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).Parent.Parent);
            int rowIndex = row.RowIndex;
            if (rowIndex >= 0)
            {
                 gvIPRanges.Rows[rowIndex].RowState = DataControlRowState.Normal;
            }
            switch (e.CommandName)
            {
                case "DoEdit": gvIPRanges.EditIndex = row.RowIndex; BindFields(); break;
                case "CancelEdit": gvIPRanges.EditIndex = -1; BindFields() ; break;
                case "CancelInsert": gvIPRanges.ShowFooter = false;  BindFields(); break;
                case "InsertOK": InsertIPRange(row); gvIPRanges.ShowFooter = false; BindFields(); break;
                case "EditOK": UpdateIPRange(row); gvIPRanges.EditIndex = -1; BindFields(); break;
                case "DoDelete": DeleteIPRange(row); gvIPRanges.EditIndex = -1; BindFields(); break;
            }

        }

    }
}