namespace Votations.NSurvey.WebControls
{
    using System;
    using System.Web.UI.Design;

    /// <summary>
    /// Implements the advanced designer services
    /// </summary>
    public class SurveyBoxDesigner : ControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            ((SurveyBox) base.Component).RenderAtDesignTime();
            return base.GetDesignTimeHtml();
        }
    }
}

