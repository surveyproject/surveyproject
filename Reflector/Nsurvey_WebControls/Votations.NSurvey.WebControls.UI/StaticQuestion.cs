namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;

    /// <summary>
    /// Renders a the question as basic static text
    /// </summary>
    public class StaticQuestion : QuestionItem
    {
        protected override void CreateChildControls()
        {
            this.Controls.Add(new LiteralControl(base.Text));
        }
    }
}

