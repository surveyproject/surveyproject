
namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    public interface ISurveyAsset
    {
      
       SurveyAssetData GetAllSurveyAssets(int SurveyId);

        void SurveyAssetAdd(int surveyId,string assetType,string name);
        void SurveyAssetDelete(int assetId);

    }
}

