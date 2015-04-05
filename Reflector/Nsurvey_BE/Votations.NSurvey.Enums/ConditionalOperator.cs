namespace Votations.NSurvey.Data
{
    using System;

    /// <summary>
    /// Enumeration to determine the conditional operator that
    /// we need to use
    /// </summary>
    public enum ConditionalOperator
    {
        Equal = 1,
        NotEqual = 2,
        ScoredLess = 4,
        ScoreEquals = 3,
        ScoreGreater = 5,
        ScoreRange = 6
    }
}

