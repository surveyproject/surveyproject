namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Store a list of answers posted by the user
    /// in a section and show an overview of all
    /// answers / sections in a grid
    /// </summary>
    public class SectionAnswersGridItem : AnswerItem
    {
        private string _addSectionLinkText;
        private AnswerData _answers;
        private string _deleteSectionLinkText;
        private string _editSectionLinkText;
        private int[] _gridAnswers = new int[0];
        private int _maxSections = 0;
        private Style _sectionGridAnswersAlternatingItemStyle;
        private Style _sectionGridAnswersHeaderStyle;
        private Style _sectionGridAnswersItemStyle;
        private Style _sectionGridAnswersStyle;

        public event SectionAnswersEventHandler AddSection;

        public event SectionAnswersEventHandler DeleteSection;

        public event SectionAnswersEventHandler EditSection;

        /// <summary>
        /// A new section has been requested
        /// </summary>
        protected virtual void AddSectionGridButton_Click(object sender, CommandEventArgs e)
        {
            this.OnSectionAdd(new SectionAnswersItemEventArgs(int.Parse(e.CommandArgument.ToString()), null));
        }

        public void BindSectionGrid()
        {
            this.Controls.Clear();
            if (this.GridVoterAnswers.Count > 0)
            {
                TableCell cell;
                Table child = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;
                child.CellSpacing = 2;
                child.CellPadding = 2;
                child.ControlStyle.CopyFrom(this.SectionGridAnswersStyle);
                TableRow row = new TableRow();
                foreach (AnswerData.AnswersRow row2 in this.Answers.Answers)
                {
                    if (this.IsVisible(row2.AnswerId))
                    {
                        cell = new TableCell();
                        cell.Text = row2.AnswerText;
                        cell.Wrap = false;
                        row.Cells.Add(cell);
                    }
                }
                cell = new TableCell();
                cell.Controls.Add(new LiteralControl("&nbsp;"));
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Controls.Add(new LiteralControl("&nbsp;"));
                row.Cells.Add(cell);
                row.ControlStyle.CopyFrom(this.SectionGridAnswersHeaderStyle);
                child.Rows.Add(row);
                cell = new TableCell();
                cell.Controls.Add(new LiteralControl("&nbsp;"));
                row.Cells.Add(cell);
                row.ControlStyle.CopyFrom(this.SectionGridAnswersHeaderStyle);
                child.Rows.Add(row);
                int sectionCount = this.GetSectionCount();
                for (int i = 0; i <= sectionCount; i++)
                {
                    row = new TableRow();
                    foreach (AnswerData.AnswersRow row3 in this.Answers.Answers)
                    {
                        if (!this.IsVisible(row3.AnswerId))
                        {
                            continue;
                        }
                        cell = new TableCell();
                        cell.VerticalAlign = VerticalAlign.Top;
                        int sectionAnswerIndex = this.GetSectionAnswerIndex(row3.AnswerId, i);
                        if (((sectionAnswerIndex != -1) && (this.GridVoterAnswers[sectionAnswerIndex].FieldText != null)) && ((((row3.TypeMode & 2) > 0) || ((row3.TypeMode & 4) > 0)) || ((row3.TypeMode & 8) > 0)))
                        {
                            cell.Text = this.GridVoterAnswers[sectionAnswerIndex].FieldText;
                        }
                        else if ((row3.TypeMode & 0x100) > 0)
                        {
                            if ((i % 2) == 0)
                            {
                                cell.Controls.Add(this.GenerateFileList(this.GridVoterAnswers[sectionAnswerIndex].FieldText, this.SectionGridAnswersAlternatingItemStyle));
                            }
                            else
                            {
                                cell.Controls.Add(this.GenerateFileList(this.GridVoterAnswers[sectionAnswerIndex].FieldText, this.SectionGridAnswersItemStyle));
                            }
                        }
                        else if ((row3.TypeMode & 1) > 0)
                        {
                            Image image = new Image();
                            image.ImageUrl = (sectionAnswerIndex == -1) ? (GlobalConfig.ImagesPath + "spot_off.gif") : (GlobalConfig.ImagesPath + "spot_on.gif");
                            cell.HorizontalAlign = HorizontalAlign.Center;
                            cell.Controls.Add(image);
                        }
                        else
                        {
                            cell.Text = "&nbsp;";
                        }
                        row.Cells.Add(cell);
                    }
                    LinkButton button = new LinkButton();
                    button.Text = this.AddSectionLinkText;
                    button.Command += new CommandEventHandler(this.AddSectionGridButton_Click);
                    button.CommandArgument = (i + 1).ToString();
                    button.Enabled = ((this.RenderMode == ControlRenderMode.Edit) || (this.MaxSections == 0)) || (sectionCount < (this.MaxSections - 1));
                    cell = new TableCell();
                    cell.Controls.Add(button);
                    cell.Wrap = false;
                    row.Cells.Add(cell);
                    LinkButton button2 = new LinkButton();
                    button2.Text = this.EditSectionLinkText;
                    button2.Command += new CommandEventHandler(this.EditSectionGridButton_Click);
                    button2.CommandArgument = i.ToString();
                    cell = new TableCell();
                    cell.Controls.Add(button2);
                    cell.Wrap = false;
                    row.Cells.Add(cell);
                    LinkButton button3 = new LinkButton();
                    button3.Text = this.DeleteSectionLinkText;
                    button3.Command += new CommandEventHandler(this.DeleteSectionGridButton_Click);
                    button3.CommandArgument = i.ToString();
                    cell = new TableCell();
                    cell.Controls.Add(button3);
                    cell.Wrap = false;
                    row.Cells.Add(cell);
                    if ((i % 2) == 0)
                    {
                        row.ControlStyle.CopyFrom(this.SectionGridAnswersAlternatingItemStyle);
                    }
                    else
                    {
                        row.ControlStyle.CopyFrom(this.SectionGridAnswersItemStyle);
                    }
                    child.Rows.Add(row);
                }
                this.Controls.Add(child);
            }
        }

        /// <summary>
        /// Creates the grid control "layout" and adds
        /// it to the overall control tree
        /// </summary>
        protected override void CreateChildControls()
        {
            this.BindSectionGrid();
        }

        /// <summary>
        /// A section must be deleted
        /// </summary>
        protected virtual void DeleteSectionGridButton_Click(object sender, CommandEventArgs e)
        {
            int sectionNumber = int.Parse(e.CommandArgument.ToString());
            for (int i = this.GridVoterAnswers.Count - 1; i >= 0; i--)
            {
                if (this.GridVoterAnswers[i].SectionNumber == sectionNumber)
                {
                    this.GridVoterAnswers.RemoveAt(i);
                }
            }
            for (int j = sectionNumber + 1; j < (this.GridVoterAnswers.Count + 1); j++)
            {
                this.SwitchAnswerSectionNumber(j, j - 1);
            }
            this.OnSectionDelete(new SectionAnswersItemEventArgs(sectionNumber, null));
            this.BindSectionGrid();
        }

        /// <summary>
        /// A section edit has been requested
        /// </summary>
        protected virtual void EditSectionGridButton_Click(object sender, CommandEventArgs e)
        {
            int sectionNumber = int.Parse(e.CommandArgument.ToString());
            this.OnSectionEdit(new SectionAnswersItemEventArgs(sectionNumber, this.GetSectionAnswers(sectionNumber)));
        }

        private Table GenerateFileList(string groupGuid, Style tableStyle)
        {
            Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;
            table.ControlStyle.CopyFrom(tableStyle);
            table.Rows.Clear();
            FileData guidFiles = new Votations.NSurvey.DataAccess.Answers().GetGuidFiles(groupGuid);
            if (guidFiles.Files.Rows.Count > 0)
            {
                foreach (FileData.FilesRow row in guidFiles.Files)
                {
                    table.Rows.Add(this.GetFileRow(row.FileId, row.GroupGuid, row.FileSize, row.FileName, row.FileType));
                }
                return table;
            }
            TableRow row2 = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = ResourceManager.GetString("NoFileUploadedMessage", base.LanguageCode);
            row2.Cells.Add(cell);
            table.Rows.Add(row2);
            return table;
        }

        protected virtual TableRow GetFileRow(int fileId, string groupGuid, int fileSize, string fileName, string fileType)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = fileName;
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = ((Math.Round((double) ((((double) fileSize) / 1048576.0) * 100000.0)) / 100000.0)).ToString("0.##") + ResourceManager.GetString("UploadFileSizeFormat", base.LanguageCode);
            row.Cells.Add(cell);
            return row;
        }

        /// <summary>
        /// Returns the all answers stored in the grid to the event subscribers 
        /// once the survey's page get posted
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            PostedAnswerDataCollection datas = new PostedAnswerDataCollection();
            foreach (GridAnswerData data in this.GridVoterAnswers)
            {
                datas.Add(new PostedAnswerData(this, data.AnswerId, data.SectionNumber, data.FieldText, data.TypeMode));
            }
            return datas;
        }

        /// <summary>
        /// Return the index of the give answer id in its section
        /// </summary>
        private int GetSectionAnswerIndex(int answerId, int sectionNumber)
        {
            for (int i = 0; i < this.GridVoterAnswers.Count; i++)
            {
                if ((this.GridVoterAnswers[i].AnswerId == answerId) && (this.GridVoterAnswers[i].SectionNumber == sectionNumber))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns only specific section number answers
        /// </summary>
        private GridAnswerDataCollection GetSectionAnswers(int sectionNumber)
        {
            GridAnswerDataCollection datas = new GridAnswerDataCollection();
            foreach (GridAnswerData data in this.GridVoterAnswers)
            {
                if (data.SectionNumber == sectionNumber)
                {
                    datas.Add(data);
                }
            }
            return datas;
        }

        /// <summary>
        /// Returns the section count based on the 
        /// current GridVoterAnswers content
        /// </summary>
        public int GetSectionCount()
        {
            int sectionNumber = -1;
            for (int i = 0; i < this.GridVoterAnswers.Count; i++)
            {
                if (this.GridVoterAnswers[i].SectionNumber > sectionNumber)
                {
                    sectionNumber = this.GridVoterAnswers[i].SectionNumber;
                }
            }
            return sectionNumber;
        }

        private bool IsVisible(int answerId)
        {
            if (this.GridAnswers.Length == 0)
            {
                return true;
            }
            for (int i = 0; i < this.GridAnswers.Length; i++)
            {
                if (this.GridAnswers[i] == answerId)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Post an event to notify subscribers that a new section
        /// must be shown
        /// </summary>
        protected virtual void OnSectionAdd(SectionAnswersItemEventArgs e)
        {
            if (this.AddSection != null)
            {
                this.AddSection(this, e);
            }
        }

        /// <summary>
        /// Post an event to notify subscribers that a section
        /// has been deleted
        /// </summary>
        protected virtual void OnSectionDelete(SectionAnswersItemEventArgs e)
        {
            if (this.DeleteSection != null)
            {
                this.DeleteSection(this, e);
            }
        }

        /// <summary>
        /// Post an event to notify subscribers that a section
        /// need to be edited
        /// </summary>
        protected virtual void OnSectionEdit(SectionAnswersItemEventArgs e)
        {
            if (this.EditSection != null)
            {
                this.EditSection(this, e);
            }
        }

        /// <summary>
        /// Change the all answers with the section number to the new section number
        /// </summary>
        private void SwitchAnswerSectionNumber(int sectionNumber, int newSectionNumber)
        {
            foreach (GridAnswerData data in this.GridVoterAnswers)
            {
                if (data.SectionNumber == sectionNumber)
                {
                    data.SectionNumber = newSectionNumber;
                }
            }
        }

        /// <summary>
        /// Text show to the user to add a new section
        /// </summary>
        public string AddSectionLinkText
        {
            get
            {
                if (this._addSectionLinkText != null)
                {
                    return this._addSectionLinkText;
                }
                return ResourceManager.GetString("AddSectionLinkText", base.LanguageCode);
            }
            set
            {
                this._addSectionLinkText = value;
            }
        }

        /// <summary>
        /// Answers infos 
        /// </summary>
        public AnswerData Answers
        {
            get
            {
                return this._answers;
            }
            set
            {
                this._answers = value;
            }
        }

        /// <summary>
        /// Text show to the user to delete a section
        /// </summary>
        public string DeleteSectionLinkText
        {
            get
            {
                if (this._deleteSectionLinkText != null)
                {
                    return this._deleteSectionLinkText;
                }
                return ResourceManager.GetString("DeleteSectionLinkText", base.LanguageCode);
            }
            set
            {
                this._deleteSectionLinkText = value;
            }
        }

        /// <summary>
        /// Text show to the user to edit a section
        /// </summary>
        public string EditSectionLinkText
        {
            get
            {
                if (this._editSectionLinkText != null)
                {
                    return this._editSectionLinkText;
                }
                return ResourceManager.GetString("EditSectionLinkText", base.LanguageCode);
            }
            set
            {
                this._editSectionLinkText = value;
            }
        }

        /// <summary>
        /// Answers that will be shown in the section's 
        /// grid overview
        /// </summary>
        public int[] GridAnswers
        {
            get
            {
                return this._gridAnswers;
            }
            set
            {
                this._gridAnswers = value;
            }
        }

        /// <summary>
        /// Contains all the answers to all
        /// sections
        /// </summary>
        public GridAnswerDataCollection GridVoterAnswers
        {
            get
            {
                if (this.ViewState["GridVoterAnswers"] == null)
                {
                    this.ViewState["GridVoterAnswers"] = new GridAnswerDataCollection();
                }
                return (GridAnswerDataCollection) this.ViewState["GridVoterAnswers"];
            }
            set
            {
                this.ViewState["GridVoterAnswers"] = value;
            }
        }

        /// <summary>
        /// how many section is the user allowed
        /// to create
        /// </summary>
        public int MaxSections
        {
            get
            {
                return this._maxSections;
            }
            set
            {
                this._maxSections = value;
            }
        }

        /// <summary>
        /// Sets the style for the section's answer grid overview items 
        /// </summary>
        public Style SectionGridAnswersAlternatingItemStyle
        {
            get
            {
                if (this._sectionGridAnswersAlternatingItemStyle != null)
                {
                    return this._sectionGridAnswersAlternatingItemStyle;
                }
                return new Style();
            }
            set
            {
                this._sectionGridAnswersAlternatingItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the section's answer grid header
        /// </summary>
        public Style SectionGridAnswersHeaderStyle
        {
            get
            {
                if (this._sectionGridAnswersHeaderStyle != null)
                {
                    return this._sectionGridAnswersHeaderStyle;
                }
                return new Style();
            }
            set
            {
                this._sectionGridAnswersHeaderStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the section's answer grid overview items 
        /// </summary>
        public Style SectionGridAnswersItemStyle
        {
            get
            {
                if (this._sectionGridAnswersItemStyle != null)
                {
                    return this._sectionGridAnswersItemStyle;
                }
                return new Style();
            }
            set
            {
                this._sectionGridAnswersItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the section's answer grid table 
        /// </summary>
        public Style SectionGridAnswersStyle
        {
            get
            {
                if (this._sectionGridAnswersStyle != null)
                {
                    return this._sectionGridAnswersStyle;
                }
                return new Style();
            }
            set
            {
                this._sectionGridAnswersStyle = value;
            }
        }
    }
}

