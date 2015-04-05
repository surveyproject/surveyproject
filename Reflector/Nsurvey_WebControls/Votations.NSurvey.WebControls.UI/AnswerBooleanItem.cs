namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Boolean checkbox item
    /// </summary>
    public class AnswerBooleanItem : AnswerItem, IAnswerPublisher
    {
        private CheckBox _boolCheckBox = new CheckBox();

        /// <summary>
        /// Creates the checkbox control "layout" and adds
        /// it to the overall control tree
        /// </summary>
        protected override void CreateChildControls()
        {
            if (this.ShowAnswerText)
            {
                if ((this.ImageUrl != null) && (this.ImageUrl.Length != 0))
                {
                    Image child = new Image();
                    child.ImageUrl = this.ImageUrl;
                    child.ImageAlign = ImageAlign.Middle;
                    child.ToolTip = this.Text;
                    this.Controls.Add(child);
                }
                else
                {
                   //JJ this.Controls.Add(new LiteralControl(this.Text));

                    Label label = new Label();
                    label.Text = this.Text;
                    label.CssClass = "AnswerTextRender";
                    this.Controls.Add(label);
                }
            }
            if (this.DefaultText != null)
            {
                bool flag = true;
                if (this.DefaultText == flag.ToString())
                {
                    this._boolCheckBox.Checked = true;
                }
            }
            this.Controls.Add(this._boolCheckBox);
            PostedAnswerDataCollection postedAnswers = new PostedAnswerDataCollection();
            postedAnswers.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._boolCheckBox.Checked.ToString(), AnswerTypeMode.Custom));
            this.OnAnswerPublisherCreated(new AnswerItemEventArgs(postedAnswers));
        }

        /// <summary>
        /// Returns the check box status to the event subscribers 
        /// once the survey's page get posted
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            PostedAnswerData data;
            bool flag;
            PostedAnswerDataCollection postedAnswers = new PostedAnswerDataCollection();
            if (this.Context.Request[this._boolCheckBox.UniqueID] != null)
            {
                flag = true;
                data = new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, flag.ToString(), AnswerTypeMode.Publisher | AnswerTypeMode.Custom);
            }
            else
            {
                flag = false;
                data = new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, flag.ToString(), AnswerTypeMode.Publisher | AnswerTypeMode.Custom);
            }
            postedAnswers.Add(data);
            this.OnAnswerPublished(new AnswerItemEventArgs(postedAnswers));
            return postedAnswers;
        }
    }
}

