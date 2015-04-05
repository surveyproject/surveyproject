namespace Votations.NSurvey.Data
{
    using System;

    /// <summary>
    /// Enumeration to determine the message conditional operator 
    /// that we need to use
    /// </summary>
    public enum MessageConditionalOperator
    {
        QuestionAnswer = 1,
        ScoredLess = 3,
        ScoreEquals = 2,
        ScoreGreater = 4,
        ScoreRange = 5
    }
}

