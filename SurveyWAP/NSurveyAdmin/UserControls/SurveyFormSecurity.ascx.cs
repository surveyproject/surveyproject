using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Security;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    public partial class SurveyFormSecurity : System.Web.UI.UserControl
    {
        public int SurveyId
        {
            get { return (ViewState["SurveyID"] == null) ? -1 : int.Parse(ViewState["SurveyID"].ToString()); }
            set
            {
                ViewState["SurveyID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LocalizePage();

            if (!Page.IsPostBack)
            {
                SurveyList.SurveyId = SurveyId;
                SurveyList.BindDropDown();
                // Header.SurveyId = SurveyId;
                ((Wap)((PageBase)Page).Master).HeaderControl.SurveyId = SurveyId;
                FillFields();
            }

            BuildAddInList();
        }


        private void BuildAddInList()
        {
            WebSecurityAddInCollection _securityAddIns;
            Control adminControl;
            Table addInContainer = new Table();
            addInContainer.Width = Unit.Percentage(100);
            Table addInTable = new Table();
            // moved to css files:
            //			addInTable.CellSpacing = 2;
            //			addInTable.CellPadding = 4;
            addInTable.CssClass = "questionBuilder";
            Style controlStyle = new Style();
            controlStyle.CssClass = "addinsLayout";
            _securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetWebSecurityAddIns(SurveyId), ViewState, null);

            if (_securityAddIns.Count == 0)
            {
                SecurityOptionsPlaceHolder.Visible = false;
                // No Addins
                addInTable.Rows.Add(BuildAddInOptionsRow(null, _securityAddIns.Count));
            }
            else
            {
                SecurityOptionsPlaceHolder.Visible = true;
                for (int i = 0; i < _securityAddIns.Count; i++)
                {
                    addInTable.Rows.Add(BuildAddInOptionsRow(_securityAddIns[i], _securityAddIns.Count));
                    adminControl = _securityAddIns[i].GetAdministrationInterface(controlStyle);
                    if (adminControl != null)
                    {
                        addInTable.Rows.Add(BuildRow(adminControl));
                    }
                    else
                    {
                        addInTable.Rows.Add(BuildRow(new LiteralControl(((PageBase)Page).GetPageResource("AddInAdminNotAvailableMessage"))));
                    }
                    addInContainer.Rows.Add(BuildRow(addInTable));

                    AddInListPlaceHolder.Controls.Add(addInContainer);

                    // Creates a new page
                    addInContainer = new Table();
                    addInContainer.Width = Unit.Percentage(100);
                    addInTable = new Table();
                    // moved to css file
                    //					addInTable.CellSpacing = 2;
                    //					addInTable.CellPadding = 4;
                    addInTable.CssClass = "questionBuilder";
                }
            }

            addInContainer.Rows.Add(BuildRow(addInTable));
            AddInListPlaceHolder.Controls.Add(addInContainer);
        }

        /// <summary>
        /// Builds a row with the options available for an addin
        /// </summary>
        /// <returns>a tablerow instance with the options</returns>
        private TableRow BuildAddInOptionsRow(IWebSecurityAddIn addIn, int totalAddIns)
        {
            // Creates a new addin options control
            SecurityAddInOptionsControl addInOptionsControl =
                (SecurityAddInOptionsControl)LoadControl("SecurityAddInOptionsControl.ascx");

            addInOptionsControl.SecurityAddIn = addIn;
            addInOptionsControl.SurveyId = SurveyId;
            addInOptionsControl.TotalAddIns = totalAddIns;
            return BuildRow(addInOptionsControl);
        }

        private TableRow BuildRow(Control child)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Controls.Add(child);
            row.Cells.Add(cell);
            return row;
        }

        private void LocalizePage()
        {
            SurveySecurityTitle.Text = ((PageBase)Page).GetPageResource("SurveySecurityTitle");
            UnAuthentifiedUserActionLabel.Text = ((PageBase)Page).GetPageResource("UnAuthentifiedUserActionLabel");
        }


        /// <summary>
        /// Get the current DB stats and fill 
        /// the label with them
        /// </summary>
        private void FillFields()
        {
            ActionsDropDownList.DataSource = new SecurityAddIns().GetUnAuthentifiedUserActions();
            ActionsDropDownList.DataTextField = "Description";
            ActionsDropDownList.DataValueField = "UnAuthentifiedUserActionId";
            ActionsDropDownList.DataBind();
            ((PageBase)Page).TranslateListControl(ActionsDropDownList);
            ActionsDropDownList.SelectedValue = new Surveys().GetSurveyUnAuthentifiedUserAction(SurveyId).ToString();
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
            this.ActionsDropDownList.SelectedIndexChanged += new System.EventHandler(this.ActionsDropDownList_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void ActionsDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            new Survey().UpdateUnAuthentifiedUserActions(SurveyId, int.Parse(ActionsDropDownList.SelectedValue));
        }

    }
}