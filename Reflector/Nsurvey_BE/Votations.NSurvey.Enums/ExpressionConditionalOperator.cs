namespace Votations.NSurvey.Data
{
    using System;

    /// <summary>
    /// Enumeration to determine the conditional operator that
    /// we need to use to evalutate an expression
    /// </summary>
    public enum ExpressionConditionalOperator
    {
        Contains = 2,
        Equals = 1,
        GreaterThan = 3,
        LessThan = 4
    }
}

