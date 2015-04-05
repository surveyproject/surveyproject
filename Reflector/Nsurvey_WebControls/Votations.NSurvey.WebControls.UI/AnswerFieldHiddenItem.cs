namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;

    /// <summary>
    /// A hidden field, mainly use to get / store default text
    /// values that were setup using the query string, 
    /// session or server variable values
    /// </summary>
    public class AnswerFieldHiddenItem : AnswerItem, IAnswerPublisher
    {
        protected TextBox _fieldTextBox = new TextBox();

        /// <summary>
        /// Create the "layout" and adds the textbox control to the 
        /// control tree
        /// </summary>
        protected override void CreateChildControls()
        {
            if (this.RenderMode == ControlRenderMode.Edit)
            {
                Literal child = new Literal();
                child.Text = this.Text;
                this.Controls.Add(child);
                this.Controls.Add(new LiteralControl("<br />"));
                this._fieldTextBox.Columns = 30;
                this._fieldTextBox.Text = this.DefaultText;
                this.Controls.Add(this._fieldTextBox);
            }
            this.OnAnswerPublisherCreated(new AnswerItemEventArgs(this.GetUserAnswers()));
        }

        /// <summary>
        /// Returns	the default answer text that was 
        /// filled with the correct template value specified
        /// by the user
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            PostedAnswerDataCollection userAnswers = this.GetUserAnswers();
            this.OnAnswerPublished(new AnswerItemEventArgs(userAnswers));
            return userAnswers;
        }

        /// <summary>
        /// Returns the answeritem user's answers
        /// </summary>
        protected virtual PostedAnswerDataCollection GetUserAnswers()
        {
            PostedAnswerDataCollection datas = new PostedAnswerDataCollection();
            if (this.RenderMode == ControlRenderMode.Edit)
            {
                if (this._fieldTextBox.Text.Length != 0)
                {
                    datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._fieldTextBox.Text, AnswerTypeMode.Publisher | AnswerTypeMode.Custom));
                    return datas;
                }
                return null;
            }
            PostedAnswerData postedAnswerData = new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this.DefaultText, AnswerTypeMode.Publisher | AnswerTypeMode.Custom);
            datas.Add(postedAnswerData);
            return datas;
        }
    }
}

