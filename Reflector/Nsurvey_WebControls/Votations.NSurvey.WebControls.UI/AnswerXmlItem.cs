namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Helpers;

    /// <summary>
    /// Base class for the Xml based answer types
    /// </summary>
    public abstract class AnswerXmlItem : AnswerDataSourceItem
    {
        protected AnswerXmlItem()
        {
        }

        protected override void CreateChildControls()
        {
            XmlFileManager manager = new XmlFileManager(this.DataSource);
            this.GenerateXmlControl(manager.GetXmlAnswers(base.LanguageCode));
        }

        protected abstract void GenerateXmlControl(NSurveyDataSource xmlAnswers);
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
    }
}

