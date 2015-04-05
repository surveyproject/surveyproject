namespace Votations.NSurvey.BusinessRules
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the business rules that are used for a filter.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Adds a new filter
        /// </summary>
        /// <param name="newFilter"></param>
        public void AddFilter(FilterData newFilter)
        {
            FilterFactory.Create().AddFilter(newFilter);
        }

        /// <summary>
        /// Adds a new filter rule
        /// </summary>
        /// <param name="newRule"></param>
        public void AddRule(FilterRuleData newRule)
        {
            FilterFactory.Create().AddRule(newRule);
        }

        /// <summary>
        /// Deletes the given filter
        /// </summary>
        /// <param name="filterId"></param>
        public void DeleteFilter(int filterId)
        {
            FilterFactory.Create().DeleteFilter(filterId);
        }

        /// <summary>
        /// Deletes the given filter rule
        /// </summary>
        /// <param name="ruleId"></param>
        public void DeleteRule(int ruleId)
        {
            FilterFactory.Create().DeleteRule(ruleId);
        }

        /// <summary>
        /// Update the filter options
        /// </summary>
        /// <param name="updatedFilter"></param>
        public void UpdateFilter(FilterData updatedFilter)
        {
            FilterFactory.Create().UpdateFilter(updatedFilter);
        }
    }
}

