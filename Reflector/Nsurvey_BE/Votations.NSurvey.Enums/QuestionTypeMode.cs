namespace Votations.NSurvey.Data
{
    using System;

    [Flags]
    public enum QuestionTypeMode
    {
        Answerable = 4,
        ChildQuestion = 8,
        Editable = 2,
        MultipleAnswers = 0x10,
        None = 0,
        Selectable = 1
    }
}

