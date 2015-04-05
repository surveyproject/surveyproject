namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Interface for the Filters / rules.
    /// </summary>
    public interface IFilter
    {
        void AddFilter(FilterData newFilter);
        void AddRule(FilterRuleData newRule);
        void DeleteFilter(int filterId);
        void DeleteRule(int ruleId);
        FilterData GetFilterById(int filterId);
        FilterData GetFilters(int surveyId);
        FilterData GetFiltersByParent(int surveyId, int parentId);
        FilterRuleData GetRulesForFilter(int filterId);
        void UpdateFilter(FilterData updatedFilter);
        FilterData GetEmptyFilter();
    }
}

