namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    public interface ISurveyLayout
    {
        SurveyLayoutData SurveyLayoutGet(int SurveyId,string LanguageCode=null);
        void SurveyLayoutUpdate(SurveyLayoutData sld, string LanguageCode = null);
   
    }
}

