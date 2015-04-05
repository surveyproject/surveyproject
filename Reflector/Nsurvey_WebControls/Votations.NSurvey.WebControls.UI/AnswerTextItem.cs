namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using Votations.NSurvey.Data;
    using System.Web.UI.WebControls;
    public class AnswerTextItem : AnswerItem
    {
        protected override void CreateChildControls()
        {
           //JJ this.Controls.Add(new LiteralControl(this.Text));
            Label label = new Label();
            label.Text = this.Text;
            label.CssClass = "AnswerTextRender";
            this.Controls.Add(label);
        }

        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            return null;
        }
    }
}

