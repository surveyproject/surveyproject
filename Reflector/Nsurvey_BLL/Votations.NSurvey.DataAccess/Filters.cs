namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the business rules that are used for a filter.
    /// </summary>
    public class Filters
    {
        public FilterData GetFilterById(int filterId)
        {
            FilterData filterById = FilterFactory.Create().GetFilterById(filterId);
            if (filterById.Filters.Rows.Count == 0)
            {
                throw new FilterNotFoundException();
            }
            return filterById;
        }

        public FilterData GetFilters(int surveyId)
        {
            return FilterFactory.Create().GetFilters(surveyId);
        }

        public FilterData GetFiltersByParent(int surveyId, int parentId)
        {
            return FilterFactory.Create().GetFiltersByParent(surveyId, parentId);
        }

        public FilterRuleData GetRulesForFilter(int filterId)
        {
            return FilterFactory.Create().GetRulesForFilter(filterId);
        }

        public FilterData GetEmptyFilter()
        {
            return FilterFactory.Create().GetEmptyFilter();
        }
    }
}

