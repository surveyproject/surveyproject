/**************************************************************************************************

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
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Threading;
using Votations.NSurvey;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.WebControlsFactories;
using Votations.NSurvey.Resources;


namespace Votations.NSurvey.WebControls.UI
{

    /// <summary>
    /// Abstract class that questions which needs
    /// postback handling of the answers and handling
    /// of multiple inner answer sections
    /// </summary>
    public abstract class SectionQuestion : ActiveQuestion
    {

        /// <summary>
        /// Sets the style for the section's answer grid header
        /// </summary>
        [
        Category("Styles"),
        DefaultValue(null),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public Style SectionGridAnswersHeaderStyle
        {
            get { return _sectionGridAnswersHeaderStyle == null ? new Style() : _sectionGridAnswersHeaderStyle; }
            set { _sectionGridAnswersHeaderStyle = value; }
        }

        /// <summary>
        /// Sets the style for the section's answer grid overview items 
        /// </summary>
        [
        Category("Styles"),
        DefaultValue(null),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public Style SectionGridAnswersItemStyle
        {
            get { return _sectionGridAnswersItemStyle == null ? new Style() : _sectionGridAnswersItemStyle; }
            set { _sectionGridAnswersItemStyle = value; }
        }

        /// <summary>
        /// Sets the style for the section's answer grid overview items 
        /// </summary>
        [
        Category("Styles"),
        DefaultValue(null),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public Style SectionGridAnswersAlternatingItemStyle
        {
            get { return _sectionGridAnswersAlternatingItemStyle == null ? new Style() : _sectionGridAnswersAlternatingItemStyle; }
            set { _sectionGridAnswersAlternatingItemStyle = value; }
        }

        /// <summary>
        /// Sets the style for the section's answer grid table 
        /// </summary>
        [
        Category("Styles"),
        DefaultValue(null),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public Style SectionGridAnswersStyle
        {
            get { return _sectionGridAnswersStyle == null ? new Style() : _sectionGridAnswersStyle; }
            set { _sectionGridAnswersStyle = value; }
        }

        /// <summary>
        /// how many section is the user allowed
        /// to create in this question
        /// </summary>
        public int MaxSections
        {
            get { return _maxSections; }
            set { _maxSections = value; }
        }

        /// <summary>
        /// How the user can create sections 
        /// </summary>
        public RepeatableSectionMode RepeatMode
        {
            get { return _repeatMode; }
            set { _repeatMode = value; }
        }

        /// <summary>
        /// Text show to the user to add a new section
        /// </summary>
        public string AddSectionLinkText
        {
            get
            {
                return _addSectionLinkText == null ?
                ResourceManager.GetString("AddSectionLinkText", LanguageCode) : _addSectionLinkText;
            }
            set { _addSectionLinkText = value; }
        }

        /// <summary>
        /// Text show to the user to delete a section
        /// </summary>
        public string DeleteSectionLinkText
        {
            get
            {
                return _deleteSectionLinkText == null ?
                ResourceManager.GetString("DeleteSectionLinkText", LanguageCode) : _deleteSectionLinkText;
            }
            set { _deleteSectionLinkText = value; }
        }

        /// <summary>
        /// Text show to the user to edit a section in the grid
        /// </summary>
        public string EditSectionLinkText
        {
            get
            {
                return _editSectionLinkText == null ?
                ResourceManager.GetString("EditSectionLinkText", LanguageCode) : _editSectionLinkText;
            }
            set { _editSectionLinkText = value; }
        }

        /// <summary>
        /// Text show to the user to edit a section in the grid
        /// </summary>
        public string UpdateSectionLinkText
        {
            get
            {
                return _updateSectionLinkText == null ?
                ResourceManager.GetString("UpdateSectionLinkText", LanguageCode) : _updateSectionLinkText;
            }
            set { _updateSectionLinkText = value; }
        }

        /// <summary>
        /// Cancel button caption
        /// </summary>
        public string CancelButtonText
        {
            get
            {
                return _cancelButtonText ?? ResourceManager.GetString("CancelButtonText", LanguageCode);
            }
            set { _cancelButtonText = value; }
        }

        /// <summary>
        /// Enable server side validation of grid answer section items
        /// </summary>
        public bool EnableGridSectionServerSideValidation
        {
            get { return _enableGridSectionServerSideValidation; }
            set { _enableGridSectionServerSideValidation = value; }
        }

        /// <summary>
        /// Enable client side validation of grid answer section items
        /// </summary>
        public bool EnableGridSectionClientSideValidation
        {
            get { return _enableGridSectionClientSideValidation; }
            set { _enableGridSectionClientSideValidation = value; }
        }

        /// <summary>
        /// Number of sections owned by this question
        /// </summary>
        public int SectionCount
        {
            get
            {
                if (ViewState["SectionCount"] != null)
                {
                    return int.Parse(ViewState["SectionCount"].ToString());
                }
                else
                {
                    ViewState["SectionCount"] = GetSectionCountFromState();
                }

                return int.Parse(ViewState["SectionCount"].ToString());
            }

            set { ViewState["SectionCount"] = value; }
        }

        /// <summary>
        /// Answers that will be shown in the section's 
        /// grid overview
        /// </summary>
        public int[] GridAnswers
        {
            get { return _gridAnswers; }

            set { _gridAnswers = value; }
        }

        /// <summary>
        /// In which state is the current grid
        /// </summary>
        protected SectionGridMode GridMode
        {
            get
            {
                if (ViewState["SectionGridMode"] == null)
                {
                    ViewState["SectionGridMode"] = SectionGridMode.None;
                }

                return (SectionGridMode)ViewState["SectionGridMode"];
            }

            set { ViewState["SectionGridMode"] = value; }
        }

        /// <summary>
        /// Section Uids must be stored between
        /// postbacks to make sure that the section 
        /// control gets the correct uid back
        /// </summary>
        protected ArrayList SectionUids
        {
            get
            {
                if (ViewState["SectionUids"] == null)
                {
                    ViewState["SectionUids"] = new ArrayList();
                }

                return (ArrayList)ViewState["SectionUids"];
            }

            set { ViewState["SectionUids"] = value; }

        }

        /// <summary>
        /// Style for the section's option row
        /// </summary>
        public Style SectionOptionStyle
        {
            get { return _sectionOptionStyle == null ? _sectionOptionStyle = new Style() : _sectionOptionStyle; }
            set { _sectionOptionStyle = value; }
        }

        /// <summary>
        /// Contains all sections and section options
        /// </summary>
        protected Table SectionTable
        {
            get { return _sectionTable; }
            set { _sectionTable = value; }
        }

        /// <summary>
        /// To which section are the new answer 
        /// in the grid be assigned
        /// </summary>
        public int TargetSection
        {
            get
            {
                if (ViewState["TargetSection"] == null)
                {
                    ViewState["TargetSection"] = -1;
                }

                return int.Parse(ViewState["TargetSection"].ToString());
            }
            set { ViewState["TargetSection"] = value; }
        }

        /// <summary>
        /// Compose all answer sections into a question
        /// </summary>
        protected override void CreateChildControls()
        {
            // Clears all childs controls 
            Controls.Clear();
            BuildQuestion();
        }

        /// <summary>
        /// Build the question layout with its 
        /// child controls
        /// </summary>
        virtual protected void BuildQuestion()
        {

            Controls.Add(QuestionTable);

            // Build default layout
            TableCell questionTableCell = new TableCell();  // default layout table cell
            TableRow questionTableRow = new TableRow();    // default layout table row

            string requiredMarkerHtml = string.Empty;
            if (ValidationMark.Length != 0 &&
           (MinSelectionRequired > 0 || MaxSelectionAllowed > 0))
            {
                //requiredMarkerHtml = string.Format("<span class='{0}'>&nbsp;{1}</span>",ValidationMarkStyle.CssClass.ToString(),ValidationMark);
                requiredMarkerHtml = string.Format("&nbsp;<span class='{0}'></span>", ValidationMarkStyle.CssClass.ToString());
            }

            // Set question's text
            Label questionLabel = new Label();
            if (QuestionNumber > 0)
            {
                // JJ Div to enclose the number and another div for text. Inline style is used because that is always necessary.can be moved into style sheet
                questionLabel.Text = string.Format(@"<div style='float:left;' class='question-number-div'>{0}.&nbsp;
              </div><div class='question-text-div'>{1}{2}</div>", QuestionNumber, Text, requiredMarkerHtml);
                //TODO (THis is not a real TODO- just a makrker)  Question Number align subsequent lines with Text and not number
            }
            else
            {
                //questionLabel.Text = Text;
                questionLabel.Text = string.Format(@"<div class='question-text-div'>{0}{1}</div>", Text, requiredMarkerHtml);
            }
            questionTableCell.Controls.Add(questionLabel);
            /*TODO JJ Commented out introducing span above after the question text
          if (ValidationMark.Length != 0 && 
            (MinSelectionRequired > 0 || MaxSelectionAllowed > 0))
          {
            Label validationMark = new Label();
            validationMark.Text = ValidationMark;
            validationMark.ControlStyle.CopyFrom(ValidationMarkStyle);
            questionTableCell.Controls.Add(validationMark);
          }
             * */
            questionTableRow.ControlStyle.CopyFrom(QuestionStyle);

            // Creates the row
            questionTableRow.Cells.Add(questionTableCell);
            QuestionTable.Rows.Add(questionTableRow);

            // Add the section table to the tree to get correct uid for children
            //_sectionTable.CellSpacing = 0;
            //_sectionTable.CellPadding = 2;
            //_sectionTable.Width = Unit.Percentage(100);
            QuestionTable.Rows.Add(BuildRow(_sectionTable, AnswerStyle));

            // Create grid with current answers and section only to add a new entry
            if (RepeatMode == RepeatableSectionMode.GridAnswers)
            {
                BuildSingleSection();
            }
            // Create all sections
            else
            {
                BuildMultipleSections();
            }


            //QuestionTable.Width = Unit.Percentage(100);
            //QuestionTable.CellSpacing = 0;
            //QuestionTable.CellPadding = 2;
        }

        /// <summary>
        /// Creates the section's container
        /// </summary>
        virtual protected void BuildMultipleSections()
        {
            for (int i = 0; i <= SectionCount; i++)
            {
                AddSection(i, GetSectionUid(i));
            }
        }

        /// <summary>
        /// Builds a section option table for the 
        /// given section number
        /// </summary>
        virtual protected Table GetSectionOptions(int sectionNumber, int sectionUid)
        {
            Table sectionOptionTable = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;
            TableRow sectionOptionRow = new TableRow();
            TableCell sectionOptionCell = new TableCell();

            if (MaxSections == 0 || sectionNumber + 1 < MaxSections)
            {
                sectionOptionCell = new TableCell();
                LinkButton addSectionAfterButton = new LinkButton();
                addSectionAfterButton.ControlStyle.CopyFrom(SectionOptionStyle);
                addSectionAfterButton.Text = AddSectionLinkText;
                addSectionAfterButton.ID = " AddSectionAfterButton" + sectionUid;
                addSectionAfterButton.Command += new CommandEventHandler(AddSectionButton_Click);
                addSectionAfterButton.CommandArgument = (sectionNumber + 1).ToString();
                addSectionAfterButton.Enabled = RenderMode != ControlRenderMode.ReadOnly;
                sectionOptionCell.Controls.Add(addSectionAfterButton);
                sectionOptionRow.Cells.Add(sectionOptionCell);
            }

            if (sectionNumber > 0)
            {
                sectionOptionCell = new TableCell();

                if (MaxSections == 0 || sectionNumber + 1 < MaxSections)
                {
                    //html to separate add and delete linkbuttons                     
                    sectionOptionCell.Controls.Add(new LiteralControl("<div style='position:relative;top:10px; left: -7px; line-height: 0px;'>&ndash; &nbsp;</div>"));
                }

                LinkButton removeSectionButton = new LinkButton();
                removeSectionButton.ControlStyle.CopyFrom(SectionOptionStyle);
                removeSectionButton.Text = DeleteSectionLinkText;
                removeSectionButton.ID = " RemoveSectionButton" + sectionUid;
                removeSectionButton.CommandArgument = sectionNumber.ToString();
                removeSectionButton.Command += new CommandEventHandler(RemoveSectionButton_Command);
                sectionOptionCell.Controls.Add(removeSectionButton);
                sectionOptionRow.Cells.Add(sectionOptionCell);
            }

            sectionOptionTable.Rows.Add(sectionOptionRow);
            sectionOptionTable.ControlStyle.CopyFrom(SectionOptionStyle);
            //sectionOptionTable.CellSpacing = 0;
            //sectionOptionTable.CellPadding = 2;
            return sectionOptionTable;
        }

        /// <summary>
        /// Adds / inserts a new full answer section
        /// </summary>
        protected virtual void AddSectionButton_Click(object sender, CommandEventArgs e)
        {
            if (MaxSections == 0 || SectionCount + 1 < MaxSections)
            {
                int sectionNumber = int.Parse(e.CommandArgument.ToString());
                if (sectionNumber > SectionCount)
                {
                    SectionCount++;
                    AddSection(SectionCount, GetSectionUid(SectionCount));
                }
                else
                {
                    InsertSection(sectionNumber);
                    SectionCount++;
                }
            }
        }

        /// <summary>
        /// Inserts a new section at the given 
        /// section number
        /// </summary>
        virtual protected void InsertSection(int sectionNumber)
        {
            // increment all user answers section numbers
            for (int i = SectionCount; i >= sectionNumber; i--)
            {
                ChangeStateAnswersSection(i, i + 1);
            }

            // Update web control's sections number
            for (int i = sectionNumber; i < _sections.Count; i++)
            {
                _sections[i].SectionNumber = _sections[i].SectionNumber + 1;
            }

            // Insert a new Uid for the section
            SectionUids.Insert(sectionNumber, GetUidForSection());

            // Creates a new section
            Section section = CreateSection(sectionNumber, GetSectionUid(sectionNumber));
            _sections.Insert(sectionNumber, section);
            _sectionTable.Rows.AddAt((sectionNumber) * 2, BuildRow(section, null));
            _sectionTable.Rows.AddAt((sectionNumber) * 2, BuildRow(GetSectionOptions(sectionNumber, section.SectionUid), SectionOptionStyle));
        }


        /// <summary>
        /// Returns a random unique ID
        /// </summary>
        /// <returns>uId</returns>
        private int GetUidForSection()
        {
            Random r = new Random();

            int uId = r.Next(90000); //no larger than 90000

            while (UidExists(uId))
            {
                uId = r.Next(90000); //no smaller than 1, no larger than 90000
            }

            return uId;

        }

        /// <summary>
        /// Checks if the Uid already exists in the section
        /// Uids collection to avoid duplicates
        /// </summary>
        /// <param name="uId"></param>
        /// <returns>true or false</returns>
        private bool UidExists(int uId)
        {
            for (int i = 0; i < SectionUids.Count; i++)
            {
                if (SectionUids[i].ToString() == uId.ToString())
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Adds a new section using the provided section Uid
        /// </summary>
        virtual protected void AddSection(int sectionNumber, int sectionUid)
        {
            Section section = CreateSection(sectionNumber, sectionUid);
            _sections.Add(section);

            if (RepeatMode == RepeatableSectionMode.FullAnswers)
            {
                _sectionTable.Rows.Add(BuildRow(GetSectionOptions(sectionNumber, section.SectionUid), SectionOptionStyle));
                _sectionTable.Rows.Add(BuildRow(section, null));

            }
            else if (RepeatMode == RepeatableSectionMode.GridAnswers)
            {
                _sectionTable.Rows.Add(BuildRow(section, null));
            }
            else
            {
                _sectionTable.Rows.Add(BuildRow(section, null));
            }

        }

        /// <summary>
        /// Deletes a section from the question
        /// </summary>
        protected virtual void RemoveSectionButton_Command(object sender, CommandEventArgs e)
        {
            int deletedSectionNumber = int.Parse(e.CommandArgument.ToString());
            RemoveSection(deletedSectionNumber);
        }

        /// <summary>
        /// Creates and adds a new section 
        /// to the question
        /// </summary>
        private void RemoveSection(int sectionNumber)
        {
            // Deletes any answer left by the user
            DeleteStateAnswersForSection(sectionNumber);

            // Decrement all user answers section numbers
            for (int i = sectionNumber + 1; i < SectionCount; i++)
            {
                ChangeStateAnswersSection(i, i - 1);
            }

            // Update local section count and remove the Guid
            // of the unwated question
            SectionCount = SectionCount - 1;
            SectionUids.RemoveAt(sectionNumber);

            // Remove unwanted section
            _sectionTable.Rows.RemoveAt((sectionNumber * 2));
            _sectionTable.Rows.RemoveAt((sectionNumber * 2));

            // Update web control's sections number
            for (int i = sectionNumber; i < _sections.Count; i++)
            {
                _sections[i].SectionNumber = _sections[i].SectionNumber - 1;
            }
        }

        /// <summary>
        /// Builds a answer's overview datagrid and 
        /// if required show an "add new" section
        /// </summary>
        private void BuildSingleSection()
        {
            _sectionGrid = GetSectionAnswersGrid();
            _sectionGrid.ID = "SectionGrid" + QuestionId;
            _sectionGrid.QuestionId = QuestionId;
            _sectionGrid.AddSection += new SectionAnswersEventHandler(AnswersGrid_AddSection);
            _sectionGrid.DeleteSection += new SectionAnswersEventHandler(AnswersGrid_DeleteSection);
            _sectionGrid.EditSection += new SectionAnswersEventHandler(AnswersGrid_EditSection);
            _sectionGrid.AddSectionLinkText = AddSectionLinkText;
            _sectionGrid.DeleteSectionLinkText = DeleteSectionLinkText;
            _sectionGrid.SectionGridAnswersHeaderStyle = SectionGridAnswersHeaderStyle;
            _sectionGrid.SectionGridAnswersItemStyle = SectionGridAnswersItemStyle;
            _sectionGrid.SectionGridAnswersAlternatingItemStyle = SectionGridAnswersAlternatingItemStyle;
            _sectionGrid.SectionGridAnswersStyle = SectionGridAnswersStyle;
            _sectionGrid.EditSectionLinkText = EditSectionLinkText;
            _sectionGrid.RenderMode = RenderMode;
            _sectionGrid.MaxSections = MaxSections;
            _sectionGrid.GridAnswers = _gridAnswers;

            if (_sectionGrid != null)
            {
                QuestionTable.Rows.Add(BuildRow(_sectionGrid, null));
            }

            // Assign any previous posted answers to the grid
            GridAnswerDataCollection gridAnswers = GetGridVoterAnswers();
            if (gridAnswers != null)
            {
                _sectionGrid.GridVoterAnswers = gridAnswers;
            }

            bool firstAddNewSection = TargetSection == -1 && _sectionGrid.GridVoterAnswers.Count == 0,
               nextAddNewSection =
                TargetSection > -1 && _sectionGrid.GridVoterAnswers.Count > 0 ||
                TargetSection == 0 && _sectionGrid.GridVoterAnswers.Count == 0;

            // Do we need to show a new section space
            if (firstAddNewSection || nextAddNewSection)
            {
                // Is this the first time ever that we show an add new section space 
                if (_sectionGrid.GridVoterAnswers.Count == 0)
                {
                    GridMode = SectionGridMode.AddNew;
                }

                AddSection(-1, 0);
                AddSubmitSectionButtons(GridMode == SectionGridMode.Edit);
            }
        }

        /// <summary>
        /// Parse the answer state and returns the 
        /// answers of this question
        /// </summary>
        /// <returns></returns>
        protected virtual GridAnswerDataCollection GetGridVoterAnswers()
        {
            GridAnswerDataCollection gridAnswers = null;

            if (VoterAnswersState != null)
            {
                VoterAnswersData.VotersAnswersRow[] answerState =
                  (VoterAnswersData.VotersAnswersRow[])VoterAnswersState.Select("QuestionId = " + QuestionId);
                if (answerState != null && answerState.Length > 0)
                {
                    gridAnswers = new GridAnswerDataCollection();
                    for (int i = 0; i < answerState.Length; i++)
                    {
                        gridAnswers.Add(new GridAnswerData(answerState[i].QuestionId, answerState[i].AnswerId, answerState[i].SectionNumber, answerState[i].AnswerText, (AnswerTypeMode)answerState[i].TypeMode));
                    }
                }
            }

            return gridAnswers;
        }


        /// <summary>
        /// A new section has been requested on the grid.
        /// Show a "new section" area
        /// </summary>
        protected virtual void AnswersGrid_AddSection(object sender, SectionAnswersItemEventArgs e)
        {
            _sectionTable.Controls.Clear();
            AddSection(-1, 0);
            SectionCount = 0;
            TargetSection = int.Parse(e.SectionNumber.ToString());
            AddSubmitSectionButtons(false);
            GridMode = SectionGridMode.AddNew;
        }

        /// <summary>
        /// A new section has been requested for edit
        /// Show a "new section" area with the section's answers
        /// </summary>
        protected virtual void AnswersGrid_EditSection(object sender, SectionAnswersItemEventArgs e)
        {
            _sectionTable.Controls.Clear();

            if (e.SectionAnswers != null)
            {
                PostedAnswerDataCollection postedAnswers = new PostedAnswerDataCollection();
                foreach (GridAnswerData answer in e.SectionAnswers)
                {
                    if (VoterAnswersState == null)
                    {
                        VoterAnswersData voterAnswersData = new VoterAnswersData();
                        voterAnswersData.EnforceConstraints = false;
                        VoterAnswersState = voterAnswersData.VotersAnswers;
                    }

                    VoterAnswersData.VotersAnswersRow voterAnswer = VoterAnswersState.NewVotersAnswersRow();
                    voterAnswer.AnswerId = answer.AnswerId;
                    voterAnswer.QuestionId = QuestionId;
                    voterAnswer.SectionNumber = -1;
                    voterAnswer.AnswerText = answer.FieldText;
                    voterAnswer.VoterId = -1;
                    VoterAnswersState.AddVotersAnswersRow(voterAnswer);
                }
            }

            // Dont use any default answers as we have 
            // setup an existing answer set
            EnableAnswersDefault = false;

            AddSection(-1, 0);
            SectionCount = 0;
            TargetSection = int.Parse(e.SectionNumber.ToString());
            AddSubmitSectionButtons(true);
            GridMode = SectionGridMode.Edit;
        }



        /// <summary>
        /// A section has been removed on the grid.
        /// Update the target section number if any was setup or
        /// show a "new section" area if the last section has
        /// been removed on the grid
        /// </summary>
        protected virtual void AnswersGrid_DeleteSection(object sender, SectionAnswersItemEventArgs e)
        {
            int deletedSection = int.Parse(e.SectionNumber.ToString());

            // Is the deleted section being in edit mode ?
            if (GridMode == SectionGridMode.Edit && TargetSection == deletedSection)
            {
                //Removes it
                SectionCount = -1;
                TargetSection = -1;
                GridMode = SectionGridMode.None;
                _sectionTable.Controls.Clear();

                // Did the user delete the last section ?
                if (GetSectionCountFromAnswers(_postedAnswers) < 1)
                {
                    AddSection(-1, 0);
                    AddSubmitSectionButtons(GridMode == SectionGridMode.Edit);
                    SectionCount = 0;
                    TargetSection = 0;
                }

            }
            else
            {
                // Is an add section is already displayed ?
                if (_sections.Count > 0)
                {
                    // Is the target section 
                    if (TargetSection > 0 && TargetSection >= deletedSection)
                    {
                        TargetSection--;

                        // Updates the button arguments
                        AddSubmitSectionButtons(GridMode == SectionGridMode.Edit);
                    }
                }
                else
                {
                    // Did the user delete the last section ?
                    if (GetSectionCountFromAnswers(_postedAnswers) < 1)
                    {
                        AddSection(-1, 0);
                        AddSubmitSectionButtons(GridMode == SectionGridMode.Edit);
                        SectionCount = 0;
                        TargetSection = 0;
                    }
                }
            }
        }


        private void AddSubmitSectionButtons(bool editMode)
        {
            _buttonsPlaceHolder.Controls.Clear();
            Button cancelSectionButton = new Button();
            Button submitSectioButton;
            if (editMode)
            {
                submitSectioButton = _updateSectionButton;
                submitSectioButton.ID = "SubmitSectionButton";
                submitSectioButton.Text = UpdateSectionLinkText;
                submitSectioButton.CssClass = CssXmlManager.GetString("AnswerUploadButton");
                submitSectioButton.Click += new EventHandler(UpdateSectionButton_Click);
            }
            else
            {
                submitSectioButton = _addSectionButton;
                submitSectioButton.Text = AddSectionLinkText;
                submitSectioButton.CssClass = CssXmlManager.GetString("AnswerUploadButton");
                submitSectioButton.Click += new EventHandler(SubmitSectionButton_Click);
            }

            // Does the section generate client side validation scripts
            if (EnableGridSectionClientSideValidation && _sections[0].GeneratesClientSideScript())
            {
                submitSectioButton.Attributes.Add("OnClick", string.Format("return {0}{1}();",
                  GlobalConfig.QuestionValidationFunction, _sections[0].UniqueID.Replace(":", "_")));
            }

            submitSectioButton.Enabled = RenderMode != ControlRenderMode.ReadOnly;

            _buttonsPlaceHolder.Controls.Add(submitSectioButton);

            if (_sectionGrid.GridVoterAnswers.Count > 0)
            {
                cancelSectionButton.ID = "CancelSectionButton";
                cancelSectionButton.Text = CancelButtonText;
                cancelSectionButton.CssClass = CssXmlManager.GetString("AnswerUploadButton");
                cancelSectionButton.Click += new EventHandler(CancelSectionButton_Click);
                _buttonsPlaceHolder.Controls.Add(cancelSectionButton);
            }

            _sectionTable.Rows.Add(BuildRow(_buttonsPlaceHolder, null));
        }

        /// <summary>
        /// Add new posted answers to the section grid     
        /// </summary>
        private void SubmitSectionButton_Click(object sender, EventArgs e)
        {
            // Can we store the section's answer into the temp question datagrid store?
            if (!EnableGridSectionServerSideValidation || (!IsSelectionOverflow && !IsSelectionRequired && !HasInvalidAnswers
              && _sectionGrid != null))
            {
                ReBindGrid();
            }

            // Do we need to check for validation errors ?
            if (EnableGridSectionServerSideValidation)
            {
                EnableServerSideValidation = true;
            }
        }

        /// <summary>
        /// Updates section grid with the posted answers
        /// </summary>
        private void UpdateSectionButton_Click(object sender, EventArgs e)
        {
            if (!EnableGridSectionServerSideValidation || (!IsSelectionOverflow && !IsSelectionRequired && !HasInvalidAnswers
              && _sectionGrid != null))
            {
                // Makes sure no answers are left to avoid duplicates
                DeleteSectionAnswers(TargetSection, _postedAnswers);

                // "Insert" updated answers
                SwitchAnswerSectionNumber(-1, TargetSection, _postedAnswers);

                ReBindGrid();
            }

            // Do we need to check for validation errors ?
            if (EnableGridSectionServerSideValidation)
            {
                EnableServerSideValidation = true;
            }
        }


        /// <summary>
        /// Cancel the "new section" area
        /// </summary>
        private void CancelSectionButton_Click(object sender, EventArgs e)
        {
            SectionCount = -1;
            TargetSection = -1;
            GridMode = SectionGridMode.None;
            _sectionTable.Controls.Clear();
        }

        /// <summary>
        /// Rebind grid based on posted answers
        /// </summary>
        private void ReBindGrid()
        {
            // Clear all items and avoid duplicates in the grid as 
            // they have already been posted back and are available 
            // in the _questionEventArgs.Answers
            _sectionGrid.GridVoterAnswers.Clear();

            // add answers to the grid
            foreach (PostedAnswerData postedAnswer in _postedAnswers)
            {
                _sectionGrid.GridVoterAnswers.Add(new GridAnswerData(QuestionId, postedAnswer.AnswerId, postedAnswer.SectionNumber, postedAnswer.FieldText, postedAnswer.TypeMode));
            }

            if (_sectionGrid.GridVoterAnswers.Count > 0)
            {
                SectionCount = -1;
                TargetSection = -1;
                GridMode = SectionGridMode.None;
                _sectionTable.Controls.Clear();
                _sectionGrid.Controls.Clear();
                _sectionGrid.BindSectionGrid();
            }

        }

        /// <summary>
        /// Handles posted answers and make sure
        /// to post answers if there aren't any in the section 
        /// or delete all unvalidated answers if answers exist
        /// in the section
        /// </summary>
        override protected void PostedAnswersHandler(PostedAnswerDataCollection answers)
        {
            _postedAnswers = answers;

            if (RepeatMode == RepeatableSectionMode.GridAnswers)
            {
                int sectionCount = GetSectionCountFromAnswers(answers);

                // if update, add modes and update, add buttons as not been 
                // used or at least one section already exisits
                // delete all updated answers
                if ((GridMode == SectionGridMode.Edit &&
                  Context.Request[_updateSectionButton.UniqueID] == null) ||
                  (GridMode == SectionGridMode.AddNew &&
                  Context.Request[_addSectionButton.UniqueID] == null &&
                  sectionCount >= 0))
                {
                    // Delete all answers as we dont want to post unvalidated answers
                    DeleteSectionAnswers(-1, answers);
                }
                // are we currently add an existing section ?
                else if (GridMode == SectionGridMode.AddNew)
                {
                    // Reorder the posted answer sections based on
                    // the current target section number
                    OrderTargetSectionAnswers(TargetSection, answers);
                }
            }
        }

        /// <summary>
        /// Removes all answers related to the target section number
        /// </summary>
        protected void DeleteSectionAnswers(int targetSection, PostedAnswerDataCollection answers)
        {
            for (int i = answers.Count - 1; i >= 0; i--)
            {
                // Find and update the section number
                if (answers[i].SectionNumber == targetSection)
                {
                    answers.RemoveAt(i);
                }
            }
        }


        /// <summary>
        /// Reorder the posted section in the correct
        /// order based on the target section's new section number
        /// new section answers are marked with a -1 sectionnumber
        /// </summary>
        /// <param name="targetSection"></param>
        protected void OrderTargetSectionAnswers(int targetSection, PostedAnswerDataCollection answers)
        {
            int sectionCount = GetSectionCountFromAnswers(answers);

            // Is this the first section ever posted ?
            if ((targetSection == -1 || targetSection == 0) && sectionCount == -1)
            {
                foreach (PostedAnswerData postedAnswer in answers)
                {
                    // Find and update the new section posted answers
                    if (postedAnswer.SectionNumber == -1)
                    {
                        postedAnswer.SectionNumber = 0;
                    }
                }

            }
            else if (targetSection != -1 && targetSection <= sectionCount)
            {
                // increment all user answers section numbers
                for (int i = sectionCount; i >= targetSection; i--)
                {
                    SwitchAnswerSectionNumber(i, i + 1, answers);
                }

            }

            if (targetSection != -1)
            {
                // Inserts the new section answers
                foreach (PostedAnswerData postedAnswer in answers)
                {
                    // Find and update the new section posted answers
                    if (postedAnswer.SectionNumber == -1)
                    {
                        postedAnswer.SectionNumber = targetSection;
                    }
                }
            }

        }

        /// <summary>
        /// Returns the section based count on the 
        /// current posted answer's data, doesnt include
        /// the count of any "add new section" only counted
        /// are the section already posted, validated and stored in
        /// the grid
        /// </summary>
        public int GetSectionCountFromAnswers(PostedAnswerDataCollection postedAnswers)
        {
            int sectionCount = -1;
            if (postedAnswers != null)
            {
                for (int i = 0; i < postedAnswers.Count; i++)
                {
                    if (postedAnswers[i].SectionNumber > sectionCount)
                    {
                        sectionCount = postedAnswers[i].SectionNumber;
                    }
                }
            }
            return sectionCount;
        }

        /// <summary>
        /// Change the all answers with the section number to the new section number
        /// </summary>
        private void SwitchAnswerSectionNumber(int sectionNumber, int newSectionNumber, PostedAnswerDataCollection answers)
        {
            // Inserts the new section answers
            foreach (PostedAnswerData postedAnswer in answers)
            {
                // Find and update the new section posted answers
                if (postedAnswer.SectionNumber == sectionNumber)
                {
                    postedAnswer.SectionNumber = newSectionNumber;
                }
            }
        }

        private void DeleteStateAnswersForSection(int sectionNumber)
        {
            if (VoterAnswersState != null)
            {
                VoterAnswersData.VotersAnswersRow[] answerState =
                  (VoterAnswersData.VotersAnswersRow[])VoterAnswersState.Select("QuestionId = " + QuestionId + " AND SectionNumber=" + sectionNumber);
                for (int i = 0; i < answerState.Length; i++)
                {
                    VoterAnswersState.RemoveVotersAnswersRow(answerState[i]);
                }
            }
        }

        private void ChangeStateAnswersSection(int oldSectionNumber, int newSectionNumber)
        {
            if (VoterAnswersState != null)
            {
                VoterAnswersData.VotersAnswersRow[] answerState =
                  (VoterAnswersData.VotersAnswersRow[])VoterAnswersState.Select("QuestionId = " + QuestionId + " AND SectionNumber=" + oldSectionNumber);
                for (int i = 0; i < answerState.Length; i++)
                {
                    answerState[i].SectionNumber = newSectionNumber;
                }
            }
        }

        /// <summary>
        /// Returns the exsiting Uid of the section
        /// if it doesnt exists creates a random
        /// UId and store it in the viewstate collection
        /// for futur postbacks
        /// </summary>
        virtual protected int GetSectionUid(int sectionNumber)
        {
            int sectionUid = -1;

            if (SectionUids.Count <= sectionNumber)
            {
                for (int i = SectionUids.Count; i <= sectionNumber; i++)
                {
                    sectionUid = GetUidForSection();
                    SectionUids.Add(sectionUid);
                }
            }
            else
            {
                sectionUid = int.Parse(SectionUids[sectionNumber].ToString());
            }

            return sectionUid;
        }

        /// <summary>
        /// Must returns the number of sections that 
        /// must be restored for this question, should 
        /// be based on the current answerstate
        /// </summary>
        /// <returns></returns>
        abstract protected int GetSectionCountFromState();

        /// <summary>
        /// Must return a new section
        /// populated with its answers or child questions
        /// </summary>
        abstract protected Section CreateSection(int sectionNumber, int sectionGuid);

        /// <summary>
        /// Must return a valid grid that will be use in 
        /// grid section mode. Null can be returned if no 
        /// grid can be generated
        /// </summary>
        abstract protected SectionAnswersGridItem GetSectionAnswersGrid();


        /// <summary>
        /// Notify subscriber that a script has been generated
        /// for the given section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Section_ClientScriptGenerated(object sender, QuestionItemClientScriptEventArgs e)
        {
            OnClientScriptGeneration(e);
        }

        private Table _sectionTable = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetSectPercentTable();//JJ;
        private SectionCollection _sections = new SectionCollection();
        private Style _sectionOptionStyle;
        private int _maxSections = 0;
        private RepeatableSectionMode _repeatMode = RepeatableSectionMode.None;
        private string _addSectionLinkText,
                _deleteSectionLinkText,
                _updateSectionLinkText,
                _editSectionLinkText,
                _cancelButtonText;

        private Button _addSectionButton = new Button(),
                _updateSectionButton = new Button();
        private PlaceHolder _buttonsPlaceHolder = new PlaceHolder();
        private PostedAnswerDataCollection _postedAnswers;

        SectionAnswersGridItem _sectionGrid;
        private Style _sectionGridAnswersStyle,
                _sectionGridAnswersHeaderStyle,
                _sectionGridAnswersAlternatingItemStyle,
                _sectionGridAnswersItemStyle;

        bool _enableGridSectionServerSideValidation = false,
            _enableGridSectionClientSideValidation = false;
        int[] _gridAnswers = new int[0];
    }
}
