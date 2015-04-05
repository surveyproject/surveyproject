namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;

    public class AnswerSelectionItem : AnswerItem
    {
        private bool _selected = false;
        private AnswerSelectionMode _selectionMode;
        private AnswerTypeMode _typeMode;
        private string _uniqueGroupId = null;
        protected ISelectionControl SelectionControl = null;

        protected override void CreateChildControls()
        {
            this.GenerateSelectionControl(this.Controls);
        }

        /// <summary>
        /// Generates a selection control (Radio, checkbox) and adds
        /// it to the container
        /// </summary>
        /// <param name="container"></param>
        /// <returns>true if a selection control was added</returns>
        protected bool GenerateSelectionControl(ControlCollection container)
        {
            if (this.SelectionMode == AnswerSelectionMode.Radio)
            {
                SelectionRadioButton child = new SelectionRadioButton();
                child.GroupName = string.Format("{0}:{1}{2}", this._uniqueGroupId, GlobalConfig.GroupName, this.QuestionId);
                child.Checked = this.Selected;
                container.Add(child);
                if (this.ShowAnswerText)
                {
                    if ((this.ImageUrl != null) && (this.ImageUrl.Length != 0))
                    {
                        Image image = new Image();
                        image.ImageUrl = this.ImageUrl;
                        image.ImageAlign = ImageAlign.Middle;
                        image.ToolTip = this.Text;
                        container.Add(image);
                    }
                    else
                    {
                        child.Text = this.Text;//JJ
                    child.CssClass = "AnswerTextRender";
                    }
                }
                this.SelectionControl = child;
            }
            else if (this.SelectionMode == AnswerSelectionMode.CheckBox)
            {
                SelectionCheckBox box = new SelectionCheckBox();
                box.Checked = this.Selected;
                container.Add(box);
                if (this.ShowAnswerText)
                {
                    if ((this.ImageUrl != null) && (this.ImageUrl.Length != 0))
                    {
                        Image image2 = new Image();
                        image2.ImageUrl = this.ImageUrl;
                        image2.ImageAlign = ImageAlign.Middle;
                        image2.ToolTip = this.Text;
                        container.Add(image2);
                    }
                    else
                    {
                        box.Text = this.Text;//JJ
                        box.CssClass = "AnswerTextRender";
                    }
                }
                this.SelectionControl = box;
            }
            else if (this.SelectionMode == AnswerSelectionMode.ListItem)
            {
                AnswerListItem item = new AnswerListItem();
                item.Text = this.Text;//JJ
                item.CssClass = "AnswerTextRender";
                container.Add(item);
            }
            else if ((this.ImageUrl != null) && (this.ImageUrl.Length != 0))
            {
                Image image3 = new Image();
                image3.ImageUrl = this.ImageUrl;
                image3.ImageAlign = ImageAlign.Middle;
                image3.ToolTip = this.Text;
                container.Add(image3);
            }
            else
            {
                Literal literal = new Literal();
                literal.Text = this.Text;
                container.Add(literal);
            }
            return (this.SelectionControl != null);
        }

        /// <summary>
        /// Returns the answer if it was checked
        /// </summary>
        /// <returns></returns>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            PostedAnswerDataCollection datas = new PostedAnswerDataCollection();
            if ((this.SelectionMode == AnswerSelectionMode.Radio) && ((SelectionRadioButton) this.SelectionControl).IsChecked())
            {
                datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, null, AnswerTypeMode.Selection));
                return datas;
            }
            if ((this.SelectionMode == AnswerSelectionMode.CheckBox) && (this.Context.Request[((SelectionCheckBox) this.SelectionControl).UniqueID] != null))
            {
                datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, null, AnswerTypeMode.Selection));
                return datas;
            }
            return null;
        }

        public bool Selected
        {
            get
            {
                return this._selected;
            }
            set
            {
                this._selected = value;
            }
        }

        public AnswerSelectionMode SelectionMode
        {
            get
            {
                return this._selectionMode;
            }
            set
            {
                this._selectionMode = value;
            }
        }

        public AnswerTypeMode TypeMode
        {
            get
            {
                return this._typeMode;
            }
            set
            {
                this._typeMode = value;
            }
        }

        public string UniqueGroupId
        {
            get
            {
                return this._uniqueGroupId;
            }
            set
            {
                this._uniqueGroupId = value;
            }
        }
    }
}

