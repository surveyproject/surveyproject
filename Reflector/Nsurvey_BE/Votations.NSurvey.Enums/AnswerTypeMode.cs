namespace Votations.NSurvey.Data
{
    using System;

    [Serializable, Flags]
    public enum AnswerTypeMode
    {
        Custom = 4,
        DataSource = 8,
        ExtendedType = 0x200, //512
        Field = 2,
        Mandatory = 0x40, //64
        None = 0,
        Other = 3,
        Publisher = 0x10, //16
        RegExValidator = 0x80, //128
        Selection = 1,
        Subscriber = 0x20, //32
        Upload = 0x100, //256
        Slider = 0x400 //1024
    }
}

